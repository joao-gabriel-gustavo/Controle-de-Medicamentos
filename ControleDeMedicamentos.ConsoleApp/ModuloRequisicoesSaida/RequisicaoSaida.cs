
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricoesMedicas;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida
{
    public class RequisicaoSaida : EntidadeBase<RequisicaoSaida>
    {
            public DateTime dataRequisicaoSaida { get; set; }
            public Paciente paciente { get; set; }
            public Medicamento medicamento { get; set; }
            public PrescricaoMedica prescricaoMedica { get; set; } 
        public RequisicaoSaida()
        {
        }
        public override void AtualizarRegistro(RequisicaoSaida requisicaoSaidaEditada)
        {
            dataRequisicaoSaida = requisicaoSaidaEditada.dataRequisicaoSaida;
            paciente = requisicaoSaidaEditada.paciente;
            medicamento = requisicaoSaidaEditada.medicamento;
            prescricaoMedica = requisicaoSaidaEditada.prescricaoMedica;
        }

        public override string Validar()
        {
            string erros = "";

            if (dataRequisicaoSaida == default)
                erros += "A data de prescricao é invalida.\n";

            else if (dataRequisicaoSaida > DateTime.Today)
                erros += "A data de prescricao não pode ser futura.\n";

            return erros.Trim();
        }
    }
}
