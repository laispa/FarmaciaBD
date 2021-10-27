using FarmaciaBD.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Controllers
{
    public class PrecoController : Controller
    {
        public IActionResult Index()
        {
            List<Preco> precos = new List<Preco>();

            using (MySqlConnection conexao = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {

                conexao.Open();
                MySqlCommand comando = new MySqlCommand("SELECT * FROM vw_medicamento_preco;   ", conexao);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    // extract your data
                    Preco preco = new Preco
                    {
                        Nome = reader["nome"].ToString(),
                        Valor = Convert.ToSingle(reader["valor"])
                    };

                    precos.Add(preco);
                }
                reader.Close();

            }
            return View(precos);
        }
    }
}
