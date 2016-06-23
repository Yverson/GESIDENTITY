using GESIDENTITY.COMMON;
using GESIDENTITY.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataForm;
using Telerik.Windows.Data;

namespace GESIDENTITY.FederationsModules.ViewModels
{
    public class FederationsViewModel : ObservableObject
    {

        #region Members
        public GESIDENTITYEntities model;
        private BackgroundWorker worker = new BackgroundWorker();
        ObservableCollection<Federations> _data = new ObservableCollection<Federations>();
        ObservableCollection<Regions> _allRegions = new ObservableCollection<Regions>();
        ObservableCollection<Departements> _allDepartements = new ObservableCollection<Departements>();
        Federations _selectedData = new Federations();
        bool _isBusy;
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<Federations> AllData
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                RaisePropertyChanged("AllData");
            }
        }

        public ObservableCollection<Regions> AllRegions
        {
            get
            {
                return _allRegions;
            }
            set
            {
                _allRegions = value;
                RaisePropertyChanged("AllRegions");
            }
        }

        public ObservableCollection<Departements> AllDepartements
        {
            get
            {
                return _allDepartements;
            }
            set
            {
                _allDepartements = value;
                RaisePropertyChanged("AllDepartements");
            }
        }

        public Federations SelectedData
        {
            get
            {
                return _selectedData;
            }
            set
            {
                _selectedData = value;
                RaisePropertyChanged("SelectedData");
            }
        }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }


        #endregion

        #region Construction
        public FederationsViewModel()
        {
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;

            Refresh();

        }

        public void Refresh()
        {

            //Demarre le Background Worker et commence le radBusyIndicator
            if (!worker.IsBusy)
            {
                this.IsBusy = true;
                worker.RunWorkerAsync();
            }

        }

        //La methode de demmarge du BackgroundWorker
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //appel la methode load
            Load();
        }

        //Met a jour le UI et stop le RadBusyIndicator
        private void UpdateDataSource()
        {

            this.IsBusy = false;

        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Quand la methode load a fini de charger les infos on appel la methode UpdateDataSource
            //pour mettre a jour le UI
            //Dispatcher.BeginInvoke(new Action(this.UpdateDataSource));

            this.IsBusy = false;
        }

        private void Load()
        {

            model = new GESIDENTITYEntities();

            var resultat = from res in model.Federations
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<Federations>(resultat.ToList());

            var resultat2 = from res in model.Regions
                            where res.Etat == "ACTIF"
                            select res;

            AllRegions = new ObservableCollection<Regions>(resultat2.ToList());

            var resultat3 = from res in model.Departements
                            where res.Etat == "ACTIF"
                            select res;

            AllDepartements = new ObservableCollection<Departements>(resultat3.ToList());

        }

        public void CancelChanged()
        {

            var resultat = from res in model.Federations
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<Federations>(resultat.ToList());

        }

        public void SaveChanged()
        {

            model.SaveChanges();

        }

        #endregion

        #region Commands
        void AddCommandExecute()
        {

            if (SelectedData != null)
            {

            }
        }

        bool CanAddCommandExecute()
        {
            return true;
        }

        public ICommand AddCommand { get { return new RelayCommand(AddCommandExecute, CanAddCommandExecute); } }


        #endregion

    }
}
