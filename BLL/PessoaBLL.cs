using System.Threading.Tasks;
using System.Collections.Generic;
using DTO;
using DTO.Dominio;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class PessoaBLL : BaseBLL, IPessoaBLL
    {
        private IPessoaDAL PessoaDAL { get; set; }
        private IPessoaRegistroDAL PessoaRegistroDAL { get; set; }
        public PessoaBLL(IPessoaDAL referenciaDAL, IPessoaRegistroDAL pessoaRegistroDAL)
        {
            PessoaDAL = referenciaDAL;
            PessoaRegistroDAL = pessoaRegistroDAL;
        }

        public async Task<string> Cadastrar(PessoaDTO pessoa)
        {
            var codigo = PessoaDAL.Cadastrar(pessoa);
            return codigo + " Registrado(a) com Sucesso!";
        }

        public async Task<List<PessoaDTO>> Consultar(PessoaConsulta pessoa)
        {
            return PessoaDAL.Consultar(new PessoaDTO()
            {
                Nome = pessoa.Nome,
                Apelido = pessoa.Apelido
            });
        }

        public async Task<List<RegistroDTO>> ObterRelacoes(int codPessoa)
        {
            return PessoaRegistroDAL.ObterRelacaoCompleta(new PessoaDTO()
            {
                Codigo = codPessoa
            });
        }
    }
}