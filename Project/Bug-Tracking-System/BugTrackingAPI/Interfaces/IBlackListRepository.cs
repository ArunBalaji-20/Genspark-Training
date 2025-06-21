using BugTrackingAPI.Models;
using System.Threading.Tasks;

namespace BugTrackingAPI.Interfaces
{
    public interface IBlackListRepository
    {
        Task<bool> IsBlacklistedAsync(string token);
        // Task<bool> Exists(string token);
    }
}