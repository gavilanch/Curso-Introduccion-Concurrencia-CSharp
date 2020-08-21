using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_2
{
    public class TaskCompletionSource_Ejemplo
    {
        public async Task btnIniciar_Click(PictureBox loadingGIF, TextBox txtInput)
        {
            loadingGIF.Visible = true;

            var tarea = EvaluarValor(txtInput.Text);

            Console.WriteLine("inicio");
            Console.WriteLine($"Is Completed: {tarea.IsCompleted}");
            Console.WriteLine($"Is Canceled: {tarea.IsCanceled}");
            Console.WriteLine($"Is Faulted: {tarea.IsFaulted}");

            try
            {
                await tarea;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción: {ex.Message}");
            }

            Console.WriteLine("fin");
            Console.WriteLine("");

            loadingGIF.Visible = false;
        }

        public Task EvaluarValor(string valor)
        {
            var tcs = new TaskCompletionSource<object>
                (TaskCreationOptions.RunContinuationsAsynchronously);

            if (valor == "1")
            {
                tcs.SetResult(null);
            }
            else if (valor == "2")
            {
                tcs.SetCanceled();
            }
            else
            {
                tcs.SetException(new ApplicationException($"Valor inválido: {valor}"));
            }

            return tcs.Task;
        }
    }
}
