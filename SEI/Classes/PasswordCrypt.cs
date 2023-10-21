using BCrypt.Net;
using static BCrypt.Net.BCrypt;

namespace SEI.Classes
{
    public static class PasswordCrypt
    {

        //Define um workfactor para o algoritimo de hash
        private const int WorkFactor = 12;
        //Cria o salt criptograficamente seguro usando o workfactor
        private static string Salt => GenerateSalt(WorkFactor);

        //Estrutura que representa a senha criptografada armazenando a senha criptografada e o salt
        public struct EncryptedPassword
        {
            public string PasswordHash { get; set; }
            public string PasswordSalt { get; set; }
        }
        // Encripta a senha
        public static EncryptedPassword EncryptPass(string pass)
        {
            string hashedPassword = HashPassword(pass, Salt);

            // Retorna a senha encriptada como uma instância da estrutura EncryptedPassword.
            return new EncryptedPassword
            {
                PasswordHash = hashedPassword,
                PasswordSalt = Salt
            };
        }
    }
}

