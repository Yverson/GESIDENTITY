﻿using GESIDENTITY.RegionsModules.ViewModels;
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

namespace GESIDENTITY.RegionsModules
{
    /// <summary>
    /// Interaction logic for InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        string Etat = "";
        RegionsViewModel viewVM;
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

        public InsertData(string etat, Regions ele, RegionsViewModel view)
        {
            InitializeComponent();

            this.DataContext = viewVM = view;

            Etat = etat;

            if (etat == "AJOUT")
            {
                this.Title = "Enregistrement de Regions";
                //ele.Code = "RG000";
                //txtCode.IsEnabled = false;

            }
            else
            {
                this.Title = "Modification de Regions";
            }
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regions ent = ((RegionsViewModel)this.DataContext).SelectedData;

                //if (ent.NoChassis == null || ent.NoChassis.Trim() == "")
                //{
                //    lblMessageError.Text = "Remplir le champ NoChassis avant de continuer";
                //    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                //    return;
                //}

                if (rcbDistrict.SearchText == "") { MessageBox.Show("Choississez un district svp!", "Regions", MessageBoxButton.OK, MessageBoxImage.Information); return; }

                if (Etat == "AJOUT")
                {
                    try
                    {
                        //Parametres param = viewVM.model.Parametres.Where(c => c.idParametre == 1).First();
                        //ent.Code = "RG" + GlobalData.GenerateFacture(param.RG.ToString(), 3);
                        //param.RG++;

                        ent.Etat = "ACTIF";

                        //Parametres param = model.Parametres.Where(c => c.idParametre == 1).First();

                        //ent.CodeEntrepot = GenerateFacture(param.ArtID.ToString(), 4);

                        //param.ArtID++;
                        //ent.Districts = rcbDistrict.SelectedItem as Districts;

                        viewVM.model.Regions.Add(ent);
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
                        //ent.Districts = rcbDistrict.SelectedItem as Districts;
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
            RegionsViewModel vehi = this.DataContext as RegionsViewModel;
            vehi.SelectedData = null;

            this.Close();
        }

        private void radButton_Click(object sender, RoutedEventArgs e)
        {
            DistrictsModules.ViewModels.DistrictsViewModel vm = new DistrictsModules.ViewModels.DistrictsViewModel();
            vm.model = viewVM.model;
            vm.SelectedData = new Districts();
            DistrictsModules.InsertData view = new DistrictsModules.InsertData("AJOUT", vm.SelectedData, vm);
            view.ShowDialog();

            if (view.Msg == "OK")
            {
                rcbDistrict.SelectedItem = viewVM.model.Districts.Where(c=>c.ID == vm.SelectedData.ID).FirstOrDefault();
                rcbDistrict.Focus();

            }
            else if (view.Msg == "Error")
            {
                MessageBox.Show("   Echec Opération    ", "Districts ", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Load();
                rcbDistrict.Focus();

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
