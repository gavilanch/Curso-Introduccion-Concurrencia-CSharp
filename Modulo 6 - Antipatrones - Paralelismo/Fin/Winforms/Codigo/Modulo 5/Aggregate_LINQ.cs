using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_5
{
    public class Aggregate_LINQ
    {
        public void btnIniciar_Click()
        {
            Console.WriteLine("inicio");

            // Ejemplos de Suma y Promedio
            //var fuente = Enumerable.Range(1, 1000);
            //var suma = fuente.AsParallel().Sum();
            //var promedio = fuente.AsParallel().Average();

            //Console.WriteLine("La suma es " + suma);
            //Console.WriteLine("El promedio es " + promedio);

            // Ejemplo de Aggregate en paralelo
            var matrices = Enumerable.Range(1, 500).Select(x => Matrices.InicializarMatriz(1000, 1000)).ToList();

            Console.WriteLine("matrices generadas");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var sumaMatricesSecuencial = matrices.Aggregate(Matrices.SumarMatricesSecuencial);

            var tiempoSecuencial = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Secuencial - duración en segundos: {0}",
                    tiempoSecuencial);

            stopwatch.Restart();

            var sumaMatricesParalelo = matrices.AsParallel().Aggregate(Matrices.SumarMatricesSecuencial);

            var tiempoParalelo = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Paralelo - duración en segundos: {0}",
                    tiempoParalelo);

            Utils.EscribirComparacion(tiempoSecuencial, tiempoParalelo);

            Console.WriteLine("fin");
        }

    }
}
