using GESIDENTITY.DepartementsModules.ViewModels;
using GESIDENTITY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GESIDENTITY.DepartementsModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        string Etat = "";
        DepartementsViewModel viewVM;
        string msg;

        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        string errorMsg;

        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        public InsertData(string etat, Departements ele, DepartementsViewModel view)
        {
            InitializeComponent();

            this.DataContext = viewVM = view;

            Etat = etat;

            if (etat == "AJOUT")
            {
                this.Title = "Enregistrement de Departements";
                ele.Code = "DP000";
                txtCode.IsEnabled = false;
            }
            else
            {
                this.Title = "Modification de Departements";
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Departements ent = ((DepartementsViewModel)this.DataContext).SelectedData;

                //if (ent.NoChassis == null || ent.NoChassis.Trim() == "")
                //{
                //    lblMessageError.Text = "Remplir le champ NoChassis avant de continuer";
                //    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                //    return;
                //}

                if (Etat == "AJOUT")
                {
                    try
                    {
                        Parametres param = viewVM.model.Parametres.Where(c => c.idParametre == 1).First();
                        ent.Code = "DP" + GlobalData.GenerateFacture(param.DP.ToString(), 3);
                        param.DP++;

                        ent.Etat = "ACTIF";

                        //Parametres param = model.Parametres.Where(c => c.idParametre == 1).First();

                        //ent.CodeEntrepot = GenerateFacture(param.ArtID.ToString(), 4);

                        //param.ArtID++;

                        viewVM.model.Departements.Add(ent);
                        viewVM.model.SaveChanges();

                        Msg = "OK";
                        this.Close();

                    }
                    catch (Exception ex)
                    {

                        Msg = "Error";
                        ErrorMsg = ex.Message;



                    }
                }
                else
                {
                    try
                    {

                        viewVM.model.SaveChanges();

                        Msg = "OK";
                        this.Close();

                    }
                    catch (Exception ex)
                    {

                        Msg = "Error";
                        ErrorMsg = ex.Message;



                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DepartementsViewModel vehi = this.DataContext as DepartementsViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

        private void radButton_Click(object sender, RoutedEventArgs e)
        {
            RegionsModules.ViewModels.RegionsViewModel vm = new RegionsModules.ViewModels.RegionsViewModel();
            vm.model = viewVM.model;
            vm.SelectedData = new Regions();
            RegionsModules.InsertData view = new RegionsModules.InsertData("AJOUT", vm.SelectedData, vm);
            view.ShowDialog();

            if (view.Msg == "OK")
            {
                rcbRegions.SelectedItem = viewVM.model.Regions.Where(c => c.ID == vm.SelectedData.ID).FirstOrDefault();
                rcbRegions.Focus();

            }
            else if (view.Msg == "Error")
            {
                MessageBox.Show("   Echec Opération    ", "Regions ", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Load();
                rcbRegions.Focus();

            }
            else
            {
                //viewM.Refresh();
            }
        }

    }
}
