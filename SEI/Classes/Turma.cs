using SEI.Classes;
using System.Collections.Generic;

namespace SEI.Classes
{
    public class Turma
    {
        private List<(int _Serie, char _Classe)> _IdentificadorTurma = new();
        private List<Aluno> _Alunodaclasse = new();

        public Turma(List<(int _Serie, char _Classe)> identificadorTurma, List<Aluno> alunodaclasse)
        {
            IdentificadorTurma = identificadorTurma;
            Alunodaclasse = alunodaclasse;
        }
        public Turma() { }

        public List<(int _Serie, char _Classe)> IdentificadorTurma { get => _IdentificadorTurma; set => _IdentificadorTurma = value; }
        public List<Aluno> Alunodaclasse { get => _Alunodaclasse; set => _Alunodaclasse = value; }

        public void Criarturma()
        {

        }
        public void Adicionarprof()
        {

        }
        public void Adicionaraluno()
        {

        }
        public void Excluirturma()
        {

        }
        public Turma Buscarturma()
        {
            Turma turma = new();
            return turma;
        }
    }
}
