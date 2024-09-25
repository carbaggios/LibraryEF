using Repository;

namespace LibraryEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var ctx = new LibraryContext();
            var allLessons = ctx.Librarians.Where(l => l.Email != null).ToArray();
            Console.WriteLine("Hello, World!");
        }
    }
}
