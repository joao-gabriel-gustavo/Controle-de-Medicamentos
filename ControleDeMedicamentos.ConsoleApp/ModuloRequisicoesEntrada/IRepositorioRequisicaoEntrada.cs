using ControleDeMedicamentos.ConsoleApp.Compartilhado;

namespace ControleDeMedicamentos.ConsoleApp.ModuloRequisicoesEntrada;

public interface IRepositorioRequisicaoEntrada : IRepositorio<RequisicaoEntrada>
{
    void RegistrarEntradaEAtualizarEstoque(RequisicaoEntrada novoRegistro);
} 