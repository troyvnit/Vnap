using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vnap.Entity;
using Vnap.Repository;
using Vnap.Service.Requests.Plant;
using Vnap.Service.Utils;

namespace Vnap.Service
{
    public interface ISyncService
    {
        Task<SyncResult> Sync();
    }
    public class SyncService : ISyncService
    {
        public async Task<SyncResult> Sync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var syncResultString = await httpClient.GetStringAsync("http://vnap.vn/api/sync");
                    var syncResult = JsonConvert.DeserializeObject<SyncResult>(syncResultString);
                    LocalDataStorage.SetPlants(syncResult.Plants);
                    LocalDataStorage.SetPlantDiseases(syncResult.PlantDiseases);
                    return syncResult;
                }
            }
            catch (Exception e)
            {
                return new SyncResult();
            }
        }
    }

    public class SyncResult
    {
        public List<Plant> Plants { get; set; }
        public List<PlantDisease> PlantDiseases { get; set; }
    }
}
