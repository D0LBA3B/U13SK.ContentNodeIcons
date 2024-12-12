using System.Collections.Generic;
using U13SK.ContentNodeIcons.Database;

namespace U13SK.ContentNodeIcons.Interfaces;

public interface IContentNodeIcons
{
    List<Schema> GetIcons();
    Schema GetIcon(int id);
    Schema SaveIcon(Schema config);
    bool RemoveIcon(int id);
}