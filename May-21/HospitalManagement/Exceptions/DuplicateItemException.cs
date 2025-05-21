using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Exceptions
{
    public class DuplicateItemException : Exception
    {
        private string _message="Duplicate item found";
        public DuplicateItemException(string msg)
        {
            _message = msg;
        }

        public override string Message => _message;
    }
}
