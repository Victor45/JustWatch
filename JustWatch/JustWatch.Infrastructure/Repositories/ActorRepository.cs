using JustWatch.Domain.Interfaces;
using JustWatch.Domain.Models;
using JustWatch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Infrastructure.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _context;
        public ActorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ActorExists(string name)
        {
            return await _context.Actors.AnyAsync(a => a.Name == name);
        }

        public async Task AddActor(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
        }
    }
}
