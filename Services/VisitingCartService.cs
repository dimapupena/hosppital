using Hospital.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hospital.Services
{
    public class VisitingCartService
    {
        private readonly VisitingCartRepository visitingCartRepository;

        public VisitingCartService()
        {
            visitingCartRepository = new VisitingCartRepository();
        }

        public async Task<List<VisitingCart>> GetVisitingCarts()
        {
            return await visitingCartRepository.GetVisitingCarts();
        }

        public async Task<VisitingCart> GetVisitingCart(int id)
        {
            return await visitingCartRepository.GetVisitingCart(id);
        }

        public async Task<VisitingCart> AddVisitingCart(VisitingCart visitingCart)
        {
            return await visitingCartRepository.AddVisitingCart(visitingCart);
        }

        public async Task CreateVisitingCart(String result, Scheduler scheduler)
        {
            var newVisitingCard = new VisitingCart();
            newVisitingCard.Id = await GetLastIndex() + 1;
            newVisitingCard.DoctorId = scheduler.DoctorId;
            newVisitingCard.ParientId = (int)scheduler.PatientId;
            newVisitingCard.result = result;
            await AddVisitingCart(newVisitingCard);
        }

        public async Task DeleteVisitingCart(int id)
        {
            await visitingCartRepository.DeleteVisitingCart(id);
        }

        public async Task<VisitingCart> UpdateVisitingCarts(VisitingCart visitingCart)
        {
            return await visitingCartRepository.UpdateVisitingCarts(visitingCart);
        }

        public async Task<List<VisitingCart>> getPatientVivitingCarts(int patientId)
        {
            List<VisitingCart> visitingCarts = await GetVisitingCarts();
            visitingCarts = visitingCarts.Where(x => x.ParientId == patientId).ToList();
            return visitingCarts;
        }

        public async Task<int> GetLastIndex()
        {
            var carts = await GetVisitingCarts();
            carts = carts.OrderByDescending(x => x.Id).ToList();
            return carts[0].Id;
        }
    }
}