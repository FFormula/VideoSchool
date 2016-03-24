using System;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class UserPassport : User
    {
        public UserPassport () : this (null)
        { }

        public UserPassport (Shared shared)
            : base (shared)
        {
        }


    }
}