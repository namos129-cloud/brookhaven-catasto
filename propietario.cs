using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

public class CatastoViewModel : INotifyPropertyChanged
{
    public ObservableCollection<UnitaImmobiliare> UnitaImmobiliari { get; set; }
    public ObservableCollection<Proprietario> Proprietari { get; set; }

    public UnitaImmobiliare UnitaSelezionata { get; set; }
    public Proprietario ProprietarioSelezionato { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public CatastoViewModel()
    {
        // Carica i dati dal database
        using (var context = new CatastoContext())
        {
            UnitaImmobiliari = new ObservableCollection<UnitaImmobiliare>(context.UnitaImmobiliari.ToList());
            Proprietari = new ObservableCollection<Proprietario>(context.Proprietari.ToList());
        }
    }

    // Funzioni per aggiungere, modificare e eliminare unità e proprietari
    public void AggiungiUnita(UnitaImmobiliare nuovaUnita)
    {
        using (var context = new CatastoContext())
        {
            context.UnitaImmobiliari.Add(nuovaUnita);
            context.SaveChanges();
            UnitaImmobiliari.Add(nuovaUnita);
        }
    }
}
