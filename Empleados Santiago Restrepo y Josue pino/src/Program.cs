using System;
using System.Collections.Generic;

// Clase que representa a un empleado
class Empleado
{
    public string Nombre { get; set; }
    public int Id { get; set; }
    public double Salario { get; set; }
    public Empleado? Anterior { get; set; }
    public Empleado? Siguiente { get; set; }

    public Empleado(string nombre, int id, double salario)
    {
        Nombre = nombre;
        Id = id;
        Salario = salario;
        Anterior = null;
        Siguiente = null;
    }
}

// Clase que maneja la lista doblemente ligada
class ListaDobleEmpleados
{
    private Empleado? cabeza;

    public ListaDobleEmpleados()
    {
        cabeza = null;
    }

    public void InsertarEmpleado(Empleado empleado)
    {
        if (BuscarEmpleado(empleado.Id) != null)
        {
            Console.WriteLine("Error: Ya existe un empleado con ese ID.");
            return;
        }

        if (cabeza == null)
        {
            cabeza = empleado;
        }
        else
        {
            Empleado actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = empleado;
            empleado.Anterior = actual;
        }
        Console.WriteLine("Empleado agregado correctamente.");
    }

    public Empleado? BuscarEmpleado(int id)
    {
        Empleado? actual = cabeza;
        while (actual != null)
        {
            if (actual.Id == id)
                return actual;
            actual = actual.Siguiente;
        }
        return null;
    }

    public void ImprimirLista()
    {
        if (cabeza == null)
        {
            Console.WriteLine("No hay empleados registrados.");
            return;
        }
        Empleado? actual = cabeza;
        while (actual != null)
        {
            Console.WriteLine($"Nombre: {actual.Nombre}, ID: {actual.Id}, Salario: {actual.Salario:C}");
            actual = actual.Siguiente;
        }
    }

    public void EliminarEmpleado(int id)
    {
        Empleado? actual = cabeza;
        while (actual != null)
        {
            if (actual.Id == id)
            {
                if (actual.Anterior != null)
                    actual.Anterior.Siguiente = actual.Siguiente;
                if (actual.Siguiente != null)
                    actual.Siguiente.Anterior = actual.Anterior;
                if (actual == cabeza)
                    cabeza = actual.Siguiente;
                Console.WriteLine("Empleado eliminado correctamente.");
                return;
            }
            actual = actual.Siguiente;
        }
        Console.WriteLine("Empleado no encontrado.");
    }

    public void OrdenarPorNombre(bool ascendente = true)
    {
        if (cabeza == null || cabeza.Siguiente == null) return;
        bool cambiado;
        do
        {
            cambiado = false;
            Empleado? actual = cabeza;
            while (actual?.Siguiente != null)
            {
                if ((ascendente && actual.Nombre.CompareTo(actual.Siguiente.Nombre) > 0) ||
                    (!ascendente && actual.Nombre.CompareTo(actual.Siguiente.Nombre) < 0))
                {
                    (actual.Nombre, actual.Siguiente.Nombre) = (actual.Siguiente.Nombre, actual.Nombre);
                    (actual.Id, actual.Siguiente.Id) = (actual.Siguiente.Id, actual.Id);
                    (actual.Salario, actual.Siguiente.Salario) = (actual.Siguiente.Salario, actual.Salario);
                    cambiado = true;
                }
                actual = actual.Siguiente;
            }
        } while (cambiado);
        Console.WriteLine("Lista ordenada correctamente por nombre.");
        ImprimirLista();
    }

    public void OrdenarPorSalario(bool ascendente = true)
    {
        if (cabeza == null || cabeza.Siguiente == null) return;
        bool cambiado;
        do
        {
            cambiado = false;
            Empleado? actual = cabeza;
            while (actual?.Siguiente != null)
            {
                if ((ascendente && actual.Salario > actual.Siguiente.Salario) ||
                    (!ascendente && actual.Salario < actual.Siguiente.Salario))
                {
                    (actual.Nombre, actual.Siguiente.Nombre) = (actual.Siguiente.Nombre, actual.Nombre);
                    (actual.Id, actual.Siguiente.Id) = (actual.Siguiente.Id, actual.Id);
                    (actual.Salario, actual.Siguiente.Salario) = (actual.Siguiente.Salario, actual.Salario);
                    cambiado = true;
                }
                actual = actual.Siguiente;
            }
        } while (cambiado);
        Console.WriteLine("Lista ordenada correctamente por salario.");
        ImprimirLista();
    } // <-- Se cerró correctamente aquí

    public int ContarEmpleados()
    {
        int count = 0;
        Empleado? actual = cabeza;
        while (actual != null)
        {
            count++;
            actual = actual.Siguiente;
        }
        return count;
    }

    // Métodos de estadísticas
    public double CalcularPromedioSalario()
    {
        double total = 0;
        int contador = 0;
        Empleado? actual = cabeza;
        while (actual != null)
        {
            total += actual.Salario;
            contador++;
            actual = actual.Siguiente;
        }
        return contador > 0 ? total / contador : 0;
    }

