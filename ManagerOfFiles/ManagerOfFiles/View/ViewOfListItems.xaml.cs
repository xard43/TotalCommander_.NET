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
using MenagerOfFiles.DiscElements;
using System.IO;
using System.Diagnostics;
using ManagerOfFiles.View;
using static ManagerOfFiles.MainWindow;

namespace ManagerOfFiles.View
{
    /// <summary>
    /// Interaction logic for ViewOfListItems.xaml
    /// </summary>
    public partial class ViewOfListItems : UserControl
    {
        public ElementsOfDisc discElement;

        
        public ViewOfListItems()
        {

        }

        public ViewOfListItems(ElementsOfDisc discElement)
        {
            
            
            this.discElement = discElement;
            InitializeComponent();

            labelName.Content = discElement.Name;
            labelTime.Content = discElement.GetCreationTime;
            if (discElement is MyDirectory)
            {
                labelSize.Content = "<DIV>";
                labelType.Content = "FOLDER";
            }
            else
            {
                labelSize.Content = ((MyFile)discElement).SizeOfFile;
                labelType.Content = "PLIK";
            }
        }

        

        public delegate void RefreshList();
        public event RefreshList refreshlist;
        private void button_Click(object sender, RoutedEventArgs e)
        {

            if (discElement is MyDirectory)
            {
                try
                {
                    Directory.Delete(discElement.DiscPath);
                }
                catch (System.IO.IOException a)
                {
                    MessageBox.Show(a.Message);
                }


            }

            else
            {
                File.Delete(discElement.DiscPath);
            }

            if (refreshlist != null)
            {
                refreshlist.Invoke();
            }
        }
        public delegate void ListAfterOpen(ElementsOfDisc discElement, int a);
        public event ListAfterOpen AfterOpenNum1;
        public event ListAfterOpen AfterOpenNum2;

        
       
        /// <summary>
        /// delegat otwarcia folderu/pliku po podwójnym kliknięciu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            if (discElement is MyDirectory)
            {

                if (AfterOpenNum1 != null)
                {
                    AfterOpenNum1.Invoke(discElement, 1);

                }
                else if (AfterOpenNum2 != null)
                {
                    AfterOpenNum2.Invoke(discElement, 2);

                }
            }
            else
            {
                
                try
                {
                    Process.Start(discElement.DiscPath);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

        }
        public delegate void CopyFile(ElementsOfDisc discElement, int a);
        public event CopyFile CopyFileNum1;
        public event CopyFile CopyFileNum2;

        /// <summary>
        /// delegat który wysyła ściężke pliku po jednym kliknięciu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserConrol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                
                if (CopyFileNum1 != null)
                {
                   CopyFileNum1.Invoke(discElement, 1);

                }
                else if (CopyFileNum2 != null)
                {
                    CopyFileNum2.Invoke(discElement, 2);

                }
           
        }
    }
}
