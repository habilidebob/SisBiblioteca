using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisBiblioteca.Banco
{
    static class LivroDAO
    {
        public static bool Cadastrar(Classes.Livro livro)
        {
            string comando;
            comando = "INSERT INTO livros (isbn, titulo, autor, ano, paginas) VALUES (@isbn, @titulo, @autor, @ano, @paginas);";

            // Instanciar a classe ConexaoBD:
            ConexaoBD conexaoBD = new ConexaoBD();
            MySqlConnection con = conexaoBD.ObterConexao();

            // Instanciar o objeto do tipo "MySqlCommand":
            MySqlCommand cmd = new MySqlCommand(comando, con);
            // Substituir os coringas por valores vindos do usuario:
            cmd.Parameters.AddWithValue("@isbn", livro.Isbn);
            cmd.Parameters.AddWithValue("@titulo", livro.Titulo);
            cmd.Parameters.AddWithValue("@autor", livro.Autor);
            cmd.Parameters.AddWithValue("@ano", livro.Ano);
            cmd.Parameters.AddWithValue("@paginas", livro.Paginas);
            
            cmd.Prepare();
            if(cmd.ExecuteNonQuery() == 0)
            {
                conexaoBD.Desconectar(con);
                return false;
            }
            else
            {
                conexaoBD.Desconectar(con);
                return true;
            }
        }
        public static bool Apagar(int id)
        {
            string comando;
            comando = "DELETE FROM livros WHERE id = @id";
            ConexaoBD conexaoBD = new ConexaoBD();
            MySqlConnection con = conexaoBD.ObterConexao();
            MySqlCommand cmd = new MySqlCommand(comando, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            if (cmd.ExecuteNonQuery() == 0)
            {
                conexaoBD.Desconectar(con);
                return false;
            }
            else
            {
                conexaoBD.Desconectar(con);
                return true;
            }
        }
        public static DataTable ListarTudo()
        {
            // Instanciar a "tabela" onde o res da consulta será armazenado:
            DataTable tabela = new DataTable();
            string comando;
            comando = "SELECT * FROM livros";
            ConexaoBD conexaoBD = new ConexaoBD();
            MySqlConnection con = conexaoBD.ObterConexao();
            MySqlCommand cmd = new MySqlCommand(comando, con);
            cmd.Prepare();
            tabela.Load(cmd.ExecuteReader());
            conexaoBD.Desconectar(con);
            return tabela;
        }
        public static DataTable Buscar(string termo)
        {
            // Instanciar a "tabela" onde o res da consulta será armazenado:
            DataTable tabela = new DataTable();
            string comando;
            comando = "SELECT * FROM livros WHERE titulo LIKE CONCAT(@termo, '%')";
            ConexaoBD conexaoBD = new ConexaoBD();
            MySqlConnection con = conexaoBD.ObterConexao();
            MySqlCommand cmd = new MySqlCommand(comando, con);
            // Subsituir o @termo pela variavel vinda por parâmetro
            cmd.Parameters.AddWithValue("@termo", termo);
            cmd.Prepare();
            tabela.Load(cmd.ExecuteReader());
            conexaoBD.Desconectar(con);
            return tabela;
        }


    }
}
