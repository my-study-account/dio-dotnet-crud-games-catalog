using api_games_catalog.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace api_games_catalog.Repositories
{
  public class GameSqlRepository : IGameRepository
  {
    private readonly SqlConnection sqlConnection;

    public GameSqlRepository(IConfiguration configuration)
    {
      sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
    }

    public async Task<List<Game>> Get(int page, int amount)
    {
      var games = new List<Game>();

      var command = $"select * from Games order by id offset {((page - 1) * amount)} rows fetch next {amount} rows only";

      await sqlConnection.OpenAsync();
      SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
      SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

      while (sqlDataReader.Read())
      {
        games.Add(new Game
        {
          Id = (Guid)sqlDataReader["Id"],
          Name = (string)sqlDataReader["Name"],
          Producer = (string)sqlDataReader["Producer"],
          Description = (string)sqlDataReader["Description"],
          Price = (double)sqlDataReader["Price"],
          Images = (string)sqlDataReader["Images"],
        });
      }

      await sqlConnection.CloseAsync();

      return games;
    }

    public async Task<Game> Get(Guid id)
    {
      Game game = null;

      var command = $"select * from Games where Id = '{id}'";

      await sqlConnection.OpenAsync();
      SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
      SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

      while (sqlDataReader.Read())
      {
        game = new Game
        {
          Id = (Guid)sqlDataReader["Id"],
          Name = (string)sqlDataReader["Name"],
          Producer = (string)sqlDataReader["Producer"],
          Description = (string)sqlDataReader["Description"],
          Price = (double)sqlDataReader["Price"],
          Images = (string)sqlDataReader["Images"],
        };
      }
      await sqlConnection.CloseAsync();
      return game;
    }

    public async Task<List<Game>> Get(string Name, string Producer)
    {
      var games = new List<Game>();

      var command = $"select * from Games where Name = '{Name}' and Producer = '{Producer}'";

      await sqlConnection.OpenAsync();
      SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
      SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

      while (sqlDataReader.Read())
      {
        games.Add(new Game
        {
          Id = (Guid)sqlDataReader["Id"],
          Name = (string)sqlDataReader["Name"],
          Producer = (string)sqlDataReader["Producer"],
          Description = (string)sqlDataReader["Description"],
          Price = (double)sqlDataReader["Price"],
          Images = (string)sqlDataReader["Images"],
        });
      }

      await sqlConnection.CloseAsync();

      return games;
    }

    public async Task Insert(Game game)
    {
      var command = $"insert Games (Id, Name, Producer, Description, Price, Images) values ('{game.Id}', '{game.Name}', '{game.Producer}','{game.Description}', {game.Price.ToString().Replace(",", ".")}, '{game.Images}')";

      await sqlConnection.OpenAsync();
      SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
      sqlCommand.ExecuteNonQuery();
      await sqlConnection.CloseAsync();
    }

    public async Task Update(Game game)
    {
      var command = $"update Games set Name = '{game.Name}', Producer = '{game.Producer}', Description = '{game.Description}',  Price = {game.Price.ToString().Replace(",", ".")}, Images = {game.Images} where Id = '{game.Id}'";

      await sqlConnection.OpenAsync();
      SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
      sqlCommand.ExecuteNonQuery();
      await sqlConnection.CloseAsync();
    }

    public async Task Delete(Guid id)
    {
      var command = $"delete from Games where Id = '{id}'";

      await sqlConnection.OpenAsync();
      SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
      sqlCommand.ExecuteNonQuery();
      await sqlConnection.CloseAsync();
    }

    public void Dispose()
    {
      sqlConnection?.Close();
      sqlConnection?.Dispose();
    }
  }
}