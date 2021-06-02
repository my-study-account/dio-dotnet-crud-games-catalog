using System;

namespace api_games_catalog.Exceptions
{
  public class GameRegisteredException : Exception
  {
    public GameRegisteredException()
        : base("Este já jogo está cadastrado")
    { }
  }
}