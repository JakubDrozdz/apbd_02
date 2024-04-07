using System;

namespace LegacyApp
{
    public class UserService
    {
        private ClientRepository _clientRepository;
        private UserCreditService _userCreditService;

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
        }
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!UserValidationService.AreUserDetailsValid(firstName, lastName, email) || !UserValidationService.ValidateMinimalAge(dateOfBirth))
            {
                return false;
            }

            var user = CreateUser(firstName, lastName, email, dateOfBirth, clientId);
            
            if (_userCreditService.isUserCreditLimitNotValid(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var client = _clientRepository.GetById(clientId);
            
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            CalculateUserCreditLimit(user, client);
            return user;
        }

        private void CalculateUserCreditLimit(User user, Client client)
        {
            using (_userCreditService)
            {
                _userCreditService.CalculateUserCreditLimit(user, client);
            }
        }
    }
}
