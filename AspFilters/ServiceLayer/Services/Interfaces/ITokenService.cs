using System.Collections.Generic;

namespace ServiceLayer.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(string Id, string email, string name, List<string> roles);
    }
}