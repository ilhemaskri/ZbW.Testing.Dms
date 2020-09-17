namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Xml.Serialization;
    using Microsoft.Win32;
    using Prism.Commands;
    using Prism.Mvvm;

    using ZbW.Testing.Dms.Client.Model;
    using ZbW.Testing.Dms.Client.Repositories;

    public class SearchViewModel : BindableBase
    {
        private List<MetadataItem> _filteredMetadataItems = new List<MetadataItem>();

        private MetadataItem _selectedMetadataItem;

        private string _selectedTypItem = "";

        private string _suchbegriff = "";

        private List<string> _typItems;

        public SearchViewModel()
        {
            TypItems = ComboBoxItems.Typ;

            CmdSuchen = new DelegateCommand(OnCmdSuchen);
            CmdReset = new DelegateCommand(OnCmdReset);
            CmdOeffnen = new DelegateCommand(OnCmdOeffnen, OnCanCmdOeffnen);
        }

        public DelegateCommand CmdOeffnen { get; }

        public DelegateCommand CmdSuchen { get; }

        public DelegateCommand CmdReset { get; }

        public string Suchbegriff
        {
            get
            {
                return _suchbegriff;
            }

            set
            {
                SetProperty(ref _suchbegriff, value);
            }
        }

        public List<string> TypItems
        {
            get
            {
                return _typItems;
            }

            set
            {
                SetProperty(ref _typItems, value);
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
                SetProperty(ref _selectedTypItem, value);
            }
        }

        public List<MetadataItem> FilteredMetadataItems
        {
            get
            {
                return _filteredMetadataItems;
            }

            set
            {
                SetProperty(ref _filteredMetadataItems, value);
            }
        }

        public MetadataItem SelectedMetadataItem
        {
            get
            {
                return _selectedMetadataItem;
            }

            set
            {
                if (SetProperty(ref _selectedMetadataItem, value))
                {
                    CmdOeffnen.RaiseCanExecuteChanged();
                }
            }
        }

        private bool OnCanCmdOeffnen()
        {
            return SelectedMetadataItem != null;
        }

        private void OnCmdOeffnen()
        {
            Process.Start(this.SelectedMetadataItem.Filepath);
        }

        private void OnCmdSuchen()
        {
            foreach (MetadataItem item in createMetaList()) {
                addToFilteredList(item);
            }
        }

        private void addToFilteredList(MetadataItem item) {
            if (containsValue(item.Bezeichnung) || containsValue(item.Stichwoerter) || item.SelectedTypItem.Equals(this.SelectedTypItem))
            {
                this.FilteredMetadataItems.Add(item);
            }
        }

        private bool containsValue(String value) {
            return this.Suchbegriff.Length > 0 && value.Contains(this.Suchbegriff);
        }


        private void OnCmdReset()
        {
            this.FilteredMetadataItems.Clear();
            this.SelectedMetadataItem = null;
            this.SelectedTypItem = "";
            this.Suchbegriff = "";
        }

        private MetadataItem deserializeMeta(string file) {
            XmlSerializer serializer = new XmlSerializer(typeof(MetadataItem));
            FileStream fileStream = new FileStream(file, FileMode.Open);
            MetadataItem item =  (MetadataItem)serializer.Deserialize(fileStream);
            fileStream.Close();
            return item;
        }

        private List<MetadataItem> createMetaList() {
            List<MetadataItem> allItems = new List<MetadataItem>();
            foreach (string directory in Directory.GetDirectories("C:\\Temp\\DMS\\")) {
                string path = directory + "\\";
                allItems.AddRange(addMetasToList(path));
            }
            return allItems;
        }

        private List<MetadataItem> addMetasToList(string path) {
            List<MetadataItem> items = new List<MetadataItem>();
            foreach (string file in Directory.GetFiles(path))
            {
                if (containsMetadata(file))
                {
                    items.Add(deserializeMeta(file));
                }
            }
            return items;
        }

        private bool containsMetadata(String file) {
            return file.Contains("_Metadata.xml");
        }
    }
}