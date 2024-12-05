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
        soundSource.PlayOneShot(Lager);
    }
    public void PlayUpgradeSound()
    {
        soundSource.PlayOneShot(UpgradeSound);
    }
    public void PlayFirePlatform()
    {
        soundSource.PlayOneShot(FirePlatform);
    }
}
