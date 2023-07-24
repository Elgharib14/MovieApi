using AngularApi.DataBase;
using AngularApi.DataBase.Entity;
using AngularApi.Interface;
using AngularApi.Migrations;
using Microsoft.EntityFrameworkCore;

namespace AngularApi.Reposatory
{

    public class TvService : ITV
    {
        private readonly AppDbContext context;

        public TvService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Tv> AddMove(Tv tv)
        {
            await context.tvs.AddAsync(tv);
            context.SaveChanges();
            return tv;
        }

        public Tv Delet(Tv tv)
        {
           context.tvs.Remove(tv);
            context.SaveChanges();
            return tv;
        }

        public async Task<IEnumerable<Tv>> getall()
        {
           return await context.tvs.ToListAsync();

        }

        public async Task<Tv> GetById(int id)
        {
            return await context.tvs.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Tv>> Search(string name)
        {
            IQueryable<Tv> querabl = context.tvs;
            if (!string.IsNullOrEmpty(name))
            {
                querabl = querabl.Where(x => x.Name!.Contains(name));
            }

            return await querabl.ToListAsync();
        }

        public Tv Update(Tv tv)
        {
            context.Update(tv);
            context.SaveChanges();
            return tv;
        }
    }
}
