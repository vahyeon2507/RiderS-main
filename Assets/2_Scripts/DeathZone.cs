using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어만 죽게 하고 싶다면 태그 비교
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameStop();
        }
    }

    // 트리거로 처리한다면 이쪽 사용
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameStop();
        }
    }
}
