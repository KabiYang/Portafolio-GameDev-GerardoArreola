using System;

namespace TiendaObjetosJugador
{
    public enum TipoObjeto
    {
        Pocion,
        Escudo,
        Espada
    }

    public class ObjetoTienda
    {
        private string nombre;
        private int precio;
        private TipoObjeto tipo;
        private int existencias;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value.ToUpper(); }
        }

        public int Precio
        {
            get { return precio; }
            set
            {
                if (value < 0) precio = 0;
                else precio = value;
            }
        }

        public TipoObjeto Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public int Existencias
        {
            get { return existencias; }
            set
            {
                if (value < 0) existencias = 0;
                else existencias = value;
            }
        }

        public ObjetoTienda(string nombre, int precio, TipoObjeto tipo, int existenciasIniciales)
        {
            Nombre = nombre;
            Precio = precio;
            Tipo = tipo;
            Existencias = existenciasIniciales;
        }

        public bool HayExistencias()
        {
            return Existencias > 0;
        }

        public void ReducirExistencia()
        {
            if (HayExistencias())
            {
                Existencias--;
            }
        }

        public void Mostrar()
        {
            Console.WriteLine(Nombre + " (" + Tipo + ") | Precio: " + Precio + " | Existencias: " + Existencias);
        }
    }

    public class Jugador
    {
        private string nombre;
        private int monedas;
        private int Vida;
        private int defensa;
        private int Daño;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value.ToUpper(); }
        }

        public int Monedas
        {
            get { return monedas; }
            set
            {
                if (value < 0) monedas = 0;
                else monedas = value;
            }
        }

        public int Salud
        {
            get { return Vida; }
            set
            {
                if (value < 0) Vida = 0;
                else if (value > 100) Vida = 100;
                else Vida = value;
            }
        }

        public int Defensa
        {
            get { return defensa; }
            set
            {
                if (value < 0) defensa = 0;
                else defensa = value;
            }
        }

        public int Ataque
        {
            get { return Daño; }
            set
            {
                if (value < 0) Daño = 0;
                else Daño = value;
            }
        }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Monedas = 200;
            Salud = 70;
            Defensa = 0;
            Ataque = 20;
        }

        public bool PuedeComprar(ObjetoTienda objeto)
        {
            if (Monedas >= objeto.Precio && objeto.HayExistencias())
                return true;
            else
                return false;
        }

        public void Comprar(ObjetoTienda objeto)
        {
            if (!PuedeComprar(objeto))
            {
                Console.WriteLine("No se pudo comprar " + objeto.Nombre + ":");
                if (Monedas < objeto.Precio)
                    Console.WriteLine("Crees que hago actos de caridad? Eres pobre y no tienes plata. Largate y busca chamba");
                if (!objeto.HayExistencias())
                    Console.WriteLine("Que estas ciego y no ves que ya no hay? ya se agoto");
                return;
            }

            Monedas -= objeto.Precio;
            objeto.ReducirExistencia();

            switch (objeto.Tipo)
            {
                case TipoObjeto.Pocion:
                    Salud += 30;
                    Console.WriteLine("Compraste " + objeto.Nombre + ", recuperas 30 de salud, salud: " + Salud);
                    break;
                case TipoObjeto.Escudo:
                    Defensa += 15;
                    Console.WriteLine("Compraste " + objeto.Nombre + ", defensa +15, defensa: " + Defensa);
                    break;
                case TipoObjeto.Espada:
                    Ataque += 20;
                    Console.WriteLine("Compraste " + objeto.Nombre + ", ataque +20, ataque: " + Ataque);
                    break;
            }
        }

        public void Mostrar()
        {
            Console.WriteLine("Nombre del jugador: " + Nombre + " Monedas: " + Monedas + " Salud: " + Salud + " Defensa: " + Defensa + " Ataque: " + Ataque);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Nombre del jugador: ");
                string nombreJugador = Console.ReadLine();

                Jugador jugador1 = new Jugador(nombreJugador);

                ObjetoTienda pocion = new ObjetoTienda("POCION", 40, TipoObjeto.Pocion, 3);
                ObjetoTienda escudo = new ObjetoTienda("ESCUDO", 75, TipoObjeto.Escudo, 2);
                ObjetoTienda espada = new ObjetoTienda("ESPADA", 100, TipoObjeto.Espada, 1);

                int opcion;
                bool ok;

                do
                {
                    Console.WriteLine("MENU PRINCIPAL");
                    Console.WriteLine("1. Comprar pocion");
                    Console.WriteLine("2. Comprar escudo");
                    Console.WriteLine("3. Comprar espada");
                    Console.WriteLine("4. Ver informacion del jugador");
                    Console.WriteLine("5. Ver informacion de la tienda");
                    Console.WriteLine("6. Salir");
                    Console.Write("Opcion: ");

                    ok = int.TryParse(Console.ReadLine(), out opcion);

                    if (!ok || opcion < 1 || opcion > 6)
                    {
                        Console.WriteLine("Opcion no valida");
                        continue;
                    }

                    switch (opcion)
                    {
                        case 1:
                            jugador1.Comprar(pocion);
                            break;
                        case 2:
                            jugador1.Comprar(escudo);
                            break;
                        case 3:
                            jugador1.Comprar(espada);
                            break;
                        case 4:
                            jugador1.Mostrar();
                            break;
                        case 5:
                            pocion.Mostrar();
                            escudo.Mostrar();
                            espada.Mostrar();
                            break;
                        case 6:
                            Console.WriteLine("Saliendo de la tienda.");
                            break;
                    }

                } while (opcion != 6);
            }
            catch
            {
                Console.WriteLine("Ocurrió un error inesperado.");
            }
            finally
            {
                Console.WriteLine("El sistema de la tienda ha terminado.");
            }
        }
    }
}