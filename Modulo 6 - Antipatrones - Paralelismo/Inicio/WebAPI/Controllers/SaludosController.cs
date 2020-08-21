using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("saludos")]
    [ApiController]
    public class SaludosController : ControllerBase
    {
        [HttpGet("{nombre}")]
        public ActionResult<string> ObtenerSaludo(string nombre)
        {
            return $"Hola, {nombre}!";
        }

        [HttpGet("delay/{nombre}")]
        public async Task<ActionResult<string>> ObtenerSaludoConDelay(string nombre)
        {
            var esperar = RandomGen.NextDouble() * 10 + 1;
            //await Task.Delay((int)esperar * 1000);

            // El try no evita el colapso del web api
            //try
            //{
            //    OperacionVoidAsync();
            //}
            //catch(Exception ex)
            //{

            //}

            //OperacionTaskAsync();
            OperacionVoidSync();
            return $"Hola, {nombre}!";
        }

        // Anti-Patrón: No debemos usar async void
        private async void OperacionVoidAsync()
        {
            await Task.Delay(1);
            throw new ApplicationException();
        }

        private void OperacionVoidSync()
        {
            throw new ApplicationException();
        }

        private async Task OperacionTaskAsync()
        {
            await Task.Delay(1);
            throw new ApplicationException();
        }

        [HttpGet("adios/{nombre}")]
        public async Task<ActionResult<string>> ObtenerAdiosConDelay(string nombre)
        {
            var esperar = RandomGen.NextDouble() * 10 + 1;
            await Task.Delay((int)esperar * 1000);
            return $"Bye, {nombre}!";
        }
    }
}
