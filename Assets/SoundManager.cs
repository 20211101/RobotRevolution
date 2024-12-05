using UnityEngine;



public class SoundManager : MonoBehaviour
{
    static SoundManager _instance;
    public static SoundManager instance { get => _instance; }
    public AudioClip BGM;
    public AudioClip Lager;
    public AudioClip UpgradeSound;
    public AudioClip FirePlatform;

    AudioSource soundSource;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        soundSource = GetComponent<AudioSource>();
    }

    public void PlayShooting()
    {
        soundSource.PlayOneShot(Lager, 0.2f);
    }
    public void PlayUpgradeSound()
    {
        soundSource.PlayOneShot(UpgradeSound, 0.2f);
    }
    public void PlayFirePlatform()
    {
        soundSource.PlayOneShot(FirePlatform, 0.2f);
    }
}
