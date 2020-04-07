namespace cw2
{
    public class Studies
    {
        public string name { get; set; }
        public string mode { get; set; }

        public Studies(string name, string mode)
        {
            this.name = name;
            this.mode = mode;
        }

        public Studies()
        {
            name = null;
            mode = null;
        }
    }
}