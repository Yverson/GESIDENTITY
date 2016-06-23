//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GESIDENTITY.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Identifications
    {
        public int ID { get; set; }
        public Nullable<int> idDistrict { get; set; }
        public Nullable<int> idRegion { get; set; }
        public Nullable<int> idDepartement { get; set; }
        public Nullable<int> idFederation { get; set; }
        public Nullable<int> idSection { get; set; }
        public Nullable<int> idBase { get; set; }
        public Nullable<int> idUtilisateur { get; set; }
        public string NoIdent { get; set; }
        public Nullable<System.DateTime> DateIdent { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Sexe { get; set; }
        public Nullable<System.DateTime> DateNaissance { get; set; }
        public string LieuNaissance { get; set; }
        public string Domicile { get; set; }
        public string Quartier { get; set; }
        public Nullable<int> idNaturePiece { get; set; }
        public string NoPiece { get; set; }
        public Nullable<System.DateTime> DateValidite { get; set; }
        public Nullable<int> idActiviteProfessionel { get; set; }
        public string Telephone { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
        public string Structure { get; set; }
        public Nullable<int> idFonction { get; set; }
        public string QualiteDe { get; set; }
        public Nullable<System.DateTime> DepuisLe { get; set; }
        public Nullable<int> idDerniereFonction { get; set; }
        public Nullable<System.DateTime> DateDerniereFonc { get; set; }
        public string PosteDerniereFonc { get; set; }
        public Nullable<System.DateTime> DateAdhesion { get; set; }
        public string Observations { get; set; }
        public byte[] Photo { get; set; }
        public string Etat { get; set; }
    
        public virtual Bases Bases { get; set; }
        public virtual Departements Departements { get; set; }
        public virtual Districts Districts { get; set; }
        public virtual Federations Federations { get; set; }
        public virtual FonctionParti FonctionParti { get; set; }
        public virtual FonctionParti FonctionParti1 { get; set; }
        public virtual Fonctions Fonctions { get; set; }
        public virtual NaturePiece NaturePiece { get; set; }
        public virtual Regions Regions { get; set; }
        public virtual Sections Sections { get; set; }
        public virtual Utilisateurs Utilisateurs { get; set; }
    }
}