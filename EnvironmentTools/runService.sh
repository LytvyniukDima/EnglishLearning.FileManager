imageName="english_file_manager"
containerName="file_manager_service"
networkName="english-net"

docker kill $containerName
docker rm $containerName

docker run -p 8900:8900 --name $containerName --network $networkName $imageName