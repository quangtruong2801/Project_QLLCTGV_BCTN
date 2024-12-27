using Managing_Teacher_Work.DAO;
using Managing_Teacher_Work.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Managing_Teacher_Work.Controllers
{
    public class GroupUserController : BaseController
    {
        MTWDbContext db = new MTWDbContext();
        public bool isThemMoi;

        // GET: GroupUser
        public ActionResult Index()
        {
            List<GroupUser> listGroupUser = db.GroupUser.ToList();
            ViewBag.listGroupUser = listGroupUser;
            return View();
        }

        public ActionResult Add(GroupUser model, string submit)
        {
            if (submit == "Thêm")
            {
                isThemMoi = true;
                if (model != null)
                {
                    model.Name_GroupUser = model.Name_GroupUser.ToString().Trim();
                    model.CodeRole = model.CodeRole.ToString().Trim();
                    db.GroupUser.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else if (submit == "Cập Nhật")
            {
                isThemMoi = false;
                if (model != null)
                {
                    // Sử dụng string thay vì int cho ID
                    var groupUser = db.GroupUser.SingleOrDefault(x => x.ID == model.ID);
                    groupUser.Name_GroupUser = model.Name_GroupUser.ToString().Trim();
                    groupUser.CodeRole = model.CodeRole.ToString().Trim();
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // Xóa nhóm quyền theo ID kiểu string
        public ActionResult Delete(string id)
        {
            GroupUser groupUser = db.GroupUser.Find(id);
            if (groupUser != null)
            {
                db.GroupUser.Remove(groupUser);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Lấy dữ liệu nhóm quyền theo ID kiểu string
        public ActionResult getList(string id)
        {
            var groupUser = db.GroupUser.SingleOrDefault(x => x.ID == id);
            var json = JsonConvert.SerializeObject(groupUser);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
