using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private string ConnString = "Data source=DESKTOP-4HBI2PP\\SQLEXPRESS; Initial catalog=Alif; Integrated security=true;";
        [HttpGet]
        public IActionResult Read(int? id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnString))
            {
                if (id == null)
                    return View(dbConnection.Query<Person>("SELECT * FROM Person").ToList<Person>());
                else
                {
                    return View(dbConnection.Query<Person>($"SELECT * FROM Person Where(Id={id})").ToList<Person>());
                }
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Person person)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnString))
            {
                dbConnection.Execute($"INSERT INTO Person(firstName,lastName,middleName) VALUES('{person.firstName}','{person.lastName}','{person.middleName}')");
            }
            return RedirectToAction("Read");
        }
        [HttpGet]
        public IActionResult Scan()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetResultOfScan(Person person)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnString))
            {
                return View(dbConnection.Query<Person>($"SELECT * FROM Person WHERE (firstName Like '%{person.firstName}%') AND (lastName Like '%{person.lastName}%') AND (middleName Like '%{person.middleName}%')").ToList<Person>());
            }
        }
    }
}