using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using My.World.Api.Models;
using My.World.Web.Helpers;
using My.World.Web.Models;
using My.World.Web.Services;
using My.World.Web.ViewModel;
using Newtonsoft.Json;

namespace My.World.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersApiService _usersApiService;
        private readonly IEmailClient _emailClient;
        private readonly IContentplansApiService _contentPlansService;
        Microsoft.Extensions.Configuration.IConfiguration _config;
        public readonly IObjectBucketApiService _iObjectBucketApiService;
        readonly IAppconfigApiService _appconfigApiService;

        public AccountController(IUsersApiService usersApiService, IEmailClient emailClient,
            IContentplansApiService contentPlansService,
            Microsoft.Extensions.Configuration.IConfiguration config, IObjectBucketApiService iObjectBucketApiService,
            IAppconfigApiService appconfigApiService)
        {
            _usersApiService = usersApiService;
            _emailClient = emailClient;
            _contentPlansService = contentPlansService;
            _config = config;
            _iObjectBucketApiService = iObjectBucketApiService;
            _appconfigApiService = appconfigApiService;
        }

        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ConfirmAccountEmail(long id)
        {
            UsersModel usersModel = new UsersModel() { id = id };
            usersModel = _usersApiService.GetUsers(usersModel);
            return View(usersModel);
        }

        public IActionResult ResendConfirmAccountEmail(string email)
        {
            UsersModel usersModel = new UsersModel() { email = email };
            usersModel = _usersApiService.GetUsersDataByEmail(usersModel);

            var callbackUrl = Request.Scheme + "://" + Request.Host.Value + "/Account/ConfirmAccount?code=" + usersModel.secure_code;

            string templateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplate");
            string emailBody = System.IO.File.ReadAllText(templateFolder + "/VerifyEmailTemplate" + ".html");
            emailBody = emailBody.Replace("[VERIFYLINK]", callbackUrl);

            _emailClient.SendEmailAsync(usersModel.email, "My World : Confirm your email", emailBody);
            return RedirectToAction("Login");
        }

        public IActionResult ConfirmAccount(string code)
        {
            UsersModel usersModel = new UsersModel() { secure_code = code, email_confirm = true };
            var result = _usersApiService.UpdateUsersEmailConfirm(usersModel);

            string templateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplate");
            string emailBody = System.IO.File.ReadAllText(templateFolder + "/VerifiedEmailTemplate" + ".html");

            _emailClient.SendEmailAsync(result.Value.email, "My World : Email confirmed !!!", emailBody);

            string welcomeEmailBody = System.IO.File.ReadAllText(templateFolder + "/WelcomeTemplate" + ".html");

            _emailClient.SendEmailAsync(result.Value.email, "Welcome to My World", welcomeEmailBody);
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            UsersModel Userdata = new UsersModel();
            Userdata.username = loginViewModel.username;
            Userdata.password = loginViewModel.password;

            var account = _usersApiService.LoginUser(Userdata);

            if (account == null)
            {
                ModelState.AddModelError("password", "User name or password is not valid.");
                return View(loginViewModel);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("AppSettings:Secret"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim(ClaimTypes.NameIdentifier, account.username),
                    new Claim(ClaimTypes.Name, account.id.ToString()),
                    new Claim("UserID", account.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            HttpContext.Session.SetString(AppConstants.JWTTOKEN, tokenString);

            var appConfigList = _appconfigApiService.GetAllAppConfig();
            appConfigList.ForEach(c=> {
                HttpContext.Session.SetString(c.key, c.value);
            });

            var userContentBucketModel = _iObjectBucketApiService.GetUserContentBucket(new UserContentBucketModel() { user_id = account.id });
            if(userContentBucketModel == null)
            {
                _iObjectBucketApiService.AddUserContentBucket(new UserContentBucketModel()
                {
                    user_id = account.id,
                    created_at = DateTime.Now,
                    bucket_Name = "user-" + account.id + "-" + account.email.RemoveSpecialCharacters() + "-bucket"
                }) ;
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(UserSignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<UserSignUpViewModel, UsersModel>()
               );

            var mapper = new Mapper(config);
            var usersModel = mapper.Map<UsersModel>(viewModel);

            var userdata = _usersApiService.GetUsersDataByEmail(usersModel);

            if (userdata != null)
            {
                if (!userdata.email_confirm)
                {
                    return RedirectToAction(nameof(ConfirmAccountEmail), new { id = userdata.id });
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                ResponseModel<string> response = _usersApiService.SignupUser(usersModel);

                if (response.IsSuccess)
                {
                    var code = new Random().Next(1000000, 999999999).ToString();
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Request.Scheme + "://" + Request.Host.Value + "/Account/ConfirmAccount?code=" + code;

                    UsersModel usrModel = new UsersModel();
                    usrModel.secure_code = code;
                    usrModel.id = Convert.ToInt64(response.Value);
                    var result = _usersApiService.UpdateUsersSecureCode(usrModel);

                    string templateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailTemplate");
                    string emailBody = System.IO.File.ReadAllText(templateFolder + "/VerifyEmailTemplate" + ".html");
                    emailBody = emailBody.Replace("[VERIFYLINK]", callbackUrl);

                    _emailClient.SendEmailAsync(viewModel.email, "My World : Confirm your email", emailBody);
                }
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult UserProfile()
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var account = _usersApiService.GetUsers(Userdata);

            if (account == null)
            {
                ModelState.AddModelError("Error", "User name or password is not valid.");
                return RedirectToAction("Login", "Account");
            }

            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<UsersModel, UserProfileViewModel>()
               );

            var mapper = new Mapper(config);
            var userProfileViewModel = mapper.Map<UserProfileViewModel>(account);

            return View(userProfileViewModel);
        }

        [HttpPost]
        public IActionResult UserProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<UserProfileViewModel, UsersModel>()
               );

            var mapper = new Mapper(config);
            var usersModel = mapper.Map<UsersModel>(model);

            usersModel.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var account = _usersApiService.UpdateUsersProfile(usersModel);

            if (account == null)
            {
                ModelState.AddModelError("Error", "User name or password is not valid.");
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("UserProfile");
        }

        [HttpGet]
        public IActionResult AccountSetting()
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var account = _usersApiService.GetUsers(Userdata);

            if (account == null)
            {
                ModelState.AddModelError("Error", "User name or password is not valid.");
                return RedirectToAction("Login", "Account");
            }

            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<UsersModel, UserAccountViewModel>()
               );

            var mapper = new Mapper(config);
            var userAccountViewModel = mapper.Map<UserAccountViewModel>(account);

            return View(userAccountViewModel);
        }

        [HttpPost]
        public IActionResult AccountSetting(UserAccountViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<UserAccountViewModel, UsersModel>()
               );

            var mapper = new Mapper(config);
            var usersModel = mapper.Map<UsersModel>(model);

            usersModel.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var account = _usersApiService.UpdateUsersAccount(usersModel);

            if (account == null)
            {
                ModelState.AddModelError("Error", "User name or password is not valid.");
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("AccountSetting");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var account = _usersApiService.GetUsers(Userdata);

            if (account == null)
            {
                ModelState.AddModelError("Error", "User name or password is not valid.");
                return RedirectToAction("Login", "Account");
            }

            return View(account);
        }

        [HttpPost]
        public IActionResult ChangePassword(UsersModel model)
        {
            if (!ModelState.IsValid)
                return View();

            UsersModel data = (UsersModel)model;
            data.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var account = _usersApiService.UpdateUsersPassword(data);

            if (account == null)
            {
                ModelState.AddModelError("Error", "User name or password is not valid.");
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("AccountSetting");
        }

        [HttpGet]
        public IActionResult UserPlanDetails()
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);

            UserPlansViewModel viewModel = new UserPlansViewModel();
            var account = _usersApiService.GetUsers(Userdata);

            if (account == null)
            {
                ModelState.AddModelError("Error", "User name or password is not valid.");
                return RedirectToAction("Login", "Account");
            }

            viewModel.UsersPlan = account.user_plan;
            viewModel.ContentPlans = _contentPlansService.GetAllContentPlans();
            viewModel.ContentPlans = viewModel.ContentPlans.FindAll(p => p.id > viewModel.UsersPlan.id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UserPlanDetails(UserPlansViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            //UserPlansViewModel data = (UserPlansViewModel)model;

            //data.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            //var account = _usersApiService.UpdateUsersPassword(data);

            //if (account == null)
            //{
            //    ModelState.AddModelError("Error", "User name or password is not valid.");
            //    return RedirectToAction("Login", "Account");
            //}
            return RedirectToAction("AccountSetting");
        }

        public IActionResult UpgradePlan(string plan_id)
        {
            UsersModel Userdata = new UsersModel();
            Userdata.id = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);

            var account = _usersApiService.GetUsers(Userdata);

            if (account == null)
            {
                ModelState.AddModelError("Error", "User name or password is not valid.");
                return RedirectToAction("Login", "Account");
            }
            Userdata.user_plan = new ContentPlansModel();
            Userdata.user_plan.id = Convert.ToInt32(plan_id);
            var result = _usersApiService.UpdateUsersPlan(Userdata);

            if (result == "1")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Login");
        }

        public ActionResult Notification()
        {
            return View();
        }
    }
}
