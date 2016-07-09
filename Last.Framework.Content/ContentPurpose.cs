namespace Last.Framework.Content
{
    public enum ContentPurpose
    {
        Collision,
        Visual,
        Any = Collision | Visual,
    }
}