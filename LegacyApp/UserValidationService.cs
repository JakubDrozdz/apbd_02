using System;

namespace LegacyApp;

public class UserValidationService
{
    public static bool AreUserDetailsValid(string firstName, string lastName, string email)
    {
        return !(IsUserNameNotValid(firstName, lastName) || IsUserEmailNotValid(email));
    }

    private static bool IsUserNameNotValid(string firstName, string lastName)
    {
        return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
    }

    private static bool IsUserEmailNotValid(string email)
    {
        return !email.Contains("@") && !email.Contains(".");
    }

    public static bool ValidateMinimalAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        return age >= 21;
    }
}