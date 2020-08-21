using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_5
{
    public class TaskWhenAll
    {
        private HttpClient httpClient;

        public TaskWhenAll()
        {
            httpClient = new HttpClient();
        }

        public async Task btnIniciar_Click()
        {

            var directorioActual = AppDomain.CurrentDomain.BaseDirectory;
            var destinoBaseSecuencial = Path.Combine(directorioActual, @"Imagenes\resultado-secuencial");
            var destinoBaseParalelo = Path.Combine(directorioActual, @"Imagenes\resultado-paralelo");
            PrepararEjecucion(destinoBaseSecuencial, destinoBaseParalelo);

            Console.WriteLine("inicio");

            var imagenes = ObtenerImagenes();

            // Parte secuencial

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var imagen in imagenes)
            {
                await ProcesarImagen(destinoBaseSecuencial, imagen);
            }

            var tiempoSecuencial = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Secuencial - duración en segundos: {0}",
                    tiempoSecuencial);

            stopwatch.Restart();

            // Parte paralelo 

            var tareasEnumerable = imagenes.Select(async imagen => await ProcesarImagen(destinoBaseParalelo, imagen));

            await Task.WhenAll(tareasEnumerable);

            var tiempoEnParalelo = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("Paralelo - duración en segundos: {0}",
                   tiempoEnParalelo);

            Utils.EscribirComparacion(tiempoSecuencial, tiempoEnParalelo);

            Console.WriteLine("fin");
        }

        private async Task ProcesarImagen(string directorio, Imagen imagen)
        {
            var response = await httpClient.GetAsync(imagen.URL);
            var content = await response.Content.ReadAsByteArrayAsync();

            Bitmap bitmap;
            using (var ms = new MemoryStream(content))
            {
                bitmap = new Bitmap(ms);
            }

            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            var destino = Path.Combine(directorio, imagen.Nombre);
            bitmap.Save(destino);
        }

        private static List<Imagen> ObtenerImagenes()
        {
            var imagenes = new List<Imagen>();
            for (int i = 0; i < 5; i++)
            {
                {
                    imagenes.Add(
                    new Imagen()
                    {
                        Nombre = $"Spider-Man Spider-Verse {i}.jpg",
                        URL = "https://m.media-amazon.com/images/M/MV5BMjMwNDkxMTgzOF5BMl5BanBnXkFtZTgwNTkwNTQ3NjM@._V1_UY863_.jpg"
                    });
                    imagenes.Add(

                    new Imagen()
                    {
                        Nombre = $"Spider-Man Far From Home {i}.jpg",
                        URL = "https://m.media-amazon.com/images/M/MV5BMGZlNTY1ZWUtYTMzNC00ZjUyLWE0MjQtMTMxN2E3ODYxMWVmXkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_UY863_.jpg"
                    });
                    imagenes.Add(

                    new Imagen()
                    {
                        Nombre = $"Moana {i}.jpg",
                        URL = "https://lumiere-a.akamaihd.net/v1/images/r_moana_header_poststreet_mobile_bd574a31.jpeg?region=0,0,640,480"
                    });
                    imagenes.Add(

                    new Imagen()
                    {
                        Nombre = $"Avengers Infinity War {i}.jpg",
                        URL = "https://img.redbull.com/images/c_crop,x_143,y_0,h_1080,w_1620/c_fill,w_1500,h_1000/q_auto,f_auto/redbullcom/2018/04/23/e4a3d8a5-2c44-480a-b300-1b2b03e205a5/avengers-infinity-war-poster"
                    });
                    imagenes.Add(

                    new Imagen()
                    {
                        Nombre = $"Avengers Endgame {i}.jpg",
                        URL = "https://hipertextual.com/files/2019/04/hipertextual-nuevo-trailer-avengers-endgame-agradece-fans-universo-marvel-2019351167.jpg"
                    });
                }
            }

            return imagenes;
        }


        private void PrepararEjecucion(string destinoBaseParalelo, string destinoBaseSecuencial)
        {
            if (!Directory.Exists(destinoBaseParalelo))
            {
                Directory.CreateDirectory(destinoBaseParalelo);
            }

            if (!Directory.Exists(destinoBaseSecuencial))
            {
                Directory.CreateDirectory(destinoBaseSecuencial);
            }

            BorrarArchivos(destinoBaseSecuencial);
            BorrarArchivos(destinoBaseParalelo);
        }

        private void BorrarArchivos(string directorio)
        {
            var archivos = Directory.EnumerateFiles(directorio);
            foreach (var archivo in archivos)
            {
                File.Delete(archivo);
            }
        }

    }
}
