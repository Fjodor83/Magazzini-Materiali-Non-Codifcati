using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Repositories;


namespace MagazziniMaterialiAPI.Services
{
    public class MagazziniService(IMagazzinoRepository MagazzinoRepository, IMagazzinoMapper MagazzinoMapper, IMaterialeMapper MaterialeMapper) : IMagazziniService
    {
        private readonly IMagazzinoRepository _MagazzinoRepository = MagazzinoRepository;
        private readonly IMagazzinoMapper _MagazzinoMapper = MagazzinoMapper;
        private readonly IMaterialeMapper _MaterialeMapper = MaterialeMapper;

        /// <summary>
        /// get list of Magazzini DTO
        /// </summary>
        /// <returns></returns>
        public List<MagazzinoDTO> GetAll()
        {
            List<MagazzinoDTO> Magazzini = [];
            Magazzini = _MagazzinoRepository.GetAll().Select(g => _MagazzinoMapper.MapToMagazzinoDTO(g)).ToList();
            return Magazzini;
        }

        /// <summary>
        /// get MagazzinoDTO by Magazzino Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MagazzinoDTO? GetById(int id)
        {
            Magazzino? Magazzino = _MagazzinoRepository.GetById(id);
            if (Magazzino == null)
            {
                return null;
            }
            MagazzinoDTO MagazzinoDTO = _MagazzinoMapper.MapToMagazzinoDTO(Magazzino);
            return MagazzinoDTO;
        }

        /// <summary>
        /// add Magazzino to db
        /// </summary>
        /// <param name="Magazzino"></param>
        /// <returns></returns>
        public Magazzino AddMagazzino(MagazzinoDTO Magazzino)
        {
            Magazzino MagazzinoEntity = _MagazzinoMapper.MapToMagazzino(Magazzino);
            return _MagazzinoRepository.AddMagazzino(MagazzinoEntity);
        }

        /// <summary>
        /// get all Materiali in a Magazzino
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>
        public List<MaterialeDTO> GetMaterialiByMagazzinoId(int MagazzinoId)
        {
            {
                List<MaterialeDTO> Materiali = new List<MaterialeDTO>();
                Materiali = _MagazzinoRepository.GetMaterialiByMagazzinoId(MagazzinoId).Select(g => _MaterialeMapper.MapToMaterialeDTO(g)).ToList();
                return Materiali;
            }
        }


        /// <summary>
        /// delete Magazzino 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMagazzino(int id)
        {
            Magazzino? Magazzino = _MagazzinoRepository.GetById(id); ;
            if (Magazzino == null)
            {
                return false;
            }
            _MagazzinoRepository.DeleteMagazzino(Magazzino);
            return true;

        }

        /// <summary>
        /// edit Magazzino data 
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <param name="MagazzinoDTO"></param>
        /// <returns></returns>
        public bool EditMagazzino(int MagazzinoId, MagazzinoDTO MagazzinoDTO)
        {
            Magazzino MagazzinoEntity = _MagazzinoMapper.MapToMagazzino(MagazzinoDTO);
            return _MagazzinoRepository.EditMagazzino(MagazzinoId, MagazzinoEntity);
        }

        /// <summary>
        /// save changes to DB
        /// </summary>
        public void SaveChanges()
        {
            _MagazzinoRepository.SaveChanges();
        }

    }
}
