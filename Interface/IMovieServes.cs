using AngularApi.DataBase.Entity;

namespace AngularApi.Interface
{
   public interface IMovieServes
    {
        Task<IEnumerable<Movie>> getall();
        Task<IEnumerable<Movie>> Search(string name);
        Task<Movie>AddMove(Movie movie);
        Task<Movie> GetById(int id);
        Movie Delet (Movie movie);
        Movie Update (Movie movie);
    }
}
