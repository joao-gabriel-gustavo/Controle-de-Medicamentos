

using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida
{
    public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaCrud
    {
        public Paciente pacienteSelecionado = new Paciente("tralala", "tralala", "tralala");
        public Medicamento medicamentoSelecionado = new Medicamento("tralala", "tralala", 0, default!);
        public TelaPaciente telaPaciente;
        public TelaMedicamento telaMedicamento;
        public RepositorioMedicamentoEmArquivo repositorioMedicamento;
        public RepositorioPacienteEmArquivo repositorioPaciente;

        public TelaRequisicaoSaida(IRepositorioRequisicaoSaida repositorio, TelaPaciente telaPaciente, TelaMedicamento telaMedicamento, RepositorioMedicamentoEmArquivo repositorioMedicamento, RepositorioPacienteEmArquivo repositorioPaciente) : base("Requisicoes de Saida", repositorio)
        {
            this.telaPaciente = telaPaciente;
            this.telaMedicamento = telaMedicamento;
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioPaciente = repositorioPaciente;
            this.telaPaciente = telaPaciente;
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine();

            Console.WriteLine($"Cadastrando {nomeEntidade}...");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine();

            RequisicaoSaida novaRequisicao = ObterDados();

            
            string erros = novaRequisicao.Validar();

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);

                CadastrarRegistro();

                return;
            }

            if (novaRequisicao != null)
            {
                repositorio.CadastrarRegistro(novaRequisicao);
                Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
            }
        }
        public override RequisicaoSaida ObterDados()
        {
            Console.WriteLine("Digite a data que esta sendo feito essa requisicao da seguinta maneira: (DD/MM/YYY)");
            DateTime dataRequisicaoSaida = Convert.ToDateTime(Console.ReadLine());

            telaPaciente.VisualizarRegistros(false);

            Console.WriteLine("Digite o id do paciente que esta sendo feito a requisição");
            int idPaciente = Convert.ToInt32(Console.ReadLine());

            pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(idPaciente);

            if(pacienteSelecionado == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Id invalido, Aperte ENTER para tentar novamente");
                Console.ReadLine();
                Console.ResetColor();
                ObterDados();
            }

            telaMedicamento.VisualizarRegistros(false);

            Console.WriteLine("Digite o id que do medicamento que esta sendo feito a requisição");
            int idMedicamento = Convert.ToInt32(Console.ReadLine());

            medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

            if (medicamentoSelecionado == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Id invalido, Aperte ENTER para tentar novamente");
                Console.ReadLine();
                Console.ResetColor();
                ObterDados();
            }
            Console.WriteLine("Digite a quantidade do medicamento que esta saindo");
            int quantidadeMedicamento = Convert.ToInt32(Console.ReadLine());

            RequisicaoSaida requisicaoSaida = new RequisicaoSaida();
            bool quantidadeSuportada = VerificarQuantidadeMedicamento(idMedicamento,  quantidadeMedicamento);
           
            if (quantidadeSuportada)
            {
                requisicaoSaida.dataRequisicaoSaida = dataRequisicaoSaida;
                requisicaoSaida.paciente = pacienteSelecionado;
                requisicaoSaida.medicamentoRequisicao = medicamentoSelecionado;
                return requisicaoSaida;
            }
            return requisicaoSaida;
        }
        private bool VerificarQuantidadeMedicamento(int idMedicamento, int quantidadeMedicamentos)
        {
            bool quantidadeSuportada = false;
            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();
            quantidadeSuportada = false;

            for (int i = 0; i < medicamentos.Count; i++)
            {
                if (idMedicamento == repositorioMedicamento.registros[i].Id)
                {
                    if (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos > 0)
                    {
                        repositorioMedicamento.registros[i].QuantidadeEmEstoque = (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos);
                        quantidadeSuportada = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Quantidade invalida, resgatando essa quantidade de medicamentos seu estoque iria ficar negativado\nAperte ENTER para continuar");
                        Console.ReadLine();
                        quantidadeSuportada = false;
                        Console.ResetColor();
                    }
                }
            }
            return quantidadeSuportada;
        }
   
        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, -10} | {1, -20} | {2, -30} | {3, -30}",
            "Id", "Data", "Paciente", "Medicamento");
        }
        protected override void ExibirLinhaTabela(RequisicaoSaida requisicaoSaida)
        {
            if (requisicaoSaida.paciente != null && requisicaoSaida.medicamentoRequisicao != null)
            {
                Console.WriteLine("{0, -10} | {1, -20} | {2, -30} | {3, -30}",
                requisicaoSaida.Id, requisicaoSaida.dataRequisicaoSaida.ToShortDateString(), requisicaoSaida.paciente.Nome, requisicaoSaida.medicamentoRequisicao.Nome);
            }
        }
    }
}
