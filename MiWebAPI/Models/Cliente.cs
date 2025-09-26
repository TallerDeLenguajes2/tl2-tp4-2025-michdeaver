public class Cliente
{
    // change to private later
    public string? Nombre { get; private set; }
    public string? Direccion { get; private set; }
    public int Telefono { get; private set; }
    public string? DatosReferenciaDireccion { get; private set; }

    //constructor para los campos de cliente

    public Cliente(string? Nombre, string? Direccion, int Telefono, string? DatosReferenciaDireccion)
    {
        this.Nombre = Nombre;
        this.Direccion = Direccion;
        this.Telefono = Telefono;
        this.DatosReferenciaDireccion = DatosReferenciaDireccion;
    }
    public void MostrarCliente()
    {
        Console.WriteLine ($"Nombre: {Nombre}, Dirección: {Direccion}, Teléfono: {Telefono}");
    }
}