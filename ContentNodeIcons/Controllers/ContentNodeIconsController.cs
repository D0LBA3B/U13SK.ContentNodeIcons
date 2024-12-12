using Microsoft.AspNetCore.Mvc;
using U13SK.ContentNodeIcons.Database;
using U13SK.ContentNodeIcons.Interfaces;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace U13SK.ContentNodeIcons.Api;

public class ContentNodeIconsController : UmbracoAuthorizedJsonController
{
    private readonly IContentNodeIcons _contentNodeIconsService;

    public ContentNodeIconsController(IContentNodeIcons contentNodeIconsService)
    => _contentNodeIconsService = contentNodeIconsService;

    [HttpGet]
    public IActionResult GetIcons()
    {
        var icons = _contentNodeIconsService.GetIcons();
        return icons != null ? Ok(icons) : NotFound();
    }

    [HttpGet]
    public IActionResult GetIcon(int id)
    {
        var icon = _contentNodeIconsService.GetIcon(id);
        return icon != null ? Ok(icon) : NoContent();
    }

    [HttpPost]
    public IActionResult SaveIcon([FromBody] Schema config)
    {
        if (config == null)
            return BadRequest("Invalid icon data.");

        var savedIcon = _contentNodeIconsService.SaveIcon(config);
        return Ok(savedIcon);
    }

    [HttpDelete]
    public IActionResult RemoveIcon(int id)
    {
        var success = _contentNodeIconsService.RemoveIcon(id);
        return success ? Ok() : UnprocessableEntity();
    }
}
