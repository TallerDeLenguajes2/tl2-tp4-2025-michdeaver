using System.Security.AccessControl;

Console.WriteLine("=== Cadeteria ===\n");
/*
Console.WriteLine("Seleccione fuente de datos: 1=CSV, 2=JSON");
int.TryParse(Console.ReadLine(), out int option);



IAccessADatos acceso = option == 1
    ? new AccesoADatosCSV() 
    : new AccesoADatosJSON();



Console.WriteLine("\n=== Cargar Datos ===\n");
// choose file paths based on file type
string archivoCadetes = option == 1 ? "cadetes.csv" : "cadetes.json";
string archivoPedidos = option == 1 ? "pedidos.csv" : "pedidos.json";
*/

//declaring instance of acceso 
IAccessoADatos acceso;
Console.WriteLine("Seleccione fuente de datos: 1=CSV, 2=JSON");
string? option = Console.ReadLine();
string? ext;
switch (option)
{
    case "1":
        acceso = new AccesoADatosCSV();
        ext = ".csv";
        break;
    case "2":
        acceso = new AccesoADatosJSON();
        ext = ".json";
        break;
    default:
        acceso = new AccesoADatosCSV();
         ext = ".csv";
        break;
}
string? archivoPedido = $"pedidos.{ext}";
string? archivoCadete = $"cadetes.{ext}";

var cadeteriaCentro = new Cadeteria("Cadeteria El Centro", 12345678);
cadeteriaCentro.MostrarCadeteria();

// create each list using interface methods 
var pedidos = acceso.CargarPedidos(archivoPedido);
var cadetes = acceso.CargarCadetes(archivoCadete);

// using method to asign lists to each attribute 
cadeteriaCentro.CargarListadoPedidos(pedidos);
cadeteriaCentro.CargarListadoCadetes(cadetes);


cadeteriaCentro.MostrarCadetes();
cadeteriaCentro.MostrarPedidos();

Console.WriteLine("\n=== Asignar Cadete a Pedido ===\n");
cadeteriaCentro.AsignarCadeteAPedidos();

cadeteriaCentro.MostrarPedidos();

Console.WriteLine("\n- -  Reasignar Cadete a Pedido - -\n");
cadeteriaCentro.AsignarCadeteAPedidos();
cadeteriaCentro.MostrarPedidos();

cadeteriaCentro.Informe();
