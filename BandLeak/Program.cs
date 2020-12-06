using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BandLeak
{
    class Program
    {
        static StringBuilder b = new StringBuilder();
        static void Complete(object sender, DownloadStringCompletedEventArgs e)
        {
            b.Append(e.Result);
        }
        static async Task Main(string[] args)
        {
            Console.WriteLine("Это свободная программа: вы можете перераспространять ее и/или изменять \n" +
                "ее на условиях Стандартной общественной лицензии GNU в том виде, в каком \n" +
                "она была опубликована Фондом свободного программного обеспечения; либо \n" +
                "версии 3 лицензии, либо(по вашему выбору) любой более поздней версии. \n" +
                "Эта программа распространяется в надежде, что она будет полезной, \n" +
                "но БЕЗО ВСЯКИХ ГАРАНТИЙ; даже без неявной гарантии ТОВАРНОГО ВИДА \n" +
                "или ПРИГОДНОСТИ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. \n" +
                "Подробнее см. в Стандартной общественной лицензии GNU. \n" +
                "Вы должны были получить копию Стандартной общественной лицензии GNU \n" +
                "вместе с этой программой.Если это не так, см. \n" +
                "< https://www.gnu.org/licenses/>.");
            var max = Console.ReadLine();
            FileStream f = File.Create("band.html");
            string link = "https://band.link/all-pages?page=";
            var vb = new WebClient();
            vb.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Complete);
            for (int i = 190; i <= Int32.Parse(max); i++) {
                await vb.DownloadStringTaskAsync(new Uri(link + i));
            }
            b.Replace("href=\"/", "href=\"https://band.link/");
            f.Write(Encoding.Default.GetBytes(b.ToString()), 0, b.ToString().Length);
        }
    }
}
