using api_games_catalog.Entities;
using api_games_catalog.Exceptions;
using api_games_catalog.InputModel;
using api_games_catalog.Repositories;
using api_games_catalog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_games_catalog.Services
{
  public class GameService : IGameService
  {
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
      _gameRepository = gameRepository;
    }

    public async Task<List<GameViewModel>> Get(int page, int amount)
    {
      var games = await _gameRepository.Get(page, amount);

      return games.Select(game => new GameViewModel
      {
        Id = game.Id,
        Name = game.Name,
        Description = game.Description,
        Producer = game.Producer,
        Price = game.Price,
        Images = game.Images
      }).ToList();
    }

    public async Task<GameViewModel> Get(Guid id)
    {
      var game = await _gameRepository.Get(id);

      if (game == null)
        return null;

      return new GameViewModel
      {
        Id = game.Id,
        Name = game.Name,
        Description = game.Description,
        Producer = game.Producer,
        Price = game.Price,
        Images = game.Images
      };
    }

    public async Task<GameViewModel> Insert(GameInputModel game)
    {
      var entityGame = await _gameRepository.Get(game.Name, game.Producer);

      if (entityGame.Count > 0)
        throw new GameRegisteredException();

      var insertGame = new Game
      {
        Id = Guid.NewGuid(),
        Name = game.Name,
        Description = game.Description,
        Producer = game.Producer,
        Price = game.Price,
        Images = game.Images
      };

      await _gameRepository.Insert(insertGame);

      return new GameViewModel
      {
        Id = insertGame.Id,
        Name = game.Name,
        Producer = game.Producer,
        Description = game.Description,
        Price = game.Price,
        Images = game.Images
      };
    }

    public async Task Update(Guid id, GameInputModel game)
    {
      var entityGame = await _gameRepository.Get(id);

      if (entityGame == null)
        throw new GameNotRegisteredException();

      entityGame.Name = game.Name;
      entityGame.Producer = game.Producer;
      entityGame.Description = game.Description;
      entityGame.Price = game.Price;
      entityGame.Images = game.Images;

      await _gameRepository.Update(entityGame);
    }

    public async Task Update(Guid id, double price)
    {
      var entityGame = await _gameRepository.Get(id);

      if (entityGame == null)
        throw new GameNotRegisteredException();

      entityGame.Price = price;
      await _gameRepository.Update(entityGame);
    }

    public async Task Delete(Guid id)
    {
      var game = await _gameRepository.Get(id);

      if (game == null)
        throw new GameNotRegisteredException();

      await _gameRepository.Delete(id);
    }

    public void Dispose()
    {
      _gameRepository?.Dispose();
    }
  }
}