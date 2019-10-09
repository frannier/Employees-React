using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesTest.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {        
        [HttpGet("[action]")]
        public IEnumerable<Employees> GetEmployees()
        {
          
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://masglobaltestapi.azurewebsites.net/");
                //HTTP GET
                var responseTask = client.GetAsync("api/Employees");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employees[]>();
                    readTask.Wait();
                    return readTask.Result;
                }
                else
                {
                    return Enumerable.Empty<Employees>();
                }
            }
        }

        public class Employees
        {
            public int Id{ get; set; }
            public string Name{ get; set; }
            public string ContractTypeName { get; set; }
            public int RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleDescription { get; set; }
            public decimal HourlySalary { get; set; }
            public decimal MonthlySalary { get; set; }

            public decimal AnnualSalary
            {
                get
                {
                    switch (ContractTypeName)
                    {
                        case "HourlySalaryEmployee":
                            return 120 * MonthlySalary * 12;
                        case "MonthlySalaryEmployee":
                            return 12 * MonthlySalary;
                        default:
                            return 0;
                    }

                }
            }

        }




    }
}
