using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        public static object SqlLoggerProvider { get; private set; }

        static void Main(string[] args)
        {

            var p1 = new Produto() { Nome= "Suco",Categoria = "Bebidas",PrecoUnitario=2.9,Unidade= "Litros"};
            var p2 = new Produto() { Nome = "Cafe", Categoria = "Bebidas", PrecoUnitario = 22.9, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrao", Categoria = "Alimentos", PrecoUnitario = 4.2, Unidade = "Gramas" }; 

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataFim = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluirProduto(p1);
            promocaoDePascoa.IncluirProduto(p2);
            promocaoDePascoa.IncluirProduto(p3);


            using (var contexto = new LojaContext())
            {

                //   contexto.Promocoes.Add(promocaoDePascoa);
                var promocao = contexto.Promocoes.Find(1);
                contexto.Promocoes.Remove(promocao);

                contexto.SaveChanges();
            }
            //  promocaoDePascoa.Produtos.Add(new Produto());


            //var paoFrances = new Produto();
            //paoFrances.Nome = "Pao Frances";
            //paoFrances.PrecoUnitario = 0.40;
            //paoFrances.Unidade = "Unitario";
            //paoFrances.Categoria = "Padaria";

            //var compra = new Compra();
            //compra.Quantidade = 6;
            //compra.Produto = paoFrances;
            //compra.Preco = compra.Quantidade * paoFrances.PrecoUnitario;

            //using(var contexto = new LojaContext())
            //{
            //    contexto.Add(compra);
            //    contexto.SaveChanges();
            //}

        }

    }
}
