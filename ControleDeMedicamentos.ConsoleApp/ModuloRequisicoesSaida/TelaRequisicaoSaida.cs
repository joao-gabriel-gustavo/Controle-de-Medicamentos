

using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricoesMedicas;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida
{
    public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaCrud
    {
        public TelaPaciente telaPaciente;
        public TelaMedicamento telaMedicamento;
        public TelaPrescricaoMedica telaPrescricaoMedica;
        public RepositorioMedicamentoEmArquivo repositorioMedicamento;
        public RepositorioPacienteEmArquivo repositorioPaciente;
        public RepositorioPrescricaoMedicaEmArquivo repositorioPrescricaoMedica;
        private IRepositorioRequisicaoSaida repositorioRequisicaoSaida;
        public TelaRequisicaoSaida(IRepositorioRequisicaoSaida repositorio, TelaPaciente telaPaciente, TelaMedicamento telaMedicamento, TelaPrescricaoMedica telaPrescricaoMedica, RepositorioMedicamentoEmArquivo repositorioMedicamento, RepositorioPacienteEmArquivo repositorioPaciente, RepositorioPrescricaoMedicaEmArquivo repositorioPrescricaoMedica) : base("Requisicoes de Saida", repositorio)
        {
            this.telaMedicamento = telaMedicamento;
            this.telaPaciente = telaPaciente;
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioPaciente = repositorioPaciente;
            this.telaPrescricaoMedica = telaPrescricaoMedica;
            this.repositorioPrescricaoMedica = repositorioPrescricaoMedica;
            repositorioRequisicaoSaida = repositorio;
        }

        public override char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine();

            Console.WriteLine($"1 - Cadastrar Requisição de Saida");
            Console.WriteLine($"2 - Editar Requisição de Saida");
            Console.WriteLine($"3 - Excluir Requisição de Saida");
            Console.WriteLine($"4 - Visualizar Requisições de Saida");
            Console.WriteLine($"5 - Visualizar Requisição de Saida por Paciente Especifico");
            Console.WriteLine("S - Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma das opções: ");
            char operacaoEscolhida = Convert.ToChar(Console.ReadLine()!);

            return operacaoEscolhida;
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

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(idPaciente);

            if (pacienteSelecionado == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Id invalido, Aperte ENTER para tentar novamente");
                Console.ReadLine();
                Console.ResetColor();
                ObterDados();
            }

            telaPrescricaoMedica.VisualizarRegistros(false);

            Console.WriteLine("Digite o id da prescrição medica que sera associada ao paciente");
            int idPrescricaoMedica = Convert.ToInt32(Console.ReadLine());
            PrescricaoMedica prescricaoMedicaSelecionada = repositorioPrescricaoMedica.SelecionarRegistroPorId(idPrescricaoMedica);

            if (prescricaoMedicaSelecionada == null)
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
            Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

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
                requisicaoSaida.medicamento = medicamentoSelecionado;
                requisicaoSaida.prescricaoMedica = prescricaoMedicaSelecionada;
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
                 

                     if (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos > 0 && repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos <= 20)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"Resgatando essa quantidade voce irá ficara com apenas {repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos} unidades disponiveis no estoque \nAperte ENTER para continuar");
                        Console.ReadLine();
                        Console.ResetColor();
                        repositorioMedicamento.registros[i].QuantidadeEmEstoque = (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos);
                        quantidadeSuportada = true;
                    }

                    else if (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos > 0)
                    {
                        repositorioMedicamento.registros[i].QuantidadeEmEstoque = (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos);
                        quantidadeSuportada = true;
                    }

                    else if (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos == 0)
                    {
                        repositorioMedicamento.registros[i].QuantidadeEmEstoque = (repositorioMedicamento.registros[i].QuantidadeEmEstoque - quantidadeMedicamentos);
                        quantidadeSuportada = true;
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Resgatando essa quantidade voce irá ficara com o estoque desse medicamento zerado\nAperte ENTER para continuar");
                        Console.ReadLine();
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

        public void  VisualizarRequisicaoPacienteEspecifico()
        {
            telaPaciente.VisualizarRegistros(false);
           
            Console.WriteLine("Digite qual o id do paciente que deseja visualizar as requisições de saida");
            int idPacienteEspecifico = Convert.ToInt32(Console.ReadLine());
            bool requisicaoExiste = false;

            Paciente pacienteEspecifico = repositorioPaciente.SelecionarRegistroPorId(idPacienteEspecifico);

            if(pacienteEspecifico == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Id invalido, Aperte ENTER para tentar novamente");
                Console.ReadLine();
                Console.ResetColor();
                VisualizarRequisicaoPacienteEspecifico();
            }
            
            List<RequisicaoSaida> requisicoesSaidas = repositorioRequisicaoSaida.SelecionarRegistros();

            foreach(RequisicaoSaida requisicao in repositorioRequisicaoSaida.SelecionarRegistros())
            {
                if(requisicao.paciente.Nome == pacienteEspecifico.Nome)
                {
                    Console.WriteLine("{0, -10} | {1, -20} | {2, -30} | {3, -30}",
                    "Id", "Data",  "Medicamento", "Prescrição Medica ID}");

                    Console.WriteLine("{0, -10} | {1, -20} | {2, -30} | {3, -30}",
                    requisicao.Id, requisicao.dataRequisicaoSaida.ToShortDateString(), requisicao.medicamento.Nome, requisicao.prescricaoMedica.Id);
                    requisicaoExiste = true;
                    Console.ReadLine();
                }
            }

            if(requisicaoExiste == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Esse paciente não possui requisições de saida");
                Console.ReadLine();
                Console.ResetColor();
            }
        }
        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine("{0, -10} | {1, -20} | {2, -30} | {3, -30}, {4, -30}",
            "Id", "Data", "Paciente", "Medicamento", "Prescrição Medica ID}");
        }
        protected override void ExibirLinhaTabela(RequisicaoSaida requisicaoSaida)
        {
            if (requisicaoSaida.paciente != null && requisicaoSaida.medicamento != null)
            {
                Console.WriteLine("{0, -10} | {1, -20} | {2, -30} | {3, -30}, {4, -30}",
                requisicaoSaida.Id, requisicaoSaida.dataRequisicaoSaida.ToShortDateString(), requisicaoSaida.paciente.Nome, requisicaoSaida.medicamento.Nome, requisicaoSaida.prescricaoMedica.Id);
            }
        }
    }
}
