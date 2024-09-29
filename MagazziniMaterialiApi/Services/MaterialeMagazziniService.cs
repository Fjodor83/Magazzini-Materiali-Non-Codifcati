using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Repositories;

namespace MagazziniMaterialiAPI.Services
{
    public class MaterialeMagazziniService : IMaterialeMagazziniService
    {
        private readonly IMaterialeMagazzinoRepository _MaterialeMaterialeMagazzinoRepository;
        public MaterialeMagazziniService(IMaterialeMagazzinoRepository MaterialeMaterialeMagazzinoRepository)
        {
            _MaterialeMaterialeMagazzinoRepository = MaterialeMaterialeMagazzinoRepository;
        }

        /// <summary>
        /// add MaterialeMaterialeMagazzino to db
        /// </summary>
        /// <param name="MaterialeMaterialeMagazzino"></param>
        /// <returns></returns>
        public MaterialeMagazzino AddMaterialeMagazzino(int MagazzinoId, string codiceMateriale)
        {
            MaterialeMagazzino MaterialeMaterialeMagazzinoEntity = new MaterialeMagazzino()
            {
                MagazzinoID = MagazzinoId,
                CodiceMateriale = codiceMateriale
            };
            return _MaterialeMaterialeMagazzinoRepository.AddMaterialeMagazzino(MaterialeMaterialeMagazzinoEntity);
        }

        /// <summary>
        /// delete MaterialeMaterialeMagazzino 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMaterialeMagazzino(int MagazzinoId, string codiceMateriale)
        {
            MaterialeMagazzino? MaterialeMaterialeMagazzino = _MaterialeMaterialeMagazzinoRepository.GetMaterialeMagazzino(codiceMateriale, MagazzinoId); ;
            if (MaterialeMaterialeMagazzino == null)
            {
                return false;
            }
            _MaterialeMaterialeMagazzinoRepository.DeleteMaterialeMagazzino(MaterialeMaterialeMagazzino);
            return true;

        }

        /// <summary>
        /// save changes to DB
        /// </summary>
        public void SaveChanges()
        {
            _MaterialeMaterialeMagazzinoRepository.SaveChanges();
        }

    }
}
