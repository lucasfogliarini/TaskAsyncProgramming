//ref.: https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/concepts/async/
using System.Diagnostics;

namespace TaskAsyncProgramming
{
    internal class Coffee { }
    internal class Egg { }

    class Program
    {
        static Stopwatch _stopWatch = new Stopwatch();

        static async Task Main(string[] args)
        {
            WriteColor("............. (6 segundos) Assíncrono esperando a primeira tarefa terminar para começar a segunda ..............", ConsoleColor.Yellow);

            await MakeBreakfast6secsAsync();

            Console.WriteLine();

            WriteColor("............. (3 segundos) Assíncrono iniciando as tarefas antes de esperar terminar ..............", ConsoleColor.Yellow);

            await MakeBreakfast3secsAsync();
        }

        static async Task MakeBreakfast3secsAsync()
        {
            _stopWatch.Restart();
            Task<Egg> eggTask = FryEggsAsync();
            Task<Coffee> coffeeTask = MakeCoffeAsync();

            var egg = await eggTask;
            var coffe = await coffeeTask;

            WriteColor($"Café da manhã ficou pronto em {_stopWatch.Elapsed.TotalSeconds} segundos!", ConsoleColor.Green);
        }

        static async Task MakeBreakfast6secsAsync()
        {
            _stopWatch.Restart();
            Egg egg = await FryEggsAsync();
            Coffee coffee = await MakeCoffeAsync();
            _stopWatch.Stop();

            WriteColor($"Café da manhã ficou pronto em {_stopWatch.Elapsed.TotalSeconds} segundos!", ConsoleColor.Green);
        }

        private static async Task<Egg> FryEggsAsync()
        {
            Console.WriteLine("Fritando o ovo ...");
            await Task.Delay(3000);

            WriteColor("Ovo frito pronto! \n", ConsoleColor.Green);

            return new Egg();
        }

        private static async Task<Coffee> MakeCoffeAsync()
        {
            Console.WriteLine("Passando café ...");
            await Task.Delay(3000);

            WriteColor("Café preto pronto! \n", ConsoleColor.Green);

            return new Coffee();
        }

        private static void WriteColor(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}