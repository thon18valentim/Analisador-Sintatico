
namespace AnalisadorSintatico
{
  internal class Letra : ItemGramatical
  {
    public int Id { get; init; }
    public string Nome { get; init; }
    public List<Galho> Galhos { get; private set; }
    public bool EPonto { get; set; }
    public bool EFinal { get; init; }
    public bool GeraEstadoAceitacao { get; init; }

    private bool lida = false;

    public Letra(int id, string nome, bool eFinal, bool geraEstadoAceitacao)
    {
      Id = id;
      Nome = nome;
      EPonto = false;
      EFinal = eFinal;
      Galhos = new();
      GeraEstadoAceitacao = geraEstadoAceitacao;
    }

    public void CriarGalhos(List<Galho> galhos)
    {
      Galhos = galhos;
    }

    public override string ToString()
    {
      return Nome;
    }

    public List<Expressao> GerarExpressoes()
    {
      if (lida)
        return null;
      else
        lida = true;

      List<Expressao> expressoes = new();
      List<List<Expressao>> subExpressoes = new();

      for (int i = 0; i < Galhos.Count; i++)
      {
        Expressao expressao = new(this, Galhos[i].Letras);
        expressoes.Add(expressao);

        var proximaLetra = expressao.PegarProximaLetra();
        if (proximaLetra == null)
          continue;

        var lista = proximaLetra.GerarExpressoes();
        if (lista != null)
          subExpressoes.Add(lista);

        //for (int j = 0; j < Galhos[i].Letras.Count; j++)
        //{
        //  if (Galhos[i].Letras[j].Id == Id)
        //    continue;

        //  var lista = Galhos[i].Letras[j].GerarExpressoes();

        //  if (lista != null)
        //    subExpressoes.Add(lista);
        //}
      }

      foreach (var listaDeExpressoes in subExpressoes)
      {
        foreach (var expressao in listaDeExpressoes)
        {
          expressoes.Add(expressao);
        }
      }

      return expressoes;
    }

    public Item GerarExpressoes(Item item)
    {
      List<Expressao> expressoes = new();
      List<List<Expressao>> subExpressoes = new();

      for (int i = 0; i < Galhos.Count; i++)
      {
        Expressao expressao = new(this, Galhos[i].Letras);
        expressoes.Add(expressao);

        var proximaLetra = expressao.PegarProximaLetra();
        if (proximaLetra == null)
          continue;

        var lista = proximaLetra.GerarExpressoes();
        if (lista != null)
          subExpressoes.Add(lista);

        //for (int j = 0; j < Galhos[i].Letras.Count; j++)
        //{
        //  if (Galhos[i].Letras[j].Id == Id)
        //    continue;

        //  var lista = Galhos[i].Letras[j].GerarExpressoes();

        //  if (lista != null)
        //    subExpressoes.Add(lista);
        //}
      }

      foreach (var listaDeExpressoes in subExpressoes)
      {
        foreach (var expressao in listaDeExpressoes)
        {
          if (!listaDeExpressoes.Any(ex => ex.ToString() == expressao.ToString()))
            expressoes.Add(expressao);
        }
      }

      item.InserirExpressoes(expressoes);

      return item;
    }
  }
}
