using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Registrolocalidade
    {
        public int Codigo { get; set; }
        public int Localidade { get; set; }
        public int Registro { get; set; }

        public virtual Localidade LocalidadeNavigation { get; set; }
        public virtual Registro RegistroNavigation { get; set; }
    }
}
