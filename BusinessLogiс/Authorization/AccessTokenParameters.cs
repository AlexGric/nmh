using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BusinessLogic.Authorization
{
    public class AccessTokenParameters
    {
        public const string ISSUER = "TaskManagementSystemIssuer";
        public const string AUDIENCE = "TaskManagementSystemIssuerUsers";
        private const string KEY = "of2178T6p2q98Jgy6702p8ypOiyf9wq8tyPq254yFpyMpq2ty8fq2PCugtmolEd2ucpTgu8vq2Npq";
        public const int LIFETIME = 10; // minutes

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}