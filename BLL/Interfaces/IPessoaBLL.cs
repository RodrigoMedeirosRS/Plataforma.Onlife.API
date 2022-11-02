using System.Threading.Tasks;
using System.Collections.Generic;
using DTO;
using DTO.Dominio;

namespace BLL.Interfaces
{
    public interface IPessoaBLL
    {
        Task<string> Cadastrar(PessoaDTO pessoa);
        Task<List<PessoaDTO>> Consultar(PessoaConsulta pessoa);
        Task<List<RegistroDTO>> ObterRelacoes(int codPessoa);
    }
}
