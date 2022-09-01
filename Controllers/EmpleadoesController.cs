using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AM180695.Models;

namespace AM180695.Controllers
{
    public class EmpleadoesController : Controller
    {
        private EmpleadoDBContex db = new EmpleadoDBContex();

        // GET: Empleadoes
        public ActionResult Index( string buscarApellidos, string allCargos, string sueldo)
        {
            decimal sueldoBase;
            var CargoLst = new List<string>();
            var CargoQry = from d in db.Empleados
                            orderby d.Cargo
                            select d.Cargo;

            CargoLst.AddRange(CargoQry.Distinct());

            ViewBag.allCargos = new SelectList(CargoLst);
            var empleados = from d in db.Empleados
                            select d;




            if (!string.IsNullOrEmpty(buscarApellidos))
            {
                empleados = empleados.Where(x => x.Apellidos.Contains(buscarApellidos));
            }
          
            if (!string.IsNullOrEmpty(allCargos))
            {
                empleados = empleados.Where(c => c.Cargo == allCargos);
            }

        

            if (Decimal.TryParse(sueldo,out sueldoBase))
            {
                empleados = empleados.Where(x => (x.SueldoBase-sueldoBase)<= 5 && (x.SueldoBase - sueldoBase) >= -5);
            }

            return View(empleados);

        }
     

        // GET: Empleadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,Apellidos,FechaNacimiento,Direccion,Telefono,Cargo,SueldoBase")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,Apellidos,FechaNacimiento,Direccion,Telefono,Cargo,SueldoBase")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleados.Find(id);
            db.Empleados.Remove(empleado);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
