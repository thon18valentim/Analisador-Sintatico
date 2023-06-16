
namespace AnalisadorSintatico
{
  internal class Gramatica
  {
    public List<Letra> Letras { get; init; }

    public Gramatica(List<Letra> letras)
    {
      Letras = letras;
    }

    public Letra PegarLetraGeradoraDoEstadoDeAceitacao()
    {
      var letra = Letras.FirstOrDefault(lt => lt.GeraEstadoAceitacao);
      return letra ?? throw new Exception("Erro, não há nenhuma letra que possa gerar um estado de aceitação.");
    }

    public List<Letra> PegarLetrasFinais()
      =>Letras.FindAll(lt => lt.EFinal && !lt.GeraEstadoAceitacao);

    public List<Letra> PegarLetrasNaoFinais()
      => Letras.FindAll(lt => !lt.EFinal && !lt.GeraEstadoAceitacao);

    public override string ToString()
    {
      var text = string.Empty;
      foreach (var letra in Letras)
      {
        foreach (var galho in letra.Galhos)
        {
          if (letra.GeraEstadoAceitacao)
            text += $"{letra} -> {galho}\n";
          else
            text += $"{letra} -> {galho} ({galho.NumeroReducao})\n";
        }
      }

      return text;
    }
  }
}
