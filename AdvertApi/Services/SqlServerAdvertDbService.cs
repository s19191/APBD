using System;
using System.Collections.Generic;
using System.Linq;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Models;
using AdvertApi.PasswordHashing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AdvertApi.Services
{
    public class SqlServerAdvertDbService : IAdvertDbService
    {
        
        private readonly s19191Context _context;

        public SqlServerAdvertDbService(s19191Context context)
        {
            _context = context;
        }
        public Client checkRefreshToken(string refreshToken)
        {
            Client client = _context.Client
                .FirstOrDefault(c => c.RefreshToken.Equals(refreshToken));
            if (client != null)
            {
                return client;
            }
            else
            {
                throw new FalseRefreshTokenException("Błędny refreshToken");
            }
        }
        
        public LoginRespone Loggining(LoginRequest request)
        {
            Client client = _context.Client
                .FirstOrDefault(c => c.Login.Equals(request.Login));
            if (client != null)
            {
                if (Pbkdf2Hashing.Validate(request.Password,client.Salt,client.Password))
                {
                    return new LoginRespone(client.FirstName, client.LastName);
                }
                else
                {
                    throw new FalsePasswordException("Podano błędne hasło");
                }
            }
            else
            {
                throw new NoSuchClientException($"Klient o podanym loginie nie istnieje");
            }
        }

        public Client Registration(RegisterRequest request)
        {
            Client client = _context.Client
                .FirstOrDefault(c => c.Login.Equals(request.Login));
            if (client == null)
            {
                string Salt = Pbkdf2Hashing.CreateSalt();
                Client newClient = new Client
                {
                    IdClient = _context.Client.Max(c => c.IdClient) + 1,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Phone = request.Phone,
                    Login = request.Login,
                    Password = Pbkdf2Hashing.Create(request.Password, Salt),
                    Salt = Salt,
                    RefreshToken = "tmp"
                };
                _context.Client.Add(newClient);
                _context.SaveChanges();
                return newClient;
            }
            else
            {
                throw new LoginOccupiedException("Podany login jest już zajęty, spróbuj inny");
            }
        }

        public void saveRefreshToken(string Login, string refreshToken)
        {
            Client client = _context.Client
                .FirstOrDefault(c => c.Login.Equals(Login));
            client.RefreshToken = refreshToken;
            _context.Update(client);
            _context.SaveChanges();
        }

        public Client aaa()
        {
            return _context.Client.FirstOrDefault(c => c.IdClient == 1);
        }
    }
}