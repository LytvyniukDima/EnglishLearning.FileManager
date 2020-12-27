using FluentMigrator;

namespace EnglishLearning.FileManager.SqlMigrations.Migrations
{
    [Migration(1)]
    public class Migration_001_AddFoldersTable : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript($"{nameof(Migration_001_AddFoldersTable)}.sql");
        }
        
        public override void Down()
        {
            Delete.Table("Folders");
        }
    }
}