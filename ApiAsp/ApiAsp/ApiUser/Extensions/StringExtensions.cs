using BCrypt.Net;

namespace ApiUser.Extensions
{
    /// <summary>
    /// Classe d'extension. 
    /// La classe doit être statique (ne peut contenir que des méthodes statiques)
    /// https://learn.microsoft.com/fr-fr/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Méthode d'extenion pour la classe "string" 
        /// (1er paramètre "this string" détermine la classe qu'on étend)
        /// Chiffre la chaine "passwordToHash" avec l'algorithme "BCRYPT"
        /// </summary>
        /// <param name="passwordToHash">La chaine à chiffrer</param>
        /// <returns>La chaine chiffrée avec l'algorithme BCRYPT</returns>
        public static string ToPassword(this string passwordToHash)
        {
            return BCrypt.Net.BCrypt.HashPassword(passwordToHash);
        }

        /// <summary>
        /// Méthode d'extenion pour la classe "string" 
        /// (1er paramètre "this string" détermine la classe qu'on étend)
        /// Vérifie si la chaine "passwordToTest" correspond au mot de passe chiffré "passwordHash"
        /// </summary>
        /// <param name="passwordHash">Le mot de passe chiffré</param>
        /// <param name="passwordToTest">Le mot de passe à tester</param>
        /// <returns>TRUE si le mot de passe correspond. False sinon.</returns>
        public static bool CheckPassword(this string passwordHash, string passwordToTest)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToTest, passwordHash);
        }

        /*
        public static void example()
        {
            string passwordToTest = "toto";
            string passwordHash = "$2a$11$5qxAIqcTc0ROwzheFKZhRel95uTL4h3AFP8Xdsaz4P1b8wFqOvYUu";

            if(passwordHash.CheckPassword(passwordToTest))
            {
                // le mot de passe est correct
            } 
            else
            {
                // Le mot de passe est incorrect
            }
        
        }*/
        
    }
}
