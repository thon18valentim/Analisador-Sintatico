
namespace AnalisadorSintatico
{
  internal class Acao
  {
    public string Input { get; init; }
    public int GoTo { get; private set; }
    public bool EFinal { get; private set; }

    public Acao(string input, int goTo, bool eFinal)
    {
      Input = input;
      GoTo = goTo;
      EFinal = eFinal;
    }

    public void SetGoTo(int goTo)
    {
      GoTo = goTo;
    }
  }
}
