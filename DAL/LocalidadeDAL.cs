using System.Linq;
using MoreLinq;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class LocalidadeDAL : BaseDAL, ILocalidadeDAL
    {
        public LocalidadeDAL(plataformaonlifeContext dataContext) : base(dataContext)
        {

        }
        public int Cadastrar(DTO.Localidade localidadeDTO)
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
        public DTO.Localidade Consultar(string nome)
        {
            var resultado = (from localidade in DataContext.Localidades
                where
                    !string.IsNullOrEmpty(localidade.Nome) && localidade.Nome.ToLower().Contains(nome.ToLower())
                select
                    Conversor.Mapear(localidade)).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).FirstOrDefault();
            return resultado;
        }
        public DTO.Localidade Consultar(int codigo)
        {
            var resultado = (from localidade in DataContext.Localidades
                where
                    localidade.Codigo == codigo
                select
                    Conversor.Mapear(localidade)).AsNoTracking().DistinctBy(registroDB => registroDB.Codigo).FirstOrDefault();
            return resultado;
        }
        public void Vincular(DTO.Localidade localizacaoGeograficaDTO, RegistroDTO registroDTO)
        {
            DataContext.Registrolocalidades.Add(new Registrolocalidade()
            {
                Registro = (int)registroDTO.Codigo,
                Localidade = (int)localizacaoGeograficaDTO.Codigo
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