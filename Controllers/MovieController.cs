using AngularApi.DataBase.Entity;
using AngularApi.Interface;
using AngularApi.Modell;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AngularApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieServes serves;

        public MovieController(IMovieServes serves)
        {
            this.serves = serves;
        }

        [HttpGet("GetAllMovie")]
        public async Task<IActionResult> GetAllMovie(int page= 1 , int pagesize = 18)
        {
            var data = await serves.getall();
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

        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovie([FromForm]MovieVM movie)
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

            //var file = Request.Form.Files[0];
            //var folderName = Path.Combine("wwwroot", "Poster");
            //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //if (file.Length > 0)
            //{
            //    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //    var fullPath = Path.Combine(pathToSave, fileName);
            //    var dbPath = Path.Combine(folderName, fileName);
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //    {
            //        file.CopyTo(stream);
            //    }



            var data = new Movie
                {
                    GeneraId = movie.generaId,
                    Name = movie.Name,
                    Poster = Imgpath,
                    Rate = movie.Rate,
                    Title = movie.Title,
                    Year = movie.Year,
                };

                await serves.AddMove(data); 
          
            return Ok();
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetByID(int id)
        {
            var data = await serves.GetById(id);
            return Ok(data);    
        }

        [HttpPut("EditMovie")]
        public async Task<IActionResult> EditMovie(int id ,[FromForm] MovieVM movie)
        {

            var data = await serves.GetById(id);

            if (data == null)
                return BadRequest($"con't find any data int id {id}");

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

            data.Title = movie.Title;
            data.Year = movie.Year;
            data.GeneraId=movie.generaId;
            data.Name = movie.Name;
            data.Rate = movie.Rate;
            data.Poster = ImgName;

            serves.Update(data);

            return Ok(data);

        }

        [HttpDelete("DeletMovie")]
        public async Task<IActionResult> DeletMovie(int id)
        {
            var data = await serves.GetById(id);
            if (data == null)
                return BadRequest($"con't find any data int id {id}");


             serves.Delet(data);
            return Ok(data);
        }


        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Movie>>> Search(string name)
        {
            var data = await serves.Search(name);
            if (data.Any())
            {
                return Ok(data);
            }

            return BadRequest();

        }



    }
}
