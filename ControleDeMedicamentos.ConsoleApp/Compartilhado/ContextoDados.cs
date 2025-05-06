using System.Text.Json.Serialization;
using System.Text.Json;
using ControleDeMedicamentos.ConsoleApp.ModuloFornecedor;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida;
using ControleDeMedicamentos.ConsoleApp.ModuloPaciente;
using ControleDeMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleDeMedicamentos.ConsoleApp.ModuloFuncionarios;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesEntrada;
using ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesSaida;
using ControleDeMedicamentos.ConsoleApp.ModuloPrescricoesMedicas;

namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public class ContextoDados
{
    private string pastaArmazenamento = "C:\\temp";
    private string arquivoArmazenamento = "dados-controle-medicamento.json";

    public List<Fornecedor> Fornecedores { get; set; }
    public List<Paciente> Pacientes { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    public List<Funcionario> Funcionarios { get; set; }
    public List<PrescricaoMedica> PrescricoesMedicas { get; set; }
    public List<RequisicaoEntrada> RequisicoesEntrada { get; set; }
    public List<RequisicaoSaida> RequisicoesSaida { get; set; }

    public ContextoDados()
    {
        Fornecedores = new List<Fornecedor>();  
        Pacientes = new List<Paciente>();
        Medicamentos = new List<Medicamento>();
        Funcionarios = new List<Funcionario>();
        PrescricoesMedicas = new List<PrescricaoMedica>();
        RequisicoesEntrada = new List<RequisicaoEntrada>();
        RequisicoesSaida = new List<RequisicaoSaida>();
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento))
            Directory.CreateDirectory(pastaArmazenamento);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(json, jsonOptions)!;

        if (contextoArmazenado == null) return;

        Fornecedores = contextoArmazenado.Fornecedores;
        Pacientes = contextoArmazenado.Pacientes;
        Medicamentos = contextoArmazenado.Medicamentos;
        Funcionarios = contextoArmazenado.Funcionarios;
        PrescricoesMedicas = contextoArmazenado.PrescricoesMedicas;
        RequisicoesEntrada = contextoArmazenado.RequisicoesEntrada;
        RequisicoesSaida = contextoArmazenado.RequisicoesSaida;
    }
}


