
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using BugTrackingAPI.Repositories;

namespace BugTrackingAPI.Services
{
    public class BugManagementService : IBugManagementService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRepository<long, Bugs> _bugsRepository;
        private readonly IRepository<long, BugAssignment> _bugAssignment;
        private readonly IBugManagementRepository _bugManagementRepository;

        public BugManagementService(IEmployeeRepository employeeRepository,
                          IRepository<long, Bugs> bugsRepository,
                          IRepository<long, BugAssignment> bugAssignment,
                          IBugManagementRepository bugManagementRepository)
        {
            _employeeRepository = employeeRepository;
            _bugsRepository = bugsRepository;
            _bugAssignment = bugAssignment;
            _bugManagementRepository = bugManagementRepository;
        }

        public async Task<BugAssignmentResponse> AssignBugToDevs(long bugId, long devId, string email)
        {
            var CheckDuplicate = await _bugAssignment.GetAll();
            var isDuplicate = CheckDuplicate.Any(e => e.BugId == bugId && e.DevId == devId);
            if (isDuplicate)
            {
                throw new Exception("Bug Already Assigned");
            }
            var isBugResolved = await _bugsRepository.Get(bugId);
            if (isBugResolved == null)
            {
                throw new Exception("Invalid bug Id");
            }
            if (isBugResolved.Status == "Resolved")
            {
                throw new Exception(" Bug is fixed.");
            }
            if (isBugResolved.Status == "InProgress")
            {
                throw new Exception("Developer is working on the bug");
            }
            if (isBugResolved.Status == "Assigned")
            {
                throw new Exception("Bug already assigned");
            }

            var isDevAvailable = await _employeeRepository.Get(devId);

            if (isDevAvailable == null || isDevAvailable.Role != "Dev")
            {
                throw new Exception("Invalid Dev Id");
            }


            var AllAssignments = await _bugAssignment.GetAll();
            var devAssignments = AllAssignments.Where(a => a.DevId == devId && (a.ResolutionStatus.ToLower() == "Assigned" || a.ResolutionStatus.ToLower() == "InProgress"));
            var devBugs = await _bugManagementRepository.GetBugsAssignedToMe(devId);
            Console.WriteLine($"******Deb bugs{devBugs.ToList()}");
            Console.WriteLine($"******Deb bugs{devBugs.Where(b => (b.Status=="Assigned" || b.Status == "InProgress") && b.CvssScore >= 7 && b.CvssScore <= 10).ToList()}");
            int highPriorityBugsCount = devBugs.Where(b => (b.Status=="Assigned" || b.Status == "InProgress") && (b.CvssScore >= 7 && b.CvssScore <= 10)).Count();
            Console.WriteLine($"******highPriorityBugsCount : {highPriorityBugsCount}");

            var devAssignmentsCount = AllAssignments
            .Where(a => a.DevId == devId &&
                (a.ResolutionStatus.ToLower() == "Assigned" || a.ResolutionStatus.ToLower() == "InProgress")) //made changes in status names
            .Count();

            if (devAssignmentsCount >= 3)
            {
                throw new Exception("Developer is already working on 3 or more unresolved bugs.");
            }

            var currentBug = await _bugsRepository.Get(bugId);

            if (highPriorityBugsCount >= 1 && currentBug.CvssScore >=7 && currentBug.CvssScore<=10)
            {
                throw new Exception("A developer can be assigned to one high priority bug(CVSS Score 7 - 10).");
            }

            var adminId = await GetBugSubmitterId(email);

            var bugAssignment = new BugAssignment
            {
                AssignedDate = DateTime.UtcNow,
                BugId = bugId,
                DevId = devId,
                AssignedById = adminId,
                // ResolutionStatus = "ongoing",
                ResolutionStatus = "Assigned",
                ResolvedOn = null

            };

            await _bugAssignment.Add(bugAssignment);

            isBugResolved.Status = "Assigned";
            await _bugsRepository.Update(isBugResolved.BugId, isBugResolved);

            var bugAssignmentResponse = new BugAssignmentResponse
            {
                AssignedDate = bugAssignment.AssignedDate,
                BugId = bugAssignment.BugId,
                DevId = bugAssignment.DevId,
                AssignedById = bugAssignment.AssignedById,
                ResolutionStatus = bugAssignment.ResolutionStatus,
                Bug = new BugResponse
                {
                    BugId = isBugResolved.BugId,
                    BugName = isBugResolved.BugName,
                    Description = isBugResolved.Description,
                    Screenshot = isBugResolved.Screenshot,
                    CvssScore = isBugResolved.CvssScore,
                    SubmittedOn = isBugResolved.SubmittedOn,
                    ResolvedAt = isBugResolved.ResolvedAt,
                    Status = isBugResolved.Status,
                    SubmittedById = isBugResolved.SubmittedById
                }
            };

            return bugAssignmentResponse;


        }

