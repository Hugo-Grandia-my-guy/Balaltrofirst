using ConsoleApp1;


class Program
{
    static void Main()
    {
        Console.OutputEncoding =
            System.Text.Encoding.UTF8;

        Round round = new Round();

        RoundViewModel viewModel =
            new RoundViewModel(round);

        viewModel.RetrieveData();
        viewModel.Render();
    }
}
