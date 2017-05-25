using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.ViewModels;
using Vnap.WebApp.Models;

namespace Vnap.WebApp.Controllers.API
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private ApplicationUserManager _userManager;

        public UserController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<UserVM> GetUsers()
        {
            IEnumerable<UserVM> users = _userManager.Users.Select(u => new UserVM()
            {
                Address = u.Address,
                City = u.City,
                UserName = u.UserName,
                Plant = u.Plant
            });

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Create")]
        public async Task<ApplicationUser> Create(UserVM userVm)
        {
            var user = new ApplicationUser()
            {
                UserName = userVm.UserName,
                Address = userVm.Address,
                City = userVm.City,
                Plant = userVm.Plant,
                Email = $"{userVm.UserName}@vnap.vn"
            };
            var existingUser = await _userManager.FindByNameAsync(user.UserName);
            if (existingUser != null)
            {
                existingUser.Address = user.Address;
                existingUser.City = user.City;
                existingUser.Plant = user.Plant;
                var result = await _userManager.UpdateAsync(existingUser);
                if (!result.Succeeded)
                {
                    return null;
                }
            }
            else
            {
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return null;
                }
            }
            return user;
        }
    }
}
