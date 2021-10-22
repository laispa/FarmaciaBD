using FarmaciaBD.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Controllers
{
    public class LojaController : Controller
    {
        public IActionResult Index()
        {
            List<Loja> lojas = new List<Loja>();
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {

                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select *from loja", conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Loja loja = new Loja();
                    loja.Numero = Convert.ToInt32(reader["numero"]);
                    loja.Cidade = reader["cidade"].ToString();
                    loja.Endereco = reader["endereco"].ToString();

                    lojas.Add(loja);
                }
                reader.Close();

            }
            return View(lojas);
        }
        public IActionResult Inserir([FromForm] Loja loja) {
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO loja (cidade, endereco) VALUES('" + loja.Cidade + "','" + loja.Endereco + "');", conexao);
                /*cmd.Parameters.AddWithValue("?cidade", loja.Cidade);
                cmd.Parameters.AddWithValue("?endereco", loja.Endereco);*/

                cmd.ExecuteNonQuery();

                conexao.Close();
            }

            return View("Inserir", loja);
        }

        public IActionResult Deletar([FromForm]Loja loja)
        {
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM loja (cidade, endereco) WHERE ,",conexao);

            }
                return View();
        }
    }

}
