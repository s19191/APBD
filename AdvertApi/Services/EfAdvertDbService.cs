using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Models;
using AdvertApi.PasswordHashing;
using Microsoft.EntityFrameworkCore;

namespace AdvertApi.Services
{
    public class EfAdvertDbService : IAdvertDbService
    {
        private readonly s19191Context _context;

        public EfAdvertDbService(s19191Context context)
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
                throw new NoSuchClientException("Klient o podanym loginie nie istnieje");
            }
        }

        public RegisterResponse Registration(RegisterRequest request)
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
                return new RegisterResponse
                {
                    IdClient = newClient.IdClient,
                    FirstName = newClient.FirstName,
                    LastName = newClient.LastName,
                    Email = newClient.Email,
                    Phone = newClient.Phone,
                    Login = newClient.Login,
                    Password = request.Password
                };
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
            _context.Client.Update(client);
            _context.SaveChanges();
        }

        public IEnumerable<GetCampaignsResponse> GetCampaigns()
        {
            var campaigns = _context.Campaign
                .Include(c=>c.Banner)
                .Join(_context.Client,
                    campaing => campaing.IdClient,
                    client => client.IdClient,
                    ((campaign, client) =>
                        new GetCampaignsResponse
                        {
                            campaign = campaign,
                            client = client
                        }
                    ))
                .OrderByDescending(c=>c.campaign.StartDate);
            return campaigns;
        }

        public AddCampaignResponse AddCampaign(AddCampaignRequest request)
        {
            if (_context.Building.Count() >= 2)
            {
                Building FromBuilding = _context.Building
                    .FirstOrDefault(b => b.IdBuilding.Equals(request.FromIdBuilding));
                Building ToBuilding = _context.Building
                    .FirstOrDefault(b => b.IdBuilding.Equals(request.ToIdBuilding));
                if (FromBuilding != null && ToBuilding != null)
                {
                    if (FromBuilding.Street.Equals(ToBuilding.Street) && FromBuilding.City.Equals(ToBuilding.City))
                    {
                        if (_context.Client.FirstOrDefault(c => c.IdClient == request.IdClient) != null)
                        {
                            int newIdCampaign = _context.Campaign.Max(c => c.IdCampaign) + 1;
                            Campaign newCampaign = new Campaign
                            {
                                IdCampaign = newIdCampaign,
                                IdClient = request.IdClient,
                                StartDate = request.StartDate,
                                EndDate = request.EndDate,
                                PricePerSquareMeter = request.PricePerSquareMeter,
                                FromBuilding = request.FromIdBuilding,
                                ToBuilding = request.ToIdBuilding
                            };
                            _context.Campaign.Add(newCampaign);
                            decimal price = 0;
                            Banner banner1;
                            Banner banner2;
                            // zakładam, że numery budynków znajdują się po tej samej stronie ulicy, niesety czasami niparzyste są pojednej, a parzyste po drugiej,
                            // ale z racji tego, że nie jest to opisane w wymaganiach to mam takie proste założenie
                            var buildings = _context.Building
                                .Where(b => b.StreetNumber >= FromBuilding.StreetNumber &&
                                            b.StreetNumber <= ToBuilding.StreetNumber)
                                .OrderBy(b => b.StreetNumber);
                            Building firtstBuilding = buildings.First();
                            Building lastBuilding = buildings.Last();
                            decimal maxHight = buildings
                                .Max(b => b.Height);
                            Building maxB = buildings
                                .FirstOrDefault(b => b.Height == maxHight);
                            if (maxB.IdBuilding == firtstBuilding.IdBuilding)
                            {
                                decimal secondMaxHight = buildings
                                    .Skip(1)
                                    .Max(b => b.Height);
                                decimal area1 = maxHight;
                                decimal price1 = area1 * request.PricePerSquareMeter;
                                banner1 = this.createBanners(1, price1, newIdCampaign, area1);
                                decimal area2 = secondMaxHight * (buildings.Count() - 1);
                                decimal price2 = area2 * request.PricePerSquareMeter;
                                banner2 = createBanners(2, price2, newIdCampaign, area2);
                                price = price1 + price2;
                            }
                            else
                            {
                                if (maxB.IdBuilding == lastBuilding.IdBuilding)
                                {
                                    decimal secondMaxHight = buildings
                                        .SkipLast(1)
                                        .Max(b => b.Height);
                                    decimal area1 = maxHight;
                                    decimal price1 = area1 * request.PricePerSquareMeter;
                                    banner1 = createBanners(1, price1, newIdCampaign, area1);
                                    decimal area2 = secondMaxHight * (buildings.Count() - 1);
                                    decimal price2 = area2 * request.PricePerSquareMeter;
                                    banner2 = createBanners(2, price2, newIdCampaign, area2);
                                    price = price1 + price2;
                                }
                                else
                                {
                                    var buildingsAtLeft = buildings
                                        .Where(b => b.StreetNumber < maxB.StreetNumber); 
                                    var buildingsAtRight = buildings
                                        .Where(b => b.StreetNumber > maxB.StreetNumber); 
                                    decimal leftArea = buildingsAtLeft.Sum(b => b.Height); 
                                    decimal rightArea = buildingsAtRight.Sum(b => b.Height); 
                                    if (maxHight * buildingsAtLeft.Count() - leftArea > maxHight * buildingsAtRight.Count() - rightArea) 
                                    { 
                                        decimal secondMaxHight = buildingsAtRight
                                            .Max(b => b.Height); 
                                        decimal area1 = maxHight * (buildingsAtRight.Count() + 1); 
                                        decimal price1 = area1 * request.PricePerSquareMeter; 
                                        banner1 = createBanners(1, price1, newIdCampaign, area1); 
                                        decimal area2 = secondMaxHight * buildingsAtLeft.Count(); 
                                        decimal price2 = area2 * request.PricePerSquareMeter; 
                                        banner2 = createBanners(2, price2, newIdCampaign, area2); 
                                        price = price1 + price2; 
                                    }
                                    else 
                                    { 
                                        decimal secondMaxHight = buildingsAtLeft
                                            .Max(b => b.Height); 
                                        decimal area1 = secondMaxHight * (buildingsAtLeft.Count() + 1); 
                                        decimal price1 = area1 * request.PricePerSquareMeter; 
                                        banner1 = createBanners(1, price1, newIdCampaign, area1); 
                                        decimal area2 = secondMaxHight * buildingsAtRight.Count(); 
                                        decimal price2 = area2 * request.PricePerSquareMeter; 
                                        banner2 = createBanners(2, price2, newIdCampaign, area2); 
                                        price = price1 + price2; 
                                    }
                                }
                            }
                            _context.Banner.Add(banner1);
                            _context.Banner.Add(banner2);
                            _context.SaveChanges();
                            return new AddCampaignResponse
                            {
                                Campaign = newCampaign,
                                TotalPrice = price
                            };
                        }
                        else
                        {
                            throw new NoSuchClientException("Klient o podanym loginie nie istnieje");
                        }
                    }
                    else
                    {
                        throw new NotOnTheSameStreetOrCityException(
                            "Budynki nie znajdują się na tej samej ulicy, bądź mieście");
                    }
                }
                else
                {
                    throw new NoSuchBuildingExsistsException("Budynek o podanym id nie istnieje w bazie danych");
                }
            }
            else
            {
                throw new NotEnoughtBuildingsInDatabaseException("W bazie danych nie ma wystarczającej ilości budynków by móc stworzyć nową kampanie");
            }
        }

        public Banner createBanners(int Name, decimal price,  int newIdCampaign, decimal Area)
        {
            Banner banner = new Banner
            {
                IdAdvertisement = _context.Banner.Max(b=>b.IdAdvertisement) + Name,
                Name = Name,
                Price = price,
                IdCampaign = newIdCampaign,
                Area = Area
            };
            return banner;
        }
    }
}