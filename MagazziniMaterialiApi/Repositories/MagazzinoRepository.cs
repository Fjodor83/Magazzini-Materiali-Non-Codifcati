using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MagazziniMaterialiAPI.Repositories;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public class MagazzinoRepository(ApplicationDbContext ApplicationDbContext) : BaseRepository(ApplicationDbContext), IMagazzinoRepository
    {
        private readonly ApplicationDbContext _ApplicationDbContext = ApplicationDbContext;

        /// <summary>
        /// get all Magazzini
        /// </summary>
        /// <returns></returns>
        public List<Magazzino> GetAll()
        {
            return _ApplicationDbContext.Magazzini.ToList();
        }

        /// <summary>
        /// get Magazzino by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Magazzino? GetById(int id)
        {
            return _ApplicationDbContext.Magazzini.FirstOrDefault(g => g.Id == id);
        }
        /// <summary>
        /// delete Magazzino
        /// </summary>
        /// <param name="Magazzino"></param>
        public void DeleteMagazzino(Magazzino Magazzino)
        {
            _ApplicationDbContext.Remove(Magazzino);
        }

        /// <summary>
        /// edit Magazzino data
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <param name="Magazzino"></param>
        public bool EditMagazzino(int MagazzinoId, Magazzino Magazzino)
        {
            Magazzino? existingEntity = _ApplicationDbContext.Magazzini.Find(MagazzinoId);
            if (existingEntity == null)
            {
                return false;
            }
            else
            {
                _ApplicationDbContext.Entry(existingEntity).State = EntityState.Detached;
            }
            _ApplicationDbContext.Attach(Magazzino);
            _ApplicationDbContext.Entry(Magazzino).State = EntityState.Modified;
            return true;
        }


        /// <summary>
        /// add Magazzino to database
        /// </summary>
        /// <param name="Magazzino"></param>
        /// <returns></returns>
        public Magazzino AddMagazzino(Magazzino Magazzino)
        {
            EntityEntry<Magazzino> x = _ApplicationDbContext.Magazzini.Add(Magazzino);
            return x.Entity;

        }
        /// <summary>
        /// get all Materiali in a Magazzino
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>
        public List<Materiale> GetMaterialiByMagazzinoId(int MagazzinoId)
        {
            List<Materiale> Materiali = new List<Materiale>();
            Magazzino? Magazzino = _ApplicationDbContext.Magazzini
                .Include(x => x.MaterialeMagazzini)
                .ThenInclude(x => x.Materiale).FirstOrDefault(x => x.Id == MagazzinoId);
            if (Magazzino != null)
            {
                Materiali = Magazzino.MaterialeMagazzini.Select(x => x.Materiale).ToList();
            }
            return Materiali;
        }
    }
}
