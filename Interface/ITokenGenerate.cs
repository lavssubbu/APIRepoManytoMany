using APIRepoPattern.Models;

namespace APIRepoPattern.Interface
{
    public interface ITokenGenerate
    {
        public string GenerateToken(User user);
      
    }
}
