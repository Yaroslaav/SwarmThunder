using SFML.Audio;
public class AudioClip
{
    public bool playing { get; private set; }

    private Sound sound;
    private SoundBuffer soundBuffer;
    
    public Action<SoundBuffer> OnPlayed;
    public Action<SoundBuffer> OnStopped;
    public Action<SoundBuffer> OnPaused;

    public float volume { get; private set; }

    public AudioClip(SoundBuffer soundBuffer, float volume)
    {
        SetVolume(volume);
        this.soundBuffer = soundBuffer;
        OnStopped += (_) => playing = false;
        OnPaused += (_) => playing = false;
        OnPlayed += (_) => playing = true;
    }
    public void Play()
    {
        sound = new(soundBuffer);
        sound.Volume = volume;
        sound.Play();
        
        OnPlayed?.Invoke(soundBuffer);
    }
    public void Update()
    {
        switch (sound.Status)
        {
            case SoundStatus.Stopped:
                Stop();
                break;
            case SoundStatus.Paused:
                OnPaused?.Invoke(soundBuffer);
                break;
        }
    }

    public void SetVolume(float volume)
    {
        if (volume < 0)
        {
            volume = 0;
        }
        else if (volume > 1)
        {
            volume = 1;
        }
        this.volume = volume;
    }
    public void Stop()
    {
        sound.Stop();
        
        OnStopped?.Invoke(soundBuffer);
    }
}