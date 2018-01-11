using LuizalabsEmployeeManager.Exit;
using LuizalabsEmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LuizalabsEmployeeManager.Controllers
{
    public class EmployeeController : ApiController
    {
        static readonly ILLEmployeeDB employeeDB = new LLEmployeeDB();


        public IEnumerable<LLEmployee> GetEmployeeWithPagination(int page, int registersPerPage)
        {
            Retorno retornoDB = employeeDB.GetAllPagination(page, registersPerPage);
            if (retornoDB.Sucess)
                return retornoDB.EmployeeList;

            return null;
        }
        public HttpResponseMessage PostProduto([FromBody]LLEmployee item)
        {
            Retorno retornoDB = employeeDB.Create(item);
            if (retornoDB.Sucess)
            {
                var response = Request.CreateResponse<LLEmployee>(HttpStatusCode.Created, retornoDB.Employee);
                string uri = Url.Link("DefaultApi", new { id = retornoDB.Employee.id});
                response.Headers.Location = new Uri(uri);
                return response;
            }

            return new HttpResponseMessage(new HttpStatusCode());
        }

        public void DeleteProduto(int id)
        {
            employeeDB.Delete(id);
        }
    }
}
