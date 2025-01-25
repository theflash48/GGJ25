using UnityEngine;

public class TriggerNotifier : MonoBehaviour
{
    // Delegado para eventos personalizados
    public delegate void TriggerEvent();
    public event TriggerEvent OnPlayerEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha activado el trigger externo.");

            // Notificar a los suscriptores que se activó el trigger
            OnPlayerEnterTrigger?.Invoke();
        }
    }
}
