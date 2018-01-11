using LuizalabsEmployeeManager.Exit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuizalabsEmployeeManager.Models
{
    interface ILLEmployeeDB
    {
        Retorno GetAllPagination(int page, int registersPerPage);
        Retorno Create(LLEmployee employee);
        Retorno Delete (int id);
        bool GetEmployee(string name, string department);
    }
}
