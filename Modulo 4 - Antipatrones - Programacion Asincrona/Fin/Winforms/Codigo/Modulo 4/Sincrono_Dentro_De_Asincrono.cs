using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_4
{
    public class Sincrono_Dentro_De_Asincrono
    {
        public async Task btnIniciar_Click(PictureBox loadingGIF)
        {
            loadingGIF.Visible = true;

            // Anti-Patrón: Síncrono dentro de asíncrono
            var valor = ObtenerValor().Result;

            // Solución ideal
            //var valor = await ObtenerValor();

            Console.WriteLine(valor);

            var a = 2 + 2; // esto no bloquea, por tanto está bien

            loadingGIF.Visible = false;
        }

        private async Task<string> ObtenerValor()
        {
            // Otra solución es usar ConfigureAawait (no ideal en este caso)
            await Task.Delay(1000).ConfigureAwait(false);
            return "Felipe";
        }
    }
}
