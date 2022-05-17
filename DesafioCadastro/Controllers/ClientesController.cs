using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesafioCadastro.Data;
using DesafioCadastro.Models;
using DesafioCadastro.ViewModel;

namespace DesafioCadastro.Controllers
{
    public class ClientesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {

            List<ClienteViewModel> listaClientesVM = new List<ClienteViewModel>();
            
            var listaClientes = (from Cli in _context.Clientes
                                 join Est in _context.Estados on Cli.EstadoId equals Est.EstadoId
                                 join Mun in _context.Municipios on Cli.MunicipioId equals Mun.MunicipioId
                                 
                                 select new
                                 {
                                     Cli.ClienteId,
                                     Cli.CNPJ,
                                     Cli.RazaoSocial,
                                     Cli.Porte,
                                     Est.NomeEstado,
                                     Mun.NomeMunicipio
                                 }).ToList();
            
            foreach (var item in listaClientes)
            {
                ClienteViewModel cliVM = new ClienteViewModel(); //ViewModel
                cliVM.ClienteId = item.ClienteId;
                cliVM.CNPJ = item.CNPJ;
                cliVM.RazaoSocial = item.RazaoSocial;
                cliVM.Porte = item.Porte;
                cliVM.Estado = item.NomeEstado;
                cliVM.Municipio = item.NomeMunicipio;
                listaClientesVM.Add(cliVM);
            }
            
            return View(listaClientesVM);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }


        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewBag.ListaEstados = _context.Estados.ToList();
            return View();
        }

        public JsonResult GetMunicipios(int estadoId)//GetSubCategoryByCategoryId
        {
            var data = _context.Municipios.Where(m => m.EstadId == estadoId).ToList();
            return Json(data);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,CNPJ,RazaoSocial,Porte,EstadoId,MunicipioId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ListaEstados = _context.Estados.ToList();
            
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,CNPJ,RazaoSocial,Porte,EstadoId,MunicipioId")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("A tabela informada 'ApplicationDbContext.Clientes'  é nula.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.ClienteId == id)).GetValueOrDefault();
        }
    }
}
