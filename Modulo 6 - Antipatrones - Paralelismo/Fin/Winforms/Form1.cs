using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms.Codigo.Modulo_6;

namespace Winforms
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;

            // Video: Paralelismo innecesario
            //await new Innecesario().btnIniciar_Click();

            // Video: Condición de Carrera
            //new Condicion_De_Carrera().btnIniciar_Click();

            // Video: Sobre Saturación
            //new Sobresaturacion().btnIniciar_Click();

            // Video: Usando Una Clase No Segura En Hilos
            //new Seguro_En_Hilos().btnIniciar_Click();

            // Video: Como no usar Locks
            //await new Locks().btnIniciar_Click();

            loadingGIF.Visible = false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
