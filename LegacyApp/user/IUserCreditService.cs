namespace LegacyApp;

public interface IUserCreditService
{
    int GetCreditLimit(string lastName);
    bool isUserCreditLimitNotValid(User user);
    void CalculateUserCreditLimit(User user, Client client);
}