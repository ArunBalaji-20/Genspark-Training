using Microsoft.AspNetCore.Authorization;

public class DoctorWithExperienceRequirement : IAuthorizationRequirement
{
    public int MinimumYearsExperience { get; }

    public DoctorWithExperienceRequirement(int minimumYears)
    {
        MinimumYearsExperience = minimumYears;
    }
}
