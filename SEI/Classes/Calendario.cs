using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using static SEI.Classes.ConnectSEI;
using System.Collections.Generic;

namespace SEI.Classes

{
    public class Calendario
    {
        public int IdEvento { get; set; }
        public DateOnly Dataselecionada { get; set; }
        public string NomeEvento { get; set; } = "";
        public string TextoEvento { get; set; } = "";

        public void AdicionarEvento(Calendario calendario, string autor)
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AdicionarEvento"
            };
            Dictionary<string, object> parametros = new()
            {
                {"@p_Nome_Evento", calendario.NomeEvento},
                {"@p_Data_Evento", calendario.Dataselecionada},
                {"@p_Descrição", calendario.TextoEvento},
                {"@p_Autor_Evento", autor}
            };
            foreach (var parametro in parametros)
            {
                command.Parameters.AddWithValue(parametro.Key, parametro.Value);
            }
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public DataTable BuscarEvento()
        {
            MySqlCommand command = new()
            {
                Connection = ConnectToDB(),
                CommandType = CommandType.StoredProcedure,
                CommandText = "BuscarEvento"
            };
            using (MySqlDataAdapter da = new MySqlDataAdapter(command))
            {
                var dataTable = new DataTable();
                da.Fill(dataTable);
                return dataTable;
            }
        }
    }
}
