using MoreLinq;
using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BibliotecaViva.DAO;
using DTO;
using DAL.Utils;
using DAL.Interfaces;

namespace DAL
{
    public class RegistroDAL : BaseDAL, IRegistroDAL
    {
        private ITipoDAL TipoDAL { get ; set; }
        private IIdiomaDAL IdiomaDAL { get ; set; }
        private IReferenciaDAL ReferenciaDAL { get; set; }
        private ILocalidadeDAL LocalizacaoGeograficaDAL { get; set; }
        public RegistroDAL(plataformaonlifeContext dataContext, IIdiomaDAL idiomaDAL,ITipoDAL tipoDAL, ILocalidadeDAL localizacaoGeograficaDAL, IReferenciaDAL referenciaDAL) : base(dataContext)
        {
            TipoDAL = tipoDAL;
            IdiomaDAL = idiomaDAL;
            ReferenciaDAL = referenciaDAL;
            LocalizacaoGeograficaDAL = localizacaoGeograficaDAL;
        }
        public RegistroDTO Consultar(int codRegistro, bool completo)
        {
            var resultado = (from registro in DataContext.Registros.AsNoTracking()
                join
                    idioma in DataContext.Idiomas.AsNoTracking()
                    on registro.Idioma equals idioma.Codigo
                join
                    tipo in DataContext.Tipos.AsNoTracking()
                    on registro.Tipo equals tipo.Codigo
                where 
                    registro.Codigo == codRegistro
                select
                    Conversor.Mapear(registro, idioma.Nome, tipo.Nome, completo, 0)).AsNoTracking().FirstOrDefault();
            
            resultado.Referencias = ReferenciaDAL.ObterReferencia(resultado.Codigo);
            resultado.CodigoCidade = BuscarCidade(resultado.Codigo);
            return resultado;
        }

        public List<RegistroDTO> Consultar(RegistroDTO registroDTO, bool completo)
        {
            var registros = (from registro in DataContext.Registros
                join
                    idioma in DataContext.Idiomas
                    on registro.Idioma equals idioma.Codigo
                join
                    tipo in DataContext.Tipos
                    on registro.Tipo equals tipo.Codigo
                where
                    registroDTO.Idioma.ToLower() == idioma.Nome.ToLower() &&
                    ((!string.IsNullOrEmpty(registroDTO.Nome) && registro.Nome.ToLower().Contains(registroDTO.Nome.ToLower())) 
                    || ((!string.IsNullOrEmpty(registroDTO.Apelido) && registro.Apelido.ToLower().Contains(registroDTO.Apelido.ToLower()))))
                select
                    Conversor.Mapear(registro, idioma.Nome, tipo.Nome, completo, 0)).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).ToList(); 

            foreach(var registro in registros)
            {
                registro.Referencias = ReferenciaDAL.ObterReferencia(registro.Codigo);
                registro.CodigoCidade = BuscarCidade(registro.Codigo);
            }
            return registros;
        }
        public List<RegistroDTO> ListarPorLocalidade(int codigoLocalidade)
        {
            var registros = (from registroLocalidade in DataContext.Registrolocalidades
                join
                    registro in DataContext.Registros
                    on registroLocalidade.Registro equals registro.Codigo
                join
                    idioma in DataContext.Idiomas
                    on registro.Idioma equals idioma.Codigo
                join
                    tipo in DataContext.Tipos
                    on registro.Tipo equals tipo.Codigo
                where
                    registroLocalidade.Localidade == codigoLocalidade
                select
                    Conversor.Mapear(registro, idioma.Nome, tipo.Nome, false, registroLocalidade.Localidade)).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).ToList();
            
            foreach(var registro in registros)
                registro.Referencias = ReferenciaDAL.ObterReferencia(registro.Codigo);
            
            return registros;
        }
        public int Cadastrar(RegistroDTO registroDTO)
        {
            var registro = DataContext.Registros.AsNoTracking().FirstOrDefault(registro => registro.Nome.ToLower() == registroDTO.Nome.ToLower());
            if(registro != null)
            {
                registroDTO.Codigo = registro.Codigo;
                DataContext.Update(MapearRegistro(registroDTO));
                DataContext.SaveChanges();
            }
            else
            {
                registro = MapearRegistro(registroDTO);
                DataContext.Add(registro);
                DataContext.SaveChanges();
                registroDTO.Codigo = registro.Codigo;
            }
            RegistrarLocalidade(registroDTO);
            ReferenciaDAL.VincularReferencia(registroDTO);
            return registro.Codigo;
        }
        private void RegistrarLocalidade(RegistroDTO registroDTO)
        {
            if (registroDTO.CodigoCidade != 0)
            {
                var cidade = DataContext.Registrolocalidades.AsNoTracking().FirstOrDefault(localidade => localidade.Localidade == registroDTO.Codigo && localidade.Registro == registroDTO.Codigo);
                if (cidade != null)
                    return;
                var relacao = new Registrolocalidade()
                {
                    Localidade = registroDTO.CodigoCidade,
                    Registro = registroDTO.Codigo
                };
                DataContext.Add(relacao);
                DataContext.SaveChanges();
            }
        }
        private int BuscarCidade(int codigoRegistro)
        {
            var resultado = DataContext.Registrolocalidades.AsNoTracking().FirstOrDefault(localidade => localidade.Registro == codigoRegistro);
            return resultado != null ? resultado.Localidade : 0;
        }
        private Registro MapearRegistro(RegistroDTO registroDTO)
        {
            var idioma = IdiomaDAL.Consultar(new IdiomaDTO(){ Nome = registroDTO.Idioma });
            var tipo = TipoDAL.Consultar(new TipoDTO(){ Nome = registroDTO.Tipo });

            return new Registro()
            {
                Codigo = (int)registroDTO.Codigo,
                Idioma = (int)idioma.Codigo,
                Tipo = (int)tipo.Codigo,
                Nome = registroDTO.Nome,
                Apelido = registroDTO.Apelido,
                Conteudo = registroDTO.Conteudo,
                Datainsercao = DateTime.Now,
                Latitude = registroDTO.Latitude,
                Longitude = registroDTO.Longitude,
                Descricao = registroDTO.Descricao
            };
        }
    }
}