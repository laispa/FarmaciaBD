using FarmaciaBD.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Controllers
{
    public class FornecedorController : Controller
    {

        public IActionResult Index()
        {

            List<Fornecedor> fornecedores = new List<Fornecedor>();


            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {

                conexao.Open();
                MySqlCommand comando = new MySqlCommand("select *from fornecedor;", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Fornecedor fornecedor = new Fornecedor
                    {
                        Matricula = Convert.ToInt32(reader["matricula"]),
                        Nome = reader["nome"].ToString(),
                        Localizacao = reader["localizacao"].ToString(),
                        Lote = Convert.ToInt32(reader["lote"])
                    };

                    fornecedores.Add(fornecedor);
                }
                reader.Close();

            }
            return View(fornecedores);
        }

        [HttpGet]
        public IActionResult Inserir()
        {

            return View(new Fornecedor());
        }

        [HttpPost]
        public IActionResult Inserir([FromForm] Fornecedor fornecedor)
        {
            //try
            //{
            if (ModelState.IsValid)
            {
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand("insert into fornecedor(nome, localizacao, lote) values('" + fornecedor.Nome + "','" + fornecedor.Localizacao + "'," + fornecedor.Lote + ");", conexao);

                    comando.ExecuteNonQuery();
                    conexao.Close();
                    TempData["Mensagem"] = "O fornecedor foi cadastrado com sucesso!";

                }
                return RedirectToAction("Index");
            }
                //return RedirectToAction("Index");
            //}
            /*catch (Exception)
            {
                TempData["Exception"] = "Erro";
            }*/

            return View("Inserir", fornecedor);
        }

        [HttpGet, ActionName("Deletar")]
        public IActionResult Deletar(int Numero)
        {
            Console.WriteLine(Numero);
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("DELETE FROM fornecedor WHERE matricula = '" + Numero + "'", conexao);

                comando.ExecuteNonQuery();

                TempData["Mensagem"] = "O fornecedor '" + Numero + "' foi excluído com sucesso!";
                conexao.Close();
            }
            return RedirectToAction("Index");
        }


        [HttpGet, ActionName("Update")]
        public IActionResult Update(int Numero)
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("SELECT *FROM fornecedor " +
                    "WHERE matricula ='" + Numero + "'", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Fornecedor fornecedor = new Fornecedor
                    {
                        Matricula = Convert.ToInt32(reader["matricula"]),
                        Nome = reader["nome"].ToString(),
                        Localizacao = reader["localizacao"].ToString(),
                        Lote = Convert.ToInt32(reader["lote"])
                    };

                    fornecedores.Add(fornecedor);
                }
                reader.Close();

            }
            return View("Atualizar", fornecedores);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Fornecedor fornecedor)
        {
            if (ModelState.IsValid) { 
            
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand("Update fornecedor set localizacao='" + fornecedor.Localizacao + "', nome='" + fornecedor.Nome + "', lote= " + fornecedor.Lote + " WHERE matricula=" + fornecedor.Matricula + "", conexao);
                    comando.ExecuteNonQuery();
                    TempData["Mensagem"] = "O fornecedor " + fornecedor.Matricula + "foi atualizado com sucesso!";
                    conexao.Close();
                }
                return RedirectToAction("Index");
            }
            return View("Atualizar", fornecedor);
        }
    }
}