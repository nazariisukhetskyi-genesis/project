#Codereview
# Genesis Practical Task

**Проблема:**
> - cтворити засіб Web API, який дозволить зареєструвати та аутентифікувати користувача, дізнатися поточний курс BTC - UAH.

**Вимоги:**
> - 3 endpoints (/user/create, /user/login, /btcRate);
> - для реєстрації достатньо використовувати email та пароль;
> - endpoint /btcRate доступний лише аутентифікованим користувачам;
> - функціонал реалізувати через роботу з файлами.

**Проєкт в node.js описаний нижче.**

**Для отримання даних про курс BTC - UAH використовується стороній [Web API](https://www.coinapi.io/).**

## Project_CSharp

Завдання реалізовано за архітектурою ASP.NET RESTful API (мова C#) з умовним дотримання таких хороших практик програмування, як:
> - Clean Code Principles
> - SOLID
> - GRASP

Аутентифікація реалізована за допомогою JWT Authentication (Access token - дійсний впродовж 10 хвилин, refresh token - використовується для отримання нового токена доступу в endpoint /updateAccessToken, дійсний впродовж 12 годин).

Паролі користувачів у файлі Data/Users.csv хешовані і "підсолені" (salted hash).

![hashed](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/CSharp/hashed.png)

Screenshots of using API (Swagger and Postman): 

![endpoints](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/CSharp/endpoints.png)

![unauthorized](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/CSharp/unauthorized.png)

![register](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/CSharp/register.png)

![login](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/CSharp/login.png)

![authorized](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/CSharp/authorized.png)

## Project_JS

Аутентифікація реалізована за допомогою JWT (token не вигасає). Для доступу до кінцівки /btcRate треба ввести header 'auth-token' зі значенням даним при логуванні. 

Паролі користувачі знаходяться у файлі data/users.json (salted hashed password using bcrypt).

Screenshots of using API (Postman):

![register](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/JS/register.png)

![login](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/JS/login.png)

![rate](https://github.com/nazariisukhetskyi-genesis/project/blob/main/Photos/JS/rate.png)
