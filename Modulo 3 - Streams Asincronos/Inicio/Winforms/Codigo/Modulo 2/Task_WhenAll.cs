using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_2
{
    public class Task_WhenAll
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Task_WhenAll(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnIniciar_Click(PictureBox loadingGIF)
        {
            loadingGIF.Visible = true;

            var tarjetas = ObtenerTarjetasDeCredito(250);
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
            var tareas = new List<Task>();

            foreach (var tarjeta in tarjetas)
            {
                var json = JsonConvert.SerializeObject(tarjeta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respuestaTask = httpClient.PostAsync($"{apiURL}/tarjetas", content);
                tareas.Add(respuestaTask);
            }

            await Task.WhenAll(tareas);
        }

        private List<string> ObtenerTarjetasDeCredito(int cantidadDeTarjetas)
        {
            var tarjetas = new List<string>();

            for (int i = 0; i < cantidadDeTarjetas; i++)
            {
                tarjetas.Add(i.ToString().PadLeft(16, '0'));
            }

            return tarjetas;
        }
    }
}
