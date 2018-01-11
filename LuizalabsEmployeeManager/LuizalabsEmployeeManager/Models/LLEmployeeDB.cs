using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LuizalabsEmployeeManager.Exit;
using NLog;

namespace LuizalabsEmployeeManager.Models
{
    public class LLEmployeeDB : ILLEmployeeDB
    {
        #region Parameters
        private List<LLEmployee> Employees = new List<LLEmployee>();
        private int _nextId = 1;
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        public LLEmployeeDB()
        {
            Create(new LLEmployee { name = "Rodrigo Carvalho", email = "rodrigo@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Renato Pedigoni", email = "renato@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Thiago Catoto", email = "catoto@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "João Carvalho", email = "joao@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "José Pedigoni", email = "jose@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Alexandre Catoto", email = "alexandre@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Murilo Carvalho", email = "murilo@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Maria Pedigoni", email = "maria@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Joana Catoto", email = "joana@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Fernanda Carvalho", email = "fernanda@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Joaquina Pedigoni", email = "joaquina@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Joaquim Catoto", email = "joaquim@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Tereza Carvalho", email = "tereza@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Geraldo Pedigoni", email = "geraldo@luizalabs.com", department = "IntegraCommerce" });
            Create(new LLEmployee { name = "Rovani Catoto", email = "rovani@luizalabs.com", department = "IntegraCommerce" });
        }
        #endregion

        #region Employe's Action
        /// <summary>
        /// Method that creates employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Retorno Create(LLEmployee employee)
        {
            #region check employe's data
            if (employee == null)
            {
                return new Retorno { Sucess = false, Message = "Data not informed" };
            }

            try
            {
                if (GetEmployee(employee.name, employee.department))
                    return new Retorno { Sucess = false, Message = "Employee alread exists in data base" };
            }
            catch (Exception ex)
            {
                _log.Error("Fail to check employee's data. Cause : {0}", ex.Message);
                return new Retorno { Sucess = false, Message = "It's not possible check employee's data. Please try again later." };
            }
            #endregion

            #region create
            try
            {
                employee.id = _nextId;
                Employees.Add(employee);
                _nextId += 1;
            }
            catch (Exception ex)
            {
                _log.Error("Fail to includade employee's data. Cause : {0}", ex.Message);
                return new Retorno { Sucess = false, Message = "It's not possible include employee's data. Please try again later." };
            }

            return new Retorno { Sucess = true, Employee = employee };
            #endregion
        }

        /// <summary>
        /// Method that remove employe's data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Retorno Delete(int id)
        {
            try
            {
                Employees.RemoveAll(x => x.id == id);
                return new Retorno { Sucess = true };
            }
            catch (Exception ex)
            {
                _log.Error("Fail to remove employe's data. Cause : {0}", ex.Message);
                return new Retorno { Sucess = false, Message = "It's not possible remove employe's data. Please try again later." };
            }
        }

        /// <summary>
        /// Method that returns employee's data per page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="registersPerPage"></param>
        /// <returns></returns>
        public Retorno GetAllPagination(int page, int registersPerPage)
        {
            #region Get Amount of pages
            int total = Employees.Count();
            try
            {
                int totalMaxPages = Int32.Parse((Math.Floor(Decimal.Parse((total / registersPerPage).ToString()))).ToString());
            }
            catch (Exception ex)
            {
                _log.Error("Fail to check amount of pages. Cause : {0}", ex.Message);
                return new Retorno { Sucess = false, Message = "It's not possible check amount of pages. Please try again later." };
            }
            if (registersPerPage > total)
            return new Retorno { Sucess = false, Message = "The amount of register per page is bigger than amount of employees." };
            #endregion

            #region get employe's data
            List<LLEmployee> returnListEmployee = new List<LLEmployee>();

            int pageAUX = 1;
            int count = 1;


            try
            {
                foreach (var item in Employees)
                {
                    returnListEmployee.Add(item);
                    if (count == registersPerPage)
                    {
                        if (pageAUX == page)
                            break;
                        else
                        {
                            count = 1;
                            pageAUX += 1;
                            returnListEmployee = new List<LLEmployee>();
                        }
                    }
                    else
                    {
                        count += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("Fail to check get data. Cause : {0}", ex.Message);
                return new Retorno { Sucess = false, Message = "It's not possible get data. Please try again later." };
            }

            return new Retorno { Sucess = true, EmployeeList = returnListEmployee };
            #endregion
        }

        /// <summary>
        /// Method check if an employee alread exists
        /// </summary>
        /// <param name="name">Employee's name</param>
        /// <param name="department">Employee's department</param>
        /// <returns></returns>
        public bool GetEmployee(string name, string department)
        {
            #region check if employee exists
            LLEmployee emp = Employees.Where(x => x.name.ToLower() == name.ToLower() && x.department.ToLower() == department).FirstOrDefault();

            if (emp == null)
                return false;

            return true;
            #endregion
        }
        #endregion
    }
}