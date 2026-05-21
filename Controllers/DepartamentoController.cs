using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PWS26ApiServer.Models;
using PWS26ApiBiblioteca;
using Microsoft.EntityFrameworkCore;

namespace PWS26ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly BdPws26Context _dbContext;

        public DepartamentoController(BdPws26Context dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<DepartamentoDTO>>();
            var listaDepartamentoDTO = new List<DepartamentoDTO>();
            try
            {
                foreach (var item in await _dbContext.TbEmpleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
                    listaDepartamentoDTO.Add(new DepartamentoDTO
                    {
                        idDepartamento = item.IdDepartamento,
                        Nombre = item.NombreCompleto,
                    });
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaDepartamentoDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

    }
}
