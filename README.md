## Как запустить бек?

### 1. Клонируем проект
### 2. Заходим в appsettings.json проекта API,меняем connection string на connection string к вашей базе. ![image](https://user-images.githubusercontent.com/60395869/177403870-8e86b8aa-51c5-4542-82a3-0c005ae30d33.png)
### 3. База создается при запросе автоматически, есть SWAGGER - можете посмотреть там примеры запросов. Сразу напоминаю, что на email и password стоит валидация, emai должен быть формата ```simple@mail.com``` пароль формата ```NewPassword1234?! - с большими, знаками и длиной > 8.```
