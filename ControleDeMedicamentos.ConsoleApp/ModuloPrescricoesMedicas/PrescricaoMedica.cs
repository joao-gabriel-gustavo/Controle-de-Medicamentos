using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ConsoleApp.ModuloPrescricoesMedicas
{
    public class PrescricaoMedica : EntidadeBase<PrescricaoMedica>
    {
        public string CRM { get; set; }
        public DateTime DataPrescricao { get; set; }
        public PrescricaoMedica(string crm, DateTime dataPrescricao)
        {
            CRM = crm;
            DataPrescricao = dataPrescricao;
        }

        public override void AtualizarRegistro(PrescricaoMedica registroEditado)
        {
            CRM = registroEditado.CRM;
            DataPrescricao = registroEditado.DataPrescricao;
        }

        public override string Validar()
        {
            throw new NotImplementedException();
        }
    }
}
