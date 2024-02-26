namespace Sips.SipsModels
{
    [Serializable]
    public class Cart
    {
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