        public async Task<BugAssignmentResponse> ResolveBugs(long bugId, string email)
        {
            var BugData = await _bugsRepository.Get(bugId);
            var ResolverId = await GetBugSubmitterId(email);
            var bugDetails = await _bugAssignment.Get(bugId);
            if (bugDetails == null)
            {
                throw new Exception("invalid Bug id or bug not yet assigned");
            }
            if (bugDetails.ResolutionStatus == "Resolved")
            {
                throw new Exception("Bug already fixed");
            }
            if (bugDetails.ResolutionStatus == "Submitted")
            {
                throw new Exception("Bug Not yet assigned");
            }
            if (ResolverId != bugDetails.DevId)
            {
                throw new Exception("You cannot fix this bug because it is not assigned to you");
            }
            //updating bug assignment db
            bugDetails.ResolutionStatus = "Resolved";
            bugDetails.ResolvedOn = DateTime.UtcNow;
            await _bugAssignment.Update(bugId, bugDetails);

            //updating bugs db
            BugData.ResolvedAt = DateTime.UtcNow;
            BugData.Status = "Resolved";
            await _bugsRepository.Update(bugId, BugData);

            var bugAssignmentResponse = new BugAssignmentResponse
            {
                AssignedDate = bugDetails.AssignedDate,
                BugId = bugDetails.BugId,
                DevId = bugDetails.DevId,
                AssignedById = bugDetails.AssignedById,
                ResolutionStatus = bugDetails.ResolutionStatus,
                Bug = new BugResponse
                {
                    BugId = BugData.BugId,
                    BugName = BugData.BugName,
                    Description = BugData.Description,
                    Screenshot = BugData.Screenshot,
                    CvssScore = BugData.CvssScore,
                    SubmittedOn = BugData.SubmittedOn,
                    ResolvedAt = BugData.ResolvedAt,
                    Status = BugData.Status,
                    SubmittedById = BugData.SubmittedById
                }
            };

            return bugAssignmentResponse;

        }

        public async Task<BugAssignmentResponse> ResolveBugsByAdmin(long bugId, string email)
        {
            var BugData = await _bugsRepository.Get(bugId);
            var ResolverId = await GetBugSubmitterIdForAdmin(email);
            var bugDetails = await _bugAssignment.Get(bugId);
            if (bugDetails == null)
            {
                throw new Exception("invalid Bug id or bug not yet assigned");
            }
            if (bugDetails.ResolutionStatus == "Resolved")
            {
                throw new Exception("Bug already fixed");
            }
            if (bugDetails.ResolutionStatus == "Submitted")
            {
                throw new Exception("Bug Not yet assigned");
            }
            if (ResolverId.Role != "Admin")
            {
                throw new Exception("You cannot fix this bug because you are not an admin.");
            }
            // var devId = bugDetails.DevId;
            //updating bug assignment db
            bugDetails.ResolutionStatus = "Resolved";
            bugDetails.ResolvedOn = DateTime.UtcNow;
            await _bugAssignment.Update(bugId, bugDetails);

            //updating bugs db
            BugData.ResolvedAt = DateTime.UtcNow;
            BugData.Status = "Resolved";
            await _bugsRepository.Update(bugId, BugData);

            var bugAssignmentResponse = new BugAssignmentResponse
            {
                AssignedDate = bugDetails.AssignedDate,
                BugId = bugDetails.BugId,
                DevId = bugDetails.DevId,
                AssignedById = bugDetails.AssignedById,
                ResolutionStatus = bugDetails.ResolutionStatus,
                Bug = new BugResponse
                {
                    BugId = BugData.BugId,
                    BugName = BugData.BugName,
                    Description = BugData.Description,
                    Screenshot = BugData.Screenshot,
                    CvssScore = BugData.CvssScore,
                    SubmittedOn = BugData.SubmittedOn,
                    ResolvedAt = BugData.ResolvedAt,
                    Status = BugData.Status,
                    SubmittedById = BugData.SubmittedById
                }
            };

            return bugAssignmentResponse;

        }

