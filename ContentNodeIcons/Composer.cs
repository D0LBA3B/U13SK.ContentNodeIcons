using Microsoft.Extensions.DependencyInjection;
using U13SK.ContentNodeIcons.Database;
using U13SK.ContentNodeIcons.Api;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace U13SK.ContentNodeIcons
{
	public class Composer : IComposer
	{
		public void Compose(IUmbracoBuilder builder)
		{
			// Content Node Icon Service
			builder.Services.AddScoped<IContentNodeIconsService, ContentNodeIconsService>();

            // Listener for Tree Events
            builder.AddNotificationHandler<TreeNodesRenderingNotification, ContentNodeIconsTreeNodesHandler>();
            builder.AddNotificationHandler<MenuRenderingNotification, ContentNodeIconsMenuHandler>();

            // Listener for Content Events
            //composition.Components().Append<ContentEvents>();

            // Database Migration
            builder.Components().Append<ContentNodeIconsComponent>();
		}
	}
}