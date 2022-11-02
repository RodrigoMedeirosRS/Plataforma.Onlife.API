using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITipoDAL
    {
        void Cadastrar(TipoDTO tipoDTO);
        TipoDTO Consultar(TipoDTO tipoDTO);
        List<TipoDTO> Listar();
    }
}