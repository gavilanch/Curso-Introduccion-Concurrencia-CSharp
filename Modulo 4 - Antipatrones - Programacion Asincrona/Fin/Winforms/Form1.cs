using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms.Codigo.Modulo_4;

namespace Winforms
{
    public partial class Form1 : Form
    {
        private string apiURL;
        private HttpClient httpClient;
        private CancellationTokenSource cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
            apiURL = "https://localhost:44313";
            httpClient = new HttpClient();
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            // Video: Síncrono dentro de Asíncrono - Bloqueo Mutuo
            await new Sincrono_Dentro_De_Asincrono().btnIniciar_Click(loadingGIF);

            // Video: Evitar Task.Factory.StartNew
            await new StartNew().btnIniciar_Click(loadingGIF);

            // Video: Async void [ver SaludosController en el proyecto de ASP.NET Core]
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
        }
    }
}
