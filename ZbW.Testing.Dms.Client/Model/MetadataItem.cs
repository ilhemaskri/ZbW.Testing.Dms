using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ZbW.Testing.Dms.Client.Model
{
    public class MetadataItem
    {
        private string _benutzer;

        private string _bezeichnung;

        private DateTime _erfassungsdatum;

        private string _selectedTypItem;

        private string _stichwoerter;

        private string _filepath;

        private DateTime? _valutaDatum;

        public string Stichwoerter
        {
            get
            {
                return _stichwoerter;
            }

            set
            {
                _stichwoerter = value;
            }
        }

        public string Filepath
        {
            get
            {
                return _filepath;
            }

            set
            {
                _filepath = value;
            }
        }

        public string Bezeichnung
        {
            get
            {
                return _bezeichnung;
            }

            set
            {
                _bezeichnung = value;
            }
        }

        public string SelectedTypItem
        {
            get
            {
                return _selectedTypItem;
            }

            set
            {
                _selectedTypItem = value;
            }
        }

        public DateTime Erfassungsdatum
        {
            get
            {
                return _erfassungsdatum;
            }

            set
            {
                _erfassungsdatum = value;
            }
        }

        public string Benutzer
        {
            get
            {
                return _benutzer;
            }

            set
            {
                _benutzer = value;
            }
        }


        public DateTime? ValutaDatum
        {
            get
            {
                return _valutaDatum;
            }

            set
            {
                _valutaDatum = value;
            }
        }
    }
}