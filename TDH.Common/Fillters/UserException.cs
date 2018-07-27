using System;

namespace TDH.Common.Fillters
{
    public class UserException : Exception
    {
        public UserException()
        {

        }

        public UserException(string message) : base(message)
        {

        }
    }
}
