
namespace AnalisadorSintatico
{
  internal class Galho
  {
    public List<Letra> Letras { get; private set; }
    public int NumeroReducao { get; private set; }

    public Galho(List<Letra> letras, int numeroReducao)
    {
      Letras = letras;
      NumeroReducao = numeroReducao;
    }

    public bool TemLetra(string nomeDaLetra)
      => Letras.Any(lt => Equals(lt.Nome, nomeDaLetra));

    public override string ToString()
    {
      return string.Join("", Letras);
    }
  }
}
