using System;

public interface IObserver
{
    void Update(string message);
}

public interface ISpesa
{
    void AggiungiOsservatore(IObserver osservatore);
    void RimuoviOsservatore(IObserver osservatore);
    void Notifica(string messaggio);
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

    public void Notifica()
    {
        foreach (var observer in _observers)
        {
            observer.Update("Nuova spesa aggiunta!");
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


