using Microsoft.AspNetCore.Mvc;

namespace CampusBids.API;

[ApiController]
[Route("api/[controller]")]
public class LotsController : ControllerBase
{
    /// <summary>
    /// Переглянути активні лоти. Перегляд списку усіх активних в обраному гуртожитку лотів.
    /// </summary>
    /// <response code="200">Повертає список активних лотів.</response>
    /// <response code="204">Список активних лотів наразі порожній.</response>
    /// <response code="401">Користувач не авторизований. Потрібен токен доступу.</response>
    /// <response code="429">Забагато запитів від одного користувача.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public IActionResult GetActiveLots() => Ok();

    /// <summary>
    /// Переглянути конкретний лот. Обрання конкретного лоту зі списку та перегляд інформації про нього.
    /// </summary>
    /// <response code="200">Повертає детальну інформацію про конкретний лот.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="404">Лот із вказаним ID не знайдено.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetLot(int id) => Ok();

    /// <summary>
    /// Створити лот. Створення нового лоту для аукціону.
    /// </summary>
    /// <response code="201">Лот успішно створено.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="403">Заборонено. Користувач не має прав створювати лоти (можливо, акаунт заблоковано).</response>
    /// <response code="422">Невалідні дані лоту (наприклад, від'ємна стартова ціна або час закінчення в минулому).</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public IActionResult CreateLot() => Created("", null);

    /// <summary>
    /// Видалити лот (Адміністратор). Видалення виставленого продавцем лоту.
    /// </summary>
    /// <response code="204">Лот успішно видалено з системи.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="403">Заборонено. Дія доступна лише для ролі Адміністратора.</response>
    /// <response code="404">Лот із вказаним ID не знайдено.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteLot(int id) => Ok();

    /// <summary>
    /// Поставити ставку на лот. Поставлення нової ставки на конкретний лот.
    /// </summary>
    /// <response code="201">Ставку успішно прийнято.</response>
    /// <response code="400">Некоректне тіло запиту.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="404">Лот із вказаним ID не знайдено або аукціон вже завершено.</response>
    /// <response code="409">Конфлікт бізнес-логіки. Запропонована сума ставки менша або дорівнює поточній лідируючій ставці.</response>
    [HttpPost("{id}/bids")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult PlaceBid(int id) => Created("", null);

    /// <summary>
    /// Здійснити оплату. Здійснення оплати за товар у разі виграшу аукціону.
    /// </summary>
    /// <response code="200">Оплату успішно проведено.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="404">Лот із вказаним ID не знайдено.</response>
    /// <response code="409">Конфлікт. Лот уже був оплачений раніше або користувач не є переможцем торгів.</response>
    [HttpPost("{id}/payments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult MakePayment(int id) => Ok();

    /// <summary>
    /// Поставити запитання до продавця. Поставлення загальнодоступного запитання до продавця стосовно виставленого ним лоту.
    /// </summary>
    /// <response code="201">Запитання успішно опубліковано.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="404">Лот із вказаним ID не знайдено.</response>
    [HttpPost("{id}/questions")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AskQuestion(int id) => Created("", null);

    /// <summary>
    /// Дати відповідь на запитання покупця. Загальнодоступна відповідь на запитання покупця стосовно виставленого користувачем лоту.
    /// </summary>
    /// <response code="201">Відповідь успішно опубліковано.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="403">Заборонено. Відповідати на запитання може лише власник лоту.</response>
    /// <response code="404">Лот або запитання із вказаним ID не знайдено.</response>
    [HttpPost("{id}/questions/{questionId}/answers")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AnswerQuestion(int id, int questionId) => Created("", null);

    /// <summary>
    /// Підтвердити отримання товару (Покупець). Підтвердження успішного отримання товару від продавця.
    /// </summary>
    /// <response code="200">Отримання товару успішно підтверджено.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="403">Заборонено. Підтверджувати отримання може лише покупець-переможець.</response>
    /// <response code="409">Конфлікт. Отримання вже було підтверджено раніше або товар ще не оплачено.</response>
    [HttpPost("{id}/receipt-confirmations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult ConfirmReceipt(int id) => Ok();

    /// <summary>
    /// Підтвердити продаж товару (Продавець). Підтвердження успішної передачі товару покупцю.
    /// </summary>
    /// <response code="200">Продаж товару успішно підтверджено.</response>
    /// <response code="401">Користувач не авторизований.</response>
    /// <response code="403">Заборонено. Підтверджувати передачу може лише продавець (власник лоту).</response>
    /// <response code="409">Конфлікт. Продаж вже було підтверджено раніше або товар ще не оплачено.</response>
    [HttpPost("{id}/sale-confirmations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult ConfirmSale(int id) => Ok();
}