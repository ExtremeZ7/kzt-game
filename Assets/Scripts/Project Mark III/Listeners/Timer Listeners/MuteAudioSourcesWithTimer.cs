using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class MuteAudioSourcesWithTimer : TimerListener
{
    [Header("Main Fields")]
    public List<AudioSource> audioSources;

    void Start()
    {
        audioSources.Where(i => i != null).ToList();

        // If the list is empty, try to populate it with
        // all the audio sources in the current gameobject
        //
        if (audioSources.Count == 0)
        {
            foreach (AudioSource audioSource in GetComponents<AudioSource>())
            {
                audioSources.Add(audioSource);
            }
        }
    }

    public override void ManagedUpdate()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = Listener.IsActivated;
        }
    }

}
