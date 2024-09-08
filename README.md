# Треубется в проекте Events.API создать json-файл со следующей конфигурацией

```json
{
  "ConnectionStrings": {
    "EventConnectionString": "*",
  },
  "Jwt": {
    "Key": "*",
    "Issuer": "https://localhost:7103",
    "Audience": "https://localhost:4200"
  }
}
```
key - для генерации jwt-токенов(более 64 символов)
EventConnectionString - для подключения к бд (использовал SqlServer)
