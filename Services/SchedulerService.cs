using Hospital.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hospital.Services
{
    public class SchedulerService
    {
        private readonly SchedulerRepository schedulerService;

        public SchedulerService()
        {
            schedulerService = new SchedulerRepository();
        }

        public async Task<List<Scheduler>> GetSchedulers()
        {
            return await schedulerService.GetSchedulers();
        }

        public async Task<Scheduler> GetScheduler(int id)
        {
            return await schedulerService.GetScheduler(id);
        }

        public async Task<Scheduler> AddScheduler(Scheduler scheduler)
        {
            return await schedulerService.AddScheduler(scheduler);
        }

        public async Task DeleteScheduler(int id)
        {
            await schedulerService.DeleteScheduler(id);
        }

        public async Task<Scheduler> UpdateSchedulers(Scheduler scheduler)
        {
            return await schedulerService.UpdateSchedulers(scheduler);
        }

        public async Task doFreeScheduler(Scheduler scheduler) 
        {
            scheduler.PatientId = null;
            await UpdateSchedulers(scheduler);
        }

        public async Task<List<Scheduler>> sortSchedulers(String method, List<Scheduler> schedulers = null)
        {
            var sortMethod = SortMethods.GetMethod(method);
            if( schedulers == null)
            {
                schedulers = await GetSchedulers();
            }
            switch (sortMethod)
            {
                case SortMethod.Standart:
                    return schedulers;

                case SortMethod.StandartTurnOver:
                    schedulers.Reverse();
                    return schedulers;

                case SortMethod.DateTop:
                    schedulers = schedulers.OrderByDescending(x => x.timeOfReception).ToList();
                    return schedulers;

                case SortMethod.DateDown:
                    schedulers = schedulers.OrderBy(x => x.timeOfReception).ToList();
                    return schedulers;
                
                case SortMethod.Free:
                    schedulers = schedulers.OrderBy(x => x.PatientId).ToList();
                    return schedulers;
                
                case SortMethod.Booked:
                    schedulers = schedulers.OrderByDescending(x => x.PatientId).ToList();
                    return schedulers;

                default:
                    return schedulers;
            }
        }

        public async Task<List<Scheduler>> GetDoctorSchedullers(int doctorId)
        {
            List<Scheduler> schedulers = await GetSchedulers();
            schedulers = schedulers.Where(x => x.DoctorId == doctorId).ToList();
            return schedulers;
        }

        public async Task<int> GetLastIndex()
        {
            var schedulers = await GetSchedulers();
            schedulers = schedulers.OrderByDescending(x => x.Id).ToList();
            return schedulers[0].Id;
        }

    }
}