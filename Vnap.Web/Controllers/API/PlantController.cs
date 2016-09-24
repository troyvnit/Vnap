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

            return Json(pageData);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var plant = await _plantRepository.GetSingleReadOnlyAsync(id);
            
            return Json(plant);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(PlantVM plantVm)
        {
            var plant = Mapper.Map<Plant>(plantVm);
            plant.CreatedUser = await _userManager.FindByIdAsync("271c4e83-7bd2-4ac4-8535-2f8071394d76");
            _plantRepository.Add(plant);
            await _plantRepository.CommitAsync();

            return Json(plant);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(PlantVM plantVm)
        {
            var plant = Mapper.Map<Plant>(plantVm);
            plant.CreatedUser = await _userManager.FindByIdAsync("271c4e83-7bd2-4ac4-8535-2f8071394d76");
            _plantRepository.Update(plant);
            await _plantRepository.CommitAsync();

            return Json(plant);
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