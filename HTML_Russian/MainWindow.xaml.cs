using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;

namespace HTML_Russian
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Rail_of_Curtain = new List<string>();
        public List<string> Gulag = new List<string>();
        public string WebSite = "\\new webpage.html";
        List<IronCurtain> ironcurtain = new List<IronCurtain>();
        public class IronCurtain
        {
            public string LineCode { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            Iron_curtain.ItemsSource = LoadCollectionData();
            Iron_curtain.CanUserResizeRows = false;
        }

        private List<IronCurtain> LoadCollectionData()
        {
            ironcurtain = new List<IronCurtain>
            {
                new IronCurtain()
                {
                    LineCode = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\r\n"
                }
            };
            for (int line_input = 1; line_input < 30; line_input++)
            {
                switch (line_input)
                {
                    case 1:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "<!ДОКТИП нтмг>"
                        });
                        break;
                    case 2:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "<нтмг>"
                        });
                        break;
                    case 3:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "\t<голова>"
                        });
                        break;
                    case 4:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "\t<мета харсет=\"UTF-8\">"
                        });
                        break;
                    case 5:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "\t\t<заглавие> Да здравствует СССР! </заглавие>"
                        });
                        break;
                    case 6:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "\t</голова>"
                        });
                        break;
                    case 7:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "\t<тело>"
                        });
                        break;
                    case 8:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "\t</тело>"
                        });
                        break;
                    case 9:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = "</нтмг> \r\n"
                        });
                        break;
                    default:
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = " \r\n"
                        });
                        break;
                }
            }

            return ironcurtain;
        }

        #region Window behaviior

        private void MouseDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Close_window(object sender, MouseButtonEventArgs e)
        {
            HTML_Main.Close();
        }

        private void Max_window(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void Min_window(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized; 
        }

        private void Auto_line_count(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        #endregion

        private void Read_Button_Click(object sender, RoutedEventArgs e)
        {
            Rail_of_Curtain.Clear();
            int Iron_curtain_No = Iron_curtain.Items.Count;
            for (int ele = 0; ele < Iron_curtain_No; ele++)
            {
                TextBlock x = Iron_curtain.Columns[0].GetCellContent(Iron_curtain.Items[ele]) as TextBlock;
                if(x != null)
                    Rail_of_Curtain.Add(x.Text);
            }
            Translate_Text();
            string path = Environment.CurrentDirectory + WebSite;
            Process.Start("IExplore.exe", path);
        }
        private void Translate_Text()
        {
            foreach(string Rails in Rail_of_Curtain)
            {
                string[] SplitStrings = { "\t", "\n", " " };
                string[] Rail = Rails.Split(SplitStrings, StringSplitOptions.RemoveEmptyEntries);
                for(int way = 0; way < Rail.Length; way++)
                {
                    if (Rail[way] == "<!ДОКТИП" && Rail[way + 1] == "нтмг>")
                    {
                        Gulag.Add("<!DOCTYPE html>");
                    }
                    else if (Rail[way] == "</нтмг>")
                    {
                        Gulag.Add("<html>");
                    }
                    else if (Rail[way] == "</нтмг>")
                    {
                        Gulag.Add("</html>");
                    }
                    else if (Rail[way] == "<голова>")
                    {
                        Gulag.Add("<head>");
                    }
                    else if (Rail[way] == "</голова>")
                    {
                        Gulag.Add("</head>");
                    }
                    else if (Rail[way] == "<тело>")
                    {
                        Gulag.Add("<body>");
                    }
                    else if (Rail[way] == "</тело>")
                    {
                        Gulag.Add("</body>");
                    }
                    else if (Rail[way] == "<мета")
                    {
                        Gulag.Add("<meta ");
                        if (Rail[way + 1] == "харсет=\"UTF-8\">")
                        {
                            Gulag.Add("charset=\"utf-8\">");
                        }
                    }
                    else if (Rail[way] == "<заглавие>")
                    {
                        int countryroad = -1;
                        for (int road = way + 1; road < Rail.Length; road++)
                        {
                            if (Rail[road] == "</заглавие>")
                            {
                                countryroad = road;
                                break;
                            }
                        }
                        if (countryroad > -1)
                        {
                            Gulag.Add("<title>");
                            for (int road = way + 1; road < countryroad - 1; road++)
                            {
                                Gulag.Add(Rail[road]);
                            }
                            Gulag.Add("</title>");
                        }
                    }
                }
            }
        }
        #region Row
        private void Delete_Row(object sender, MouseButtonEventArgs e)
        {
            if (DeleteRow(e) != null)
            Iron_curtain.ItemsSource = DeleteRow(e);
        }
        private List<IronCurtain> DeleteRow(MouseButtonEventArgs e)
        {
            int LineNum = -1;
            string LineNumText = (e.OriginalSource as FrameworkElement).DataContext.ToString();
            ironcurtain = new List<IronCurtain>();
            if ((e.OriginalSource as FrameworkElement).DataContext != null)
            {
                LineNumText = (e.OriginalSource as FrameworkElement).DataContext.ToString();
                if (int.TryParse(LineNumText, out LineNum) && LineNum != -1)
                {
                    int Items_Counts = Iron_curtain.Items.Count - 1;
                    for (int line_input = 0; line_input < Items_Counts; line_input++)
                    {
                        string NewText = "";
                        if (Iron_curtain.Columns[0].GetCellContent(Iron_curtain.Items[line_input]) != null)
                        {
                            NewText = (Iron_curtain.Columns[0].GetCellContent(Iron_curtain.Items[line_input]) as TextBlock).Text;
                            if (line_input != LineNum - 1)
                            {
                                ironcurtain.Add(new IronCurtain()
                                {
                                    LineCode = NewText
                                });
                            }
                        }
                    }
                    return ironcurtain;
                }
                else
                    return null;
            }
            else
                return null;
        }

        private void Create_New_Row(object sender, MouseButtonEventArgs e)
        {
            if (CreateRow(e) != null)
            Iron_curtain.ItemsSource = CreateRow(e);            
        }
        private List<IronCurtain> CreateRow(MouseButtonEventArgs e)
        {
            int LineNum = -1;
            string LineNumText = "";
            ironcurtain = new List<IronCurtain>();
            if ((e.OriginalSource as FrameworkElement).DataContext != null)
            {
                LineNumText = (e.OriginalSource as FrameworkElement).DataContext.ToString();
                if (int.TryParse(LineNumText, out LineNum) && LineNum != -1)
                {
                    int Items_Counts = Iron_curtain.Items.Count - 1;
                    for (int line_input = 0; line_input < Items_Counts; line_input++)
                    {
                        string NewText = "";
                        if ((Iron_curtain.Columns[0].GetCellContent(Iron_curtain.Items[line_input])) != null)
                            NewText = (Iron_curtain.Columns[0].GetCellContent(Iron_curtain.Items[line_input]) as TextBlock).Text;
                        ironcurtain.Add(new IronCurtain()
                        {
                            LineCode = NewText
                        });
                        if (line_input == LineNum - 1)
                        {
                            ironcurtain.Add(new IronCurtain()
                            {
                                LineCode = " "
                            });
                        }
                    }
                    return ironcurtain;
                }
                else
                    return null;
            }
            else
                return null;
        }

        private void Create_New_Page(object sender, RoutedEventArgs e)
        {
            Iron_curtain.ItemsSource = LoadCollectionData();
        }
        #endregion

    }
}
