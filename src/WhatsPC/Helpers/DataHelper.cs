using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsPC;

namespace DataHelper
{
    internal class Data
    {
        public static string DataRootDir = Application.StartupPath +
                                         @"\app\";
    }

    public static class Uri
    {
        public const string URL_BUILTBYBEL = "https://www.builtbybel.com";
        public const string URL_ASSEMBLY = "https://raw.githubusercontent.com/builtbybel/WhatsPC/main/src/WhatsPC/Properties/AssemblyInfo.cs";
        public const string URL_GITREPO = "https://github.com/builtbybel/WhatsPC";
        public const string URL_ICONATTRIBUTION = "https://www.flaticon.com/free-icon/toaster_7175212";
        public const string URL_GITLATEST = URL_GITREPO + "/releases/latest";
    }
}