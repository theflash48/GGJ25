using UnityEngine;

public class Triigger1 : MonoBehaviour
{
    private SphereTexture SphereTexture_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SphereTexture_ = FindAnyObjectByType<SphereTexture>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Player")
        {
            SphereTexture_.objectBrushRadii[0] += 10f; // Incremento, ajusta este valor según sea necesario
            //Debug.Log($"Nuevo radio del pincel para {obj.name}: {objectBrushRadii[i]}");

        }*/
    }
}
