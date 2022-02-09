using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using My.World.Api.Models;
using System.Threading.Tasks;

namespace My.World.Web.Services
{
    public interface IUsersApiService
    {
        ResponseModel<string> SignupUser(UsersModel model);
        UsersModel LoginUser(UsersModel model);
        string AddUsers(UsersModel model);
        string UpdateUsers(UsersModel model);
        string UpdateUsersProfile(UsersModel model);
        string UpdateUsersAccount(UsersModel model);
        string UpdateUsersPassword(UsersModel model);
        UsersModel GetUsers(UsersModel model);
        UsersModel GetUsersDataByEmail(UsersModel model);
        string DeleteUsers(UsersModel model);
        List<UsersModel> GetAllUsers();
        ResponseModel<UsersModel> UpdateUsersEmailConfirm(UsersModel model);
        ResponseModel<string> UpdateUsersSecureCode(UsersModel model);
        string UpdateUsersPlan(UsersModel model);
        ContentTemplateModel GetUsersContentTemplate(UsersModel model);
        string UpdateUsersContentTemplate(UsersModel model);
    }
}
