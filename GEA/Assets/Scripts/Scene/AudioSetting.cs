using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    public static AudioSettings Instance;

    public AudioMixer mixer;

    const string BGM_KEY = "BGMVolume";
    const string SFX_KEY = "SFXVolume";

    void Awake()
    {
        // 싱글톤
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 저장된 값 불러오기
        SetBGMVolume(PlayerPrefs.GetFloat(BGM_KEY, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_KEY, 1f));
    }

    public void SetBGMVolume(float value)
    {
        mixer.SetFloat(BGM_KEY, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(BGM_KEY, value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat(SFX_KEY, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(SFX_KEY, value);
    }
}
