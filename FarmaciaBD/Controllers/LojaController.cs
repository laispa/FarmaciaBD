using FarmaciaBD.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (ModelState.IsValid) { 
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO loja (cidade, endereco) VALUES('" + loja.Cidade + "','" + loja.Endereco + "');", conexao);
               
                    TempData["Mensagem"] = "A loja foi cadastrada com sucesso!";
                    cmd.ExecuteNonQuery();

                    conexao.Close();
                }
                
                return RedirectToAction("Index");

            }
            return View("Inserir", loja);
        }

        [HttpGet, ActionName("Deletar")]
        public IActionResult Deletar(int Numero)
        {
            Console.WriteLine(Numero);
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM loja WHERE numero = '"+Numero+"'",conexao);

                cmd.ExecuteNonQuery();

                TempData["Mensagem"] = "A loja'" + Numero + "' foi excluída com sucesso!";
                conexao.Close();
            }
                return RedirectToAction("Index");
        }
        [HttpGet,ActionName("Update")]
        public IActionResult Update(int Numero)
        {
            List<Loja> lojas = new List<Loja>();
            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT *FROM loja " +
                    "WHERE numero ='"+Numero+"'", conexao);
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
            return View("Atualizar", lojas);
        }

        [HttpPost]
        public IActionResult Update([FromForm] Loja loja)
        {
            if (ModelState.IsValid)
            {
                using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
                {
                    
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE loja SET " +
                        "cidade ='" + loja.Cidade + "', endereco='" + loja.Endereco + "'" +
                        " WHERE numero = " + loja.Numero +"", conexao);
                    cmd.ExecuteNonQuery();
                    TempData["Mensagem"] = "A loja'" + loja.Numero + "' foi atualizada com sucesso!";
                    conexao.Close();
                }
                return RedirectToAction("Index");
            }
            return View("Atualizar", loja);
        }
    }

}
