using Login_Signup_withcrud.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Login_Signup_withcrud.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        rihanEntities2 db = new rihanEntities2();
        public ActionResult Index()
        {
            var result = db.ahmads.ToList();
            List<ahmad> e = new List<ahmad>();
            foreach (var item in result)
            {
                e.Add(new ahmad { id = item.id, Fname = item.Fname, Lname = item.Lname, age = item.age, gender = item.gender, gmail = item.gmail, password = item.password, username = item.username });
            }
            return View(e);
        }


        public ActionResult delete(int? id)
        {
            var delete = db.ahmads.Where(m => m.id == id).First();
            db.ahmads.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(ahmad e)
        {
            ahmad a = new ahmad();
            a.id = e.id;
            a.Fname = e.Fname;
            a.Lname = e.Lname;
            a.age = e.age;
            a.gender = e.gender;
            a.gmail = e.gmail;
            a.password = e.password;
            a.username = e.username;


            if (a.id == 0)
            {
                db.ahmads.Add(a);
                db.SaveChanges();
            }
            else
            {
                db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public ActionResult details(int? id)
        {
            var details = db.ahmads.Where(m => m.id == id).First();
            ahmad a = new ahmad();
            a.id = details.id;
            a.Fname = details.Fname;
            a.Lname = details.Lname;
            a.age = details.age;
            a.gender = details.gender;
           
            return View(a);
        }
        public ActionResult edit(int? id)
        {
            var edit = db.ahmads.Where(m => m.id == id).First();
            ahmad a = new ahmad();
            a.id = edit.id;
            a.Fname = edit.Fname;
            a.Lname = edit.Lname;
            a.age = edit.age;
            a.gender = edit.gender;
            a.gmail = edit.gmail;
            a.password = edit.password;
            a.username = edit.username;
            return View("create", a);
        }
        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult login(loginmodel l)
        {
            var s = db.ahmads.Where(m => m.username == l.username).FirstOrDefault();
            if (s == null)
            {
                TempData["email"] = "username not found";
            }
            else
            {
                if (s.username == l.username && s.password == l.password)
                {
                    FormsAuthentication.SetAuthCookie(s.username, false);
                    Session["login"] = s.username;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["password"] = "password is incorrect";
                }
            }
            return View();
        }
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("login");
        }
        [AllowAnonymous]
        public ActionResult create_new()
        {
            return View();
        }

    }
}