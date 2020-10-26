using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passificator.Data
{
    internal static class GuestRepository
    {
        public static IEnumerable<Guest> GetGuestList()
        {
            using (var context = new DatabaseContext())
            {
                var guests = (from a in context.Guests
                                      select a).ToList();
                return guests;
            }
        }

        public static Guest Find(int id)
        {
            var found = GetGuestList().Where(a => a.Id == id);
            return found.First();
        }

        public static int Create(Guest newGuest)
        {
            using (var context = new DatabaseContext())
            {
                var guest = context.Guests.Add(newGuest);
                context.SaveChanges();
                return guest.Id;
            }
        }

        public static void Delete(Guest guest)
        {
            using (var context = new DatabaseContext())
            {
                context.Guests.Remove(guest);
                context.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (var context = new DatabaseContext())
            {
                context.Guests.Remove(context.Guests.Find(id));
                context.SaveChanges();
            }
        }

        public static void Update(Guest guest)
        {
            using (var context = new DatabaseContext())
            {
                var entity = context.Guests.Find(guest.Id);
                entity.Name = guest.Name;
                entity.Company = guest.Company;
                entity.Document = guest.Document;
                entity.Car = guest.Car;
                context.SaveChanges();
            }
        }
    }
}
