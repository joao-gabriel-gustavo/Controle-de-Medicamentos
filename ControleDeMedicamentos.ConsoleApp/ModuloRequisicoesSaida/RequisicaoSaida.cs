
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
            DateTime anosDoisMil = new DateTime(01/01/2000);

            if (dataRequisicaoSaida < anosDoisMil)
            {
                erros += "Não permitimos o cadastro de requisicoes  com datas anteriores o de dois mil";
            }

            return erros.Trim();
        }
    }
}
