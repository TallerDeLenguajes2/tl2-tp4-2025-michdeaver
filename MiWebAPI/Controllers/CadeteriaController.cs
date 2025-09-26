using Microsoft.AspNetCore.Mvc;
namespace MiWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadeteriaController: ControllerBase
{
    private readonly IAccessoADatos _dataAccess;
    public PedidosController(IAccessoADatos dataAccess)
    {
        _dataAccess = dataAccess;
    }
    [HttpGet("GetPedidos")]
    public IActionResult <List<Pedido>> GetPedidos()
    {
        string archivo = "Data/pedidos.csv";
        var pedidos = _dataAccess.CargarPedidos(archivo);
        return Ok(pedidos);
    } //=> Retorna una lista de Pedidos
//[Get] GetCadetes() => Retorna una lista de Cadetes
//[Get] GetInforme() => Retorna un objeto Informe
//[Post] AgregarPedido(Pedido pedido)
//[Put] AsignarPedido(int idPedido, int idCadete)
//[Put] CambiarEstadoPedido(int idPedido,int NuevoEstado)
//[Put] CambiarCadetePedido(int idPedido,int idNuevoCadete)

}
