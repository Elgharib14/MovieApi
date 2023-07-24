using AngularApi.DataBase;
using AngularApi.DataBase.Entity;
using AngularApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace AngularApi.Reposatory
{
    public class MovieServes : IMovieServes
    {
        private readonly AppDbContext context;

        public MovieServes(AppDbContext context)
        {
            this.context = context;
        }
        public  async Task<Movie> AddMove(Movie movie)
        {
          await context.movies.AddAsync(movie);
            context.SaveChanges();
            return movie;   
        }

        public Movie Delet(Movie movie)
        {
            context.Remove(movie);
            context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> getall()
        {
            return await context.movies.Include(x=>x.genera).ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await context.movies.Include(x=>x.genera).SingleOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IEnumerable<Movie>> Search(string name)
        {
            IQueryable<Movie> querabl = context.movies;
            if (!string.IsNullOrEmpty(name))
            {
                querabl = querabl.Where(x=>x.Name!.Contains(name) );
            }

            return await querabl.ToListAsync();
        }

        public Movie Update(Movie movie)
        {
            context.Update(movie);
            context.SaveChanges();
            return movie;
        }
    }
}
