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

        public IActionResult UpdateBug(string BugId, string DateTimeCreated, string BugComment, string Author)
        {
            string SqlString = "INSERT INTO BugTrackerDatabase.dbo.BugComments (BugId, DateTimeCreated, BugComment, Author) " +
                               "VALUES (@BugId, @DateTimeCreated, @BugComment, @Author);";

            Utilities.Command = new System.Data.SqlClient.SqlCommand(SqlString, Utilities.Connection);
            Utilities.Command.Parameters.AddWithValue("@BugId", Int32.Parse(BugId));
            Utilities.Command.Parameters.AddWithValue("@DateTimeCreated", DateTime.Parse(DateTimeCreated));
            Utilities.Command.Parameters.AddWithValue("@BugComment", BugComment);
            Utilities.Command.Parameters.AddWithValue("Author", Author);
            Utilities.Connection.Open();

            Utilities.Command.ExecuteNonQuery();

            Utilities.Connection.Close();

            return GetBug(Int32.Parse(BugId));
        }

        

        public IActionResult CreateBug()
        {
            return View("CreateNewBug");
        }

        public IActionResult GetBug(int BugIdParameter)
        {
            string BugId, BugDescription, CreatedBy, UpdatedBy, Comment, Author;
            DateTime BugCommentDate;


            string SqlString = "SELECT * FROM BugTrackerDatabase.dbo.BugTable " +
                               "WHERE BugId = '" + BugIdParameter + "';";

            Utilities.Command = new System.Data.SqlClient.SqlCommand(SqlString, Utilities.Connection);

            Utilities.Connection.Open();

            using (Utilities.Reader = Utilities.Command.ExecuteReader())
            {
                Utilities.Reader.Read();
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
            }

            BugModel Bug = new BugModel { BugId = BugId, BugDescription = BugDescription, CreatedBy = CreatedBy, UpdatedBy = UpdatedBy };
            ViewBag.Bug = Bug;


            SqlString = "SELECT DateTimeCreated, BugComment, Author FROM BugTrackerDatabase.dbo.BugComments " +
                        "WHERE BugId = " + Bug.BugId + " ORDER BY DateTimeCreated ASC;";

            List<BugCommentModel> ListOfBugComments = new List<BugCommentModel>();

            Utilities.Command = new System.Data.SqlClient.SqlCommand(SqlString, Utilities.Connection);

            using (Utilities.Reader = Utilities.Command.ExecuteReader())
            {
                while (Utilities.Reader.Read())
                {
                    if (!Utilities.Reader.IsDBNull(0))
                    {
                        BugCommentDate = Utilities.Reader.GetDateTime(0);
                    }
                    else
                    {
                        BugCommentDate = default;
                    }

                    if (!Utilities.Reader.IsDBNull(1))
                    {
                        Comment = Utilities.Reader.GetString(1);
                    }
                    else
                    {
                        Comment = null;
                    }

                    if (!Utilities.Reader.IsDBNull(2))
                    {
                        Author = Utilities.Reader.GetString(2);
                    }
                    else
                    {
                        Author = null;
                    }

                    ListOfBugComments.Add(new BugCommentModel { BugId = Bug.BugId, Author = Author, BugComment = Comment, BugCommentDate = BugCommentDate });
                }
            }

            ViewBag.ListOfBugComments = ListOfBugComments;

            Utilities.Connection.Close();

            return View("UpdateBug");
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

            string SqlString = "SELECT * from BugTrackerDatabase.dbo.BugTable;"; 

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
            Utilities.Connection.Close();
            return View(BugsList);
        }
    }
}
