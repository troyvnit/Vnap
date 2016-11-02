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
    [RoutePrefix("api/Solution")]
    public class SolutionController : ApiController
    {
        private readonly ISolutionRepository _solutionRepository;

        public SolutionController(ISolutionRepository solutionRepository)
        {
            _solutionRepository = solutionRepository;
        }

        public async Task<IEnumerable<SolutionVM>> GetSolutions(int skip = 0, int take = 10)
        {
            IEnumerable<Solution> pageData = await _solutionRepository.AllIncludingAsync(pd => pd.PlantDisease);

            return Mapper.Map<IEnumerable<SolutionVM>>(pageData);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<SolutionVM> Get(int id)
        {
            var solutions = await _solutionRepository.AllIncludingAsync(pd => pd.PlantDisease);

            return Mapper.Map<SolutionVM>(solutions.FirstOrDefault(pd => pd.Id == id));
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IEnumerable<SolutionVM>> Search(string query)
        {
            var solutions = await _solutionRepository.FindByAsync(p => p.Name.Contains(query));

            return Mapper.Map<IEnumerable<SolutionVM>>(solutions);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<SolutionVM> Add(SolutionVM solutionVm)
        {
            var solution = Mapper.Map<Solution>(solutionVm);
            _solutionRepository.Add(solution);
            await _solutionRepository.CommitAsync();

            return solutionVm;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<SolutionVM> Update(SolutionVM solutionVm)
        {
            var solution = Mapper.Map<Solution>(solutionVm);
            _solutionRepository.Update(solution);
            await _solutionRepository.CommitAsync();

            return solutionVm;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<SolutionVM> Delete(SolutionVM solutionVm)
        {
            await _solutionRepository.DeleteByIdAsync(solutionVm.Id);
            await _solutionRepository.CommitAsync();

            return solutionVm;
        }
    }
}