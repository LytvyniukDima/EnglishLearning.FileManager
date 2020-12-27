using FluentMigrator;

namespace EnglishLearning.FileManager.SqlMigrations.Migrations
{
    [Migration(3)]
    public class Migration_003_AddDefaultData : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript($"{nameof(Migration_003_AddDefaultData)}.sql");
        }

        public override void Down()
        {
        }
    }
}