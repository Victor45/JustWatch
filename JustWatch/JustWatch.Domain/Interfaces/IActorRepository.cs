using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Interfaces
{
    public interface IActorRepository
    {
        Task AddActor(Actor actor);
        Task<bool> ActorExists(string name);
    }
}
