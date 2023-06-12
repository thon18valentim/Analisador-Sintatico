
using AnalisadorSintatico;

var slinha = new Letra(1, "S'", false, true);
var s = new Letra(2, "S", false, false);
var a = new Letra(3, "A", false, false);
var azinho = new Letra(5, "a", true, false);
var bzinho = new Letra(6, "b", true, false);

slinha.CriarGalhos(new() { new Galho(new() { s }) });
s.CriarGalhos(new() { new Galho(new() { a, a }) });
a.CriarGalhos(new() { new Galho(new() { azinho, a }), new Galho(new() { bzinho }) });

var gramatica = new Gramatica(new() { slinha, s, a, azinho, bzinho });

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

var tabelaCanonica = new TabelaCanonica(acoes, goTo);
tabelaCanonica.CriarTabela(listaDeItems);
