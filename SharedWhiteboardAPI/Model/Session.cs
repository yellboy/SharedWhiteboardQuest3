namespace SharedWhiteboardAPI.Model
{
    public class Session
    {
        public IDictionary<string, Whiteboard> Whiteboards { get; } = new Dictionary<string, Whiteboard>();
    }
}
