using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_2
{
    public class Reportando_Progreso_Tiempo
    {
        private readonly ProgressBar pgProcesamiento;
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Reportando_Progreso_Tiempo(ProgressBar pgProcesamiento, string apiURL)
        {
            this.pgProcesamiento = pgProcesamiento;
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnIniciar_Click(PictureBox loadingGIF)
        {
            loadingGIF.Visible = true;
            pgProcesamiento.Visible = true;
            var reportarProgreso = new Progress<int>(ReportarProgresoTarjetas);

            var tarjetas = await ObtenerTarjetasDeCredito(20);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcesarTarjetas(tarjetas, reportarProgreso);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operación finalizada en {stopwatch.ElapsedMilliseconds / 1000.0} segundos");

            loadingGIF.Visible = false;
            pgProcesamiento.Value = 0;
            pgProcesamiento.Visible = false;
        }

        private void ReportarProgresoTarjetas(int porcentaje)
        {
            pgProcesamiento.Value = porcentaje;
        }

        private async Task ProcesarTarjetas(List<string> tarjetas, IProgress<int> progress = null)
        {
            using var semaforo = new SemaphoreSlim(2);

            var tareas = new List<Task<HttpResponseMessage>>();

            tareas = tarjetas.Select(async tarjeta =>
            {
                var json = JsonConvert.SerializeObject(tarjeta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await semaforo.WaitAsync();
                try
                {
                    return await httpClient.PostAsync($"{apiURL}/tarjetas", content);
                }
                finally
                {
                    semaforo.Release();
                }
            }).ToList();

            var respuestasTareas = Task.WhenAll(tareas);

            if (progress != null)
            {
                while (await Task.WhenAny(respuestasTareas, Task.Delay(3000)) != respuestasTareas)
                {
                    var tareasCompletadas = tareas.Where(x => x.IsCompleted).Count();
                    var porcentaje = (double)tareasCompletadas / tarjetas.Count;
                    porcentaje = porcentaje * 100;
                    var porcentajeInt = (int)Math.Round(porcentaje, 0);
                    progress.Report(porcentajeInt);
                }
            }

            var respuestas = await respuestasTareas;

            var tarjetasRechazadas = new List<string>();

            foreach (var respuesta in respuestas)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                var respuestaTarjeta = JsonConvert
                    .DeserializeObject<RespuestaTarjeta>(contenido);
                if (!respuestaTarjeta.Aprobada)
                {
                    tarjetasRechazadas.Add(respuestaTarjeta.Tarjeta);
                }
            }
        }

        private async Task<List<string>> ObtenerTarjetasDeCredito(int cantidadDeTarjetas)
        {
            return await Task.Run(() =>
            {
                var tarjetas = new List<string>();

                for (int i = 0; i < cantidadDeTarjetas; i++)
                {
                    tarjetas.Add(i.ToString().PadLeft(16, '0'));
                }

                return tarjetas;
            });

        }
    }
}
