using System;

namespace LegacyApp
{
    public class UserService
    {
        private IClientRepository _clientRepository;
        private IUserCreditService _userCreditService;
        private IUserDataAccess _userDataAccess;

        public UserService() : this(new ClientRepository(), new UserCreditService(), new UserDataAccessAdapter())
        {
        }

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService, IUserDataAccess userDataAccess)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
            _userDataAccess = userDataAccess;
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
            _userDataAccess.AddUser(user);
            
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
            _userCreditService.CalculateUserCreditLimit(user, client);
            return user;
        }
    }
}
