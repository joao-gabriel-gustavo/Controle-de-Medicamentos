
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida
{
    public class RequisicaoSaida : EntidadeBase<RequisicaoSaida>
    {
            public DateTime dataRequisicaoSaida { get; set; }
            public Paciente paciente { get; set; }
            public Medicamento medicamentoRequisicao { get; set; }

            
        public RequisicaoSaida()
        {
        }
        public override void AtualizarRegistro(RequisicaoSaida requisicaoSaidaEditada)
        {
            dataRequisicaoSaida = requisicaoSaidaEditada.dataRequisicaoSaida;
            paciente = requisicaoSaidaEditada.paciente;
            medicamentoRequisicao = requisicaoSaidaEditada.medicamentoRequisicao;
        }

        public override string Validar()
        {
            string erros = "";
            return erros.Trim();
        }
    }
}
