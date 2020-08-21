using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_5
{
    public class LINQ_ForAll
    {
        public void btnIniciar_Click()
        {
            Console.WriteLine("inicio");

            var queryParalelo = Enumerable.Range(1, 10).AsParallel()
                .WithDegreeOfParallelism(2).Select(x => Matrices.InicializarMatriz(100, 100));

            // Procesa todo junto
            //foreach (var matriz in queryParalelo)
            //{
            //    Console.WriteLine(matriz[0, 0]);
            //}

            queryParalelo.ForAll(matriz => Console.WriteLine(matriz[0, 0]));

            Console.WriteLine("fin");
        }
    }
}
