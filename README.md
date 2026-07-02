На данный момент решение является полностью рабочим - сервер, клиенты: WPF и Avalonia. <br>
Есть возможность открывать страницу товаров, делать заказы, тем самым делая записи в БД <br> 
----------------

Дизайн и цвета - пока в разработке и не являются окончательным решением. <br>

----------------
Клиент: <br>
![](https://github.com/Thamadar/Online_shop/blob/main/preview_client.gif) <br>

----------------
 
Swagger: <br>
![](https://github.com/Thamadar/Online_shop/blob/main/preview_swagger.gif) <br> 
 
----------------

Для запуска необходимо иметь: <br>
-.NET 7.0. <br>
-SQL Server (Желательно Express версия. Также база и таблицы сами инициализируются при запуске shop.server). <br> 
<br>
Инструкция по запуску: <br>
-Настройте при необходимости параметры сервера и подключения к серверу в shop.server/Properties/launchSerttings.json и в shop.server/appsettings.json  <br>
-Настройте при необходимости параметры подключения к серверу в shop.сlient.ВыбранныйВамиКлиент/Configurations/ConnectionConfiguration в параметре Defaults (изначально адрес стоит "http://localhost:5100")  <br>
-Запустите shop.server. <br>
**Запуск server'а: <br>
dotnet run --project "ваш путь\src\shop.server.prj\shop.server.csproj" --launch-profile "http" <br>**
-Запустите shop.client.ВыбранныйВамиКлиент. (WPF/Avalonia) <br>
<br>  

----------------

P.S. Ещё в работе некоторые модули, такие как: <br>
-Авторизация <br>
-Страница пользователя <br>
-Локализация (Локализация наименований товаров уже имеется. Речь идет о подписях, контента кнопок и подобного на клиенте)<br>
-Модальные окна: ожидания/оповещения <br>
