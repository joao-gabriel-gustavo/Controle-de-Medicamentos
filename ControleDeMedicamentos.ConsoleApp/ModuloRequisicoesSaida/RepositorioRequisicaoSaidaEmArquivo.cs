

using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida
{
    public class RepositorioRequisicaoSaidaEmArquivo : RepositorioBaseEmArquivo<RequisicaoSaida>, IRepositorioRequisicaoSaida
    {
        public RepositorioRequisicaoSaidaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }
        protected override List<RequisicaoSaida> ObterRegistros()
        {
            return contexto.requisicoes;
        }
    }
}
