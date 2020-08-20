using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_2
{
    public class Task_No_Exitosa
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Task_No_Exitosa(string apiURL)
        {
            this.apiURL = apiURL;
            httpClient = new HttpClient();
        }

        public async Task btnIniciar_Click(PictureBox loadingGIF, TextBox txtInput)
        {
            loadingGIF.Visible = true;
            await Esperar();
            var nombre = txtInput.Text;
            try
            {
                var saludo = await ObtenerSaludo(nombre);
                MessageBox.Show(saludo);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            loadingGIF.Visible = false;
        }

        private async Task Esperar()
        {
            await Task.Delay(TimeSpan.FromSeconds(0));
        }

        private async Task<string> ObtenerSaludo(string nombre)
        {
            using (var respuesta = await httpClient.GetAsync($"{apiURL}/saludos2/{nombre}"))
            {
                respuesta.EnsureSuccessStatusCode();
                var saludo = await respuesta.Content.ReadAsStringAsync();
                return saludo;
            }
        }
    }
}
