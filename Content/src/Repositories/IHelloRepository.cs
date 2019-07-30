using Nancy.Template.WebService.Models.Entities;

namespace Nancy.Template.WebService.Repositories
{
    public interface IHelloRepository
    {
        Hello SayHello(string name);
    }
}
