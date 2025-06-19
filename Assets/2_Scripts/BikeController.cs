using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float moveForce = 10f;
    public float maxSpeed = 20f;
    public float maxAngularVelocity = 300f;
    public float rotateForce = 5f;

    [Header("Crash (Flip)")]
    public ParticleSystem crashParticles;   // 터질 때 나올 파티클
    public AudioClip crashSound;            // 터질 때 나올 효과음
    public float crashDelay = 2f;           // 몇 초 후에 게임오버
    public string groundTag = "Ground";     
    public string blueGroundTag = "BlueGround";

    private AudioSource audioSource;
    private bool isGrounded;
    private bool isCrashed;

    void Start()
    {
        rb.centerOfMass = new Vector2(0f, -0.4f);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isCrashed) return;  // 크래시 시 컨트롤 잠금

        float speed = rb.linearVelocity.magnitude;
        UIManager.Instance.UpdateCarSpeedText($"Car Speed : {speed:F2} m/s");

        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                if (speed < maxSpeed)
                    rb.AddForce(transform.right * moveForce, ForceMode2D.Force);
            }
            else
            {
                if (Mathf.Abs(rb.angularVelocity) < maxAngularVelocity)
                    rb.AddTorque(rotateForce, ForceMode2D.Force);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;

        // BlueGround과 Ground 모두 바닥으로 처리
        if (tag == groundTag || tag == blueGroundTag)
            isGrounded = true;

        // 오직 일반 Ground와 닿았을 때, 뒤집혀 있으면 크래시
        if (!isCrashed && tag == groundTag)
        {
            // transform.up이 아래(Vector2.down)를 얼마나 바라보고 있는지
            float upDot = Vector2.Dot(transform.up, Vector2.down);
            if (upDot > 0.5f)  // 대략 120° 이상 뒤집힌 상태
            {
                StartCoroutine(CrashSequence());
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        if (tag == groundTag || tag == blueGroundTag)
            isGrounded = false;
    }

    private IEnumerator CrashSequence()
    {
        isCrashed = true;
        // 1) 모션 멈춤
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        // 2) 파티클 & 사운드
        if (crashParticles != null)
        {
            crashParticles.transform.position = transform.position;
            crashParticles.Play();
        }
        if (crashSound != null)
        {
            audioSource.PlayOneShot(crashSound);
        }
        // 3) 잠시 기다렸다가 게임오버
        yield return new WaitForSeconds(crashDelay);
        GameManager.Instance.GameStop();
    }
}
