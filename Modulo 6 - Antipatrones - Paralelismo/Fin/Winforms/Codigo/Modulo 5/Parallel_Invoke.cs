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
    public class Parallel_Invoke
    {
        public void btnIniciar_Click()
        {
            // Preparando código de voltear imágenes
            var directorioActual = AppDomain.CurrentDomain.BaseDirectory;
            var carpetaOrigen = Path.Combine(directorioActual, @"Imagenes\resultado-secuencial");
            var carpetaDestinoSecuencial = Path.Combine(directorioActual, @"Imagenes\foreach-secuencial");
            var carpetaDestinoParalelo = Path.Combine(directorioActual, @"Imagenes\foreach-paralelo");
            Utils.PrepararEjecucion(carpetaDestinoSecuencial, carpetaDestinoParalelo);
            var archivos = Directory.EnumerateFiles(carpetaOrigen);

            // Preparando código de matrices
            int columnasMatrizA = 208;
            int filas = 1240;
            int columnasMatrizB = 750;
            var matrizA = Matrices.InicializarMatriz(filas, columnasMatrizA);
            var matrizB = Matrices.InicializarMatriz(columnasMatrizA, columnasMatrizB);
            var resultado = new double[filas, columnasMatrizB];

            Action multiplicarMatrices = () => Matrices.MultiplicarMatricesSecuencial(matrizA, matrizB, resultado);
            Action voltearImagenes = () =>
            {
                foreach (var archivo in archivos)
                {
                    VoltearImagen(archivo, carpetaDestinoSecuencial);
                }
            };

            Action[] acciones = new Action[] { multiplicarMatrices, voltearImagenes };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // TODO: Algoritmo secuencial
            foreach (var accion in acciones)
            {
                accion();
            }

            var tiempoSecuencial = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Secuencial - duración en segundos: {0}",
                    tiempoSecuencial);

            Utils.PrepararEjecucion(carpetaDestinoSecuencial, carpetaDestinoParalelo);

            stopwatch.Restart();

            // TODO: Algoritmo paralelo
            Parallel.Invoke(acciones);

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
