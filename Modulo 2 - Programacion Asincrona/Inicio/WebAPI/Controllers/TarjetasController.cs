using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("tarjetas")]
    public class TarjetasController: ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> ProcesarTarjeta([FromBody] string tarjeta)
        {
            var valorAleatorio = RandomGen.NextDouble();
            var aprobada = valorAleatorio > 0.1;
            await Task.Delay(1000);
            Console.WriteLine($"Tarjeta {tarjeta} procesada");
            return Ok(new { Tarjeta = tarjeta, Aprobada = aprobada });
        }
    }
}
