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
using System.Net;

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
            ad.AdType = type;

            var category = categoryRepository.getByName(adDTO.Category);
            if (category == null)
                return BadRequest("Invalid data");
            ad.CategoryId = category.Id;
            ad.Category = category;

            ad.Cost = adDTO.Cost;

            Content content = new Content();
            #region 3

            try
            {
                WebRequest request = WebRequest.Create(adDTO.ContentLink);
                WebResponse response = request.GetResponse();
                content.Link = adDTO.ContentLink;

            }
            catch
            {
                content.Link = "broken";
                adDTO.ContentLink = "broken";
            }
            #endregion
            content.Structure = adDTO.ContentStructure;
            var contentRes = contentRepository.create(content);
            ad.ContentId = contentRes.Id;
            ad.Content = content;

            string[] newtags;
            if (adDTO.Tags.Length != 0)
            { newtags = adDTO.Tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); }
            else
            {
                string[] tgs = adDTO.ContentStructure.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                newtags = tgs.GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .Take(3).ToArray();
            }
            adDTO.Tags = string.Join(" ", newtags);
            var tags = tagRepository.GetAll();
            var tagsnames = from t in tags select t.Name;
            var result = newtags.Except(tagsnames);
            foreach (var item in result)
            {
                Tag tag = new Tag();
                tag.Name = item;
                var restag = tagRepository.create(tag);
                AdTag adTag = new AdTag();
                adTag.Tag_Id = restag.Id;
                var resadtag = adTagRepository.create(adTag);
            }
            var resultinter = newtags.Intersect(tagsnames);
            foreach (var item in resultinter)
            {
                AdTag adTag = new AdTag();
                adTag.Tag_Id = tagRepository.GetAll().Where(t => t.Name == item).FirstOrDefault().Id;
                var resadtag = adTagRepository.create(adTag);
                //ad.AdTags.Add(resadtag);

            }

            var res = adRepository.create(ad);
            var adtag = adTagRepository.GetAll();
            //foreach (var item in adtag)
            //{
            //    if (item.Ad_Id == null)
            //    {
            //        item.Ad_Id = res.Id;
            //    adTagRepository.update(item); }
            //}
            for (var re = adtag.FirstOrDefault(); re != null; re = adtag.Where(a => a.Id > re.Id).FirstOrDefault())
            {
                if (re.Ad_Id == null)
                {
                    re.Ad_Id = res.Id;
                    adTagRepository.update(re);
                }
            }

            return Ok(adDTO);
        }

        [HttpGet]
        [Route("GetAd")]
        public ActionResult<AdDTO> GetAd()
        {
            var ads = adRepository.GetAll();
            var currentad = ads.Where(a => a.CurrentAd == true)
                .FirstOrDefault();
            if (currentad == null)
            {
                currentad = ads.Where(a => a.CurrentAd == false)
                        .FirstOrDefault();
            }
            currentad.CurrentAd = false;
            currentad.ViewingNumber++;
            adRepository.update(currentad);
            var nextad = ads.Where(a => a.Id > currentad.Id)
                .FirstOrDefault();
            if (nextad != null)
            {
                nextad.CurrentAd = true;
                adRepository.update(nextad);
            }
            return Ok(adRepository.AdDTOConvert(currentad));
        }

        [HttpGet]
        [Route("GetAdByParam")]
        public ActionResult<AdDTO> GetAdByParam(string type, string value)
        {
            var ads = adRepository.GetAll();

            var resultAds = ads;
            switch (type)
            {
                case "Category":
                    resultAds = ads.Where(a => a.Category.Name == value);
                    break;
                case "Type":
                    resultAds = ads.Where(a => a.AdType.Name == value);
                    break;
                case "Tags":
                    var tags = tagRepository.GetAll().Select(t => t.Name);
                    string[] values = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var intersectTags = tags.Intersect(values);
                    resultAds = adTagRepository.GetAll().Where(at => intersectTags.Contains(at.Tag.Name)).Select(a => a.Ad);
                    break;
                default:
                    break;
            }
            var currentAd = resultAds.Where(a => a.CurrentAd == true)
                .FirstOrDefault();
            if (currentAd != null)
            {
                currentAd.CurrentAd = false;
                currentAd.ViewingNumber++;
                adRepository.update(currentAd);
            }
            else
            {
                currentAd = resultAds.FirstOrDefault();
            }
            var nextAd = resultAds.Where(a => a.Id > currentAd.Id)
                    .FirstOrDefault();
            if (nextAd != null)
            {
                nextAd.CurrentAd = true;
                adRepository.update(nextAd);
            }
            return Ok(adRepository.AdDTOConvert(currentAd));


        }
        [HttpDelete]
        [Route("DeleteAdById")]
        public ActionResult Delete(int id)
        {
            adRepository.delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("GetStatistic")]
        public ActionResult GetStatistic()
        {
            int TextAdView = adRepository.GetAll().Where(ac => ac.AdType.Name == "TextAd").Sum(av => av.ViewingNumber);
            int HtmlAdView = adRepository.GetAll().Where(ac => ac.AdType.Name == "HtmlAd").Sum(av => av.ViewingNumber);
            int BannerAdView = adRepository.GetAll().Where(ac => ac.AdType.Name == "BannerAd").Sum(av => av.ViewingNumber);
            int VideoAdView = adRepository.GetAll().Where(ac => ac.AdType.Name == "VideoAd").Sum(av => av.ViewingNumber);
            var PopularCategories = categoryRepository.GetAll().OrderByDescending(a => a.Ads.Count()).Select(c => c.Name).Take(3);
            var PopularAd = adRepository.GetAll().OrderBy(a => a.ViewingNumber).Take(10);
            var PopularAdDTO = PopularAd.Select(a => adRepository.AdDTOConvert(a));
            //var PopularTag = adTagRepository.GetAll().OrderBy(a => a.Ad.ViewingNumber).Select(t => t.Tag.Name).Take(15);
            var PopularTag = tagRepository.GetAll().OrderBy(ts => ts.AdTags.Sum(tv => tv.Ad.ViewingNumber)).Select(tn => tn.Name).Take(15);
            var statistic = new
            {
                text = TextAdView,
                html = HtmlAdView,
                banner = BannerAdView,
                video = VideoAdView,
                categories = PopularCategories,
                ads = PopularAdDTO,
                tags = PopularTag
            };
            return Ok(statistic);
        }
        [HttpGet]
        [Route("AddCategory")]
        public ActionResult AddCategory(Category category)
        {
            return Ok(categoryRepository.create(category));
        }
    }
}
