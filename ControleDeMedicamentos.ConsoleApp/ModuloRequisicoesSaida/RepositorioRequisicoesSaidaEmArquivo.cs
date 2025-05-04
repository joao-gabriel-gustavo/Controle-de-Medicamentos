

using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida
{
    public class RepositorioRequisicoesSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicoesSaida>, IRepositorioRequisicoesSaida
    {
        public RepositorioRequisicoesSaidaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }
        protected override List<RequisicoesSaida> ObterRegistros()
        {
            return contexto.requisicoesSaidas;
        }
    }
}
