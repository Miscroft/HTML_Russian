using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Controls.Primitives;

namespace HTML_Russian
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Rail_of_Curtain = new List<string>();
        public List<string> Gulag = new List<string>();
        public string WebSite = "\\new_webpage.html";
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
            Propaganda();
            string path = Environment.CurrentDirectory + WebSite;
            Process.Start("msedge.exe", path);
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
                    else if (Rail[way] == "<п>")
                    {
                        int countryroad = -1;
                        for (int road = way + 1; road < Rail.Length; road++)
                        {
                            if (Rail[road] == "</п>")
                            {
                                countryroad = road;
                                break;
                            }
                        }
                        if (countryroad > way && countryroad < Rail.Length)
                        {
                            Gulag.Add("<p>");
                            for (int road = way + 1; road < countryroad; road++)
                            {
                                Gulag.Add(Rail[road]);
                            }
                            Gulag.Add("</p>");
                        }
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
                        if (countryroad > way && countryroad < Rail.Length)
                        {
                            Gulag.Add("<title>");
                            for (int road = way + 1; road < countryroad; road++)
                            {
                                Gulag.Add(Rail[road]);
                            }
                            Gulag.Add("</title>");
                        }
                    }
                }
            }
        }
        private void Propaganda()
        {
            using (StreamWriter dictatorship_of_proletariat = new StreamWriter("new_webpage.html"))
            {
                Gulag.ForEach(delegate (string penal_colony)
                    {
                        dictatorship_of_proletariat.Write(penal_colony);
                        dictatorship_of_proletariat.WriteLine();
                    });
            }
        }
            #region Row
            private void Delete_Row(object sender, MouseButtonEventArgs e)
        {
            ironcurtain = DeleteRow(e);
            Iron_curtain.ItemsSource = DeleteRow(e);
        }
        private List<IronCurtain> DeleteRow(MouseButtonEventArgs e)
        {
            if ((e.OriginalSource as FrameworkElement).DataContext != null)
            {
                string LineNumText = (e.OriginalSource as FrameworkElement).DataContext.ToString();

                List<IronCurtain> ironcurtain_man = new List<IronCurtain>();
                for (int rail = 0; rail < ironcurtain.Count; rail++)
                {
                    if ((rail + 1).ToString() != LineNumText)
                    {
                        ironcurtain_man.Add(ironcurtain[rail]);
                    }
                }
                ironcurtain = ironcurtain_man;
                return ironcurtain;
            }
            else
                return null;
        }

        private void Create_New_Row(object sender, MouseButtonEventArgs e)
        {
            ironcurtain = CreateRow(e);
            Iron_curtain.ItemsSource = ironcurtain;
        }
        private List<IronCurtain> CreateRow(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement shelter)
            {
                List<IronCurtain> ironcurtain_man = new List<IronCurtain>();
                for (int rail = 0; rail < ironcurtain.Count; rail++)
                {
                    ironcurtain_man.Add(ironcurtain[rail]);
                    if ((rail + 1).ToString() == shelter.DataContext.ToString())
                    {
                        IronCurtain new_ironcurtain = new IronCurtain();
                        ironcurtain_man.Add(new_ironcurtain);
                    }
                }
                ironcurtain = ironcurtain_man;
                return ironcurtain;
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
