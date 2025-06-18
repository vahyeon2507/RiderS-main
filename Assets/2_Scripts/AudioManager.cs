using UnityEngine;


public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }

    public AudioSource bgmSource;

    public AudioClip gameplayMusic;

    void Start()
    {
        AudioManager.Instance.PlayBGM(gameplayMusic, 0.5f);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �ٲ� ��Ƴ���
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        if (bgmSource.clip == clip) return; // �̹� ��� ���̸� ����

        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void SetVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
