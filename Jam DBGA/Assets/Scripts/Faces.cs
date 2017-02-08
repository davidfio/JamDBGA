using UnityEngine;
using System.Collections;

public class Faces : MonoBehaviour {

    public Transform faceToScale;
    public GameObject face_1;
    public GameObject face_2;
    public GameObject face_3;
    public GameObject face_4;


    void Start ()
    {
        faceToScale = GetComponent<Transform>();
	}
	
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            faceToScale.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);           
        }     


        // Set next face
        if (faceToScale.localScale.x > 1.5 && faceToScale.localScale.y > 1.5 && faceToScale.localScale.z > 1.5)
        {
            face_1.SetActive(false);
            face_2.SetActive(true);
        }

        if (faceToScale.localScale.x > 2 && faceToScale.localScale.y > 2 && faceToScale.localScale.z > 2)
        {
            face_2.SetActive(false);
            face_3.SetActive(true);
        }

        if (faceToScale.localScale.x > 3 && faceToScale.localScale.y > 2 && faceToScale.localScale.z > 3)
        {
            face_3.SetActive(false);
            face_4.SetActive(true);
        }       

    }
}
