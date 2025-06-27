using BugTrackingAPI.Models.DTO;

public interface IBugManagementRepository
{
    Task<IEnumerable<BugResponse>> GetBugsAssignedToMe(long devId);
}
