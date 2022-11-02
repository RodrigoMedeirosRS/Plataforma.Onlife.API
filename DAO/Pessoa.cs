using System;
using System.Collections.Generic;

#nullable disable

namespace BibliotecaViva.DAO
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Pessoaregistros = new HashSet<Pessoaregistro>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Foto { get; set; }
        public string Researchgate { get; set; }
        public string Linkedin { get; set; }
        public string Lattes { get; set; }

        public virtual ICollection<Pessoaregistro> Pessoaregistros { get; set; }
    }
}
