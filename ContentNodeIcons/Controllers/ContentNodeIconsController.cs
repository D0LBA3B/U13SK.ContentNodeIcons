using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using U13SK.ContentNodeIcons.Database;
using U13SK.ContentNodeIcons.Interfaces;
using Umbraco.Cms.Web.Common.Controllers;

namespace U13SK.ContentNodeIcons.Api;

public class ContentNodeIconsController : UmbracoApiController
{
    private readonly IContentNodeIcons _contentNodeIconsService;

    public ContentNodeIconsController(IContentNodeIcons contentNodeIconsService)
    => _contentNodeIconsService = contentNodeIconsService;

    [Route("geticons")]
    public List<Schema> GetIcons()
        => _contentNodeIconsService.GetIcons();

    [HttpGet]
    [Route("geticon/{id}")]
    // umbraco/api/contentnodeicons/geticon
    public Schema GetIcon(int id = 0)
        => _contentNodeIconsService.GetIcon(id);

    [HttpPost]
    // umbraco/api/contentnodeicons/saveicon
    public Schema SaveIcon(Schema config)
        => _contentNodeIconsService.SaveIcon(config);

    [HttpGet]
    [Route("removeicon/{id}")]
    // umbraco/api/contentnodeicons/removeicon
    public bool RemoveIcon(int id = 0)
        => _contentNodeIconsService.RemoveIcon(id);
}