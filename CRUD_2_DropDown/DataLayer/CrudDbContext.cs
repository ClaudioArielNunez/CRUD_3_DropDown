using CRUD_2_DropDown.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_2_DropDown.DataLayer
{
    public class CrudDbContext : DbContext
    {
        public CrudDbContext(DbContextOptions options) : base(options)
        {
        }

    //Creamos DbSet, uno por cada tabla
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    }

}
