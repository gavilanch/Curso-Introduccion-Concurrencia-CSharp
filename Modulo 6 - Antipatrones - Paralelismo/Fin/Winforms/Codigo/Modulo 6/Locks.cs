using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_6
{
    public class Locks
    {
        public async Task btnIniciar_Click()
        {
            Console.WriteLine("inicio");

            var mutexA = new object();
            var mutexB = new object();

            var tarea1 = Task.Run(() =>
            {
                Parallel.For(1, 100000, i =>
                {
                    lock (mutexA)
                    {
                        lock (mutexB)
                        {
                            var valor = i;
                        }
                    }
                });
            });

            var tarea2 = Task.Run(() =>
            {
                Parallel.For(1, 100000, i =>
                {
                    lock (mutexB)
                    {
                        lock (mutexA)
                        {
                            var valor = i;
                        }
                    }
                });
            });

            await Task.WhenAll(tarea1, tarea2);

            // Nunca llegaremos a escribir "fin".
            Console.WriteLine("fin");
        }
    }
}
