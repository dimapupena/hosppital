using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Hospital;
using Hospital.Services;

namespace Hospital.Controllers
{
    public class ApoitmentController : Controller
    {
        private readonly SchedulerService shedulerService;
        private readonly PatientService patientService;
        private readonly VisitingCartService visitingCartService;
        private readonly DoctorService doctorService;
        // GET: apoitment

        public ApoitmentController()
        {
            shedulerService = new SchedulerService();
            patientService = new PatientService();
            visitingCartService = new VisitingCartService();
            doctorService = new DoctorService();
        }

        public async Task<ActionResult> Index()
        {
            var scheduler = await shedulerService.GetSchedulers();
            ViewBag.patients = await patientService.GetPatients();
            ViewBag.doctors = await doctorService.GetDoctors();
            return View(scheduler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(String sortMethod = null, int? DoctorId = null)
        {
            List<Scheduler> scheduler;
            if ( DoctorId != null && DoctorId != -1 )
            {
                scheduler = await shedulerService.GetDoctorSchedullers((int)DoctorId);
                var doctor = await doctorService.GetDoctor((int)DoctorId);
                ViewBag.doctorName = doctor.DoctopName;
            } else
            {
                scheduler = await shedulerService.GetSchedulers();
            }
            scheduler = await shedulerService.sortSchedulers(sortMethod, scheduler);
            ViewBag.current = sortMethod;
            ViewBag.doctors = await doctorService.GetDoctors();
            return View(scheduler);
        }

        // GET: apoitment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheduler scheduler = await shedulerService.GetScheduler((int)id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            return View(scheduler);
        }

        // GET: apoitment/Create
        public async Task<ActionResult> Create()
        {
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName");
            ViewBag.PatientId = new SelectList(patients, "Id", "PatienName");
            return View();
        }

        // POST: apoitment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,timeOfReception,PatientId,DoctorId")] Scheduler scheduler)
        {
            if (ModelState.IsValid)
            {
                scheduler.Id = await shedulerService.GetLastIndex() + 1;
                await shedulerService.AddScheduler(scheduler);
                return RedirectToAction("Index");
            }
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName", scheduler.DoctorId);
            ViewBag.PatientId = new SelectList(patients, "Id", "PatienName", scheduler.PatientId);
            return View(scheduler);
        }

        public async Task<ActionResult> SetPatient(int? schedullerId)
        {
            if (schedullerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheduler scheduler = await shedulerService.GetScheduler((int)schedullerId);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.PatientId = new SelectList(patients, "Id", "PatienName", scheduler.PatientId);
            return View(scheduler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPatient([Bind(Include = "Id,timeOfReception,PatientId,DoctorId")] Scheduler scheduler)
        {
            if (ModelState.IsValid)
            {
                await shedulerService.UpdateSchedulers(scheduler);
                return RedirectToAction("Index");
            }
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName", scheduler.DoctorId);
            ViewBag.PatientId = new SelectList(patients, "Id", "PatienName", scheduler.PatientId);
            return View(scheduler);
        }

        // GET: apoitment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheduler scheduler = await shedulerService.GetScheduler((int)id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName", scheduler.DoctorId);
            ViewBag.PatientId = new SelectList(patients, "Id", "PatienName", scheduler.PatientId);
            return View(scheduler);
        }

        // POST: apoitment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,timeOfReception,PatientId,DoctorId")] Scheduler scheduler, string doFree)
        {
            if (doFree != "on")
            {
                if (ModelState.IsValid)
                {
                    await shedulerService.UpdateSchedulers(scheduler);
                    return RedirectToAction("Index");
                }
            } else
            {
                await shedulerService.doFreeScheduler(scheduler);
                return RedirectToAction("Index");
            }
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName", scheduler.DoctorId);
            ViewBag.PatientId = new SelectList(patients, "Id", "PatienName", scheduler.PatientId);
            return View(scheduler);
        }

        // GET: apoitment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scheduler scheduler = await shedulerService.GetScheduler((int)id);
            if (scheduler == null)
            {
                return HttpNotFound();
            }
            return View(scheduler);
        }

        // POST: apoitment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await shedulerService.DeleteScheduler(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> WriteResult(int? id)
        {
            Scheduler scheduler = await shedulerService.GetScheduler((int)id);
            if (scheduler.PatientId != null)
            {
                Patient patient = await patientService.GetPatient((int)scheduler.PatientId);
                ViewBag.patientName = patient.PatienName;
            }
            return View(scheduler);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> WriteResult([Bind(Include = "Id,timeOfReception,PatientId,DoctorId")] Scheduler scheduler, string result)
        {
            var sched = await shedulerService.GetScheduler(scheduler.Id);
            await visitingCartService.CreateVisitingCart(result, sched);
            await shedulerService.DeleteScheduler(scheduler.Id);
            return RedirectToAction("Index");
        }
    }
}
