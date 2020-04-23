using ERA_WebAPI.ERA.Models.UserModels.responseMessage;
using ERA_WebAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ERA_WebAPI.ERA.Models.UserModels.services
{
    public interface IUserService
    {
        Task<ResponseMessage> RegisterUserAsync(RegisterModel model);
        Task<ResponseMessage> LoginUserAsync(LoginModel model);
        Task<userResponseMessage> GetUserAsync(string Id);
        Task<userResponseMessage> EditUserAsync(AppUser user, string id);
    }

    public class userService : IUserService
    {
        private UserManager<AppUser> _userManger;
        private IConfiguration _configuration;
        

        public userService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManger = userManager;
            _configuration = configuration;
            
        }


        public async Task<ResponseMessage> RegisterUserAsync(RegisterModel registerModel)
        {
            if (registerModel == null)
                throw new NullReferenceException("Reigster Model is null");

            FullName fullName = new FullName()
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName
            };
            var user = new AppUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                FullName = fullName,
                Address = registerModel.Address,
                Age = registerModel.Age,
                PhoneNumber = registerModel.PhoneNumber,
                ProfilePic = registerModel.ProfilePhoto,
                PasswordHash = registerModel.Password,
                EmailConfirmed = true
            };

            var result = await _userManger.CreateAsync(user, registerModel.Password);
            

            if (result.Succeeded)
            {
                //it is for add role
               await _userManger.AddToRoleAsync(user, "user");

                //for confirmEmail
                #region confirmEmail

                //var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

                //var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                //string url = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                //await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                //    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");


                #endregion

                return new ResponseMessage
                {
                    Message = $"User created successfully!{user.Email}",
                    IsSuccess = true,
                };

            }

            return new ResponseMessage
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description),

            };
        }

        public async Task<ResponseMessage> LoginUserAsync(LoginModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new ResponseMessage
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            var result = await _userManger.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new ResponseMessage
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };


            //user Info
            #region Jwt
            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role,"user")
            };

            //create key 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            #endregion

            return new ResponseMessage
            {
               Message = tokenAsString,
               IsSuccess = true,
               ExpireDate = token.ValidTo
            };
        }


        public async Task<userResponseMessage> GetUserAsync(string Id)
        {
            AppUser user = await _userManger.FindByIdAsync(Id);

            if (user == null)
            {
                return new userResponseMessage
                {
                    Message = "There is no user with that ID",
                    IsSuccess = false,
                };
            }
            RegisterModel userInfo = new RegisterModel()
            {
                Address = user.Address,
                Email = user.Email,
                Age = user.Age,
                FirstName = user.FullName.FirstName,
                LastName = user.FullName.LastName,
                PhoneNumber = user.PhoneNumber,
                ProfilePhoto = user.ProfilePic,
                Gender = user.Gender

            };
            return new userResponseMessage
            {
                user = userInfo,
                IsSuccess = true,
                Message=$"Get user {userInfo.FirstName}"

            };
        }

        public async Task<userResponseMessage> EditUserAsync(AppUser newUser, string id)
        {
            AppUser user = await _userManger.FindByIdAsync(id);

            if (user == null)
            {
                return new userResponseMessage
                {
                    Message = "There is no user with that ID",
                    IsSuccess = false,
                };
            }

            user.PhoneNumber = newUser.PhoneNumber;
            user.ProfilePic = newUser.ProfilePic;
            user.Address = newUser.Address;
            user.Email = newUser.Email;
            user.Age = newUser.Age;
            user.Gender = newUser.Gender;
            if (user.FullName != null)
            {
                user.FullName.FirstName = newUser.FullName.FirstName;
                user.FullName.LastName = newUser.FullName.LastName;
            }
            //gender

            await _userManger.UpdateAsync(user);
            return new userResponseMessage
            {
                IsSuccess = true,
                Message = user.Email

            };
        }

    }
}
