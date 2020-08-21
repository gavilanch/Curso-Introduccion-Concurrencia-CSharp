using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Codigo.Modulo_3
{
    public class Repaso_IEnumerable
    {
        public void btnIniciar_Click()
        {
            foreach (var nombre in GenerarNombres())
            {
                Console.WriteLine(nombre);
            }
        }

        private IEnumerable<string> GenerarNombres()
        {
            yield return "Felipe";
            yield return "Claudia";
        }

    }
}
