using Umbraco.Core;
using Umbraco.Core.Composing;
using U13SK.ContentNodeIcons.Database;
using U13SK.ContentNodeIcons.Trees;
using U13SK.ContentNodeIcons.Api;

namespace U13SK.ContentNodeIcons
{
	public class CustomIconsComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			// Content Node Icon Service
			composition.Register<IContentNodeIconsService, ContentNodeIconsService>();

			// Listener for Tree Events
			composition.Components().Append<TreeEvents>();

			// Listener for Content Events
			//composition.Components().Append<ContentEvents>();

			// Database Migration
			composition.Components().Append<ContentNodeIconsComponent>();
		}
	}
}