﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Entity.Enums;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.ViewModels;
using Vnap.WebApp.ViewModels;

namespace Vnap.WebApp.Controllers.API
{
    public class SyncController : ApiController
    {
        private IPlantRepository _plantRepository { get; set; }
        private IPlantDiseaseRepository _plantDiseaseRepository { get; set; }
        private IImageRepository _imageRepository { get; set; }
        private ISolutionRepository _solutionRepository { get; set; }
        private IArticleRepository _articleRepository { get; set; }

        public SyncController(IPlantRepository plantRepository, IPlantDiseaseRepository plantDiseaseRepository, IImageRepository imageRepository, ISolutionRepository solutionRepository, IArticleRepository articleRepository)
        {
            _plantRepository = plantRepository;
            _plantDiseaseRepository = plantDiseaseRepository;
            _imageRepository = imageRepository;
            _solutionRepository = solutionRepository;
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<SyncResponseVM> Sync()
        {
            var plants = await _plantRepository.GetAllAsync();
            var plantDiseases = await _plantDiseaseRepository.AllIncludingAsync(pd => pd.Plant, pd => pd.Images, pd => pd.Solutions);
            var introduction = await _articleRepository.Queryable().Where(a => a.ArticleType == ArticleType.Introduction).FirstOrDefaultAsync();

            return new SyncResponseVM()
            {
                Plants = Mapper.Map<IEnumerable<PlantVM>>(plants),
                PlantDiseases = Mapper.Map<IEnumerable<PlantDiseaseVM>>(plantDiseases),
                Introduction = Mapper.Map<ArticleVM>(introduction)
            };
        }
    }
}
