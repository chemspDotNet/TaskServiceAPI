using System.Linq;
using System.Web.Http;
using TaskServiceAPI.DAL_Lyr;
using TaskServiceAPI.CustomFilters;
using TaskServiceAPI.Models;
using TaskServiceAPI.AuthenticationService;
using System.Web.Http.Cors;

namespace TaskServiceAPI.Controllers
{
  
    [CustomExceptionFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        static TaskDBContext td = new TaskDBContext();

        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login(UserLoginDTO _user)
        {
            // 
            var user = td.tblUsers.Where((u) => u.Username == _user.Username && u.Password == _user.Password).ToList();
            if(user != null && user.Count>=1)
            {
                UserProfile user_tkn = new UserProfile();
                user_tkn.Username = user[0].Username;
                user_tkn.UserId = user[0].Id;

                AuthenticationModule auth = new AuthenticationModule();
                var token = auth.GenerateTokenForUser(user_tkn.Username, user_tkn.UserId);
                return Ok(token);
            } else
            {
             return   BadRequest();
            }
           
        }

        [HttpPost]
        [Route("api/register")]
        public IHttpActionResult Register(UserRegistrationDTO userDto)
        {
        
            // UserDTO user = userDto;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
               
               
                var users = td.tblUsers.ToList().Find(x=>x.Username == userDto.Username);
                if (users != null)
                {
                    return BadRequest("Username Already Exist!");
                }
                else
                {
                    tblUser user = new tblUser();
                    user.Password = userDto.Password;
                    user.Username = userDto.Username;
                    var result = td.tblUsers.Add(user);
                    var res = td.SaveChanges();

                    return Ok("Registration Successful");
                }
              
            }
               
           // AuthenticationModule auth = new AuthenticationModule();
           // var token = auth.GenerateTokenForUser(userDto.Username, userDto.UserId);
           
        }
    }
}
