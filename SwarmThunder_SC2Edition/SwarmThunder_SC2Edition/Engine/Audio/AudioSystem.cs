using SFML.Audio;

public static class AudioSystem
{
    private static string pathToCFG = "CFGs/AudioSystem.cfg";
    
    private static Dictionary<string, AudioClip> _clips = new();
    private static Dictionary<string, AudioSource> _sources = new();


    public static AudioClip GetClip(string clipPath) => _clips[clipPath];
    public static AudioSource GetSource(string sourceName) => _sources[sourceName];

    public static void PlaySoundOnce(string clipName, string sourceName)
    {
        if(!_clips.ContainsKey(clipName))
            return;

        AudioSource _source = _sources[sourceName];
        _source.loop = false;
        _source.SetClip(_clips[clipName]);
        _source.PlayClip();
    }
    public static void PlaySoundOnce(string clipName, AudioSource source)
    {
        if(!_clips.ContainsKey(clipName))
            return;

        source.loop = false;
        source.SetClip(_clips[clipName]);
        source.PlayClip();
    }
    public static void PlaySoundLooped(string clipName, string sourceName)
    {
        if(!_clips.ContainsKey(clipName))
            return;

        AudioSource _source = _sources[sourceName];
        _source.loop = true;
        _source.SetClip(_clips[clipName]);
        _source.PlayClip();
    }
    public static void PlaySoundLooped(string clipName, AudioSource source)
    {
        if(!_clips.ContainsKey(clipName))
            return;
        source.loop = true;
        source.SetClip(_clips[clipName]);
        source.PlayClip();
    }
    public static void StopSound(string sourceName)
    {

        _sources[sourceName].StopClip();
        
    }

    public static void Setup()
    {
        Load();
    }
    public static void Load()
    {
        if (!File.Exists(pathToCFG))
        {
            return;
        }

        StreamReader sr = new (pathToCFG);

        while (!sr.EndOfStream)
        {
            
            string[] line = sr.ReadLine().Split(' ');
            
            if(line.Length < 3)
                continue;

            if (!File.Exists(line[0]))
            {
                continue;
            }
            SoundBuffer _soundBuffer = new(line[0]);
            AudioClip _clip = new AudioClip(_soundBuffer, float.TryParse(line[2], out float _volume) ? _volume : .5f);
            _clips.Add(line[1], _clip);
        }
    }
}