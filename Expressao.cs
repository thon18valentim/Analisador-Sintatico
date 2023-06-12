
namespace AnalisadorSintatico
{
  internal class Expressao
  {
    public Letra LetraGeradora { get; init; }
    public List<Letra> LetrasGeradas { get; init; }
    private Ponto Ponto { get; set; }
    public int ApontaPara { get; private set; }

    public Expressao(Letra letraGeradora, List<Letra> letrasGeradas)
    {
      Ponto = new();
      LetraGeradora = letraGeradora;
      LetrasGeradas = letrasGeradas;
      ApontaPara = -1;
    }

    public Expressao(Ponto ponto, Letra letraGeradora, List<Letra> letrasGeradas)
    {
      Ponto = ponto;
      LetraGeradora = letraGeradora;
      LetrasGeradas = letrasGeradas;
      ApontaPara = -1;
    }

    public Item LerProximaLetra(Item itemNovo)
    {
      var letraRecebida = LetrasGeradas[Ponto.Posicao]; 
      Console.WriteLine($"Passando {letraRecebida}");

      Ponto.AvancarPonto();

      Console.WriteLine(this);
      Console.WriteLine("\n");

      itemNovo.InserirExpressao(this);

      if (Ponto.Posicao > LetrasGeradas.Count - 1)
      {
        itemNovo.ConcluirItem();

        if (LetraGeradora.GeraEstadoAceitacao)
          itemNovo.SetarComoEstadoDeAceitacao();

        return itemNovo;
      }

      return PegarProximaLetra().GerarExpressoes(itemNovo);
    }

    private Letra PegarProximaLetra()
    {
      return LetrasGeradas[Ponto.Posicao];
    }

    public void ApontarParaItem(int index)
      => ApontaPara = index;

    public Letra PegarLetraAtual()
      => LetrasGeradas[Ponto.Posicao];

    public override string ToString()
    {
      var letrasGeradas = string.Empty;
      LetrasGeradas.ForEach(l => letrasGeradas += l.ToString());

      var expressaoFinal = string.Empty;
      for (int i = 0; i < letrasGeradas.Length; i++)
      {
        if (i == Ponto.Posicao)
          expressaoFinal += Ponto.Nome;

        expressaoFinal += letrasGeradas[i];
      }

      if (Ponto.Posicao > expressaoFinal.Length - 1)
        expressaoFinal += Ponto.Nome;

      return $"{LetraGeradora} -> {expressaoFinal}";
    }

    public string ToStringSemPonto()
    {
      var letrasGeradas = string.Empty;
      LetrasGeradas.ForEach(l => letrasGeradas += l.ToString());

      return $"{LetraGeradora} -> {letrasGeradas}";
    }
  }
}
