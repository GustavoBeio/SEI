using System;

namespace SEI.Classes

{
    public class Calendario
    {
        private int _IdEvento = 0;
        private DateOnly dataselecionada = DateOnly.MinValue;
        private string _NomeEvento = "";
        private string _TextoEvento = "";

        public int IdEvento { get => _IdEvento; set => _IdEvento = value; }
        public DateOnly Dataselecionada { get => dataselecionada; set => dataselecionada = value; }
        public string NomeEvento { get => _NomeEvento; set => _NomeEvento = value; }
        public string TextoEvento { get => _TextoEvento; set => _TextoEvento = value; }
    }
}
