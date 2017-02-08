using UnityEngine;
using System.Collections;

public class CloudsMovement : MonoBehaviour
{

    public float speed;
    public bool moveDX;
    public bool moveSX;

    public Transform sxTrigger;
    public Transform dxTrigger;
    private Vector3 startPos;

    void Start ()
    {
        sxTrigger = GetComponent<Transform>();
        dxTrigger = GetComponent<Transform>();
        startPos = new Vector3(transform.position.x, this.transform.position.y, 90);
    }

    void Update ()
    {

        if (moveDX)
        {
            this.transform.Translate(-Vector3.right * Time.deltaTime * speed);
        }


        if (moveSX)
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.transform.position.x == dxTrigger.transform.position.x)
        {
            transform.position = startPos;
        }

        if (this.transform.position.x == sxTrigger.transform.position.x)
        {
            transform.position = startPos;
        }


    }


}
