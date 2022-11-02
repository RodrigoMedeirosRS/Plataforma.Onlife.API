using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DTO;
using DTO.Dominio;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class LocalidadeBLL : ILocalidadeBLL
    {
        private ILocalidadeDAL DAL { get; set; }

        public LocalidadeBLL(ILocalidadeDAL dal)
        {
            DAL = dal;
        }

        public async Task<string> Cadastrar(LocalidadeDTO localidade)
        {
            var codigo = DAL.Cadastrar(localidade);
            return codigo + " Registrado(a) com Sucesso!";
        }
        public async Task<LocalidadeDTO> Consultar(LocalidadeConsulta localidadeConsulta)
        {
            var localidade = localidadeConsulta.Codigo != 0 ? 
                DAL.Consultar(localidadeConsulta.Codigo, localidadeConsulta.Completo) :
                DAL.Consultar(localidadeConsulta.Nome, localidadeConsulta.Completo);
            return localidade; 
        }
        public async Task<List<LocalidadeDTO>> Listar(string entrada)
        {
            return DAL.Listar();
        }
        public async Task<string> Vincular(VincularRegistroLocalidade vinculo)
        {
            DAL.Vincular(vinculo.Localidade, vinculo.Registro);
            return "Registro " + vinculo.Registro.Nome + " foi vinculado com sucesso a " + vinculo.Localidade.Nome; 
        }
    }
}