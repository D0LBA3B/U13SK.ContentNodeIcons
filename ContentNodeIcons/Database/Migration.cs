using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Migrations;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Migrations.Upgrade;

namespace U13SK.ContentNodeIcons.Database
{

    /*
	 * Umbraco Documentation: https://our.umbraco.com/documentation/extending/database/
	 */
    public class ContentNodeIconsComponent : IComponent
    {
        private readonly ICoreScopeProvider _coreScopeProvider;
        private readonly IMigrationPlanExecutor _migrationPlanExecutor;
        private IKeyValueService _keyValueService;
        private readonly ILogger<ContentNodeIconsComponent> _logger;

        public ContentNodeIconsComponent(ICoreScopeProvider coreScopeProvider, IMigrationPlanExecutor migrationPlanExecutor, IKeyValueService keyValueService, ILogger<ContentNodeIconsComponent> logger)
        {
            _coreScopeProvider = coreScopeProvider;
            _migrationPlanExecutor = migrationPlanExecutor;
            _keyValueService = keyValueService;
            _logger = logger;
        }

        public void Initialize()
        {

            var migrationPlan = new MigrationPlan("U13SKContentNodeIcons");

            migrationPlan.From(string.Empty)
                .To<AddContentNodeIconsTable>("contentNodeIcons-db");

            var upgrader = new Upgrader(migrationPlan);
            upgrader.Execute(_migrationPlanExecutor, _coreScopeProvider, _keyValueService);
        }

        public void Terminate()
        {
        }
    }

    public class AddContentNodeIconsTable : MigrationBase
    {
        public AddContentNodeIconsTable(IMigrationContext context) : base(context)
        {
        }

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "AddU13SKContentNodeIconsTable ");

            // Lots of methods available in the MigrationBase class - discover with this.
            if (TableExists("U13SK_ContentNodeIcons") == false)
                Create.Table<Schema>().Do();
            else
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "U13SKContentNodeIcons");
        }
    }
}