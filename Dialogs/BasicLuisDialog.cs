using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using LuisBot.PreguntaService;
using LuisBot.CreditoService;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using AdaptiveCards;
using System.Net;
using System.IO;
using System.Text;
using LuisBot.Dominio;
using System.Web.Script.Serialization;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        private string Correo = "";
        private string Contrasena = "";
        private string Nombres = "";
        private string OpcionLoginSignUp = "";
        private string token = "";
        private string idCurso = "";
        private string idPregunta = "";

        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        //inicio flujo de saludo
        [LuisIntent("Saludo")]
        public async Task SaludoIntent(IDialogContext context, LuisResult result)
        {
            if (token == "")
            {
                string[] login_signup = { "Iniciar Sesión", "Registrarse" };

                var options = login_signup;
                var descriptions = login_signup;
                PromptDialog.Choice<string>(context, AfterSaludoPrompt,
                    options, "¡Bienvenido a Ask Bot Upecino!, Inicie Sesión o Regístrese para comenzar", descriptions: descriptions);
            }
            else
            {
                string[] opciones = { "Consultar Pregunta", "Crear Pregunta" };

                var options = opciones;
                var descriptions = opciones;
                PromptDialog.Choice<string>(context, ConsultaCreaPregunta,
                    options, $"{Nombres}, Escoge entre consultar o crear una pregunta:", descriptions: descriptions);
            }
        }

        private async Task AfterSaludoPrompt(IDialogContext context, IAwaitable<string> result)
        {
            OpcionLoginSignUp = await result;

            if (OpcionLoginSignUp == "Iniciar Sesión")
            {
                PromptDialog.Text(context, IniciaSesion1, $"Por favor escriba su correo...");
            }
            else if (OpcionLoginSignUp == "Registrarse")
            {
                PromptDialog.Text(context, Registrarse1, $"Por favor escriba sus nombres completos");
            }
        }

        private async Task IniciaSesion1(IDialogContext context, IAwaitable<string> result)
        {
            Correo = await result;
            PromptDialog.Text(context, Member, $"Por favor escriba su contraseña...");
        }

        private async Task Registrarse1(IDialogContext context, IAwaitable<string> result)
        {
            Nombres = await result;
            PromptDialog.Text(context, Registrarse2, $"Por favor escriba su correo...");
        }

        private async Task Registrarse2(IDialogContext context, IAwaitable<string> result)
        {
            Correo = await result;
            PromptDialog.Text(context, Member, $"Por favor escriba su contraseña...");
        }

        private async Task Member(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                Contrasena = await result;

                if (OpcionLoginSignUp == "Iniciar Sesión")
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string tramaJson = "";

                    LogIn logIn = new LogIn
                    {
                        access_token = "LmTXw9oHsVZhhjgllFQphiZfXXJJGVwF"
                    };

                    string postdata = js.Serialize(logIn);

                    byte[] dataBytes = Encoding.UTF8.GetBytes(postdata);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://loginrestapi.herokuapp.com/auth");

                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    request.ContentLength = dataBytes.Length;
                    request.ContentType = "application/json";
                    request.Method = "POST";

                    string authInfo = Correo + ":" + Contrasena;
                    authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

                    request.Headers.Add("Authorization", "Basic " + authInfo);

                    using (Stream requestBody = request.GetRequestStream())
                    {
                        requestBody.Write(dataBytes, 0, dataBytes.Length);
                    }

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        tramaJson = reader.ReadToEnd();
                    }

                    UsuarioObtenido usuarioObtenido = js.Deserialize<UsuarioObtenido>(tramaJson);

                    token = usuarioObtenido.token;

                    string[] opciones = { "Consultar Pregunta", "Crear Pregunta" };

                    var options = opciones;
                    var descriptions = opciones;
                    PromptDialog.Choice<string>(context, ConsultaCreaPregunta,
                        options, $"Hola {usuarioObtenido.user.name}, ahora escoge entre consultar o crear pregunta:", descriptions: descriptions);
                }

                else if (OpcionLoginSignUp == "Registrarse")
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string tramaJson = "";

                    SignUp signUp = new SignUp
                    {
                        access_token = "LmTXw9oHsVZhhjgllFQphiZfXXJJGVwF",
                        email = Correo,
                        password = Contrasena,
                        name = Nombres,
                        picture = "",
                        role = ""
                    };

                    string postdata = js.Serialize(signUp);

                    byte[] dataBytes = Encoding.UTF8.GetBytes(postdata);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://loginrestapi.herokuapp.com/users");

                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    request.ContentLength = dataBytes.Length;
                    request.ContentType = "application/json";
                    request.Method = "POST";

                    using (Stream requestBody = request.GetRequestStream())
                    {
                        requestBody.Write(dataBytes, 0, dataBytes.Length);
                    }

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        tramaJson = reader.ReadToEnd();
                    }

                    UsuarioObtenido usuarioObtenido = js.Deserialize<UsuarioObtenido>(tramaJson);

                    token = usuarioObtenido.token;

                    string[] opciones = { "Consultar Pregunta", "Crear Pregunta" };

                    var options = opciones;
                    var descriptions = opciones;
                    PromptDialog.Choice<string>(context, ConsultaCreaPregunta,
                        options, $"Hola {usuarioObtenido.user.name}, ahora escoge entre consultar o crear pregunta:", descriptions: descriptions);
                }
            }

            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    if (((HttpWebResponse)wex.Response).StatusCode.ToString() == "Unauthorized")
                    {
                        await context.PostAsync($"El usuario o contraseña ingresada no es correcta.");
                        context.Wait(MessageReceived);
                    }
                    else
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                string error = reader.ReadToEnd();
                                JavaScriptSerializer js = new JavaScriptSerializer();
                                UsuarioError usuarioError = js.Deserialize<UsuarioError>(error);
                                await context.PostAsync($"Error en: {usuarioError.message}");
                                context.Wait(MessageReceived);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await context.PostAsync($"{ex.Message.ToString()}");
                context.Wait(MessageReceived);
            }
        }

        private async Task ConsultaCreaPregunta(IDialogContext context, IAwaitable<string> result)
        {
            var seleccion = await result;
            if (seleccion == "Consultar Pregunta")
            {
                PreguntaServiceClient preguntaServiceClient = new PreguntaServiceClient();                
                List<Curso> listaCursos = preguntaServiceClient.ListarCurso().ToList();

                PromptDialog.Choice<string>(context, ConsultarTema,
                    listaCursos.Select(x=>x.IdCurso.ToString()).AsEnumerable(), $"Escoge un curso a consultar:", descriptions: listaCursos.Select(x => x.Nombre).AsEnumerable());
            }

            else if (seleccion == "Crear Pregunta")
            {
                PreguntaServiceClient preguntaServiceClient = new PreguntaServiceClient();
                List<Curso> listaCursos = preguntaServiceClient.ListarCurso().ToList();

                PromptDialog.Choice<string>(context, After_Pregunta0Prompt,
                    listaCursos.Select(x => x.IdCurso.ToString()).AsEnumerable(), $"Primero escoja un curso:", descriptions: listaCursos.Select(x => x.Nombre).AsEnumerable());
            }
        }

        private async Task ConsultarTema(IDialogContext context, IAwaitable<string> result)
        {
            idCurso = await result;

            int idCursoint = Int32.Parse(idCurso);

            PreguntaServiceClient preguntaServiceClient = new PreguntaServiceClient();
            List<Pregunta> listaPreguntas = preguntaServiceClient.ListarPregunta().Where(x=>x.IDCurso==idCursoint).ToList();

            PromptDialog.Choice<string>(context, MostrarPregunta,
                listaPreguntas.Select(x => x.IDPregunta.ToString()).AsEnumerable(), $"Escoja una pregunta:", descriptions: listaPreguntas.Select(x => x.Descripcion).AsEnumerable());
        }

        private async Task MostrarPregunta(IDialogContext context, IAwaitable<string> result)
        {
            idPregunta = await result;

            int idPreguntaint = Int32.Parse(idPregunta);

            PreguntaServiceClient preguntaServiceClient = new PreguntaServiceClient();
            Pregunta pregunta = preguntaServiceClient.preguntar(idPreguntaint);

            await context.PostAsync($"Esta es la repuesta: { pregunta.respuesta }");
            context.Wait(MessageReceived);
        }
        // fin flujo de saludo

        //inicio flujo pregunta
        private string currentCurso;
        private string currentPregunta;
        private string currentRespuesta;
        [LuisIntent("Pregunta")]
        public async Task PreguntaIntent(IDialogContext context, LuisResult result)
        {
            if (token == "")
            {
                string[] login_signup = { "Iniciar Sesión", "Registrarse" };

                var options = login_signup;
                var descriptions = login_signup;
                PromptDialog.Choice<string>(context, AfterSaludoPrompt,
                    options, "¡Bienvenido a Ask Bot Upecino!, Inicie Sesión o Regístrese para comenzar", descriptions: descriptions);
            }
            else
            {
                PreguntaServiceClient preguntaServiceClient = new PreguntaServiceClient();
                List<Curso> listaCursos = preguntaServiceClient.ListarCurso().ToList();

                PromptDialog.Choice<string>(context, After_Pregunta0Prompt,
                    listaCursos.Select(x => x.IdCurso.ToString()).AsEnumerable(), $"Primero escoja un curso:", descriptions: listaCursos.Select(x => x.Nombre).AsEnumerable());
            }
        }

        private async Task After_Pregunta0Prompt(IDialogContext context, IAwaitable<string> result)
        {
            currentCurso = await result;
            PromptDialog.Text(context, After_PreguntaPrompt, "Ahora escriba una pregunta...");
        }

        private async Task After_PreguntaPrompt(IDialogContext context, IAwaitable<string> result)
        {
            currentPregunta = await result;
            PromptDialog.Text(context, After_RespuestaPrompt, $"Ahora escriba una respuesta de la pregunta {currentPregunta}.");
        }

        private async Task After_RespuestaPrompt(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                currentRespuesta = await result;
                int currentCursoint = Int32.Parse(currentCurso);

                PreguntaServiceClient preguntaserviceClient = new PreguntaServiceClient();
                var x = preguntaserviceClient.crear(token, currentCursoint, currentPregunta, "1", currentRespuesta);

                CreditoServiceClient creditoServiceClient = new CreditoServiceClient();
                Credito credito = new Credito();
                credito.CodCredito = 9;
                credito.CodAlumno = token;
                credito.CodCurso = currentCurso;
                credito.CodDescripcion = x.IDPregunta.ToString();
                credito.Tipo = "";
                creditoServiceClient.CrearCredito(credito);

                await context.PostAsync($"Gracias por contribuir con Ask Bot Upecino!");

                context.Wait(MessageReceived);
            }
            catch (Exception ex)
            {
                await context.PostAsync($"{ex.Message.ToString()}");
                context.Wait(MessageReceived);
            }
            
        }
        //fin flujo pregunta

        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Lo siento, no te he entendido.");
            context.Wait(MessageReceived);
        }
    }
}