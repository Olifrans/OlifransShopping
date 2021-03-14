using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OlifransShopping.DAL;
using OlifransShopping.Models;
using OlifransShopping.Repository;



namespace OlifransShopping.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();


        public List<SelectListItem> GetCategoria()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Categoria>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoriaId.ToString(), Text = item.CategoriaNome });
            }
            return list;
        }




        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult Categorias()
        {
            List<Categoria> allcategorias = _unitOfWork.GetRepositoryInstance<Categoria>().GetAllRecordsIQueryable().Where(i => i.StatusDelete == false).ToList();
            return View(allcategorias);
        }



        public ActionResult AddCategoria()
        {
            return UpdateCategoria(0);
        }


        public ActionResult UpdateCategoria(int categoriaId)
        {
            CategoriaDetalhes cd;
            if (categoriaId != null)
            {
                cd = JsonConvert.DeserializeObject<CategoriaDetalhes>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Categoria>().GetFirstorDefault(categoriaId)));
            }
            else
            {
                cd = new CategoriaDetalhes();
            }
            return View("UpdateCategoria", cd);
        }

        public ActionResult EditarCategoria(int catId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Categoria>().GetFirstorDefault(catId));
        }

        [HttpPost]
        public ActionResult EditarCategoria(Categoria tbl)
        {
            _unitOfWork.GetRepositoryInstance<Categoria>().Update(tbl);
            return RedirectToAction("Categorias");
        }


        public ActionResult Produtos()
        {
           return View(_unitOfWork.GetRepositoryInstance<Produto>().GetProduto());
        }

        public ActionResult EditarProduto(int produtoId)
        {
            ViewBag.CategoriaList = GetCategoria(); //Exibi a categoria
            return View(_unitOfWork.GetRepositoryInstance<Produto>().GetFirstorDefault(produtoId));
           
        }

        [HttpPost]
        public ActionResult EditarProduto(Produto tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProdutoImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProdutoImage = file != null ? pic : tbl.ProdutoImage;
            tbl.ModificadoData = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Produto>().Update(tbl);
            return RedirectToAction("Produtos");         
        }


        public ActionResult AddPoduto()
        {
            ViewBag.CategoriaList = GetCategoria(); //Exibi a categoria
            return View();
        }
        

        [HttpPost]
        public ActionResult AddPoduto(Produto tbl,HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProdutoImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProdutoImage = pic;
            tbl.CriadoData = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Produto>().Add(tbl);
            return RedirectToAction("Produtos");
        }

    }
}