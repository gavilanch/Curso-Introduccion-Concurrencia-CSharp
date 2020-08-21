using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_2
{
    public class Solo_Uno
    {

        private readonly string apiURL;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly HttpClient httpClient;

        public Solo_Uno(string apiURL, CancellationTokenSource cancellationTokenSource)
        {
            this.apiURL = apiURL;
            this.cancellationTokenSource = cancellationTokenSource;
            httpClient = new HttpClient();
        }

        public async Task btnIniciar_Click(PictureBox loadingGIF)
        {
            loadingGIF.Visible = true;

            var token = cancellationTokenSource.Token;
            var nombres = new string[] { "Felipe", "Claudia", "Antonio", "Alexandria" };

            // Ejemplo 1
            //var tareasHTTP = nombres.Select(x => ObtenerSaludo(x, token));
            //var tarea = await Task.WhenAny(tareasHTTP);
            //var contenido = await tarea;
            //Console.WriteLine(contenido.ToUpper());
            //cancellationTokenSource.Cancel();

            // Ejemplo 2
            //var tareasHTTP = nombres.Select(x =>
            //{
            //    Func<CancellationToken, Task<string>> funcion = (ct) => ObtenerSaludo(x, ct);
            //    return funcion;
            //});

            //var contenido = await EjecutarUno(tareasHTTP);
            //Console.WriteLine(contenido.ToUpper());

            // Ejemplo 3
            var contenido = await EjecutarUno(
                (ct) => ObtenerSaludo("Felipe", ct),
                (ct) => ObtenerAdios("Felipe", ct));

            Console.WriteLine(contenido.ToUpper());

            loadingGIF.Visible = false;
        }

        private async Task<T> EjecutarUno<T>(IEnumerable<Func<CancellationToken, Task<T>>> funciones)
        {
            var cts = new CancellationTokenSource();
            var tareas = funciones.Select(funcion => funcion(cts.Token));
            var tarea = await Task.WhenAny(tareas);
            cts.Cancel();
            return await tarea;
        }

        private async Task<T> EjecutarUno<T>(params Func<CancellationToken, Task<T>>[] funciones)
        {
            var cts = new CancellationTokenSource();
            var tareas = funciones.Select(funcion => funcion(cts.Token));
            var tarea = await Task.WhenAny(tareas);
            cts.Cancel();
            return await tarea;
        }

        private async Task<string> ObtenerSaludo(string nombre, CancellationToken cancellationToken)
        {
            using (var respuesta = await httpClient.GetAsync($"{apiURL}/saludos/delay/{nombre}", cancellationToken))
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine(contenido);
                return contenido;
            }

        }

        private async Task<string> ObtenerAdios(string nombre, CancellationToken cancellationToken)
        {
            using (var respuesta = await httpClient.GetAsync($"{apiURL}/saludos/adios/{nombre}", cancellationToken))
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine(contenido);
                return contenido;
            }

        }

    }
}
