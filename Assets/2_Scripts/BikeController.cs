using UnityEngine;

public class BikeController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb;
    public float moveForce = 10f;
    public float maxSpeed = 20f;
    public float maxAngularVelocity = 300f;
    public float rotateForce = 5f;

    [Header("Particles (Visual Only)")]
    public ParticleSystem groundParticles;
    public Vector3 particleOffset;       // 연기 위치 보정용

    [Header("Collision Filtering")]
    public LayerMask ignoreCollisionLayers;

    private bool isGrounded;

    void Start()
    {
        // 1) 물리에서 완전 분리: 부모 끊기
        groundParticles.transform.SetParent(null);

        // 2) 월드 좌표에서 시뮬레이션
        var main = groundParticles.main;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        // 3) 혹시 켜져 있을지 모르는 파티클 충돌 모듈 끄기
        var coll = groundParticles.collision;
        coll.enabled = false;

        // 무게중심 앞쪽으로 당기기 (기존 설정)
        rb.centerOfMass = new Vector2(0f, -0.4f);
    }

    void LateUpdate()
    {
        // 4) 연기 위치만 차 위치 따라다니게
        groundParticles.transform.position = transform.position + particleOffset;
    }

    void Update()
    {
        float speed = rb.linearVelocity.magnitude;
        UIManager.Instance.UpdateCarSpeedText($"Car Speed : {speed:F2} m/s");

        // 이젠 땅에 닿을 때만 연기 재생/정지
        if (isGrounded && speed > 3f)
        {
            if (!groundParticles.isPlaying) groundParticles.Play();
        }
        else
        {
            if (groundParticles.isPlaying) groundParticles.Stop();
        }

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
        if ((ignoreCollisionLayers.value & (1 << collision.gameObject.layer)) != 0)
            return;
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if ((ignoreCollisionLayers.value & (1 << collision.gameObject.layer)) != 0)
            return;
        isGrounded = false;
    }
}
