using System;

namespace TDH.Filters
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