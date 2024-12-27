using System.Linq;
using System.Web.Mvc;
using Managing_Teacher_Work.Models;

namespace Managing_Teacher_Work.Controllers
{
    public class StatisticsController : Controller
    {
        private MTWDbContext db = new MTWDbContext();

        public ActionResult Index()
        {
            // Đếm số lượng các mục
            var userCount = db.User.Count();       // Đếm số lượng User
            var studentCount = db.Student.Count(); // Đếm số lượng Student
            var teacherCount = db.Teacher.Count(); // Đếm số lượng Teacher
            var classCount = db.Class.Count();     // Đếm số lượng Class
            var scienceCount = db.Science.Count(); // Đếm số lượng Science

            // Tạo ViewModel hoặc dùng ViewBag để truyền dữ liệu ra View
            ViewBag.UserCount = userCount;
            ViewBag.StudentCount = studentCount;
            ViewBag.TeacherCount = teacherCount;
            ViewBag.ClassCount = classCount;
            ViewBag.ScienceCount = scienceCount;

            return View();
        }
    }
}
