using U13SK.ContentNodeIcons.Database;
using U13SK.ContentNodeIcons.Interfaces;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Extensions;

namespace U13SK.ContentNodeIcons.Services;

public class ContentNodeIconsService(AppCaches appCaches, IScopeProvider scopeProvider) : IContentNodeIcons
{
    private readonly IAppPolicyCache _runtimeCache = appCaches.RuntimeCache;

    public List<Schema> GetIcons()
        => _runtimeCache.GetCacheItem(Settings.CacheKey, FetchAllIconsFromDatabase);

    public Schema GetIcon(int id)
        => GetIcons().FirstOrDefault(x => x.ContentId == id);

    public Schema SaveIcon(Schema config)
    {
        ExecuteDatabaseOperation(scope => scope.Database.Save(config));
        RecycleCache();
        return config;
    }

    public bool RemoveIcon(int id)
    {
        ExecuteDatabaseOperation(scope => scope.Database.Delete<Schema>(id));
        RecycleCache();
        return true;
    }

    private void RecycleCache()
        => _runtimeCache.ClearByKey(Settings.CacheKey);

    private List<Schema> FetchAllIconsFromDatabase()
        => ExecuteDatabaseOperation(scope => scope.Database.Fetch<Schema>("SELECT * FROM U13SK_ContentNodeIcons"));

    private T ExecuteDatabaseOperation<T>(Func<IScope, T> operation)
    {
        using var scope = scopeProvider.CreateScope(autoComplete: true);
        var result = operation(scope);
        scope.Complete();
        return result;
    }

    private void ExecuteDatabaseOperation(Action<IScope> operation)
    {
        ExecuteDatabaseOperation(scope =>
        {
            operation(scope);
            return true;
        });
    }
}