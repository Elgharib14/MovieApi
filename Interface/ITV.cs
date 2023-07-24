using AngularApi.DataBase.Entity;

namespace AngularApi.Interface
{
    public interface ITV
    {
        Task<IEnumerable<Tv>> getall();
        Task<IEnumerable<Tv>> Search(string name);
        Task<Tv> AddMove(Tv tv);
        Task<Tv> GetById(int id);
        Tv Delet(Tv tv);
        Tv Update(Tv tv);
    }
}
