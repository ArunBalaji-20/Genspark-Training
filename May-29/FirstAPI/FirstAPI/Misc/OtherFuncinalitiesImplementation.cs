using FirstAPI.Contexts;
using FirstAPI.Interfaces;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Misc
{
    public class OtherFuncinalitiesImplementation : IOtherContextFunctionities
    {
        private readonly ClinicContext _clinicContext;

        public OtherFuncinalitiesImplementation(ClinicContext clinicContext)
        {
            _clinicContext = clinicContext;
        }

        public async Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string specilaity)
        {
            var result = await _clinicContext.GetDoctorsBySpeciality(specilaity);
            return result;
        }
    }
}