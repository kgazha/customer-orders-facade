# Задание

## Создание сервиса CustomerOrders

Добавление нового сервиса для получения информации о заказах пользователей

## Требуется

- Реализовать новый сервис CustomerOrders
- Добавить валидацию REST запросов
- Реализовать ExceptionFilter для обработки исключений в REST ручке
- Реализовать Middleware для логирования времени обработки запроса


### Описание контрактов

- CustomerOrders выполняет агрегацию данных на основании ответов от OrderService и CustomerService

1. Получение заказов клиента
    - Запрос: customer_id, limit, offset 
    - Ответ:
        - Успешный с данными о заказах пользователя и информации о пользователе, включая имя пользователя
        - Успешный с данными пользователя, но без заказов
        - Ошибка, пользователь не найден
     
2. Получение заказов по региону
    - Запрос: region_id, limit, offset 
    - Ответ: Данные о заказах пользователей в регионе, включая имена пользователей

3. Написать unit и интеграционные тесты, используя xUnit, Mock, AutoFixture, FluentAssertions. 

## Дополнительное задание
- Ручки должны отображаться в Swagger
