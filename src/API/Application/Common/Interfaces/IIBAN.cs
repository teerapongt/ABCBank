using System.Threading.Tasks;

namespace API.Application.Common.Interfaces
{
    public interface IIBAN
    {
        Task<string> Generate { get; }
    }
}