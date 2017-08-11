using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Entity.Enums;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.ViewModels;
using Vnap.WebApp.Models;

namespace Vnap.WebApp.Controllers.API
{
    [RoutePrefix("api/Setting")]
    public class SettingController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly ISettingRepository _settingRepository;

        public SettingController(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        // GET: api/Setting
        public async Task<IEnumerable<SettingVM>> GetSettings(int skip = 0, int take = 10)
        {
            IEnumerable<Setting> pageData = await _settingRepository.GetAllAsync();

            return Mapper.Map<IEnumerable<SettingVM>>(pageData);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<SettingVM> Get(int id)
        {
            var setting = await _settingRepository.GetSingleReadOnlyAsync(id);

            return Mapper.Map<SettingVM>(setting);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<SettingVM> Add(SettingVM settingVm)
        {
            var setting = Mapper.Map<Setting>(settingVm);
            _settingRepository.Add(setting);
            await _settingRepository.CommitAsync();

            return settingVm;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<SettingVM> Update(SettingVM settingVm)
        {
            var setting = Mapper.Map<Setting>(settingVm);
            _settingRepository.Update(setting);
            await _settingRepository.CommitAsync();

            return settingVm;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<SettingVM> Delete(SettingVM settingVm)
        {
            await _settingRepository.DeleteByIdAsync(settingVm.Id);
            await _settingRepository.CommitAsync();

            return settingVm;
        }
    }
}