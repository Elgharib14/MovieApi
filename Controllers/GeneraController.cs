using AngularApi.DataBase.Entity;
using AngularApi.Interface;
using AngularApi.Modell;
using AngularApi.Reposatory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneraController : ControllerBase
    {
        private readonly IGeneraServes genera;

        public GeneraController(IGeneraServes genera)
        {
            this.genera = genera;
        }


        [HttpGet("GetAllGenera")]
        public async Task<IActionResult> GetAllGenera()
        {
            var data = await genera.GetAll();
            return Ok(data);    
        }

        [HttpPost("AddGenera")]
        public async Task<IActionResult> AddGenera(GeneraVM model)
        {
            if(genera == null) 
            {
                return BadRequest();
            }
            var data = new Genera
            {
                GName = model.GName,
            };
            await genera.Post(data);
            return Ok(data);    
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await genera.GetById(id);
            return Ok(data);
        }

        [HttpPut("EditeGenera")]
        public async Task<IActionResult> EditeGenera(int id , GeneraVM model)
        {
            var data = await genera.GetById(id);
            if(data == null)
            {
                return BadRequest($"Con't find eany data in id {id}");
            }

            data.GName = model.GName;
            genera.Update(data);
            return Ok(data);
        }

        [HttpDelete("DeletGenera")]
        public async Task<IActionResult> DeletGenera(int id)
        {
            var data = await genera.GetById(id);
            if (data == null)
            {
                return BadRequest($"Con't find eany data in id {id}");
            }
            genera.Delet(data);
            return Ok(data);    
        }
    }
}
