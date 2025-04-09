using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public enum ErrorCode
    {
        // Общие ошибки (1xxx)
        [Description("Внутренняя ошибка сервера")]
        InternalError = 1000,
        ConfigurationError = 1001,

        // Ошибки валидации (2xxx)
        [Description("Некорректные данные")]
        ValidationFailed = 2000,
        [Description("Поле обязательно для заполнения")]
        RequiredField = 2001,
        [Description("Значение параметра выходит за допустимые")]
        OutOfRange = 2002,
        [Description("Неправильный формат email")]
        InvalidEmail = 2003,

        // Ошибки авторизации/аутентификации (3xxx)
        [Description("Пользователь не авторизован")]
        Unauthorized = 3000,
        [Description("Неверные учетные данные")]
        InvalidCredentials = 3001,
        [Description("Невалидный токен")]
        InvalidToken = 3002,
        [Description("Доступ запрещен")]
        AccessDenied = 3003,

        // Ошибки при работе с БД (4xxx)
        [Description("Не удалось подключиться к базе данных")]
        DatabaseConnection = 4000,
        [Description("Элемент не найден")]
        NotFound = 4001,
        [Description("Элемент уже существует")]
        AlreadyExists = 4002,

        // Интеграционные ошибки (5xxx)
        [Description("Возникла ошибка при получении данных с сервиса")]
        ExternalApiError = 5000,
        [Description("Сервис не отвечает")]
        ThirdPartyTimeout = 5001,

        // Бизнес-ошибки (6xxx)
    }
}
