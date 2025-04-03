# Script para rodar DeveloperStore API com Docker Compose

Write-Host "🔄 Limpando containers antigos..."
docker-compose down -v --remove-orphans

Write-Host "`n🐳 Subindo containers com build..."
docker-compose up --build -d

Write-Host "`n⏳ Aguardando inicialização dos serviços..."
Start-Sleep -Seconds 10

Write-Host "`n🌐 Abrindo Swagger em http://localhost:8080/swagger"
Start-Process "http://localhost:8080/swagger"
