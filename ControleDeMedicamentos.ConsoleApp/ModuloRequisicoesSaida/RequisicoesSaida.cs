
using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida
{
    public class RequisicoesSaida : EntidadeBase<RequisicoesSaida>
    {
            public DateTime dataRequisicaoSaida;
            public Paciente paciente;
            public Medicamento medicamentoRequisicao;

        public RequisicoesSaida(DateTime dataRequisicaoSaida, Paciente paciente, Medicamento medicamentoRequisicao)
        {
            this.dataRequisicaoSaida = dataRequisicaoSaida;
            this.paciente = paciente;
            this.medicamentoRequisicao = medicamentoRequisicao;
        }
        public override void AtualizarRegistro(RequisicoesSaida requisicaoSaidaEditada)
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
