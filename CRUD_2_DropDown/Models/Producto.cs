using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_2_DropDown.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        [ForeignKey("Categorias")]  //Definimos FK, debe coincidir con la prop de navegacion si hay, sino lleva el nombre de la FK
        public int CategoriaId { get; set; }
        public virtual Categoria Categorias { get; set; }
        //Propiedad de navegación que permite acceder a la entidad Categoria asociada,
        //facilita la carga de la entidad Categoria relacionada, permiten navegar a través de las relaciones entre entidades en el código.
        //permite acceder a los detalles de la categoría desde una instancia de Producto.
        //Usar virtual en propiedades de navegación es una práctica común y recomendada cuando se trabaja con Entity Framework
        //y quieres aprovechar la carga diferida, esta permite que las entidades relacionadas se carguen
        //automáticamente cuando se acceden por primera vez, mejorando el rendimiento en algunos escenarios.
    }
    
}
