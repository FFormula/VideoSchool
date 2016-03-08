using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoSchool.Models
{
    public class Shared
    {
        public DataBase db { get; private set; }
        public Error error { get; private set; }
        public Controller controller { get; private set; }

        public Shared (Controller controller)
        {
            this.controller = controller;
            error = new Error();
            db = new DataBase(error);
        }

    }
}