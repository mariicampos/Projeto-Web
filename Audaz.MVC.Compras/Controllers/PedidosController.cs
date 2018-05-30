using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Audaz.MVC.Compras.Models;
using System.Net.Mail;

namespace Audaz.MVC.Compras.Controllers
{
    public class PedidosController : Controller
    {
        PedidosDBContext db = new PedidosDBContext();

        // GET: Pedidos
        public ActionResult Index()
        {
            db = new PedidosDBContext();

            return View(db.Pedidos.ToList());
        }


        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Finalizar(Pedido pedido)
        {            
            ViewBag.PedidoItem1 = "Item 1: " + pedido.Item1;
            ViewBag.PedidoQuantidade1 = "Quantidade 1: " + pedido.Quantidade1;
            ViewBag.PedidoItem2 = "Item 2: " + pedido.Item2;
            ViewBag.PedidoQuantidade2 = "Quantidade 2: " + pedido.Quantidade2;
            ViewBag.PedidoEmail = "Email: " + pedido.Email;
            ViewBag.Pedidos = pedido.Item1 + "|" + pedido.Quantidade1 + "|" + pedido.Item2 + "|" + pedido.Quantidade2 + "|" + pedido.Email;

            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Item1,Quantidade1,Item2,Quantidade2,Email")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Finalizar", pedido);
            }

            return View();
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Item1,Quantidade1,Item2,Quantidade2,Email")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult sendMessage(String pedido)
        {
            var aux = pedido.Split('|');
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("development.test00@gmail.com", "development12345");
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress("development.test00@gmail.com", "Pedido realizado");
            mail.From = new MailAddress("development.test00@gmail.com", "Pedido realizado");
            mail.To.Add(new MailAddress(aux[4], "RECEBEDOR"));
            mail.Subject = "Contato";
            mail.Body = "Resumo do pedido <br/>" +
                "Item:" + aux[0] + " Quantidade:" + aux[1] + "<br/>" +
                "Item:" + aux[2] + " Quantidade:" + aux[3];
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
            }
            catch (System.Exception erro)
            {
                Console.WriteLine(erro.Message);

            }
            finally
            {
                mail = null;
            }
            return RedirectToAction("Index");
        }
    }
}
