using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Registro
    {
        public Registro()
        {
            Pessoaregistros = new HashSet<Pessoaregistro>();
            ReferenciumReferenciaNavigations = new HashSet<Referencium>();
            ReferenciumRegistroNavigations = new HashSet<Referencium>();
            Registrolocalidades = new HashSet<Registrolocalidade>();
        }

        public int Codigo { get; set; }
        public int Idioma { get; set; }
        public int Tipo { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Conteudo { get; set; }
        public DateTime Datainsercao { get; set; }
        public string Descricao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual Idioma IdiomaNavigation { get; set; }
        public virtual Tipo TipoNavigation { get; set; }
        public virtual ICollection<Pessoaregistro> Pessoaregistros { get; set; }
        public virtual ICollection<Referencium> ReferenciumReferenciaNavigations { get; set; }
        public virtual ICollection<Referencium> ReferenciumRegistroNavigations { get; set; }
        public virtual ICollection<Registrolocalidade> Registrolocalidades { get; set; }
    }
}
