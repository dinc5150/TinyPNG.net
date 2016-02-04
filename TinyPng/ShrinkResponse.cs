namespace TinyPngDotNet {
    public class Input {
        public int size { get; set; }
        public string type { get; set; }
    }

    public class Output {
        public int size { get; set; }
        public string type { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public double ratio { get; set; }
        public string url { get; set; }
    }

    public class ShrinkResponse {
        public string error { get; set; }
        public string message { get; set; }
        public Input input { get; set; }
        public Output output { get; set; }
    }
}
