using System.ComponentModel.DataAnnotations;
using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ControllerLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JunController : Controller
    {
        private readonly IAdRepository adRepository;
        private readonly IAdTagRepository adTagRepository;
        private readonly IAdTypeRepository adTypeRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IContentRepository contentRepository;
        private readonly ITagRepository tagRepository;

        public JunController(IAdRepository adRepository, IAdTagRepository adTagRepository, IAdTypeRepository adTypeRepository, ICategoryRepository categoryRepository, IContentRepository contentRepository, ITagRepository tagRepository)
        {
            this.adRepository = adRepository;
            this.adTagRepository = adTagRepository;
            this.adTypeRepository = adTypeRepository;
            this.categoryRepository = categoryRepository;
            this.contentRepository = contentRepository;
            this.tagRepository = tagRepository;
        }

        [HttpPost]
        [Route("CreateAd")]
        public ActionResult<AdDTO> CreateAd(AdDTO adDTO)
        {
            Ad ad = new Ad();

            var type = adTypeRepository.getByName(adDTO.Adtype);
            if (type == null)
                return BadRequest("Invalid data");
            ad.AdTypeId = type.Id;

            var category = categoryRepository.getByName(adDTO.Category);
            if (category == null)
                return BadRequest("Invalid data");
            ad.CategoryId = category.Id;

            ad.Cost = adDTO.Cost;

            Content content = new Content();
            content.Link = adDTO.ContentLink;
            content.Structure = adDTO.ContentStructure;
            var contentRes = contentRepository.create(content);
            ad.ContentId = contentRes.Id;

            string[] newtags = adDTO.Tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var tags = tagRepository.GetAll();
            var tagsnames = from t in tags select t.Name;
            var result = tagsnames.Except(newtags);
            foreach (var item in result)
            {
                Tag tag = new Tag();
                tag.Name = item;
                var restag = tagRepository.create(tag);
                AdTag adTag = new AdTag();
                var resadtag = adTagRepository.create(adTag);
                adTag.Tag_Id = resadtag.Id;
            }

            var res = adRepository.create(ad);
            var adtag = adTagRepository.GetAll();
            foreach (var item in adtag)
            {
                item.Ad_Id = res.Id;
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<AdDTO, Ad>()
                .ForMember("AdType", opt => opt.MapFrom(c => c.Adtype))
                .ForMember("Category", opt => opt.MapFrom(c => c.Category))
                .ForMember("Tag", opt => opt.MapFrom(c => c.Tags))
                .ForMember("Content", opt => opt.MapFrom(c => c.ContentLink + "/n" + c.ContentStructure)));
            var mapper = new Mapper(config);
            res = mapper.Map<AdDTO, Ad>(adDTO);   
                
            return Ok(res);


        }
    }
}
