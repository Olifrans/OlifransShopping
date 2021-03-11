using OlifransShopping.DAL;
using OlifransShopping.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using PagedList;
using PagedList.Mvc;



namespace OlifransShopping.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        OlifransShoppingEntities context = new OlifransShoppingEntities();

        public IPagedList<Produto> ListOfProduto { get; set; }
        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@search",search??(object)DBNull.Value)
            };

            IPagedList<Produto> data = context.Database.SqlQuery<Produto>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);


            return new HomeIndexViewModel
            {
                ListOfProduto = data
            };
        }
    }
}