using System;

using BibliotecaViva.DAO;
using DTO;
using DTO.Utils;

namespace DAL.Utils
{
    internal static class Conversor
    {
        internal static Idioma Mapear(IdiomaDTO idioma)
        {
            return idioma != null ? new Idioma()
            {
                Codigo = idioma.Codigo,
                Nome = idioma.Nome
            } : null;
        }
        internal static IdiomaDTO Mapear(Idioma idioma)
        {
            return idioma != null ? new IdiomaDTO()
            {
                Codigo = idioma.Codigo,
                Nome = idioma.Nome
            } : null;
        }
        internal static Tipo Mapear(TipoDTO tipo)
        {
            return tipo != null ? new Tipo()
            {
                Codigo = tipo.Codigo,
                Nome = tipo.Nome,
                Extensao = tipo.Extensao,
                Tipodeexecucao = (int)tipo.TipoExecucao
            } : null;
        }
        internal static TipoDTO Mapear(Tipo tipo)
        {
            return tipo != null ? new TipoDTO()
            {
                Codigo = tipo.Codigo,
                Nome = tipo.Nome,
                Extensao = tipo.Extensao,
                TipoExecucao = (TipoExecucao)tipo.Tipodeexecucao,
                Binario = TratadorUtil.VerificarBinario((TipoExecucao)tipo.Tipodeexecucao)
            } : null;
        }
        internal static TipoExecucaoDTO Mapear(Tipodeexecucao tipo)
        {
            return tipo != null ? new TipoExecucaoDTO()
            {
                Codigo = tipo.Codigo,
                Nome = tipo.Nome,
                Binario = tipo.Binario
            } : null;
        }
        internal static Tipodeexecucao Mapear(TipoExecucaoDTO tipo)
        {
            return tipo != null ? new Tipodeexecucao()
            {
                Codigo = tipo.Codigo,
                Nome = tipo.Nome,
                Binario  = tipo.Binario
            } : null;
        }
        internal static BibliotecaViva.DAO.Localidade Mapear(DTO.LocalidadeDTO localidade)
        {
            return localidade != null ? new BibliotecaViva.DAO.Localidade()
            {
                Codigo = localidade.Codigo,
                Nome = localidade.Nome,
                Descricao = localidade.Descricao,
                Mapa = localidade.Mapa,
                X = Convert.ToDouble(localidade.X),
                Y = Convert.ToDouble(localidade.Y),
                Z = Convert.ToDouble(localidade.Z)
            } : null;
        }
        internal static DTO.LocalidadeDTO Mapear(BibliotecaViva.DAO.Localidade localidade, bool completo)
        {
            return localidade != null ? new DTO.LocalidadeDTO()
            {
                Codigo = localidade.Codigo,
                Nome = localidade.Nome,
                Descricao = localidade.Descricao,
                Mapa = completo ? localidade.Mapa : string.Empty,
                X = Convert.ToSingle(localidade.X),
                Y = Convert.ToSingle(localidade.Y),
                Z = Convert.ToSingle(localidade.Z)
            } : null;
        }
        internal static Pessoa Mapear(PessoaDTO pessoa)
        {
            return pessoa != null ? new Pessoa()
            {
                Codigo = pessoa.Codigo,
                Nome = pessoa.Nome,
                Apelido = pessoa.Apelido,
                Foto = pessoa.Foto,
                Researchgate = pessoa.ResearchGate,
                Linkedin = pessoa.LinkedIn,
                Lattes = pessoa.Lattes
            } : null;
        }
        internal static PessoaDTO Mapear(Pessoa pessoa)
        {
            return pessoa != null ? new PessoaDTO()
            {
                Codigo = pessoa.Codigo,
                Nome = pessoa.Nome,
                Apelido = pessoa.Apelido,
                Foto = pessoa.Foto,
                ResearchGate = pessoa.Researchgate,
                LinkedIn = pessoa.Linkedin,
                Lattes = pessoa.Lattes
            } : null;
        }
        internal static Tiporelacao Mapear(TipoRelacaoDTO tipoRelacao)
        {
            return tipoRelacao != null ? new Tiporelacao()
            {
                Codigo = tipoRelacao.Codigo,
                Nome = tipoRelacao.Nome
            } : null;
        }
        internal static TipoRelacaoDTO Mapear(Tiporelacao tipoRelacao)
        {
            return tipoRelacao != null ? new TipoRelacaoDTO()
            {
                Codigo = tipoRelacao.Codigo,
                Nome = tipoRelacao.Nome
            } : null;
        }
        internal static RegistroDTO Mapear (Registro registro, string tipo, bool completo, int codigoLocalidade = 0)
        {
            return registro != null ? new RegistroDTO()
            {
                Codigo = registro.Codigo,
                Nome = registro.Nome,
                Tipo = tipo,
                Conteudo = completo ? registro.Conteudo : string.Empty,
                DataInsercao = registro.Datainsercao,
                Latitude = Convert.ToSingle(registro.Latitude),
                Longitude = Convert.ToSingle(registro.Longitude),
                Descricao = registro.Descricao,
                CodigoCidade = codigoLocalidade
            } : null;
        }
        internal static Registro Mapear (RegistroDTO registro)
        {
            return registro != null ? new Registro()
            {
                Codigo = registro.Codigo,
                Nome = registro.Nome,
                Conteudo = registro.Conteudo,
                Datainsercao = registro.DataInsercao,
                Latitude = registro.Latitude,
                Longitude = registro.Longitude,
                Descricao = registro.Descricao
            } : null;
        }
    }
}