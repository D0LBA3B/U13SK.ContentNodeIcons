using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using U13SK.ContentNodeIcons.Database;
using Umbraco.Cms.Web.Common.Controllers;

namespace U13SK.ContentNodeIcons.Api
{
    public class ContentNodeIconsController : UmbracoApiController
    {

        private readonly IContentNodeIconsService _contentNodeIconsService;

        public ContentNodeIconsController(IContentNodeIconsService contentNodeIconsService)
        {
            _contentNodeIconsService = contentNodeIconsService;
        }

        [Route("geticons")]
        public List<Schema> GetIcons()
        {
            return _contentNodeIconsService.GetIcons();
        }

        [HttpGet]
        [Route("geticon/{id}")]
        // umbraco/api/contentnodeicons/geticon
        public Schema GetIcon(int id = 0)
        {
            return _contentNodeIconsService.GetIcon(id);
        }

        [HttpPost]
        // umbraco/api/contentnodeicons/saveicon
        public Schema SaveIcon(Schema config)
        {
            return _contentNodeIconsService.SaveIcon(config);
        }

        [HttpGet]
        [Route("removeicon/{id}")]
        // umbraco/api/contentnodeicons/removeicon
        public bool RemoveIcon(int id = 0)
        {
            return _contentNodeIconsService.RemoveIcon(id);
        }
    }
}
