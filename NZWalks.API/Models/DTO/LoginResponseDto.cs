namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string UserName { get; set; }

        public string responseMessage { get; set; }
        public string JwtToken { get; set; }
    }
}
