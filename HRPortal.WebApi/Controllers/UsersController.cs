using HRPortal.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using HRPortal.WebApi.Extension;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRPortal.WebApi.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        OnboardingDBContext _dbContext;

        public UsersController(OnboardingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _dbContext.Users;
        }
        // GET: api/<UsersController>
        [HttpPost]
        [Route("signin")]
        public bool Login(Int64 userId, string passwordHash)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.LoginId.Equals(userId));
            if (user == null)
                throw new Exception("Invalid User id");
            else if (!user.PasswordSalt.Equals(passwordHash))
            {
                throw new Exception("Wrong password");
            }
            user.Active = true; 
            _dbContext.SaveChanges();
            return true;
        }
        // GET: api/<UsersController>
        [HttpGet("signout/{userId}")]
        public bool Logout(long userId)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.LoginId.Equals(userId));
            if (user == null)
                throw new Exception("Invalid User id");

            user.Active = false;
            _dbContext.SaveChanges();

            return true;
        }
        // GET: api/<UsersController>
        [HttpGet("IsLoggedIn/{userId}")]
        public bool? IsLoggedIn(long userId)
        {
            if (userId <= 0)
                throw new Exception("Invalid User id");
            bool? status = false;
            var users = _dbContext.Users;
            try
            {
                status = _dbContext.Users.SingleOrDefault(u => u.LoginId.Equals(userId))?.Active;
            }
            catch(Exception e)
            {
                throw e;
            }
            return status;
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("register")]
        public long Post([FromBody] User newUser)
        {
            long retId = 0;
            if (newUser.Id > 0) // Update
            {
                var data = _dbContext.Users.Where(e => e.Id.Equals(newUser.Id)).FirstOrDefault();
                if (data == null)
                {
                    throw new Exception("User not found");
                }
                newUser.CopyProperties(data);
                _dbContext.SaveChanges();
                retId = newUser.Id;
            }
            else                // Create
            {
                var _user = new User();
                newUser.CopyProperties(_user);

                _dbContext.Users.Add(_user);
                _dbContext.SaveChanges();
                retId = _user.Id;
            }
            return retId;
        }
    }
}
