
using Spectre.Console;

namespace AnalisadorSintatico
{
  internal class TabelaCanonica
  {
    public List<string> Acoes { get; private set; }
    public List<string> GoTo { get; private set; }

    public TabelaCanonica(List<string> acoes, List<string> goTo)
    {
      Acoes = acoes;
      GoTo = goTo;

      Acoes.Add("$");
    }

    public void CriarTabela(List<Item> items)
    {
      var opcs = Acoes.Concat(GoTo).ToList();
      var tabela = new string[items.Count, opcs.Count];

      SetarNumeroDeReducoes(items);

      var tabelaView = new Table();
      Acoes.ForEach(ac => tabelaView.AddColumn(new TableColumn(ac).Centered()));
      GoTo.ForEach(gt => tabelaView.AddColumn(new TableColumn(gt).Centered()));

      AnsiConsole.Write(new Markup("\n[bold green]Tabela Canônica[/]\n"));
      for (int i = 0; i < items.Count; i++)
      {
        var item = items.FirstOrDefault(it => it.Index == i) ?? throw new Exception("Erro, item não encontrado na criação da tabela");
        
        if (item.Aceitacao)
        {
          tabela[i, Acoes.Count - 1] = "X";
          continue;
        }
        else if (item.Reducao)
        {
          for (int j = 0; j < Acoes.Count; j++)
          {
            tabela[i, j] = $"r{item.NumeroReducao}";
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
                tabela[i, j] = $"S{acao.GoTo}";
              }
              else
              {
                tabela[i, j] = $"{acao.GoTo}";
              }
            }
          }
        }
      }

      //MostrarTabela(tabela);
      MontarTabela(tabela, tabelaView);
      AnsiConsole.Write(tabelaView);
    }

    private static void SetarNumeroDeReducoes(List<Item> items)
    {
      int contador = 1;
      var listaFiltrada = items.FindAll(it => it.Reducao);
      listaFiltrada.ForEach(it =>
      {
        it.NumeroReducao = contador;
        contador += 1;
      });
    }

    private static void MostrarTabela(string[,] tabela)
    {
      CorrigindoEspacoEmBranco(tabela);

      Console.WriteLine("\n\n-- Demonstrando Tabela --");
      for (int i = 0; i < tabela.GetLength(0); i++)
      {
        for (int j = 0; j < tabela.GetLength(1); j++)
        {
          Console.Write($"{tabela[i, j]} ");
        }
        Console.Write("\n");
      }
    }

    private static void MontarTabela(string[,] tabela, Table tabelaView)
    {
      CorrigindoEspacoEmBranco(tabela);

      for (int i = 0; i < tabela.GetLength(0); i++)
      {
        var texts = new string[tabela.GetLength(1)];
        for (int j = 0; j < tabela.GetLength(1); j++)
        {
          if (tabela[i, j] == null)
            texts[j] = " ";
          else
            texts[j] = tabela[i, j];
        }

        tabelaView.AddRow(texts);
      }
    }

    private static void CorrigindoEspacoEmBranco(string[,] tabela)
    {
      for (int i = 0; i < tabela.GetLength(0); i++)
      {
        for (int j = 0; j < tabela.GetLength(1); j++)
        {
          if (tabela[i, j] == "")
            tabela[i, j] = " ";
        }
      }
    }
  }
}
