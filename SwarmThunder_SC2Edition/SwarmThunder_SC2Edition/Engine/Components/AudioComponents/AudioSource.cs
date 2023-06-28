using SFML.Audio;

public class AudioSource : Component
{
    private AudioClip _currentAudioClip;
    private AudioClip currentAudioClip
    {
        get => _currentAudioClip;
        set
        {
            _currentAudioClip = value;
            if (!loop)
                _currentAudioClip.OnStopped += (_) => _currentAudioClip = null;
        }
    }

    public bool loop;

    public AudioSource()
    {
        Setup();
    }

    public void Setup()
    {
        
    }

    public AudioClip SetClip(AudioClip clip)
    {
        currentAudioClip = clip;
        
        return currentAudioClip;
    }
    public override void Update()
    {
        base.Update();
        
        currentAudioClip?.Update();
        if(loop)
            TryPlayClipLooped();
    }
    public AudioClip PlayClip()
    {
        currentAudioClip.Play();
        
        return currentAudioClip;
    }
    private void TryPlayClipLooped()
    {
        
        if (loop && !currentAudioClip.playing)
        {
            currentAudioClip.Play();
        }
            
    }
    public void StopClip()
    {
        currentAudioClip.Stop();
    }
    public override void Destroy()
    {
        OnDestroy?.Invoke();

        base.Destroy();
    }
}