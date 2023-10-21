using System;

namespace SEI.Classes
{
    public class Pessoa
    {
        protected string Nome { get; set; } = "Nome";
        protected string Sobrenome { get; set; } = "sobrenome";
        protected string Login { get; set; } = "login";
        protected DateOnly DataNasc { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        protected string Endereco { get; set; } = "endereco";
        protected string RegistroGeral { get; set; } = "RG";
        protected string LocalNasc { get; set; } = "Local";
        protected string Email { get; set; } = "email";
        protected string Password { get; set; } = "password";
        protected string Salt { get; set; } = "salt";
        protected string Gender { get; set; } = "Gender";
    }
}
