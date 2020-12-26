set -x

imageName="english_file_manager"
containerName="file_manager_migrations"
networkName="english-net"
connectionString="Server=sql_server,1433;Database=EnglishLearning_FileManager;User=sa;Password=Qwerty123;"

docker kill $containerName
docker rm $containerName

docker run --name $containerName --network $networkName --env 'ConnectionStrings__SqlServer'=$connectionString $imageName "dotnet /app/migrations/EnglishLearning.FileManager.SqlMigrations.dll"
