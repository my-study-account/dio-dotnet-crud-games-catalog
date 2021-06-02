using System;
using System.Collections.Generic;
namespace api_games_catalog.ViewModel
{
  public class GameViewModel
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Producer { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Images { get; set; }
  }
}