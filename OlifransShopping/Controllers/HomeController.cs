using OlifransShopping.DAL;
using OlifransShopping.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OlifransShopping.Models;
using OlifransShopping.Repository;




namespace OlifransShopping.Controllers
{
    public class HomeController : Controller
    {
        OlifransShoppingEntities ctx = new OlifransShoppingEntities();

        public ActionResult Index(string search, int? page ) 
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 12, page));
        }


        //Adciona produtos ao carrinho
        public ActionResult AddToCarrinho(int produtoId)
        {

            if (Session["carrinho"] == null)
            {
                List<Item> carrinho = new List<Item>();

                var produto = ctx.Produto.Find(produtoId);
                carrinho.Add(new Item()
                {
                    Produto = produto,
                    Quantidade = 1
                });
                Session["carrinho"] = carrinho;
            }
            else
            {
                List<Item> carrinho = (List<Item>) Session["carrinho"];
                var count = carrinho.Count();
                var produto = ctx.Produto.Find(produtoId);

                //foreach (var item in carrinho) //Contador somatorio do mesmo produto no carrinho
                for (int i = 0; i < count; i++)
                {
                    /* if(item.Produto.ProdutoId == produtoId)
                     {
                         int prevQty = item.Quantidade;
                         carrinho.Remove(item);

                         carrinho.Add(new Item()
                         {
                             Produto = produto,
                             Quantidade = prevQty+1
                         });
                         break;
                     }*/

                    if (carrinho[i].Produto.ProdutoId == produtoId)
                    {
                        int prevQty = carrinho[i].Quantidade;
                        carrinho.Remove(carrinho[i]);
                        carrinho.Add(new Item()
                        {
                            Produto = produto,
                            Quantidade = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        /* carrinho.Add(new Item()
                        {
                            Produto = produto,
                            Quantidade = 1
                        }); */

                        var prd = carrinho.Where(x => x.Produto.ProdutoId == produtoId).SingleOrDefault();
                        if (prd == null)
                        {
                            carrinho.Add(new Item()
                            {
                                Produto = produto,
                                Quantidade = 1
                            });
                        }
                    }
                }
                Session["carrinho"] = carrinho;
            }
            return Redirect("Index");
        }


        public ActionResult RemoveFromCarrinho(int produtoId)
        {
            List<Item> carrinho = (List<Item>)Session["carrinho"];

            foreach (var item in carrinho)
            {
                if (item.Produto.ProdutoId == produtoId)
                {
                    carrinho.Remove(item);
                    break;
                }
            }
            Session["carrinho"] = carrinho;
            return Redirect("Index");
        }



        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetalhes()
        {
            return View();
        }



        public ActionResult DiminuirQty(int produtoId)
        {
            if (Session["carrinho"] != null)
            {
                List<Item> carrinho = (List<Item>)Session["carrinho"];

                var produto = ctx.Produto.Find(produtoId);
                foreach (var item in carrinho)
                {
                    if (item.Produto.ProdutoId == produtoId)
                    {
                        int prevQty = item.Quantidade;
                        if (prevQty > 0)
                        {
                            carrinho.Remove(item);
                            carrinho.Add(new Item()
                            {
                                Produto = produto,
                                Quantidade = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["carrinho"] = carrinho;
            }
            return Redirect("Checkout");
        }





        public ActionResult About()
        {
            ViewBag.Message = "Your application description c.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}