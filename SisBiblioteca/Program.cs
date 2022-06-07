using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisBiblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string opc = "";
            while(opc != "5")
            {
                MostrarMenu();
                opc = Console.ReadLine();
                switch (opc)
                {
                    case "1":
                        MenuCadastrarLivro();
                        break;

                    case "2":
                        MenuApagarPorID();
                        break;

                    case "3":
                        MenuListarTudo();
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "4":
                        MenuBuscar();
                        break;

                    case "5":
                        Console.Clear();
                        //Console.WriteLine("ENCERRADO");
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
           
        }

        // Bloquinhos organizados:
        static void MostrarMenu()
        {
            Console.WriteLine("*** SIS-BIBLIOTECA ***");
            Console.WriteLine("1 - CADASTRAR NOVO LIVRO");
            Console.WriteLine("2 - APAGAR UM LIVRO");
            Console.WriteLine("3 - LISTAR TUDO");
            Console.WriteLine("4 - BUSCAR");
            Console.WriteLine("5 - SAIR");
            Console.Write("Escolha uma opção: ");
        }
        //Bloco para cadastrar um novo livro:
        static void MenuCadastrarLivro()
        {
            // Instanciar o livro:
            Classes.Livro livro = new Classes.Livro();
            // Obter as informações do livro:
            Console.Write("ISBN: ");
            livro.Isbn = Console.ReadLine();
            Console.Write("Titulo: ");
            livro.Titulo = Console.ReadLine();
            Console.Write("Autor: ");
            livro.Autor = Console.ReadLine();
            Console.Write("Ano: ");
            livro.Ano = int.Parse(Console.ReadLine());
            Console.Write("Quantidade de Páginas: ");
            livro.Paginas = int.Parse(Console.ReadLine());
            // Enviar o livro ao BD pelo DAO:
            if (Banco.LivroDAO.Cadastrar(livro))
            {
                Console.WriteLine("Livro cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro! Verifique as informações digitadas.");
            }
            Console.ReadKey();
            Console.Clear();
        }
        static void MenuApagarPorID()
        {
            MenuListarTudo();
            Console.Write("ID do livro que deseja apagar: ");
            int id = int.Parse(Console.ReadLine());
            // Chamar o apagar do DAO:
            if (Banco.LivroDAO.Apagar(id))
            {
                Console.WriteLine("Livro removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro! Verifique as informações digitadas.");
            }
            Console.ReadKey();
            Console.Clear();
        }
        static void MenuListarTudo()
        {
            DataTable tabela = new DataTable();
            // Atribuir na tabela o resultado da consulta:
            tabela = Banco.LivroDAO.ListarTudo();
            //Console.WriteLine(tabela.Rows[0]["titulo"]);
            Console.WriteLine("======================");
            Console.WriteLine("ID\tISBN\t\tTitulo\t\tAutor\t\tAno\tPaginas");
            foreach(DataRow linha in tabela.Rows)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}" +
                    "", linha["id"], linha["isbn"], linha["titulo"], linha["autor"]
                    , linha["ano"], linha["paginas"]);
            }
            
        }
        static void MenuBuscar()
        {
            string busca;
            DataTable tabela = new DataTable(); 

            Console.Write("Nome do livro: ");
            busca = Console.ReadLine();
            tabela = Banco.LivroDAO.Buscar(busca);
            // Console.WriteLine(tabela.Rows.Count); // Qtd de resultados vindos do BD:
            // Verificar se a tabela resultado tem mais de 1 linha:
            if(tabela.Rows.Count > 0)
            {
                // Apresentar o resultado da busca:
                foreach (DataRow linha in tabela.Rows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}" +
                        "", linha["id"], linha["isbn"], linha["titulo"], linha["autor"]
                        , linha["ano"], linha["paginas"]);
                }
            }
            else
            {
                Console.WriteLine("Nada encontrado para \"{0}\"!", busca);
            }
            Console.ReadKey();
            Console.Clear();
        }
    }
}
