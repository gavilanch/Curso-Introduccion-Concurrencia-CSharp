using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_2
{
    public class Cancelando_Tareas_No_Cancelables
    {

        private CancellationTokenSource cancellationTokenSource;

        public Cancelando_Tareas_No_Cancelables(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public async Task btnIniciar_Click(PictureBox loadingGIF)
        {
            loadingGIF.Visible = true;

            try
            {
                var resultado = await Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    return 7;
                }).WithCancellation(cancellationTokenSource.Token);

                Console.WriteLine(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }

            loadingGIF.Visible = false;
        }

        
    }

    public static class TaskExtensionMethods
    {
        public static async Task<T> WithCancellation<T>(this Task<T> task,
            CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            using (cancellationToken.Register(state =>
            {
                ((TaskCompletionSource<object>)state).TrySetResult(null);
            }, tcs))
            {
                var tareaResultante = await Task.WhenAny(task, tcs.Task);
                if (tareaResultante == tcs.Task)
                {
                    throw new OperationCanceledException(cancellationToken);
                }

                return await task;
            }

        }
    }
}
