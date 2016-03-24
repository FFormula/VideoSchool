using System;
using VideoSchool.Models.Share;

namespace VideoSchool.Models.Units
{
    public class UserContact : User
    {
        public UserContact()
            : this(null)
        { }

        public UserContact(Shared shared)
            : base(shared)
        {
        }


    }
}