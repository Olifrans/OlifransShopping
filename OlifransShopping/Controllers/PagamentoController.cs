using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal;
using PayPal.Api;

namespace OlifransShopping.Controllers
{
    public class PagamentoController : Controller
    {
        // GET: Pagamento
        public ActionResult PagamentoWithPaypal()
        {

            APIContext apicontext = PaypalConfiguration.GetAPIContext();
            try
            {

                string PayerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(PayerId) && PayerId != null)
                {
                    string baseURi = Request.Url.Scheme + "://" + Request.Url.Authority +
                                     "PagamentoWithPaypal/PagamentoWithPaypal?";

                    var Guid = Convert.ToString((new Random()).Next(100000000));
                    var createPayment = this.CreatePayment(apicontext, baseURi + "guid=" + Guid);

                    var links = createPayment.links.GetEnumerator();
                    string paypalRedirectURL = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectURL = lnk.href;
                        }

                    }
                }

                else
                {
                    var guid = Request.Params["guid"];
                    var executedPaymnt = ExecutePagamento(apicontext, PayerId, Session[guid] as string);

                    if (executedPaymnt.ToString().ToLower() != "approved")
                    {
                        return View("FailureView");
                    }

                }
            }

            catch (Exception)
            {
                return View("FailureView");

                throw;
            }
            return View("SuccessView");

        }

        private object ExecutePagamento(APIContext apicontext, string payerId, string PaymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = PaymentId };
            return this.payment.Execute(apicontext, paymentExecution);
        }

        private PayPal.Api.Payment payment;

        private Payment CreatePayment(APIContext apicontext, string redirectURl)
        {

            var ItemLIst = new ItemList() { items = new List<Item>() };

#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
            if (Session["carrinho"] != "")
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
            {
                List<Models.Home.Item> carrinho = (List<Models.Home.Item>)(Session["carrinho"]);
                foreach (var item in carrinho)
                {
                    ItemLIst.items.Add(new Item()
                    {
                        name = item.Produto.ProdutoNome.ToString(),
                        currency = "BR",
                        price = item.Produto.PrecoUnitario.ToString(),
                        quantity = item.Produto.Quantidade.ToString(),
                        sku = "sku"
                    });
                }


                var payer = new Payer() { payment_method = "paypal" };

                var redirUrl = new RedirectUrls()
                {
                    cancel_url = redirectURl + "&Cancel=true",
                    return_url = redirectURl
                };

                var details = new Details()
                {
                    tax = "1",
                    shipping = "1",
                    subtotal = "1"
                };

                var amount = new Amount()
                {
                    currency = "BRL",

                    total = Session["SesTotal"].ToString(),
                    details = details
                };


                var ListarTransacao = new List<Transaction>();
                ListarTransacao.Add(new Transaction()
                {
                    description = "Transaction Description",
                    invoice_number = "#100000",
                    amount = amount,
                    item_list = ItemLIst

                });

                this.payment = new Payment()
                {
                    intent = "oferta",
                    payer = payer,
                    transactions = ListarTransacao,
                    redirect_urls = redirUrl
                };
            }

            return this.payment.Create(apicontext);

        }

    }
}