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
    public class PlantDiseaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlantDiseaseRepository _plantDiseaseRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly IImageRepository _imageRepository;


        public PlantDiseaseController(UserManager<ApplicationUser> userManager, IPlantDiseaseRepository plantDiseaseRepository, IPlantRepository plantRepository, IImageRepository imageRepository)
        {
            _userManager = userManager;
            _plantDiseaseRepository = plantDiseaseRepository;
            _plantRepository = plantRepository;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List(int skip = 0, int take = 10)
        {
            IEnumerable<PlantDisease> pageData = await _plantDiseaseRepository.AllIncludingAsync(pd => pd.Plant);

            return Json(Mapper.Map<IEnumerable<PlantDiseaseVM>>(pageData));
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var plantDiseases = await _plantDiseaseRepository.AllIncludingAsync(pd => pd.Plant, pd => pd.Images);

            return Json(Mapper.Map<PlantDiseaseVM>(plantDiseases.FirstOrDefault(pd => pd.Id == id)));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(PlantDiseaseVM plantDiseaseVm)
        {
            var plantDisease = Mapper.Map<PlantDisease>(plantDiseaseVm);
            plantDisease.Plant = await _plantRepository.GetSingleReadOnlyAsync(plantDisease.PlantId);
            _plantDiseaseRepository.Add(plantDisease);
            await _plantDiseaseRepository.CommitAsync();

            return Json(plantDiseaseVm);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(PlantDiseaseVM plantDiseaseVm)
        {
            var plantDisease = Mapper.Map<PlantDisease>(plantDiseaseVm);
            plantDisease.Plant = await _plantRepository.GetSingleReadOnlyAsync(plantDisease.PlantId);
            _plantDiseaseRepository.Update(plantDisease);
            await _plantDiseaseRepository.CommitAsync();

            return Json(plantDiseaseVm);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(PlantDiseaseVM plantDiseaseVm)
        {
            await _plantDiseaseRepository.DeleteByIdAsync(plantDiseaseVm.Id);
            await _plantDiseaseRepository.CommitAsync();

            return Json(plantDiseaseVm);
        }

        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImage(int plantDiseaseId, string imageUrl)
        {
            var plantDiseases = await _plantDiseaseRepository.AllIncludingAsync(pd => pd.Id == plantDiseaseId);
            var plantDisease = plantDiseases.FirstOrDefault();
            if (plantDisease != null)
            {
                var image = new Image()
                {
                    Url = imageUrl
                };
                plantDisease.Images.Add(image);
                await _plantDiseaseRepository.CommitAsync();
                return Json(image);
            }
            return Json("");
        }
    }
}