using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUD_2_DropDown.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Categoría")]
        public string? Nombre { get; set; }
    }
}
