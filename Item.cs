
namespace AnalisadorSintatico
{
  internal class Item
  {
    public int Index { get; init; }
    public List<Expressao> ?Expressoes { get; private set; }
    public bool Aceitacao { get; private set; }
    public bool Reducao { get; private set; }
    public int NumeroReducao { get; set; }
    public bool Percorrido { get; set; }
    public List<Acao> Acoes { get; private set; }

    public Item(int index, List<Expressao> expressoes)
    {
      Index = index;
      Expressoes = expressoes;
      DeletarDuplicatas();
      Reducao = false;
      Percorrido = false;
      Acoes = new();
      Aceitacao = false;
    }

    public Item(int index)
    {
      Index = index;
      Expressoes = new();
      Reducao = false;
      Percorrido = false;
      Acoes = new();
      Aceitacao = false;
    }

    public void SetarComoEstadoDeAceitacao()
    {
      Aceitacao = true;
      Reducao = false;
    }

    public void InserirExpressao(Expressao expressao)
    {
      Expressoes?.Add(expressao);
    }

    public void InserirExpressoes(List<Expressao> expressoes)
    {
      foreach (var ex in expressoes)
      {
        Expressoes?.Add(ex);
      }
    }

    public void DeletarDuplicatas()
    {
      Expressoes = Expressoes?.DistinctBy(ex => ex.ToString()).ToList();
    }

    public override string ToString()
    {
      return $"Item {Index} - Percorrido: {Percorrido} & Redução: {Reducao} & Aceitação: {Aceitacao}\nExpressões:\n{string.Join("\n", Expressoes)}";
    }

    public List<Item> PassarAdiante(int contador, List<Item> listaAtual)
    {
      var listaDeItems = new List<Item>();
      for (int i = 0; i < Expressoes?.Count; i++)
      {
        Console.WriteLine("\n\n-----------------");
        var item = new Item(contador)
        {
          Percorrido = false
        };

        var letraRecebida = Expressoes[i].PegarLetraAtual();
        var acao = RegistrarAcao(letraRecebida, item.Index);
        var expressao = Expressao.CopiarExpressao(Expressoes[i]);

        item = expressao.LerProximaLetra(item);
        
        if (listaAtual.Count > 0)
        {
          var itemRepetido = listaAtual.FirstOrDefault(la => CompararItems(la, item));
          if (itemRepetido != null)
          {
            Console.WriteLine("Item Repetido, não salvando");

            acao.SetGoTo(itemRepetido.Index);

            continue;
          }
          else
          {
            Console.WriteLine(item);
            listaDeItems.Add(item);
            contador += 1;
          }
        }
        else
        {
          Console.WriteLine(item);
          listaDeItems.Add(item);
          contador += 1;
        }

        Console.WriteLine("-----------------");
      }

      return listaDeItems;
    }

    public void ConcluirItem()
    {
      Reducao = true;
      Percorrido = true;
    }

    public static bool CompararItems(Item itemA, Item itemB)
    {
      if (itemA?.Expressoes?.Count != itemB?.Expressoes?.Count) 
        return false;

      int contador = 0;
      foreach (var ex in itemA.Expressoes)
      {

        if (!Equals(ex.ToString(), itemB?.Expressoes?[contador].ToString()))
          return false;

        contador += 1;
      }

      return true;
    }

    private void GerarExpressoesLetraADireitaDoPonto()
    {
      foreach (var expressao in Expressoes)
      {

      }
    }

    public Acao RegistrarAcao(Letra letra, int goTo)
    {
      var acao = new Acao(letra.Nome, goTo, letra.EFinal);
      Acoes.Add(acao);
      return acao;
    }
  }
}
