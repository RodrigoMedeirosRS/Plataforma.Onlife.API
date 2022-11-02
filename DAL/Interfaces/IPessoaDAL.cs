using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IPessoaDAL
    {
        int Cadastrar(PessoaDTO pessoaDTO);
        List<PessoaDTO> Consultar(PessoaDTO pessoaDTO);   
        PessoaDTO Consultar(int codigoPessoa);     
    }
}
