using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Money
{
    /// <summary>
    /// Account type enum
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Cash
        /// </summary>
        Cash = 0,

        /// <summary>
        /// Debit card
        /// </summary>
        Debit = 1,

        /// <summary>
        /// Credit card
        /// </summary>
        Credit = 2,

        /// <summary>
        /// Borrow money
        /// </summary>
        Borrow = 3,

        /// <summary>
        /// Loan money
        /// </summary>
        Loan = 4,

        /// <summary>
        /// Saving account
        /// </summary>
        Saving = 5
    }
}
