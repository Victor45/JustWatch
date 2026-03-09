using JustWatch.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchContentDTO>> SearchContentAsync(string q);
        Task<List<ActorSearchDTO>> SearchActorAsync(string q);
    }
}
