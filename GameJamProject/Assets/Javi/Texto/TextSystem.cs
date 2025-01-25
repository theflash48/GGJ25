using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class TextSystem : MonoBehaviour
{
    public float speed;

    public string fullText;
    private string currentText = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShowText());
    }

    public IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(speed);
        }
    }
}
