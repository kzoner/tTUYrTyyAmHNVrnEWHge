using System;
using System.Collections.Generic;
using System.Text;

namespace Inside.DataProviders
{
    class SQLException : Exception
    {      
        public SQLException(string message)
            : base(message)
        {
        }

        public SQLException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
