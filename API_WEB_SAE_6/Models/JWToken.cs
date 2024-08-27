namespace API_WEB_SAE_6.Models
{
    public class JWToken
    {
        public string token { get; set; }

        public JWToken(string token)
        {
            this.token = token;
        }
    }
}
