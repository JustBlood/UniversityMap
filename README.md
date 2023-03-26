# UniversityMap
## Принципы:
  1. Ветка main всегда deployable - т.е. всегда работоспособна и готова к выпуску на сервер.
## Схема работы с ветками:
  1. Вся разработка ведется в ветке develop
  2. Каждый отдельный разработчик для внедрения новой функциональности создает ветку feature_НАЗВАНИЕ-ФИЧИ-КОТОРУЮ-ВЫ-ДЕЛАЕТЕ
  3. При окончании работы с новой функциональностью - делаем запрос на слияние ветки feature_X с develop
  4. При неправильной работе ветки develop после слияния - в ветке hotfix устраняем / откатываем изменения и делаем слияние с develop, если все корректно работает
  5. Когда ветка develop пополнилась значительно функциональностью - сливаем в main. При ошибках - действуем аналогично пункту 4
### Со временем будем настраивать **Jenkins** для автоматического тестирования кода, но сначала хотя бы для успешной сборки проекта.

# Как создать локальную базу данных в своем проекте:

(если локальная база данных уже была создана, то на до предварительно ее удалить и остановить службы sqllocaldb)
Выполните следующее:


1. Откройте терминал в visual studio (Вид - Терминал)
2. В Терминале введите команду: dotnet tool install --global dotnet-ef --version 6.*
3. Введите в Консоли команду: dotnet-ef database update

edit: возможно при выполнении последней команды у вас выдаст ошибку - что SQL Server не установлен. Его нужно установить с оф. сайта: https://www.microsoft.com/ru-RU/download/details.aspx?id=101064.
Также установить пакет SQL Server LocalDB: https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16. Скачиваете, устанавливаете, дублируете команду №3 и всё должно заработать.

После данных манипуляций, все страницы будут доступны и у вас будет локальная база данных (в проект сохраняться она не будет, она в папке C:/users/user/ с расширением .mdf)
