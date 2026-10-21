using Microsoft.AspNetCore.Mvc;

namespace CampusBids.API;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Автентифікуватися. Вхід до особистого акаунту за допомогою корпоративної пошти та паролю.
    /// </summary>
    /// <response code="200">Успішна автентифікація. Повертає JWT токен доступу.</response>
    /// <response code="400">Неправильний формат запиту (відсутні обов'язкові поля).</response>
    /// <response code="422">Помилка валідації (неіснуюча пошта або неправильний пароль).</response>
    /// <response code="429">Забагато невдалих спроб входу. Акаунт тимчасово заблоковано системою захисту.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public IActionResult Login()
    {
        return Ok(new { Token = "mock-jwt-token" });
    }
}