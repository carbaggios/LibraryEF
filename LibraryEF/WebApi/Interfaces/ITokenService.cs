using Entity.Models;

namespace WebApi.Interfaces
{
    public interface ITokenService
    {
        public string GetToken(Librarian librarian);

        public string GetToken(Reader reader);
    }
}
