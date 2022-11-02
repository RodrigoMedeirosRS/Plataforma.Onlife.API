using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITipoExecucaoDAL
    {
        List<TipoExecucaoDTO> Listar();
    }
}