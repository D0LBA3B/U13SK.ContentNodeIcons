using Microsoft.Extensions.DependencyInjection;
using U13SK.ContentNodeIcons.Database;
using U13SK.ContentNodeIcons.Interfaces;
using U13SK.ContentNodeIcons.Services;
using U13SK.ContentNodeIcons.Trees;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace U13SK.ContentNodeIcons;

public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        // Content Node Icon Service
        builder.Services.AddScoped<IContentNodeIcons, ContentNodeIconsService>();

        // Listener for Tree Events
        builder.AddNotificationHandler<TreeNodesRenderingNotification, ContentNodeIconsTreeNodesHandler>();
        builder.AddNotificationHandler<MenuRenderingNotification, ContentNodeIconsMenuHandler>();

        // Database Migration
        builder.Components().Append<ContentNodeIconsComponent>();
    }
}