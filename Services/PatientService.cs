using Hospital.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hospital.Services
{
    public class PatientService
    {
        private readonly PatientRepository patientRepository;

        public PatientService()
        {
            patientRepository = new PatientRepository();
        }

        public async Task<List<Patient>> GetPatients()
        {
            return await patientRepository.GetPatients();
        }

        public async Task<Patient> GetPatient(int id)
        {
            return await patientRepository.GetPatient(id);
        }

        public async Task<Patient> AddPatient(Patient patient)
        {
            return await patientRepository.AddPatient(patient);
        }

        public async Task DeletePatient(int id)
        {
            await patientRepository.DeletePatient(id);
        }

        public async Task<Patient> UpdatePatients(Patient patient)
        {
            return await patientRepository.UpdatePatients(patient);
        }

        public async Task<int> GetLastIndex()
        {
            var patients = await GetPatients();
            patients = patients.OrderByDescending(x => x.Id).ToList();
            return patients[0].Id;
        }
    }
}