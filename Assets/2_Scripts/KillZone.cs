using UnityEngine;

public class KillZone : MonoBehaviour
{
    [Tooltip("특별 엔딩을 발생시키는 태그")]
    public string specialEndingTag = "SpecialEnding";

    [TextArea]
    public string specialEndingMessage = "축하합니다! 숨겨진 엔딩에 도달했습니다!";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1) 먼저 게임오버 패널 띄우기
        GameManager.Instance.GameStop();

        // 2) 만약 닿은 오브젝트에 specialEndingTag가 붙어있다면
        if (gameObject.CompareTag(specialEndingTag))
        {
            // 엔딩 메시지 노출
            UIManager.Instance.ShowEndingMessage(specialEndingMessage);
        }
    }
}
