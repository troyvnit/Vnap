using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.ViewModels;
using Vnap.WebApp.Models;

namespace Vnap.Web.Controllers.API
{
    [RoutePrefix("api/PlantDisease")]
    public class PlantDiseaseController : ApiController
    {
        private readonly IPlantDiseaseRepository _plantDiseaseRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly IImageRepository _imageRepository;


        public PlantDiseaseController(IPlantDiseaseRepository plantDiseaseRepository, IPlantRepository plantRepository, IImageRepository imageRepository)
        {
            _plantDiseaseRepository = plantDiseaseRepository;
            _plantRepository = plantRepository;
            _imageRepository = imageRepository;
        }
        
        public async Task<IEnumerable<PlantDiseaseVM>> GetPlantDiseases(int skip = 0, int take = 10)
        {
            IEnumerable<PlantDisease> pageData = await _plantDiseaseRepository.AllIncludingAsync(pd => pd.Plant, pd => pd.Images, pd => pd.Solutions);

            return Mapper.Map<IEnumerable<PlantDiseaseVM>>(pageData);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<PlantDiseaseVM> Get(int id)
        {
            var plantDiseases = await _plantDiseaseRepository.AllIncludingAsync(pd => pd.Plant, pd => pd.Images, pd => pd.Solutions);

            return Mapper.Map<PlantDiseaseVM>(plantDiseases.FirstOrDefault(pd => pd.Id == id));
        }

        [HttpGet]
        [Route("GetByPlantIds")]
        public IEnumerable<PlantDiseaseVM> GetByPlantIds(string plantIds)
        {
            IEnumerable<PlantDisease> pageData = _plantDiseaseRepository.Queryable().Where(pd => string.IsNullOrEmpty(plantIds) || plantIds.Contains(pd.PlantId.ToString())).ToList();

            return Mapper.Map<IEnumerable<PlantDiseaseVM>>(pageData);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IEnumerable<PlantDiseaseVM>> Search(string query)
        {
            var plantDiseases = await _plantDiseaseRepository.FindByAsync(p => p.Name.Contains(query));

            return Mapper.Map<IEnumerable<PlantDiseaseVM>>(plantDiseases);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<PlantDiseaseVM> Add(PlantDiseaseVM plantDiseaseVm)
        {
            var plantDisease = Mapper.Map<PlantDisease>(plantDiseaseVm);
            _plantDiseaseRepository.Add(plantDisease);
            await _plantDiseaseRepository.CommitAsync();

            return plantDiseaseVm;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<PlantDiseaseVM> Update(PlantDiseaseVM plantDiseaseVm)
        {
            var plantDisease = Mapper.Map<PlantDisease>(plantDiseaseVm);
            _plantDiseaseRepository.Update(plantDisease);
            await _plantDiseaseRepository.CommitAsync();

            return plantDiseaseVm;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<PlantDiseaseVM> Delete(PlantDiseaseVM plantDiseaseVm)
        {
            await _plantDiseaseRepository.DeleteByIdAsync(plantDiseaseVm.Id);
            await _plantDiseaseRepository.CommitAsync();

            return plantDiseaseVm;
        }

        [HttpPost]
        [Route("AddImage")]
        public async Task<ImageVM> AddImage(ImageVM imageVm)
        {
            var image = Mapper.Map<Image>(imageVm);
            _imageRepository.Add(image);
            await _imageRepository.CommitAsync();
            _imageRepository.Detach(image);

            return Mapper.Map<ImageVM>(image);
        }

        [HttpPost]
        [Route("UpdateImage")]
        public async Task<ImageVM> UpdateImage(ImageVM imageVm)
        {
            var image = Mapper.Map<Image>(imageVm);
            _imageRepository.Update(image);
            await _imageRepository.CommitAsync();
            _imageRepository.Detach(image);

            return imageVm;
        }

        [HttpPost]
        [Route("DeleteImage")]
        public async Task<ImageVM> DeleteImage(ImageVM imageVm)
        {
            await _imageRepository.DeleteByIdAsync(imageVm.Id);
            await _imageRepository.CommitAsync();

            return imageVm;
        }
    }
}