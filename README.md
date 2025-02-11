На данный момент проект является полностью рабочим (Только клиент на Avalonia. Перенос на WPF начал совсем недавно. В ближайшие недели закончу).
Есть возможность открывать страницу товаров, делать заказы, тем самым делая записи в БД<br>

Описание проекта, ТЗ указаны в файле "ТЗ.docx". Также в ходе разработки были пересмотрены некоторые аспекты ТЗ в угоду "правильности UI/UX" или же "логичности".<br>

----------------

Дизайн и цвета - пока в разработке и не являются окончательным решением. <br>

----------------

![Project preview](https://github.com/Thamadar/Online_shop/blob/main/Readme-preview.jpg?raw=true)

----------------

Для запуска необходимо иметь: <br>
-.NET 7.0. <br>
-SQL Server (База и таблицы сами инициализируются при запуске shop.server). <br>
<br>
Инструкция по запуска: <br>
-Настройте при необходимости параметры сервера и подключения к серверу в shop.server/Properties/launchSerttings.json и в shop.server/appsettings.json  <br>
-Настройте при необходимости параметры подключения к серверу в Shop.Client/Configurations/ConnectionConfiguration в параметре Defaults (изначально адрес стоит "http://localhost:5100")  <br>
-Запустите shop.server. <br>
-Запустите shop.client. <br>
<br>
----------------
<br>
P.S. Ещё в работе некоторые модули, такие как: <br>
-Страница пользователя <br>
-Локализация <br>
-Модальные окна: ожидания/оповещения

