using UnityEngine;

public class Catcher : MonoBehaviour
{
    public float speed = 15f;
    public float limite = 15f; 

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * move * speed * Time.deltaTime);


        float x = Mathf.Clamp(transform.position.x, -limite, limite);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}