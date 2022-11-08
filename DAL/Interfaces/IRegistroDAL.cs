using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IRegistroDAL
    {
        List<RegistroDTO> Consultar(RegistroDTO registroDTO, bool completo);
        RegistroDTO Consultar(int codRegistro, bool completo);
        RegistroDTO ObterRelacao(int codigoregistro);
        List<RegistroDTO> ListarPorLocalidade(int codigoLocalidade);
        int Cadastrar(RegistroDTO registroDTO);
    }
}
