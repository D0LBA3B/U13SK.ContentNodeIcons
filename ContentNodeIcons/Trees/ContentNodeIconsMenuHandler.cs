using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Notifications;

namespace U13SK.ContentNodeIcons.Trees;

public class ContentNodeIconsMenuHandler : INotificationHandler<MenuRenderingNotification>
{
    public void Handle(MenuRenderingNotification notification)
    {
        if (notification.TreeAlias == Constants.Trees.Content && int.TryParse(notification.NodeId, out var nodeId) && nodeId >= 0)
        {
            var menuItem = new MenuItem("setIcon", "Set Icon")
            {
                Icon = "favorite",
                OpensDialog = true
            };

            menuItem.AdditionalData.Add("actionView", "/App_Plugins/U13SK.ContentNodeIcons/Tree.action.setIconTemplate.html");
            notification.Menu.Items.Insert(5, menuItem);
        }
    }
}