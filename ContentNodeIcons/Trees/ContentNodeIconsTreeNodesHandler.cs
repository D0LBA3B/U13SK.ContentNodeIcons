using System.Linq;
using U13SK.ContentNodeIcons.Interfaces;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Trees;

namespace U13SK.ContentNodeIcons.Trees;

public class ContentNodeIconsTreeNodesHandler : INotificationHandler<TreeNodesRenderingNotification>
{
    private readonly IContentNodeIcons _contentNodeIconsService;

    public ContentNodeIconsTreeNodesHandler(IContentNodeIcons contentNodeIconsService)
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
