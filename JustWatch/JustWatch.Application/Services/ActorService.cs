using JustWatch.Application.DTO;
using JustWatch.Application.Interfaces;
using JustWatch.Domain.Commom;
using JustWatch.Domain.Interfaces;
using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<Result> AddActorAsync(NewActorDTO actorDTO)
        {
            if (await _actorRepository.ActorExists(actorDTO.Name))
            {
                return Result.Error("This actor already exists!");
            }

            var actor = new Actor
            {
                Name = actorDTO.Name,
                BirthDate = actorDTO.BirthDate,
                Description = actorDTO.Description, 
            };

            await _actorRepository.AddActor(actor);

            return Result.Success();
        }
    }
}
