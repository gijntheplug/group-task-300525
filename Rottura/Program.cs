using System;

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
        set { _descrizione = value;}
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