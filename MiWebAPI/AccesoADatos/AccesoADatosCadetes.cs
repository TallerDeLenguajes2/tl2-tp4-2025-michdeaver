using System.Text.Json;

public class AccesoADatosCadetes
{
    private readonly string _filePath = "Cadetes.json";

    public List<Cadete> Obtener()
    {
        if (!File.Exists(_filePath))
            return new List<Cadete>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Cadete>>(json) ?? new List<Cadete>();
    }
}
