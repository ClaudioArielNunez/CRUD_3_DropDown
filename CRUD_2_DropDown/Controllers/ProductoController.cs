using CRUD_2_DropDown.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;
using System;
using CRUD_2_DropDown.Models;

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
            //Al usar Include, la consulta carga los Productos y sus Categorias en una sola operación,mejorando la eficiencia
            var productos = context.Productos.Include("Categorias").ToList();
            return View(productos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            //Cargamos categorias
            CargarCategorias();
            return View();
        }

        //[NonAction]: Evita que el método sea llamado como una acción HTTP.
        [NonAction]
        private void CargarCategorias()
        {
            var categorias = context.Categorias.ToList();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
        }

        [HttpPost]
        public IActionResult Crear(Producto producto)
        {            
            context.Productos.Add(producto);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
