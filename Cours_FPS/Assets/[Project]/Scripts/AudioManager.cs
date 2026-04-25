using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource _audioSourcePrefab;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void PlayFX(AudioClip clip, Vector3 worldPos)
    {
        if (!clip) return;
        AudioSource source = Instantiate(_audioSourcePrefab, worldPos, Quaternion.identity);
        source.clip = clip;
        source.Play();
        Destroy(source, clip.length);
    }
}