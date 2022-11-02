using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DTO;
using DTO.Dominio;

namespace BLL.Interfaces
{
    public interface ILocalidadeBLL
    {
        Task<string> Cadastrar(LocalidadeDTO localidade);
        Task<LocalidadeDTO> Consultar(LocalidadeConsulta localidadeConsulta);
        Task<List<LocalidadeDTO>> Listar(string entrada);
        Task<string> Vincular(VincularRegistroLocalidade vinculo);
    }
}