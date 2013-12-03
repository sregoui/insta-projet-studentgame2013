using Microsoft.Xna.Framework;

namespace TooLateLibrary.Animation
{
    public class SimpleAnimationDefinition
    {
        public string AssetName { get; set; }
        public Point FrameSize { get; set; }
        public Point NbFrames { get; set; }
        public int FrameRate { get; set; }
        public bool Loop { get; set; }
    }
}
