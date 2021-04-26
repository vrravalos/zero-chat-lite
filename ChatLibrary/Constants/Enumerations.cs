namespace ChatLibrary.Constants
{
    /// <summary>
    /// Available commands
    /// </summary>
    public enum Command
    {
        UNKNOWN,
        REQ_PING,
        REQ_REGISTER_USERNAME,
        REQ_CHAT,
        REQ_START_SESSION,
        REQ_END_SESSION,
        BROADCAST_NEW_USER,
        BROADCAST_END_SESSION,
    }

    /// <summary>
    /// Participant type
    /// </summary>
    public enum Participant
    {
        UNKNOWN,
        SERVER,
        CLIENT,
    }
}