using MagazziniMaterialiAPI.Models.Entity;
using System.Collections.Generic;

namespace MagazziniMaterialiAPI.Repositories
{
    public interface IMovimentazioneRepository
    {
        Movimentazione GetById(int id);
        IEnumerable<Movimentazione> GetAll();
        IEnumerable<Movimentazione> GetByMaterialeId(string codiceMateriale);
        void Add(Movimentazione movimentazione);
        void Delete(int id);
        void Update(Movimentazione movimentazione);
        bool EsisteMovimentazioneSuccessiva(string codiceMateriale, DateTime dataMovimentazione);
    }
}
