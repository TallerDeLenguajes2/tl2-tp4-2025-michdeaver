using System.Globalization;

public class Cadeteria
{
    public string? Nombre { get; private set; }
    public int Telefono { get; private set; }
    public List<Cadete> ListadoCadetes { get; private set; } = new List<Cadete>();
    public List<Pedido> ListadoPedidos { get; private set; } = new List<Pedido>();


    public Cadeteria(string? nombre, int telefono)
    {
        this.Nombre = nombre;
        this.Telefono = telefono;
    }

    // encapsulated set 
    public void CargarListadoPedidos(List<Pedido> Pedidos)
    {
        ListadoPedidos = Pedidos;

    }
    public void CargarListadoCadetes(List<Cadete> Cadetes){
        ListadoCadetes = Cadetes;
    }

    // no need for method reasignar, asignarCadete functions for both. 
    public void AsignarCadeteAPedidos()
    {
        Console.Write("Ingrese ID de cadete: ");
        int.TryParse(Console.ReadLine(), out int idCadete);

        Console.Write("Ingrese ID de pedido: ");
        int.TryParse(Console.ReadLine(), out int idPedido);

        //first go through each list to identifty respective cadete and pedido
        var cadete = ListadoCadetes.FirstOrDefault(c => c.Id == idCadete);
        var pedido = ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);
        //access is set private, needs function for capsulation.
        if (cadete == null || pedido == null)
        {
            Console.WriteLine("No se pudo asignar cadete a pedido. Entrada Nulo");
        }
        else pedido.SetCadete(cadete);
    }

    public float JornalACobrar(int id)
    {
        var cadete = ListadoCadetes.FirstOrDefault(c => c.Id == id);
        var cantPedidos = ListadoPedidos.Count(p => p.Cadete == cadete);
        float costoPorPedido = 800;
        float sueldo = cantPedidos * costoPorPedido;
        return sueldo;
    }

    // Cadeteria doesn't care how the data is loaded
    // AccesoADatosCSV or AccesoADatosJSON will implement the steps requires for each case

    public void MostrarCadetes()
    {
        Console.WriteLine("Cargando Lista de Cadetes...");
        Console.WriteLine("-----------");
        foreach (var cadete in ListadoCadetes)
        {
            Console.WriteLine($"Cadete Id: {cadete.Id} - Nombre: {cadete.Nombre} - Direccion: {cadete.Direccion} - Telefono: {cadete.Telefono}");
            Console.WriteLine("-----------");
        }
    }

    public void MostrarPedidos()
    {
        Console.WriteLine("Cargando Lista de Pedidos...");
        Console.WriteLine("- - -");
        foreach (var pedido in ListadoPedidos)
        {
            Console.WriteLine($"Pedido #{pedido.Nro} - Obs: {pedido.Obs} - Estado: {pedido.Estado}");
            pedido.VerDatosCliente();
            pedido.VerDireccionCliente();
            if (pedido.Cadete != null)
            {
                Console.WriteLine($"Cadete #{pedido.Cadete.Id} - Nombre: {pedido.Cadete.Nombre}");
            }
            else Console.WriteLine("No asignado");

            Console.WriteLine("- - -");
        }
    }
    public void MostrarCadeteria()
    {
        Console.WriteLine($"Cadeteria: {Nombre}, Telefono: {Telefono}");
    }

    public void Informe()
    {
        Console.WriteLine("\n=== Informe de Jornada ===\n");

        int totalPedidos = 0;
        float totalGanado = 0;

        foreach (var cadete in ListadoCadetes)
        {
            var cantPedidos = ListadoPedidos.Count(p => p.Cadete == cadete);
            float jornal = JornalACobrar(cadete.Id);

            Console.WriteLine($"Cadete: {cadete.Nombre}");
            Console.WriteLine($"  Envíos realizados: {cantPedidos}");
            Console.WriteLine($"  Jornal ganado: ${jornal}\n");
            // adding up pedidos and total jornal while moving through list
            totalPedidos += cantPedidos;
            totalGanado += jornal;
        }

        int cantidadCadetes = ListadoCadetes.Count();
        //checks if cantidad de cadetes is empty before calculating promedio
        var promedioEnvios = cantidadCadetes > 0 ? (float)totalPedidos / cantidadCadetes : 0;

        Console.WriteLine("=== Totales ===");
        Console.WriteLine($"Total de envíos: {totalPedidos}");
        Console.WriteLine($"Total ganado por todos los cadetes: ${totalGanado}");
        Console.WriteLine($"Promedio de envíos por cadete: {promedioEnvios:F2}");
    }
    
    
}
