using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Transform circleCenter;
    public float radius = 30f; 
    public Renderer planeRenderer; 
    public Color circleColor = Color.red; 
    public Color outsideColor = Color.green; 

    private Texture2D texture;

    void Start()
    {
        // Crear una textura dinámica para el plano
        texture = new Texture2D(256, 256);
        planeRenderer.material.mainTexture = texture;
        UpdateTexture();
    }

    void Update()
    {
        UpdateTexture();
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag =="Suelo") {
            UpdateTexture();
            Debug.Log("AA");
        } */
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

    void UpdateTexture()
    {

        Vector2 centerUV = WorldToUV(circleCenter.position, planeRenderer);

        
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                
                Vector2 pixelUV = new Vector2((float)x / texture.width, (float)y / texture.height);

               
                float distance = Vector2.Distance(centerUV, pixelUV);

                if (distance <= radius / 10f) //tamaño del plano
                {
                    texture.SetPixel(x, y, circleColor);
                }
                else
                {
                    texture.SetPixel(x, y, outsideColor);
                }
               
            }
        }

        texture.Apply();
    }

    Vector2 WorldToUV(Vector3 worldPosition, Renderer renderer)
    {
        Vector3 localPos = renderer.transform.InverseTransformPoint(worldPosition);
        
        Vector2 uv = new Vector2(
            (localPos.x / renderer.bounds.size.x) + 0.5f,
            (localPos.z / renderer.bounds.size.z) + 0.5f
        );

        
        uv.x = 1f - uv.x; 
        uv.y = 1f - uv.y; 

        return uv;
    }
}
