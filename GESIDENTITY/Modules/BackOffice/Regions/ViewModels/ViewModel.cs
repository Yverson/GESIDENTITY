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

namespace GESIDENTITY.RegionsModules.ViewModels
{
    public class RegionsViewModel : ObservableObject
    {

        #region Members
        public GESIDENTITYEntities model;
        private BackgroundWorker worker = new BackgroundWorker();
        ObservableCollection<Regions> _data = new ObservableCollection<Regions>();
        ObservableCollection<Districts> _allDistricts = new ObservableCollection<Districts>();
        Regions _selectedData = new Regions();
        bool _isBusy;
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<Regions> AllData
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

        public ObservableCollection<Districts> AllDistricts
        {
            get
            {
                return _allDistricts;
            }
            set
            {
                _allDistricts = value;
                RaisePropertyChanged("AllDistricts");
            }
        }

        public Regions SelectedData
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
        public RegionsViewModel()
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

            var resultat = from res in model.Regions
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<Regions>(resultat.ToList());

            var resultat2 = from res in model.Districts
                            where res.Etat == "ACTIF"
                            select res;

            AllDistricts = new ObservableCollection<Districts>(resultat2.ToList());

        }

        public void CancelChanged()
        {

            var resultat = from res in model.Regions
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<Regions>(resultat.ToList());

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
