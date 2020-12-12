using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Pos();
        Rotate();
    }

    private void Pos()
    {
        transform.position = Player.transform.position + (Vector3.up * 1.5f);
    }

    private void Rotate()
    {
        Vector3 Angles = transform.rotation.eulerAngles;
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");
        float eulerAnglesX = Angles.x - MouseY;

        if (eulerAnglesX < 180)
        {
            eulerAnglesX = Mathf.Clamp(eulerAnglesX, -1, 70);
        }
        else if (eulerAnglesX >= 180)
        {
            eulerAnglesX = Mathf.Clamp(eulerAnglesX, 340, 361);
        }

        //transform.eulerAngles += new Vector3(-MouseY, MouseX, 0) * 3f;
        transform.rotation = Quaternion.Euler(eulerAnglesX, Angles.y + MouseX, Angles.z);
    }
}
