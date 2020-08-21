using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_5
{
    public class Parallel_ForEach
    {
        public void btnIniciar_Click()
        {
            var directorioActual = AppDomain.CurrentDomain.BaseDirectory;
            var carpetaOrigen = Path.Combine(directorioActual, @"Imagenes\resultado-secuencial");
            var carpetaDestinoSecuencial = Path.Combine(directorioActual, @"Imagenes\foreach-secuencial");
            var carpetaDestinoParalelo = Path.Combine(directorioActual, @"Imagenes\foreach-paralelo");
            Utils.PrepararEjecucion(carpetaDestinoSecuencial, carpetaDestinoParalelo);

            var archivos = Directory.EnumerateFiles(carpetaOrigen);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Algoritmo secuencial
            foreach (var archivo in archivos)
            {
                VoltearImagen(archivo, carpetaDestinoSecuencial);
            }

            var tiempoSecuencial = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Secuencial - duración en segundos: {0}",
                    tiempoSecuencial);

            stopwatch.Restart();

            // Algoritmo en paralelo
            Parallel.ForEach(archivos, archivo =>
            {
                VoltearImagen(archivo, carpetaDestinoParalelo);
            });

            var tiempoEnParalelo = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Paralelo - duración en segundos: {0}",
                   tiempoEnParalelo);

            Utils.EscribirComparacion(tiempoSecuencial, tiempoEnParalelo);

            Console.WriteLine("fin");
        }

        private void VoltearImagen(string archivo, string carpetaDestino)
        {
            using (var image = new Bitmap(archivo))
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                var nombreArchivo = Path.GetFileName(archivo);
                var destino = Path.Combine(carpetaDestino, nombreArchivo);
                image.Save(destino);
            }
        }
    }
}
