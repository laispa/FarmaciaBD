using FarmaciaBD.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FarmaciaBD.Controllers
{
    public class FuncionarioController : Controller
    {
        
        public IActionResult Index()
        {

            List<Funcionario> funcionarios = new List<Funcionario>();

            
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {

                conexao.Open();
                MySqlCommand comando = new MySqlCommand("select *from funcionario;", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Funcionario funcionario = new Funcionario
                    {
                        Matricula = Convert.ToInt32(reader["matricula"]),
                        Nascimento =reader["nascimento"].ToString(),
                        Nome = reader["nome"].ToString(),
                        Cargo = reader["cargo"].ToString(),
                        Sexo = reader["sexo"].ToString(),
                        Salario = reader["salario"].ToString(),
                        Loja_numero = Convert.ToInt32(reader["loja_numero"])


                       

                };

                    funcionarios.Add(funcionario);
                }
                reader.Close();

            }
            return View(funcionarios);
        }

        [HttpGet]
        public IActionResult Inserir()
        {

            return View(new Funcionario());
        }

        [HttpPost]
        public IActionResult Inserir([FromForm] Funcionario funcionario)
        {
            try {
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    conexao.Open();
                    MySqlCommand comando = new MySqlCommand("insert into funcionario(nascimento, nome, cargo, sexo, salario, loja_Numero) values('" + funcionario.Nascimento + "','" + funcionario.Nome + "','" + funcionario.Cargo + "','" + funcionario.Sexo + "','" + funcionario.Salario + "'," + funcionario.Loja_numero + ");", conexao);
                    
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    TempData["Mensagem"] = "O funcionário foi cadastrado com sucesso!";
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                    TempData["Exception"] = "Você digitou uma loja existente? Verifique também o formato da data";
            }
                 
            return View("Inserir", funcionario);
        }

        [HttpGet, ActionName("Deletar")]
        public IActionResult Deletar(int Numero)
        {
            Console.WriteLine(Numero);
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("DELETE FROM funcionario WHERE matricula = '" + Numero + "'", conexao);

                comando.ExecuteNonQuery();

                TempData["Mensagem"] = "O funcionário'" + Numero + "' foi excluído com sucesso!";
                conexao.Close();
            }
            return RedirectToAction("Index");
        }


        [HttpGet, ActionName("Update")]
        public IActionResult Update(int Numero)
        {
            
            List<Funcionario> funcionarios = new List<Funcionario>();
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("SELECT *FROM funcionario " +
                    "WHERE matricula ='" + Numero + "'", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Funcionario funcionario = new Funcionario
                    {
                        Matricula = Convert.ToInt32(reader["matricula"]),
                        Nascimento = reader["nascimento"].ToString(),
                        Nome = reader["nome"].ToString(),
                        Cargo = reader["cargo"].ToString(),
                        Sexo = reader["sexo"].ToString(),
                        Salario = reader["salario"].ToString(),
                        Loja_numero = Convert.ToInt32(reader["loja_numero"])
                        
                    };
                    funcionarios.Add(funcionario);

                }
                    reader.Close();

            }
            return View("Atualizar", funcionarios);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Funcionario funcionario)
        {
            try
            {
                /*if (ModelState.IsValid)
                {*/
                    using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                    {
                        conexao.Open();
                        MySqlCommand comando = new MySqlCommand(
                            "Update funcionario set nascimento='" + funcionario.Nascimento + "', nome='" + funcionario.Nome + "', cargo= '" + funcionario.Cargo + "', sexo='" + funcionario.Sexo + "', salario='" + funcionario.Salario + "',loja_numero='" + funcionario.Loja_numero + "' WHERE matricula=" + funcionario.Matricula + "", conexao);

                        comando.ExecuteNonQuery();

                        TempData["Mensagem"] = "O funcionário" + funcionario.Matricula + "' foi atualizado com sucesso!";
                        conexao.Close();
                        return RedirectToAction("Index");
                    }
                
            }
            
            catch(Exception)
            {
                TempData["Exception"] = "ERRO. Verifique se tentou cadastrar um número de loja existente";
                
            }
            
            return RedirectToAction("Index");
        }
         
    }
}

