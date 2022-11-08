using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using API.Interface;
using DTO;
using DTO.Dominio;
using BLL.Interfaces;

namespace BibliotecaViva.CTRL
{
    [Route("PlataformaOnlife/Registro")]
    [ApiController]
    public class RegistroController : Controller
    {
        private IRegistroBLL _BLL { get; set; }
        private IRequisicao _Requisicao { get; set; }
        
        public RegistroController(IRegistroBLL bll, IRequisicao requisicao)
        {
            _BLL = bll;
            _Requisicao = requisicao;
        }

        [HttpPost("Cadastrar")]
        public async Task<string> Cadastrar(RegistroDTO registro)
        {
            return _Requisicao.ExecutarRequisicao<RegistroDTO, string>(registro, _BLL.Cadastrar).Result;
        }
        [HttpPost("Consultar")]
        public async Task<List<RegistroDTO>> Consultar(RegistroConsulta registro)
        {
            return _Requisicao.ExecutarRequisicao<RegistroConsulta, List<RegistroDTO>>(registro, _BLL.Consultar).Result;
        }
        [HttpPost("ObterRelacao")]
        public async Task<RegistroDTO> ObterRelacao(RelacaoConsulta registro)
        {
            return _Requisicao.ExecutarRequisicao<RelacaoConsulta, RegistroDTO>(registro, _BLL.ObterRelacao).Result;
        }
        [HttpPost("ListarPorLocalidade")]
        public async Task<List<RegistroDTO>> ListarPorLocalidade(LocalidadeConsulta localidadeConsulta)
        {
            return _Requisicao.ExecutarRequisicao<LocalidadeConsulta, List<RegistroDTO>>(localidadeConsulta, _BLL.ListarPorLocalidade).Result;
        }

        [HttpPost("ObterReferencias")]
        public async Task<ReferenciaRetorno> ObterReferencias(RelacaoConsulta Relacao)
        {
            return _Requisicao.ExecutarRequisicao<int, ReferenciaRetorno>(Relacao.CodRegistro, _BLL.ObterReferencias).Result;
        }
    }
}
