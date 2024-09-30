
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;

namespace MagazziniMaterialiAPI.Repositories
{
    public class MaterialeMagazzinoRepository : BaseRepository, IMaterialeMagazzinoRepository
    {
        private ApplicationDbContext _ApplicationDbContext;
        public MaterialeMagazzinoRepository(ApplicationDbContext ApplicationDbContext) : base(ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }

        /// <summary>
        /// get MaterialeMagazzino by Magazzino id and Materiale id
        /// </summary>
        /// <param name="codiceMateriale"></param>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>
        public MaterialeMagazzino? GetMaterialeMagazzino(string codiceMateriale, int MagazzinoId)
        {
            return _ApplicationDbContext.MaterialeMagazzini.FirstOrDefault(x => x.CodiceMateriale == codiceMateriale && x.MagazzinoID == MagazzinoId);
        }


        /// <summary>
        /// delete MaterialeMagazzino
        /// </summary>
        /// <param name="MaterialeMagazzino"></param>
        public void DeleteMaterialeMagazzino(MaterialeMagazzino MaterialeMagazzino)
        {
            _ApplicationDbContext.Remove(MaterialeMagazzino);
        }


        /// <summary>
        /// add MaterialeMagazzino to database
        /// </summary>
        /// <param name="MaterialeMagazzino"></param>
        /// <returns></returns>
        public MaterialeMagazzino AddMaterialeMagazzino(MaterialeMagazzino MaterialeMagazzino)
        {
            EntityEntry<MaterialeMagazzino> x = _ApplicationDbContext.MaterialeMagazzini.Add(MaterialeMagazzino);
            return x.Entity;

        }
    }
}
