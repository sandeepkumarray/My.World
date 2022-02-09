using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My.World.Api.DataAccess;
using My.World.Api.Models;
using My.World.Api.Services;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace My.World.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyWorldController : ControllerBase
    {
        DBContext _dbContext = null;

        public MyWorldController(IServiceProvider services)
        {
            _dbContext = services.GetService(typeof(DBContext)) as DBContext;
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
        [Route("GetAllUniversesForUserID/{userId}")]
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

        #region Characters
        [HttpPost]
        [Route("AddCharacters")]
        public IActionResult AddCharacters()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CharactersModel>(_rawContent);
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.AddCharactersData(objPayLoad);
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
        [Route("GetCharacters")]
        public IActionResult GetCharacters()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<CharactersModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CharactersModel>(_rawContent);
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.GetCharactersData(objPayLoad);
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
        [Route("DeleteCharacters")]
        public IActionResult DeleteCharacters()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CharactersModel>(_rawContent);
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.DeleteCharactersData(objPayLoad);
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
        [Route("GetAllCharacters/{userId}")]
        public IActionResult GetAllCharactersForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<CharactersModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.GetAllCharactersForUserID(userId);
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
        [Route("SaveCharacter")]
        public IActionResult SaveCharacter()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CharactersModel>(_rawContent);
                var CharactersService = new CharactersService(_dbContext);
                responseModel = CharactersService.SaveCharacter(objPayLoad);
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
        #endregion Characters

        #region Items   

        [HttpPost]
        [Route("AddItems")]
        public IActionResult AddItems()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ItemsModel>(_rawContent);
                var ItemsService = new ItemsService(_dbContext);
                responseModel = ItemsService.AddItemsData(objPayLoad);
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
        [Route("GetItems")]
        public async Task<IActionResult> GetItems()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ItemsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ItemsModel>(_rawContent);
                var itemsService = new ItemsService(_dbContext);
                responseModel = itemsService.GetItemsData(objPayLoad);
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
        [Route("DeleteItems")]
        public async Task<IActionResult> DeleteItems()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ItemsModel>(_rawContent);
                var itemsService = new ItemsService(_dbContext);
                responseModel = itemsService.DeleteItemsData(objPayLoad);
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
        [Route("GetAllItems/{userId}")]
        public async Task<IActionResult> GetAllItems(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ItemsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var itemsService = new ItemsService(_dbContext);
                responseModel = itemsService.GetAllItemsData(userId);
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
        [Route("SaveItem")]
        public IActionResult SaveItem()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ItemsModel>(_rawContent);
                var ItemsService = new ItemsService(_dbContext);
                responseModel = ItemsService.SaveItem(objPayLoad);
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
        #endregion Items

        #region Floras

        [HttpPost]
        [Route("AddFloras")]
        public IActionResult AddFloras()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FlorasModel>(_rawContent);
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.AddFlorasData(objPayLoad);
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
        [Route("GetFloras")]
        public IActionResult GetFloras()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<FlorasModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FlorasModel>(_rawContent);
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.GetFlorasData(objPayLoad);
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
        [Route("DeleteFloras")]
        public IActionResult DeleteFloras()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FlorasModel>(_rawContent);
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.DeleteFlorasData(objPayLoad);
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
        [Route("GetAllFloras/{userId}")]
        public IActionResult GetAllFlorasForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<FlorasModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.GetAllFlorasForUserID(userId);
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
        [Route("SaveFlora")]
        public IActionResult SaveFlora()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FlorasModel>(_rawContent);
                var FlorasService = new FlorasService(_dbContext);
                responseModel = FlorasService.SaveFlora(objPayLoad);
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


        #endregion

        #region Buildings
        [HttpPost]
        [Route("AddBuildings")]
        public IActionResult AddBuildings()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<BuildingsModel>(_rawContent);
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.AddBuildingsData(objPayLoad);
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
        [Route("GetBuildings")]
        public IActionResult GetBuildings()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<BuildingsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<BuildingsModel>(_rawContent);
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.GetBuildingsData(objPayLoad);
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
        [Route("DeleteBuildings")]
        public IActionResult DeleteBuildings()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<BuildingsModel>(_rawContent);
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.DeleteBuildingsData(objPayLoad);
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
        [Route("GetAllBuildings/{userId}")]
        public IActionResult GetAllBuildingsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<BuildingsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.GetAllBuildingsForUserID(userId);
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
        [Route("SaveBuilding")]
        public IActionResult SaveBuilding()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<BuildingsModel>(_rawContent);
                var BuildingsService = new BuildingsService(_dbContext);
                responseModel = BuildingsService.SaveBuilding(objPayLoad);
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
        #endregion Buildings

        #region Conditions
        [HttpPost]
        [Route("AddConditions")]
        public IActionResult AddConditions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ConditionsModel>(_rawContent);
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.AddConditionsData(objPayLoad);
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
        [Route("GetConditions")]
        public IActionResult GetConditions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ConditionsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ConditionsModel>(_rawContent);
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.GetConditionsData(objPayLoad);
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
        [Route("DeleteConditions")]
        public IActionResult DeleteConditions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ConditionsModel>(_rawContent);
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.DeleteConditionsData(objPayLoad);
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
        [Route("GetAllConditions/{userId}")]
        public IActionResult GetAllConditionsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ConditionsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.GetAllConditionsForUserID(userId);
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
        [Route("SaveCondition")]
        public IActionResult SaveCondition()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ConditionsModel>(_rawContent);
                var ConditionsService = new ConditionsService(_dbContext);
                responseModel = ConditionsService.SaveCondition(objPayLoad);
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
        #endregion Conditions

        #region Continents
        [HttpPost]
        [Route("AddContinents")]
        public IActionResult AddContinents()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContinentsModel>(_rawContent);
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.AddContinentsData(objPayLoad);
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
        [Route("GetContinents")]
        public IActionResult GetContinents()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ContinentsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContinentsModel>(_rawContent);
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.GetContinentsData(objPayLoad);
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
        [Route("DeleteContinents")]
        public IActionResult DeleteContinents()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContinentsModel>(_rawContent);
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.DeleteContinentsData(objPayLoad);
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
        [Route("GetAllContinents/{userId}")]
        public IActionResult GetAllContinentsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ContinentsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.GetAllContinentsForUserID(userId);
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
        [Route("SaveContinent")]
        public IActionResult SaveContinent()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ContinentsModel>(_rawContent);
                var ContinentsService = new ContinentsService(_dbContext);
                responseModel = ContinentsService.SaveContinent(objPayLoad);
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
        #endregion Continents

        #region Countries
        [HttpPost]
        [Route("AddCountries")]
        public IActionResult AddCountries()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CountriesModel>(_rawContent);
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.AddCountriesData(objPayLoad);
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
        [Route("GetCountries")]
        public IActionResult GetCountries()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<CountriesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CountriesModel>(_rawContent);
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.GetCountriesData(objPayLoad);
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
        [Route("DeleteCountries")]
        public IActionResult DeleteCountries()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CountriesModel>(_rawContent);
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.DeleteCountriesData(objPayLoad);
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
        [Route("GetAllCountries/{userId}")]
        public IActionResult GetAllCountriesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<CountriesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.GetAllCountriesForUserID(userId);
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
        [Route("SaveCountrie")]
        public IActionResult SaveCountrie()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CountriesModel>(_rawContent);
                var CountriesService = new CountriesService(_dbContext);
                responseModel = CountriesService.SaveCountrie(objPayLoad);
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
        #endregion Countries

        #region Creatures
        [HttpPost]
        [Route("AddCreatures")]
        public IActionResult AddCreatures()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CreaturesModel>(_rawContent);
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.AddCreaturesData(objPayLoad);
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
        [Route("GetCreatures")]
        public IActionResult GetCreatures()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<CreaturesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CreaturesModel>(_rawContent);
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.GetCreaturesData(objPayLoad);
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
        [Route("DeleteCreatures")]
        public IActionResult DeleteCreatures()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CreaturesModel>(_rawContent);
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.DeleteCreaturesData(objPayLoad);
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
        [Route("GetAllCreatures/{userId}")]
        public IActionResult GetAllCreaturesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<CreaturesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.GetAllCreaturesForUserID(userId);
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
        [Route("SaveCreature")]
        public IActionResult SaveCreature()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<CreaturesModel>(_rawContent);
                var CreaturesService = new CreaturesService(_dbContext);
                responseModel = CreaturesService.SaveCreature(objPayLoad);
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
        #endregion Creatures

        #region Deities
        [HttpPost]
        [Route("AddDeities")]
        public IActionResult AddDeities()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DeitiesModel>(_rawContent);
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.AddDeitiesData(objPayLoad);
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
        [Route("GetDeities")]
        public IActionResult GetDeities()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<DeitiesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DeitiesModel>(_rawContent);
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.GetDeitiesData(objPayLoad);
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
        [Route("DeleteDeities")]
        public IActionResult DeleteDeities()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DeitiesModel>(_rawContent);
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.DeleteDeitiesData(objPayLoad);
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
        [Route("GetAllDeities/{userId}")]
        public IActionResult GetAllDeitiesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<DeitiesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.GetAllDeitiesForUserID(userId);
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
        [Route("SaveDeitie")]
        public IActionResult SaveDeitie()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<DeitiesModel>(_rawContent);
                var DeitiesService = new DeitiesService(_dbContext);
                responseModel = DeitiesService.SaveDeitie(objPayLoad);
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
        #endregion Deities

        #region Foods
        [HttpPost]
        [Route("AddFoods")]
        public IActionResult AddFoods()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FoodsModel>(_rawContent);
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.AddFoodsData(objPayLoad);
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
        [Route("GetFoods")]
        public IActionResult GetFoods()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<FoodsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FoodsModel>(_rawContent);
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.GetFoodsData(objPayLoad);
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
        [Route("DeleteFoods")]
        public IActionResult DeleteFoods()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FoodsModel>(_rawContent);
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.DeleteFoodsData(objPayLoad);
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
        [Route("GetAllFoods/{userId}")]
        public IActionResult GetAllFoodsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<FoodsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.GetAllFoodsForUserID(userId);
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
        [Route("SaveFood")]
        public IActionResult SaveFood()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<FoodsModel>(_rawContent);
                var FoodsService = new FoodsService(_dbContext);
                responseModel = FoodsService.SaveFood(objPayLoad);
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
        #endregion Foods

        #region Governments
        [HttpPost]
        [Route("AddGovernments")]
        public IActionResult AddGovernments()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<GovernmentsModel>(_rawContent);
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.AddGovernmentsData(objPayLoad);
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
        [Route("GetGovernments")]
        public IActionResult GetGovernments()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<GovernmentsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<GovernmentsModel>(_rawContent);
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.GetGovernmentsData(objPayLoad);
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
        [Route("DeleteGovernments")]
        public IActionResult DeleteGovernments()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<GovernmentsModel>(_rawContent);
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.DeleteGovernmentsData(objPayLoad);
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
        [Route("GetAllGovernments/{userId}")]
        public IActionResult GetAllGovernmentsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<GovernmentsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.GetAllGovernmentsForUserID(userId);
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
        [Route("SaveGovernment")]
        public IActionResult SaveGovernment()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<GovernmentsModel>(_rawContent);
                var GovernmentsService = new GovernmentsService(_dbContext);
                responseModel = GovernmentsService.SaveGovernment(objPayLoad);
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
        #endregion Governments

        #region Groups
        [HttpPost]
        [Route("AddGroups")]
        public IActionResult AddGroups()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<GroupsModel>(_rawContent);
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.AddGroupsData(objPayLoad);
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
        [Route("GetGroups")]
        public IActionResult GetGroups()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<GroupsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<GroupsModel>(_rawContent);
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.GetGroupsData(objPayLoad);
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
        [Route("DeleteGroups")]
        public IActionResult DeleteGroups()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<GroupsModel>(_rawContent);
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.DeleteGroupsData(objPayLoad);
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
        [Route("GetAllGroups/{userId}")]
        public IActionResult GetAllGroupsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<GroupsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.GetAllGroupsForUserID(userId);
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
        [Route("SaveGroup")]
        public IActionResult SaveGroup()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<GroupsModel>(_rawContent);
                var GroupsService = new GroupsService(_dbContext);
                responseModel = GroupsService.SaveGroup(objPayLoad);
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
        #endregion Groups

        #region Jobs
        [HttpPost]
        [Route("AddJobs")]
        public IActionResult AddJobs()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<JobsModel>(_rawContent);
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.AddJobsData(objPayLoad);
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
        [Route("GetJobs")]
        public IActionResult GetJobs()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<JobsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<JobsModel>(_rawContent);
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.GetJobsData(objPayLoad);
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
        [Route("DeleteJobs")]
        public IActionResult DeleteJobs()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<JobsModel>(_rawContent);
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.DeleteJobsData(objPayLoad);
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
        [Route("GetAllJobs/{userId}")]
        public IActionResult GetAllJobsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<JobsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.GetAllJobsForUserID(userId);
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
        [Route("SaveJob")]
        public IActionResult SaveJob()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<JobsModel>(_rawContent);
                var JobsService = new JobsService(_dbContext);
                responseModel = JobsService.SaveJob(objPayLoad);
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
        #endregion Jobs

        #region Landmarks
        [HttpPost]
        [Route("AddLandmarks")]
        public IActionResult AddLandmarks()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LandmarksModel>(_rawContent);
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.AddLandmarksData(objPayLoad);
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
        [Route("GetLandmarks")]
        public IActionResult GetLandmarks()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<LandmarksModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LandmarksModel>(_rawContent);
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.GetLandmarksData(objPayLoad);
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
        [Route("DeleteLandmarks")]
        public IActionResult DeleteLandmarks()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LandmarksModel>(_rawContent);
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.DeleteLandmarksData(objPayLoad);
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
        [Route("GetAllLandmarks/{userId}")]
        public IActionResult GetAllLandmarksForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<LandmarksModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.GetAllLandmarksForUserID(userId);
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
        [Route("SaveLandmark")]
        public IActionResult SaveLandmark()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LandmarksModel>(_rawContent);
                var LandmarksService = new LandmarksService(_dbContext);
                responseModel = LandmarksService.SaveLandmark(objPayLoad);
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
        #endregion Landmarks

        #region Languages
        [HttpPost]
        [Route("AddLanguages")]
        public IActionResult AddLanguages()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LanguagesModel>(_rawContent);
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.AddLanguagesData(objPayLoad);
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
        [Route("GetLanguages")]
        public IActionResult GetLanguages()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<LanguagesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LanguagesModel>(_rawContent);
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.GetLanguagesData(objPayLoad);
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
        [Route("DeleteLanguages")]
        public IActionResult DeleteLanguages()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LanguagesModel>(_rawContent);
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.DeleteLanguagesData(objPayLoad);
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
        [Route("GetAllLanguages/{userId}")]
        public IActionResult GetAllLanguagesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<LanguagesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.GetAllLanguagesForUserID(userId);
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
        [Route("SaveLanguage")]
        public IActionResult SaveLanguage()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LanguagesModel>(_rawContent);
                var LanguagesService = new LanguagesService(_dbContext);
                responseModel = LanguagesService.SaveLanguage(objPayLoad);
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
        #endregion Languages

        #region Locations
        [HttpPost]
        [Route("AddLocations")]
        public IActionResult AddLocations()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LocationsModel>(_rawContent);
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.AddLocationsData(objPayLoad);
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
        [Route("GetLocations")]
        public IActionResult GetLocations()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<LocationsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LocationsModel>(_rawContent);
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.GetLocationsData(objPayLoad);
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
        [Route("DeleteLocations")]
        public IActionResult DeleteLocations()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LocationsModel>(_rawContent);
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.DeleteLocationsData(objPayLoad);
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
        [Route("GetAllLocations/{userId}")]
        public IActionResult GetAllLocationsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<LocationsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.GetAllLocationsForUserID(userId);
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
        [Route("SaveLocation")]
        public IActionResult SaveLocation()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LocationsModel>(_rawContent);
                var LocationsService = new LocationsService(_dbContext);
                responseModel = LocationsService.SaveLocation(objPayLoad);
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
        #endregion Locations

        #region Lores
        [HttpPost]
        [Route("AddLores")]
        public IActionResult AddLores()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LoresModel>(_rawContent);
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.AddLoresData(objPayLoad);
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
        [Route("GetLores")]
        public IActionResult GetLores()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<LoresModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LoresModel>(_rawContent);
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.GetLoresData(objPayLoad);
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
        [Route("DeleteLores")]
        public IActionResult DeleteLores()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LoresModel>(_rawContent);
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.DeleteLoresData(objPayLoad);
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
        [Route("GetAllLores/{userId}")]
        public IActionResult GetAllLoresForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<LoresModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.GetAllLoresForUserID(userId);
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
        [Route("SaveLore")]
        public IActionResult SaveLore()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<LoresModel>(_rawContent);
                var LoresService = new LoresService(_dbContext);
                responseModel = LoresService.SaveLore(objPayLoad);
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
        #endregion Lores

        #region Magics
        [HttpPost]
        [Route("AddMagics")]
        public IActionResult AddMagics()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<MagicsModel>(_rawContent);
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.AddMagicsData(objPayLoad);
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
        [Route("GetMagics")]
        public IActionResult GetMagics()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<MagicsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<MagicsModel>(_rawContent);
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.GetMagicsData(objPayLoad);
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
        [Route("DeleteMagics")]
        public IActionResult DeleteMagics()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<MagicsModel>(_rawContent);
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.DeleteMagicsData(objPayLoad);
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
        [Route("GetAllMagics/{userId}")]
        public IActionResult GetAllMagicsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<MagicsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.GetAllMagicsForUserID(userId);
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
        [Route("SaveMagic")]
        public IActionResult SaveMagic()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<MagicsModel>(_rawContent);
                var MagicsService = new MagicsService(_dbContext);
                responseModel = MagicsService.SaveMagic(objPayLoad);
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
        #endregion Magics

        #region Planets
        [HttpPost]
        [Route("AddPlanets")]
        public IActionResult AddPlanets()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<PlanetsModel>(_rawContent);
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.AddPlanetsData(objPayLoad);
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
        [Route("GetPlanets")]
        public IActionResult GetPlanets()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<PlanetsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<PlanetsModel>(_rawContent);
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.GetPlanetsData(objPayLoad);
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
        [Route("DeletePlanets")]
        public IActionResult DeletePlanets()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<PlanetsModel>(_rawContent);
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.DeletePlanetsData(objPayLoad);
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
        [Route("GetAllPlanets/{userId}")]
        public IActionResult GetAllPlanetsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<PlanetsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.GetAllPlanetsForUserID(userId);
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
        [Route("SavePlanet")]
        public IActionResult SavePlanet()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<PlanetsModel>(_rawContent);
                var PlanetsService = new PlanetsService(_dbContext);
                responseModel = PlanetsService.SavePlanet(objPayLoad);
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
        #endregion Planets

        #region Races
        [HttpPost]
        [Route("AddRaces")]
        public IActionResult AddRaces()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<RacesModel>(_rawContent);
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.AddRacesData(objPayLoad);
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
        [Route("GetRaces")]
        public IActionResult GetRaces()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<RacesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<RacesModel>(_rawContent);
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.GetRacesData(objPayLoad);
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
        [Route("DeleteRaces")]
        public IActionResult DeleteRaces()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<RacesModel>(_rawContent);
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.DeleteRacesData(objPayLoad);
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
        [Route("GetAllRaces/{userId}")]
        public IActionResult GetAllRacesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<RacesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.GetAllRacesForUserID(userId);
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
        [Route("SaveRace")]
        public IActionResult SaveRace()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<RacesModel>(_rawContent);
                var RacesService = new RacesService(_dbContext);
                responseModel = RacesService.SaveRace(objPayLoad);
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
        #endregion Races

        #region Religions
        [HttpPost]
        [Route("AddReligions")]
        public IActionResult AddReligions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ReligionsModel>(_rawContent);
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.AddReligionsData(objPayLoad);
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
        [Route("GetReligions")]
        public IActionResult GetReligions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ReligionsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ReligionsModel>(_rawContent);
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.GetReligionsData(objPayLoad);
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
        [Route("DeleteReligions")]
        public IActionResult DeleteReligions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ReligionsModel>(_rawContent);
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.DeleteReligionsData(objPayLoad);
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
        [Route("GetAllReligions/{userId}")]
        public IActionResult GetAllReligionsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ReligionsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.GetAllReligionsForUserID(userId);
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
        [Route("SaveReligion")]
        public IActionResult SaveReligion()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ReligionsModel>(_rawContent);
                var ReligionsService = new ReligionsService(_dbContext);
                responseModel = ReligionsService.SaveReligion(objPayLoad);
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
        #endregion Religions

        #region Scenes
        [HttpPost]
        [Route("AddScenes")]
        public IActionResult AddScenes()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ScenesModel>(_rawContent);
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.AddScenesData(objPayLoad);
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
        [Route("GetScenes")]
        public IActionResult GetScenes()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<ScenesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ScenesModel>(_rawContent);
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.GetScenesData(objPayLoad);
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
        [Route("DeleteScenes")]
        public IActionResult DeleteScenes()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ScenesModel>(_rawContent);
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.DeleteScenesData(objPayLoad);
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
        [Route("GetAllScenes/{userId}")]
        public IActionResult GetAllScenesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<ScenesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.GetAllScenesForUserID(userId);
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
        [Route("SaveScene")]
        public IActionResult SaveScene()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<ScenesModel>(_rawContent);
                var ScenesService = new ScenesService(_dbContext);
                responseModel = ScenesService.SaveScene(objPayLoad);
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
        #endregion Scenes

        #region Sports
        [HttpPost]
        [Route("AddSports")]
        public IActionResult AddSports()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<SportsModel>(_rawContent);
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.AddSportsData(objPayLoad);
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
        [Route("GetSports")]
        public IActionResult GetSports()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<SportsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<SportsModel>(_rawContent);
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.GetSportsData(objPayLoad);
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
        [Route("DeleteSports")]
        public IActionResult DeleteSports()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<SportsModel>(_rawContent);
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.DeleteSportsData(objPayLoad);
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
        [Route("GetAllSports/{userId}")]
        public IActionResult GetAllSportsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<SportsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.GetAllSportsForUserID(userId);
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
        [Route("SaveSport")]
        public IActionResult SaveSport()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<SportsModel>(_rawContent);
                var SportsService = new SportsService(_dbContext);
                responseModel = SportsService.SaveSport(objPayLoad);
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
        #endregion Sports

        #region Technologies
        [HttpPost]
        [Route("AddTechnologies")]
        public IActionResult AddTechnologies()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TechnologiesModel>(_rawContent);
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.AddTechnologiesData(objPayLoad);
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
        [Route("GetTechnologies")]
        public IActionResult GetTechnologies()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<TechnologiesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TechnologiesModel>(_rawContent);
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.GetTechnologiesData(objPayLoad);
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
        [Route("DeleteTechnologies")]
        public IActionResult DeleteTechnologies()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TechnologiesModel>(_rawContent);
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.DeleteTechnologiesData(objPayLoad);
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
        [Route("GetAllTechnologies/{userId}")]
        public IActionResult GetAllTechnologiesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<TechnologiesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.GetAllTechnologiesForUserID(userId);
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
        [Route("SaveTechnologie")]
        public IActionResult SaveTechnologie()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TechnologiesModel>(_rawContent);
                var TechnologiesService = new TechnologiesService(_dbContext);
                responseModel = TechnologiesService.SaveTechnologie(objPayLoad);
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
        #endregion Technologies

        #region Towns
        [HttpPost]
        [Route("AddTowns")]
        public IActionResult AddTowns()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TownsModel>(_rawContent);
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.AddTownsData(objPayLoad);
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
        [Route("GetTowns")]
        public IActionResult GetTowns()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<TownsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TownsModel>(_rawContent);
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.GetTownsData(objPayLoad);
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
        [Route("DeleteTowns")]
        public IActionResult DeleteTowns()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TownsModel>(_rawContent);
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.DeleteTownsData(objPayLoad);
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
        [Route("GetAllTowns/{userId}")]
        public IActionResult GetAllTownsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<TownsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.GetAllTownsForUserID(userId);
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
        [Route("SaveTown")]
        public IActionResult SaveTown()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TownsModel>(_rawContent);
                var TownsService = new TownsService(_dbContext);
                responseModel = TownsService.SaveTown(objPayLoad);
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
        #endregion Towns

        #region Traditions
        [HttpPost]
        [Route("AddTraditions")]
        public IActionResult AddTraditions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TraditionsModel>(_rawContent);
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.AddTraditionsData(objPayLoad);
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
        [Route("GetTraditions")]
        public IActionResult GetTraditions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<TraditionsModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TraditionsModel>(_rawContent);
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.GetTraditionsData(objPayLoad);
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
        [Route("DeleteTraditions")]
        public IActionResult DeleteTraditions()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TraditionsModel>(_rawContent);
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.DeleteTraditionsData(objPayLoad);
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
        [Route("GetAllTraditions/{userId}")]
        public IActionResult GetAllTraditionsForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<TraditionsModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.GetAllTraditionsForUserID(userId);
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
        [Route("SaveTradition")]
        public IActionResult SaveTradition()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<TraditionsModel>(_rawContent);
                var TraditionsService = new TraditionsService(_dbContext);
                responseModel = TraditionsService.SaveTradition(objPayLoad);
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
        #endregion Traditions

        #region Vehicles
        [HttpPost]
        [Route("AddVehicles")]
        public IActionResult AddVehicles()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<VehiclesModel>(_rawContent);
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.AddVehiclesData(objPayLoad);
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
        [Route("GetVehicles")]
        public IActionResult GetVehicles()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<VehiclesModel>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<VehiclesModel>(_rawContent);
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.GetVehiclesData(objPayLoad);
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
        [Route("DeleteVehicles")]
        public IActionResult DeleteVehicles()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<VehiclesModel>(_rawContent);
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.DeleteVehiclesData(objPayLoad);
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
        [Route("GetAllVehicles/{userId}")]
        public IActionResult GetAllVehiclesForUserID(long userId)
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<List<VehiclesModel>>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.GetAllVehiclesForUserID(userId);
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
        [Route("SaveVehicle")]
        public IActionResult SaveVehicle()
        {
            string _rawContent = null;

            var responseModel = new ResponseModel<string>()
            {
                HttpStatusCode = "200"
            };

            try
            {
                _rawContent = GetRawContent(_rawContent);
                var objPayLoad = JsonConvert.DeserializeObject<VehiclesModel>(_rawContent);
                var VehiclesService = new VehiclesService(_dbContext);
                responseModel = VehiclesService.SaveVehicle(objPayLoad);
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
        #endregion Vehicles

    }
}
