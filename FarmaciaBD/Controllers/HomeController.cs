using FarmaciaBD.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Loja> lojas = new List<Loja>();

            using (MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=farmacia;port=3306;password=gatosloucos"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select *from Loja", con);

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
    }
}
