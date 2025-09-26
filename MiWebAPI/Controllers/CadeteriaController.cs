using Microsoft.AspNetCore.Mvc;
namespace MiWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadeteriaController: ControllerBase
{
[HttpGet("GetPedidos")]
 public IactionResult
GetPedidos() //=> Retorna una lista de Pedidos
//[Get] GetCadetes() => Retorna una lista de Cadetes
//[Get] GetInforme() => Retorna un objeto Informe
//[Post] AgregarPedido(Pedido pedido)
//[Put] AsignarPedido(int idPedido, int idCadete)
//[Put] CambiarEstadoPedido(int idPedido,int NuevoEstado)
//[Put] CambiarCadetePedido(int idPedido,int idNuevoCadete)

}
