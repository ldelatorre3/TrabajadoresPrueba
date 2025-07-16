using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Data;
using TrabajadoresPrueba.Models;

namespace TrabajadoresPrueba.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrabajadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index(string sexo)
        {
            ViewData["SexoSeleccionado"] = sexo;

            var trabajadores = _context.Trabajadores
                .Include(t => t.Departamento)
                .Include(t => t.Provincia)
                .Include(t => t.Distrito)
                .AsQueryable();

            if (!string.IsNullOrEmpty(sexo))
                trabajadores = trabajadores.Where(t => t.Sexo == sexo);

            return View(await trabajadores.ToListAsync());
        }
        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            ViewData["Departamentos"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["Provincias"] = new SelectList(_context.Provincias, "Id", "NombreProvincia");
            ViewData["Distritos"] = new SelectList(_context.Distritos, "Id", "NombreDistrito");

            ViewData["TipoDocumento"] = new SelectList(new[]
            {
                new { Value = "DNI", Text = "DNI" },
                new { Value = "RUC", Text = "RUC" },
                new { Value = "CE",  Text = "Carné de Extranjería" }
            }, "Value", "Text");

            return View();
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null) return NotFound();

            ViewData["Departamentos"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajador.DepartamentoId);
            ViewData["Provincias"] = new SelectList(_context.Provincias, "Id", "NombreProvincia", trabajador.ProvinciaId);
            ViewData["Distritos"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajador.DistritoId);
            ViewData["TipoDocumento"] = new SelectList(new[]
            {
                new { Value = "DNI", Text = "DNI" },
                new { Value = "RUC", Text = "RUC" },
                new { Value = "CE", Text = "Carné de Extranjería" }
            }, "Value", "Text", trabajador.TipoDocumento);

            return View(trabajador);
        }

        // POST: Trabajadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trabajador trabajador)
        {
            if (id != trabajador.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Trabajadores.Any(e => e.Id == trabajador.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            // Recargar combos si falla
            ViewData["Departamentos"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajador.DepartamentoId);
            ViewData["Provincias"] = new SelectList(_context.Provincias, "Id", "NombreProvincia", trabajador.ProvinciaId);
            ViewData["Distritos"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajador.DistritoId);
            ViewData["TipoDocumento"] = new SelectList(new[]
            {
                new { Value = "DNI", Text = "DNI" },
                new { Value = "RUC", Text = "RUC" },
                new { Value = "CE", Text = "Carné de Extranjería" }
            }, "Value", "Text", trabajador.TipoDocumento);

            return View(trabajador);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var trabajador = await _context.Trabajadores
                .Include(t => t.Departamento)
                .Include(t => t.Provincia)
                .Include(t => t.Distrito)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trabajador == null) return NotFound();

            return View(trabajador);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        // POST: Trabajadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recargar combos si falla
            ViewData["Departamentos"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["Provincias"] = new SelectList(_context.Provincias, "Id", "NombreProvincia");
            ViewData["Distritos"] = new SelectList(_context.Distritos, "Id", "NombreDistrito");
            ViewData["TipoDocumento"] = new SelectList(new[]
            {
                new { Value = "DNI", Text = "DNI" },
                new { Value = "RUC", Text = "RUC" },
                new { Value = "CE",  Text = "Carné de Extranjería" }
            }, "Value", "Text");

            return View(trabajador);
        }
    }
}
