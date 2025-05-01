namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public interface IRepositorio<T> where T : EntidadeBase<T>
{
    public void CadastrarRegistro(T novoRegistro);

    public bool EditarRegistro(int idRegistro, T registroEditado);

    public bool ExcluirRegistro(int idRegistro);

    public List<T> SelecionarRegistros();

    public T SelecionarRegistroPorId(int idRegistro);
}
