using System;
using System.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZbW.Testing.Dms.Client.Model;

namespace Zbw.Testing.Dms.Client.Testing.ModelTest
{
    [TestClass]
    public class MetadataItemTest
    {
        [TestMethod]
        public void TestBenutzer()
        {
            MetadataItem meta = new MetadataItem();
            meta.Benutzer = "Tester";
            Assert.AreEqual(meta.Benutzer, "Tester");
        }

        [TestMethod]
        public void TestBezeichnung()
        {
            MetadataItem meta = new MetadataItem();
            meta.Bezeichnung = "Hallo Welt";
            Assert.AreEqual(meta.Bezeichnung, "Hallo Welt");
        }

        [TestMethod]
        public void TestErfassungsdatum()
        {
            MetadataItem meta = new MetadataItem();
            meta.Erfassungsdatum = new DateTime(2020, 03, 09);
            Assert.AreEqual(meta.Erfassungsdatum, new DateTime(2020, 03, 09));
        }

        [TestMethod]
        public void TestSelectedTypeItem()
        {
            MetadataItem meta = new MetadataItem();
            meta.SelectedTypItem = "Quittung";
            Assert.AreEqual(meta.SelectedTypItem, "Quittung");
        }

        [TestMethod]
        public void TestStichwoerter()
        {
            MetadataItem meta = new MetadataItem();
            meta.Stichwoerter = "Tag";
            Assert.AreEqual(meta.Stichwoerter, "Tag");
        }

        [TestMethod]
        public void TestValutaDatum()
        {
            MetadataItem meta = new MetadataItem();
            meta.ValutaDatum = new DateTime(2020, 09, 17);
            Assert.AreEqual(meta.ValutaDatum, new DateTime(2020, 09, 17));
        }

        [TestMethod]
        public void TestFilePath()
        {
            MetadataItem meta = new MetadataItem();
            meta.Filepath = "C:\\Temp\\DMS\\";
            Assert.AreEqual(meta.Filepath, "C:\\Temp\\DMS\\");
        }
    }
}
