using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;

    public void PlayAudio(int index, float volume, float pitch)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clips[index];
        source.volume = volume;
        source.pitch = pitch;

        source.Play();

        Destroy(source, clips[index].length);
    }
}
