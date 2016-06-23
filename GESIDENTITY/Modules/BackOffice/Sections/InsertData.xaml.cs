using GESIDENTITY.SectionsModules.ViewModels;
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
using System.Collections.ObjectModel;

namespace GESIDENTITY.SectionsModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        string Etat = "";
        SectionsViewModel viewVM;
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

        public InsertData(string etat, Sections ele, SectionsViewModel view)
        {
            InitializeComponent();

            this.DataContext = viewVM = view;

            Etat = etat;

            if (etat == "AJOUT")
            {
                this.Title = "Enregistrement de Sections";
                //ele.Code = "SC000";
                //txtCode.IsEnabled = false;
            }
            else
            {
                this.Title = "Modification de Sections";
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sections ent = ((SectionsViewModel)this.DataContext).SelectedData;

                //if (ent.NoChassis == null || ent.NoChassis.Trim() == "")
                //{
                //    lblMessageError.Text = "Remplir le champ NoChassis avant de continuer";
                //    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                //    return;
                //}

                if (rcbFederation.SearchText == "") { MessageBox.Show("Choississez une section svp!", "Sections", MessageBoxButton.OK, MessageBoxImage.Information); return; }

                if (Etat == "AJOUT")
                {
                    try
                    {
                        //Parametres param = viewVM.model.Parametres.Where(c => c.idParametre == 1).First();
                        //ent.Code = "SC" + GlobalData.GenerateFacture(param.SC.ToString(), 3);
                        //param.SC++;

                        ent.Etat = "ACTIF";

                        //Parametres param = model.Parametres.Where(c => c.idParametre == 1).First();

                        //ent.CodeEntrepot = GenerateFacture(param.ArtID.ToString(), 4);

                        //param.ArtID++;
                        //ent.Federations = rcbFederation.SelectedItem as Federations;

                        viewVM.model.Sections.Add(ent);
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
                        //ent.Federations = rcbFederation.SelectedItem as Federations;
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
            SectionsViewModel vehi = this.DataContext as SectionsViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

        private void radButton_Click(object sender, RoutedEventArgs e)
        {
            FederationsModules.ViewModels.FederationsViewModel vm = new FederationsModules.ViewModels.FederationsViewModel();
            vm.model = viewVM.model;
            vm.SelectedData = new Federations();
            FederationsModules.InsertData view = new FederationsModules.InsertData("AJOUT", vm.SelectedData, vm);
            view.ShowDialog();

            if (view.Msg == "OK")
            {
                rcbFederation.SelectedItem = viewVM.model.Federations.Where(c=>c.ID == vm.SelectedData.ID).FirstOrDefault();
                rcbFederation.Focus();

            }
            else if (view.Msg == "Error")
            {
                MessageBox.Show("   Echec Opération    ", "Federations ", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Load();
                rcbFederation.Focus();

            }
            else
            {
                //viewM.Refresh();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
