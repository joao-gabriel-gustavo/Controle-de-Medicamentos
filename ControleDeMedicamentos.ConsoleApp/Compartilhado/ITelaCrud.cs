namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public interface ITelaCrud
{
    char ApresentarMenu();
    void CadastrarRegistro();
    void EditarRegistro();
    void ExcluirRegistro();
    void VisualizarRegistros(bool exibirCabecalho);
}
