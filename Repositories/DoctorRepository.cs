using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Hospital
{
    public class DoctorRepository
    {
        private static List<Doctor> doctors;

        public DoctorRepository()
        {
            var db = new RegistyHospitalEntities1();
            doctors = db.Doctor.ToList();
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            var doctorResult = new List<Doctor>();
            using (var db = new RegistyHospitalEntities1())
            {
                doctorResult = await Task.Run(() => doctors);
            }
            return doctorResult;
        }

        public async Task<Doctor> GetDoctor(int id)
        {
            var doctorResult = new Doctor();
            using (var db = new RegistyHospitalEntities1())
            {
                doctorResult = await Task.Run(() => doctors.FirstOrDefault(f => f.Id == id));
            }
            return doctorResult;
        }

        public async Task<Doctor> AddDoctor(Doctor doctor)
        {
            Doctor result = null;
            using (var db = new RegistyHospitalEntities1())
            {
                result = db.Doctor.Add(doctor);
                await db.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteDoctor(int id)
        {
            using (var db = new RegistyHospitalEntities1())
            {
                var doctor = db.Doctor.FirstOrDefault(f => f.Id == id);
                db.Entry(doctor).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
        }

        public async Task<Doctor> UpdateDoctors(Doctor doctor)
        {
            using (var db = new RegistyHospitalEntities1())
            {
                db.Entry(doctor).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return doctor;
        }
    }
}