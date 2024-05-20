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
            if (ModelState.IsValid)
            {
                context.Productos.Add(producto);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();            
        }

        [HttpGet]
        public IActionResult Editar(int? id) //permite que el parámetro id sea null
        {
            if (id == null)
            {
                return NotFound();
            }
            CargarCategorias();
            var producto = context.Productos.FirstOrDefault(x => x.Id == id);
            if(producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        public IActionResult Editar(Producto producto)
        {
            ModelState.Remove("Categorias");//*
            if (ModelState.IsValid)
            {
                context.Productos.Update(producto);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);//retornamos el producto a editar
        }
        /*
         El problema surge porque ModelState intenta validar todas las propiedades del modelo, 
        incluida Categorias. Si Categorias no está presente en los datos enviados (lo cual es 
        el caso aquí, ya que solo envías CategoriaId), ModelState puede marcarlo como inválido,
        porque no puede vincular un valor a Categorias.
         Al eliminar Categorias del ModelState, se asegura que esta propiedad no afecte la
        validación general del modelo. 
        Si Categorias es una propiedad de navegación en Entity Framework que no se necesita validar 
        puede ser conveniente eliminarla del ModelState.
        Si la vista de edición de Producto incluye un DropDownList para seleccionar categorías,
        pero no envía la propiedad Categorias directamente como parte del formulario, 
        el ModelState puede marcar la propiedad como inválida si espera datos para ella.
         */
        [HttpGet]
        public IActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CargarCategorias();
            var producto = context.Productos.FirstOrDefault(x => x.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        public IActionResult Eliminar(Producto producto)
        {
            ModelState.Remove("Categorias");
            if (ModelState.IsValid)
            {
                context.Productos.Remove(producto);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
            
        }
    }
}
