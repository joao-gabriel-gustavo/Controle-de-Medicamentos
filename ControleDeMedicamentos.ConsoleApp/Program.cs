using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida;
using ControleDeMedicamentos.ConsoleApp.Util;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace ControleDeMedicamentos.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //criar um servidor web
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            WebApplication app = builder.Build();

            app.MapGet("/", PaginaInicial);

            app.MapGet("/Medicamentos/visualizar", VisualizarMedicamentos);

            app.Run();
        }

        static Task VisualizarMedicamentos(HttpContext context)
        {
            ContextoDados contextoDados = new ContextoDados();
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmArquivo(contextoDados);

            string conteudo = File.ReadAllText("ModuloMedicamento/html/Visualizar.html");

            StringBuilder stringBuilder = new StringBuilder(conteudo);

            foreach (Medicamento m in repositorioMedicamento.SelecionarRegistros())
            {
                string itemLista = $"Li>{false.ToString()}</Li> #medicamento#";

                stringBuilder.Replace("#Fabricantes#", itemLista);
            }
            stringBuilder.Replace("#Fabricantes#", "");

            string conteudoString = stringBuilder.ToString();


            return context.Response.WriteAsync(conteudo);
        }

        static Task PaginaInicial(HttpContext context)
        {
            string conteudo = File.ReadAllText("html/PaginaInicial.html");

            return context.Response.WriteAsync(conteudo);
        }
    }
}
