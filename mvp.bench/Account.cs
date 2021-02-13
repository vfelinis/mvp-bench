namespace mvp.bench
{
    public class Account
    {
        public string Name { get; }
        public int Amount { get; }

        public Account(string name, int amount) => (Name, Amount) = (name, amount);
    }
}
