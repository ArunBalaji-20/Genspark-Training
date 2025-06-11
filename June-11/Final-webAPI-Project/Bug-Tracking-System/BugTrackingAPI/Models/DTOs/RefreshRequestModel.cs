using System.ComponentModel.DataAnnotations;

public class RefreshRequestModel
{
    [Required(ErrorMessage = "Refresh token is mandatory")]
    public string RefreshToken { get; set; }
}
