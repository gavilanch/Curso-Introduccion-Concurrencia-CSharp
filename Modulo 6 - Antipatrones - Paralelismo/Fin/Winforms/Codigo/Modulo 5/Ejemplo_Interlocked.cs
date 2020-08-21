using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_5
{
    public class Ejemplo_Interlocked
    {
        public void btnIniciar_Click()
        {
            Console.WriteLine("inicio");

            var valorSinInterlocked = 0;

            Parallel.For(0, 1000000, numero => valorSinInterlocked++);

            Console.WriteLine($"Sumatoria sin interlocked: {valorSinInterlocked}");

            var valorConInterlocked = 0;

            Parallel.For(0, 1000000, numero => Interlocked.Increment(ref valorConInterlocked));

            Console.WriteLine($"Sumatoria con interlocked: {valorConInterlocked}");

            Console.WriteLine("fin");
        }
    }
}
