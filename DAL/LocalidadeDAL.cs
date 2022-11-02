using System.Linq;
using System.Collections.Generic;
using MoreLinq;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using DTO;
using DAL.Utils;
using DAL.Interfaces;

namespace DAL
{
    public class LocalidadeDAL : BaseDAL, ILocalidadeDAL
    {
        public LocalidadeDAL(plataformaonlifeContext dataContext) : base(dataContext)
        {

        }
        public int Cadastrar(LocalidadeDTO localidadeDTO)
        {
            var localidade = DataContext.Localidades.AsNoTracking().FirstOrDefault(localidade => localidade.Codigo == localidadeDTO.Codigo);
            
            if(localidade != null)
            {
                localidade.Nome = localidadeDTO.Nome;
                DataContext.Update(localidade);
                DataContext.SaveChanges();
            }
            else
            {
                localidade = Conversor.Mapear(localidadeDTO);
                DataContext.Add(localidade);
                DataContext.SaveChanges();
                localidadeDTO.Codigo = localidade.Codigo;
            }

            return localidade.Codigo;
        }
        public LocalidadeDTO Consultar(string nome, bool completo)
        {
            var resultado = (from localidade in DataContext.Localidades
                where
                    !string.IsNullOrEmpty(localidade.Nome) && localidade.Nome.ToLower().Contains(nome.ToLower())
                select
                    Conversor.Mapear(localidade, completo)).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).FirstOrDefault();
            return resultado;
        }
        public LocalidadeDTO Consultar(int codigo, bool completo)
        {
            var resultado = (from localidade in DataContext.Localidades
                where
                    localidade.Codigo == codigo
                select
                    Conversor.Mapear(localidade, completo)).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).FirstOrDefault();
            return resultado;
        }
        public List<LocalidadeDTO> Listar()
        {
            var resultado =  (from localidade in DataContext.Localidades
                select
                    Conversor.Mapear(localidade, false)).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).ToList();
            return resultado;
        }
        public void Vincular(LocalidadeDTO localidadeDTO, RegistroDTO registroDTO)
        {
            DataContext.Registrolocalidades.Add(new Registrolocalidade()
            {
                Registro = (int)registroDTO.Codigo,
                Localidade = (int)localidadeDTO.Codigo
            });
            DataContext.SaveChanges();
        }
        public void RemoverVinculoRegistro(int? codigoRegistro)
        {
            var localizacao = DataContext.Registrolocalidades.AsNoTracking().FirstOrDefault(localizacao => localizacao.Registro == codigoRegistro);
            if (localizacao != null)
                DataContext.Remove(localizacao);
            DataContext.SaveChanges();
        }  
    }
}