using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Util;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricoesMedicas
{
    public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
    {
        private IRepositorioPrescricaoMedica repositorioPrescricao;

        public TelaPrescricaoMedica(IRepositorioPrescricaoMedica repositorio) : base("Prescrição Médica", repositorio)
        {
            this.repositorioPrescricao = repositorio;
        }

        public override PrescricaoMedica ObterDados()
        {
            Console.Write("Digite o CRM do médico: ");
            string CRM = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite a data da prescrição (dd/mm/yyyy): ");
            DateTime dataPrescricao;
            while (!DateTime.TryParse(Console.ReadLine(), out dataPrescricao))
            {
                Console.Write("Data inválida. Digite novamente (dd/mm/yyyy): ");
            }

            PrescricaoMedica PrescricaoMedica = new PrescricaoMedica(CRM, dataPrescricao);

            return PrescricaoMedica;
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