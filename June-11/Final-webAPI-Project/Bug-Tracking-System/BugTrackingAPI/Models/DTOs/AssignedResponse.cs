namespace BugTrackingAPI.Models.DTO;

public class AssignedResponse
{
    public long bugId{ get; set; }

    public string BugName { get; set; }

    public long DevId{ get; set; }
    public string DevName { get; set; }
}