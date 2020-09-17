namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Windows;
    using System.Xml.Serialization;
    using Microsoft.Win32;

    using Prism.Commands;
    using Prism.Mvvm;
    using ZbW.Testing.Dms.Client.Model;
    using ZbW.Testing.Dms.Client.Repositories;

    public class DocumentDetailViewModel : BindableBase
    {
        private readonly Action _navigateBack;

        private string _benutzer;

        private string _guid;

        private string _bezeichnung;

        private DateTime _erfassungsdatum;

        private string _filePath;

        private string _storedFilePath;

        private bool _isRemoveFileEnabled;

        private string _selectedTypItem;

        private string _stichwoerter;

        private List<string> _typItems;

        private DateTime? _valutaDatum;


        public DocumentDetailViewModel(string benutzer, Action navigateBack)
        {
            _navigateBack = navigateBack;
            Benutzer = benutzer;
            Erfassungsdatum = DateTime.Now;
            TypItems = ComboBoxItems.Typ;

            CmdDurchsuchen = new DelegateCommand(OnCmdDurchsuchen);
            CmdSpeichern = new DelegateCommand(OnCmdSpeichern);
        }

        public string Stichwoerter
        {
            get
            {
                return _stichwoerter;
            }

            set
            {
                SetProperty(ref _stichwoerter, value);
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
                SetProperty(ref _bezeichnung, value);
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

        public DateTime Erfassungsdatum
        {
            get
            {
                return _erfassungsdatum;
            }

            set
            {
                SetProperty(ref _erfassungsdatum, value);
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
                SetProperty(ref _benutzer, value);
            }
        }

        public DelegateCommand CmdDurchsuchen { get; }

        public DelegateCommand CmdSpeichern { get; }

        public DateTime? ValutaDatum
        {
            get
            {
                return _valutaDatum;
            }

            set
            {
                SetProperty(ref _valutaDatum, value);
            }
        }

        public bool IsRemoveFileEnabled
        {
            get
            {
                return _isRemoveFileEnabled;
            }

            set
            {
                SetProperty(ref _isRemoveFileEnabled, value);
            }
        }

        private void OnCmdDurchsuchen()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                _filePath = openFileDialog.FileName;
            }
        }

        private void OnCmdSpeichern()
        {
            if (hasValue(Stichwoerter) && hasValue(Bezeichnung) && ValutaDatum.HasValue)
            {
                _guid = Convert.ToString(Guid.NewGuid());
                fileLoaded();
                fileDeleted();
                serializeMeta(createFileName("_Metadata"), createMeta());
                _navigateBack();
            }
            else {
                MessageBox.Show("Es müssen alle Pflichtfelder ausgefüllt werden!", "Fehler");
            }
        }

        private void GetDirectory() {
            if (!Directory.Exists("C:\\Temp\\DMS\\" + Convert.ToString(ValutaDatum.Value.Year))) {
                Directory.CreateDirectory("C:\\Temp\\DMS\\" + Convert.ToString(ValutaDatum.Value.Year));
            }    
        }

        private Boolean fileLoaded()
        {
            GetDirectory();
            if (File.Exists(_filePath))
            {
                _storedFilePath = createFileName("_Content");
                    File.Copy(_filePath, createFileName("_Content"));
                    return true;
            }
            return false;
        }

        private Boolean fileDeleted()
        {
            if (IsRemoveFileEnabled && File.Exists(_filePath))
            {
                File.Delete(_filePath);
                return true;
            }
            return false;
        }


        private Boolean hasValue(String value) {
            return value != null && value.Trim().Length > 0;
        }

        private String createFileName(String type) {
            if (type == "_Content")
            {
                String[] subvalues = _filePath.Split(new char[] { '.' });
                String end = "." + subvalues[subvalues.Length - 1];
                return "C:\\Temp\\DMS\\" + Convert.ToString(ValutaDatum.Value.Year) + "\\" + _guid + type + end;
            }
            else {
                String end = ".xml";
                return "C:\\Temp\\DMS\\" + Convert.ToString(ValutaDatum.Value.Year) + "\\" + _guid + type + end;
            }
        }

        private MetadataItem createMeta() {
            MetadataItem meta = new MetadataItem();
            meta.Benutzer = this.Benutzer;
            meta.Bezeichnung = this.Bezeichnung;
            meta.Erfassungsdatum = this.Erfassungsdatum;
            meta.SelectedTypItem = this.SelectedTypItem;
            meta.Stichwoerter = this.Stichwoerter;
            meta.ValutaDatum = this.ValutaDatum;
            meta.Filepath = this._storedFilePath;
            return meta;
        }

        private void serializeMeta(String filename, MetadataItem meta)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MetadataItem));
            StreamWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, meta);
            writer.Close();
        }
    }
}