using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBanking.Domain.Models
{
    public class AccountDTO
    {
        /// <summary>
        /// User first name
        /// </summary>
        [Required]
        public string? FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        [Required]
        public string? LastName { get; set; }

        /// <summary>
        /// user account closing balance
        /// </summary>
        [Required]
        public decimal Balance { get; set; }
    }
}
