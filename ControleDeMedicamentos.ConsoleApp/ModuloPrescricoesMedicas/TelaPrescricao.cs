using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricoesMedicas
{
    public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
    {
        private IRepositorioPrescricaoMedica repositorioPrescricao;
        public IRepositorioMedicamento repositorioMedicamento;
        public TelaPrescricaoMedica(IRepositorioPrescricaoMedica repositorio, IRepositorioMedicamento repositorioMedicamento) : base("Prescrição Médica", repositorio)
        {
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioPrescricao = repositorio;
        }
        public override void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine();

            Console.WriteLine($"Cadastrando Requisição Medica...");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine();

            
            PrescricaoMedica novoRegistro = ObterDados();
            if (novoRegistro != null)
            {
                string erros = novoRegistro.Validar();

                if (erros.Length > 0)
                {
                    Notificador.ExibirMensagem(erros, ConsoleColor.Red);

                    CadastrarRegistro();

                    return;
                }

                repositorio.CadastrarRegistro(novoRegistro);
                Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
            }
        }
        
        public override PrescricaoMedica ObterDados()
        {
            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarRegistros();

            if (medicamentos.Count == 0)
            { 
                Console.WriteLine("Para realizar a prescrição voce precisa de medicamentos registrados\nAperte ENTER para continuar");
                Console.ReadLine();
                return null;
            }

            else
            {
                Console.Write("Digite o CRM do médico: ");
                string CRM = Console.ReadLine() ?? string.Empty;

                Console.Write("Digite a data da prescrição (dd/mm/yyyy): ");
                DateTime dataPrescricao;
                while (!DateTime.TryParse(Console.ReadLine(), out dataPrescricao))
                {
                    Console.Write("Data inválida. Digite novamente (dd/mm/yyyy): ");
                }

                
                Console.WriteLine("Digite");
                PrescricaoMedica PrescricaoMedica = new PrescricaoMedica(CRM, dataPrescricao);

                return PrescricaoMedica;
            }
        }
        
        protected override void ExibirCabecalhoTabela()
        {
            Console.WriteLine(
                "{0, -6} | {1, -15} | {2, -20}",
                "Id", "CRM", "Data da Prescrição"
            );
        }

        protected override void ExibirLinhaTabela(PrescricaoMedica registro)
        {
            Console.WriteLine(
                "{0, -6} | {1, -15} | {2, -20}",
                registro.Id, registro.CRM, registro.DataPrescricao.ToShortDateString()
            );
        }
    }
}