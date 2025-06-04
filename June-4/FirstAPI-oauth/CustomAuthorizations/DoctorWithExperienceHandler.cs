using System.Security.Claims;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Repositories;
using FirstAPI.Services;
using Microsoft.AspNetCore.Authorization;

public class DoctorWithExperienceHandler : AuthorizationHandler<DoctorWithExperienceRequirement>
{
    private readonly IDoctorService _doctorService;

    public DoctorWithExperienceHandler(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DoctorWithExperienceRequirement requirement)
    {
        // throw new NotImplementedException();
        var username = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
        if (string.IsNullOrEmpty(username))
        {
            return;
        }
        // System.Console.WriteLine(role);

        var user = await _doctorService.GetDoctbyEmail(username);
        if (user == null)
        {
            return;
        }
        if (role=="Doctor" && user.YearsOfExperience > requirement.MinimumYearsExperience)
            {
                context.Succeed(requirement);
            }
         
    }
}
