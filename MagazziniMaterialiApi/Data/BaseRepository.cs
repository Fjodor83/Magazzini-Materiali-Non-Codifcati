using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI.Data
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _magazzinoDb;
        public BaseRepository(ApplicationDbContext ApplicationDbContext)
        {
            _magazzinoDb = ApplicationDbContext;
        }

        public void SaveChanges()
        {
            _magazzinoDb.SaveChanges();
        }
    }
}
