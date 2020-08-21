using Newtonsoft.Json;
using System;
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
using Winforms.Codigo.Modulo_5;

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
            loadingGIF.Visible = true;

            // Video: Con Task.WhenAll Ejecutamos Tareas Simultáneas
            //await new TaskWhenAll().btnIniciar_Click();

            // Video: Entendiendo Parallel.Fors
            //new Intro_Parallel_For().btnIniciar_Click();

            // Video: Ejemplo de Parallel.For - Velocidad
            //await new Parallel_For_Matrices().btnIniciar_Click();

            // Video: Iterando Colecciones en Paralelo con Parallel.ForEach
            //new Parallel_ForEach().btnIniciar_Click();

            // Video: Parallel.Invoke - Distintos Métodos en Paralelo
            //new Parallel_Invoke().btnIniciar_Click();

            // Video: Cancelando Tareas en Paralelo y Máximo Grado de Paralelismo
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Maximo_Grado_Paralelismo(cancellationTokenSource).btnIniciar_Click();
            //cancellationTokenSource = null;

            // Video: Interlocked - Operaciones Simples Atómicas
            //new Ejemplo_Interlocked().btnIniciar_Click();

            // Video: Locks - Sincronizando Hilos
            //new Ejemplo_Lock().btnIniciar_Click();

            // Video: Introducción a PLINQ
            //cancellationTokenSource = new CancellationTokenSource();
            //new Intro_LINQ(cancellationTokenSource).btnIniciar_Click();
            //cancellationTokenSource = null;

            // Video: PLINQ - Operaciones de Agregado
            //new Aggregate_LINQ().btnIniciar_Click();

            // Video: PLINQ - ForAll - Procesando Resultados de Inmediato
            //new LINQ_ForAll().btnIniciar_Click();

            loadingGIF.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
