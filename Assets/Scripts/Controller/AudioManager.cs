using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource source;
    public List<Sounds> sceneSoundFXTypes;
    public List<AudioClip> sceneSoundFXClips;
    public List<float> audioLevels;
    Dictionary<Sounds, AudioClip> sceneSoundFX;
    Dictionary<Sounds, float> soundEQSettings;

    private void Awake()
    {
        sceneSoundFX = new Dictionary<Sounds, AudioClip>();
        if (sceneSoundFXTypes.Count == sceneSoundFXClips.Count) {
            for (int i = 0; i < sceneSoundFXTypes.Count; i++) {
                sceneSoundFX.Add(sceneSoundFXTypes[i], sceneSoundFXClips[i]);
            }
        }
        soundEQSettings = new Dictionary<Sounds, float>();
        if (sceneSoundFXTypes.Count == audioLevels.Count)
        {
            for (int i = 0; i < sceneSoundFXTypes.Count; i++)
            {
                soundEQSettings.Add(sceneSoundFXTypes[i], audioLevels[i]);
            }
        }
    }

    public void PlaySound(Sounds sound) {
        //ADD LOGIC HERE ACCORDING TO WHAT NEEDS TO BE SCALED

        source.volume = soundEQSettings[sound];
        source.clip = sceneSoundFX[sound];
        source.Play();
    }
}
