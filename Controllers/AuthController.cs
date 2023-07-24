using AngularApi.Interface;
using AngularApi.Modell;
using AngularApi.Reposatory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices authServices;

        public AuthController(IAuthServices authServices)
        {
            this.authServices = authServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }

            var result = await authServices.Registration(model);

            if (!result.IsAuth)
            {
                return Ok(new Data { Message = result.Massage});
            }
            var ret = new Data { Message = "Succedd" };
            return Ok(ret);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ModelState);
            }

            var result = await authServices.Login(model);

            if (!result.IsAuth)
            {
                return Ok(new Data { Message = result.Massage });
            }
            var ret = new Data { Message = "Succedd"  , Token =result.Token};
            return Ok(ret);

        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleModel model)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await authServices.AddRole(model);

            if (!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            return Ok(model);

        }


    }
}
