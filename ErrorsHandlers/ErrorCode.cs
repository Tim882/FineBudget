using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorsHandlers
{
    public enum ErrorCode
    {
        // Общие ошибки (1xxx)
        [Description("Внутренняя ошибка сервера")]
        InternalError = 1000,

        [Description("Неверный запрос")]
        BadRequest = 1001,

        [Description("Ресурс не найден")]
        NotFound = 1002,

        // Ошибки валидации (2xxx)
        [Description("Ошибка валидации данных")]
        ValidationError = 2001,

        [Description("Email уже занят")]
        EmailAlreadyExists = 2002,

        // Ошибки авторизации (3xxx)
        [Description("Требуется авторизация")]
        Unauthorized = 3001,

        [Description("Неверные учетные данные")]
        InvalidCredentials = 3002,

        [Description("Доступ запрещен")]
        Forbidden = 3003,

        // Ошибки базы данных (4xxx)
        [Description("Не удалось подключиться к базе данных")]
        ConnectionFailed = 4000

        // Ошибки интеграции с внешними сервисами (5xxx)

    }
}
