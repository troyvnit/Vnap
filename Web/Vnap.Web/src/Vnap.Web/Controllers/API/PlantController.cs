using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.ViewModels;

namespace Vnap.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PlantController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlantRepository _plantRepository;

        public PlantController(UserManager<ApplicationUser> userManager, IPlantRepository plantRepository)
        {
            _userManager = userManager;
            _plantRepository = plantRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List(int skip = 0, int take = 10)
        {
            IEnumerable<Plant> pageData = await _plantRepository.GetAllAsync();

            return Json(Mapper.Map<IEnumerable<PlantVM>>(pageData));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var plant = await _plantRepository.GetSingleReadOnlyAsync(id);
            
            return Json(Mapper.Map<PlantVM>(plant));
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string query)
        {
            var plants = await _plantRepository.FindByAsync(p => p.Name.Contains(query));

            return Json(Mapper.Map<IEnumerable<PlantVM>>(plants));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(PlantVM plantVm)
        {
            var plant = Mapper.Map<Plant>(plantVm);
            _plantRepository.Add(plant);
            await _plantRepository.CommitAsync();

            return Json(plantVm);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(PlantVM plantVm)
        {
            var plant = Mapper.Map<Plant>(plantVm);
            _plantRepository.Update(plant);
            await _plantRepository.CommitAsync();

            return Json(plantVm);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(PlantVM plantVm)
        {
            await _plantRepository.DeleteByIdAsync(plantVm.Id);
            await _plantRepository.CommitAsync();

            return Json(plantVm);
        }
    }
}