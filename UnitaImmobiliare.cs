public class UnitaImmobiliare
{
    public int Id { get; set; }
    public string Indirizzo { get; set; }
    public string NumeroCatastale { get; set; }
    public double Superficie { get; set; }
    public string Proprietario { get; set; }
    public double ValoreImponibile { get; set; }
}

public class Proprietario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CodiceFiscale { get; set; }
    public string IndirizzoResidenza { get; set; }
}
