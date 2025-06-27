
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

        public BugService(IEmployeeRepository employeeRepository,
                          IRepository<long, Bugs> bugsRepository)
        {
            _employeeRepository = employeeRepository;
            _bugsRepository = bugsRepository;
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

        public async Task<BugSubmission> SubmitBug(BugSubmission bugDto, string email)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(bugDto.Screenshot.FileName);
            string filePath = Path.Combine(uploadsFolder, fileName);


            var stream = new FileStream(filePath, FileMode.Create);
            await bugDto.Screenshot.CopyToAsync(stream);
            stream.Close();

            long submitterId = await GetBugSubmitterId(email);
            var bug = new Bugs
            {
                BugName = bugDto.BugName,
                SubmittedById = submitterId,
                Description = bugDto.Description,
                CvssScore = bugDto.CvssScore,
                SubmittedOn = DateTime.UtcNow,
                ResolvedAt = null,
                // Status = "ongoing",
               Status="Submitted",
                Screenshot = filePath

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
        

        private async Task<long> GetBugSubmitterId(string email)
        {
            var result = await _employeeRepository.GetEmployeeByEmail(email);
            return result.EmployeeId;
        }
    }
}