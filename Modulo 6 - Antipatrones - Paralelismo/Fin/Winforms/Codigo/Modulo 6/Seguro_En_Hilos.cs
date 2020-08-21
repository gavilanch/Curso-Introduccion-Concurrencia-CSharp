using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_6
{
    public class Seguro_En_Hilos
    {
        public void btnIniciar_Click()
        {
            Console.WriteLine("inicio");
            var mutex = new object();
            Random random = new Random();

            var diccionarioConcurrente = new ConcurrentDictionary<double, int>();

            Parallel.For(1, 1000000, i =>
            {
                double llave;

                // antipatrón: La clase Random no es segura en hilos
                llave = random.NextDouble();

                // Solución: Utilizar un mecanismo de sincronización
                //lock (mutex)
                //{
                //    llave = random.NextDouble();
                //}
                diccionarioConcurrente.AddOrUpdate(llave, 1, (llave, valorAnterior) => valorAnterior + 1);
            });

            var masFrecuentes = diccionarioConcurrente.OrderByDescending(x => x.Value).Take(5).ToList();

            foreach (var item in masFrecuentes)
            {
                Console.WriteLine($"La llave {item.Key} se repite {item.Value} veces");
            }

            Console.WriteLine("fin");
        }

    }
}
