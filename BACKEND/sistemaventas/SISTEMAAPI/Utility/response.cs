namespace SISTEMAAPI.Utility
{
    public class response<T>
    {
        public bool status { get; set; }
        public T Value { get; set; }

        public string msg { get; set; }
    }
}
