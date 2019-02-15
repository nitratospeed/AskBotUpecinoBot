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
            string[] login_signup = { "Iniciar Sesión", "Registrarse" };

            var options = login_signup;
            var descriptions = login_signup;
            PromptDialog.Choice<string>(context, AfterSaludoPrompt,
                options, "¡Bienvenido a Ask Bot Upecino!, Inicie Sesión o Regístrese para comenzar", descriptions: descriptions);
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
            Contrasena = await result;

            if (OpcionLoginSignUp == "Iniciar Sesión")
            {
                //consumo login
                string[] opciones = { "Consultar Pregunta", "Crear Pregunta" };

                var options = opciones;
                var descriptions = opciones;
                PromptDialog.Choice<string>(context, ConsultaCreaPregunta,
                    options, $"Hola {Nombres}, ahora escoge entre consultar o crear pregunta:", descriptions: descriptions);
            }

            else if (OpcionLoginSignUp == "Registrarse")
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string tramaJson = "";
                Usuarios usuarios = new Usuarios()
                {
                    access_token = "LmTXw9oHsVZhhjgllFQphiZfXXJJGVwF",
                    email = Correo,
                    password = Contrasena,
                    name = Nombres,
                    picture = "",
                    role = ""
                };

                string postdata = js.Serialize(usuarios);

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
                UsuarioCreado usuarioCreado = js.Deserialize<UsuarioCreado>(tramaJson);

                string[] opciones = { "Consultar Pregunta", "Crear Pregunta" };

                var options = opciones;
                var descriptions = opciones;
                PromptDialog.Choice<string>(context, ConsultaCreaPregunta,
                    options, $"Hola {usuarioCreado.user.name}, ahora escoge entre consultar o crear pregunta:", descriptions: descriptions);
            }
        }

        private async Task ConsultaCreaPregunta(IDialogContext context, IAwaitable<string> result)
        {
            var seleccion = await result;
            if (seleccion == "Consultar Pregunta")
            {
                PreguntaServiceClient preguntaServiceClient = new PreguntaServiceClient();
                string[] cursos = preguntaServiceClient.ListarCurso().Select(m => m.Nombre).ToArray();

                var options = cursos;
                var descriptions = cursos;
                PromptDialog.Choice<string>(context, ConsultarTema,
                    options, $"Escoge un curso a consultar:", descriptions: descriptions);
            }

            else if (seleccion == "Crear Pregunta")
            {
                PromptDialog.Text(context, After_PreguntaPrompt, "Primero escriba la pregunta...");
            }
        }

        private async Task ConsultarTema(IDialogContext context, IAwaitable<string> result)
        {
            var selection = await result;
            PromptDialog.Text(context, MostrarPregunta, $"¿Qué tema deseas consultar del curso de {selection} ?.");
        }

        private async Task MostrarPregunta(IDialogContext context, IAwaitable<string> result)
        {
            await context.PostAsync($"Esta es la pregunta deseada...");
            context.Wait(MessageReceived);
        }
        // fin flujo de saludo

        //inicio flujo pregunta
        private string currentPregunta;
        private string currentRespuesta;
        [LuisIntent("Pregunta")]
        public async Task PreguntaIntent(IDialogContext context, LuisResult result)
        {
            PromptDialog.Text(context, After_PreguntaPrompt, "Primero escriba la pregunta...");
        }

        private async Task After_PreguntaPrompt(IDialogContext context, IAwaitable<string> result)
        {
            currentPregunta = await result;
            PromptDialog.Text(context, After_RespuestaPrompt, $"Ahora escriba la respuesta de la pregunta {currentPregunta}.");
        }

        private async Task After_RespuestaPrompt(IDialogContext context, IAwaitable<string> result)
        {
            currentRespuesta = await result;
            PreguntaServiceClient preguntaserviceClient = new PreguntaServiceClient();
            preguntaserviceClient.crear(1, 1, currentPregunta, "1", currentRespuesta);

            //CreditoServiceClient creditoServiceClient = new CreditoServiceClient();
            //Credito credito = new Credito();
            //credito.CodCredito = 9;
            //credito.CodAlumno = "A09";
            //credito.CodCurso = "C01";
            //credito.CodDescripcion = "P10";
            //credito.Tipo = "P";
            //creditoServiceClient.CrearCredito(credito);

            await context.PostAsync($"Gracias por contribuir con Ask Bot Upecino!");

            context.Wait(MessageReceived);
        }
        //fin flujo pregunta

        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Lo siento, no te he entendido.");
            context.Wait(MessageReceived);
        }
    }
}