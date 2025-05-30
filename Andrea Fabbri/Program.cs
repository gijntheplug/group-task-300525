public sealed class GestoreSpese
{
    private static GestoreSpese _instance;
    private static readonly object _lock = new object();
    //TODO INSERIRE QUI LISTA PER LE COSE DA GESTIRE (SPESA)
    private List<Spesa> spese = new List<Spesa>();
    private GestoreSpese() { }
    public static GestoreSpese Instance
    {
        get
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    if (_instance is null)
                    {
                        _instance = new GestoreSpese();
                    }
                }
            }
            return _instance;
        }
    }
    public void SalvaSpesa(SpeseFamiglia spese)
    {
        spese.Add(spesa)
    }
}