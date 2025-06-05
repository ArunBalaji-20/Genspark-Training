
using DocNotifyAPI.Models;

namespace DocNotifyAPI.Interfaces
{
    public interface IEncryptionService
    {
        public Task<EncryptModel> EncryptData(EncryptModel data);
    }
}