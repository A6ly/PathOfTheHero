using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static Define;

public class SoundManager
{
    AudioSource[] audioSources = new AudioSource[(int)SoundType.Max];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject soundRoot = GameObject.Find("@SoundRoot");

        if (soundRoot == null)
        {
            soundRoot = new GameObject { name = "@SoundRoot" };
            Object.DontDestroyOnLoad(soundRoot);

            string[] soundTypeNames = System.Enum.GetNames(typeof(SoundType));

            for (int count = 0; count < soundTypeNames.Length - 1; count++)
            {
                GameObject go = new GameObject { name = soundTypeNames[count] };
                audioSources[count] = go.AddComponent<AudioSource>();
                go.transform.SetParent(soundRoot.transform);
            }

            audioSources[(int)SoundType.Effect].volume = Managers.Data.UserData.EffectVolume;
            audioSources[(int)SoundType.Bgm].volume = Managers.Data.UserData.BgmVolume;
            audioSources[(int)SoundType.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        foreach (AudioClip audioClip in audioClips.Values)
        {
            Addressables.Release(audioClip);
        }

        audioClips.Clear();
    }

    AudioClip GetOrAddAudioClip(string key, SoundType type = SoundType.Effect)
    {
        AudioClip audioClip = null;

        if (type == SoundType.Bgm)
        {
            audioClip = Addressables.LoadAssetAsync<AudioClip>(key).WaitForCompletion();
        }
        else
        {
            if (!audioClips.TryGetValue(key, out audioClip))
            {
                audioClip = Addressables.LoadAssetAsync<AudioClip>(key).WaitForCompletion();
                audioClips.Add(key, audioClip);
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {key}");
        }

        return audioClip;
    }

    public void Play(AudioClip audioClip, SoundType type = SoundType.Effect)
    {
        if (audioClip == null)
        {
            return;
        }

        if (type == SoundType.Bgm)
        {
            AudioSource audioSource = audioSources[(int)SoundType.Bgm];

            if (audioSource.isPlaying)
            {
                if (audioSource.clip.name == audioClip.name)
                {
                    return;
                }
                else
                {
                    Addressables.Release(audioSource.clip);
                    audioSource.Stop();
                }
            }

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = audioSources[(int)SoundType.Effect];
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string key, SoundType type = SoundType.Effect)
    {
        AudioClip audioClip = GetOrAddAudioClip(key, type);
        Play(audioClip, type);
    }

    public void StopBgm()
    {
        AudioSource audioSource = audioSources[(int)SoundType.Bgm];
        Addressables.Release(audioSource.clip);
        audioSource.Stop();
    }

    public void PauseBgm()
    {
        AudioSource audioSource = audioSources[(int)SoundType.Bgm];
        audioSource.Pause();
    }

    public void ResumeBgm()
    {
        AudioSource audioSource = audioSources[(int)SoundType.Bgm];
        audioSource.Play();
    }

    public void SetEffectVolume(float volume)
    {
        audioSources[(int)SoundType.Effect].volume = volume;
        Managers.Data.SetEffectVolume(volume);
    }

    public void SetBgmVolume(float volume)
    {
        audioSources[(int)SoundType.Bgm].volume = volume;
        Managers.Data.SetBgmVolume(volume);
    }
}