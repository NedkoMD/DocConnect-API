using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DocConnectContext _docConnectContext;

        public TokenRepository(DocConnectContext docConnectContext)
        {
            _docConnectContext = docConnectContext;
        }

        public async Task AddAsync(Token token)
        {
            _docConnectContext.Tokens.Add(token);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Token token)
        {
            _docConnectContext.Tokens.Remove(token);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task<Token> GetByValueAsync(string tokenValue)
        {
            var token = await _docConnectContext.Tokens
                .Where(t => !t.IsDeleted)
                .FirstOrDefaultAsync(t => t.Value == tokenValue);

            return token;
        }
    }
}
