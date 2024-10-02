using Repository;

namespace LibraryEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login();
        }

        private static void Login()
        {
            string? login = string.Empty;
            string? passw = string.Empty;
            bool result;
            string message = string.Empty;

            do
            {
                Console.WriteLine("Please input login:");
                login = Console.ReadLine();

                Console.WriteLine("Please input password:");
                passw = Console.ReadLine();

                result = TryLogin(login, passw, out message);

                Console.WriteLine(message);
            }
            while (!result);
        }

        static bool TryLogin(string? login, string? password, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(login))
            {
                message = $"The login is empty. Please input login again";
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                message = $"The password is empty. Please input password again";
                return false;
            }

            using var ctx = new LibraryContext();
            var librarian = ctx.Librarians.Where(e => e.Login == login).SingleOrDefault();

            if (librarian == null)
            {
                message = $"The Librarian does not exists with login \"{login}\"";
                return false;
            }
            else if (!password.Equals(librarian.Password))
            {
                message = "Incorrect password. Please try again";
                return false;
            }

            message = "Logged on";

            return true;
        }
    }
}
