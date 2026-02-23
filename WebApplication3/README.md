# UsersAPI

## Запуск проекта

1. Распакуй архив
2. Открой папку `UsersAPI` в Visual Studio или выполни в терминале:

```bash
cd UsersAPI
dotnet run
```

3. Браузер автоматически откроется на `https://localhost:7223/swagger`

## Если браузер не открылся автоматически

Перейди вручную: https://localhost:7223/swagger

## Эндпоинты

- `GET /users` — получить всех пользователей
- `POST /users` — добавить пользователя

### Пример POST-запроса (через PowerShell):

```powershell
Invoke-RestMethod -Uri "http://localhost:5282/users" -Method Post -ContentType "application/json" -Body '{"name":"Иван","email":"ivan@mail.ru","birthday":"1990-01-15","gender":"Male"}'
```
