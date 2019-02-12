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

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
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
            PreguntaServiceClient preguntaServiceClient = new PreguntaServiceClient();
            string[] cursos = preguntaServiceClient.ListarCurso().Select(m => m.Nombre).ToArray();

            var options = cursos;
            var descriptions = cursos;
            PromptDialog.Choice<string>(context, After_CursoPrompt,
                options, "¡Bienvenido a Ask Bot Upecino!, ¿Qué curso deseas consultar?", descriptions: descriptions);
        }

        private async Task After_CursoPrompt(IDialogContext context, IAwaitable<string> result)
        {
            var selection = await result;
            PromptDialog.Text(context, After_TemaPrompt, $"¿Qué tema deseas consultar del curso de {selection} ?.");
        }

        private async Task After_TemaPrompt(IDialogContext context, IAwaitable<string> result)
        {
            var selection = await result;
            await context.PostAsync($"Esta es una pregunta del tema de {selection}.");
        }
        // fin flujo de saludo

        [LuisIntent("Curso")]
        public async Task CursoIntent(IDialogContext context, LuisResult result)
        {

        }

        //flujo pregunta
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

            CreditoServiceClient creditoServiceClient = new CreditoServiceClient();
            Credito credito = new Credito();
            credito.CodCredito = 9;
            credito.CodAlumno = "A09";
            credito.CodCurso = "C01";
            credito.CodDescripcion = "P10";
            credito.Tipo = "P";
            creditoServiceClient.CrearCredito(credito);

            await context.PostAsync($"Gracias por contribuir con Ask Bot Upecino!");

            context.Wait(MessageReceived);
        }
        //fin flujo pregunta

        private async Task ShowLuisResult(IDialogContext context, LuisResult result) 
        {
            await context.PostAsync($"Escogiste {result.Intents[0].Intent}. Dijiste: {result.Query}");
            context.Wait(MessageReceived);
        }
    }
}