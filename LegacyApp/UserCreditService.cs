using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp
{
    public class UserCreditService : IDisposable
    {
        /// <summary>
        /// Simulating database
        /// </summary>
        private readonly Dictionary<string, int> _database =
            new Dictionary<string, int>()
            {
                {"Kowalski", 200},
                {"Malewski", 20000},
                {"Smith", 10000},
                {"Doe", 3000},
                {"Kwiatkowski", 1000}
            };
        
        public void Dispose()
        {
            //Simulating disposing of resources
        }

        /// <summary>
        /// This method is simulating contact with remote service which is used to get info about someone's credit limit
        /// </summary>
        /// <returns>Client's credit limit</returns>
        internal int GetCreditLimit(string lastName)
        {
            int randomWaitingTime = new Random().Next(3000);
            Thread.Sleep(randomWaitingTime);

            if (_database.ContainsKey(lastName))
                return _database[lastName];

            throw new ArgumentException($"Client {lastName} does not exist");
        }
        
        public void CalculateUserCreditLimit(User user, Client client)
        {
            if (client.Type == LegacyAppConstants.VERY_IMPORTANT_CLIENT)
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == LegacyAppConstants.IMPORTANT_CLIENT)
            { 
                user.CreditLimit = GetCreditLimit(user.LastName) * 2;
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName);
                    user.CreditLimit = creditLimit;
                }
            }
        }

        public bool isUserCreditLimitNotValid(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }
    }
}