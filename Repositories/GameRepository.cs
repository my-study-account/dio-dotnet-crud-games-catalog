using api_games_catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace api_games_catalog.Repositories
{
  public class GameRepository : IGameRepository
  {
    private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Game{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Name = "Fifa 21", Producer = "EA", Price = 200, Description="Description", Images="" } },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Game{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Name = "Fifa 20", Producer = "EA", Price = 190, Description="Description",Images=""} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Game{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Name = "Fifa 19", Producer = "EA", Price = 180, Description="Description",Images=""} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Game{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Name = "Fifa 18", Producer = "EA", Price = 170, Description="Description",Images=""} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Game{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Name = "Street Fighter V", Producer = "Capcom",Price = 80, Description="Description", Images=""} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Game{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Name = "Grand Theft Auto V", Producer = "Rockstar", Price = 190,Description="Description",Images="" } }
        };

    public Task<List<Game>> Get(int page, int amount)
    {
      return Task.FromResult(games.Values.Skip((page - 1) * amount).Take(amount).ToList());
    }

    public Task<Game> Get(Guid id)
    {

      if (!games.ContainsKey(id))
        return Task.FromResult<Game>(null);
      return Task.FromResult(games[id]);
    }

    public Task<List<Game>> Get(string Name, string Producer)
    {
      return Task.FromResult(games.Values.Where(jogo => jogo.Name.Equals(Name) && jogo.Producer.Equals(Producer)).ToList());
    }

    public Task<List<Game>> GetNotLambda(string Name, string Producer)
    {
      var response = new List<Game>();

      foreach (var game in games.Values)
      {
        if (game.Name.Equals(Name) && game.Producer.Equals(Producer))
          response.Add(game);
      }
      return Task.FromResult(response);
    }

    public Task Insert(Game game)
    {
      games.Add(game.Id, game);
      return Task.CompletedTask;
    }

    public Task Update(Game game)
    {
      games[game.Id] = game;
      return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
      games.Remove(id);
      return Task.CompletedTask;
    }

    public void Dispose()
    {
      //Fechar conexão com o banco
    }
  }
}


