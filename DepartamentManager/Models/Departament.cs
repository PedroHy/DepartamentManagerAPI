
namespace DepartamentManager.Models
{
    public class Departament
    {
        public required int id { get; set; }
        public required string name { get; set; }
        public required string acronym { get; set; }
        
        public void Update(int id, string name, string acronym)
        {
            this.id = id;
            this.name = name;
            this.acronym = acronym;
        }

    }

}
