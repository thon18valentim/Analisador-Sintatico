
using Spectre.Console;

namespace AnalisadorSintatico
{
  internal class TabelaCanonica
  {
    public List<string> Acoes { get; private set; }
    public List<string> GoTo { get; private set; }
    private string[] Cabecalho { get; set; }
    private string[,] Tabela { get; set; }

    public TabelaCanonica(List<string> acoes, List<string> goTo)
    {
      Acoes = acoes;
      GoTo = goTo;

      Acoes.Add("$");
    }

    public void CriarTabela(List<Item> items, Gramatica gramatica)
    {
      var opcs = Acoes.Concat(GoTo).ToList();

      Cabecalho = new string[opcs.Count];
      Tabela = new string[items.Count, opcs.Count + 1];

      CriarCabecalho(opcs);
      SetarNumeroDeReducoes(items, gramatica);

      var tabelaView = new Table();
      tabelaView.AddColumn(new TableColumn("Item").Centered());
      Acoes.ForEach(ac => tabelaView.AddColumn(new TableColumn(ac).Centered()));
      GoTo.ForEach(gt => tabelaView.AddColumn(new TableColumn(gt).Centered()));

      AnsiConsole.Write(new Markup("\n[bold green]Tabela Canônica[/]\n"));
      for (int i = 0; i < items.Count; i++)
      {
        var item = items.FirstOrDefault(it => it.Index == i) ?? throw new Exception("Erro, item não encontrado na criação da tabela");
        Tabela[i, 0] = i.ToString();

        if (item.Aceitacao)
        {
          //tabelaTEMP[i, Acoes.Count - 1] = "X";
          Tabela[i, Acoes.Count] = "X";
          continue;
        }
        else if (item.Reducao)
        {
          for (int j = 0; j < Acoes.Count; j++)
          {
            Tabela[i, j + 1] = $"r{item.NumeroReducao}";
          }
          continue;
        }
        else
        {
          for (int j = 0; j < opcs.Count; j++)
          {
            if (opcs[j] == "$")
              continue;

            var letra = opcs[j];
            var acao = item.Acoes.FirstOrDefault(ac => ac.Input == letra);
            if (acao != null)
            {
              if (acao.EFinal)
              {
                Tabela[i, j + 1] = $"S{acao.GoTo}";
              }
              else
              {
                Tabela[i, j + 1] = $"{acao.GoTo}";
              }
            }
          }
        }
      }

      MostrarTabela(tabelaView);
    }

    private void CriarCabecalho(List<string> opcs)
    {
      int index = 0;
      opcs.ForEach(op =>
      {
        Cabecalho[index] = op;
        index++;
      });
    }

    private static void SetarNumeroDeReducoes(List<Item> items, Gramatica gramatica)
    {
      var listaFiltrada = items.FindAll(it => it.Reducao);
      listaFiltrada.ForEach(it =>
      {
        //if (it.Expressoes.Any(ex => string.Equals(ex.ToStringSemPonto(), gramatica.Letras)))
        //int contador = 0;
        foreach (var lt in gramatica.Letras)
        {
          foreach (var g in lt.Galhos)
          {
            if (it.Expressoes.Any(ex => Equals(ex.ToStringSomenteLetrasGeradas(), g.ToString())))
            {
              it.NumeroReducao = g.NumeroReducao;
            }
          }
        }

        //it.NumeroReducao = contador;
        //contador += 1;
      });
    }

    private void MostrarTabela(Table tabelaView)
    {
      CorrigindoNulos();

      for (int i = 0; i < Tabela.GetLength(0); i++)
      {
        var texts = new string[Tabela.GetLength(1)];
        for (int j = 0; j < Tabela.GetLength(1); j++)
        {
            texts[j] = Tabela[i, j];
        }

        tabelaView.AddRow(texts);
      }

      AnsiConsole.Write(tabelaView);
    }

    private void CorrigindoNulos()
    {
      for (int i = 0; i < Tabela.GetLength(0); i++)
      {
        for (int j = 0; j < Tabela.GetLength(1); j++)
        {
          if (Tabela[i, j] == null)
            Tabela[i, j] = " ";
        }
      }
    }

    public string[,] PegarTabela()
    {
      if (Tabela == null)
        throw new Exception("Erro, a tabela deve ser CRIADA antes de ser acessada.");

      return Tabela;
    }

    public string[] PegarCabecalho()
    {
      return Cabecalho;
    }
  }
}
