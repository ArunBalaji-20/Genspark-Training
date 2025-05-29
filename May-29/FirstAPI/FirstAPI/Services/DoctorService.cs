using FirstAPI.Interfaces;
using FirstAPI.Misc;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Services
{
    public class DoctorService : IDoctorService
    {
        DoctorMapper doctorMapper ;
        SpecialityMapper specialityMapper;
        private readonly IRepository<int, Doctor> _doctorRepository;
        private readonly IRepository<int, Speciality> _specialityRepository;
        private readonly IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;
        private readonly IOtherContextFunctionities _otherContextFunctionities;


        public DoctorService(IRepository<int, Doctor> doctorRepository,
                            IRepository<int, Speciality> specialityRepository,
                            IRepository<int, DoctorSpeciality> doctorSpecialityRepository,
                            IOtherContextFunctionities otherContextFunctionities)
        {
            doctorMapper = new DoctorMapper();
            specialityMapper = new();
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
            _doctorSpecialityRepository = doctorSpecialityRepository;
            _otherContextFunctionities = otherContextFunctionities;

        }

        public async Task<Doctor> AddDoctor(DoctorAddRequestDto doctor)
        {
            try
            {
                var newDoctor = doctorMapper.MapDoctorAddRequestDoctor(doctor);
                newDoctor = await _doctorRepository.Add(newDoctor);
                if (newDoctor == null)
                    throw new Exception("Could not add doctor");
                if (doctor.Specialities.Count() > 0)
                {
                    int[] specialities = await MapAndAddSpeciality(doctor);
                    for (int i = 0; i < specialities.Length; i++)
                    {
                        var doctorSpeciality = specialityMapper.MapDoctorSpecility(newDoctor.Id, specialities[i]);
                        doctorSpeciality = await _doctorSpecialityRepository.Add(doctorSpeciality);
                    }
                }
                return newDoctor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        private async Task<int[]> MapAndAddSpeciality(DoctorAddRequestDto doctor)
        {
            int[] specialityIds = new int[doctor.Specialities.Count()];
            IEnumerable<Speciality> existingSpecialities = null;
            try
            {
                existingSpecialities = await _specialityRepository.GetAll();
            }
            catch (Exception e)
            {

            }
            int count = 0;
            foreach (var item in doctor.Specialities)
            {
                Speciality speciality = null;
                if (existingSpecialities != null)
                    speciality = existingSpecialities.FirstOrDefault(s => s.Name.ToLower() == item.Name.ToLower());
                if (speciality == null)
                {
                    speciality = specialityMapper.MapSpecialityAddRequestDoctor(item);
                    speciality = await _specialityRepository.Add(speciality);
                }
                specialityIds[count] = speciality.Id;
                count++;
            }
            return specialityIds;
        }

        public Task<Doctor> GetDoctByName(string name)
        {
            throw new NotImplementedException();
        }

         public async Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string speciality)
        {
            var result = await _otherContextFunctionities.GetDoctorsBySpeciality(speciality);
            return result;
        }
    }

        // IRepository<int, Doctor> _doctorRepository;
        // IRepository<int, Speciality> _specialityRepository;
        // IRepository<int, DoctorSpeciality> _doctorSpecialityRepository;
        // public DoctorService(IRepository<int, Doctor> doctorRepository,
        //                     IRepository<int, Speciality> specialityRepository,
        //                     IRepository<int, DoctorSpeciality> doctorSpecialityRepository)
        // {
        //     _doctorRepository = doctorRepository;
        //     _doctorSpecialityRepository = doctorSpecialityRepository;
        //     _specialityRepository = specialityRepository;
        // }


        // public async Task<Doctor> GetDoctByName(string name)
        // {
        //     var allDoctors = await _doctorRepository.GetAll();
        //     return allDoctors.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        // }

        // public async Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality)
        // {
        //     if (string.IsNullOrWhiteSpace(speciality))
        //     {
        //         throw new ArgumentException("Speciality cannot be empty");
        //     }


        //     var allDoctors = await _doctorRepository.GetAll();

        //     var matchedDoctors = allDoctors
        //         .Where(d => d.DoctorSpecialities != null && d.DoctorSpecialities.Any(ds =>
        //             {
        //                 var spec = _specialityRepository.GetAll().Result
        //                     .FirstOrDefault(s => s.Id == ds.SpecialityId);
        //                 return spec != null && spec.Name.Equals(speciality, StringComparison.OrdinalIgnoreCase);
        //             }))
        //         .ToList();

        //     return matchedDoctors;
        // }

        // public async Task<Doctor> AddDoctor(DoctorAddRequestDto doctorDto)
        // {
        //     try
        //     {
        //         var newDoctor = new Doctor
        //         {
        //             Name = doctorDto.Name,
        //             YearsOfExperience = doctorDto.YearsOfExperience,
        //             Status = "Active"
        //         };

        //         var addedDoctor = await _doctorRepository.Add(newDoctor);

        //         if (addedDoctor != null && doctorDto.Specialities != null)
        //         {
        //             foreach (var specialityDto in doctorDto.Specialities)
        //             {
        //                 var newSpeciality = new Speciality
        //                 {
        //                     Name = specialityDto.Name
        //                 };

        //                 var addedSpeciality = await _specialityRepository.Add(newSpeciality);

        //                 var doctorSpeciality = new DoctorSpeciality
        //                 {
        //                     DoctorId = addedDoctor.Id,
        //                     SpecialityId = addedSpeciality.Id
        //                 };

        //                 await _doctorSpecialityRepository.Add(doctorSpeciality);
        //             }
        //         }

        //         return addedDoctor;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Insert failed: {ex.Message}");
        //     }

        // }
    
}