

using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida
{
    public class TelaRequisicoesSaida : TelaBase<RequisicoesSaida>, ITelaCrud
    {
        public Paciente paciente;
        public Medicamento medicamento;
        public TelaPaciente telaPaciente;
        public TelaMedicamento telaMedicamento;
        public RepositorioMedicamentoEmArquivo repositorioMedicamento;
        public RepositorioPacienteEmArquivo repositorioPaciente;

        public TelaRequisicoesSaida(IRepositorioRequisicoesSaida repositorio, TelaPaciente telaPaciente, TelaMedicamento telaMedicamento, RepositorioMedicamentoEmArquivo repositorioMedicamento, RepositorioPacienteEmArquivo repositorioPaciente) : base("RequisicoesDeSaida", repositorio)
        {
            this.telaPaciente = telaPaciente;
            this.telaMedicamento = telaMedicamento;
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioPaciente = repositorioPaciente;
            this.telaPaciente = telaPaciente;
        }
        public override RequisicoesSaida ObterDados()
        {

            Console.WriteLine("Digite a data que esta sendo feito essa requisicao da seguinta maneira: (DD/MM/YYY)");
            DateTime dataRequisicaoSaida = Convert.ToDateTime(Console.ReadLine());

            telaPaciente.VisualizarRegistros(false);
            Console.WriteLine("Digite o id do paciente que esta sendo feito a requisição");
            int idPaciente = Convert.ToInt32(Console.ReadLine());
            bool pacienteExiste = VerificarIdPaciente(idPaciente);

            telaMedicamento.VisualizarRegistros(false);

            Console.WriteLine("Digite o id que do medicamento que esta sendo feito a requisição");
            int idMedicamento = Convert.ToInt32(Console.ReadLine());
            bool medicamentoExiste = VerificarIdMedicamentoEQuantidade(idMedicamento, default);

            if (medicamentoExiste)
            {
                Console.WriteLine("Digite a quantidade do medicamento que esta saindo");
                int quantidadeMedicamento = Convert.ToInt32(Console.ReadLine());
                VerificarIdMedicamentoEQuantidade(default, quantidadeMedicamento);
            }


          
                RequisicoesSaida requisicaoSaida = new RequisicoesSaida(dataRequisicaoSaida, paciente, medicamento);
                return requisicaoSaida;
            

           
        }

        private bool VerificarIdMedicamentoEQuantidade(int idMedicamento, int quantidadeMedicamentos)
        {
            bool medicamentoExiste = false;
            List<Medicamento> medicamentos = repositorioMedicamento.registros;
            foreach(Medicamento item in medicamentos)
            {
                if(item.Id == idMedicamento)
                {
                    medicamentoExiste = true;
                   for(int i = 0; i < repositorioMedicamento.registros.Count; i ++)
                    {
                        if(idMedicamento == repositorioMedicamento.registros[i].Id)
                        {
                            medicamento = repositorioMedicamento.registros[i];
                            repositorioMedicamento.registros[i].QuantidadeEmEstoque = (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos);
                        }
                    }
                }
            }
            return medicamentoExiste;
        }

        private bool VerificarIdPaciente(int idPaciente)
        {
            bool pacienteExiste = false;
            if (repositorioPaciente.registros != null)
            {
                List<Paciente> pacientes = repositorioPaciente.registros;

                foreach (Paciente item in pacientes)
                {
                    if (item.Id == idPaciente)
                    {
                        paciente = item;
                        pacienteExiste = true;
                    }
                }
            }

            return pacienteExiste;

        }

        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, -10} | {1, -30} | {2, -20} | {3, -20}",
            "Id", "Data", "Paciente", "Medicamento");
        }

        protected override void ExibirLinhaTabela(RequisicoesSaida requisicaoSaida)
        {
            Console.WriteLine("{0, -10} | {1, -30} | {2, -20} | {3, -20}",
            requisicaoSaida.Id, requisicaoSaida.dataRequisicaoSaida, requisicaoSaida.paciente, requisicaoSaida.medicamentoRequisicao);
        }
    }
}
