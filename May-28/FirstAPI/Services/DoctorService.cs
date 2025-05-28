using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Services
{
    public class DoctorService : IDoctorService
    {
        IRepository<int, Doctor> _doctorRepository;
        IRepository<int, Speciality> _specialityRepository;
        IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;
        public DoctorService(IRepository<int, Doctor> doctorRepository,
                            IRepository<int, Speciality> specialityRepository,
                            IRepository<int, DoctorSpeciality> doctorSpecialityRepository)
        {
            _doctorRepository = doctorRepository;
            _doctorSpecialityRepository = doctorSpecialityRepository;
            _specialityRepository = specialityRepository;
        }

        //         public Task<Doctor> GetDoctByName(string name)
        //         { throw new NotImplementedException();
        // }
        //         public Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality)
        //         {throw new NotImplementedException();
        //  }
        //         public Task<Doctor> AddDoctor(DoctorAddRequestDto doctor)
        //         {throw new NotImplementedException();
        // }

        public async Task<Doctor> GetDoctByName(string name)
        {
            var allDoctors = await _doctorRepository.GetAll();
            return allDoctors.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality)
        {
            if (string.IsNullOrWhiteSpace(speciality))
            {
                throw new ArgumentException("Speciality cannot be empty");
            }

           
            var allDoctors = await _doctorRepository.GetAll();

            var matchedDoctors = allDoctors
                .Where(d => d.DoctorSpecialities != null && d.DoctorSpecialities.Any(ds =>
                    {
                        var spec = _specialityRepository.GetAll().Result
                            .FirstOrDefault(s => s.Id == ds.SpecialityId);
                        return spec != null && spec.Name.Equals(speciality, StringComparison.OrdinalIgnoreCase);
                    }))
                .ToList();
            
            return matchedDoctors;
        }

        public async Task<Doctor> AddDoctor(DoctorAddRequestDto doctorDto)
        {
            try
            {
                var newDoctor = new Doctor
                {
                    Name = doctorDto.Name,
                    YearsOfExperience = doctorDto.YearsOfExperience,
                    Status = "Active"
                };

                var addedDoctor = await _doctorRepository.Add(newDoctor);

                if (addedDoctor != null && doctorDto.Specialities != null)
                {
                    foreach (var specialityDto in doctorDto.Specialities)
                    {
                        var newSpeciality = new Speciality
                        {
                            Name = specialityDto.Name
                        };

                        var addedSpeciality = await _specialityRepository.Add(newSpeciality);

                        var doctorSpeciality = new DoctorSpeciality
                        {
                            DoctorId = addedDoctor.Id,
                            SpecialityId = addedSpeciality.Id
                        };

                        await _doctorSpecialityRepository.Add(doctorSpeciality);
                    }
                }

                return addedDoctor;
            }
            catch (Exception ex)
            {
                throw new Exception($"Insert failed: {ex.Message}");
            }

        }
    }
}