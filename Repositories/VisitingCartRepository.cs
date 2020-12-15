using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hospital.Repositories
{
    public class VisitingCartRepository
    {
        private static List<VisitingCart> visitingCarts;

        public VisitingCartRepository()
        {
            var db = new RegistyHospitalEntities1();
            visitingCarts = db.VisitingCart.ToList();
        }

        public async Task<List<VisitingCart>> GetVisitingCarts()
        {
            var cartResult = new List<VisitingCart>();
            using (var db = new RegistyHospitalEntities1())
            {
                cartResult = await Task.Run(() => visitingCarts);
            }
            return cartResult;
        }

        public async Task<VisitingCart> GetVisitingCart(int id)
        {
            var cartResult = new VisitingCart();
            using (var db = new RegistyHospitalEntities1())
            {
                cartResult = await Task.Run(() => visitingCarts.FirstOrDefault(f => f.Id == id));
            }
            return cartResult;
        }

        public async Task<VisitingCart> AddVisitingCart(VisitingCart visitingCart)
        {
            VisitingCart result = null;
            using (var db = new RegistyHospitalEntities1())
            {
                result = db.VisitingCart.Add(visitingCart);
                await db.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteVisitingCart(int id)
        {
            using (var db = new RegistyHospitalEntities1())
            {
                var visitingCart = db.VisitingCart.FirstOrDefault(f => f.Id == id);
                db.Entry(visitingCart).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
        }

        public async Task<VisitingCart> UpdateVisitingCarts(VisitingCart visitingCart)
        {
            using (var db = new RegistyHospitalEntities1())
            {
                db.Entry(visitingCart).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return visitingCart;
        }
    }
}