        public async Task<IEnumerable<AvailableDev>> GetAvailableDevs()
        {
            var bugAssignments = await _bugAssignment.GetAll();

            var notAvailableDevIds = bugAssignments
                .Where(b => b.ResolutionStatus == "InProgress" || b.ResolutionStatus == "Assigned")
                .GroupBy(b => b.DevId)
                .Where(g => g.Count() >= 3)
                .Select(g => g.Key)
                .ToList();

            var allEmployees = await _employeeRepository.GetAll();

            var availableDevs = allEmployees
                .Where(e => e.Role == "Dev" && !notAvailableDevIds.Contains(e.EmployeeId))
                .Select(e => new AvailableDev
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name
                })
                .ToList();

            return availableDevs;
        }

        public async Task<IEnumerable<AssignedResponse>> GetAssignedList()
        {
            var allAssignments = await _bugAssignment.GetAll();

            var assignedResponses = new List<AssignedResponse>();

            foreach (var assignment in allAssignments)
            {
                var bug = await _bugsRepository.Get(assignment.BugId);
                var dev = await _employeeRepository.Get(assignment.DevId);

                assignedResponses.Add(new AssignedResponse
                {
                    bugId = assignment.BugId,
                    BugName = bug.BugName,
                    DevId = assignment.DevId,
                    DevName = dev.Name
                });
            }

            return assignedResponses;
        }

        public async Task<IEnumerable<BugResponse>> GetBugsAssignedToMe(string email)
        {
            long DevId = await GetBugSubmitterId(email);

            var bugs = await _bugManagementRepository.GetBugsAssignedToMe(DevId);
            return bugs;

        }

        public async Task<IEnumerable<BugResponse>> GetBugsAssignedToDev(long empId)
        {
            var bugs = await _bugManagementRepository.GetBugsAssignedToMe(empId);

            return bugs;
        }

        // public async Task<> UpdateInProgress()
        public async Task<BugAssignmentResponse> UpdateInProgress(long bugId, string email)
        {
            var BugData = await _bugsRepository.Get(bugId);
            var ResolverId = await GetBugSubmitterId(email);
            var bugDetails = await _bugAssignment.Get(bugId);
            if (bugDetails == null)
            {
                throw new Exception("invalid Bug id or bug not yet assigned");
            }
            if (bugDetails.ResolutionStatus == "Resolved")
            {
                throw new Exception("Bug already fixed");
            }
            if (bugDetails.ResolutionStatus == "Submitted")
            {
                throw new Exception("Bug Not yet assigned");
            }
            if (ResolverId != bugDetails.DevId)
            {
                throw new Exception("You cannot fix this bug because it is not assigned to you");
            }
            //updating bug assignment db
            bugDetails.ResolutionStatus = "InProgress";
            bugDetails.ResolvedOn = DateTime.UtcNow;
            await _bugAssignment.Update(bugId, bugDetails);

            //updating bugs db
            BugData.ResolvedAt = DateTime.UtcNow;
            BugData.Status = "InProgress";
            await _bugsRepository.Update(bugId, BugData);

            var bugAssignmentResponse = new BugAssignmentResponse
            {
                AssignedDate = bugDetails.AssignedDate,
                BugId = bugDetails.BugId,
                DevId = bugDetails.DevId,
                AssignedById = bugDetails.AssignedById,
                ResolutionStatus = bugDetails.ResolutionStatus,
                Bug = new BugResponse
                {
                    BugId = BugData.BugId,
                    BugName = BugData.BugName,
                    Description = BugData.Description,
                    Screenshot = BugData.Screenshot,
                    CvssScore = BugData.CvssScore,
                    SubmittedOn = BugData.SubmittedOn,
                    ResolvedAt = BugData.ResolvedAt,
                    Status = BugData.Status,
                    SubmittedById = BugData.SubmittedById
                }
            };

            return bugAssignmentResponse;

        }

        private async Task<long> GetBugSubmitterId(string email)
        {
            var result = await _employeeRepository.GetEmployeeByEmail(email);
            return result.EmployeeId;
        }

        private async Task<SubmitterData> GetBugSubmitterIdForAdmin(string email)
        {
            var result = await _employeeRepository.GetEmployeeByEmail(email);
            var submitterData = new SubmitterData
            {
                SubmitterId = result.EmployeeId,
                Role=result.Role
            };
            return submitterData;
        }


    }
}
