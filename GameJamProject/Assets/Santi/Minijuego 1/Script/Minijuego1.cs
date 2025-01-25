using UnityEngine;
using System.Collections.Generic;

public class Minijuego1 : MonoBehaviour
{
    private enum Turn { None, Player, Computer }
    private Turn currentTurn = Turn.None;
    private int targetNumber;           // Número aleatorio generado por el host (entre 0 y 9)
    private int currentIndex = 0;       // Índice de búsqueda lineal de la computadora

    public GameObject[] cubos;          // Referencia a los cubos (0-9)
    private Color colorVerde = Color.green;
    private Color colorRojo = Color.red;
    private Color colorAmarillo = Color.yellow;
    private Color colorBlanco = Color.white; // Color inicial de los cubos

    private HashSet<int> selectedNumbers = new HashSet<int>(); // Números ya seleccionados
    private bool gameActive = true;     // Bandera para controlar si el juego está activo

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (gameActive && currentTurn == Turn.Player) // Verifica si el juego está activo antes del turno del jugador
        {
            HandlePlayerTurn();
        }
    }

    // ========================
    // Iniciar el Juego
    // ========================
    void StartGame()
    {
        targetNumber = GenerateRandomNumber(); // Generar un número aleatorio entre 0 y 9
        Debug.Log($"El número objetivo (secreto) es: {targetNumber}"); // Solo para pruebas
        Debug.Log("El primero que adivine el número gana.");

        currentTurn = Turn.Player; // El jugador comienza
        currentIndex = 0;          // Reiniciar el índice de la computadora
        selectedNumbers.Clear();   // Limpiar los números seleccionados
        InitializeCubes();         // Inicializar y mostrar los cubos
        gameActive = true;         // Activar el juego
    }

    // ========================
    // Generar un Número Aleatorio
    // ========================
    int GenerateRandomNumber()
    {
        int randomNumber = Random.Range(0, 10);
        Debug.Assert(randomNumber >= 0 && randomNumber <= 9, "El número generado está fuera del rango 0-9.");
        return randomNumber;
    }

    // ========================
    // Turno del Jugador
    // ========================
    void HandlePlayerTurn()
    {
        if (!gameActive) return; // Salir si el juego ha terminado

        int number = CheckKeyInput();
        if (number != -1 && !selectedNumbers.Contains(number)) // Si el jugador selecciona un número válido y no repetido
        {
            Debug.Log($"El jugador seleccionó: {number}");

            selectedNumbers.Add(number); // Registrar el número como seleccionado

            // Verificar si el número coincide con el objetivo
            if (number == targetNumber)
            {
                CambiarColorCubo(number, colorVerde);
                Debug.Log("¡Correcto! El jugador adivinó el número. ¡Ganaste!");
                EndGame(); // Terminar el juego
            }
            else
            {
                CambiarColorCubo(number, colorRojo);
                Debug.Log("Número incorrecto. Turno de la computadora...");
                currentTurn = Turn.Computer;
                Invoke("HandleComputerTurn", 1.0f); // Retraso antes del turno de la computadora
            }

            DesactivarCubo(number); // Desactivar el cubo seleccionado
        }
        else if (number != -1)
        {
            Debug.Log($"El número {number} ya fue seleccionado. Intenta con otro número.");
        }
    }

    // ======================== 
    // Turno de la Computadora
    // ========================
    void HandleComputerTurn()
    {
        if (!gameActive) return; // Salir si el juego ha terminado

        // Asegurarse de que hay números disponibles
        if (selectedNumbers.Count < cubos.Length) // Comparar seleccionados con el total de cubos
        {
            // Crear una lista de números no seleccionados
            List<int> numerosDisponibles = new List<int>();
            for (int i = 0; i < cubos.Length; i++)
            {
                if (!selectedNumbers.Contains(i)) // Agregar números que no han sido seleccionados
                {
                    numerosDisponibles.Add(i);
                }
            }

            // Seleccionar un número aleatorio de la lista
            int indiceAleatorio = UnityEngine.Random.Range(0, numerosDisponibles.Count);
            int numeroSeleccionado = numerosDisponibles[indiceAleatorio];

            Debug.Log($"La computadora seleccionó: {numeroSeleccionado}");

            selectedNumbers.Add(numeroSeleccionado); // Registrar el número como seleccionado

            // Verificar si el número coincide con el objetivo
            if (numeroSeleccionado == targetNumber)
            {
                CambiarColorCubo(numeroSeleccionado, colorAmarillo);
                Debug.Log("¡La computadora adivinó el número! ¡Perdiste!");
                EndGame(); // Terminar el juego
            }
            else
            {
                CambiarColorCubo(numeroSeleccionado, colorRojo);
                Debug.Log("La computadora falló. Turno del jugador...");
                currentTurn = Turn.Player; // Vuelve al turno del jugador
            }

            DesactivarCubo(numeroSeleccionado); // Desactivar el cubo seleccionado
        }
        else
        {
            Debug.Log("No hay más números disponibles. ¡Empate!");
            EndGame(); // Terminar el juego
        }
    }

    // ========================
    // Métodos Auxiliares
    // ========================
    int CheckKeyInput()
    {
        for (int i = 0; i <= 9; i++) // Detectar teclas numéricas
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) || Input.GetKeyDown(KeyCode.Keypad0 + i))
                return i; // Retorna el número presionado
        }
        return -1; // Ninguna tecla presionada
    }

    void CambiarColorCubo(int index, Color color)
    {
        if (index >= 0 && index < cubos.Length)
        {
            Renderer renderer = cubos[index].GetComponent<Renderer>();
            if (renderer != null)
                renderer.material.color = color;
        }
    }

    void InitializeCubes()
    {
        for (int i = 0; i < cubos.Length; i++)
        {
            cubos[i].SetActive(true); // Asegurarse de que todos los cubos estén activos
            CambiarColorCubo(i, colorBlanco); // Establecer el color inicial de los cubos a blanco
        }
    }

    void DesactivarCubo(int index)
    {
        if (index >= 0 && index < cubos.Length)
        {
            cubos[index].SetActive(false); // Desactivar el cubo para que no pueda seleccionarse nuevamente
        }
    }

    void EndGame()
    {
        Debug.Log("El juego ha terminado.");
        currentTurn = Turn.None; // Detener el flujo del juego
        gameActive = false;      // Desactivar el juego
    }
}
