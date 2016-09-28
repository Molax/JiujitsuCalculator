using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dal;
using System.Net;

namespace CalculadoraJiuJitsu.Controllers
{
    public class CalculadoraController : Controller
    {
        public int CalculaPontos(Models.Pontos pontos)
        {
            int x = pontos.categoria + pontos.categoriaCamp1 + pontos.categoriaCamp2 + pontos.categoriaCamp3 + pontos.categoriaCamp4 + pontos.categoriaCamp5 + pontos.colocacaoCamp1 + pontos.colocacaoCamp2 + pontos.colocacaoCamp3 + pontos.colocacaoCamp4 + pontos.colocacaoCamp5 + pontos.faixa + pontos.faixaCamp1 + pontos.faixaCamp2 + pontos.faixaCamp3 + pontos.faixaCamp4 + pontos.faixaCamp5 + pontos.tempo;
            Session.Add("PontosFace", x);

            return 1;
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
            //total.faixas = RetornaFaixa();
            //total.lugares = RetornaLugar();
            //total.modalidades = RetornaModalidade();
            //total.tempos = RetornaTempo();


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

                WebResponse response = null;
                string pictureUrl = string.Empty;
                WebRequest requests = WebRequest.Create(string.Format("https://graph.facebook.com/{0}/picture", ViewBag.Fb.id));
                response = requests.GetResponse();
                ViewBag.Foto = response.ResponseUri.ToString();
            }
            return View(total);
        }

        public ActionResult Calcular()
        {
            ViewBag.Pontos = Session["PontosFace"].ToString();

            return View();
        }
    }
}