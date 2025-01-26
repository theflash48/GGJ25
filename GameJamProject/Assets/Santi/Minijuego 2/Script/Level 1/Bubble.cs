using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int bubbleType; // Tipo de burbuja (0 para burbujas válidas)
    public CharacterController characterController; // Controlador del personaje
    public float speed; // Velocidad de la burbuja
    void OnMouseDown()
    {
        GameManager.Instance.CheckBubble(this); // Verifica si es la burbuja correcta
        Destroy(gameObject); // Destruye la burbuja
    }
    private void OnCollisionEnter(Collision collisionn)
    {
        if (collisionn.gameObject.tag == "SueloMinijuego") // Si toca el suelo
        {
            Debug.LogWarning("Estoy");
            GameManager.Instance.EndLevel(true); // Pierdes el nivel
            Destroy(gameObject); // Destruye la burbuja
            Debug.Log("Cambiando de escena");
        }
    }
    void Start()
    {
        characterController= GetComponent<CharacterController>(); // Obtiene el controlador del personaje
    }
    void Update()
    {
        characterController.Move(Vector2.down * speed * Time.deltaTime); // Mueve la burbuja hacia abajo
    }
}

