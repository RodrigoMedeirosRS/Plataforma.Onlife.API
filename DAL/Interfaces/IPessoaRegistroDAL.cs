using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IPessoaRegistroDAL
    {
        void VincularReferencia(PessoaDTO pessoaDTO);
        List<RelacaoDTO> ObterRelacao(int codPessoa);
        List<RegistroDTO> ObterRelacaoCompleta(PessoaDTO pessoaDTO);
    }
}