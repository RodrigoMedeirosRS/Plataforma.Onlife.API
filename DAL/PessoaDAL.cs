using System;
using MoreLinq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BibliotecaViva.DAO;
using BibliotecaViva.DTO;
using BibliotecaViva.DAL.Utils;
using BibliotecaViva.DAL.Interfaces;

namespace BibliotecaViva.DAL
{
    public class PessoaDAL : BaseDAL, IPessoaDAL
    {
        private IPessoaRegistroDAL PessoaRegistroDAL { get; set; }
        public PessoaDAL(plataformaonlifeContext dataContext, IPessoaRegistroDAL pessoaRegistroDAL) : base(dataContext)
        {
            PessoaRegistroDAL = pessoaRegistroDAL;
        }
        public int Cadastrar(PessoaDTO pessoaDTO)
        {
            var pessoa = DataContext.Pessoas.AsNoTracking().FirstOrDefault(pessoa => pessoa.Codigo == pessoaDTO.Codigo);
            
            if(pessoa != null)
            {
                pessoa.Nome = pessoaDTO.Nome;
                DataContext.Update(pessoa);
                DataContext.SaveChanges();
            }
            else
            {
                pessoa = Conversor.Mapear(pessoaDTO);
                DataContext.Add(pessoa);
                DataContext.SaveChanges();
                pessoaDTO.Codigo = pessoa.Codigo;
            }

            PessoaRegistroDAL.VincularReferencia(pessoaDTO);
            return pessoa.Codigo;
        }
        public List<PessoaDTO> Consultar(PessoaDTO pessoaDTO)
        {
            var pessoas = (from pessoa in DataContext.Pessoas
                where 
                    (!string.IsNullOrEmpty(pessoaDTO.Nome) && pessoa.Nome.ToLower().Contains(pessoaDTO.Nome.ToLower())) 
                    || (!string.IsNullOrEmpty(pessoaDTO.Apelido) && pessoa.Apelido.ToLower().Contains(pessoaDTO.Apelido.ToLower()))
                    
                select
                    Conversor.Mapear(pessoa)).AsNoTracking().DistinctBy(pessoaDB => pessoaDB.Codigo).ToList();
                
            foreach(var pessoa in pessoas)
                pessoa.Relacoes = PessoaRegistroDAL.ObterRelacao((int)pessoa.Codigo);
            
            return pessoas;
        }
        public PessoaDTO Consultar(int codigoPessoa)
        {
            var pessoaDB = (from pessoa in DataContext.Pessoas
                where 
                    pessoa.Codigo == codigoPessoa
                
                select
                    Conversor.Mapear(pessoa)).AsNoTracking().FirstOrDefault();
                
            pessoaDB.Relacoes = PessoaRegistroDAL.ObterRelacao((int)pessoaDB.Codigo);
            
            return pessoaDB;
        }
    }
}