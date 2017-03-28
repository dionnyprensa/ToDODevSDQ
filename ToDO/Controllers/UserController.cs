using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDO.Application.Concrete;
using ToDO.Application.Interfaces;
using ToDO.Domain.Entities;

namespace ToDO.Controllers
{
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserServices();
        }

        // GET api/user
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var users = _userService.GetAllUsers();

            if (users == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuarios no encontrados");

            var userEntities = users as List<User> ?? users.ToList();

            return userEntities.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, userEntities)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuarios no encontrados");
        }

        // GET api/user/5
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var user = _userService.GetUserById(id);

            return user != null
                ? Request.CreateResponse(HttpStatusCode.OK, user)
                : Request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
        }

        // POST api/user
        [HttpPost]
        public int Post([FromBody] User m)
        {
            return _userService.IsValid(m) ? _userService.CreateUser(m) : 0;
        }

        // PUT api/user/5
        [HttpPut]
        public bool Put(int id, [FromBody] User m)
        {
            return id > 0 && _userService.UpdateUser(id, m);
        }

        // DELETE api/user/5
        [HttpDelete]
        public bool Delete(int id)
        {
            return id > 0 && _userService.DeleteUser(id);
        }
    }
}