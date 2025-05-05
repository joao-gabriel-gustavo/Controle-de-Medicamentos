using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricoesMedicas
{
    public class RepositorioPrescricaoMedicaEmArquivo : RepositorioBaseEmArquivo<PrescricaoMedica>, IRepositorioPrescricaoMedica
    {
        public RepositorioPrescricaoMedicaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<PrescricaoMedica> ObterRegistros()
        {
            return contexto.PrescicoesMedicas;
        }
    }
}
