using UnityEngine;

public class SoundndManager
{
    private float _soundVolume;
    private float _musicVolume;
    public float SoundVolume { get { return _soundVolume; } }
    public float MusicVolume { get { return _musicVolume; } }

    public void SetSoondVolume(float value) 
    {
        _soundVolume = value;
    }
    public void SetMusicVolume(float value)
    {
        _soundVolume = value;
    }
}
