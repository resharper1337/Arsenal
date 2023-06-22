# Arsenal
Arsenal - to control your employers

# Внимание: сервер уже запущен и доступен по ссылке totalover.com (запускать его повторно не требуется)
В коде присутствуют небольшие комментарии для улучшения его читабельности.

# Стек используемых технологий:
-ASP.NET (с rest и SignalR) - сервер(админка)   
-WinForms C# - клиент для сотрудников  
-Mysql(MariaDb) - субд для нашего проекта  

# Сервер уже размещен на хосинге для удобства, доступ к админке предоставляется по ссылке "http://totalover.com/".
На данной странице мы будем видеть всех подключенных пользователей к сети и иметь возможность получить скриншот их рабочего стола в любой момент времени.

# Клиент представляет собой маленькое WinForms приложение, 
которое должны запускать сотрудники на своих компьютерах. После авторизации, клиент переходит в silent mode и сворачивается в трей. Для отключения от сети, необходимо нажать правой кнопкой на значок в трее, после чего нажать кнопку "Выход".

# Для тестирования приложения было создано 5 тестовых клиент-пользователей (т.к. формы регистрации пока нет):  
(1)Domain: root  
Login: Anya  
Pass: 6858  

(2)Domain: root  
Login: Baby22  
Pass: 12345  

(3)Domain: root  
Login: Artem7  
Pass: 5656  

(4)Domain: ChopCentavr  
Login: Admin  
Pass: 78956  

(5)Domain: ChopCentavr  
Login: Zack99  
Pass: 89898  
