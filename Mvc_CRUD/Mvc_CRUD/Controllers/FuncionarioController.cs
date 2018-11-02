using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_CRUD.DAO;
using Mvc_CRUD.Models;

namespace Mvc_CRUD.Controllers
{
    public class FuncionarioController : Controller
    {

        public readonly CRUD mvc_CRUD;
        public FuncionarioController()
        {
            mvc_CRUD = new CRUD();
        }

        public ActionResult Create(Funcionario func)
        {
            if (ModelState.IsValid)
            {
            
                mvc_CRUD.Incluir(func);
                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: Funcionario
        public ActionResult Index()
        {
            return View(mvc_CRUD.ListaTodosFuncionarios());
        }
      
        public ActionResult Edit(int id)
        {
            return View(mvc_CRUD.BuscaPorId(id));
        }
        public ActionResult Details(int id)
        {
            return View(mvc_CRUD.BuscaPorId(id));
        }
        public ActionResult Delete(int id)
        {
          
            mvc_CRUD.Deletar(id);
           return RedirectToAction("Index");
           
           
        }
        [HttpPost]
        public ActionResult Edit(Funcionario func)
        {
            if (ModelState.IsValid)
            {
                mvc_CRUD.Alterar(func);
                return RedirectToAction("Index");
            }
            return View(func);
        }
    }
}