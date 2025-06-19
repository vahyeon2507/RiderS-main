using UnityEngine;

// 파티클 오브젝트에 달아서 'target' 위치만 따라다니게 해준다
public class ParticleFollower : MonoBehaviour
{
    [Tooltip("따라가고 싶은 차 Transform")]
    public Transform target;

    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("ParticleFollower: target이 할당되지 않았습니다.");
            enabled = false;
            return;
        }
        // 최초 위치 차와의 상대 오프셋 계산
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // 차 위치 + 오프셋 만큼만 이동
        transform.position = target.position + offset;
    }
}
