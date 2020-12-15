using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hospital.Repositories
{
    public class SchedulerRepository
    {
        private static List<Scheduler> schedulers;

        public SchedulerRepository()
        {
            var db = new RegistyHospitalEntities1();
            schedulers = db.Scheduler.ToList();
        }

        public async Task<List<Scheduler>> GetSchedulers()
        {
            var schedulerResult = new List<Scheduler>();
            using (var db = new RegistyHospitalEntities1())
            {
                schedulerResult = await Task.Run(() => schedulers);
            }
            return schedulerResult;
        }

        public async Task<Scheduler> GetScheduler(int id)
        {
            var schedulerResult = new Scheduler();
            using (var db = new RegistyHospitalEntities1())
            {
                schedulerResult = await Task.Run(() => schedulers.FirstOrDefault(f => f.Id == id));
            }
            return schedulerResult;
        }

        public async Task<Scheduler> AddScheduler(Scheduler scheduler)
        {
            Scheduler result = null;
            using (var db = new RegistyHospitalEntities1())
            {
                result = db.Scheduler.Add(scheduler);
                await db.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteScheduler(int id)
        {
            using (var db = new RegistyHospitalEntities1())
            {
                var scheduler = db.Scheduler.FirstOrDefault(f => f.Id == id);
                db.Entry(scheduler).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
        }

        public async Task<Scheduler> UpdateSchedulers(Scheduler scheduler)
        {
            using (var db = new RegistyHospitalEntities1())
            {
                db.Entry(scheduler).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return scheduler;
        }
    }
}