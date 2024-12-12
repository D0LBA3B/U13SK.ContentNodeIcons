using System;
using System.Linq;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core;
using U13SK.ContentNodeIcons.Api;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Trees;

public class ContentNodeIconsTreeNodesHandler : INotificationHandler<TreeNodesRenderingNotification>
{
    private readonly IContentNodeIconsService _contentNodeIconsService;

    public ContentNodeIconsTreeNodesHandler(IContentNodeIconsService contentNodeIconsService)
    {
        _contentNodeIconsService = contentNodeIconsService;
    }

    public void Handle(TreeNodesRenderingNotification notification)
    {
        if (notification.TreeAlias == Constants.Trees.Content)
        {
            var customIcons = _contentNodeIconsService.GetIcons();

            foreach (TreeNode treeNode in notification.Nodes)
            {
                if (int.TryParse(treeNode.Id?.ToString(), out var nodeId) && nodeId > 0)
                {
                    var node = customIcons.FirstOrDefault(x => x.ContentId == nodeId);
                    if (node != null)
                    {
                        treeNode.Icon = $"{node.Icon} {node.IconColor}";
                    }
                }
            }
        }
    }
}

public class ContentNodeIconsMenuHandler : INotificationHandler<MenuRenderingNotification>
{
    private readonly IMenuItemCollectionFactory _menuItemCollectionFactory;

    public ContentNodeIconsMenuHandler(IMenuItemCollectionFactory menuItemCollectionFactory)
    {
        _menuItemCollectionFactory = menuItemCollectionFactory;
    }

    public void Handle(MenuRenderingNotification notification)
    {
        if (notification.TreeAlias == Constants.Trees.Content && int.TryParse(notification.NodeId, out var nodeId) && nodeId >= 0)
        {
            var menuItem = new MenuItem("setIcon", "Set Icon")
            {
                Icon = "favorite",
                OpensDialog = true
            };

            menuItem.AdditionalData.Add("actionView", "/app_plugins/U13SK.contentnodeicons/tree.action.seticontemplate.html");
            notification.Menu.Items.Insert(5, menuItem);
        }
    }
}
