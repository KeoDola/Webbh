using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.DAO
{
    public class UserGroupDAO
    {
        ApplicationDbContext db = null;
        private static UserGroupDAO instance;
        static object key = new object();
        public static UserGroupDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new UserGroupDAO();
                    }
                }
                return instance;
            }
        }
        public UserGroupDAO()
        {
            db = new ApplicationDbContext();
        }

        public long Insert(UserGroup entity)
        {
            db.UserGroups.Add(entity);
            db.SaveChanges();
            return entity.UserGroupID;
        }
        public bool Update(UserGroup entity)
        {
            try
            {
                var userGroup = db.UserGroups.Find(entity.UserGroupID);
                userGroup.GroupName = entity.GroupName;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var userGroup = db.UserGroups.Find(id);
                db.UserGroups.Remove(userGroup);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserGroup ViewDetail(int userGroupID)
        {
            return db.UserGroups.Find(userGroupID);
        }
        public IEnumerable<UserGroup> ListAllpaging(string searchString, int page, int pageSize)
        {
            IQueryable<UserGroup> model = db.UserGroups;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.GroupName.Contains(searchString));
            }
            return model.OrderBy(x => x.UserGroupID).ToPagedList(page, pageSize);
        }
        public List<UserGroup> ListAll()
        {
            return db.UserGroups.ToList();
        }
    }
}