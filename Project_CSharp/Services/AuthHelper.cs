namespace Project_CSharp.Services
{
    public class AuthHelper
    {
        public Answer Answer { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthHelper(Answer answer)
        {
            Answer = answer;
        }

        public AuthHelper(Answer answer, string accessToken, string refreshToken)
        {
            Answer = answer;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
