using UnityEngine;

public class TriggerFinal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private RayCast1 RayCS;
    public GameObject CanvasBueno, CanvasMalo;
    void Start()
    {
        RayCS= FindAnyObjectByType<RayCast1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (RayCS.FinalBueno == true&& other.gameObject.tag == "Player") 
        {
            CanvasBueno.SetActive(true);
            Debug.Log("Bueno");
        }
        if (RayCS.FinalMalo == true && other.gameObject.tag == "Player")
        {
            CanvasMalo.SetActive(true);
            Debug.Log("Malo");

        }
    }
}
