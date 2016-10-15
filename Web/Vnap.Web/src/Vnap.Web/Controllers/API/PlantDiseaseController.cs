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
            _plantDiseaseRepository.Add(plantDisease);
            await _plantDiseaseRepository.CommitAsync();

            return Json(plantDiseaseVm);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(PlantDiseaseVM plantDiseaseVm)
        {
            var plantDisease = Mapper.Map<PlantDisease>(plantDiseaseVm);
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
            var image = new Image()
            {
                Url = imageUrl,
                PlantDiseaseId = plantDiseaseId
            };
            _imageRepository.Add(image);
            await _imageRepository.CommitAsync();
            _imageRepository.Detach(image);

            return Json(Mapper.Map<ImageVM>(image));
        }

        [HttpPost("UpdateImage")]
        public async Task<IActionResult> UpdateImage(ImageVM imageVm)
        {
            var image = Mapper.Map<Image>(imageVm);
            _imageRepository.Update(image);
            await _imageRepository.CommitAsync();
            _imageRepository.Detach(image);

            return Json(imageVm);
        }

        [HttpPost("DeleteImage")]
        public async Task<IActionResult> DeleteImage(ImageVM imageVm)
        {
            await _imageRepository.DeleteByIdAsync(imageVm.Id);
            await _imageRepository.CommitAsync();

            return Json(imageVm);
        }
    }
}