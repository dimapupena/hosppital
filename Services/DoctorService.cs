using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Hospital
{
    public class DoctorService
    {
        private readonly DoctorRepository doctorRepository;

        public DoctorService()
        {
            doctorRepository = new DoctorRepository();
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            return await doctorRepository.GetDoctors();
        }

        public async Task<Doctor> GetDoctor(int id)
        {
            return await doctorRepository.GetDoctor(id);
        }

        public async Task<Doctor> AddDoctors(Doctor doctor)
        {
            return await doctorRepository.AddDoctor(doctor);
        }

        public async Task DeletedDoctors(int id)
        {
            await doctorRepository.DeleteDoctor(id);
        }

        public async Task<Doctor> UpdateDoctors(Doctor doctor)
        {
            return await doctorRepository.UpdateDoctors(doctor);
        }

        public async Task<int> GetLastIndex()
        {
            var doctors = await GetDoctors();
            doctors = doctors.OrderByDescending(x => x.Id).ToList();
            return doctors[0].Id;
        }
    }
}