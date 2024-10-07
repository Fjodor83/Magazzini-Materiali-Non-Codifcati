using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using System.Collections.Generic;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMovimentazioneRepository
    {
        MovimentazioneDTO GetById(int id);
        IEnumerable<MovimentazioneDTO> GetAllAsync();
        IEnumerable<MovimentazioneDTO> GetByMaterialeId(string codiceMateriale);
        void Add(MovimentazioneDTO movimentazioneDTO);
        void Delete(int id);
        void Update(MovimentazioneDTO movimentazione);
        bool EsisteMovimentazioneSuccessiva(string codiceMateriale, DateTime dataMovimentazione);
    }
}
