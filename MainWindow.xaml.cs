using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace CatastoApp
{
    // MODEL (Dati)
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

    // DbContext (Gestione database)
    public class CatastoContext : DbContext
    {
        public DbSet<UnitaImmobiliare> UnitaImmobiliari { get; set; }
        public DbSet<Proprietario> Proprietari { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=catasto.db");
        }
    }

    // VIEWMODEL (Logica applicativa)
    public class CatastoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UnitaImmobiliare> UnitaImmobiliari { get; set; }
        public ObservableCollection<Proprietario> Proprietari { get; set; }

        private UnitaImmobiliare _unitaSelezionata;
        public UnitaImmobiliare UnitaSelezionata
        {
            get { return _unitaSelezionata; }
            set
            {
                _unitaSelezionata = value;
                OnPropertyChanged("UnitaSelezionata");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CatastoViewModel()
        {
            using (var context = new CatastoContext())
            {
                context.Database.EnsureCreated(); // Crea il database se non esiste
                UnitaImmobiliari = new ObservableCollection<UnitaImmobiliare>(context.UnitaImmobiliari.ToList());
                Proprietari = new ObservableCollection<Proprietario>(context.Proprietari.ToList());
            }
        }

        public void AggiungiUnita(UnitaImmobiliare nuovaUnita)
        {
            using (var context = new CatastoContext())
            {
                context.UnitaImmobiliari.Add(nuovaUnita);
                context.SaveChanges();
                UnitaImmobiliari.Add(nuovaUnita);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // VIEW (Interfaccia grafica)
    public partial class MainWindow : Window
    {
        public CatastoViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new CatastoViewModel();
            DataContext = ViewModel;
        }

        private void Aggiungi_Click(object sender, RoutedEventArgs e)
        {
            // Logica per aggiungere una nuova unità immobiliare
            var nuovaUnita = new UnitaImmobiliare
            {
                Indirizzo = "Nuovo Indirizzo",
                NumeroCatastale = "12345",
                Superficie = 100,
                Proprietario = "Mario Rossi",
                ValoreImponibile = 100000
            };
            ViewModel.AggiungiUnita(nuovaUnita);
        }
    }
}

