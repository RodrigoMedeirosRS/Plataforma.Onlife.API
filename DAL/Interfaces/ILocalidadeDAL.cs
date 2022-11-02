using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface ILocalidadeDAL
    {
        int Cadastrar(LocalidadeDTO localidadeDTO);
        DTO.LocalidadeDTO Consultar(string nome, bool completo);
        DTO.LocalidadeDTO Consultar(int codigo, bool completo);
        List<LocalidadeDTO> Listar();
        void Vincular(LocalidadeDTO localidadeDTO, RegistroDTO registroDTO);
    }
}