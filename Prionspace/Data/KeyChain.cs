namespace Prionspace.Data
{
    /// <summary>
    /// In-app settings not needed to be abstracted to user secrets
    /// </summary>
    public static class KeyChain
    {
        public static string AppName
        {
            get
            {
                return "Prionspace";
            }
        }
        public static string AppCode
        {
            get
            {
                return "Prionspace";
            }
        }
        public static string Container
        {
            get
            {
                return "prionspacestorage";
            }
        }
        public static string OwnerEmail
        {
            get
            {
                return "prions48@gmail.com";
            }
        }
    }
    public enum Environ
    {
        MainApp
    }

}