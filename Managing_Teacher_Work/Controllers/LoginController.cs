using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.DAO;
using Managing_Teacher_Work.Common;

namespace Managing_Teacher_Work.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public bool alertLogin = false;
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin
                    {
                        UserName = user.UserName,
                        ID = user.ID,
                        Name = user.Name,
                        GroupID = user.GroupID
                    };

                    // Lưu thông tin người dùng vào session
                    Session.Add(Managing_Teacher_Work.Common.CommonConstants.USER_SESSION, userSession);

                    // Điều hướng theo ID người dùng
                    if (user.GroupID == "ADMIN")
                    {
                        return RedirectToAction("Index", "Statistics"); // Điều hướng đến trang thống kê
                    }
                    return RedirectToAction("Index", "Home"); // Điều hướng mặc định
                }
                else if (result == 0)
                {
                    alertLogin = true;
                    ViewBag.alertLogin = alertLogin;
                    ViewBag.Mes = "Tài khoản không tồn tại. Vui lòng kiểm tra lại!";
                    return View("Index"); // Trả về lại view để người dùng có thể thử lại
                }
                else if (result == -1)
                {
                    alertLogin = true;
                    ViewBag.alertLogin = alertLogin;
                    ViewBag.Mes = "Tài khoản đã bị khoá!";
                    return View("Index");
                }
                else if (result == -2)
                {
                    alertLogin = true;
                    ViewBag.alertLogin = alertLogin;
                    ViewBag.Mes = "Sai mật khẩu. Vui lòng kiểm tra lại!";
                    return View("Index");
                }
                else if (result == -3)
                {
                    alertLogin = true;
                    ViewBag.alertLogin = alertLogin;
                    ViewBag.Mes = "Nhóm quyền không hợp lệ!";
                    return View("Index");
                }
                else
                {
                    alertLogin = true;
                    ViewBag.alertLogin = alertLogin;
                    ViewBag.Mes = "Đăng nhập không thành công. Vui lòng thử lại!";
                    return View("Index");
                }
            }

            return View("Index");
        }

    }
}