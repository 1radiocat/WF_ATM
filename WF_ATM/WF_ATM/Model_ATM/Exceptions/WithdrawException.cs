using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_ATM.Model_ATM.Exceptions
{
    class WithdrawException : Exception
    {
        public WithdrawException(string message)
            : base(message)
        {
            LogEx.Record(base.Message);
        }
    }
}
