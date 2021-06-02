
using api_games_catalog.InputModel;
using api_games_catalog.ViewModel;
using api_games_catalog.Services;
using api_games_catalog.Exceptions;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace api_games_catalog.Controllers.V1
{
  [Route("api/v1/[controller]")]
  [ApiController]

  public class GamesController : ControllerBase
  {
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
      this._gameService = gameService;
    }

    /// <summary>
    /// Buscar todos os jogos de forma paginada
    /// </summary>
    /// <remarks>
    /// Não é possível retornar os jogos sem paginação
    /// </remarks>
    /// <param name="page">Indica qual página está sendo consultada. Mínimo 1</param>
    /// <param name="amount">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
    /// <response code="200">Retorna a lista de jogos</response>
    /// <response code="204">Caso não haja jogos</response>   
    [HttpGet]
    public async Task<ActionResult<List<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int amount = 5)
    {
      var response = await _gameService.Get(page, amount);
      if (response.Count() == 0)
        return NoContent();
      return Ok(response);
    }

    /// <summary>
    /// Buscar um jogo pelo seu Id
    /// </summary>
    /// <param name="id">Id do jogo buscado</param>
    /// <response code="200">Retorna o jogo filtrado</response>
    /// <response code="204">Caso não haja jogo com este id</response>  

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<GameViewModel>>> Get([FromRoute] Guid id)
    {

      var response = await _gameService.Get(id);
      if (response == null)
        return NoContent();
      return Ok(response);
    }

    /// <summary>
    /// Inserir um jogo no catálogo
    /// </summary>
    /// <param name="gameInputModel">Dados do jogo a ser inserido</param>
    /// <response code="200">Cao o jogo seja inserido com sucesso</response>
    /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>   
    [HttpPost]
    public async Task<ActionResult<List<GameViewModel>>> Insert([FromBody] GameInputModel gameInputModel)
    {
      try
      {
        var response = await _gameService.Insert(gameInputModel);
        return Ok(response);
      }
      catch (GameRegisteredException)
      {
        return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
      }
    }

    /// <summary>
    /// Atualizar um jogo no catálogo
    /// </summary>
    /// /// <param name="id">Id do jogo a ser atualizado</param>
    /// <param name="gameInputModel">Novos dados para atualizar o jogo indicado</param>
    /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
    /// <response code="404">Caso não exista um jogo com este Id</response>  
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<List<GameViewModel>>> Update([FromRoute] Guid id, [FromBody] GameInputModel gameInputModel)
    {
      try
      {
        await _gameService.Update(id, gameInputModel);
        return Ok();
      }
      catch (GameNotRegisteredException)
      {
        return NotFound("Não existe este jogo");
      }
    }

    /// <summary>
    /// Atualizar o preço de um jogo
    /// </summary>
    /// /// <param name="id">Id do jogo a ser atualizado</param>
    /// <param name="price">Novo preço do jogo</param>
    /// <response code="200">Cao o preço seja atualizado com sucesso</response>
    /// <response code="404">Caso não exista um jogo com este Id</response>   
    [HttpPatch("{id:guid}/price/{price:double}")]
    public async Task<ActionResult<List<GameViewModel>>> Update([FromRoute] Guid id, [FromRoute] double price)
    {
      try
      {
        await _gameService.Update(id, price);

        return Ok();
      }
      catch (GameNotRegisteredException)
      {
        return NotFound("Não existe este jogo");
      }
    }

    /// <summary>
    /// Excluir um jogo
    /// </summary>
    /// /// <param name="id">Id do jogo a ser excluído</param>
    /// <response code="200">Cao o preço seja atualizado com sucesso</response>
    /// <response code="404">Caso não exista um jogo com este Id</response>  
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<List<GameViewModel>>> Delete([FromRoute] Guid id)
    {
      try
      {
        await _gameService.Delete(id);
        return Ok();
      }
      catch (GameNotRegisteredException)
      {
        return NotFound("Não existe este jogo");
      }
    }
  }
}



