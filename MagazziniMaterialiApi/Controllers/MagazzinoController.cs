using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagazziniMaterialiAPI.Data;
using MagazziniMaterialiAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MagazziniMaterialiAPI.Models.Entity.DTOs;
using MagazziniMaterialiAPI.Services;

namespace MagazziniMaterialiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MagazzinoController : Controller
    {
        private readonly IMagazziniService _MagazziniService;
        private readonly IMaterialeMagazziniService _MaterialeMagazziniService;
        private readonly IMagazzinoMapper _MagazzinoMapper;

        public MagazzinoController(IMagazziniService Magazziniervice, IMaterialeMagazziniService MaterialeMagazziniervice, IMagazzinoMapper MagazzinoMapper)
        {
            _MaterialeMagazziniService = MaterialeMagazziniervice;
            _MagazziniService = Magazziniervice;
            _MagazzinoMapper = MagazzinoMapper;
        }

        /// <summary>
        /// get Magazzino list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetMagazzini()
        {
            List<MagazzinoDTO> result = _MagazziniService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// get Magazzino by id
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>
        [HttpGet("{MagazzinoId}")]
        public ActionResult GetMagazzinoById([FromRoute] int MagazzinoId)
        {
            MagazzinoDTO? Magazzino = _MagazziniService.GetById(MagazzinoId);
            return Magazzino == null ? NotFound() : Ok(Magazzino);
        }

        /// <summary>to Db
        /// add Magazzino 
        /// </summary>
        /// <param name="MagazzinoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMagazzino([FromBody] MagazzinoDTO MagazzinoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please Add Magazzino");
            }
            else
            {
                try
                {
                    Magazzino AddMagazzino = _MagazziniService.AddMagazzino(MagazzinoDTO);
                    _MagazziniService.SaveChanges();
                    MagazzinoDTO MagazzinoDTOResult = _MagazzinoMapper.MapToMagazzinoDTO(AddMagazzino);
                    return Ok(MagazzinoDTOResult);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
        }

        /// <summary>
        /// edit Magazzino data in db
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <param name="MagazzinoDTO"></param>
        /// <returns></returns>
        [HttpPut("{MagazzinoId}")]
        public ActionResult EditMagazzino([FromRoute] int MagazzinoId, [FromBody] MagazzinoDTO MagazzinoDTO)
        {

            bool isEdited = _MagazziniService.EditMagazzino(MagazzinoId, MagazzinoDTO);
            if (!isEdited)
            {
                return NotFound("Magazzino Not Found!!!!!!");
            }

            _MagazziniService.SaveChanges();
            return Ok(MagazzinoDTO);
        }

        /// <summary>
        /// delete Magazzino from db
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>

        [HttpDelete("{MagazzinoId}")]
        public ActionResult DeleteMagazzino([FromRoute] int MagazzinoId)
        {
            bool isDeleted = _MagazziniService.DeleteMagazzino(MagazzinoId);
            if (!isDeleted)
            {
                return NotFound("Magazzino Not Found!!!!!!");
            }
            _MagazziniService.SaveChanges();
            return Ok("Magazzino Deleted Successfully");
        }

        [HttpPost("registerMaterialeMagazzino/{MagazzinoId}/{codiceMateriale}")]
        public ActionResult RegisterMaterialeMagazzino([FromRoute] int MagazzinoId, [FromRoute] string codiceMateriale)
        {

            MaterialeMagazzino MaterialeMagazzino = _MaterialeMagazziniService.AddMaterialeMagazzino(MagazzinoId, codiceMateriale);
            _MaterialeMagazziniService.SaveChanges();
            return Ok("Materiale Registered to Magazzino Successfully");
        }

        [HttpDelete("registerMaterialeMagazzino/{MagazzinoId}/{codiceMateriale}")]
        public ActionResult DeleteMaterialeMagazzino([FromRoute] int MagazzinoId, [FromRoute] string codiceMateriale)
        {

            MaterialeMagazzino MaterialeMagazzino = _MaterialeMagazziniService.AddMaterialeMagazzino(MagazzinoId, codiceMateriale);
            _MaterialeMagazziniService.SaveChanges();
            return Ok("Materiale Deleted from Magazzino Successfully");
        }

        /// <summary>
        /// get all Materiali in a Magazzino
        /// </summary>
        /// <param name="MagazzinoId"></param>
        /// <returns></returns>
        [HttpGet("MaterialiByMagazzino/{MagazzinoId}")]
        public ActionResult MaterialiByMagazzinoId([FromRoute] int MagazzinoId)
        {
            List<MaterialeDTO> Materiali = _MagazziniService.GetMaterialiByMagazzinoId(MagazzinoId);
            return Ok(Materiali);
        }
    }
}
