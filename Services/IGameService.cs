using api_games_catalog.InputModel;
using api_games_catalog.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_games_catalog.Services
{
  public interface IGameService : IDisposable
  {
    Task<List<GameViewModel>> Get(int page, int amount);
    Task<GameViewModel> Get(Guid id);
    Task<GameViewModel> Insert(GameInputModel game);
    Task Update(Guid id, GameInputModel jogo);
    Task Update(Guid id, double price);
    Task Delete(Guid id);
  }
}