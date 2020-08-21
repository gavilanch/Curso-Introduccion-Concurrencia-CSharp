using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_6
{
    public class Innecesario
    {
        public async Task btnIniciar_Click()
        {
            Console.WriteLine("inicio");

            var stopwatch = new Stopwatch();

            var max = int.MaxValue / 3;
            var numeros = Enumerable.Range(0, max);

            stopwatch.Start();

            await Task.Run(() =>
            {
                foreach (var numero in numeros)
                {
                    var resultado = numero + numero;
                }
            });

            Console.WriteLine("Secuencial - duración en segundos: {0}",
                stopwatch.ElapsedMilliseconds / 1000.0);

            stopwatch.Restart();

            await Task.Run(() =>
            {
                Parallel.ForEach(numeros, numero => { var resultado = numero + numero; });
            });

            Console.WriteLine("Paralelo - duración en segundos: {0}",
               stopwatch.ElapsedMilliseconds / 1000.0);

            Console.WriteLine("fin");
        }
    }
}
