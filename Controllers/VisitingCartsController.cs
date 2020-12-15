using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hospital;
using Hospital.Services;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    public class VisitingCartsController : Controller
    {
        private readonly VisitingCartService visitingCartService;
        private readonly PatientService patientService;
        private readonly DoctorService doctorService;

        public VisitingCartsController() {
            visitingCartService = new VisitingCartService();
            patientService = new PatientService();
            doctorService = new DoctorService();
        }
        // GET: VisitingCarts
        public async Task<ActionResult> Index()
        {
            var visitingCart = await visitingCartService.GetVisitingCarts();
            ViewBag.patients = await patientService.GetPatients();
            return View(visitingCart);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(int? patientId = null)
        {
            List<VisitingCart> visitingCarts;
            if (patientId != null)
            {
                visitingCarts = await visitingCartService.getPatientVivitingCarts((int)patientId);
            }
            else
            {
                visitingCarts = await visitingCartService.GetVisitingCarts();
            }
            ViewBag.patients = await patientService.GetPatients();
            return View(visitingCarts);
        }

        // GET: VisitingCarts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitingCart visitingCart = await visitingCartService.GetVisitingCart((int) id);
            if (visitingCart == null)
            {
                return HttpNotFound();
            }
            return View(visitingCart);
        }

        // GET: VisitingCarts/Create
        public async Task<ActionResult> Create()
        {
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName");
            ViewBag.ParientId = new SelectList(patients, "Id", "PatienName");
            return View();
        }

        // POST: VisitingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DoctorId,ParientId,result")] VisitingCart visitingCart)
        {
            if (ModelState.IsValid)
            {
                visitingCart.Id = await visitingCartService.GetLastIndex() + 1;
                await visitingCartService.AddVisitingCart(visitingCart);
                return RedirectToAction("Index");
            }
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName", visitingCart.DoctorId);
            ViewBag.ParientId = new SelectList(patients, "Id", "PatienName", visitingCart.ParientId);
            return View(visitingCart);
        }

        // GET: VisitingCarts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitingCart visitingCart = await visitingCartService.GetVisitingCart((int)id);
            if (visitingCart == null)
            {
                return HttpNotFound();
            }
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName", visitingCart.DoctorId);
            ViewBag.ParientId = new SelectList(patients, "Id", "PatienName", visitingCart.ParientId);
            return View(visitingCart);
        }

        // POST: VisitingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DoctorId,ParientId,result")] VisitingCart visitingCart)
        {
            if (ModelState.IsValid)
            {
                await visitingCartService.UpdateVisitingCarts(visitingCart);
                return RedirectToAction("Index");
            }
            List<Doctor> doctors = await doctorService.GetDoctors();
            List<Patient> patients = await patientService.GetPatients();
            ViewBag.DoctorId = new SelectList(doctors, "Id", "DoctopName", visitingCart.DoctorId);
            ViewBag.ParientId = new SelectList(patients, "Id", "PatienName", visitingCart.ParientId);
            return View(visitingCart);
        }

        // GET: VisitingCarts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitingCart visitingCart = await visitingCartService.GetVisitingCart((int)id);
            if (visitingCart == null)
            {
                return HttpNotFound();
            }
            return View(visitingCart);
        }

        // POST: VisitingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await visitingCartService.DeleteVisitingCart(id);
            return RedirectToAction("Index");
        }
    }
}
