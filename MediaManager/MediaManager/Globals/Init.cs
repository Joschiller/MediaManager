using System.Collections.Generic;

namespace MediaManager.Globals
{
    public static class Init
    {
        public static void Initialize()
        {
            InitLanguages();
        }
        private static void InitLanguages()
        {
            LanguageProvider.LanguageProvider.ConfigureLanguages(new Dictionary<string, byte[]>
            {
                { "English", Properties.Resources.English },
                { "Deutsch", Properties.Resources.Deutsch }
            }, "English");
        }
    }
}
