using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.ViewModels;
using Vnap.WebApp.Models;

namespace Vnap.WebApp.Controllers.API
{
    [RoutePrefix("api/Plant")]
    public class PlantController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IPlantRepository _plantRepository;

        public PlantController(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        // GET: api/Plant
        public async Task<IEnumerable<PlantVM>> GetPlants(int skip = 0, int take = 10)
        {
            IEnumerable<Plant> pageData = await _plantRepository.GetAllAsync();

            return Mapper.Map<IEnumerable<PlantVM>>(pageData);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<PlantVM> Get(int id)
        {
            var plant = await _plantRepository.GetSingleReadOnlyAsync(id);

            return Mapper.Map<PlantVM>(plant);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IEnumerable<PlantVM>> Search(string query)
        {
            var plants = await _plantRepository.FindByAsync(p => p.Name.Contains(query));

            return Mapper.Map<IEnumerable<PlantVM>>(plants);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<PlantVM> Add(PlantVM plantVm)
        {
            var plant = Mapper.Map<Plant>(plantVm);
            _plantRepository.Add(plant);
            await _plantRepository.CommitAsync();

            return plantVm;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<PlantVM> Update(PlantVM plantVm)
        {
            var plant = Mapper.Map<Plant>(plantVm);
            _plantRepository.Update(plant);
            await _plantRepository.CommitAsync();

            return plantVm;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<PlantVM> Delete(PlantVM plantVm)
        {
            await _plantRepository.DeleteByIdAsync(plantVm.Id);
            await _plantRepository.CommitAsync();

            return plantVm;
        }
    }
}