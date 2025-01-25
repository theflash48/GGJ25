using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int bubbleType; // Tipo de burbuja (0 para burbujas válidas)

    void OnMouseDown()
    {
        GameManager.Instance.CheckBubble(this); // Verifica si es la burbuja correcta
        Destroy(gameObject); // Destruye la burbuja
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) // Si toca el suelo
        {
            GameManager.Instance.EndLevel(false); // Pierdes el nivel
            Destroy(gameObject); // Destruye la burbuja
        }
    }
}

