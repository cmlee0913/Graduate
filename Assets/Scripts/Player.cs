using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject Pivot;
    private bool isMove = false;
    private Rigidbody playerRigid;
    public bool isJumping = false;

    private void Start()
    {
        Pivot = GameObject.FindWithTag("Pivot");
        playerRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerJump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        float Speed = 5f;

        /* 대쉬 유무 판별 */
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            Speed = 10f;
        }
        else if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            Speed = 5f;
        }
        /* 방향키 입력 유무 판별 */
        if (Horizontal != 0 || Vertical != 0)
        {
            isMove = true;
        }

        /* 카메라 스팟의 전후방 좌우 방향을 기준 */
        Vector3 moveVertical = new Vector3(Pivot.transform.forward.x,
                                           0, // 이동 방향을 2차원적으로 고정 시키기 위해
                                           Pivot.transform.forward.z).normalized;
        Vector3 moveHorizontal = new Vector3(Pivot.transform.right.x,
                                             0, // 이동 방향을 2차원적으로 고정 시키기 위해
                                             Pivot.transform.right.z).normalized;
        Vector3 move = moveVertical * Vertical + moveHorizontal * Horizontal;

        if (isMove == true)
        {
            transform.forward = move; // 플레이어의 시선 방향이 방향키 입력 방향과 일치하도록 회전
            transform.position += move * Time.deltaTime * Speed; // 키보드 입력 시 camSpot의 전방 또는 좌우으로 이동
            isMove = false; // 시선 방향 고정
        }
    }
    private void PlayerJump()
    {
        float jumpPower = 2f;

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            if (isJumping == false)
            {
                playerRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                isJumping = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            isJumping = false;
        }
    }
}
