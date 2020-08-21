using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_5
{
    public class Intro_Parallel_For
    {
        public void btnIniciar_Click()
        {
            Console.WriteLine("secuencial");

            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("en paralelo");

            Parallel.For(0, 11, i => Console.WriteLine(i));
        }
    }
}
