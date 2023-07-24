using AngularApi.DataBase.Entity;

namespace AngularApi.Interface
{
    public interface IGeneraServes
    {
        Task<IEnumerable<Genera>> GetAll();
        Task<Genera> Post(Genera genera);
        Task<Genera> GetById(int id);
        Genera Delet(Genera genera);
        Genera Update(Genera genera);
    }
}
