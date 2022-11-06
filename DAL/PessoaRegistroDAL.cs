using MoreLinq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BibliotecaViva.DAO;
using DTO;
using DAL.Utils;
using DAL.Interfaces;


namespace DAL
{
    public class PessoaRegistroDAL : BaseDAL, IPessoaRegistroDAL
    {
        private ITipoRelacaoDAL TipoRelacaoDAL { get; set; }
        private IRegistroDAL RegistroDAL { get; set; }
        private IReferenciaDAL ReferenciaDAL { get; set; }
        public PessoaRegistroDAL(plataformaonlifeContext dataContext, ITipoRelacaoDAL tipoRelacaoDAL, IRegistroDAL registroDAL, IReferenciaDAL referenciaDAL) : base(dataContext)
        {
            TipoRelacaoDAL = tipoRelacaoDAL;
            RegistroDAL = registroDAL;
            ReferenciaDAL = referenciaDAL;
        }
        public void VincularReferencia(PessoaDTO pessoaDTO)
        {
            var relacoes = ListarRelacoes((int)pessoaDTO.Codigo);

            foreach (var ralacao in relacoes)
                DataContext.Pessoaregistros.Remove(ralacao);

            foreach (var relacao in pessoaDTO.Relacoes)
                DataContext.Add(new Pessoaregistro()
                {
                    Pessoa = pessoaDTO.Codigo,
                    Registro = (int)relacao.RelacaoID,
                    Tiporelacao = (int)TipoRelacaoDAL.Consultar(new TipoRelacaoDTO()
                    {
                        Nome = relacao.TipoRelacao
                    }).Codigo
                });
            DataContext.SaveChanges();
        }
        public List<RelacaoDTO> ObterRelacao(int codPessoa)
        {
            var relacoes = (from relacao in DataContext.Pessoaregistros
                join
                    tiporelacao in DataContext.Tiporelacaos
                    on relacao.Tiporelacao equals tiporelacao.Codigo
                where 
                    relacao.Pessoa == codPessoa
                select 
                    new RelacaoDTO()
                    {
                        Codigo = relacao.Codigo,
                        RegistroPessoaID = (int)relacao.Pessoa,
                        RelacaoID = (int)relacao.Registro,
                        TipoRelacao = tiporelacao.Nome
                    }).AsNoTracking().ToList();
            return relacoes;
        }
        public List<RegistroDTO> ObterRelacaoCompleta(PessoaDTO pessoaDTO)
        {
            var registros = (from pessoaRelacao in DataContext.Pessoaregistros
                join         
                    registro in DataContext.Registros
                    on pessoaRelacao.Registro equals registro.Codigo
                join
                    idioma in DataContext.Idiomas
                    on registro.Idioma equals idioma.Codigo
                join
                    tipo in DataContext.Tipos
                    on registro.Tipo equals tipo.Codigo
                where 
                    pessoaRelacao.Pessoa == pessoaDTO.Codigo 
                select 
                    Conversor.Mapear(registro, tipo.Nome, false, 0)).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).ToList();
            
            foreach(var registro in registros)
            {
                registro.Referencias = ReferenciaDAL.ObterReferencia(registro.Codigo);
                registro.CodigoCidade = BuscarCidade(registro.Codigo);
            }
                
            return registros;
        }
        private int BuscarCidade(int codigoRegistro)
        {
            var resultado = DataContext.Registrolocalidades.AsNoTracking().FirstOrDefault(localidade => localidade.Registro == codigoRegistro);
            return resultado != null ? resultado.Localidade : 0;
        }
        private IQueryable<Pessoaregistro> ListarRelacoes(int codPessoa)
        {
            return DataContext.Pessoaregistros.Where(relacao => relacao.Pessoa == codPessoa).AsNoTracking();
        }
    }
}