using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_5
{
    public class Ejemplo_Lock
    {
        public void btnIniciar_Click()
        {
            Console.WriteLine("inicio");

            var valorIncrementado = 0;
            var valorSumado = 0;

            var mutex = new object();

            Parallel.For(0, 10000, numero =>
            {
                // Hay condición de carrera
                //Interlocked.Increment(ref valorIncrementado);
                //Interlocked.Add(ref valorSumado, valorIncrementado);

                // en paralelo...

                lock (mutex)
                {
                    valorIncrementado++;
                    valorSumado += valorIncrementado;
                }

                // en paralelo...

            });

            Console.WriteLine("---");

            Console.WriteLine($"Valor Incrementado: {valorIncrementado}");
            Console.WriteLine($"Valor Sumado: {valorSumado}");

            Console.WriteLine("fin");
        }

    }
}
