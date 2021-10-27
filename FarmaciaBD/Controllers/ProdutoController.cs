using FarmaciaBD.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Controllers
{
    public class ProdutoController : Controller
    {

        public IActionResult Index()
        {

            List<Produto> produtos = new List<Produto>();


            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {

                conexao.Open();
                MySqlCommand comando = new MySqlCommand("select *from produto;", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Produto produto = new Produto
                    {
                        Codigo_de_Barras = Convert.ToInt32(reader["codigo_de_Barras"]),
                        Estoque = Convert.ToInt32(reader["estoque"]),
                        Valor = Convert.ToSingle(reader["valor"]),
                        Fornecedor_Matricula = Convert.ToInt32(reader["fornecedor_matricula"])

                    };

                    produtos.Add(produto);
                }
                reader.Close();

            }
            return View(produtos);
        }

        [HttpGet]
        public IActionResult Inserir()
        {

            return View(new Produto());
        }

        [HttpPost]
        public IActionResult Inserir([FromForm] Produto produto)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand("insert into produto(estoque, valor, fornecedor_matricula) values(" + produto.Estoque + "," + produto.Valor + "," + produto.Fornecedor_Matricula +");", conexao);

                    comando.ExecuteNonQuery();
                    conexao.Close();
                    TempData["Mensagem"] = "O produto foi cadastrado com sucesso!";

                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Exception"] = "Você digitou um fornecedor existente?";
            }

            return View("Inserir", produto);
        }

        [HttpGet, ActionName("Deletar")]
        public IActionResult Deletar(int Numero)
        {
            Console.WriteLine(Numero);
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("DELETE FROM produto WHERE codigo_de_barras = '" + Numero + "'", conexao);

                comando.ExecuteNonQuery();

                TempData["Mensagem"] = "O produto'" + Numero + "' foi excluído com sucesso!";
                conexao.Close();
            }
            return RedirectToAction("Index");
        }


        [HttpGet, ActionName("Update")]
        public IActionResult Update(int Numero)
        {

            List<Produto> produtos = new List<Produto>();
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("SELECT *FROM produto " +
                    "WHERE codigo_de_barras ='" + Numero + "'", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Produto produto = new Produto
                    {
                        Codigo_de_Barras = Convert.ToInt32(reader["codigo_de_Barras"]),
                        Estoque = Convert.ToInt32(reader["estoque"]),
                        Valor = Convert.ToSingle(reader["valor"]),
                        Fornecedor_Matricula = Convert.ToInt32(reader["fornecedor_matricula"])

                    };
                    produtos.Add(produto);

                }
                reader.Close();

            }
            return View("Atualizar", produtos);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Produto produto)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand(
                        "Update produto set estoque=" + produto.Estoque + ", valor=" + produto.Valor + ", fornecedor_matricula= " + produto.Fornecedor_Matricula + "", conexao);

                    comando.ExecuteNonQuery();

                    TempData["Mensagem"] = "O produto '" + produto.Codigo_de_Barras + "' foi atualizado com sucesso!";
                    conexao.Close();
                }
                return RedirectToAction("Index");
            }
            /*
            }
            catch (Exception)
            {
                TempData["Exception"] = "Você digitou uma loja existente? Verifique também o formato da data";
            }*/
            return View("Atualizar", produto);
        }
    }
}

