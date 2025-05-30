using System;

//Andrea Rottura
public abstract class Spesa
{
    private decimal _prezzo;
    private string _descrizione;
    private string _utente;

    public decimal Prezzo
    {
        get { return _prezzo; }
        set
        {
            if (value >= 0)
            {
                _prezzo = value;
            }
        }
    }

    public string Descrizione
    {
        get { return _descrizione; }
        set { _descrizione = value; }
    }

    public string Utente
    {
        get { return _utente; }
        set
        {
            if (value.ToLower() == "madre" || value.ToLower() == "padre")
            {
                _utente = value;
            }
        }
    }

    public Spesa(decimal prezzo, string descrizione, string utente)
    {
        Prezzo = prezzo;
        Descrizione = descrizione;
        Utente = utente;
    }
}

public class Bollette : Spesa
{
    public Bollette(decimal prezzo, string descrizione, string utente) : base(prezzo, descrizione, utente) { }

    public override string ToString()
    {
        return $"Bolletta: {Descrizione}, Spesa: {Prezzo}";
    }
}

public class Alimentari : Spesa
{
    public Alimentari(decimal prezzo, string descrizione, string utente) : base(prezzo, descrizione, utente) { }

    public override string ToString()
    {
        return $"Alimentari: {Descrizione}, Spesa: {Prezzo}";
    }
}

public class Mutuo : Spesa
{
    public Mutuo(decimal prezzo, string descrizione, string utente) : base(prezzo, descrizione, utente) { }

    public override string ToString()
    {
        return $"Mutuo: {Descrizione}, Spesa: {Prezzo}";
    }
}

public class Spesina : Spesa
{
    public Spesina(decimal prezzo, string descrizione, string utente) : base(prezzo, descrizione, utente) { }

    public override string ToString()
    {
        return $"Descrizione: {Descrizione}, Spesa: {Prezzo}";
    }
}

public static class SpesaFactory
{
    public static Spesa CreaSpesa(string tipo, decimal prezzo, string descrizione, string utente)
    {
        switch (tipo.ToLower())
        {
            case "bollette":
                return new Bollette(prezzo, descrizione, utente);

            case "alimentari":
                return new Alimentari(prezzo, descrizione, utente);

            case "mutuo":
                return new Mutuo(prezzo, descrizione, utente);

            case "spesina":
                return new Spesina(prezzo, descrizione, utente);

            default:
                Console.WriteLine("Tipo di spesa non valido.");
                return null;
        }
    }
}

//Alessio Macrì
public interface IObserver
{
    void Update(Spesa s);
}

public interface ISpesa
{
    void AggiungiOsservatore(IObserver osservatore);
    void RimuoviOsservatore(IObserver osservatore);
    void Notifica(Spesa s);
}

public class ObserverSpese : ISpesa
{
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void AggiungiOsservatore(IObserver osservatore)
    {
        _observers.Add(osservatore);
    }

    public void RimuoviOsservatore(IObserver osservatore)
    {
        _observers.Remove(osservatore);
    }

    public void Notifica(Spesa s)
    {
        foreach (var observer in _observers)
        {
            observer.Update(s);
        }

    }
}

public class NotificaMadre : IObserver
{
    public void Update(Spesa s)
    {
        Console.WriteLine($"Mamma ha inserito: {s.ToString()}");
    }
}

public class NotificaPadre : IObserver
{
    public void Update(Spesa s)
    {
        Console.WriteLine($"Papà ha inserito: {s.ToString()}");
    }
}

//Andrea Fabbri
public sealed class GestoreSpese
{
    private static GestoreSpese _instance;
    private static readonly object _lock = new object();
    public List<Spesa> spese = new List<Spesa>();
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
    public void SalvaSpesa(Spesa spesa)
    {
        spese.Add(spesa);
    }
}

public class Program
{
    static void Main(string[] args)
    {
        bool exit = false;
        GestoreSpese gestoreSpese= GestoreSpese.Instance;
        var notificaMadre = new NotificaMadre();
        var notificaPadre = new NotificaPadre();
        var observerspese = new ObserverSpese();

        observerspese.AggiungiOsservatore(notificaPadre);
        observerspese.AggiungiOsservatore(notificaMadre);
        
        
        while (!exit)
        {
            Console.WriteLine("\nMenù");
            Console.WriteLine("[1] Aggiungi spesa");
            Console.WriteLine("[2] Visualizza spese");
            Console.WriteLine("[3] Visualizza spese madre");
            Console.WriteLine("[4] Visualizza spese padre");
            Console.WriteLine("[0] Esci");
            Console.Write("Scelta: ");
            int menuAction = int.Parse(Console.ReadLine());

            switch (menuAction)
            {
                case 1:
                    Console.Write("Inserisci l'utente: ");
                    string utente = Console.ReadLine();
                    Console.Write("Inserisci il tipo (bollette, alimentari, mutuo, spesina): ");
                    string tipo = Console.ReadLine();
                    Console.Write("Inserisci descrizione: ");
                    string descrizione = Console.ReadLine();
                    Console.Write("Inserisci l'importo: ");
                    decimal prezzo = decimal.Parse(Console.ReadLine());


                    Spesa spesa = SpesaFactory.CreaSpesa(tipo, prezzo, descrizione, utente);
                    gestoreSpese.SalvaSpesa(spesa);
                    observerspese.Notifica(spesa);
                    break;

                case 2:
                    Console.WriteLine("\nSpese:");
                    foreach (Spesa s in gestoreSpese.spese)
                    {
                        Console.WriteLine(s.ToString());
                    }
                    break;

                case 3:
                    Console.WriteLine("\nSpese madre:");
                    foreach (Spesa s in gestoreSpese.spese)
                    {
                        if (s.Utente == "madre")
                        {
                            Console.WriteLine(s.ToString());
                        }
                    }
                    break;

                case 4:
                    Console.WriteLine("Spese padre:");
                    foreach (Spesa s in gestoreSpese.spese)
                    {
                        if (s.Utente == "padre")
                        {
                            Console.WriteLine(s.ToString());
                        }
                    }
                    break;

                case 0:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
    }
}
