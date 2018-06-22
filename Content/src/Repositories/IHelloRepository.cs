using Api.Models.Entities;

namespace Api.Repositories
{
    public interface IHelloRepository
    {
        Hello SayHello(string name);
    }
}
