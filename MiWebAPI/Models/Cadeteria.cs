using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;

public class Cadeteria
{
    public string? Nombre { get; private set; }
    public int Telefono { get; private set; }

    // Lists now come from Data Access Layer
    private List<Cadete> ListadoCadetes { get; set; } = new();
    private List<Pedido> ListadoPedidos { get; set; } = new();


    public Cadeteria(string? nombre, int telefono)
    {
        this.Nombre = nombre;
        this.Telefono = telefono;
    }

    // Methods to inject data from JSON
    public void AgregarListaCadetes(List<Cadete> cadetes) => ListadoCadetes = cadetes;
    public void AgregarListaPedidos(List<Pedido> pedidos) => ListadoPedidos = pedidos;

    public List<Cadete> ObtenerCadetes() => ListadoCadetes;
    public List<Pedido> ObtenerPedidos() => ListadoPedidos;

    // Safe getters
    public Cadete? GetCadete(int idCadete) => ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);
    public Pedido? GetPedido(int idPedido) => ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);


    // no need for method reasignar, asignarCadete functions for both. 
    public void AsignarCadeteAPedidos(Pedido pedido, Cadete cadete)
    {
        if (cadete != null && pedido != null)
        {
            pedido.SetCadete(cadete);
        }
    }

    public float JornalACobrar(int idCadete)
    {
        var cadete = GetCadete(idCadete);
        if (cadete == null) return 0;
        var cantPedidos = ListadoPedidos.Count(p => p.Cadete == cadete);
        float costoPorPedido = 800;
        return cantPedidos * costoPorPedido;
    }

    public string? MostrarCadetes()
    {
        StringBuilder mostrar = new StringBuilder();
        foreach (var cadete in ListadoCadetes)
        {
            mostrar.Append($"Cadete Id: {cadete.Id} - Nombre: {cadete.Nombre} - Direccion: {cadete.Direccion} - Telefono: {cadete.Telefono}");
            mostrar.Append("\n-----------\n");
        }
        return mostrar.ToString();
    }

    public string? MostrarPedidos()
    {
        StringBuilder mostrar = new StringBuilder();
        foreach (var pedido in ListadoPedidos)
        {
            mostrar.Append($"Pedido #{pedido.Nro} - Obs: {pedido.Obs} - Estado: {pedido.Estado}");
            mostrar.Append(pedido.VerDatosCliente());
            mostrar.Append(pedido.VerDireccionCliente());
            if (pedido.Cadete != null)
            {
                mostrar.Append($"Cadete #{pedido.Cadete.Id} - Nombre: {pedido.Cadete.Nombre}");
            }
            else mostrar.Append("No asignado");

            mostrar.Append("- - -");
        }
        return mostrar.ToString();
    }
     public string? MostrarCadeteria() => $"Cadeteria: {Nombre}, Telefono: {Telefono}";

    // Structured data for API
    public object Informe()
    {
        int totalPedidos = 0;
        float totalGanado = 0;

        var cadetesInfo = ListadoCadetes.Select(c =>
        {
            int cantPedidos = ListadoPedidos.Count(p => p.Cadete == c);
            float jornal = JornalACobrar(c.Id);

            totalPedidos += cantPedidos;
            totalGanado += jornal;

            return new
            {
                c.Id,
                c.Nombre,
                EnvíosRealizados = cantPedidos,
                Jornal = jornal
            };
        }).ToList();

        float promedioEnvios = ListadoCadetes.Count > 0 ? (float)totalPedidos / ListadoCadetes.Count : 0;

        return new
        {
            Cadetes = cadetesInfo,
            Totales = new
            {
                TotalPedidos = totalPedidos,
                TotalGanado = totalGanado,
                PromedioEnvios = promedioEnvios
            }
        };
    }
    
}
