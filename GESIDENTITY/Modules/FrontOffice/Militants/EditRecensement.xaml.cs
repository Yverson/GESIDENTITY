using GESIDENTITY.COMMON;
using GESIDENTITY.IdentificationsModules.ViewModels;
using GESIDENTITY.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace GESIDENTITY.IdentificationsModules
{
    /// <summary>
    /// Logique d'interaction pour Facture JUMIA.xaml
    /// </summary>
    public partial class EditRecensement : UserControl
    {
        string Etat = "";
        IdentificationsViewModel viewVM;
        IDMilitants mil;
        string msg;
        bool isphotochanged = false;
        string Titre;

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

        public EditRecensement(string etat, Identifications ele, IdentificationsViewModel view)
        {
            InitializeComponent();

            try
            {
                FrameworkElement.LanguageProperty.OverrideMetadata(
    typeof(FrameworkElement),
    new FrameworkPropertyMetadata(
    System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentUICulture.IetfLanguageTag)));
            }
            catch (Exception)
            {

            }

            this.DataContext = viewVM = view;
            lblNo.Content = viewVM.SelectedData.NoIdent;


            if (view.SelectedData.Sexe == "H")
            {
                chkH.IsChecked = true;
            }
            else
            {
                chkF.IsChecked = true;
            }

            Etat = etat;

            Load();

            Titre = "Modifier Militant " + view.SelectedData.NoIdent;
            riPhoto.Source = LoadImage(view.SelectedData.Photo);


        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {



        }

        private void Load()
        {

            visibilite();

        }

        private void visibilite()
        {

            //btnAdd.Visibility = System.Windows.Visibility.Collapsed;

            //if (trans.Etat == "VALIDER" || trans.Etat == "TERMINER")
            //{
            //btnValider.Visibility = System.Windows.Visibility.Collapsed;
            //btnAnnuler.Visibility = System.Windows.Visibility.Collapsed;

            //rgvDetail.IsReadOnly = true;


            //}
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Identifications ent = ((IdentificationsViewModel)this.DataContext).SelectedData;

                //if (ent.NoChassis == null || ent.NoChassis.Trim() == "")
                //{
                //    lblMessageError.Text = "Remplir le champ NoChassis avant de continuer";
                //    lblMessageError.Visibility = System.Windows.Visibility.Visible;

                //    return;
                //}

                //if (rcbDistrict.SearchText == "") { MessageBox.Show("Choississez un district svp!", "Identifications", MessageBoxButton.OK, MessageBoxImage.Information); return; }

                try
                {
                    if (chkH.IsChecked == true)
                    {
                        ent.Sexe = "H";
                    }
                    else
                    {
                        ent.Sexe = "F";
                    }

                    //ent.NoIdent = generateno();

                    if (isphotochanged)
                    {
                        if (riPhoto.Source != null)
                        {
                            ent.Photo = BufferFromImage(riPhoto.Source as BitmapImage);
                        }
                    }
                    

                    //Parametres param = viewVM.model.Parametres.Where(c => c.idParametre == 1).FirstOrDefault();
                    //param.IDENTID++;
                    
                    viewVM.model.SaveChanges();
                    viewVM.Refresh();

                    GlobalData.RemoveItem(Titre);

                    Clear();
                    Msg = "OK";

                    MessageBox.Show("Opération effectuée avec succès", "Identifications", MessageBoxButton.OK, MessageBoxImage.Information);



                }
                catch (Exception ex)
                {

                    Msg = "Error";
                    MessageBox.Show("    Echec Opération    ", "Identifications", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ErrorMsg = ex.Message;

                }
            }
            catch (Exception)
            {

            }
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            IdentificationsViewModel vehi = this.DataContext as IdentificationsViewModel;
            vehi.SelectedData = null;

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
                rcbDistrict.SelectedItem = viewVM.model.Districts.Where(c => c.ID == vm.SelectedData.ID).FirstOrDefault();
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

        private void radRegions_Click(object sender, RoutedEventArgs e)
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

        private void radDepartement_Click(object sender, RoutedEventArgs e)
        {
            //DepartementsModules.ViewModels.DepartementsViewModel vm = new DepartementsModules.ViewModels.DepartementsViewModel();
            //vm.model = viewVM.model;
            //vm.SelectedData = new Departements();
            //DepartementsModules.InsertData view = new DepartementsModules.InsertData("AJOUT", vm.SelectedData, vm);
            //view.ShowDialog();

            //if (view.Msg == "OK")
            //{
            //    rcbDepartement.SelectedItem = viewVM.model.Departements.Where(c => c.ID == vm.SelectedData.ID).FirstOrDefault();
            //    rcbDepartement.Focus();

            //}
            //else if (view.Msg == "Error")
            //{
            //    MessageBox.Show("   Echec Opération    ", "Departements ", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    //Load();
            //    rcbDepartement.Focus();

            //}
            //else
            //{
            //    //viewM.Refresh();
            //}
        }

        private void radFederation_Click(object sender, RoutedEventArgs e)
        {
            FederationsModules.ViewModels.FederationsViewModel vm = new FederationsModules.ViewModels.FederationsViewModel();
            vm.model = viewVM.model;
            vm.SelectedData = new Federations();
            FederationsModules.InsertData view = new FederationsModules.InsertData("AJOUT", vm.SelectedData, vm);
            view.ShowDialog();

            if (view.Msg == "OK")
            {
                rcbFederation.SelectedItem = viewVM.model.Federations.Where(c => c.ID == vm.SelectedData.ID).FirstOrDefault();
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

        private void radDFonctionPartiF_Click(object sender, RoutedEventArgs e)
        {
            FonctionPartiModules.ViewModels.FonctionPartiViewModel vm = new FonctionPartiModules.ViewModels.FonctionPartiViewModel();
            vm.model = viewVM.model;
            vm.SelectedData = new FonctionParti();
            FonctionPartiModules.InsertData view = new FonctionPartiModules.InsertData("AJOUT", vm.SelectedData, vm);
            view.ShowDialog();

            if (view.Msg == "OK")
            {
                rcbDFonctionParti.SelectedItem = viewVM.model.FonctionParti.Where(c => c.ID == vm.SelectedData.ID).FirstOrDefault();
                rcbDFonctionParti.Focus();

            }
            else if (view.Msg == "Error")
            {
                MessageBox.Show("   Echec Opération    ", "Fonction du Parti ", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Load();
                rcbDFonctionParti.Focus();

            }
            else
            {
                //viewM.Refresh();
            }
        }

        private void radFonctionParti_Click(object sender, RoutedEventArgs e)
        {
            FonctionPartiModules.ViewModels.FonctionPartiViewModel vm = new FonctionPartiModules.ViewModels.FonctionPartiViewModel();
            vm.model = viewVM.model;
            vm.SelectedData = new FonctionParti();
            FonctionPartiModules.InsertData view = new FonctionPartiModules.InsertData("AJOUT", vm.SelectedData, vm);
            view.ShowDialog();

            if (view.Msg == "OK")
            {
                rcbFonctionParti.SelectedItem = viewVM.model.FonctionParti.Where(c => c.ID == vm.SelectedData.ID).FirstOrDefault();
                rcbFonctionParti.Focus();

            }
            else if (view.Msg == "Error")
            {
                MessageBox.Show("   Echec Opération    ", "Fonction du Parti ", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Load();
                rcbFonctionParti.Focus();

            }
            else
            {
                //viewM.Refresh();
            }
        }

        private void radFonction_Click(object sender, RoutedEventArgs e)
        {
            FonctionsModules.ViewModels.FonctionsViewModel vm = new FonctionsModules.ViewModels.FonctionsViewModel();
            vm.model = viewVM.model;
            vm.SelectedData = new Fonctions();
            FonctionsModules.InsertData view = new FonctionsModules.InsertData("AJOUT", vm.SelectedData, vm);
            view.ShowDialog();

            if (view.Msg == "OK")
            {
                rcbFonction.SelectedItem = viewVM.model.Fonctions.Where(c => c.ID == vm.SelectedData.ID).FirstOrDefault();
                rcbFonction.Focus();

            }
            else if (view.Msg == "Error")
            {
                MessageBox.Show("   Echec Opération    ", "Fonctions ", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Load();
                rcbFonction.Focus();

            }
            else
            {
                //viewM.Refresh();
            }
        }

        private void NaturePiece_Click(object sender, RoutedEventArgs e)
        {
            NaturePieceModules.ViewModels.NaturePieceViewModel vm = new NaturePieceModules.ViewModels.NaturePieceViewModel();
            vm.model = viewVM.model;
            vm.SelectedData = new NaturePiece();
            NaturePieceModules.InsertData view = new NaturePieceModules.InsertData("AJOUT", vm.SelectedData, vm);
            view.ShowDialog();

            if (view.Msg == "OK")
            {
                rcbNaturePiece.SelectedItem = viewVM.model.NaturePiece.Where(c => c.ID == vm.SelectedData.ID).FirstOrDefault();
                rcbNaturePiece.Focus();

            }
            else if (view.Msg == "Error")
            {
                MessageBox.Show("   Echec Opération    ", "NaturePiece ", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Load();
                rcbNaturePiece.Focus();

            }
            else
            {
                //viewM.Refresh();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;

                viewVM.SelectedData.Sexe = chk.Content.ToString().Substring(0, 1);
            }
            catch (Exception)
            {
            }


        }

        private void rcbFonctionParti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //lblNo.Content = generateno();
        }

        private void rcbDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblNo.Content = generateno();
        }

        private void rcbRegions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblNo.Content = generateno();
        }

        private void rcbFederation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblNo.Content = generateno();
        }

        string generateno()
        {
            string no = "";

            if (rcbDistrict.SelectedItem != null)
            {
                Districts d = rcbDistrict.SelectedItem as Districts;


                no += d.Code;
            }
            else
            {
                no += "DD";
            }

            if (rcbRegions.SelectedItem != null)
            {
                Regions R = rcbRegions.SelectedItem as Regions;

                no += R.Code;
            }
            else
            {
                no += "R";
            }

            if (rcbFederation.SelectedItem != null)
            {
                Federations f = rcbFederation.SelectedItem as Federations;

                no += f.Code;
            }
            else
            {
                no += "F";
            }

            if (rcbDistrict.SelectedItem != null && rcbRegions.SelectedItem != null && rcbFederation.SelectedItem != null)
            {

                try
                {
                    Districts d = rcbDistrict.SelectedItem as Districts;
                    Regions r = rcbRegions.SelectedItem as Regions;
                    Federations f = rcbFederation.SelectedItem as Federations;

                    //Parametres param = viewVM.model.Parametres.Where(c => c.idParametre == 1).FirstOrDefault();
                    mil = viewVM.model.IDMilitants.Where(c => c.idDistrict == d.ID && c.idRegion == r.ID && c.idFederation == f.ID).FirstOrDefault();

                    if (mil != null)
                    {

                        string str = viewVM.SelectedData.NoIdent;
                        str = str.Substring(4, 6);
                        no += str;
                        //mil.IDENTID;
                    }
                    //else
                    //{
                    //    mil = new IDMilitants();
                    //    mil.idDistrict = d.ID;
                    //    mil.idFederation = f.ID;
                    //    mil.idRegion = r.ID;
                    //    mil.IDENTID = 1;


                    //}

                }
                catch (Exception)
                {
                    no += "000000";
                }
            }
            else
            {
                no += "000000";
            }

            if (rcbFonctionParti.SelectedItem != null)
            {
                FonctionParti f = rcbFonctionParti.SelectedItem as FonctionParti;

                txtOrgane.Text = f.Organes.Description;

                no += " - " + f.Organes.Cle + f.Cle + "";
            }
            else
            {
                txtOrgane.Text = "";
                no += "";
            }


            return no;
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpeg";
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                riPhoto.Source = new BitmapImage(new Uri(filename));
                isphotochanged = true;
            }
        }

        private void btnAnnulerPhoto_Click(object sender, RoutedEventArgs e)
        {
            riPhoto.Source = null;
        }

        public byte[] BufferFromImage(BitmapImage imageSource)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(imageSource.UriSource.AbsolutePath);
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }

            return arr;
        }

        private void Clear()
        {
            viewVM = new IdentificationsViewModel();
            this.DataContext = viewVM;
            riPhoto.Source = null;
            //txtOrgane.Text = "";
        }

    }
}
