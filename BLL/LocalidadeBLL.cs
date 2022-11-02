using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}