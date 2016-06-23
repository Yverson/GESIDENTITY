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

namespace GESIDENTITY.BasesModules.ViewModels
{
    public class BasesViewModel : ObservableObject
    {

        #region Members
        public GESIDENTITYEntities model;
        private BackgroundWorker worker = new BackgroundWorker();
        ObservableCollection<Bases> _data = new ObservableCollection<Bases>();
        ObservableCollection<Sections> _allSections = new ObservableCollection<Sections>();
        Bases _selectedData = new Bases();
        bool _isBusy;
        int _count = 0;
        #endregion

        #region Properties
        public ObservableCollection<Bases> AllData
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

        public ObservableCollection<Sections> AllSections
        {
            get
            {
                return _allSections;
            }
            set
            {
                _allSections = value;
                RaisePropertyChanged("AllSections");
            }
        }

        public Bases SelectedData
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
        public BasesViewModel()
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

            var resultat = from res in model.Bases
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<Bases>(resultat.ToList());

            var resultat2 = from res in model.Sections
                            where res.Etat == "ACTIF"
                            select res;

            AllSections = new ObservableCollection<Sections>(resultat2.ToList());

        }

        public void CancelChanged()
        {

            var resultat = from res in model.Bases
                           where res.Etat == "ACTIF"
                           select res;

            AllData = new ObservableCollection<Bases>(resultat.ToList());

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
