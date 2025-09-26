using System.Text.Json;

public interface IAccessoADatos
{
    List<Pedido> CargarPedidos(string archivo);
    List<Cadete> CargarCadetes(string archivo);
}

public class AccesoADatosCSV : IAccessoADatos
{
    public List<Pedido> CargarPedidos(string archivo)
    {
        var pedidos = new List<Pedido>();
        if (!File.Exists(archivo)) return pedidos;

        // read csv file and asign values to list of atributes 
        var lines = File.ReadAllLines(archivo);
        //skip header 
        for (int i = 1; i < lines.Length; i++)
        {   // split lines by comma 
            var parts = lines[i].Split(',');

            // create variables to be passed to pedido
            int nro = int.Parse(parts[0]);
            string obs = parts[1];
            bool estado = bool.Parse(parts[2]);

            // create variables to be passed to cliente
            string nombre = parts[3];
            string direccion = parts[4];
            int telefono = int.Parse(parts[5]);
            string datosRef = parts[6];

            var pedido = new Pedido(nro, obs, nombre, direccion, telefono, datosRef);
            pedido.CambiarEstado(estado);
            // add each instance to list 
            pedidos.Add(pedido);
        }

        return pedidos;
    }

    public List<Cadete> CargarCadetes(string archivo)
    {
        var cadetes = new List<Cadete>();
        if (!File.Exists(archivo)) return cadetes;

        var lines = File.ReadAllLines(archivo);
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            int id = int.Parse(parts[0]);
            string nombre = parts[1];
            string direccion = parts[2];
            int telefono = int.Parse(parts[3]);

            cadetes.Add(new Cadete(id, nombre, direccion, telefono));
        }
        return cadetes;
    }
}

public class AccesoADatosJSON : IAccessoADatos
{
    public List<Pedido> CargarPedidos(string archivo)
    {
        if (!File.Exists(archivo)) return new List<Pedido>();

        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
    }

    public List<Cadete> CargarCadetes(string archivo)
    {
        if (!File.Exists(archivo)) return new List<Cadete>();

        string json = File.ReadAllText(archivo);
        return JsonSerializer.Deserialize<List<Cadete>>(json) ?? new List<Cadete>();
    }
}



