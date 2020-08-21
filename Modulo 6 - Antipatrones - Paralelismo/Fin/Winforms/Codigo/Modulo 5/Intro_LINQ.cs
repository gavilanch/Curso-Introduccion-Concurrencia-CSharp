using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_5
{
    public class Intro_LINQ
    {
        private readonly CancellationTokenSource cancellationTokenSource;

        public Intro_LINQ(CancellationTokenSource cancellationTokenSource)
        {
            this.cancellationTokenSource = cancellationTokenSource;
        }

        public void btnIniciar_Click()
        {
            Console.WriteLine("inicio");

            var fuente = Enumerable.Range(1, 20);

            var elementosPares = fuente
                .AsParallel()
                .WithDegreeOfParallelism(2)
                .WithCancellation(cancellationTokenSource.Token)
                .AsOrdered()
                .Where(x => x % 2 == 0)
                .ToList();

            foreach (var numero in elementosPares)
            {
                Console.WriteLine(numero);
            }

            Console.WriteLine("fin");
        }
    }
}
