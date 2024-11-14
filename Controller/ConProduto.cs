using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Models;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vendas.Controller
{
    public class ConProduto
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public int quantidade { get; set; }
        public decimal preco { get; set; }

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Vendas2\\DbVenda.mdf;Integrated Security=True");
        Produto produto = new Produto();
        public List<Produto> listaproduto()
        {
            List<Produto> li = new List<Produto>();
            string sql = "SELECT * FROM Produto";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            {
                Produto produto = new Produto();
                produto.Id = (int)dr["Id"];
                produto.nome = dr["nome"].ToString();
                produto.quantidade = (int)dr["quantidade"];
                produto.preco = (decimal)dr["preco"];
                li.Add(produto);
            }
            dr.Close();
            con.Close();
            return li;
        }

        public void Inserir(string nome, int quantidade, string preco)
        {
            try
            {
                decimal precofinal = Convert.ToDecimal(preco) / 100;
                string sql = "INSERT INTO Produto(nome,quantidade,preco) VALUES ('"+nome+"','"+quantidade+"',@preco)";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@preco", SqlDbType.Decimal).Value = precofinal;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public void Atualizar(int Id, string nome, int quantidade, string preco)
        {
            try
            {
                decimal precofinal = Convert.ToDecimal(preco);
                string sql = "UPDATE Produto SET nome='" + nome + "',quantidade='" + quantidade + "', preco=@preco WHERE Id='" + Id + "'";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@preco", SqlDbType.Decimal).Value = precofinal;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public void Excluir(int Id)
        {
            try
            {
                string sql = "DELETE FROM Produto WHERE Id='"+Id+"'";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        public void Localizar(int Id)
        {
            string sql = "SELECT * FROM Produto WHERE Id='"+Id+"'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                nome = dr["nome"].ToString();
                quantidade = (int)dr["quantidade"];
                preco = (decimal)dr["preco"];
            }
            dr.Close();
            con.Close();
        }

        public bool RegistroRepetido(string nome)
        {
            string sql = "SELECT * FROM Produto WHERE nome='" + nome + "'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }
    }
}
    