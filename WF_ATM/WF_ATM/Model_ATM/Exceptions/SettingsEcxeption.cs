using System;

namespace WF_ATM.Model_ATM.Exceptions
{
    class SettingsException : Exception
    {
        public SettingsException(string message)
            : base(message)
        {
            LogEx.Record(base.Message);
        }        
    }
}
