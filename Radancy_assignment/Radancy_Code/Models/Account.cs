using System.Net;

namespace Radancy_Code.Models
{
    public class Account
    {
        /// <summary>
        /// unique and application automatically create account number
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        public string? LastName { get; set; }
        
        /// <summary>
        /// user account closing balance
        /// </summary>
        public decimal Balance { get; set; }

    }
}
