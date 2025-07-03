namespace TicketApp.Api.Application.CommonServices;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password), "Пароль не может быть пустым или null");

        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }

    public static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword)) return false;

        if (string.IsNullOrWhiteSpace(providedPassword)) return false;

        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}