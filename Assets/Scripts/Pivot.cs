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

    /*플레이어 머리 위로 위치 고정*/
    private void Pos()
    {
        transform.position = Player.transform.position + (Vector3.up * 1.5f);
    }

    private void Rotate()
    {
        Vector3 Angles = transform.rotation.eulerAngles;
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");
        float eulerAnglesX = Angles.x - MouseY; // 최대 카메라 각도 설정을 위한 변수

        /* 회전 각도 최대 범위 설정 */
        if (eulerAnglesX < 180)
        {
            eulerAnglesX = Mathf.Clamp(eulerAnglesX, -1, 70); // -1도의 이유는 수평 부근에서의 멈춤을 막기위해
        }
        else if (eulerAnglesX >= 180)
        {
            eulerAnglesX = Mathf.Clamp(eulerAnglesX, 340, 361); // 1도의 이유는 위와 같음
        }

        //transform.eulerAngles += new Vector3(-MouseY, MouseX, 0) * 3f;

        /* 마우스 움직임에 따라 CamSpot 회전 값 변경 */
        transform.rotation = Quaternion.Euler(eulerAnglesX, Angles.y + MouseX, Angles.z);
    }
}
