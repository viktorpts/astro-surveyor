namespace AstroSurveyor
{
    public class Message
    {
        public float aliveFor = 6f;
        public string Content { get; private set; }

        public Message(string content)
        {
            Content = content;
        }
    }
}