public class Tag<T>
{
    private readonly List<T> _tags = new List<T>();

    public T this[int i]
    {
        get => _tags[i];
    }

    public void Replace(int i, T item)
    {
        _tags[i] = item;
    }

    public override bool Equals(object? obj)
    {
        return obj is Tag<T> tag &&
               EqualityComparer<List<T>>.Default.Equals(_tags, tag._tags);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_tags); 
    }

    public static Tag<T> operator +(Tag<T> tag, T newTag)
    {
        tag._tags.Add(newTag);
        return tag;
    }

    public static Tag<T> operator -(Tag<T> tag, T newTag)
    {
        tag._tags.Remove(newTag);
        return tag;
    }

    public static bool operator ==(Tag<T> tag, T newTag)
        => tag._tags.Exists((x) => x.Equals(newTag));

    public static bool operator !=(Tag<T> tag, T newTag)
        => !(tag == newTag);
}