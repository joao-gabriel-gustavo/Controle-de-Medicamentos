namespace ControleDeMedicamentos.ConsoleApp.Util;

public static class Notificador
{
    public static void ExibirMensagem(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;

        Console.WriteLine();

        Console.WriteLine(mensagem);

        Console.ResetColor();

        Console.ReadLine();
    }
}
