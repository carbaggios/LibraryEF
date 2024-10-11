using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Repositories;
using Entity.Models;

namespace WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILibrarianRepository librarianRepository;
        private readonly IReaderRepository readerRepository;
        private readonly IHashService hashService;
        private readonly ITokenService tokenService;

        public AccountService(IReaderRepository readerRepository, ILibrarianRepository librarianRepository, IHashService hashService, ITokenService tokenService)
        {
            this.readerRepository = readerRepository;
            this.librarianRepository = librarianRepository;
            this.hashService = hashService;
            this.tokenService = tokenService;
        }

        public async Task<string> RegisterReader(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if ((await readerRepository.GetReader(registerDto.Login, cancellationToken)) != null)
            {
                throw new InvalidOperationException("Account already exist.");
            }

            var hashResult = hashService.GetHash(registerDto.Password);

            var model = await readerRepository.AddReader(new Reader
            {
                Login = registerDto.Login,
                PasswordHash = hashResult.hash,
                PasswordSalt = hashResult.salt,
                Email = registerDto.Email
            }, cancellationToken);

            return tokenService.GetToken(model);
        }

        public async Task<string> RegisterLibrarian(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if ((await librarianRepository.GetLibrarian(registerDto.Login, cancellationToken)) != null)
            {
                throw new InvalidOperationException("Account already exist.");
            }

            var hashResult = hashService.GetHash(registerDto.Password);

            var model = await librarianRepository.AddLibrarian(new Librarian
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
            var reader = await readerRepository.GetReader(loginDto.Login, cancellationToken);
            var librarian = await librarianRepository.GetLibrarian(loginDto.Login, cancellationToken);
            if (reader == null)
            {
                if (librarian == null)
                {
                    throw new InvalidOperationException("Account doesn't exist.");
                }
            }

            var hashResult = hashService.GetHash(loginDto.Password, reader != null ? reader.PasswordSalt : librarian!.PasswordSalt);

            if (!(reader != null 
                    ? reader.PasswordHash!.SequenceEqual(hashResult.hash) 
                    : librarian!.PasswordHash!.SequenceEqual(hashResult.hash)))
            {
                throw new InvalidOperationException("Incorrect password.");
            }

            if (reader != null)
                return tokenService.GetToken(reader);
            else
                return tokenService.GetToken(librarian!);

        }

        public Task<List<Librarian>> GetLibrarians(CancellationToken cancellationToken)
        {
            return librarianRepository.GetLibrarians(cancellationToken);
        }

        public Task<List<Reader>> GetReaders(CancellationToken cancellationToken)
        {
            return readerRepository.GetReaders(cancellationToken);
        }
    }
}
