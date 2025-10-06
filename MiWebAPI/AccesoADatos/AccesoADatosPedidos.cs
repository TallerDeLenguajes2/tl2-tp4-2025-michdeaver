using System.Text.Json;

public class AccesoADatosPedidos
{
    private readonly string _filePath = "Pedidos.json";

    public List<Pedido> Obtener()
    {
        if (!File.Exists(_filePath))
            return new List<Pedido>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
    }

    public void Guardar(List<Pedido> pedidos)
    {
        var json = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
