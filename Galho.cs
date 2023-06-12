using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorSintatico
{
  internal class Galho
  {
    public List<Letra> Letras { get; private set; }

    public Galho(List<Letra> letras)
    {
      Letras = letras;
    }
  }
}
