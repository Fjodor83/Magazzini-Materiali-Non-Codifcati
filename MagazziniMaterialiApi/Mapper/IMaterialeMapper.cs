

using MagazziniMaterialiAPI.Models.Entity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;

namespace MagazziniMaterialiAPI
{
    public interface IMaterialeMapper
    {
        public Materiale MapToMateriale(MaterialeDTO MaterialeDTO);
        public MaterialeDTO MapToMaterialeDTO(Materiale Materiale);

    }
}