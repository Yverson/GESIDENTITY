using GESIDENTITY.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace GESIDENTITY.IdentificationsModules.ViewModels
{
    public class EntityObjectToInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {

            if (value is Districts)
            {
                var rec = (Districts)value;
                return rec.ID;
            }

            if (value is Regions)
            {
                var rec = (Regions)value;
                return rec.ID;
            }

            if (value is Departements)
            {
                var rec = (Departements)value;
                return rec.ID;
            }

            if (value is Federations)
            {
                var rec = (Federations)value;
                return rec.ID;
            }

            if (value is Sections)
            {
                var rec = (Sections)value;
                return rec.ID;
            }

            if (value is Bases)
            {
                var rec = (Bases)value;
                return rec.ID;
            }

            if (value is Utilisateurs)
            {
                var rec = (Utilisateurs)value;
                return rec.idUtilisateur;
            }



            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {

            if (value is Districts)
            {
                var rec = (Districts)value;
                return rec.ID;
            }

            if (value is Regions)
            {
                var rec = (Regions)value;
                return rec.ID;
            }

            if (value is Departements)
            {
                var rec = (Departements)value;
                return rec.ID;
            }

            if (value is Federations)
            {
                var rec = (Federations)value;
                return rec.ID;
            }

            if (value is Sections)
            {
                var rec = (Sections)value;
                return rec.ID;
            }

            if (value is Bases)
            {
                var rec = (Bases)value;
                return rec.ID;
            }

            if (value is Utilisateurs)
            {
                var rec = (Utilisateurs)value;
                return rec.idUtilisateur;
            }



            return 0;
        }
    }


}
