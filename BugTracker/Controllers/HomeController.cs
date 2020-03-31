using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateBug()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Bug()
        {
            var BugsList = new List<BugModel>();

            string SqlString = "SELECT * from BugTrackerDatabase.dbo.BugTable; ";

            string BugId, BugDescription, CreatedBy, UpdatedBy;


            Utilities.Connection.Open();

            Utilities.Command = new System.Data.SqlClient.SqlCommand(SqlString, Utilities.Connection);

            using (Utilities.Reader = Utilities.Command.ExecuteReader())
            {
                while (Utilities.Reader.Read())
                {
                    if (!Utilities.Reader.IsDBNull(0))
                    {
                        BugId = Utilities.Reader.GetInt32(0).ToString();
                    }
                    else
                    {
                        BugId = null;
                    }

                    if (!Utilities.Reader.IsDBNull(1))
                    {
                        BugDescription = Utilities.Reader.GetString(1);
                    }
                    else
                    {
                        BugDescription = null;
                    }

                    if (!Utilities.Reader.IsDBNull(2))
                    {
                        CreatedBy = Utilities.Reader.GetString(2);
                    }
                    else
                    {
                        CreatedBy = null;
                    }

                    if (!Utilities.Reader.IsDBNull(3))
                    {
                        UpdatedBy = Utilities.Reader.GetString(3);
                    }
                    else
                    {
                        UpdatedBy = null;
                    }

                    BugsList.Add(new BugModel { BugId = BugId, BugDescription = BugDescription, CreatedBy = CreatedBy, UpdatedBy = UpdatedBy });
                }
            }
            return View(BugsList);
        }
    }
}
