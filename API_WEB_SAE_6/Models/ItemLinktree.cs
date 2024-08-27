using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class ItemLinktree
    {
        [Key]
        public int id { get; set; }
        public string titulo { get; set; }
        public int id_index_ico { get; set; }
        public string hipervinculo { get; set; }
        public int? contador_clicks { get; set; }

        public ItemLinktree()
        {

        }
        public ItemLinktree(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            titulo = data["titulo"].ToString() ?? "Error";
            id_index_ico = int.Parse(data["id_index_ico"].ToString() ?? "0");
            hipervinculo = data["hipervinculo"].ToString() ?? "Error";
            contador_clicks = int.Parse(data["contador_clicks"].ToString() ?? "0");
        }
    }
}
