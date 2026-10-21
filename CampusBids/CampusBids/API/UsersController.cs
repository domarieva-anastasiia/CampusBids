using Microsoft.AspNetCore.Mvc;

namespace CampusBids.API;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    /// <summary>
    /// Переглянути історію сповіщень. Перегляд списку сповіщень стосовно участі в аукціонах та попереджень, що надійшли до користувача.
    /// </summary>
    /// <response code="200">Повертає список сповіщень користувача.</response>
    /// <response code="204">Список сповіщень наразі порожній.</response>
    /// <response code="401">Користувач не авторизований. Потрібен токен доступу.</response>
    [HttpGet("me/notifications")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetNotifications() => Ok();

    /// <summary>
    /// Оскаржити попередження чи блокування. Оскарження отриманого користувачем попередження чи блокування акаунту.
    /// </summary>
    /// <response code="201">Оскарження успішно подано до розгляду адміністратором.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="403">Заборонено. Користувач не має активних попереджень або блокувань для оскарження.</response>
    /// <response code="409">Конфлікт. Користувач вже подав оскарження, яке наразі розглядається.</response>
    [HttpPost("me/appeals")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult AppealWarning() => Created("", null);

    /// <summary>
    /// Додати платіжну картку. Додавання платіжної картки до акаунту для автоматичного отримання оплати за товар після успішної передачі товару покупцю.
    /// </summary>
    /// <response code="201">Картку успішно додано до профілю.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="422">Помилка валідації. Некоректний номер картки, строк дії або CVV-код.</response>
    [HttpPost("me/payment-methods")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public IActionResult AddPaymentMethod() => Created("", null);

    /// <summary>
    /// Переглянути історію ставок. Перегляд історії зроблених користувачем ставок на лоти.
    /// </summary>
    /// <response code="200">Повертає історію ставок користувача.</response>
    /// <response code="204">Історія ставок наразі порожня.</response>
    /// <response code="401">Користувач не авторизований.</response>
    [HttpGet("me/bids")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetMyBids() => Ok();

    /// <summary>
    /// Переглянути історію продажів. Перегляд історії створених користувачем лотів.
    /// </summary>
    /// <response code="200">Повертає історію створених лотів.</response>
    /// <response code="204">Історія продажів наразі порожня.</response>
    /// <response code="401">Користувач не авторизований.</response>
    [HttpGet("me/lots")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetMyLots() => Ok();

    /// <summary>
    /// Попередити користувача (Адміністратор). Надання користувачу попередження з можливістю оскарження.
    /// </summary>
    /// <response code="201">Попередження успішно надіслано користувачу.</response>
    /// <response code="401">Адміністратор не авторизований.</response>
    /// <response code="403">Заборонено. Дія доступна лише для ролі Адміністратора.</response>
    /// <response code="404">Користувача із вказаним ID не знайдено.</response>
    [HttpPost("{id}/warnings")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult WarnUser(int id) => Created("", null);

    /// <summary>
    /// Заблокувати користувача (Адміністратор). Блокування акаунту користувача з можливістю оскарження.
    /// </summary>
    /// <response code="200">Акаунт користувача успішно заблоковано.</response>
    /// <response code="401">Адміністратор не авторизований.</response>
    /// <response code="403">Заборонено. Поточний користувач не має прав Адміністратора.</response>
    /// <response code="409">Конфлікт. Вказаний користувач вже заблокований.</response>
    [HttpPost("{id}/blocks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult BlockUser(int id) => Ok();

    /// <summary>
    /// Зняти попередження (Адміністратор). Скасування раніше виданого користувачу попередження (наприклад, після успішного оскарження).
    /// </summary>
    /// <response code="204">Попередження успішно знято.</response>
    /// <response code="401">Адміністратор не авторизований.</response>
    /// <response code="403">Заборонено. Поточний користувач не має прав Адміністратора.</response>
    /// <response code="404">Користувача із вказаним ID не знайдено або він не має активних попереджень.</response>
    [HttpDelete("{id}/warnings")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoveWarning(int id) => NoContent();

    /// <summary>
    /// Зняти блокування (Адміністратор). Розблокування акаунту користувача (наприклад, після успішного оскарження).
    /// </summary>
    /// <response code="204">Акаунт користувача успішно розблоковано, доступ до системи відновлено.</response>
    /// <response code="401">Адміністратор не авторизований.</response>
    /// <response code="403">Заборонено. Поточний користувач не має прав Адміністратора.</response>
    /// <response code="404">Користувача із вказаним ID не знайдено або його акаунт наразі не заблокований.</response>
    [HttpDelete("{id}/blocks")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoveBlock(int id) => NoContent();
}