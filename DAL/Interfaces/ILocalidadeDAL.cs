using BibliotecaViva.DTO;

namespace BibliotecaViva.DAL.Interfaces
{
    public interface ILocalidadeDAL
    {
        int Cadastrar(Localidade localizacaoGeograficaDTO);
        void RemoverVinculoRegistro(int? codigoRegistro);
        DTO.Localidade Consultar(string nome);
        DTO.Localidade Consultar(int codigo);
        void Vincular(Localidade localizacaoGeograficaDTO, RegistroDTO registroDTO);
    }
}