using MagazziniMaterialiAPI.Mapper;
using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Repositories;

namespace MagazziniMaterialiAPI.Services
{
    public class GiacenzaService : IGiacenzaService
    {
        private readonly IGiacenzaRepository _giacenzaRepository;
        private readonly IGiacenzaMapper _giacenzaMapper;

        public GiacenzaService(IGiacenzaRepository giacenzaRepository, IGiacenzaMapper giacenzaMapper)
        {
            _giacenzaRepository = giacenzaRepository;
            _giacenzaMapper = giacenzaMapper;
        }

        public async Task<GiacenzaDTO> GetByIdAsync(int id)
        {
            var giacenza = await _giacenzaRepository.GetByIdAsync(id);
            return _giacenzaMapper.MapToMagazzinoDTO(giacenza);
        }

        public async Task<IEnumerable<GiacenzaDTO>> GetAllAsync()
        {
            var giacenze = await _giacenzaRepository.GetAllAsync();
            return giacenze.Select(_giacenzaMapper.MapToMagazzinoDTO);
        }

        public async Task<IEnumerable<GiacenzaDTO>> GetByMagazzinoIdAsync(int magazzinoId)
        {
            var giacenze = await _giacenzaRepository.GetByMagazzinoIdAsync(magazzinoId);
            return giacenze.Select(_giacenzaMapper.MapToMagazzinoDTO);
        }

        public async Task<IEnumerable<GiacenzaDTO>> GetByCodiceMaterialeAsync(string codiceMateriale)
        {
            var giacenze = await _giacenzaRepository.GetByCodiceMaterialeAsync(codiceMateriale);
            return giacenze.Select(_giacenzaMapper.MapToMagazzinoDTO);
        }

        public async Task<GiacenzaDTO> GetByMagazzinoAndMaterialeAsync(int magazzinoId, string codiceMateriale)
        {
            var giacenza = await _giacenzaRepository.GetByMagazzinoAndMaterialeAsync(magazzinoId, codiceMateriale);
            return _giacenzaMapper.MapToMagazzinoDTO(giacenza);
        }

        public async Task AddAsync(GiacenzaDTO giacenzaDto)
        {
            var giacenza = _giacenzaMapper.MapToMagazzino(giacenzaDto);
            await _giacenzaRepository.AddAsync(giacenza);
        }

        public async Task UpdateAsync(GiacenzaDTO giacenzaDto)
        {
            var giacenza = _giacenzaMapper.MapToMagazzino(giacenzaDto);
            await _giacenzaRepository.UpdateAsync(giacenza);
        }

        public async Task DeleteAsync(int id)
        {
            await _giacenzaRepository.DeleteAsync(id);
        }

        public async Task AggiornaQuantitaAsync(int id, int nuovaQuantitaDisponibile, int nuovaQuantitaImpegnata)
        {
            var giacenza = await _giacenzaRepository.GetByIdAsync(id);
            if (giacenza != null)
            {
                giacenza.QuantitaDisponibile = nuovaQuantitaDisponibile;
                giacenza.QuantitaImpegnata = nuovaQuantitaImpegnata;
                await _giacenzaRepository.UpdateAsync(giacenza);
            }
        }
    }
}