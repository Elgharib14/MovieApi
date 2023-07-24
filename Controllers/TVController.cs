using AngularApi.DataBase.Entity;
using AngularApi.Interface;
using AngularApi.Modell;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVController : ControllerBase
    {
        private readonly ITV tv;

        public TVController(ITV tv)
        {
            this.tv = tv;
        }

        [HttpGet("GetAllMovie")]
        public async Task<IActionResult> GetAllMovie(int page = 1, int pagesize = 10)
        {
            var data = await tv.getall();
            var totalitem = data.Count();
            var pagedItems = data.Skip((page - 1) * pagesize).Take(pagesize);
            var result = new
            {
                TotalItems = totalitem,
                Page = page,
                PageSize = pagesize,
                Items = pagedItems
            };
            return Ok(result);
        }

        [HttpPost("AddTV")]
        public async Task<IActionResult> AddMovie([FromForm] TVmodell movie)
        {
            // 1=> Get Directory
            string Imgpath = Directory.GetCurrentDirectory() + "/wwwroot/Poster";
            // 2=> Get FileName 
            string ImgName = Guid.NewGuid() + Path.GetFileName(movie.Poster!.FileName);
            // 3=> Merg path with file name
            string finalImgpath = Path.Combine(Imgpath, ImgName);
            // 4=> Save File as streams
            using (var stream = new FileStream(finalImgpath, FileMode.Create))
            {
                movie.Poster.CopyTo(stream);
            }
            var data = new Tv
            {
                Name = movie.Name,
                Poster = ImgName,
                Rate = movie.Rate,
                Title = movie.Title,
                Year = movie.Year,
            };

            await tv.AddMove(data);
            return Ok(data);
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetByID(int id)
        {
            var data = await tv.GetById(id);
            return Ok(data);
        }


        [HttpDelete("DeletTV")]
        public async Task<IActionResult> DeletMovie(int id)
        {
            var data = await tv.GetById(id);
            if (data == null)
                return BadRequest($"con't find any data int id {id}");


            tv.Delet(data);
            return Ok(data);
        }

    }
}
