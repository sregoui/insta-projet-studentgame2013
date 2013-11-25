using System;

namespace INSTA.TooLate.UI.Windows
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Point d’entrée principal pour l’application.
        /// </summary>
        static void Main(string[] args)
        {
            using (StartGame game = new StartGame())
            {
                game.Run();
            }
        }
    }
#endif
}

