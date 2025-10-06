using System.Text.Json;

public class AccesoADatosCadeteria
{
    private readonly string _filePath = "Cadeteria.json";

    public Cadeteria Obtener()
    {
        if (!File.Exists(_filePath))
            return new Cadeteria("Mi Cadeteria", 12345678);

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<Cadeteria>(json) ?? new Cadeteria("Mi Cadeteria", 12345678);
    }
}
