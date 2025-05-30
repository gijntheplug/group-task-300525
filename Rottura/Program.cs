using System;

public abstract class Spesa
{
    public decimal Prezzo;
    public string Descrizione;

    public Spesa(decimal prezzo, string descrizione)
    {
        Prezzo = prezzo;
        Descrizione = descrizione;
    }
}

public class Bollette : Spesa
{
    public Bollette(decimal prezzo, string descrizione) : base(prezzo, descrizione) { }

    public override string ToString()
    {
        return $"Bolletta: {Descrizione}, Spesa: {Prezzo}";
    }
}

public class Alimentari : Spesa
{
    public Alimentari(decimal prezzo, string descrizione) : base(prezzo, descrizione) { }

    public override string ToString()
    {
        return $"Alimentari: {Descrizione}, Spesa: {Prezzo}";
    }
}

public class Mutuo : Spesa
{
    public Mutuo(decimal prezzo, string descrizione) : base(prezzo, descrizione) { }

    public override string ToString()
    {
        return $"Mutuo: {Descrizione}, Spesa: {Prezzo}";
    }
}

public class Spesina : Spesa
{
    public Spesina(decimal prezzo, string descrizione) : base(prezzo, descrizione) { }

    public override string ToString()
    {
        return $"Descrizione: {Descrizione}, Spesa: {Prezzo}";
    }
}

public static class SpesaFactory
{
    public static Spesa CreaSpesa(string tipo, decimal prezzo, string descrizione)
    {
        switch (tipo.ToLower())
        {
            case "bollette":
                return new Bollette(prezzo, descrizione);

            case "alimentari":
                return new Alimentari(prezzo, descrizione);

            case "mutuo":
                return new Mutuo(prezzo, descrizione);

            case "spesina":
                return new Spesina(prezzo, descrizione);

            default:
                Console.WriteLine("Tipo di spesa non valido.");
                return null;
        }
    }
}