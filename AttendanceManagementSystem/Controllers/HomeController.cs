using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        AttendanceManagementSystemEntities db = new AttendanceManagementSystemEntities();
        public ActionResult Index()
        {
            var students = db.Students.ToList();
            ViewBag.students = students;
            return View();
        }
        [HttpPost]
        public ActionResult Index(int[] markedStudentId, DateTime attendanceDate)
        {

            int[] markstd = markedStudentId;
            var checkdate = db.Attendances.Where(c => c.Date == attendanceDate).FirstOrDefault();
            if (checkdate == null)
            {
                foreach (var id in markstd)
                {
                    Attendance attendance = new Attendance();
                    attendance.StudentId = id;
                    attendance.Date = attendanceDate;
                    db.Attendances.Add(attendance);
                    db.SaveChanges();
                    TempData["message"] = "Attendance has been marked for " + attendanceDate.ToString("d");
                }
            }
            else
            {
                TempData["message"] = "Attendance has already been marked for " + attendanceDate.ToString("d");
            }
            
            var students = db.Students.ToList();
            ViewBag.students = students;

            return View();
        }
    }
}