using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using BibliotecaViva.DAO;
using DTO;
using DAL.Utils;
using DAL.Interfaces;

namespace DAL
{
    public class TipoExecucaoDAL : BaseDAL, ITipoExecucaoDAL
    {
        public TipoExecucaoDAL(plataformaonlifeContext dataContext) : base(dataContext)
        {

        }
        public List<TipoExecucaoDTO> Listar()
        {
            return (from tipo in DataContext.Tipodeexecucaos
                orderby
                    tipo.Codigo
                select
                    Conversor.Mapear(tipo)).ToList();
        }
    }
}