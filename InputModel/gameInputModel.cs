using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace api_games_catalog.InputModel
{
  public class GameInputModel
  {
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracteres")]
    public string Name { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
    public string Producer { get; set; }
    [Required]
    [Range(1, 10000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no máximo 1000 reais")]
    public double Price { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
    public string Description { get; set; }
    [Required]
    public string Images { get; set; }
  }
}