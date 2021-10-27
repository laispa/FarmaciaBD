using FarmaciaBD.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Controllers
{
    public class MedicamentoController : Controller
    {

        public IActionResult Index()
        {

            List<Medicamento> medicamentos = new List<Medicamento>();


            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {

                conexao.Open();
                MySqlCommand comando = new MySqlCommand("select *from medicamento;", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Medicamento medicamento = new Medicamento
                    {
                        Codigo = Convert.ToInt32(reader["codigo"]),
                        Laboratorio = reader["laboratorio"].ToString(),
                        Nome = reader["nome"].ToString(),
                        Composicao = reader["composicao"].ToString(),
                        Tarja = reader["tarja"].ToString(),
                        Tipo = reader["tipo"].ToString(),
                        Produto_codigo_de_barras = Convert.ToInt32(reader["produto_codigo_de_barras"])

                };

                    medicamentos.Add(medicamento);
                }
                reader.Close();

            }
            return View(medicamentos);
        }

        [HttpGet]
        public IActionResult Inserir()
        {

            return View(new Medicamento());
        }

        [HttpPost]
        public IActionResult Inserir([FromForm] Medicamento medicamento)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand("insert into medicamento(laboratorio, nome, composicao, tarja, tipo, produto_codigo_de_barras) values('" + medicamento.Laboratorio + "','" + medicamento.Nome + "','" + medicamento.Composicao + "','" + medicamento.Tarja + "','" + medicamento.Tipo + "'," + medicamento.Produto_codigo_de_barras + ");", conexao);

                    comando.ExecuteNonQuery();
                    conexao.Close();
                    TempData["Mensagem"] = "O medicamento foi cadastrado com sucesso!";

                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["Exception"] = "Você digitou um codigo de produto existente?";
            }

            return View("Inserir", medicamento);
        }

        [HttpGet, ActionName("Deletar")]
        public IActionResult Deletar(int Numero)
        {
            Console.WriteLine(Numero);
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("DELETE FROM medicamento WHERE codigo = '" + Numero + "'", conexao);

                comando.ExecuteNonQuery();

                TempData["Mensagem"] = "O medicamento '" + Numero + "' foi excluído com sucesso!";
                conexao.Close();
            }
            return RedirectToAction("Index");
        }


        [HttpGet, ActionName("Update")]
        public IActionResult Update(int Numero)
        {

            List<Medicamento> medicamentos = new List<Medicamento>();
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("SELECT *FROM medicamento " +
                    "WHERE codigo ='" + Numero + "'", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Medicamento medicamento = new Medicamento
                    {
                        Codigo = Convert.ToInt32(reader["codigo"]),
                        Laboratorio = reader["laboratorio"].ToString(),
                        Nome = reader["nome"].ToString(),
                        Composicao = reader["composicao"].ToString(),
                        Tarja = reader["tarja"].ToString(),
                        Tipo = reader["tipo"].ToString(),
                        Produto_codigo_de_barras = Convert.ToInt32(reader["produto_codigo_de_barras"])

                    };
                    medicamentos.Add(medicamento);

                }
                reader.Close();

            }
            return View("Atualizar", medicamentos);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand(
                        "Update medicamento set laboratorio='" + medicamento.Laboratorio + "', nome='" + medicamento.Nome + "', composicao= '" + medicamento.Composicao + "', tarja='" + medicamento.Tarja + "', tipo='" + medicamento.Tipo + "',produto_codigo_de_barras='" + medicamento.Produto_codigo_de_barras + "' WHERE Codigo=" + medicamento.Codigo + "", conexao);

                    comando.ExecuteNonQuery();

                    TempData["Mensagem"] = "O Medicamento " + medicamento.Codigo + " foi atualizado com sucesso!";
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
            return View("Atualizar", medicamento);
        }
    }
}

