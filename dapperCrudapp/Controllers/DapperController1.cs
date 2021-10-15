using Dapper;
using dapperCrudapp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace dapperCrudapp.Controllers
{
    public class DapperController1 : Controller
    {
      
        string Connection = "Data Source = LAPTOP-Q6S7B3KA; Initial Catalog = DapperCRUD; Integrated Security = true";

        public IActionResult Index()
        {
            return View();
        }


        public JsonResult Create(login m)
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                string myCommand = "Insert into Login(Email, Password) values('" + m.Email + "','" + m.Password + "');";
                connection.Execute(myCommand);
            }
            return Json("");
        }


        public JsonResult Update(login m)
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                string myCommand = "Update Login set Email = '" + m.Email + "', Password = '" + m.Password + "');";
                connection.Execute(myCommand);
            }
            return Json("");
        }

        public JsonResult Read()
        {
            List<login> Temp = new List<login>();
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                var Params = new DynamicParameters();
                Params.Add("@Flag", "Read");
                Temp = connection.Query<login>("LoginProcedure", Params, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
            return Json(Temp);
        }

        public JsonResult Delete()
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                var Params = new DynamicParameters();
                Params.Add("@Flag", "Delete");
               connection.Execute("LoginProcedure", Params, commandType: System.Data.CommandType.StoredProcedure);
            }
            return Json("");
        }
    }
}
