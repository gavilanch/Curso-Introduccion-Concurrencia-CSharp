using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winforms.Codigo.Modulo_2;

namespace Winforms
{
    public partial class Form1 : Form
    {
        private string apiURL;
        private CancellationTokenSource cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
            // Debes cambiar la siguiente URL por la correspondiente a tu Web API en caso de no ser la misma
            apiURL = "https://localhost:44313";
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            // Video: UI Que no se Congela
            //new UI_Que_No_Se_Congela().VersionSincrona(loadingGIF);
            //await new UI_Que_No_Se_Congela().VersionAsincrona(loadingGIF);

            // Videos: Task y Task Que Retorna Un Valor
            //await new Task_Y_TaskDeT(apiURL).btnIniciar_Click(loadingGIF, txtInput);

            // Videos: Tasks Que no Son Exitosas
            //await new Task_No_Exitosa(apiURL).btnIniciar_Click(loadingGIF, txtInput);

            // Videos: Ejecutando Múltiples Tareas - Task.WhenAll
            //await new Task_WhenAll(apiURL).btnIniciar_Click(loadingGIF);

            // Videos: Creando Nuevas Tareas con Task.Run
            //await new Task_Run(apiURL).btnIniciar_Click(loadingGIF);

            // Videos: Limitando las Tareas con un Semáforo - SemaphoreSlim
            //await new Semaforo(apiURL).btnIniciar_Click(loadingGIF);

            // Videos: Flujo de Tareas
            //await new Flujo_de_Tareas(apiURL).btnIniciar_Click(loadingGIF);

            // Videos: Reportando Progreso con IProgress
            //await new Reportando_Progreso(pgProcesamiento, apiURL).btnIniciar_Click(loadingGIF);

            // Videos: Reportando Progreso por Intervalos con Task.WhenAny
            //await new Reportando_Progreso_Tiempo(pgProcesamiento, apiURL).btnIniciar_Click(loadingGIF);

            // Videos: Cancelando Tareas - Tokens de Cancelación
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelando_Tareas(pgProcesamiento, apiURL, cancellationTokenSource).btnIniciar_Click(loadingGIF);

            // Videos: Cancelando Bucles
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelando_Bucles(pgProcesamiento, apiURL, cancellationTokenSource).btnIniciar_Click(loadingGIF);

            // Videos: Cancelando Tareas por Tiempo - Timeout
            //cancellationTokenSource = new CancellationTokenSource();
            //cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            //await new Cancelando_Timeout(pgProcesamiento, apiURL, cancellationTokenSource).btnIniciar_Click(loadingGIF);

            // Videos: Creando Tareas ya Terminadas - Task.FromResult y Amigos
            //var creando_Tareas_Terminadas = new Creando_Tareas_Terminadas();
            //var tarjetas = await creando_Tareas_Terminadas.ObtenerTarjetasDeCreditoMock(1);
            //await creando_Tareas_Terminadas.ProcesarTarjetasMock(tarjetas);
            //await creando_Tareas_Terminadas.ObtenerTareaCancelada();
            //await creando_Tareas_Terminadas.ObtenerTareaConError();  // Esta lanza un error al hacerle await

            // Videos: Entendiendo ConfigureAwait - Ignorando el Contexto de Sincronización
            //CheckForIllegalCrossThreadCalls = true; // Esto solo es necesario en apps de WinForms
            //await new ConfigureAwait().btnIniciar_Click(loadingGIF, btnCancelar);

            // Videos: Patrón de Reintento
            //await new Reintento(apiURL).btnIniciar_Click(loadingGIF);

            // Videos: Patrón Solo Una Tarea
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Solo_Uno(apiURL, cancellationTokenSource).btnIniciar_Click(loadingGIF);

            // Videos: Controlando el Resultado de la Tarea - TaskCompletionSource
            //await new TaskCompletionSource_Ejemplo().btnIniciar_Click(loadingGIF, txtInput);

            // Videos: Cancelando Tareas no Cancelables con TaskCompletionSource
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelando_Tareas_No_Cancelables(cancellationTokenSource).btnIniciar_Click(loadingGIF);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
