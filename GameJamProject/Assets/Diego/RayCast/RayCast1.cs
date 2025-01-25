using System.Security.Cryptography;
using UnityEngine;

public class RayCast1 : MonoBehaviour
{
    public GameObject[] objects; // Los objetos que lanzarán los raycasts
    public float[] objectBrushRadii; // Radios de pincel para cada objeto
    public Color defaultColor = Color.white; // Color base del plano
    public Color paintColor = Color.red; // Color de pintura
    public int textureSize = 256; // Tamaño de la textura
    public GameObject Trigger1,Trigger2,Trigger3, Trigger4, Trigger5;
    public bool parar;

    private Texture2D texture;
    private Renderer planeRenderer;

    void Start()
    {
        // Crear una nueva textura para el plano
        texture = new Texture2D(textureSize, textureSize);
        planeRenderer = GetComponent<Renderer>();

        // Inicializar la textura con el color predeterminado
        ClearTexture();
        planeRenderer.material.mainTexture = texture;
        parar = true;
        //FuntionActive();
        Invoke("PararFuntion", 1);

        if (Trigger1 != null)
        {
            var triggerNotifier = Trigger1.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger;
            }
        }
        if (Trigger2 != null)
        {
            var triggerNotifier = Trigger2.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger1;
            }
        }
        if (Trigger3 != null)
        {
            var triggerNotifier = Trigger3.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger2;
            }
        }
        if (Trigger4 != null)
        {
            var triggerNotifier = Trigger4.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger3;
            }
        }
        if (Trigger5 != null)
        {
            var triggerNotifier = Trigger5.GetComponent<TriggerNotifier>();
            if (triggerNotifier != null)
            {
                triggerNotifier.OnPlayerEnterTrigger += DoSomethingOnCustomTrigger4;
            }
        }

    }
    void DoSomethingOnCustomTrigger()
    {
        objectBrushRadii[0] = 100.0f;
        Debug.Log("Acción personalizada ejecutada en el trigger externo.");
        // Aquí puedes agregar la lógica adicional que desees
    }
    void DoSomethingOnCustomTrigger1()
    {
        objectBrushRadii[1] = 100.0f;
        Debug.Log("AH");
        // Aquí puedes agregar la lógica adicional que desees
    }
    void DoSomethingOnCustomTrigger2()
    {
        objectBrushRadii[2] = 100.0f;
        Debug.Log("AH");
        // Aquí puedes agregar la lógica adicional que desees
    }
    void DoSomethingOnCustomTrigger3()
    {
        objectBrushRadii[3] = 100.0f;
        Debug.Log("AH");
        // Aquí puedes agregar la lógica adicional que desees
    }
    void DoSomethingOnCustomTrigger4()
    {
        objectBrushRadii[4] = 100.0f;
        Debug.Log("AH");
        // Aquí puedes agregar la lógica adicional que desees
    }


    void Update()
    {
        FuntionActive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Trigger1)
        {
            Debug.Log("El jugador ha activado el trigger externo!");

            // Lógica adicional que deseas ejecutar cuando el trigger se activa
        }
    }
    public void FuntionActive()
    {
        
        
            // Limpiar la textura al inicio de cada frame
            ClearTexture();

            // Lanzar raycasts desde cada objeto
            for (int i = 0; i < objects.Length; i++)
            {
                var obj = objects[i];
                if (obj == null) continue;

                // Obtener el radio correspondiente al objeto
                float brushRadius = objectBrushRadii[i];

                Ray ray = new Ray(obj.transform.position, Vector3.down);
                Debug.DrawRay(obj.transform.position, Vector3.down * 10f, Color.green); // Dibuja el raycast

                RaycastHit[] hits = Physics.RaycastAll(ray);
                foreach (var hit in hits)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log($"El jugador ha pasado por el raycast del objeto: {obj.name}");

                        // Incrementar el radio del pincel para este raycast
                        //objectBrushRadii[i] += 0.01f; // Incremento, ajusta este valor según sea necesario
                        Debug.Log($"Nuevo radio del pincel para {obj.name}: {objectBrushRadii[i]}");
                    }

                    if (hit.collider.gameObject == gameObject) // Asegurar que impacta el plano
                    {
                        Vector2 textureCoord = hit.textureCoord; // Coordenadas UV
                        PaintOnTexture(textureCoord, brushRadius); // Usar radio específico
                    }
                }
            }

            // Aplicar los cambios a la textura
            texture.Apply();
        
    }

    public void PararFuntion() 
    {
        parar = false;
    }

    void ClearTexture()
    {
        // Rellenar toda la textura con el color predeterminado
        Color[] pixels = new Color[textureSize * textureSize];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = defaultColor;
        }
        texture.SetPixels(pixels);
    }

    void PaintOnTexture(Vector2 uv, float brushRadius)
    {
        int x = Mathf.RoundToInt(uv.x * textureSize);
        int y = Mathf.RoundToInt(uv.y * textureSize);

        for (int i = -Mathf.CeilToInt(brushRadius); i <= Mathf.CeilToInt(brushRadius); i++)
        {
            for (int j = -Mathf.CeilToInt(brushRadius); j <= Mathf.CeilToInt(brushRadius); j++)
            {
                int px = x + i;
                int py = y + j;

                if (px >= 0 && px < textureSize && py >= 0 && py < textureSize)
                {
                    float distance = Mathf.Sqrt(i * i + j * j);
                    if (distance <= brushRadius)
                    {
                        float intensity = 1 - (distance / brushRadius);
                        Color currentColor = texture.GetPixel(px, py);
                        Color blendedColor = Color.Lerp(currentColor, paintColor, intensity);
                        texture.SetPixel(px, py, blendedColor);
                    }
                }
            }
        }
    }
}