namespace Utils
{
    public static class Utils
    {
        public static string[] OpenInput(string name)
        {
            return File.ReadAllLines($"./in/{name}");
        }
    }
}