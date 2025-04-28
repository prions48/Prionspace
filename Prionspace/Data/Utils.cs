namespace Prionspace.Data
{
    public static class Utils
    {
        public static string ColorName(this MudBlazor.Color color)
        {
            switch (color)
            {
                case MudBlazor.Color.Dark: return "Black";
                case MudBlazor.Color.Error: return "Red";
                case MudBlazor.Color.Warning: return "Yellow";
                case MudBlazor.Color.Success: return "Green";
                case MudBlazor.Color.Info: return "Blue";
                case MudBlazor.Color.Primary: return "Purple";
                case MudBlazor.Color.Secondary: return "Pink";
                case MudBlazor.Color.Default: return "Gray";
                default: return "Unknown";
            }
        }
    }
}