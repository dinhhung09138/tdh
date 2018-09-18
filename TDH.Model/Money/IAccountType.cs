using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDH.Model.Money
{
    /// <summary>
    /// Account type
    /// </summary>
    public enum IAccountType
    {
        Cash = 0,
        Debit = 1,
        Credit = 2,
        Borrow = 3,
        Loan = 4,
        Saving = 5
    }
}
