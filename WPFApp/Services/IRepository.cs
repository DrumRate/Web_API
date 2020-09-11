using System.Collections.Generic;
using System.Threading.Tasks;
using WPFApp.Models;

namespace WPFApp.Services
{
    public interface IRepository
    {
        Task<IReadOnlyList<Event>> GetEvents(int j);
    }
}