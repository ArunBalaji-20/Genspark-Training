using System;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models.DTO;
using BCrypt.Net;

namespace BugTrackingAPI.Services;

public class EncryptionService : IEncryptionService
{
    public async Task<EncryptModel> EncryptData(EncryptModel data)
    {
        data.EncryptedData = BCrypt.Net.BCrypt.HashPassword(data.Data);
        return data;
    }
}

