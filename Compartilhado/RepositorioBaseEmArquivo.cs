namespace ControleDeMedicamentos.ConsoleApp.Compartilhado;

public abstract class RepositorioBaseEmArquivo<T> where T : EntidadeBase<T>
{
    private List<T> registros = new List<T>();
    private int contadorIds = 0;

    protected ContextoDados contexto;

    protected RepositorioBaseEmArquivo(ContextoDados contexto)
    {
        this.contexto = contexto;

        registros = ObterRegistros();

        int maiorId = 0;

        foreach (var registro in registros)
        {
            if (registro.Id > maiorId)
                maiorId = registro.Id;
        }

        contadorIds = maiorId;
    }

    protected abstract List<T> ObterRegistros();

    public void CadastrarRegistro(T novoRegistro)
    {
        novoRegistro.Id = ++contadorIds;

        registros.Add(novoRegistro);

        contexto.Salvar();
    }

    public bool EditarRegistro(int idRegistro, T registroEditado)
    {
        foreach (T item in registros)
        {
            if (item.Id == idRegistro)
            {
                item.AtualizarRegistro(registroEditado);

                contexto.Salvar();

                return true;
            }
        }

        return false;
    }

    public bool ExcluirRegistro(int idRegistro)
    {
        T registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado != null)
        {
            registros.Remove(registroSelecionado);

            contexto.Salvar();

            return true;
        }

        return false;
    }

    public List<T> SelecionarRegistros()
    {
        return registros;
    }

    public T SelecionarRegistroPorId(int idRegistro)
    {
        foreach (T item in registros)
        {
            if (item.Id == idRegistro)
                return item;
        }

        return null;
    }

    
}
