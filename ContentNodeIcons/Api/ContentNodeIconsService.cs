﻿using System.Collections.Generic;
using System.Linq;
using U13SK.ContentNodeIcons.Database;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Extensions;

namespace U13SK.ContentNodeIcons.Api
{
    public class ContentNodeIconsService : IContentNodeIconsService
    {
        private readonly IAppPolicyCache _runtimeCache;
        private readonly IScopeProvider _scopeProvider;

        public ContentNodeIconsService(AppCaches appCaches, IScopeProvider scopeProvider)
        {
            _runtimeCache = appCaches.RuntimeCache;
            _scopeProvider = scopeProvider;
        }

        public List<Schema> GetIcons()
        {
            // GetCacheItem will automatically insert the object
            // into cache if it doesn't exist.
            return _runtimeCache.GetCacheItem(Settings.CacheKey, () =>
            {
                using (var scope = _scopeProvider.CreateScope(autoComplete: true))
                {
                    var database = scope.Database;
                    var results = scope.Database.Fetch<Schema>("SELECT * FROM U13SK_ContentNodeIcons");
                    scope.Complete();
                    return results;
                }
            });
        }

        public Schema GetIcon(int id)
        {
            var icons = GetIcons();
            return icons.Where(x => x.ContentId.Equals(id)).FirstOrDefault();
        }

        public Schema SaveIcon(Schema config)
        {
            using (var scope = _scopeProvider.CreateScope(autoComplete: true))
            {
                var database = scope.Database;
                scope.Database.Save<Schema>(config);
                scope.Complete();

                RecycleCache();

                return config;
            }
        }

        public bool RemoveIcon(int id)
        {
            using (var scope = _scopeProvider.CreateScope(autoComplete: true))
            {
                var database = scope.Database;
                scope.Database.Delete<Schema>(id);
                scope.Complete();

                RecycleCache();

                return true;
            }
        }

        private void RecycleCache()
        {
            // Clear cache
            _runtimeCache.ClearByKey(Settings.CacheKey);

            // Rebuild cache
            GetIcons();
        }
    }
}
