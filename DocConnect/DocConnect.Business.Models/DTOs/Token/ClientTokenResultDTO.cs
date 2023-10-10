namespace DocConnect.Business.Models.DTOs.Token
{
    public class ClientTokenResultDTO
    {
        public TokenResultDTO AccessTokenResultDTO { get; set; }

        public TokenResultDTO RefreshTokenResultDTO { get; set; }
    }
}
