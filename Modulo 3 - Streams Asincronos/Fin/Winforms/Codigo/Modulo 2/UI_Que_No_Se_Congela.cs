using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Codigo.Modulo_2
{
    public class UI_Que_No_Se_Congela
    {
        public void VersionSincrona(PictureBox loadingIMG)
        {
            loadingIMG.Visible = true;
            Thread.Sleep(5000);
            loadingIMG.Visible = false;
        }

        public async Task VersionAsincrona(PictureBox loadingIMG)
        {
            loadingIMG.Visible = true;
            await Task.Delay(TimeSpan.FromSeconds(5));
            loadingIMG.Visible = false;
        }
    }
}
