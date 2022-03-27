using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using My.World.Api.DataAccess;
using My.World.Api.Models;
using My.World.Api.Services;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace My.World.Api.Controllers
{
    [Route("myworld")]
    [ApiController]
    public class MyWorldController : ControllerBase
    {
        DBContext _dbContext = null;

        private readonly ILogger<MyWorldController> _logger;

        public MyWorldController(IServiceProvider services, ILogger<MyWorldController> logger)
        {
            _dbContext = services.GetService(typeof(DBContext)) as DBContext;
            _logger = logger;
        }

        // GET: api/<MyWorldController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        private string GetRawContent(string _rawContent)
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return reader.ReadToEndAsync().Result;
            }
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            _logger.LogInformation("Exception happened.");
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context != null)
            {
                _logger.LogError(context.Error?.Message);
            }
            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [HttpPost]
        [Route("GetDashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<DashboardModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DashboardModel>(_rawContent);
                var dashboardService = new DashboardService(_dbContext);
                responseModel = dashboardService.GetDashboard(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpGet]
        [Route("GetAllContentTypes")]
        public async Task<IActionResult> GetAllContentTypes()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ContentTypesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var dashboardService = new DashboardService(_dbContext);
                responseModel = dashboardService.GetAllContentTypes();
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpGet]
        [Route("GetRecents/{userId}")]
        public async Task<IActionResult> GetRecents(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<DashboardRecentModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var dashboardService = new DashboardService(_dbContext);
                responseModel = dashboardService.GetRecentData(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }


        [HttpGet]
        [Route("GetMentions/{userId}")]
        public async Task<IActionResult> GetMentions(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<MentionsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var dashboardService = new DashboardService(_dbContext);
                responseModel = dashboardService.GetMentions(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpPost]
        [Route("GetUsersDataByEmail")]
        public async Task<IActionResult> GetUsersDataByEmail()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<UsersModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var userService = new UsersService(_dbContext);
                responseModel = userService.GetUsersDataByEmail(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpPost]
        [Route("SignupUser")]
        public async Task<IActionResult> SignupUser()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var userService = new UsersService(_dbContext);
                responseModel = userService.SignupUser(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser()
        {
            string _rawContent = null;
            var responseModel = new ResponseModel<UsersModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var userService = new UsersService(_dbContext);
                responseModel = userService.LoginUser(objPayLoad);
                if (responseModel.IsSuccess)
                {
                    responseModel.Value.last_sign_in_ip = responseModel.Value.current_sign_in_ip;
                    responseModel.Value.last_sign_in_at = responseModel.Value.current_sign_in_at;
                    responseModel.Value.current_sign_in_ip = HttpContext.Connection.RemoteIpAddress.ToString();
                    userService.UpdateUsersSignInData(responseModel.Value);
                }
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpPost]
        [Route("GetUsersContentTemplate")]
        public async Task<IActionResult> GetUsersContentTemplate()
        {
            string _rawContent = null;
            var responseModel = new ResponseModel<ContentTemplateModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var userService = new UsersService(_dbContext);
                responseModel = userService.GetUsersContentTemplate(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpPost]
        [Route("UpdateUsersContentTemplate")]
        public async Task<IActionResult> UpdateUsersContentTemplate()
        {
            string _rawContent = null;
            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var userService = new UsersService(_dbContext);
                responseModel = userService.UpdateUsersContentTemplate(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpPost]
        [Route("UpdateUsersEmailConfirm")]
        public async Task<IActionResult> UpdateUsersEmailConfirm()
        {
            string _rawContent = null;
            var responseModel = new ResponseModel<UsersModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var userService = new UsersService(_dbContext);
                responseModel = userService.UpdateUsersEmailConfirmData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpPost]
        [Route("UpdateUsersSecureCode")]
        public async Task<IActionResult> UpdateUsersSecureCode()
        {
            string _rawContent = null;
            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var userService = new UsersService(_dbContext);
                responseModel = userService.UpdateUsersSecureCodeData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }
            return new JsonResult(responseModel);
        }

        [HttpPost]
        [Route("AddUserDetails")]
        public async Task<IActionResult> AddUserDetails()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UserDetailsModel>(_rawContent);
                var user_detailsService = new UserdetailsService(_dbContext);
                responseModel = user_detailsService.AddUserDetailsData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UserDetailsModel>(_rawContent);
                var user_detailsService = new UserdetailsService(_dbContext);
                responseModel = user_detailsService.UpdateUserDetailsData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<UserDetailsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UserDetailsModel>(_rawContent);
                var user_detailsService = new UserdetailsService(_dbContext);
                responseModel = user_detailsService.GetUserDetailsData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("DeleteUserDetails")]
        public async Task<IActionResult> DeleteUserDetails()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UserDetailsModel>(_rawContent);
                var user_detailsService = new UserdetailsService(_dbContext);
                responseModel = user_detailsService.DeleteUserDetailsData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllUserDetails")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<UserDetailsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var user_detailsService = new UserdetailsService(_dbContext);
                responseModel = user_detailsService.GetAllUserDetailsData();
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("AddUsers")]
        public async Task<IActionResult> AddUsers()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.AddUsersData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("UpdateUsers")]
        public async Task<IActionResult> UpdateUsers()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.UpdateUsersData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("UpdateUsersProfile")]
        public async Task<IActionResult> UpdateUsersProfile()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.UpdateUsersProfileData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("UpdateUsersAccount")]
        public async Task<IActionResult> UpdateUsersAccountData()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.UpdateUsersAccountData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("UpdateUsersPassword")]
        public async Task<IActionResult> UpdateUsersPasswordData()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.UpdateUsersPasswordData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<UsersModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.GetUsersData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("DeleteUsers")]
        public async Task<IActionResult> DeleteUsers()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.DeleteUsersData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<UsersModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.GetAllUsersData();
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetUniverses")]
        public async Task<IActionResult> GetUniverses()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<UniversesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UniversesModel>(_rawContent);
                var universesService = new UniversesService(_dbContext);
                responseModel = universesService.GetUniversesData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllUniverses/{userId}")]
        public async Task<IActionResult> GetAllUniversesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<UniversesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var universesService = new UniversesService(_dbContext);
                responseModel = universesService.GetAllUniversesForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("DeleteUniverses")]
        public async Task<IActionResult> DeleteUniverses()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UniversesModel>(_rawContent);
                var universesService = new UniversesService(_dbContext);
                responseModel = universesService.DeleteUniversesData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("SaveUniverse")]
        public async Task<IActionResult> SaveUniverse()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UniversesModel>(_rawContent);
                var universesService = new UniversesService(_dbContext);
                responseModel = universesService.SaveUniverse(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("AddUniverses")]
        public IActionResult AddUniverses()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UniversesModel>(_rawContent);
                var UniversesService = new UniversesService(_dbContext);
                responseModel = UniversesService.AddUniversesData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }
        [HttpGet]
        [Route("GetAllContentPlans")]
        public async Task<IActionResult> GetAllContentPlans()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ContentPlansModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var contentPlansService = new ContentPlansService(_dbContext);
                responseModel = contentPlansService.GetAllContentPlans();
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("UpdateUsersPlan")]
        public async Task<IActionResult> UpdateUsersPlan()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UsersModel>(_rawContent);
                var usersService = new UsersService(_dbContext);
                responseModel = usersService.UpdateUsersPlan(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);
        }


        [HttpPost]
        [Route("Adddocuments")]
        public async Task<IActionResult> Adddocuments()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DocumentsModel>(_rawContent);
                var documentsService = new DocumentsService(_dbContext);
                responseModel = documentsService.AdddocumentsData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAlldocuments/{userId}")]
        public async Task<IActionResult> GetAlldocuments(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<DocumentsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var documentsService = new DocumentsService(_dbContext);
                responseModel = documentsService.GetAlldocumentsData(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Deletedocuments")]
        public async Task<IActionResult> Deletedocuments()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DocumentsModel>(_rawContent);
                var documentsService = new DocumentsService(_dbContext);
                responseModel = documentsService.DeletedocumentsData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Getdocuments")]
        public async Task<IActionResult> Getdocuments()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<DocumentsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DocumentsModel>(_rawContent);
                var documentsService = new DocumentsService(_dbContext);
                responseModel = documentsService.GetdocumentsData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }


        [HttpPost]
        [Route("Savedocuments")]
        public async Task<IActionResult> Savedocuments()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DocumentsModel>(_rawContent);
                var documentsService = new DocumentsService(_dbContext);
                responseModel = documentsService.Savedocuments(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }


        [HttpPost]
        [Route("Addfolders")]
        public async Task<IActionResult> Addfolders()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FoldersModel>(_rawContent);
                var FoldersService = new FoldersService(_dbContext);
                responseModel = FoldersService.AddfoldersData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Updatefolders")]
        public async Task<IActionResult> Updatefolders()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FoldersModel>(_rawContent);
                var FoldersService = new FoldersService(_dbContext);
                responseModel = FoldersService.UpdatefoldersData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Getfolders")]
        public async Task<IActionResult> Getfolders()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<FoldersModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FoldersModel>(_rawContent);
                var FoldersService = new FoldersService(_dbContext);
                responseModel = FoldersService.GetfoldersData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("Deletefolders")]
        public async Task<IActionResult> Deletefolders()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FoldersModel>(_rawContent);
                var FoldersService = new FoldersService(_dbContext);
                responseModel = FoldersService.DeletefoldersData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllfolders/{userId}")]
        public async Task<IActionResult> GetAllfolders(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<FoldersModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoldersService = new FoldersService(_dbContext);
                responseModel = FoldersService.GetAllfoldersData(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }


        [HttpGet]
        [Route("GetAllFolderDocuments/{userId}/{folderId}")]
        public async Task<IActionResult> GetAllFolderDocuments(long userId, long folderId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<DocumentsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var documentsService = new DocumentsService(_dbContext);
                responseModel = documentsService.GetAllFolderDocumentsData(userId, folderId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);
        }


        [HttpGet]
        [Route("GetAllChildFolders/{folderId}")]
        public async Task<IActionResult> GetAllChildFolders(long folderId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<FoldersModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoldersService = new FoldersService(_dbContext);
                responseModel = FoldersService.GetAllChildFoldersData(folderId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetEligibleParentFolders/{userId}/{folderId}")]
        public async Task<IActionResult> GetEligibleParentFolders(long userId, long folderId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<FoldersModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoldersService = new FoldersService(_dbContext);
                responseModel = FoldersService.GetEligibleParentFoldersData(userId, folderId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetContentTypes")]
        public IActionResult GetContentTypes()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ContentTypesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentTypesModel>(_rawContent);
                var ContentTypesService = new ContentTypesService(_dbContext);
                responseModel = ContentTypesService.GetContentTypesData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetContentDetailsFromTypeID/{contentType}/{contentId}")]
        public async Task<IActionResult> GetContentDetailsFromTypeID(string contentType, string contentId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<BaseModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var DashboardService = new DashboardService(_dbContext);
                responseModel = DashboardService.GetContentDetailsFromTypeID(contentType, contentId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        #region UserContentBucket
        [HttpPost]
        [Route("AddUserContentBucket")]
        public IActionResult AddUserContentBucket()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UserContentBucketModel>(_rawContent);
                var UserContentBucketService = new UserContentBucketService(_dbContext);
                responseModel = UserContentBucketService.AddUserContentBucketData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetUserContentBucket")]
        public IActionResult GetUserContentBucket()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<UserContentBucketModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UserContentBucketModel>(_rawContent);
                var UserContentBucketService = new UserContentBucketService(_dbContext);
                responseModel = UserContentBucketService.GetUserContentBucketData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("DeleteUserContentBucket")]
        public IActionResult DeleteUserContentBucket()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UserContentBucketModel>(_rawContent);
                var UserContentBucketService = new UserContentBucketService(_dbContext);
                responseModel = UserContentBucketService.DeleteUserContentBucketData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllUserContentBucket/{userId}")]
        public IActionResult GetAllUserContentBucketForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<UserContentBucketModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var UserContentBucketService = new UserContentBucketService(_dbContext);
                responseModel = UserContentBucketService.GetAllUserContentBucketForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("SaveUsercontentbucke")]
        public IActionResult SaveUsercontentbucke()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<UserContentBucketModel>(_rawContent);
                var UserContentBucketService = new UserContentBucketService(_dbContext);
                responseModel = UserContentBucketService.SaveUsercontentbucke(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }
        #endregion UserContentBucket

        #region ContentObject
        [HttpPost]
        [Route("AddContentObject")]
        public IActionResult AddContentObject()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentObjectModel>(_rawContent);
                var ContentObjectService = new ContentObjectService(_dbContext);
                responseModel = ContentObjectService.AddContentObjectData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetContentObject")]
        public IActionResult GetContentObject()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ContentObjectModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentObjectModel>(_rawContent);
                var ContentObjectService = new ContentObjectService(_dbContext);
                responseModel = ContentObjectService.GetContentObjectData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("DeleteContentObject")]
        public IActionResult DeleteContentObject()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentObjectModel>(_rawContent);
                var ContentObjectService = new ContentObjectService(_dbContext);
                responseModel = ContentObjectService.DeleteContentObjectData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllContentObject/{userId}")]
        public IActionResult GetAllContentObjectForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ContentObjectModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContentObjectService = new ContentObjectService(_dbContext);
                responseModel = ContentObjectService.GetAllContentObjectForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("SaveContentobjec")]
        public IActionResult SaveContentobjec()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentObjectModel>(_rawContent);
                var ContentObjectService = new ContentObjectService(_dbContext);
                responseModel = ContentObjectService.SaveContentobjec(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }


        [HttpGet]
        [Route("GetAllContentObjectAttachments/{content_id}/{content_type}")]
        public IActionResult GetAllContentObjectAttachments(long content_id, string content_type)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ContentObjectModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContentObjectService = new ContentObjectService(_dbContext);
                responseModel = ContentObjectService.GetAllContentObjectAttachments(content_id, content_type);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        #endregion ContentObject

        #region ContentObjectAttachment
        [HttpPost]
        [Route("AddContentObjectAttachment")]
        public IActionResult AddContentObjectAttachment()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentObjectAttachmentModel>(_rawContent);
                var ContentObjectAttachmentService = new ContentObjectAttachmentService(_dbContext);
                responseModel = ContentObjectAttachmentService.AddContentObjectAttachmentData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetContentObjectAttachment")]
        public IActionResult GetContentObjectAttachment()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ContentObjectAttachmentModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentObjectAttachmentModel>(_rawContent);
                var ContentObjectAttachmentService = new ContentObjectAttachmentService(_dbContext);
                responseModel = ContentObjectAttachmentService.GetContentObjectAttachmentData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("DeleteContentObjectAttachment")]
        public IActionResult DeleteContentObjectAttachment()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentObjectAttachmentModel>(_rawContent);
                var ContentObjectAttachmentService = new ContentObjectAttachmentService(_dbContext);
                responseModel = ContentObjectAttachmentService.DeleteContentObjectAttachmentData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllContentObjectAttachment/{userId}")]
        public IActionResult GetAllContentObjectAttachmentForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ContentObjectAttachmentModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContentObjectAttachmentService = new ContentObjectAttachmentService(_dbContext);
                responseModel = ContentObjectAttachmentService.GetAllContentObjectAttachmentForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("SaveContentobjectattachmen")]
        public IActionResult SaveContentobjectattachmen()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContentObjectAttachmentModel>(_rawContent);
                var ContentObjectAttachmentService = new ContentObjectAttachmentService(_dbContext);
                responseModel = ContentObjectAttachmentService.SaveContentobjectattachmen(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }
        #endregion ContentObjectAttachment

        #region ObjectStorageKeys
        [HttpPost]
        [Route("AddObjectStorageKeys")]
        public IActionResult AddObjectStorageKeys()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ObjectStorageKeysModel>(_rawContent);
                var ObjectStorageKeysService = new ObjectStorageKeysService(_dbContext);
                responseModel = ObjectStorageKeysService.AddObjectStorageKeysData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetObjectStorageKeys")]
        public IActionResult GetObjectStorageKeys()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ObjectStorageKeysModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ObjectStorageKeysModel>(_rawContent);
                var ObjectStorageKeysService = new ObjectStorageKeysService(_dbContext);
                responseModel = ObjectStorageKeysService.GetObjectStorageKeysData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("DeleteObjectStorageKeys")]
        public IActionResult DeleteObjectStorageKeys()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ObjectStorageKeysModel>(_rawContent);
                var ObjectStorageKeysService = new ObjectStorageKeysService(_dbContext);
                responseModel = ObjectStorageKeysService.DeleteObjectStorageKeysData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllObjectStorageKeys/{userId}")]
        public IActionResult GetAllObjectStorageKeysForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ObjectStorageKeysModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ObjectStorageKeysService = new ObjectStorageKeysService(_dbContext);
                responseModel = ObjectStorageKeysService.GetAllObjectStorageKeysForUserID(userId);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("SaveObjectstoragekey")]
        public IActionResult SaveObjectstoragekey()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ObjectStorageKeysModel>(_rawContent);
                var ObjectStorageKeysService = new ObjectStorageKeysService(_dbContext);
                responseModel = ObjectStorageKeysService.SaveObjectstoragekey(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }
        #endregion ObjectStorageKeys

        #region AppConfig
        [HttpPost]
        [Route("AddAppConfig")]
        public IActionResult AddAppConfig()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<AppConfigModel>(_rawContent);
                var AppConfigService = new AppConfigService(_dbContext);
                responseModel = AppConfigService.AddAppConfigData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("GetAppConfig")]
        public IActionResult GetAppConfig()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<AppConfigModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<AppConfigModel>(_rawContent);
                var AppConfigService = new AppConfigService(_dbContext);
                responseModel = AppConfigService.GetAppConfigData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("DeleteAppConfig")]
        public IActionResult DeleteAppConfig()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<AppConfigModel>(_rawContent);
                var AppConfigService = new AppConfigService(_dbContext);
                responseModel = AppConfigService.DeleteAppConfigData(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpGet]
        [Route("GetAllAppConfig")]
        public IActionResult GetAllAppConfigForUserID()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<AppConfigModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var AppConfigService = new AppConfigService(_dbContext);
                responseModel = AppConfigService.GetAllAppConfigForUserID();
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }

        [HttpPost]
        [Route("SaveAppconfig")]
        public IActionResult SaveAppconfig(AppConfigModel model)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<AppConfigModel>(_rawContent);
                var AppConfigService = new AppConfigService(_dbContext);
                responseModel = AppConfigService.SaveAppConfig(objPayLoad);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }


        [HttpPost]
        [Route("UpdateAppconfig")]
        public IActionResult UpdateAppconfig(AppConfigModel model)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var AppConfigService = new AppConfigService(_dbContext);
                responseModel = AppConfigService.UpdateAppConfig(model);
            }
            catch (Exception ex)
            {
                string message = "Error while processing ";

                if (!ex.Message.ToLower().Contains("object reference"))
                    message += ex.Message;

                responseModel.HttpStatusCode = ((int)HttpStatusCode.BadRequest).ToString();
                responseModel.Message = message;
                responseModel.Reason.Add("ERROR");
                responseModel.IsSuccess = false;
            }

            return new JsonResult(responseModel);

        }
        #endregion AppConfig

    }
}
