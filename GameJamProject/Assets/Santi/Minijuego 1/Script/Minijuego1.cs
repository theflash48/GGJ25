using UnityEngine;
using System.Collections.Generic;

public class Minijuego1 : MonoBehaviour
{
    private enum Turn { None, Player, Computer }
    private Turn currentTurn = Turn.None;

    public GameObject[] cubos;          // Referencia a los cubos (0-9)
    private int targetNumber;           // Número objetivo (aleatorio entre 0 y 9)
    private HashSet<int> selectedNumbers = new HashSet<int>(); // Números seleccionados
    private bool gameActive = true;     // Indica si el juego está activo

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (gameActive && currentTurn == Turn.Player)
        {
            HandlePlayerTurn();
        }
    }

    // ========================
    // Iniciar el Juego
    // ========================
    void StartGame()
    {
        targetNumber = Random.Range(0, 10); // Generar un número aleatorio entre 0 y 9
        Debug.Log($"Número objetivo (secreto): {targetNumber}"); // Solo para pruebas

        currentTurn = Turn.Player;      // El jugador comienza
        selectedNumbers.Clear();        // Limpiar los números seleccionados
        InitializeCubes();              // Activar y preparar los cubos
        gameActive = true;              // Activar el juego
    }

    // ========================
    // Turno del Jugador
    // ========================
    void HandlePlayerTurn()
    {
        int number = CheckKeyInput();

        if (number != -1 && !selectedNumbers.Contains(number)) // Si selecciona un número válido
        {
            Debug.Log($"Jugador seleccionó: {number}");
            selectedNumbers.Add(number);

            if (number == targetNumber)
            {
                Debug.Log("¡Correcto! El jugador adivinó el número. ¡Ganaste!");
                for (int i = 0; i < cubos.Length; i++)
                {
                    if (i != number)
                    {
                        DesactivarCubo(i);
                    }
                }
                EndGame();
            }
            else
            {
                Debug.Log("Número incorrecto. Turno de la computadora...");
                DesactivarCubo(number);
                currentTurn = Turn.Computer;
                Invoke("HandleComputerTurn", 1.0f); // Espera un segundo antes del turno de la computadora
            }
        }
        else if (number != -1)
        {
            Debug.Log($"El número {number} ya fue seleccionado. Intenta con otro.");
        }
    }

    // ======================== 
    // Turno de la Computadora
    // ========================
    void HandleComputerTurn()
    {
        if (!gameActive) return;

        if (selectedNumbers.Count < cubos.Length) // Si aún hay cubos disponibles
        {
            // Buscar números no seleccionados
            List<int> numerosDisponibles = new List<int>();
            for (int i = 0; i < cubos.Length; i++)
            {
                if (!selectedNumbers.Contains(i))
                {
                    numerosDisponibles.Add(i);
                }
            }

            // Seleccionar un número aleatorio
            int numeroSeleccionado = numerosDisponibles[Random.Range(0, numerosDisponibles.Count)];
            Debug.Log($"Computadora seleccionó: {numeroSeleccionado}");
            selectedNumbers.Add(numeroSeleccionado);

            if (numeroSeleccionado == targetNumber)
            {
                Debug.Log("¡La computadora adivinó el número! ¡Perdiste!");
                for (int i = 0; i < cubos.Length; i++)
                {
                    if (i != numeroSeleccionado)
                    {
                        DesactivarCubo(i);
                    }
                }
                EndGame();
            
            }
            else
            {
                Debug.Log("La computadora falló. Turno del jugador...");
                DesactivarCubo(numeroSeleccionado);
                currentTurn = Turn.Player;
            }
        }
        else
        {
            Debug.Log("No hay más números disponibles. ¡Empate!");
            EndGame();
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
                return i;
        }
        return -1;
    }

    void InitializeCubes()
    {
        foreach (GameObject cubo in cubos)
        {
            cubo.SetActive(true); // Activar todos los cubos al inicio del juego
        }
    }

    void DesactivarCubo(int index)
    {
        if (index >= 0 && index < cubos.Length)
        {
            cubos[index].SetActive(false); // Desactivar el cubo seleccionado
        }
    }

    void EndGame()
    {
        Debug.Log("El juego ha terminado.");
        gameActive = false;      // Desactivar el juego
        currentTurn = Turn.None; // Detener los turnos
    }
}
