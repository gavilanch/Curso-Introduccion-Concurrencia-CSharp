using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_5
{
    public class Parallel_For_Matrices
    {
        public async Task btnIniciar_Click()
        {
            var columnasMatrizA = 1100;
            var filas = 1000;
            var columnasMatrizB = 1750;

            var matrizA = Matrices.InicializarMatriz(filas, columnasMatrizA);
            var matrizB = Matrices.InicializarMatriz(columnasMatrizA, columnasMatrizB);
            var resultado = new double[filas, columnasMatrizB];

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await Task.Run(() => Matrices.MultiplicarMatricesSecuencial(matrizA, matrizB, resultado));

            var tiempoSecuencial = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Secuencial - duración en segundos: {0}",
                    tiempoSecuencial);

            resultado = new double[filas, columnasMatrizB];

            stopwatch.Restart();

            await Task.Run(() => Matrices.MultiplicarMatricesParalelo(matrizA, matrizB, resultado));

            var tiempoEnParalelo = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Paralelo - duración en segundos: {0}",
                   tiempoEnParalelo);

            Utils.EscribirComparacion(tiempoSecuencial, tiempoEnParalelo);

            Console.WriteLine("fin");
        }
    }
}
