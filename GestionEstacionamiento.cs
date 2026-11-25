using System;

namespace Examen_2_programacion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Nombre del encargado del estacionamiento: ");
                string encargado = Console.ReadLine().ToUpper();

                int cantidadVehiculos = SolicitarCantidadVehiculos();

                double totalDinero = 0;
                int cantMotos = 0;
                int cantAutos = 0;
                int cantCamionetas = 0;
                int cantConDescuento = 0;

                for (int i = 0; i < cantidadVehiculos; i++)
                {
                    Console.WriteLine($"VEHICULO {i + 1}");

                    string propietario;
                    do
                    {
                        Console.Write("Nombre propietario: ");
                        propietario = Console.ReadLine().ToUpper();
                        if (string.IsNullOrWhiteSpace(propietario)) Console.WriteLine("Error: no vacio");
                    } while (string.IsNullOrWhiteSpace(propietario));

                    int tipo = SolicitarTipoVehiculo();
                    int horas = SolicitarHoras();

                    double costoHora = ObtenerCostoPorHora(tipo);
                    string nombreTipo = ObtenerNombreTipo(tipo);

                    if (tipo == 1) cantMotos++;
                    else if (tipo == 2) cantAutos++;
                    else if (tipo == 3) cantCamionetas++;

                    double porcDescuento = CalcularPorcentajeDescuento(horas);
                    if (porcDescuento > 0) cantConDescuento++;

                    double costoInicial = horas * costoHora;

                    double cantidadDescuento = (costoInicial * porcDescuento) / 100;

                    double totalPagar = CalcularTotalPagar(costoInicial, cantidadDescuento);
                    totalDinero += totalPagar;

                    MostrarInfoVehiculo(propietario, nombreTipo, horas, costoHora, costoInicial, porcDescuento, totalPagar);
                }

                string tipoMasRegistrado = DeterminarTipoMasRegistrado(cantMotos, cantAutos, cantCamionetas);
                double promedio = totalDinero / cantidadVehiculos;

                MostrarReporteFinal(encargado, cantidadVehiculos, cantMotos, cantAutos, cantCamionetas, cantConDescuento, tipoMasRegistrado, totalDinero, promedio);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error inesperado: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("El sistema de estacionamiento ha terminado.");
            }
        }

        static int SolicitarCantidadVehiculos()
        {
            int cantidad; bool valido;
            do
            {
                Console.Write("Indique la cantidad de Vehiculos a registrar (1-10): ");
                valido = int.TryParse(Console.ReadLine(), out cantidad) && cantidad >= 1 && cantidad <= 10;
                if (!valido) Console.WriteLine("Error, por favor coloca un numero del 1 al 10");
            } while (!valido);
            return cantidad;
        }

        static int SolicitarTipoVehiculo()
        {
            int tipo; bool ok;
            do
            {
                Console.WriteLine("Muestra el siguiente menú para seleccionar el tipo de vehículo:");
                Console.WriteLine("1. Motocicleta");
                Console.WriteLine("2. Automóvil");
                Console.WriteLine("3. Camioneta");
                Console.Write("Numero: ");
                ok = int.TryParse(Console.ReadLine(), out tipo) && tipo >= 1 && tipo <= 3;
                if (!ok) Console.WriteLine("Error: Escribe un numero del 1 a 3");
            } while (!ok);
            return tipo;
        }

        static int SolicitarHoras()
        {
            int horas; bool ok;
            do
            {
                Console.Write("Horas (1-24): ");
                ok = int.TryParse(Console.ReadLine(), out horas) && horas >= 1 && horas <= 24;
                if (!ok) Console.WriteLine("Error: Escribe un numero del 1 a 24");
            } while (!ok);
            return horas;
        }

        static double ObtenerCostoPorHora(int tipo)
        {
            switch (tipo)
            {
                case 1: return 15;
                case 2: return 25;
                case 3: return 35;
                default: return 0;
            }
        }

        static string ObtenerNombreTipo(int tipo)
        {
            switch (tipo)
            {
                case 1: return "MOTOCICLETA";
                case 2: return "AUTOMOVIL";
                case 3: return "CAMIONETA";
                default: return "DESCONOCIDO";
            }
        }

        static double CalcularPorcentajeDescuento(int horas)
        {
            if (horas > 10) return 20;      
            else if (horas >= 5) return 10;  
            else return 0;                  
        }

        static double CalcularTotalPagar(double costoInicial, double descuento)
        {
            return costoInicial - descuento;
        }

        static string DeterminarTipoMasRegistrado(int motos, int autos, int camionetas)
        {
            if (motos == autos && autos == camionetas) return "Existe un empate entre los tipos de vehiculos.";
            if (motos == autos && motos > camionetas) return "Existe un empate entre los tipos de vehiculos.";
            if (motos == camionetas && motos > autos) return "Existe un empate entre los tipos de vehiculos.";
            if (autos == camionetas && autos > motos) return "Existe un empate entre los tipos de vehiculos.";

            if (motos > autos && motos > camionetas) return "MOTOCICLETA";
            if (autos > motos && autos > camionetas) return "AUTOMOVIL";
            if (camionetas > motos && camionetas > autos) return "CAMIONETA";
            return "Existe un empate entre los tipos de vehiculos.";
        }

        static void MostrarInfoVehiculo(string propietario, string tipo, int horas, double costoHora, double costoInicial, double porc, double total)
        {
            Console.WriteLine(" Muestra después de cada registro");
            Console.WriteLine($"• Nombre del propietario: {propietario}");
            Console.WriteLine($"• Tipo de vehículo: {tipo}");
            Console.WriteLine($"• Horas estacionado: {horas}");
            Console.WriteLine($"• Costo por hora: ${costoHora}");
            Console.WriteLine($"• Costo inicial: ${costoInicial}");
            Console.WriteLine($"• Porcentaje de descuento: {porc}%");
            Console.WriteLine($"• Total a pagar: ${total}");
        }

        static void MostrarReporteFinal(string encargado, int totalVeh, int motos, int autos, int camionetas, int conDesc, string masRegistrado, double dineroTotal, double promedio)
        {
            Console.WriteLine("REPORTE FINAL");
            Console.WriteLine($"• Nombre del encargado: {encargado}");
            Console.WriteLine($"• Cantidad total de vehiculos: {totalVeh}");
            Console.WriteLine($"• Cantidad de motocicletas: {motos}");
            Console.WriteLine($"• Cantidad de automoviles: {autos}");
            Console.WriteLine($"• Cantidad de camionetas: {camionetas}");
            Console.WriteLine($"• Vehiculos que recibieron descuento: {conDesc}");
            Console.WriteLine($"• Tipo de vehiculo mas registrado: {masRegistrado}");
            Console.WriteLine($"• Dinero total recibido: ${dineroTotal:F2}");
            Console.WriteLine($"• Promedio pagado por vehiculo: ${promedio:F2}");
        }
    }
}