using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_3
{
    public class Stream_Asincrono
    {
        public async Task btnIniciar_Click(PictureBox loadingGIF)
        {
            loadingGIF.Visible = true;

            await foreach (var nombre in GenerarNombres())
            {
                Console.WriteLine(nombre);
            }

            loadingGIF.Visible = false;
        }

        private async IAsyncEnumerable<string> GenerarNombres()
        {
            yield return "Felipe";
            await Task.Delay(2000);
            yield return "Claudia";
        }
    }
}
