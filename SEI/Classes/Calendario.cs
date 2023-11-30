using System;
using System.Diagnostics.Eventing.Reader;

namespace SEI.Classes

{
    public class Calendario
    {
        public int IdEvento { get; set; }
        public DateOnly Dataselecionada { get; set; }
        public string NomeEvento { get; set; } = "";
        public string TextoEvento { get; set; } = "";

        public void AdicionarEvento(Calendario calendario)
        {

        }
    }
}
