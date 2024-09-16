using SharedWhiteboardAPI.Model;

namespace SharedWhiteboardAPI.Services
{
    public interface IWhiteboardMerger
    {
        public Whiteboard Merge(IEnumerable<Whiteboard> whiteboards);
    }
}
