using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface ITipoRelacaoDAL
    {
        void Cadastrar(TipoRelacaoDTO tipoRelacaoDTO);
        TipoRelacaoDTO Consultar(TipoRelacaoDTO tipoRelacaoDTO);
        List<TipoRelacaoDTO> Listar();
    }
}