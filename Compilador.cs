
using System.Diagnostics;
using System.Linq;

namespace AnalisadorSintatico
{
  internal class Compilador
  {
    private int acaoNaPilha;
    private bool aceitacao;

    private string Palavra { get; set; }
    private int Ponteiro { get; set; }
    private string[] CabecalhoCanonico { get; set; }
    private string[,] TabelaCanonica { get; set; }
    private Stack<string> PilhaDeAnaliseSintatica { get; set; }
    private List<Item> Estados { get; set; }
    private Gramatica GramaticaConhecida { get; set; }
    private List<Expressao> ExpressoesDeReducao { get; set; }

    public Compilador(string[] cabecalhoCanonico, string[,] tabelaCanonica, List<Item> estados, Gramatica gramaticaConhecida)
    {
      CabecalhoCanonico = cabecalhoCanonico;
      SetarTabelaCanonica(tabelaCanonica);
      Estados = estados;
      GramaticaConhecida = gramaticaConhecida;
    }

    public Compilador(string[] cabecalhoCanonico, string[,] tabelaCanonica, Gramatica gramaticaConhecida, List<Expressao> expressoesDeReducao)
    {
      CabecalhoCanonico = cabecalhoCanonico;
      SetarTabelaCanonica(tabelaCanonica);
      GramaticaConhecida = gramaticaConhecida;
      ExpressoesDeReducao = expressoesDeReducao;
    }

    private void InicializarPilhaDeAnaliseSintatica()
    {
      PilhaDeAnaliseSintatica = new();
      PilhaDeAnaliseSintatica.Push("$");
    }

    public void SetarPalavra(string palavra)
    {
      if (palavra[^1] != '$')
        palavra += "$";

      Palavra = palavra;
      InicializarPonteiro();
      InicializarPilhaDeAnaliseSintatica();
    }

    private void InicializarPonteiro()
      => Ponteiro = 0;

    private void SetarTabelaCanonica(string[,] tabelaCanonica)
      => TabelaCanonica = tabelaCanonica;

    public bool Executar()
    {
      acaoNaPilha = 1;
      aceitacao = false;

      PilhaDeAnaliseSintatica.Push("0");
      return LerPalavra();
    }

    private bool LerPalavra()
    {
      bool sucesso;

      if (acaoNaPilha == 0)
      {
        sucesso = BuscarAcaoNaTabela(PegarEstadoMaisProximo(), PegarLetraMaisProxima());
      }
      else if (acaoNaPilha == 1)
      {
        sucesso = BuscarAcaoNaTabela(PegarEstadoMaisProximo(), Palavra[Ponteiro]);
      }
      else
      {
        sucesso = BuscarAcaoNaTabela(PegarEstadoMaisProximo(), Palavra[Ponteiro]);
      }

      if (!sucesso)
        return false;

      if (aceitacao)
        return true;

      return LerPalavra();
    }

    private bool BuscarAcaoNaTabela(int estadoAtual, char letra)
    {
      int indexLetra = -1;
      for (int j = 0; j < CabecalhoCanonico.Length; j++)
      {
        if (CabecalhoCanonico[j][0] == letra)
        {
          indexLetra = j;
          break;
        }
      }

      if (indexLetra == -1)
        return false;

      var acao = TabelaCanonica[estadoAtual, indexLetra + 1];
      if (acao == " ")
      {
        Console.WriteLine($"\nEstado {estadoAtual} | indexLetra: {indexLetra}");
        return false;
      }

      if (acao == "X")
      {
        aceitacao = true;
        return true;
      }

      // verificando se é redução
      if (acao[0] == 'r')
      {
        AplicarReducaoTESTEDOIS(int.Parse(acao[1].ToString()));
        acaoNaPilha = 0;
      }
      else if (acao[0] == 's')
      {
        PilhaDeAnaliseSintatica.Push(letra.ToString());
        PilhaDeAnaliseSintatica.Push(acao[1].ToString());

        acaoNaPilha = 1;
        Ponteiro += 1;
      }
      else
      {
        PilhaDeAnaliseSintatica.Push(acao.ToString());
        acaoNaPilha = 2;
      }

      return true;
    }

    private int PegarEstadoMaisProximo()
    {
      for (int i = 0; i < PilhaDeAnaliseSintatica.Count; i++)
      {
        var resultado = int.TryParse(PilhaDeAnaliseSintatica.ElementAt(i), out int estado);

        if (!resultado)
          continue;

        return estado;
      }

      return 0;
    }

    private char PegarLetraMaisProxima()
    {
      for (int i = 0; i < PilhaDeAnaliseSintatica.Count; i++)
      {
        var resultado = int.TryParse(PilhaDeAnaliseSintatica.ElementAt(i), out int letra);

        if (resultado)
          continue;

        return PilhaDeAnaliseSintatica.ElementAt(i)[0];
      }

      return 'a';
    }

    private void AplicarReducaoTESTE(int numeroReducao)
    {
      var estado = Estados.FirstOrDefault(es => es.NumeroReducao == numeroReducao);

      if (estado == null)
        throw new Exception($"Erro, não existem nenhum estado com redução r{numeroReducao}");

      var expressao = estado?.Expressoes[0];
      expressao.LetrasGeradas.ForEach(lt =>
      {
        PilhaDeAnaliseSintatica.Pop();
        PilhaDeAnaliseSintatica.Pop();
      });

      PilhaDeAnaliseSintatica.Push(expressao.LetraGeradora.Nome);
    }

    private void AplicarReducaoTESTEDOIS(int numeroReducao)
    {
      if (numeroReducao > ExpressoesDeReducao.Count)
        throw new Exception($"Erro, não existem nenhuma redução r{numeroReducao}");

      var expressao = ExpressoesDeReducao[numeroReducao - 1];
      expressao.LetrasGeradas.ForEach(lt =>
      {
        PilhaDeAnaliseSintatica.Pop();
        PilhaDeAnaliseSintatica.Pop();
      });

      PilhaDeAnaliseSintatica.Push(expressao.LetraGeradora.Nome);
    }
  }
}
