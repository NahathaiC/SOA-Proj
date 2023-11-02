using Northwind.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NorthWind.Controllers
{
    public class HomeController : Controller
    {
        private readonly string BaseUrl = "http://localhost:5156/";

        public List<Employee> EmployeeList { get; private set; }

        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/Employees/GetEmployees");

            if (response.IsSuccessStatusCode)
            {
                var employeeResponse = await response.Content.ReadAsStringAsync();
                EmployeeList = JsonConvert.DeserializeObject<List<Employee>>(employeeResponse);
                return View(EmployeeList);
            }
            else
            {
                // Handle the case where the API request was not successful
                // You may want to log an error, display an error page, or take some other action.
                return View("Error");
            }
        }
    }
}
