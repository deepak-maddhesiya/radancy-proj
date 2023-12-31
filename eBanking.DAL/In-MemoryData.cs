﻿using eBanking.Domain;

namespace eBanking.DAL
{
    public class In_MemoryData
    {
        public readonly static List<Account> accounts = new List<Account>()
        {
            new Account() { FirstName = "John", LastName = "Roger", AccountNumber = 1, Balance = 1000 },
            new Account() { FirstName = "Den", LastName = "Smith", AccountNumber = 2, Balance = 4000 },
            new Account() { FirstName = "Joy", LastName = "Smith", AccountNumber = 3, Balance = 1500 }
        };
    }
}
