using DTO;

namespace DAL.Interfaces
{
    public interface ILocalidadeDAL
    {
        int Cadastrar(Localidade localizacaoGeograficaDTO);
        void RemoverVinculoRegistro(int? codigoRegistro);
        DTO.Localidade Consultar(string nome, bool completo);
        DTO.Localidade Consultar(int codigo, bool completo);
        void Vincular(Localidade localizacaoGeograficaDTO, RegistroDTO registroDTO);
    }
}