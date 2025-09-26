using System.Globalization;

public class Cadete
{
    public int Id { get; private set; }
    public string? Nombre { get; private set; }
    public string? Direccion { get; private set; }
    public int Telefono { get; private set; }
    

    public Cadete(int Id, string? Nombre, string? Direccion, int Telefono)
    {
        this.Id = Id;
        this.Nombre = Nombre;
        this.Direccion = Direccion;
        this.Telefono = Telefono;
    }
    public void MostrarCadete()
    { 
        Console.WriteLine("Cargando Cadete...");
        Console.WriteLine("-----------");
        Console.WriteLine($"Cadete Id: {Id} - Nombre: {Nombre} - Direccion: {Direccion} - Telefono: {Telefono}");
    }

}