using Managing_Teacher_Work.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.DAO
{
    public class UserDao
    {
        MTWDbContext db = null;

        public UserDao()
        {
            db = new MTWDbContext();
        }
        public IEnumerable<User> Listpg(int page, int pageSize)
        {
            return db.User.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public List<User> ListAll()
        {
            return db.User.ToList();
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.User.Find(id);
                db.User.Remove(user);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }
        public User GetById(string userName)
        {
            return db.User.SingleOrDefault(x => x.UserName == userName);
        }
        public List<string> GetListCredential(string userName)
        {
            return null;

        }
        public User Get(string username)
        {
            return db.User.SingleOrDefault(x => x.UserName == username);
        }
        public int Login(string username, string password)
        {
            var result = db.User.SingleOrDefault(x => x.UserName == username);

            if (result == null)
            {
                return 0; // Tài khoản không tồn tại
            }

            if (result.Status == false)
            {
                return -1; // Tài khoản bị khóa
            }

            if (result.Password == password)
            {
                var group = db.GroupUser.SingleOrDefault(g => g.ID == result.GroupID);
                if (group != null)
                {
                    return 1; // Đăng nhập thành công
                }
                return -3; // Nhóm quyền không hợp lệ
            }

            return -2; // Sai mật khẩu
        }

    }
}