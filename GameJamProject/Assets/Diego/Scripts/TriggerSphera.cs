using UnityEngine;

public class TriggerSphera : MonoBehaviour
{
    private Sphere_ Esfera;
    private CameraFolllow Camera_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Esfera = FindAnyObjectByType<Sphere_>();
        Camera_=FindAnyObjectByType<CameraFolllow>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            Esfera.Empezar();
            Camera_.PlayerIn=false;
            }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Esfera.Acabar();
            Camera_.PlayerIn = true;

        }
    }
}
