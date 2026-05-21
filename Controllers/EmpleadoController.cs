using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PWS26ApiServer.Models;
using PWS26ApiBiblioteca;
using Microsoft.EntityFrameworkCore;

namespace PWS26ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly BdPws26Context _dbContext;

        public EmpleadoController(BdPws26Context dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EmpleadoDTO>>();
            var listaEmpleadoDTO = new List<EmpleadoDTO>();

            try
            {
                foreach (var item in await _dbContext.TbEmpleados.Include(e => e.IdDepartamentoNavigation).ToListAsync())
                {
                    listaEmpleadoDTO.Add(new EmpleadoDTO
                    {
                        IdDepartamento = item.IdDepartamento,
                        FechaContrato = item.FechaContrato,
                        IdEmpleado = item.IdEmpleado,
                        NombreCompleto = item.NombreCompleto,
                        Sueldo = item.Sueldo,
                    });
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<EmpleadoDTO>();
            var empleadoDTO = new EmpleadoDTO();

            try
            {
                var dbEmpleado = await _dbContext.TbEmpleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);
                if (dbEmpleado != null)
                {
                    empleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
                    empleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
                    empleadoDTO.Sueldo = dbEmpleado.Sueldo;
                    empleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
                    empleadoDTO.FechaContrato = dbEmpleado.FechaContrato;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = empleadoDTO;
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEmpleado = new TbEmpleado
                {
                    NombreCompleto = empleado.NombreCompleto,
                    FechaContrato = empleado.FechaContrato,
                    IdDepartamento = empleado.IdDepartamento,
                    Sueldo = empleado.Sueldo,
                };

                _dbContext.TbEmpleados.Add(dbEmpleado);
                await _dbContext.SaveChangesAsync();
                if (responseApi.EsCorrecto)
                {
                    responseApi.EsCorrecto = true;
                    //responseApi.Valor = ;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(EmpleadoDTO empleado, int id)
        {
            var responseApi = new ResponseAPI<EmpleadoDTO>();
            var empleadoDTO = new EmpleadoDTO();

            try
            {
                var dbEmpleado = await _dbContext.TbEmpleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);
                if (dbEmpleado != null)
                {
                    empleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
                    empleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
                    empleadoDTO.Sueldo = dbEmpleado.Sueldo;
                    empleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
                    empleadoDTO.FechaContrato = dbEmpleado.FechaContrato;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = empleadoDTO;
                }
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
