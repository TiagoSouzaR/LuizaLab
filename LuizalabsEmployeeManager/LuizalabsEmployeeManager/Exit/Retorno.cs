using LuizalabsEmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuizalabsEmployeeManager.Exit
{
    public class Retorno
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public LLEmployee Employee { get; set; }
        public IEnumerable<LLEmployee> EmployeeList { get; set; }
    }
}