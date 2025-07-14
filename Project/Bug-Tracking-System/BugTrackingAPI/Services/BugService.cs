
using Azure.Storage.Blobs;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Repositories;

namespace BugTrackingAPI.Services
{
    public class BugService : IBugService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRepository<long, Bugs> _bugsRepository;
        // private readonly IScreenshotStorageService _screenshotStorage;
        private readonly BlobContainerClient? _containerClient;   // nullable when running locally
        private readonly bool _useAzure;

        public BugService(IEmployeeRepository employeeRepository,
                          IRepository<long, Bugs> bugsRepository,
                          IConfiguration config)
        {
            _employeeRepository = employeeRepository;
            _bugsRepository = bugsRepository;

            

            var connectionString = config["Storage:ConnectionString"];
            var containerName = config["Storage:ScreenshotsContainer"] ?? "screenshots";

            // decide once at start‑up
            _useAzure = !string.IsNullOrWhiteSpace(connectionString);

            if (_useAzure)
            {
                _containerClient = new BlobServiceClient(connectionString!)
                                   .GetBlobContainerClient(containerName);
                _containerClient.CreateIfNotExists();
            }
        }

        public async Task<IEnumerable<BugResponse>> GetAllBugsList()
        {
            var result = await _bugsRepository.GetAll();
            var bugs = result
            .Select(b => new BugResponse
            {
                BugId = b.BugId,
                BugName = b.BugName,
                Description = b.Description,
                Screenshot = b.Screenshot,
                CvssScore = b.CvssScore,
                SubmittedOn = b.SubmittedOn,
                ResolvedAt = b.ResolvedAt,
                Status = b.Status,
                SubmittedById = b.SubmittedById
            })
            .ToList();

            return bugs;
        }

        public async Task<BugResponse> GetBugStatus(long BugId)
        {
            var Bugdata = await _bugsRepository.Get(BugId);
            if (Bugdata == null)
            {
                throw new KeyNotFoundException("Invalid Bug Id");
            }
            var result = new BugResponse
            {
                BugId = Bugdata.BugId,
                BugName = Bugdata.BugName,
                Description = Bugdata.Description,
                Screenshot = Bugdata.Screenshot,
                CvssScore = Bugdata.CvssScore,
                SubmittedOn = Bugdata.SubmittedOn,
                ResolvedAt = Bugdata.ResolvedAt,
                Status = Bugdata.Status,
                SubmittedById = Bugdata.SubmittedById
            };
            return result;
        }

        // public async Task<BugSubmission> SubmitBug(BugSubmission bugDto, string email)
        // {
            // string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");
            // if (!Directory.Exists(uploadsFolder))
            //     Directory.CreateDirectory(uploadsFolder);

            // string fileName = Guid.NewGuid().ToString() + Path.GetExtension(bugDto.Screenshot.FileName);
            // string filePath = Path.Combine(uploadsFolder, fileName);


            // var stream = new FileStream(filePath, FileMode.Create);
            // await bugDto.Screenshot.CopyToAsync(stream);
            // stream.Close();

            // long submitterId = await GetBugSubmitterId(email);

            // var blobLink= await UploadScreenshot(bugDto.Screenshot,fileName);
            // var blobLink = "";
        //     string blobLink;
        //     try
        //     {
        //         blobLink = await UploadScreenshot(bugDto.Screenshot, fileName);
        //     }
        //     catch (Exception)
        //     {
        //         blobLink = filePath;
        //     }


        //     var bug = new Bugs
        //     {
        //         BugName = bugDto.BugName,
        //         SubmittedById = submitterId,
        //         Description = bugDto.Description,
        //         CvssScore = bugDto.CvssScore,
        //         SubmittedOn = DateTime.UtcNow,
        //         ResolvedAt = null,
        //         // Status = "ongoing",
        //         Status = "Submitted",
        //         Screenshot = blobLink

        //     };
        //     await _bugsRepository.Add(bug);
        //     return bugDto;

        // }
        public async Task<BugSubmission> SubmitBug(BugSubmission bugDto, string email)
        {
            var fileName   = $"{Guid.NewGuid()}{Path.GetExtension(bugDto.Screenshot.FileName)}";
            var screenshot = await StoreScreenshotAsync(bugDto.Screenshot, fileName);

            var submitterId = await GetBugSubmitterId(email);

            var bug = new Bugs
            {
                BugName       = bugDto.BugName,
                SubmittedById = submitterId,
                Description   = bugDto.Description,
                CvssScore     = bugDto.CvssScore,
                SubmittedOn   = DateTime.UtcNow,
                Status        = "Submitted",
                Screenshot    = screenshot
            };

            await _bugsRepository.Add(bug);
            return bugDto;
        }

        public async Task<IEnumerable<BugResponse>> GetAllBugsInSubmittedState()
        {
            var result = await _bugsRepository.GetAll();
            var sortedBugs = result.Where(e => e.Status == "Submitted").ToList();
            var bugs = sortedBugs
            .Select(b => new BugResponse
            {
                BugId = b.BugId,
                BugName = b.BugName,
                Description = b.Description,
                Screenshot = b.Screenshot,
                CvssScore = b.CvssScore,
                SubmittedOn = b.SubmittedOn,
                ResolvedAt = b.ResolvedAt,
                Status = b.Status,
                SubmittedById = b.SubmittedById
            })
            .ToList();

            return bugs;
        }

        public async Task<IEnumerable<BugResponse>> GetBugsReportedBy(long empId)
        {
            var result = await _bugsRepository.GetAll();

            if (result == null)
            {
                throw new Exception("No bugs found");
            }

            var sortedBugs = result.Where(e => e.SubmittedById == empId).ToList();

            if (sortedBugs == null)
            {
                throw new Exception("Invalid EmployeeId");
            }
            var bugs = sortedBugs
            .Select(b => new BugResponse
            {
                BugId = b.BugId,
                BugName = b.BugName,
                Description = b.Description,
                Screenshot = b.Screenshot,
                CvssScore = b.CvssScore,
                SubmittedOn = b.SubmittedOn,
                ResolvedAt = b.ResolvedAt,
                Status = b.Status,
                SubmittedById = b.SubmittedById
            })
            .ToList();

            return bugs;
        }

        private async Task<long> GetBugSubmitterId(string email)
        {
            var result = await _employeeRepository.GetEmployeeByEmail(email);
            return result.EmployeeId;
        }






        // public async Task<string> UploadScreenshot(IFormFile file, string filename)
        // {
        //     var blobClient = _containerClient.GetBlobClient(filename);
        //     await blobClient.UploadAsync(file.OpenReadStream(), overwrite: true);
        //     return blobClient.Uri.ToString(); // Can be private/SAS URL in real use

        // }
        
        private async Task<string> StoreScreenshotAsync(IFormFile file, string filename)
        {
            if (_useAzure && _containerClient is not null)
            {
                var blob = _containerClient.GetBlobClient(filename);
                await blob.UploadAsync(file.OpenReadStream(), overwrite: true);
                return blob.Uri.ToString();                   // Azure URL
            }
            else
            {
                // Local filesystem fallback
                var localFolder = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");
                Directory.CreateDirectory(localFolder);

                var localPath = Path.Combine(localFolder, filename);
                await using var fs = new FileStream(localPath, FileMode.Create);
                await file.CopyToAsync(fs);
                return localPath;                             // Local path
            }
        }

    }
}