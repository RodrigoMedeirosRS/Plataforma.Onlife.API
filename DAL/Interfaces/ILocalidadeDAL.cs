using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface ILocalidadeDAL
    {
        int Cadastrar(LocalidadeDTO localidadeDTO);
        void RemoverVinculoRegistro(int? codigoRegistro);
        DTO.LocalidadeDTO Consultar(string nome, bool completo);
        DTO.LocalidadeDTO Consultar(int codigo, bool completo);
        List<LocalidadeDTO> Listar();
        void Vincular(LocalidadeDTO localidadeDTO, RegistroDTO registroDTO);
    }
}