using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_6
{
    public class Condicion_De_Carrera
    {
        public void btnIniciar_Click()
        {
            Console.WriteLine("inicio");
            var valorSinInterlocked = 0;

            // Antipatrón: Condición de carrera
            Parallel.For(0, 1000000, numero => valorSinInterlocked++);

            // Solución: utilizar un mecanismo de sincronización
            //Parallel.For(0, 1000000, numero => Interlocked.Increment(ref valorSinInterlocked));

            Console.WriteLine($"Sumatoria sin interlocked: {valorSinInterlocked}");

            Console.WriteLine("fin");
        }
    }
}
