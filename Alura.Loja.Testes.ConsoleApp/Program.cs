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

        static void Main(string[] args)
        {
            using(var contexto = new LojaContext())
            {
                var cliente = contexto
                                    .Clientes
                                    .Include(c => c.EnderecoEntrega)
                                    .FirstOrDefault();
                Console.WriteLine($"Endereço de entrega: {cliente.EnderecoEntrega.Logradouro}");

                Console.WriteLine("Produto comprado");
                var produto = contexto
                                    .Produtos
                             //       .Include(p => p.Compras)
                                    .Where(p => p.Id == 1)
                                    .FirstOrDefault();

                contexto.Entry(produto)
                          .Collection(p => p.Compras)
                          .Query()
                          .Where(c => c.Preco > 1)
                          .Load();

                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(produto.Nome);
                }
            }

            
        }


        private static void listarProdutoJoin()
        {
            using(var contexto2 = new LojaContext())
            {
                var promocao = contexto2
                                .Promocoes
                                .Include(p => p.Produtos)
                                .ThenInclude(pp => pp.Produto)
                                .FirstOrDefault();


    Console.WriteLine("\n Mostrar os produtos da promoção...");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.Produto);
                }
            }
        }

        private static void IncluiPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Queima total";
                promocao.DataInicio = new DateTime(2019, 1, 1);
                promocao.DataFim = new DateTime(2019, 1, 31);

                var produto = contexto
                               .Produtos
                               .Where(p => p.Categoria == "Bebidas")
                               .ToList();
                foreach (var item in produto)
                {
                    promocao.IncluirProduto(item);
                }

                contexto.Promocoes.Add(promocao);
                contexto.SaveChanges();

            }
        }

        private static void MuntosParaMuntos()
        {

            var p1 = new Produto() { Nome = "Suco", Categoria = "Bebidas", PrecoUnitario = 2.9, Unidade = "Litros" };
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
    }

        private static void umPraum()
        {
            var fulano = new Cliente();

        fulano.Nome = "David";
            fulano.EnderecoEntrega = new Endereco()
        {
            Numero = 123,
                Logradouro = "Rua X",
                Complemento = "Ap 123",
                Bairro = "Centro",
                Cidade = "Uberlandia"
            };
            using(var contexto = new LojaContext())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }
        }
    }
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