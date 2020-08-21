using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_2
{
    public class Creando_Tareas_Terminadas
    {
        public Task ProcesarTarjetasMock(List<string> tarjetas,
            IProgress<int> progress = null,
            CancellationToken cancellationToken = default)
        {

            // ...

            return Task.CompletedTask;
        }

        public Task<List<string>> ObtenerTarjetasDeCreditoMock(int cantidadDeTarjetas,
           CancellationToken cancellationToken = default)
        {
            var tarjetas = new List<string>();
            tarjetas.Add("0000000001");

            return Task.FromResult(tarjetas);
        }

        public Task ObtenerTareaConError()
        {
            return Task.FromException(new ApplicationException());
        }

        public Task ObtenerTareaCancelada()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            return Task.FromCanceled(cancellationTokenSource.Token);
        }

    }
}
