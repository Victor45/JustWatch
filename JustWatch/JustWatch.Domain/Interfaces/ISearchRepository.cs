using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<SearchContent>> SearchContent(string q); 
        Task<List<Actor>> SearchActor(string q);
    }
}
