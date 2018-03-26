using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using ManagerOfFiles.View;
using MenagerOfFiles.DiscElements;

namespace ManagerOfFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int forcheck;
        private ElementsOfDisc elementdisc;

        public MainWindow()
        {
            InitializeComponent();
            RefreshListNum1(textBoxPathNum1.Text);
            RefreshListNum2(textBoxPathNum1.Text);
            PathNewDir.Text = textBoxPathNum1.Text;

        }

        /// <summary>
        /// odświeżanie pierwszego okna
        /// </summary>
        /// <param name="path"></param>
        private void RefreshListNum1(string path)
        {
            MyDirectory MyDir1 = new MyDirectory(path);
            listBoxNumber1.Items.Clear();
            List<ElementsOfDisc> DiscElementsNum1 = MyDir1.GetAllFilesAndDirectories();
            foreach (ElementsOfDisc discElement in DiscElementsNum1)
            {
                ViewOfListItems discElementView = new ViewOfListItems(discElement);
                listBoxNumber1.Items.Add(discElementView);
                discElementView.refreshlist += RefreshListAfterDelete;
                discElementView.AfterOpenNum1 += WindowAfterOpen;
                discElementView.CopyFileNum1 += GetPathAfterClick;
            }

        }

        /// <summary>
        /// pobiera ścieżke po zaznaczeniu elementu na listboxie
        /// </summary>
        /// <param name="discElement"></param>
        /// <param name="NumberOfList"></param>
        private void GetPathAfterClick(ElementsOfDisc discElement, int NumberOfList)
        {

            if (NumberOfList == 1)
            {
                elementdisc = discElement;
                forcheck = 1;
                PathNewDir.Text = textBoxPathNum1.Text;
            }
            else
            {
                elementdisc = discElement;
                forcheck = 2;
                PathNewDir.Text = textBoxPathNum2.Text;
            }
        }

        /// <summary>
        /// odświeża drugie okno 
        /// </summary>
        /// <param name="path"></param>
        private void RefreshListNum2(string path)
        {


            MyDirectory MyDir2 = new MyDirectory(path);
            listBoxNumber2.Items.Clear();
            List<ElementsOfDisc> DiscElementsNum2 = MyDir2.GetAllFilesAndDirectories();
            foreach (ElementsOfDisc discElement in DiscElementsNum2)
            {
                ViewOfListItems discElementView = new ViewOfListItems(discElement);

                listBoxNumber2.Items.Add(discElementView);
                discElementView.refreshlist += RefreshListAfterDelete;
                discElementView.AfterOpenNum2 += WindowAfterOpen;
                discElementView.CopyFileNum2 += GetPathAfterClick;

            }
        }

        //private void BackWindow(int NumberOfList)
        //{
        //    MyDirectory MyDir = new MyDirectory(textBoxPathNum1.Text);
        //    if (NumberOfList == 1)
        //    {
        //        RefreshListNum1(MyDir.ParentDir);
        //        textBoxPathNum1.Text = MyDir.ParentDir;
        //    }
        //    else
        //    {
        //        RefreshListNum2(MyDir.ParentDir);
        //        textBoxPathNum2.Text = MyDir.ParentDir;
        //    }
        //}


        /// <summary>
        /// Wyświetlanie plików w oknie po podwójnym kliknięciu (otwarcie) 
        /// </summary>
        /// <param name="discElement"></param>
        /// <param name="NumberOfList"></param>
        private void WindowAfterOpen(ElementsOfDisc discElement, int NumberOfList)
        {

            if (NumberOfList == 1)
            {
                RefreshListNum1(discElement.DiscPath);
                textBoxPathNum1.Text = discElement.DiscPath;
                PathNewDir.Text = textBoxPathNum1.Text;
            }
            else
            {
                RefreshListNum2(discElement.DiscPath);
                textBoxPathNum2.Text = discElement.DiscPath;
                PathNewDir.Text = textBoxPathNum2.Text;
            }

        }

        /// <summary>
        /// odświeżanie listy po usunięciu elementu
        /// </summary>
        private void RefreshListAfterDelete()
        {
            RefreshListNum1(textBoxPathNum1.Text);
            RefreshListNum2(textBoxPathNum2.Text);
        }



        /// <summary>
        /// wylistowanie plików i folderów dla pierwszego okna 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDiscElements1_Click(object sender, RoutedEventArgs e)
        {
            RefreshListNum1(textBoxPathNum1.Text);
            PathNewDir.Text = textBoxPathNum1.Text;
        }

        /// <summary>
        /// wylistowanie plików i folderów dla drugiego okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDiscElements2_Click(object sender, RoutedEventArgs e)
        {
            RefreshListNum2(textBoxPathNum2.Text);
            PathNewDir.Text = textBoxPathNum2.Text;
        }


        /// <summary>
        /// pobieranie wartości z textboxów i stworzenie nowego folderu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewDir(object sender, RoutedEventArgs e)
        {
            string name = PathNewDir.Text + "\\" + NameOfNewDir.Text;
            DirectoryInfo di = Directory.CreateDirectory(name);
            RefreshListNum1(textBoxPathNum1.Text);
            RefreshListNum2(textBoxPathNum2.Text);
        }

        /// <summary>
        /// Cofanie pierwszej strony
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backPage1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyDirectory mydir = new MyDirectory(textBoxPathNum1.Text);
                RefreshListNum1(mydir.ParentDir);
                textBoxPathNum1.Text = mydir.ParentDir;
                
            }
            catch (ArgumentNullException)
            {
                RefreshListNum1(textBoxPathNum1.Text);
            }
         }

        /// <summary>
        /// cofanie drugiej strony
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backPage2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyDirectory mydir = new MyDirectory(textBoxPathNum2.Text);
                RefreshListNum2(mydir.ParentDir);
                textBoxPathNum2.Text = mydir.ParentDir;
                
                
            }
            catch (ArgumentNullException)
            {
                RefreshListNum2(textBoxPathNum2.Text);
            }
        }

        /// <summary>
        /// Wyszukiwanie plików i katalogów po nazwie
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchElement_Click(object sender, RoutedEventArgs e)
        {
           
            if (NameOfNewDir.Text != "" && NameOfNewDir.Text != "Podaj Nazwe")
            {
                if (PathNewDir.Text == textBoxPathNum1.Text)
                {
                    string a = textBoxPathNum1.Text;
                    string[] result = new string[Directory.GetFileSystemEntries(PathNewDir.Text, NameOfNewDir.Text).Length];
                    result = Directory.GetFileSystemEntries(PathNewDir.Text, NameOfNewDir.Text);
                    listBoxNumber1.Items.Clear();
                    try
                    {
                        if (result[0] != null)
                        {
                            textBoxPathNum1.Text = textBoxPathNum1.Text;
                        }
                    }
                    catch(System.IndexOutOfRangeException)
                    {
                        RefreshListNum1(textBoxPathNum1.Text);
                        MessageBox.Show("W podanym katalogu element o takiej nazwie nie istnieje ! ");
                        
                        
                    }
                   
                    {
                        for (int i = 0; i < result.Length; i++)
                        {
                            MyDirectory mydir = new MyDirectory(result[i]);

                            ViewOfListItems discElementView = new ViewOfListItems(mydir.PathMyDir);

                            listBoxNumber1.Items.Add(discElementView);
                            discElementView.refreshlist += RefreshListAfterDelete;
                            discElementView.AfterOpenNum1 += WindowAfterOpen;
                            textBoxPathNum1.Text = mydir.Path2;
                        }
                    }
                    
                }
                else
                {
                    string[] result = new string[Directory.GetFileSystemEntries(PathNewDir.Text, NameOfNewDir.Text).Length];
                    result = Directory.GetFileSystemEntries(PathNewDir.Text, NameOfNewDir.Text);
                    listBoxNumber2.Items.Clear();
                    try
                    {
                        if (result[0] != null)
                        {
                            textBoxPathNum2.Text = textBoxPathNum2.Text;
                        }
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        RefreshListNum2(textBoxPathNum2.Text);
                        MessageBox.Show("W podanym katalogu element o takiej nazwie nie istnieje ! ");

                    }
                    {
                        for (int i = 0; i < result.Length; i++)
                        {
                            MyDirectory mydir = new MyDirectory(result[i]);

                            ViewOfListItems discElementView = new ViewOfListItems(mydir.PathMyDir);

                            listBoxNumber2.Items.Add(discElementView);
                            discElementView.refreshlist += RefreshListAfterDelete;
                            discElementView.AfterOpenNum2 += WindowAfterOpen;
                            textBoxPathNum2.Text = mydir.Path2;
                        }
                    }
                   
                }

            }

        }

        
        /// <summary>
        /// kopiowanie plików i folderów z pierwsze do drugiego okna i na odwrót 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (forcheck == 1)
            {
                try
                {
                    File.Copy(elementdisc.DiscPath, textBoxPathNum2.Text + "\\" + elementdisc.Name);
                }
                catch (System.NullReferenceException)
                {
                    DirectoryCopyExample.CopyDirectory.DirectoryCopy(elementdisc.DiscPath, textBoxPathNum2.Text + "\\" + elementdisc.Name, true);
                    RefreshListNum2(textBoxPathNum2.Text);
                }
                catch (System.UnauthorizedAccessException)
                {
                    DirectoryCopyExample.CopyDirectory.DirectoryCopy(elementdisc.DiscPath, textBoxPathNum2.Text + "\\" + elementdisc.Name, true);
                    RefreshListNum2(textBoxPathNum2.Text);
                }
                catch (IOException)
                {
                    MessageBox.Show("Plik lub katalog o takiej nazwie już istnieje !");
                }
                RefreshListNum2(textBoxPathNum2.Text);
            }
            else 
            {
                try
                {
                    File.Copy(elementdisc.DiscPath, textBoxPathNum1.Text + "\\" + elementdisc.Name);
                }
                catch (System.UnauthorizedAccessException)
                {
                    DirectoryCopyExample.CopyDirectory.DirectoryCopy(elementdisc.DiscPath, textBoxPathNum1.Text + "\\" + elementdisc.Name, true);
                    RefreshListNum1(textBoxPathNum1.Text);

                }
                catch(System.NullReferenceException)
                {
                    DirectoryCopyExample.CopyDirectory.DirectoryCopy(elementdisc.DiscPath, textBoxPathNum1.Text + "\\" + elementdisc.Name, true);
                    RefreshListNum1(textBoxPathNum1.Text);
                }
                catch (IOException)
                {
                  MessageBox.Show("Plik lub katalog o takiej nazwie już istnieje !");
                }

                RefreshListNum1(textBoxPathNum1.Text);
            }

        }

        /// <summary>
        /// edytownaie pliku 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (elementdisc is MyFile)
            {

                try
                {
                    Process.Start(elementdisc.DiscPath);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

        }

        /// <summary>
        /// sortowanie po czasie utworzenia pierwsza liste 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSoryByCreationTime_Click(object sender, RoutedEventArgs e)
        {

            listBoxNumber1.Items.Clear();
            MyDirectory mydir = new MyDirectory(textBoxPathNum1.Text);
            IOrderedEnumerable<ElementsOfDisc> result = mydir.GetFilesAndDirectoriesSoryByCreationTime();
            foreach (ElementsOfDisc file in result)
            {
                ViewOfListItems discElementView = new ViewOfListItems(file);
                listBoxNumber1.Items.Add(discElementView);
                discElementView.refreshlist += RefreshListAfterDelete;
                discElementView.AfterOpenNum1 += WindowAfterOpen;
                discElementView.CopyFileNum1 += GetPathAfterClick;
            }
        }

        /// <summary>
        /// sortuje po imieniu pierwsza liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSortByName_Click(object sender, RoutedEventArgs e)
        {
            RefreshListNum1(textBoxPathNum1.Text);
        }

        /// <summary>
        /// sortuje po imieniu druga liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSortByNameList2_Click(object sender, RoutedEventArgs e)
        {
            RefreshListNum2(textBoxPathNum2.Text);
        }

        /// <summary>
        /// sortuje po czasie utworzenia druga liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSoryByCreationTimeList2_Click(object sender, RoutedEventArgs e)
        {
            listBoxNumber2.Items.Clear();
            MyDirectory mydir = new MyDirectory(textBoxPathNum2.Text);
            IOrderedEnumerable<ElementsOfDisc> result = mydir.GetFilesAndDirectoriesSoryByCreationTime();
            foreach (ElementsOfDisc file in result)
            {
                
                ViewOfListItems discElementView = new ViewOfListItems(file);
                listBoxNumber2.Items.Add(discElementView);
                discElementView.refreshlist += RefreshListAfterDelete;
                discElementView.AfterOpenNum2 += WindowAfterOpen;
                discElementView.CopyFileNum2 += GetPathAfterClick;
            }
        }
    }

}
