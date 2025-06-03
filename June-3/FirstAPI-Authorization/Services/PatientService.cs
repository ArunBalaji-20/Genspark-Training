using AutoMapper;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Models.DTOs.DoctorSpecialities;
using FirstAPI.Repositories;

namespace FirstAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;
        private readonly IRepository<string, User> _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        public PatientService(IRepository<int, Patient> patientRepository,
                             IRepository<string, User> userRepository,
                             IEncryptionService encryptionService,
                             IMapper mapper)
        {
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;

        }
        public async Task<Patient> AddPatients(PatientAddRequestDto patient)
        {
            try
            {
                var user = _mapper.Map<PatientAddRequestDto, User>(patient);
                var encryptedData = await _encryptionService.EncryptData(new EncryptModel
                {
                    Data = patient.Password
                });

                user.Password = encryptedData.EncryptedData;
                user.HashKey = encryptedData.HashKey;
                user.Role = "Patient";
                user = await _userRepository.Add(user);

                var patientMapper = new Patient
                {
                    Name = patient.Name,
                    Age = patient.Age,
                    Phone = patient.Phone,
                    Email = patient.Email
                };
                var newPatient = await _patientRepository.Add(patientMapper);
                if (newPatient == null)
                {
                    throw new Exception("could not add patient");
                }
                return newPatient;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<Patient> GetPatientByName(string name)
        {
            throw new NotImplementedException();
        }

        
    }
}