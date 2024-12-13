using U13SK.ContentNodeIcons.Interfaces;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Trees;

namespace U13SK.ContentNodeIcons.Trees;

public class ContentNodeIconsTreeNodesHandler(IContentNodeIcons contentNodeIconsService)
    : INotificationHandler<TreeNodesRenderingNotification>
{
    public void Handle(TreeNodesRenderingNotification notification)
    {
        if (notification.TreeAlias == Constants.Trees.Content)
        {
            var customIcons = contentNodeIconsService.GetIcons();

            foreach (TreeNode treeNode in notification.Nodes)
            {
                if (int.TryParse(treeNode.Id?.ToString(), out var nodeId) && nodeId > 0)
                {
                    var node = customIcons.FirstOrDefault(x => x.ContentId == nodeId);
                    if (node != null)
                    {
                        if(!string.IsNullOrEmpty(node.Icon))
                            treeNode.Icon = $"{node.Icon} {node.IconColor}";

                        if (node.TextColorization)
                        {
                            treeNode.CssClasses.Add("text-colorized");
                            treeNode.CssClasses.Add(node.IconColor);
                        }
                    }
                }
            }
        }
    }
}
