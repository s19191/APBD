﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace cw2
{
    class Program
    {
        public static void ErrorLOgging(Exception ex)
        {
            String logpath = @"D:\APBD\cw2\Log.txt";
            if (!File.Exists(logpath))
            {
                File.Create(logpath).Dispose();
            }
            StreamWriter sw = File.AppendText(logpath);
            sw.WriteLine("START " + DateTime.Now);
            sw.WriteLine("Error Messange " + ex.Message);
            sw.WriteLine("END " + DateTime.Now);
            sw.Close();
        }
        static void Main(string[] args)
        {
            try
            {
                String adresCSV = Console.ReadLine(); //C:\Users\kfaff\Desktop\APBD\2_csvtoxml\dane.csv
                if (String.IsNullOrEmpty(adresCSV))
                {
                    adresCSV = "data.csv";
                }
                String adresDolcelowy = Console.ReadLine(); //D:\APBD\cw2\
                if (String.IsNullOrEmpty(adresDolcelowy))
                {
                    adresDolcelowy = "";
                }
                String formatDanych = Console.ReadLine(); //xml
                if (String.IsNullOrEmpty(formatDanych))
                {
                    formatDanych = "xml";
                }

                if (File.Exists(adresCSV) && Directory.Exists(adresDolcelowy))
                {
                    string[] source = File.ReadAllLines(adresCSV);
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    for (int i = 0; i < source.Length; i++)
                    {
                        try
                        {
                            if (source[i].Split(",").Length == 9)
                            {
                                string[] tmp1 = new string[9];
                                string tmp2 = "";
                                string tmp3 = "";
                                bool czyNiePustaKolumna = true;
                                for (int j = 0; j < 9; j++)
                                {
                                    if (source[i].Split(",")[j].Equals(" ") || source[i].Split(",")[j].Equals(""))
                                    {
                                        czyNiePustaKolumna = false;
                                    }

                                    if (j == 0 || j == 1 || j == 5)
                                    {
                                        tmp2 += source[i].Split(",")[j] + ",";
                                    }
                                    else
                                    {
                                        if (j == 8)
                                        {
                                            tmp3 += source[i].Split(",")[j];
                                        }
                                        else
                                        {
                                            tmp3 += source[i].Split(",")[j] + ",";
                                        }
                                    }

                                    if (j == 8 && czyNiePustaKolumna)
                                    {
                                        try
                                        {
                                            dictionary.Add(tmp2, tmp3);
                                        }
                                        catch (ArgumentException e)
                                        {
                                            ErrorLOgging(new Exception("Powtarzający się student!" + source[i]));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("Błędne informacje o studencie!" + source[i]);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLOgging(ex);
                        }
                    }
                    
                    int k = 0;
                    List<string> tmp = new List<string>();
                    foreach (var keyAndVal in dictionary)
                    {
                        tmp.Add(keyAndVal.Key + keyAndVal.Value);
                        k++;
                    }

                    string[] filtred = tmp.ToArray();

                    switch (formatDanych)
                    {
                        case "xml":
                        {
                            xmlFormatFile.save(filtred, adresDolcelowy);
                            break;
                        }
                        case "json":
                        {
                            
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Nie obługiwany wyjściowy format plików");
                            break;
                        }
                    }
                }
                else
                {
                    if (!File.Exists(adresCSV))
                    {
                        throw new Exception("File does not exsist");
                    }
                    if (!Directory.Exists(adresDolcelowy))
                    {
                        throw new Exception("Directory does not exsit");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLOgging(ex);
            }
        }
    }
}