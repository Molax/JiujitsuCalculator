using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dal;

namespace CalculadoraJiuJitsu.Controllers
{
    public class CalculadoraController : Controller
    {
        public List<Models.classe> RetornaFaixa()
        {
            List<Models.classe> faixa = new List<Models.classe>();

            using (var db = new BancoDataContext())
            {
                var faixas = db.Faixas.ToList();
                foreach (var item in faixas)
                {
                    faixa.Add(new Models.classe
                    {
                        descricao = item.DESCRICAO,
                        id = item.PK_ID_FAIXA,
                        pontos = item.PONTOS
                    });
                }

            }
            return faixa;
        }

        public List<Models.classe> RetornaModalidade()
        {
            List<Models.classe> modalidade = new List<Models.classe>();

            using (var db = new BancoDataContext())
            {
                var modalidades = db.Modalidades.ToList();
                foreach (var item in modalidades)
                {
                    modalidade.Add(new Models.classe
                    {
                        descricao = item.DESCRICAO,
                        id = item.PK_ID_MODALIDADE,
                        pontos = item.PONTOS
                    });
                }

            }
            return modalidade;
        }

        public List<Models.classe> RetornaTempo()
        {
            List<Models.classe> tempo = new List<Models.classe>();
            using (var db = new BancoDataContext())
            {
                var tempos = db.Tempos.ToList();

                foreach (var item in tempos)
                {
                    tempo.Add(new Models.classe {
                        descricao = item.DESCRICAO,
                        id = item.PK_ID_TEMPO,
                        pontos = item.PONTOS
                    });
                }
            }

            return tempo;
        }


        public List<Models.classe> RetornaLugar()
        {
            List<Models.classe> lugar = new List<Models.classe>();
            using (var db = new BancoDataContext())
            {
                var lugares = db.Lugars.ToList();
                foreach (var item in lugares)
                {
                    lugar.Add(new Models.classe {
                        descricao = item.DESCRICAO,
                        id = item.PK_ID_LUGAR,
                        pontos = item.PONTOS
                    });
                }
            }
            return lugar;
        }

        public ActionResult PublicarMensagem()
        {
            if (Session["FbuserToken"] != null)
            {
                var _fb = new FacebookClient(Session["FbuserToken"].ToString());

                //Postar uma mensagem na timeline
                dynamic messagePost = new ExpandoObject();
                messagePost.picture = "http://www.rodolfofadino.com.br/wp-content/uploads/2013/12/image_thumb10.png";
                messagePost.link = "http://www.rodolfofadino.com.br/2013/12/test-mode-values-para-o-microsoft-advertising-sdk-windows-8/";
                messagePost.name = "Post name...";
                messagePost.caption = " Post Caption";
                messagePost.description = "post description";
                messagePost.message = "Mensagem de testes da aplicação";

                try
                {
                    var postId = _fb.Post("me/feed", messagePost);
                }
                catch (FacebookOAuthException ex)
                {
                    //handle oauth exception
                }
                catch (FacebookApiException ex)
                {
                    //handle facebook exception
                }
            }

            return RedirectToAction("MeuJiuJitsu");
        }
        // GET: Calculadora
        public ActionResult Index()
        {
            Models.Total total = new Models.Total();
            total.faixas = RetornaFaixa();
            total.lugares = RetornaLugar();
            total.modalidades = RetornaModalidade();
            total.tempos = RetornaTempo();


            Models.facebook fb = new Models.facebook();

            var _fb = new FacebookClient();
            FacebookOAuthResult oauthResult;

            _fb.TryParseOAuthCallbackUrl(Request.Url, out oauthResult);

            if (oauthResult.IsSuccess)
            {
                //Pega o Access Token "permanente"
                dynamic parameters = new ExpandoObject();
                parameters.client_id = "1363597323667956";
                parameters.redirect_uri = "http://localhost:10738/Calculadora/Index";
                parameters.client_secret = "a84641c80191358c865b954f59a8def2";
                parameters.code = oauthResult.Code;

                dynamic result = _fb.Get("/oauth/access_token", parameters);

                var accessToken = result.access_token;

                //TODO: Guardar no banco
                Session.Add("FbUserToken", accessToken);
            }
            else
            {
                // tratar
            }

            if (Session["FbuserToken"] != null)
            {
                var __fb = new FacebookClient(Session["FbuserToken"].ToString());

                //detalhes do usuario
                var request = __fb.Get("me");
                ViewBag.Fb = request;

            }
            return View(total);
        }

        public void Calcular()
        {
            Response.Redirect("/Resultado/Index");
        }
    }
}