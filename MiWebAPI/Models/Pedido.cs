public class Pedido
{
    public int Nro { get; private set; }
    public string? Obs { get; private set; }
    public bool Estado { get; private set; } = false; // default false

    public Cliente Cliente { get; private set; }

    public Cadete? Cadete { get; private set; } = null; // default null, pedido can exist without cadete


    public Pedido(int Nro, string? Obs, string? Nombre, string? Direccion, int Telefono, string? DatosReferenciaDireccionbool)
    {
        this.Cliente = new Cliente(Nombre, Direccion, Telefono, DatosReferenciaDireccionbool); // composition
        this.Nro = Nro;
        this.Obs = Obs;
    }
    public void CambiarEstado(bool nuevoEstado)
    {
        Estado = nuevoEstado;
    }
    public void SetCadete(Cadete cadete)
    {
        Cadete = cadete;
    }

    public void VerDireccionCliente()
    {
        Console.WriteLine($"Direccion: {Cliente.Direccion ?? "Sin dirección"}");
    }
    public void VerDatosCliente()
    {
        Cliente.MostrarCliente();
    }
    public void MostrarPedido()
    {
        Console.WriteLine("Cargando Pedido...");
        Console.WriteLine("- - -");
        Console.WriteLine($"Pedido #{Nro} - Obs: {Obs} - Estado: {Estado}");
        VerDatosCliente();
        VerDireccionCliente();
    }
    public void DarDeAltaPedido(int nro, string obs, string nombre, bool estado, string direccion, int telefono, string datosRef)
    {
        // cadete removed from parameter because pedido can exist without cadete 
        Pedido nuevoPedido = new Pedido(nro, obs, nombre, direccion, telefono, datosRef);
        Console.WriteLine($"Pedido #{nro} dado de alta correctamente.");
    }

}