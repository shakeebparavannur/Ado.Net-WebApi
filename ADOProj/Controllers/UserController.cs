using ADOProj.Models;
using ADOProj.Repository;
using ADOProj.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ADOProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOptions<MySettingsModel> appsettings;

        public UserController(IOptions<MySettingsModel> app)
        {
            appsettings = app;
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult Get()
        {
            var data = DbClientFactory<UserDbClient>.Instance.GetAllUsers(appsettings.Value.DefConnection);
            return Ok(data);
        }
        [HttpPost]
        [Route("SaveUser")]
        public IActionResult SaveUser([FromBody] UsersModel model)
        {
            var msg = new Message<UsersModel>();
            var data = DbClientFactory<UserDbClient>.Instance.SaveUser(model, appsettings.Value.DefConnection);
            if (data == "C200")
            {
                msg.IsSuccess = true;
                if (model.Id == 0)
                    msg.ReturnMessage = "User saved successfully";
                else
                    msg.ReturnMessage = "User updated successfully";
            }
            else if (data == "C201")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Email Id already exists";
            }
            else if (data == "C202")
            {
                msg.IsSuccess = false;
                msg.ReturnMessage = "Mobile Number already exists";
            }
            return Ok(msg);
        }
    }
}
