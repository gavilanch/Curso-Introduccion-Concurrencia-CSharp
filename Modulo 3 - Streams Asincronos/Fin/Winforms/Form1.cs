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
using Winforms.Codigo.Modulo_3;

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
            // Video: Repasando IEnumerable y yield
            //new Repaso_IEnumerable().btnIniciar_Click();

            // Video: Streams Asíncronos
            //await new Stream_Asincrono().btnIniciar_Click(loadingGIF);

            // Video: Cancelando Streams Asíncronos
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelando_Stream_Asincrono(cancellationTokenSource).btnIniciar_Click(loadingGIF);

            cancellationTokenSource = null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
