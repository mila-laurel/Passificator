﻿using System.Collections.Generic;
using Passificator.Model;
using System.Linq;

namespace Passificator.Data
{
    internal static class StaffRepository
    {
        public static IEnumerable<Staff> GetStaffList()
        {
            using var context = new DatabaseContext();
            var administrators = (from a in context.Administrators
                select a).ToList();
            return administrators;
        }

        public static Staff Find(int id)
        {
            var found = GetStaffList().Where(a => a.Id == id);
            return found.First();
        }

        public static void Create(Staff newStaff)
        {
            using var context = new DatabaseContext();
            context.Administrators.Add(newStaff);
            context.SaveChanges();
        }

        public static void Delete(Staff staff)
        {
            using var context = new DatabaseContext();
            context.Administrators.Remove(staff);
            context.SaveChanges();
        }

       public static void Delete(int id)
       {
           using var context = new DatabaseContext();
           context.Administrators.Remove(context.Administrators.Find(id));
           context.SaveChanges();
       }

        public static void Update(Staff staff)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.Administrators.Find(staff.Id);
                entity.Name = staff.Name;
                entity.Position = staff.Position;
                entity.Department = staff.Department;
                context.SaveChanges();
            }
        }
    }
}
