
using AnalisadorSintatico;

#region
/*
//var slinha = new Letra(1, "S'", false, true);
//var s = new Letra(2, "S", false, false);
//var a = new Letra(3, "A", false, false);
//var azinho = new Letra(5, "a", true, false);
//var bzinho = new Letra(6, "b", true, false);

//slinha.CriarGalhos(new() { new Galho(new() { s }, 0) });
//s.CriarGalhos(new() { new Galho(new() { a, a }, 1) });
//a.CriarGalhos(new() { new Galho(new() { azinho, a }, 2), new Galho(new() { bzinho }, 3) });

var Slinha = new Letra(0, "S'", false, true);
var S = new Letra(1, "S", false, false);
var E = new Letra(2, "E", false, false);
var x = new Letra(3, "x", true, false);
var pi = new Letra(4, "i", true, false);
var pf = new Letra(5, "f", true, false);
var m = new Letra(6, "m", true, false);

Slinha.CriarGalhos(new() { new Galho(new() { S }, 0) });
S.CriarGalhos(new() { new Galho(new() { pi, E, pf }, 1), new Galho(new() { x }, 2) });
E.CriarGalhos(new() { new Galho(new() { S }, 3), new Galho(new() { E, m, S }, 4) });

var gramatica = new Gramatica(new() { Slinha, S, E, x, pi, pf, m });

var item = new Item(0, gramatica.PegarLetraGeradoraDoEstadoDeAceitacao().GerarExpressoes());
Console.WriteLine(item);

var listaDeItems = item.PassarAdiante(1, new());
var indexContador = listaDeItems[^1].Index + 1;

int contador = 0;
do
{

  if (!listaDeItems[contador].Percorrido)
  {
    var novosItems = listaDeItems[contador].PassarAdiante(indexContador, listaDeItems);
    foreach (var items in novosItems)
      listaDeItems.Add(items);
    listaDeItems[contador].Percorrido = true;
  }

  contador++;

  if (contador >= listaDeItems.Count)
    contador = 0;

  indexContador = listaDeItems[^1].Index + 1;

} while (listaDeItems.Any(item => !item.Percorrido));

listaDeItems.Add(item);

var acoes = new List<string>();
var goTo = new List<string>();

gramatica.PegarLetrasFinais().ForEach(lt => acoes.Add(lt.Nome));
gramatica.PegarLetrasNaoFinais().ForEach(lt => goTo.Add(lt.Nome));

Console.WriteLine("\n\n");
Console.WriteLine("--- Items ---");
Console.WriteLine(string.Join("\n\n", listaDeItems));

Console.WriteLine("\n\n");
Console.WriteLine("--- Gramática ---");
Console.WriteLine(gramatica);

var tabelaCanonica = new TabelaCanonica(acoes, goTo);
tabelaCanonica.CriarTabela(listaDeItems, gramatica);

var palavra = "xmx";
Compilador compilador = new(tabelaCanonica.PegarCabecalho(), tabelaCanonica.PegarTabela(), listaDeItems, gramatica);
compilador.SetarPalavra(palavra);
var resultado = compilador.Executar();

if (resultado)
  Console.WriteLine($"Palavra {palavra} ACEITA!");
else
  Console.WriteLine($"Palavra {palavra} RECUSADA!");
*/
#endregion

var Slinha = new Letra(0, "S'", false, true);
var S = new Letra(1, "S", false, false);
var E = new Letra(2, "E", false, false);
var x = new Letra(3, "x", true, false);
var pi = new Letra(4, "i", true, false);
var pf = new Letra(5, "f", true, false);
var m = new Letra(6, "m", true, false);

Gramatica gramaticaConhecida = new Gramatica(new() { Slinha, S, E, x, pi, pf, m });
