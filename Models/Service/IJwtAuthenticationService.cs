using ChimieProject.Models.Entities;
using System.Security.Claims;

namespace ChimieProject.Models.Service
{
    public interface IJwtAuthenticationService
    {
        Laboratoire Authenticate(string Nom);

        string GenerateToken(string secret, List<Claim> claims);


        bool IsTokenValid(string key, string issuer, string token);

        string BuildToken(string key, string issuer, Laboratoire objLab);

        string BuildMessage(string stringToSplit, int chunkSize);

        public string Decrypt(string cipherText);

        public string Encrypt(string clearText);

    }
}
