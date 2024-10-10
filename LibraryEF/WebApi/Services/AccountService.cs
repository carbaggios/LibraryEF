using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Repositories;
using Entity.Models;

namespace WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILibrarianRepository librarianRepository;
        private readonly IHashService hashService;
        private readonly ITokenService tokenService;

        public AccountService(ILibrarianRepository accountRepository, IHashService hashService, ITokenService tokenService)
        {
            this.librarianRepository = accountRepository;
            this.hashService = hashService;
            this.tokenService = tokenService;
        }

        public async Task<string> Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if ((await librarianRepository.GetLibrarian(registerDto.Login, cancellationToken)) != null)
            {
                throw new InvalidOperationException("Account already exist.");
            }

            var hashResult = hashService.GetHash(registerDto.Password);

            var model = await librarianRepository.AddLibrarian(new Entity.Models.Librarian
            {
                Login = registerDto.Login,
                PasswordHash = hashResult.hash,
                PasswordSalt = hashResult.salt,
                Email = registerDto.Email
            }, cancellationToken);

            return tokenService.GetToken(model);
        }

        public async Task<string> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var librarian = await librarianRepository.GetLibrarian(loginDto.Login, cancellationToken);
            if (librarian == null)
            {
                throw new InvalidOperationException("Account doesn't exist.");
            }

            var hashResult = hashService.GetHash(loginDto.Password, librarian.PasswordSalt);

            if (librarian.PasswordHash!.SequenceEqual(hashResult.hash))
            {
                throw new InvalidOperationException("Incorrect password.");
            }

            return tokenService.GetToken(librarian);
        }

        public Task<List<Librarian>> GetLibrarians(CancellationToken cancellationToken)
        {
            return librarianRepository.GetLibrarians(cancellationToken);
        }
    }
}
