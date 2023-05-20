using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SoundManager
{
    AudioSource[] audioSources = new AudioSource[(int)Sound.Max];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject soundRoot = GameObject.Find("@SoundRoot");

        if (soundRoot == null)
        {
            soundRoot = new GameObject { name = "@SoundRoot" };
            Object.DontDestroyOnLoad(soundRoot);

            string[] soundTypeNames = System.Enum.GetNames(typeof(Sound));

            for (int count = 0; count < soundTypeNames.Length - 1; count++)
            {
                GameObject go = new GameObject { name = soundTypeNames[count] };
                audioSources[count] = go.AddComponent<AudioSource>();
                go.transform.SetParent(soundRoot.transform);
            }

            audioSources[(int)Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        audioClips.Clear();
    }

    AudioClip GetOrAddAudioClip(string path, Sound type = Sound.Effect)
    {
        if (!path.Contains("Sounds/"))
        {
            path = $"Sounds/{path}";
        }

        AudioClip audioClip = null;

        if (type == Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (!audioClips.TryGetValue(path, out audioClip))
            {
                audioClip = Resources.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }

        return audioClip;
    }

    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float volume = 1.0f, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            return;
        }

        if (type == Sound.Bgm)
        {
            AudioSource audioSource = audioSources[(int)Sound.Bgm];

            if (audioSource.isPlaying)
            {
                if (audioSource.clip.name == audioClip.name)
                {
                    return;
                }
                else
                {
                    audioSource.Stop();
                }
            }

            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = audioSources[(int)Sound.Effect];
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, Sound type = Sound.Effect, float volume = 1.0f, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, volume, pitch);
    }

    public void StopBgm()
    {
        AudioSource audioSource = audioSources[(int)Sound.Bgm];
        audioSource.Stop();
    }

    public void PauseBgm()
    {
        AudioSource audioSource = audioSources[(int)Sound.Bgm];
        audioSource.Pause();
    }

    public void ResumeBgm()
    {
        AudioSource audioSource = audioSources[(int)Sound.Bgm];
        audioSource.Play();
    }
}