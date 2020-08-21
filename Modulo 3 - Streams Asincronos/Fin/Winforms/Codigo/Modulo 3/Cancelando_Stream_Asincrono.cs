using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_3
{
    class Cancelando_Stream_Asincrono
    {
        private CancellationTokenSource cancellationTokenSource;

        public Cancelando_Stream_Asincrono(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public async Task btnIniciar_Click(PictureBox loadingGIF)
        {
            loadingGIF.Visible = true;

            try
            {
                // Opción 2: Cancellation Token
                await foreach (var nombre in GenerarNombres(cancellationTokenSource.Token))
                {
                    Console.WriteLine(nombre);
                    // Opción 1: Un break
                    // break;
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("operación cancelada");
            }
            finally
            {
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }

            // Opción 3: Utilizando EnumeratorCancellation
            var nombresEnumerable = GenerarNombres();
            await ProcesarNombres(nombresEnumerable);

            Console.WriteLine("fin");


            loadingGIF.Visible = false;
        }

        private async Task ProcesarNombres(IAsyncEnumerable<string> nombresEnumerable)
        {

            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await foreach (var nombre in nombresEnumerable.WithCancellation(cancellationTokenSource.Token))
                {
                    Console.WriteLine(nombre);
                }
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("operación cancelada");
            }
            finally
            {
                cancellationTokenSource?.Dispose();
            }

        }

        private async IAsyncEnumerable<string> GenerarNombres(
            [EnumeratorCancellation] CancellationToken token = default)
        {
            yield return "Felipe";
            await Task.Delay(2000, token);
            yield return "Claudia";
            await Task.Delay(2000, token);
            yield return "Antonio";
        }

    }
}
