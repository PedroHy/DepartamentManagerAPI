using Microsoft.EntityFrameworkCore.Update.Internal;

namespace DepartamentManager.Models
{
    public class Colaborator
    {
        public required int id { get; set; }
        public required string name { get; set; }
        public required string picture { get; set; }
        public required string rg { get; set; }
        public required int departamentId { get; set; }

        public void Update(int id, string name, string picture, string rg, int departamentId)
        {
            this.id = id;
            this.name = name;
            this.picture = picture;
            this.rg = rg;
            this.departamentId = departamentId;
        }
   
    }
}
