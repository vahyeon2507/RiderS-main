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
                // �� ��: ������ ��
                rb.AddForce(transform.right * moveForce, ForceMode2D.Force);
            }
            else
            {
                // ����: ȸ��
                rb.AddTorque(rotateForce, ForceMode2D.Force); // �ð������ ���̳ʽ�
            }
        }
    }

    // �ٴ� ����
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
