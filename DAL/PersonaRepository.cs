using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.IO;


namespace DAL
{
    public class PersonaRepository
    {
        private string ruta = "Persona.txt";
        public List<Persona> personas;
        public PersonaRepository()
        {
            personas = new List<Persona>();
        }
        public void Guardar(List<Persona> listaPersonas)
        {
            File.Delete(ruta);
            personas = listaPersonas;
            FileStream file = new FileStream(ruta, FileMode.Append);
            StreamWriter escritor = new StreamWriter(file);
            foreach (var item in personas)
            {
                escritor.WriteLine($"{item.Nombre};{item.Identificacion};{item.Sexo};{item.Edad};{item.Pulsacion}");
            }
            
            escritor.Close();
            file.Close();
        }


        public List<Persona> ConsultarPersonas()
        {
            personas = new List<Persona>();
            string Linea = string.Empty;
            FileStream sourceStream = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(sourceStream);
            while((Linea = reader.ReadLine()) != null)
            {
                Persona persona = new Persona();
                char delimiter = ';';
                string[] MatrizPersona = Linea.Split(delimiter);
                persona.Nombre = MatrizPersona[0];
                persona.Identificacion = MatrizPersona[1];
                persona.Sexo = MatrizPersona[2];
                persona.Edad = Convert.ToInt32(MatrizPersona[3]);
                persona.Pulsacion = Convert.ToDecimal(MatrizPersona[4]);
                personas.Add(persona);
            }
            reader.Close();
            sourceStream.Close();
            return personas;
        }

        public Persona  BuscarPersona(string Identificacion)
        {
            Persona persona = new Persona();
            foreach (var item in personas)
            {
                if (item.Identificacion.Equals(Identificacion))
                {   
                    persona = item;
                    return persona;  
                }
            }
            return null;    
        }

        public void Modificar(Persona persona, Persona personaNueva)
        {
            personas.Remove(persona);
            personas.Add(personaNueva);
            Guardar(personas);
        }

        public void Eliminar(Persona persona)
        {
            personas.Remove(persona);
            Guardar(personas);
        }
    }
}
