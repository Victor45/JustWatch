using JustWatch.Application.DTO;
using JustWatch.Domain.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Interfaces
{
    public interface IActorService
    {
        Task<Result> AddActorAsync(NewActorDTO actorDTO);
    }
}
