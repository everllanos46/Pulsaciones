using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using BLL;

namespace Pulsaciones
{
    public class Program
    {
        public static List<Persona> personas= new List<Persona>();
        static Persona persona;
        static Persona personaNueva;
        static PersonaService personaService = new PersonaService();
        static void Main(string[] args)
        {
            int Opcion;
            char Seguir = 'S';
            while (Seguir == 'S')
            {
                Console.Clear();
                Console.WriteLine("-----Programa Pulsaciones-----");
                Console.WriteLine("1.Calcular pulsaciones");
                Console.WriteLine("2.Modificar");
                Console.WriteLine("3.Consultar Personas");
                Console.WriteLine("4.Eliminar Persona");
                Console.WriteLine("5.Salir");
                Console.WriteLine("Digite una opción");
                Opcion = Convert.ToInt32(Console.ReadLine());
                switch (Opcion)
                {
                    case 1:
                        CalcularPulsaciones();
                        break;
                    case 2:
                        Modificar();
                        break;
                    case 3:
                        ConsultarPersonas();
                        break;
                    case 4:
                        Eliminar();
                        break;
                    default:
                        Seguir = 'N';
                        break;
                }
            }
        }
        
        public static void CalcularPulsaciones() {
            Console.Clear();
            persona = new Persona();
            Console.WriteLine("Ingrese su identificación");
            persona.Identificacion = Console.ReadLine();
            Console.WriteLine("Ingrese su Nombre");
            persona.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese su Edad");
            persona.Edad = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese su Sexo");
            persona.Sexo = Console.ReadLine();
            personaService.CalcularPulsacion(persona);
            personaService.Guardar(persona);
            Console.WriteLine($"Su pulsación es {persona.Pulsacion}");
            Console.ReadKey();
        }

        public static void Modificar() {
            persona = new Persona();  
            string Identificacion;           
            Console.WriteLine("Identificacion que quiera modificar");
            Identificacion = Console.ReadLine();
            persona=personaService.BuscarPersona(Identificacion);
            if (persona != null)
            {
                personaNueva = new Persona();
                personaNueva = persona;
                personaNueva = MenuModificar( personaNueva);
                personaService.Modificar(persona, personaNueva);
            } else
            {
                Console.WriteLine("No se encontró la identificación que busca");
                Console.ReadKey();
            }
        }

        public static Persona MenuModificar(Persona persona)
        {
            int Opcion;
            do {
                Console.Clear();
                Console.WriteLine("¿Qué quiere modificar?");
                Console.WriteLine("1.Nombre");
                Console.WriteLine("2.Edad");
                Console.WriteLine("3.Sexo");
                Opcion = Convert.ToInt32(Console.ReadLine());
                switch (Opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el nombre");
                        persona.Nombre = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Ingrese la edad");
                        persona.Edad = Convert.ToInt32(Console.ReadLine());
                        personaService.CalcularPulsacion(persona);
                        break;
                    case 3:
                        Console.WriteLine("Ingrese el sexo");
                        persona.Sexo = Console.ReadLine();
                        personaService.CalcularPulsacion(persona);
                        break;
                }
            } while (Opcion != 4);
            return persona;
        }

        public static void Eliminar()
        {
            persona = new Persona();
            Console.Clear();
            string Identificacion;
            Console.WriteLine("Identificacion de la persona que quiera eliminar");
            Identificacion = Console.ReadLine();
            persona = personaService.BuscarPersona(Identificacion);
            if (persona != null)
            {
                personaService.Eliminar(persona);
                Console.WriteLine("Persona eliminada correctamente");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No se encontró la identificación que quiere eliminar");
                Console.ReadKey();
            }
        }

        public static void ConsultarPersonas()
        {
            Console.Clear();
            personas=personaService.ConsultarPersonas();
            foreach (var item in personas)
            {
                Console.WriteLine($"Nombre: {item.Nombre}");
                Console.WriteLine($"Identificación: {item.Identificacion}");
                Console.WriteLine($"Edad: {item.Edad}");
                Console.WriteLine($"Sexo: {item.Sexo}");
                Console.WriteLine($"Pulsación: {item.Pulsacion}\n");       
            }
            Console.ReadKey();
        }
    }
}
