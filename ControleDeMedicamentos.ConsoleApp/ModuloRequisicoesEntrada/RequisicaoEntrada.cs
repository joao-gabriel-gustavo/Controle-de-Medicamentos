using ControleDeMedicamentos.ConsoleApp.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesEntrada;

public class RequisicaoEntrada : EntidadeBase<RequisicaoEntrada>
{
    public DateTime Data { get; set; }
    public Medicamento Medicamento { get; set; }
    public Funcionario Funcionario { get; set; }
    public int Quantidade { get; set; }

    public RequisicaoEntrada() { }

    public RequisicaoEntrada(DateTime data, Medicamento medicamento, Funcionario funcionario, int quantidade) : this()
    {
        Data = data;
        Medicamento = medicamento;
        Funcionario = funcionario;
        Quantidade = quantidade;
    }

    public override void AtualizarRegistro(RequisicaoEntrada registroEditado)
    {
        Data = registroEditado.Data;
        Medicamento = registroEditado.Medicamento;
        Funcionario = registroEditado.Funcionario;
        Quantidade = registroEditado.Quantidade;
    }

    public override string Validar()
    {
        string erros = "";

        if (Data == DateTime.MinValue)
            erros += "O campo 'Data' é obrigatório e deve ser uma data válida.\n";

        if (Medicamento == null)
            erros += "O campo 'Medicamento' é obrigatório.\n";

        if (Funcionario == null)
            erros += "O campo 'Funcionário' é obrigatório.\n";

        if (Quantidade <= 0)
            erros += "O campo 'Quantidade' deve ser um número positivo.\n";

        return erros;
    }
} 