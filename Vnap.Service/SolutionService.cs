using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Solution;
using Vnap.Service.Utils;

namespace Vnap.Service
{
    public interface ISolutionService
    {
        Task<List<Solution>> GetSolutions(GetSolutionsRq rq);
        int GetSolutionsCount();
    }
    public class SolutionService : ISolutionService
    {
        HttpClient httpClient = new HttpClient();
        private IRepository<Solution> _postRepository;
        public SolutionService(IRepository<Solution> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<Solution>> GetSolutions(GetSolutionsRq rq)
        {
            var getSolutionsRs = await httpClient.GetStringAsync($"http://vnap.vn/api/solution?plantDiseaseId={rq.PlantDiseaseId}");
            var solutions = JsonConvert.DeserializeObject<List<Solution>>(getSolutionsRs);
            return solutions;
        }

        public int GetSolutionsCount()
        {
            return 0;
        }
    }
}
