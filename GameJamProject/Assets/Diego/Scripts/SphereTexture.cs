using UnityEngine;

public class SphereTexture : MonoBehaviour
{
    public  GameObject[] objects; // Los objetos que lanzarán los raycasts
    public float[] objectBrushRadii; // Radios de pincel para cada objeto
    public Color defaultColor = Color.white; // Color base del plano
    public Color paintColor = Color.red; // Color de pintura
    public int textureSize = 256; // Tamaño de la textura

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

        //FuntionActive();


    }

    void Update()
    {
        //FuntionActive();
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
                    objectBrushRadii[i] += 10f; // Incremento, ajusta este valor según sea necesario
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