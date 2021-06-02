using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api_games_catalog.Entities;
namespace api_games_catalog.Repositories
{
  public interface IGameRepository : IDisposable
  {
    Task<List<Game>> Get(int page, int amount);
    Task<Game> Get(Guid id);
    Task<List<Game>> Get(string name, string producer);
    Task Insert(Game game);
    Task Update(Game jogo);
    Task Delete(Guid id);
  }
}