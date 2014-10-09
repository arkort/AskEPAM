using AskEpamClientApplication.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace AskEpamClientApplication
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        AskServiceClient connectionToServer;

        public SettingsWindow(AskServiceClient connectionToServer)
        {
            InitializeComponent();
            this.connectionToServer = connectionToServer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "configClientAskEpam.xml";

            if (File.Exists(fileName))
            {
                try
                {
                    XDocument readedDoc = XDocument.Load(fileName);
                    var element = readedDoc.Root.Elements().Where((el) => el.Name == "AddressOfService").FirstOrDefault();
                    element.Value = textBoxAddress.Text;
                    readedDoc.Save(fileName);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                XElement contacts =
                    new XElement("Settings",
                        new XElement("AddressOfService", textBoxAddress.Text)
                        );

                XDocument doc = new XDocument(contacts);
                doc.Save(fileName);
            }

            try
            {
                EndpointAddress addr = new EndpointAddress(textBoxAddress.Text);
                connectionToServer.Endpoint.Address = addr;
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
    }
}
