using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Authorize(Roles = "client")]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }
        private IAdvertDbService _service;

        public ClientsController(IConfiguration configuration, IAdvertDbService service)
        {
            Configuration = configuration;
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("refresh/{request}")]
        public IActionResult RefreshToken(string request)
        {
            try
            {
                Client client = _service.checkRefreshToken(request);
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, client.Login));
                claims.Add(new Claim(ClaimTypes.Name, client.FirstName + " " + client.LastName));
                claims.Add(new Claim(ClaimTypes.Role, "client"));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                    issuer: "s19191",
                    audience: "Clients",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );
                var refreshToken = Guid.NewGuid();
                _service.saveRefreshToken(client.Login, refreshToken.ToString());
                return Ok(new
                {
                    accesstoken = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = refreshToken
                });
            }
            catch (FalseRefreshTokenException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                LoginRespone response = _service.Loggining(request);
                
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier,request.Login));
                claims.Add(new Claim(ClaimTypes.Name,response.FirstName + " " + response.LastName));
                claims.Add(new Claim(ClaimTypes.Role,"client"));
                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                
                var token = new JwtSecurityToken
                (
                    issuer: "s19191",
                    audience: "Clients",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );
                var refreshToken = Guid.NewGuid();
                _service.saveRefreshToken(request.Login, refreshToken.ToString());
                return Ok(new
                {
                    accesstoken = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = refreshToken
                });
            }
            catch (NoSuchClientException e)
            {
                return NotFound(e.Message);
            }
            catch (FalsePasswordException e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            try
            {
                Client client = _service.Registration(request);
                
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier,request.Login));
                claims.Add(new Claim(ClaimTypes.Name,client.FirstName + " " + client.LastName));
                claims.Add(new Claim(ClaimTypes.Role,"client"));
                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                
                var token = new JwtSecurityToken
                (
                    issuer: "s19191",
                    audience: "Clients",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );
                var refreshToken = Guid.NewGuid();
                _service.saveRefreshToken(request.Login, refreshToken.ToString());
                return Ok(new
                {
                    accesstoken = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = refreshToken
                });
            }
            catch (LoginOccupiedException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}