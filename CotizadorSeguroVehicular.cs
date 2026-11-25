using System;

namespace SeguroVehicular
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. SOLICITUD DE DATOS CON VALIDACIÓN INMEDIATA
            Console.WriteLine("Hola, ¿cuál es tu nombre?");
            string nombre = Console.ReadLine();
            nombre = nombre.ToUpper(); 

            Console.WriteLine("Introduce tu edad:");
            int edad = Convert.ToInt32(Console.ReadLine());
            while (edad < 18 || edad > 80) 
            {
                Console.WriteLine("[ERROR] La edad debe estar entre 18 y 80 años. Pon tu edad otra vez:");
                edad = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Introduce cuántos años llevas conduciendo:");
            int conduciendo = Convert.ToInt32(Console.ReadLine());
            while (conduciendo < 0 || conduciendo > (edad - 18))
            {
                Console.WriteLine($"[ERROR] Experiencia no válida. Para tus {edad} años, el máximo posible es {edad - 18} años.");
                Console.WriteLine("Introduce los años de conducción de nuevo:");
                conduciendo = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("¿Cuál tipo de vehículo tienes? (1: Automóvil | 2: Motocicleta | 3: Camioneta):");
            int vehiculo = Convert.ToInt32(Console.ReadLine());
            while (vehiculo < 1 || vehiculo > 3)
            {
                Console.WriteLine("[ERROR] Opción inválida. Elige 1, 2 o 3:");
                vehiculo = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Introduce de qué año es tu vehículo:");
            int añodelcarro = Convert.ToInt32(Console.ReadLine());
            while (añodelcarro < 1990 || añodelcarro > 2026) 
            {
                Console.WriteLine("[ERROR] El año del vehículo debe estar entre 1990 y 2026. Pon el año de nuevo:");
                añodelcarro = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Tipo de cobertura (B: Básica | A: Amplia | P: Premium): ");
            string tipoCobertura = Console.ReadLine();
            tipoCobertura = tipoCobertura.ToUpper(); 
            while (tipoCobertura != "B" && tipoCobertura != "A" && tipoCobertura != "P")
            {
                Console.Write("[ERROR] Opción no válida. Escribe B, A o P: ");
                tipoCobertura = Console.ReadLine().ToUpper();
            }

            Console.Write("¿Tiene registro de accidentes en los últimos 3 años? (si / no): ");
            string registroAcc = Console.ReadLine();
            registroAcc = registroAcc.ToLower(); 
            while (registroAcc != "si" && registroAcc != "no")
            {
                Console.Write("[ERROR] Responde únicamente si o no: ");
                registroAcc = Console.ReadLine().ToLower();
            }

            Console.WriteLine("¿Cuántos accidentes ha tenido?");
            int CantAccidentes = Convert.ToInt32(Console.ReadLine());
            while ((registroAcc == "no" && CantAccidentes != 0) || (registroAcc == "si" && CantAccidentes < 1))
            {
                if (registroAcc == "no")
                    Console.WriteLine("[ERROR] Si seleccionó que no, la cantidad debe ser 0. Introduce de nuevo:");
                else
                    Console.WriteLine("[ERROR] Si seleccionó que si, debe registrar al menos 1 accidente. Introduce de nuevo:");

                CantAccidentes = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("¿El vehículo tiene alarma? (si / no):");
            string Alarma = Console.ReadLine();
            Alarma = Alarma.ToLower();
            while (Alarma != "si" && Alarma != "no")
            {
                Console.WriteLine("[ERROR] Responde únicamente si o no:");
                Alarma = Console.ReadLine().ToLower();
            }

            Console.WriteLine("Introduce la zona de circulación (1: Bajo riesgo | 2: Medio | 3: Alto):");
            int zona = Convert.ToInt32(Console.ReadLine());
            while (zona < 1 || zona > 3)
            {
                Console.WriteLine("[ERROR] Opción no válida. Escribe 1, 2 o 3:");
                zona = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Introduce la forma de pago (1: Anual | 2: Semestral | 3: Mensual):");
            int formaPago = Convert.ToInt32(Console.ReadLine());
            while (formaPago < 1 || formaPago > 3)
            {
                Console.WriteLine("[ERROR] Opción no válida. Escribe 1, 2 o 3:");
                formaPago = Convert.ToInt32(Console.ReadLine());
            }


            //9. CONDICIONES DE RECHAZO
            bool rechazado = false;
            int antiguedad = 2026 - añodelcarro;

            if (CantAccidentes >= 3) rechazado = true;
            if (antiguedad > 30) rechazado = true;
            if (edad < 21 && vehiculo == 2 && tipoCobertura == "P") rechazado = true;


            double precioBase = 0;
            double totalCargos = 0;
            double totalDescuentos = 0;
            string textoVehiculo = "";

            if (rechazado)
            {
                MostrarReporteFinal(nombre, "N/A", tipoCobertura, registroAcc.ToLower(), CantAccidentes, 0, 0, 0, 0, "N/A", 0, 0, "RECHAZADA");
            }
            else
            {
                // 4. SWITCH PRECIO BASE
                switch (vehiculo)
                {
                    case 1:
                        textoVehiculo = "Automóvil";
                        precioBase = 8000;
                        break;
                    case 2:
                        textoVehiculo = "Motocicleta";
                        precioBase = 5000;
                        break;
                    case 3:
                        textoVehiculo = "Camioneta";
                        precioBase = 10000;
                        break;
                    default:
                        textoVehiculo = "Otro";
                        precioBase = 0;
                        break;
                }

                //5. SWITCH COSTO DE COBERTURA
                switch (tipoCobertura)
                {
                    case "B":
                        break;
                    case "A":
                        totalCargos += precioBase * 0.35;
                        break;
                    case "P":
                        totalCargos += precioBase * 0.60;
                        break;
                }

                //6. CARGOS ADICIONALES
                if (edad >= 18 && edad <= 24)
                {
                    totalCargos += precioBase * 0.25;
                }

                if (conduciendo < 2)
                {
                    totalCargos += precioBase * 0.20;
                }
                else if (conduciendo >= 2 && conduciendo <= 5)
                {
                    totalCargos += precioBase * 0.10;
                }

                if (CantAccidentes == 1)
                {
                    totalCargos += precioBase * 0.15;
                }
                else if (CantAccidentes == 2)
                {
                    totalCargos += precioBase * 0.30;
                }

                //7. SWITCH CASE: CARGO POR ZONA
                switch (zona)
                {
                    case 1:
                        break;
                    case 2:
                        totalCargos += precioBase * 0.10;
                        break;
                    case 3:
                        totalCargos += precioBase * 0.20;
                        break;
                }

                //8. DESCUENTO POR ALARMA
                if (Alarma == "si" && CantAccidentes < 3)
                {
                    totalDescuentos += precioBase * 0.05;
                }

                //10. COSTO ANUAL INICIAL
                double costoAnual = precioBase + totalCargos - totalDescuentos;

                //11. SWITCH CASE: FORMA DE PAGO
                string textoPago = "";
                int cantidadPagos = 0;
                double montoPorPago = 0;

                switch (formaPago)
                {
                    case 1:
                        textoPago = "Anual";
                        totalDescuentos += costoAnual * 0.05;
                        costoAnual -= costoAnual * 0.05;
                        cantidadPagos = 1;
                        montoPorPago = costoAnual;
                        break;
                    case 2:
                        textoPago = "Semestral";
                        totalCargos += costoAnual * 0.03;
                        costoAnual += costoAnual * 0.03;
                        cantidadPagos = 2;
                        montoPorPago = costoAnual / 2;
                        break;
                    case 3:
                        textoPago = "Mensual";
                        totalCargos += costoAnual * 0.08;
                        costoAnual += costoAnual * 0.08;
                        cantidadPagos = 12;
                        montoPorPago = costoAnual / 12;
                        break;
                }

                //12. MOSTRAR REPORTE
                MostrarReporteFinal(nombre, textoVehiculo, tipoCobertura, registroAcc.ToLower(), CantAccidentes, precioBase, totalCargos, totalDescuentos, costoAnual, textoPago, cantidadPagos, montoPorPago, "ACEPTADA");
            }
        }

        static void MostrarReporteFinal(string cond, string veh, string cob, string accidenteTexto, int totalAcc, double pBase, double tCargos, double tDesc, double cAnual, string fPago, int cPagos, double mPago, string estado)
        {
            Console.WriteLine("REPORTE DE SEGURO VEHICULAR");
            Console.WriteLine($"· Nombre del conductor: {cond}");
            Console.WriteLine($"· Tipo de vehículo:     {veh}");
            Console.WriteLine($"· Tipo de cobertura:    {cob}");
            Console.WriteLine($"· Registro accidentes:  {accidenteTexto}");
            Console.WriteLine($"· Total de accidentes:  {totalAcc}"); 
            Console.WriteLine($"· Precio base:          ${pBase:N2}");
            Console.WriteLine($"· Total de cargos:      ${tCargos:N2}");
            Console.WriteLine($"· Total de descuentos:  ${tDesc:N2}");
            Console.WriteLine($"· Costo anual:          ${cAnual:N2}");
            Console.WriteLine($"· Forma de pago:        {fPago}");
            Console.WriteLine($"· Cantidad de pagos:    {cPagos}");
            Console.WriteLine($"· Monto de los pagos:   ${mPago:N2}");
            Console.WriteLine($"· Estado de solicitud:  {estado}");

        }
    }
}
