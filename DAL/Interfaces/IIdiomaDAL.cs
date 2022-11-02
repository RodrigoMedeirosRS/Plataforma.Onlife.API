using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IIdiomaDAL
    {
        void Cadastrar(IdiomaDTO idiomaDTO);
        IdiomaDTO Consultar(IdiomaDTO idiomaDTO);
        List<IdiomaDTO> Listar();
    }
}