﻿using ChimieProject.Models.Entities;
using System.Security.Claims;

namespace ChimieProject.Models.Service
{
    public interface IJwtAuthenticationService
    {
        Structure Authenticate(string Nom);

        string GenerateToken(string secret, List<Claim> claims);


        bool IsTokenValid(string key, string issuer, string token);

        string BuildToken(string key, string issuer, Structure objLab);

        string BuildMessage(string stringToSplit, int chunkSize);

        public string Decrypt(string cipherText);

        public string Encrypt(string clearText);

    }
}
