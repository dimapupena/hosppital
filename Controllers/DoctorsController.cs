using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Hospital;

namespace Hospital.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly DoctorService doctorService;

        public DoctorsController()
        {
            doctorService = new DoctorService();
        }

        // GET: Doctors
        public async Task<ActionResult> Index()
        {
            var doctors = await doctorService.GetDoctors();

            return View(doctors);
            //return View(db.Doctor.ToList());
        }

        // GET: Doctors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await doctorService.GetDoctor((int)id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DoctopName,specialty")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                doctor.Id = await doctorService.GetLastIndex() + 1;
                await doctorService.AddDoctors(doctor);
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await doctorService.GetDoctor((int)id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DoctopName,specialty")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await doctorService.UpdateDoctors(doctor);
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await doctorService.GetDoctor((int)id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await doctorService.DeletedDoctors(id);
            return RedirectToAction("Index");
        }
    }
}
