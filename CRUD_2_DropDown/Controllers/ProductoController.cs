using CRUD_2_DropDown.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_2_DropDown.Controllers
{
    public class ProductoController : Controller
    {
        private readonly CrudDbContext context;

        public ProductoController(CrudDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var productos = context.Productos.Include("Categorias");
            return View(productos);
        }
    }
}
