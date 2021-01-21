using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiSetup.Models
{
    public class AUX_SETUP_CLASS
    {
        public bool? EULA_ACEITO { get; set; } = null;
        public string AMBISTORE_SERIAL { get; set; }
        public string AMBITECH_SERIAL { get; set; }
        public string AMBIMAITRE_SERIAL { get; set; }
        public string AMBICD_SERIAL { get; set; }
        public string AMBIWEB_SERIAL { get; set; }
        public string INSTALL_LOCATION { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Trilha Tecnologia\AmbiStore");
        public bool INSTALL_STORE { get; set; } = true;
        public bool INSTALL_MYSQL { get; set; }
        public bool INSTALL_ANYDESK { get; set; }
        public bool INSTALL_NETCORE { get; set; }
    }
}
