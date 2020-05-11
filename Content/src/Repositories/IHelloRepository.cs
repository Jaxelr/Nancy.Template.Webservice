using Nancy.Template.WebService.Entities;

namespace Nancy.Template.WebService.Repositories
{
    public interface IHelloRepository
    {
        Hello SayHello(string name);
    }
}
