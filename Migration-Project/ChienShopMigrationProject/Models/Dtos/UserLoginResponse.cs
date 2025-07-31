namespace ChienVHShopOnline.Models.Dtos
{
    public class UserLoginResponse
    {
        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;
    }
}