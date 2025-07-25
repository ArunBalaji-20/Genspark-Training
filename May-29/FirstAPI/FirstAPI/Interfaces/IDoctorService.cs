using System;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;
namespace FirstAPI.Interfaces
{
    public interface IDoctorService
    {
        public Task<Doctor> GetDoctByName(string name);
        // public Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality);
        public Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string speciality);
        public Task<Doctor> AddDoctor(DoctorAddRequestDto doctor);
    }
}