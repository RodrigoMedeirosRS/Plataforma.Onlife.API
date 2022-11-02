using System;
using Newtonsoft.Json;
using BLL.Interfaces;

namespace BLL
{
    public abstract class BaseBLL : IBaseBLL
    {
        
        public virtual string SerializarRetorno(object dado)
        {
            return dado != null ? JsonConvert.SerializeObject(dado) : throw new Exception("Pessoa Não Encontrada");
        }
    }
}