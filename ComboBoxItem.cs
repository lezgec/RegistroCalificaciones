namespace RegistroCalificaciones
{
    public class ComboBoxItem
    {
        public string Text { get; set; } = "";
        public int Value { get; set; }

        public override string ToString() => Text;
    }
}