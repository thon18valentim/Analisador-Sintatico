
namespace AnalisadorSintatico
{
  internal class Ponto : ItemGramatical
  {
    public string Nome { get; init; }
    public bool EPonto { get; set; }
    public int Posicao { get; private set; }

    public Ponto()
    {
      Nome = ".";
      EPonto = true;
      Posicao = 0;
    }

    public Ponto(int posicao)
    {
      Nome = ".";
      EPonto = true;
      Posicao = posicao;
    }

    public void AvancarPonto()
    {
      Posicao += 1;
    }

    public static Ponto CriarNovoPonto(int posicao)
    {
      return new Ponto(posicao);
    }
  }
}
