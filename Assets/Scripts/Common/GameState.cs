namespace Common
{
    [System.Flags]
    public enum GameState
    {
        Paused,
        Resumed,
        Playing,
        Started,
        Ended
    }
}