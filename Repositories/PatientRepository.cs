using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Hospital.Repositories
{
    public class PatientRepository
    {
        private static List<Patient> patients;

        public PatientRepository()
        {
            var db = new RegistyHospitalEntities1();
            patients = db.Patient.ToList();
        }

        public async Task<List<Patient>> GetPatients()
        {
            var patientResult = new List<Patient>();
            using (var db = new RegistyHospitalEntities1())
            {
                patientResult = await Task.Run(() => patients);
            }
            return patientResult;
        }

        public async Task<Patient> GetPatient(int id)
        {
            var patientResult = new Patient();
            using (var db = new RegistyHospitalEntities1())
            {
                patientResult = await Task.Run(() => patients.FirstOrDefault(f => f.Id == id));
            }
            return patientResult;
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            Patient result = null;
            using (var db = new RegistyHospitalEntities1())
            {
                result = db.Patient.Add(patient);
                await db.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeletePatient(int id)
        {
            using (var db = new RegistyHospitalEntities1())
            {
                var patient = db.Patient.FirstOrDefault(f => f.Id == id);
                db.Entry(patient).State = EntityState.Deleted;
                await db.SaveChangesAsync();
            }
        }

        public async Task<Patient> UpdatePatients(Patient patient)
        {
            using (var db = new RegistyHospitalEntities1())
            {
                db.Entry(patient).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return patient;
        }
    }
}