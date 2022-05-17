using Clientes.Data;
using Clientes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clientes.ViewModel;

namespace Clientes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly ClienteDbContext _context;

        public ClienteController(ClienteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            //var clientes = await _context.Clientes.ToListAsync();
            //return Ok(clientes);
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
            return Ok(listaClientesVM);
            
        }

        [HttpGet]
        [Route("{clientId:int}")]
        [ActionName("GetCliente")]
        public async Task<IActionResult> GetCliente([FromRoute] int clientId)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == clientId);
            if (cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound("Cliente não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> NovoCliente([FromBody] Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetCliente), cliente.ClienteId, cliente);
            return Ok("Cliente cadastrado com sucesso");
        }

        [HttpPut]
        [Route("{clientId:int}")]
        public async Task<IActionResult> AtualizarCliente([FromRoute] int clientId, [FromBody]Cliente cliente)
        {
            var existeCliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == clientId);

            if(existeCliente != null)
            {
                existeCliente.MunicipioId = cliente.MunicipioId;
                existeCliente.RazaoSocial = cliente.RazaoSocial;
                existeCliente.EstadoId = cliente.EstadoId;
                existeCliente.CNPJ = cliente.CNPJ;
                existeCliente.Porte = cliente.Porte;

                await _context.SaveChangesAsync();

            }

            return Ok(existeCliente);
        }

        [HttpDelete]
        [Route("{clienteId:int}")]
        public async Task<IActionResult> ExcluirCliente(int clienteId)
        {
            var existeCliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == clienteId);

            if (existeCliente != null)
            {
                _context.Remove(existeCliente);
                await _context.SaveChangesAsync();
            }

            return Ok(existeCliente);
        }
    }


}
