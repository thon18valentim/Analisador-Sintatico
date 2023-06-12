using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorSintatico
{
  internal interface ItemGramatical
  {
    public string Nome { get; init; }
    public bool EPonto { get; set; }
  }
}
