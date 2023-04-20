using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskCarBrandProject.Context;
using TaskCarBrandProject.Helper;
using TaskCarBrandProject.Models;

namespace TaskCarBrandProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly CarDetailsContext _CarDetailsContext;

        public SignUpController(CarDetailsContext carDetailsContext)
        {
            _CarDetailsContext = carDetailsContext;
        }

        [HttpPost("authenticateLogin")]
        public async Task<IActionResult> Authenticate(SignUp signUp)
        {
            if (signUp == null)
                return BadRequest();

            var data = await _CarDetailsContext.signUpForm.FirstOrDefaultAsync(findData => findData.UserName == signUp.UserName);

            if (data == null)
                return BadRequest(new { Message = "User Not Found!" });

            //Password Verify

            if (!PasswordHasher.VarifyPassword(signUp.Password, data.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }




            data.Token = CreateJWT(data);

            return Ok(new
            {
                Token = data.Token,
                Message = "Login Success!"
            });
        }


        [HttpPost("registerOrSignUp")]
        public async Task<IActionResult> RegisterUser([FromBody] SignUp signUp)
        {
            if (signUp == null)
                return BadRequest();



            //check UserName
            if (await CheckUserNameExistAsync(signUp.UserName))
                return BadRequest(new { Message = "userName Already Exist!" });

            //Email
            if (await CheckEmailExistAsync(signUp.Email))
                return BadRequest(new { Message = "Email Already Exist!" });





            // Check Password Strngth
            var passwordChecking = CheckPasswordStrength(signUp.Password, signUp.Email); // didn't used email Validation here 

            if (!string.IsNullOrEmpty(passwordChecking))
                return BadRequest(new { Message = passwordChecking.ToString() });

            // Check Password
            signUp.Password = PasswordHasher.HashPassword(signUp.Password);
            signUp.Role = "user";
            signUp.Token = "";

            await _CarDetailsContext.signUpForm.AddAsync(signUp);
            await _CarDetailsContext.SaveChangesAsync();
            return Ok(new
            {
                Message = " User Regestered!"
            });

        }

        //UserNameCheck
        private Task<bool> CheckUserNameExistAsync(string userName)
            => _CarDetailsContext.signUpForm.AnyAsync(x => x.UserName == userName);

        //EmailCheck
        private Task<bool> CheckEmailExistAsync(string email)
            => _CarDetailsContext.signUpForm.AnyAsync(x => x.Email == email);



        private string CheckPasswordStrength(string password, string email)
        {
            StringBuilder sb = new StringBuilder(); // system.Text



            if (!(Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)))
                sb.Append("Email is not valid" + Environment.NewLine);


            // password 

            if (password.Length < 8)
                sb.Append("Password Must Be 8 charachters" + Environment.NewLine);


            // one Chapital 
            if (!(Regex.IsMatch(password, "[A-Z]")))
                sb.Append("Passowrd Must Be one CHapital " + Environment.NewLine);//usig system

            // one Smaill 
            if (!(Regex.IsMatch(password, "[a-z]")))
                sb.Append("Passowrd Must Be one Small Charachter" + Environment.NewLine);//usig system

            // one Number 
            if (!(Regex.IsMatch(password, "[0-9]")))
                sb.Append("Passowrd Must Be one Number" + Environment.NewLine);//usig system

            // one Special Charachter 
            if (!(Regex.IsMatch(password, "[ < , >  , , , . , !,@,#,$,% ,< ,^,& , * , ( , )  , _ , + , = , { , } , \\ , | , ? , / : , ; , ~ , ` ,  ' ,]")))
                sb.Append("Passowrd Must Be one Special Charachter" + Environment.NewLine);
            return sb.ToString();


            // system.Text.RegularExpricence


            // system.Text.RegularExpricence
        }


        // create JWT Token

        private string CreateJWT(SignUp signUp)
        {
            var JWTTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("veryverysecret....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, signUp.Role),
                new Claim(ClaimTypes.Name , $"{signUp.FirstName } {signUp.LastName}")

            }); // using System.Security.Claims; 


            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials

            };

            var token = JWTTokenHandler.CreateToken(tokenDescriptor);

            return JWTTokenHandler.WriteToken(token);
        }
    }
}
