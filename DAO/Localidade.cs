using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Localidade
    {
        public Localidade()
        {
            Registrolocalidades = new HashSet<Registrolocalidade>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Mapa { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public virtual ICollection<Registrolocalidade> Registrolocalidades { get; set; }
    }
}
