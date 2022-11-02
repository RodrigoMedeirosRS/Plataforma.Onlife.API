using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using API.Interface;
using DTO;
using DTO.Dominio;
using BLL.Interfaces;

namespace CTRL
{
    [Route("BibliotecaViva/Localidade")]
    [ApiController]
    public class Localidade : Controller
    {
        private ILocalidadeBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }

        public Localidade(IRequisicao requisicao, ILocalidadeBLL bll)
        {
            _BLL = bll;
            _Requisicao = requisicao;
        }

        [HttpPost("Cadastrar")]
        public async Task<string> Cadastrar(LocalidadeDTO localidade)
        {
            return _Requisicao.ExecutarRequisicao<LocalidadeDTO, string>(localidade, _BLL.Cadastrar).Result;
        }

        [HttpPost("Consultar")]
        public async Task<LocalidadeDTO> Consultar(LocalidadeConsulta localidadeConsulta)
        {
            return _Requisicao.ExecutarRequisicao<LocalidadeConsulta, LocalidadeDTO>(localidadeConsulta, _BLL.Consultar).Result;
        }

        [HttpPost("Listar")]
        public async Task<List<LocalidadeDTO>> Listar(string entrada)
        {
            return _Requisicao.ExecutarRequisicao<string, List<LocalidadeDTO>>(entrada, _BLL.Listar).Result;
        }
    }
}