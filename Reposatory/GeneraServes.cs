using AngularApi.DataBase;
using AngularApi.DataBase.Entity;
using AngularApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace AngularApi.Reposatory
{
    public class GeneraServes : IGeneraServes
    {
        private readonly AppDbContext dbContext;

        public GeneraServes(AppDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public Genera Delet(Genera genera)
        {
            dbContext.Remove(genera);
            dbContext.SaveChanges();
            return genera;
        }

        public async Task<IEnumerable<Genera>> GetAll()
        {
            return await dbContext.generas.ToListAsync();
        }

        public async Task<Genera> GetById(int id)
        {
            return await dbContext.generas.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Genera> Post(Genera genera)
        {
            await dbContext.AddAsync(genera);
            dbContext.SaveChanges();
            return genera;
        }

        public Genera Update(Genera genera)
        {
            dbContext.Update(genera);
            dbContext.SaveChanges();
            return genera;
        }
    }
}
