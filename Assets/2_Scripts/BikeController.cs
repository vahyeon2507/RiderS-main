using UnityEngine;

public class BikeController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveForce = 10f;
    public float rotateForce = 5f;

    private bool isGrounded;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                // 땅 위: 앞으로 힘
                rb.AddForce(transform.right * moveForce, ForceMode2D.Force);
            }
            else
            {
                // 공중: 회전
                rb.AddTorque(rotateForce, ForceMode2D.Force); // 시계방향은 마이너스
            }
        }
    }

    // 바닥 감지
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
