using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winforms.Codigo.Modulo_5;

namespace Winforms.Codigo.Modulo_6
{
    public class Sobresaturacion
    {
        public void btnIniciar_Click()
        {
            Console.WriteLine("inicio");
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var matrices = Enumerable.Range(1, 1000).AsParallel().Select(x => Matrices.InicializarMatriz(750, 750)).ToList();

            var tiempoParalelismo = stopwatch.ElapsedMilliseconds / 1000.0;
            Console.WriteLine($"Paralelismo - Transcurridos {tiempoParalelismo} segundos");

            stopwatch.Restart();
            var matrices2 = Enumerable.Range(1, 1000).AsParallel().Select(x => Matrices.InicializarMatrizSaturado(750, 750)).ToList();

            var tiempoSobreSaturacion = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine($"Sobre Saturación - Transcurridos {tiempoSobreSaturacion} segundos");
            Utils.EscribirComparacion(tiempoParalelismo, tiempoSobreSaturacion);

            Console.WriteLine("fin");
        }
    }
}
