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
        public async Task<IActionResult> Index()
        {
            var trabajadores = await _context.Trabajadores
                .Include(t => t.Departamento)
                .Include(t => t.Provincia)
                .Include(t => t.Distrito)
                .ToListAsync();

            return View(trabajadores);
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