    public Empleado? EncontrarSalarioMaximo()
    {
        if (cabeza == null) return null;
        Empleado? maximo = cabeza;
        Empleado? actual = cabeza.Siguiente;
        while (actual != null)
        {
            if (actual.Salario > maximo.Salario)
                maximo = actual;
            actual = actual.Siguiente;
        }
        return maximo;
    }

    public Empleado? EncontrarSalarioMinimo()
    {
        if (cabeza == null) return null;
        Empleado? minimo = cabeza;
        Empleado? actual = cabeza.Siguiente;
        while (actual != null)
        {
            if (actual.Salario < minimo.Salario)
                minimo = actual;
            actual = actual.Siguiente;
        }
        return minimo;
    }

    public double ObtenerMedianaSalario()
    {
        List<double> salarios = new();
        Empleado? actual = cabeza;
        while (actual != null)
        {
            salarios.Add(actual.Salario);
            actual = actual.Siguiente;
        }
        salarios.Sort();
        int n = salarios.Count;
        if (n == 0) return 0;
        return n % 2 == 1 ? salarios[n / 2] : (salarios[(n / 2) - 1] + salarios[n / 2]) / 2;
    }
}

// Clase principal con el menú de usuario
class Program
{
    static void Main()
    {
        ListaDobleEmpleados listaEmpleados = new ListaDobleEmpleados();
        while (true)
        {
            Console.WriteLine("\n--- Gestión de Empleados ---");
            Console.WriteLine("1. Agregar Empleado");
            Console.WriteLine("2. Buscar Empleado");
            Console.WriteLine("3. Eliminar Empleado");
            Console.WriteLine("4. Mostrar Empleados");
            Console.WriteLine("5. Ordenar por Nombre");
            Console.WriteLine("6. Ordenar por Salario");
            Console.WriteLine("7. Estadísticas (Submenú)");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");
            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el nombre: ");
                    string nombre = Console.ReadLine() ?? "";
                    Console.Write("Ingrese el ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("ID inválido.");
                        break;
                    }
                    Console.Write("Ingrese el salario: ");
                    if (!double.TryParse(Console.ReadLine(), out double salario))
                    {
                        Console.WriteLine("Salario inválido.");
                        break;
                    }
                    listaEmpleados.InsertarEmpleado(new Empleado(nombre, id, salario));
                    break;

                case "2":
                    Console.Write("Ingrese el ID del empleado a buscar: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        Empleado? encontrado = listaEmpleados.BuscarEmpleado(id);
                        Console.WriteLine(encontrado != null
                            ? $"Empleado encontrado: {encontrado.Nombre}, Salario: {encontrado.Salario:C}"
                            : "Empleado no encontrado.");
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                    break;

                case "3":
                    Console.Write("Ingrese el ID del empleado a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        listaEmpleados.EliminarEmpleado(id);
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                    break;

                case "4":
                    listaEmpleados.ImprimirLista();
                    break;

                case "5":
                    listaEmpleados.OrdenarPorNombre();
                    break;

                case "6":
                    listaEmpleados.OrdenarPorSalario();
                    break;

                case "7":
                    SubmenuEstadisticas(listaEmpleados);
                    break;

                case "8":
                    return;

                default:
                    Console.WriteLine("Opción no válida, intente de nuevo.");
                    break;
            }
        }
    }

    // Submenú de Estadísticas
    static void SubmenuEstadisticas(ListaDobleEmpleados lista)
    {
        while (true)
        {
            Console.WriteLine("\n--- Submenú Estadísticas ---");
            Console.WriteLine("1. Salario Promedio");
            Console.WriteLine("2. Salario Máximo");
            Console.WriteLine("3. Salario Mínimo");
            Console.WriteLine("4. Mediana de Salarios");
            Console.WriteLine("5. Volver al Menú Principal");
            Console.Write("Seleccione una opción: ");
            string? opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    double prom = lista.CalcularPromedioSalario();
                    Console.WriteLine($"Salario Promedio: {prom:C}");
                    break;

                case "2":
                    Empleado? max = lista.EncontrarSalarioMaximo();
                    if (max != null)
                        Console.WriteLine($"Salario Máximo: {max.Salario:C} (ID: {max.Id}, Nombre: {max.Nombre})");
                    else
                        Console.WriteLine("No hay empleados.");
                    break;

                case "3":
                    Empleado? min = lista.EncontrarSalarioMinimo();
                    if (min != null)
                        Console.WriteLine($"Salario Mínimo: {min.Salario:C} (ID: {min.Id}, Nombre: {min.Nombre})");
                    else
                        Console.WriteLine("No hay empleados.");
                    break;

                case "4":
                    double mediana = lista.ObtenerMedianaSalario();
                    Console.WriteLine($"Mediana de Salarios: {mediana:C}");
                    break;

                case "5":
                    return; // volver al menú principal

                default:
                    Console.WriteLine("Opción de estadística no válida, intente de nuevo.");
                    break;
            }
        }
    }
}