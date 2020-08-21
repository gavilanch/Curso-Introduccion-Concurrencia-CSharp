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
    public class Semaforo
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Semaforo(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnIniciar_Click(PictureBox loadingGIF)
        {
            loadingGIF.Visible = true;

            var tarjetas = await ObtenerTarjetasDeCredito(25000);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcesarTarjetas(tarjetas);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operación finalizada en {stopwatch.ElapsedMilliseconds / 1000.0} segundos");

            loadingGIF.Visible = false;
        }

        private async Task ProcesarTarjetas(List<string> tarjetas)
        {
            using var semaforo = new SemaphoreSlim(4000);

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

            await Task.WhenAll(tareas);
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
