﻿using GESIDENTITY.IdentificationsModules.ViewModels;
using GESIDENTITY.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace GESIDENTITY.IdentificationsModules
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataGridView : UserControl
    {
        IdentificationsViewModel viewM;
        public DataGridView()
        {
            InitializeComponent();

            this.dataGrid.MouseDoubleClick += this.OnGridMouseDoubleClick;

        }

        //Evenement double clic du RadGridview
        void OnGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement originalSender = e.OriginalSource as FrameworkElement;
            if (originalSender != null)
            {
                //var cell = originalSender.ParentOfType<GridViewCell>();
                //if (cell != null)
                //{
                //    MessageBox.Show("The double-clicked cell is " + cell.Value);
                //}

                var row = originalSender.ParentOfType<GridViewRow>();
                if (row != null)
                {
                    try
                    {
                        if (GlobalData.VerificationDroit("CanEditIdentifications"))
                        {
                            viewM = this.Main.DataContext as IdentificationsViewModel;

                            RadDocumentPane rad = new RadDocumentPane();
                            rad.Content = new GESIDENTITY.IdentificationsModules.EditRecensement("MOD", viewM.SelectedData, viewM);
                            rad.Header = "Modifier Militant " + viewM.SelectedData.NoIdent;
                            GlobalData.rrvMain.Title = "Modifier Militant " + viewM.SelectedData.NoIdent;
                            rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                            
                            if (!GlobalData.VerifyDock("Modifier Militant " + viewM.SelectedData.NoIdent))
                            {
                                GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                            }
                            else
                            {

                            }
                        }


                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + "Veuillez fermer le formulaire puis recommencer.", "Identifications", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                }

            }

        }

        private void dataform_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            //switch (e.EditAction)
            //{
            //    case Telerik.Windows.Controls.Data.DataForm.EditAction.Cancel:
            //        viewM = this.Main.DataContext as ModeReglementsViewModel;
            //        viewM.CancelChanged();
            //        (sender as RadDataForm).ValidationSummary.Errors.Clear();
            //        break;
            //    case Telerik.Windows.Controls.Data.DataForm.EditAction.Commit:
            //        viewM = this.Main.DataContext as ModeReglementsViewModel;
            //        var data = (sender as RadDataForm).CurrentItem as ModeReglements;
            //        //var modelContainsEntity = vehiVM.model.ModeReglements.Where(c => c.idModeReglements == data.idModeReglements).FirstOrDefault();
            //        if (data.idModeReglements == 0)
            //        {
            //            viewM.model.ModeReglements.Add(data);
            //        }

            //        viewM.model.SaveChangesAsync();
            //        break;
            //    default:
            //        throw new InvalidOperationException("Edit action should be Cancel or Commit only.");
            //}
        }

        private void dataform_CurrentItemChanged(object sender, EventArgs e)
        {
            //(sender as RadDataForm).CancelEdit();
            //(sender as RadDataForm).ValidationSummary.Errors.Clear();

            //viewM = this.Main.DataContext as UtilisateursViewModel;
            //viewM.CancelChanged();

        }

        private void dataform_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {


        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            viewM = this.Main.DataContext as IdentificationsViewModel;
            viewM.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //var addNewCommand = RadDataFormCommands.AddNew as RoutedUICommand;
            //addNewCommand.Execute(null, dataform);

            try
            {

                //if (GlobalData.VerificationDroit("CanAddTemplate"))
                //{
                viewM = this.Main.DataContext as IdentificationsViewModel;
                viewM.SelectedData = new Identifications();
                InsertData view = new InsertData("AJOUT", viewM.SelectedData, viewM);
                view.ShowDialog();

                if (view.Msg == "OK")
                {

                    MessageBox.Show("Opération effectuée avec succès", "Identifications", MessageBoxButton.OK, MessageBoxImage.Information);
                    viewM.Refresh();

                }
                else if (view.Msg == "Error")
                {
                    MessageBox.Show("   Echec Opération    ", "Identifications ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    viewM.Refresh();

                }
                else
                {
                    viewM.Refresh();
                }
                //}


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Identifications", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    if (GlobalData.VerificationDroit("CanEditIdentifications"))
                    {
                        viewM = this.Main.DataContext as IdentificationsViewModel;

                        RadDocumentPane rad = new RadDocumentPane();
                        rad.Content = new GESIDENTITY.IdentificationsModules.EditRecensement("MOD", viewM.SelectedData, viewM);
                        rad.Header = "Modifier Militant " + viewM.SelectedData.NoIdent;
                        GlobalData.rrvMain.Title = "Modifier Militant " + viewM.SelectedData.NoIdent;
                        rad.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        rad.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;


                        if (!GlobalData.VerifyDock("Modifier Militant " + viewM.SelectedData.NoIdent))
                        {
                            GlobalData.PaneGroup.AddItem(rad, Telerik.Windows.Controls.Docking.DockPosition.Center);
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Identifications", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
            else
            {
                MessageBox.Show("Aucune ligne selectionnée dans la liste", "Identifications", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            //if (GlobalData.VerificationDroit("CanAddIdentifications"))
            //{

            var result = MessageBox.Show("Voulez vous vraiment supprimer ?", "Message", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                if (dataGrid.SelectedItem != null)
                {
                    try
                    {
                        viewM = this.Main.DataContext as IdentificationsViewModel;
                        Identifications ent = dataGrid.SelectedItem as Identifications;
                        ent.Etat = "SUPPRIMER";

                        viewM.model.SaveChanges();

                        viewM.Refresh();

                        MessageBox.Show("Opération effectuée avec succès", "Identifications", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Identifications", MessageBoxButton.OK, MessageBoxImage.Warning);
                        viewM.Refresh();

                    }
                }
                else
                {
                    MessageBox.Show("Aucune ligne selectionnée dans la liste", "Identifications", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

            }
            //}

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //if (GlobalData.VerificationDroit("CanAddIdentifications"))
            //{
            //    btnAdd.Visibility = System.Windows.Visibility.Visible;
            //}

            if (GlobalData.VerificationDroit("CanEditIdentifications"))
            {
                btnEdit.Visibility = System.Windows.Visibility.Visible;
            }

            if (GlobalData.VerificationDroit("CanDeleteIdentifications"))
            {
                btnDelete.Visibility = System.Windows.Visibility.Visible;
            }

        }

    }
}
