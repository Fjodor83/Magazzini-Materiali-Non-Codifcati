using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Repositories;
using Microsoft.Extensions.Logging;

namespace MagazziniMaterialiAPI.Services
{
    public class MaterialiService : IMaterialiService
    {
        private readonly IMaterialeRepository _materialeRepository;
        private readonly IMaterialeMapper _materialeMapper;
        private readonly IMagazzinoMapper _magazzinoMapper;
        private readonly ILogger<MaterialiService> _logger;

        public MaterialiService(IMaterialeRepository materialeRepository, ILogger<MaterialiService> logger, IMaterialeMapper materialeMapper, IMagazzinoMapper magazzinoMapper)
        {
            _materialeRepository = materialeRepository;
            _logger = logger;
            _materialeMapper = materialeMapper;
            _magazzinoMapper = magazzinoMapper;
        }

        public List<MaterialeDTO> GetAll()
        {
            try
            {
                return _materialeRepository.GetAll()
                    .Select(g => _materialeMapper.MapToMaterialeDTO(g))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il recupero di tutti i materiali.");
                throw new InvalidOperationException("Errore durante il recupero dei materiali.", ex);
            }
        }

        public MaterialeDTO? GetByCodiceMateriale(string codiceMateriale)
        {
            var materiale = _materialeRepository.GetByCodiceMateriale(codiceMateriale);
            return materiale != null ? _materialeMapper.MapToMaterialeDTO(materiale) : null;
        }

        public Materiale AddMateriale(MaterialeDTO materialeDTO)
        {
            var materialeEntity = _materialeMapper.MapToMateriale(materialeDTO);
            _materialeRepository.AddMateriale(materialeEntity);
            _materialeRepository.SaveChanges();
            return materialeEntity;
        }

        public bool DeleteMateriale(string codiceMateriale)
        {
            var materiale = _materialeRepository.GetByCodiceMateriale(codiceMateriale);
            if (materiale == null)
            {
                return false;
            }

            _materialeRepository.DeleteMateriale(materiale);
            _materialeRepository.SaveChanges();
            return true;
        }

        public bool EditMateriale(string codiceMateriale, MaterialeDTO materialeDTO)
        {
            var materialeEntity = _materialeMapper.MapToMateriale(materialeDTO);
            if (_materialeRepository.EditMateriale(codiceMateriale, materialeEntity))
            {
                _materialeRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public List<MagazzinoDTO> GetMagazziniByMaterialeId(string codiceMateriale)
        {
            return _materialeRepository.GetMagazziniByMaterialeId(codiceMateriale)
                .Select(m => _magazzinoMapper.MapToMagazzinoDTO(m))
                .ToList();
        }

        public bool ExistsByCodice(string codiceMateriale)
        {
            return _materialeRepository.ExistsByCodice(codiceMateriale);
        }


        public void SaveChanges()
        {
            _materialeRepository.SaveChanges();
        }
    }
}
