using FluentMigrator;

namespace EnglishLearning.FileManager.SqlMigrations.Migrations
{
    [Migration(2)]
    public class Migration_002_AddFilesTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("Migration_002_AddFilesTable.sql");
        }

        public override void Down()
        {
            Delete.Table("Files");
        }
    }
}