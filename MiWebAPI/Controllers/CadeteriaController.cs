using Microsoft.AspNetCore.Mvc;
namespace MiWebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private readonly Cadeteria _cadeteria;
    private readonly AccesoADatosCadeteria _ADCadeteria;
    private readonly AccesoADatosCadetes _ADCadetes;
    private readonly AccesoADatosPedidos _ADPedidos;

    public CadeteriaController()
    {
        // Initialize data-access classes
        _ADCadeteria = new AccesoADatosCadeteria();
        _ADCadetes = new AccesoADatosCadetes();
        _ADPedidos = new AccesoADatosPedidos();

        // Initialize Cadeteria domain
        _cadeteria = _ADCadeteria.Obtener();
        _cadeteria.AgregarListaCadetes(_ADCadetes.Obtener());
        _cadeteria.AgregarListaPedidos(_ADPedidos.Obtener());
    }

    // Get all cadetes
    [HttpGet("GetCadetes")]
    public ActionResult <List<Cadete>>GetCadetes()
    {
        return Ok(_cadeteria.ObtenerCadetes());
    }

    // Get all pedidos
    [HttpGet("GetPedidos")]
    public ActionResult <List<Pedido>> GetPedidos()
    {
        var pedidos = _cadeteria.ObtenerPedidos();
        if (pedidos == null || pedidos.Count == 0)
            return NotFound();

        return Ok(pedidos);
    }

    // Get cadeteria info 
    [HttpGet("GetCadeteriaInfo")]
    public ActionResult GetCadeteriaInfo()
    {
        return Ok(new { _cadeteria.Nombre, _cadeteria.Telefono });
    }

    // Get jornada report (structured)
    [HttpGet("GetInforme")]
    public IActionResult GetInforme()
    {
        return Ok(_cadeteria.Informe());
    }

    // Add a new pedido and save JSON
    [HttpPost("DarAltaPedido")]
    public ActionResult DarAltaPedido(Pedido nuevoPedido)
    {
        _cadeteria.ObtenerPedidos().Add(nuevoPedido);
        _adPedidos.Guardar(_cadeteria.ObtenerPedidos());

        return Created("", new { mensaje = "Pedido dado de alta exitosamente", pedido = nuevoPedido });
    }

    // Assign a cadete to a pedido and save JSON
    [HttpPost("AsignarPedido")]
    public ActionResult AsignarPedido([FromQuery] int pedidoId, [FromQuery] int cadeteId)
    {
        var pedido = _cadeteria.GetPedido(pedidoId);
        var cadete = _cadeteria.GetCadete(cadeteId);

        if (pedido == null || cadete == null)
            return NotFound(new { mensaje = "Pedido o Cadete no encontrado" });

        _cadeteria.AsignarCadeteAPedidos(pedido, cadete);
        _adPedidos.Guardar(_cadeteria.ObtenerPedidos());

        return Ok(new { mensaje = "Pedido asignado exitosamente", pedidoId, cadeteId });
    }

    // Optional: change estado of pedido
    [HttpPost("CambiarEstadoPedido")]
    public ActionResult CambiarEstadoPedido([FromQuery] int pedidoId, [FromQuery] string nuevoEstado)
    {
        var pedido = _cadeteria.GetPedido(pedidoId);
        if (pedido == null) return NotFound(new { mensaje = "Pedido no encontrado" });

        pedido.Estado = nuevoEstado;
        _adPedidos.Guardar(_cadeteria.ObtenerPedidos());

        return Ok(new { mensaje = "Estado del pedido actualizado", pedidoId, nuevoEstado });
    }
}
