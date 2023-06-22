using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.ViewerConsoleApp
{
    internal class Program
    {
        static public void Main(String[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Neplatný počet argumentov.");
            }

            //najdem povinny arg
            string? input = null;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--input")
                {
                    //ak je --input posledny arg, tak je chyba lebo posledny arg by musela byt cesta (--input C:/../../subor.csv)
                    if (i == args.Length - 1)
                    {
                        Console.WriteLine("Neplatne argumenty");
                    }
                    input = args[i + 1];
                }
            }
            if (input == null)
            {
                Console.WriteLine("Chyba povinny argument");
                return;
            }

            //najdem volitelne arg
            string? name = null;
            string? position = null;
            string? mainWorkplace = null;
            string? output = null;
            for (int i = 0; i < args.Length; i++)
            {
                int offset;
                switch (args[i])
                {
                    case "--name":
                        offset = 1;
                        name = args[i + offset];
                        offset++;
                        //ak ma parameter prikazu --name v sebe medzery (npr: --name veduci katedry --position ...)
                        while ((i + offset) < args.Length && !args[i + offset].StartsWith("--")) //POZOR zalezi na poradi vyhodnotenia podmienok (najprv (i + offset) < args.Length)
                        {
                            name += args[i + offset];
                            name += " ";
                            offset++;
                        }
                        break;
                    case "--position":
                        offset = 1;
                        position = args[i + offset];
                        offset++;
                        //ak ma parameter prikazu --name v sebe medzery (npr: --name veduci katedry --position ...)
                        while ((i + offset) < args.Length && !args[i + offset].StartsWith("--")) //POZOR zalezi na poradi vyhodnotenia podmienok (najprv (i + offset) < args.Length) 
                        {
                            position += " ";
                            position += args[i + offset];
                            offset++;
                        }
                        break;
                    case "--main-workplace":
                        offset = 1;
                        mainWorkplace = args[i + offset];
                        offset++;
                        //ak ma parameter prikazu --name v sebe medzery (npr: --name veduci katedry --position ...)
                        while ((i + offset) < args.Length && !args[i + offset].StartsWith("--")) //POZOR zalezi na poradi vyhodnotenia podmienok (najprv (i + offset) < args.Length) 
                        {
                            mainWorkplace += " ";
                            mainWorkplace += args[i + offset];
                            offset++;
                        }
                        break;
                    case "--output":
                        output = args[i + 1];
                        break;
                    default:
                        break;
                }
            }

            if (!File.Exists(input))
            {
                Console.WriteLine("Zadaná cesta k --input json súboru je neplatná.");
            }

            //tu startni appku 
            ResultViewer resultViewer = new ResultViewer(input, name, mainWorkplace, position, output);
            resultViewer.viewResult();

        }




        // zakomentovany kod: sofistikovana kontrola args 

        //static public void Main(String[] args)
        //{
        //    if (args.Length < 2)
        //    {
        //        Console.WriteLine("Neplatný počet argumentov.");
        //    }

        //    //najdem povinny arg
        //    string? input = null;
        //    for (int i = 0; i < args.Length; i++)
        //    {
        //        if (args[i] == "--input")
        //        {
        //            //ak je --input posledny arg, tak je chyba lebo posledny arg by musela byt cesta (--input C:/../../subor.csv)
        //            if (i == args.Length - 1)
        //            {
        //                Console.WriteLine("Neplatne argumenty");
        //            }
        //            input = args[i + 1];
        //        }
        //    }
        //    if (input == null)
        //    {
        //        Console.WriteLine("Chyba povinny argument");
        //        return;
        //    }

        //    //najdem volitelne arg
        //    string? name = null;
        //    string? position = null;
        //    string? mainWorkplace = null;
        //    string? output = null;
        //    for (int i = 0; i < args.Length; i++)
        //    {
        //        switch (args[i])
        //        {
        //            case "--name":
        //                if (i + 1 <= args.Length - 1)
        //                {
        //                    name = args[i + 1];
        //                    i++;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Neplatne argumenty");
        //                }
        //                break;
        //            case "--position":
        //                if (i + 1 <= args.Length - 1)
        //                {
        //                    position = args[i + 1];
        //                    i++;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Neplatne argumenty");
        //                }
        //                break;
        //            case "--main-workplace":
        //                if (i + 1 <= args.Length - 1)
        //                {  
        //                    mainWorkplace = args[i + 1];
        //                    i++;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Neplatne argumenty");
        //                }
        //                break;
        //            case "--output":
        //                //if (i == args.Length - 1)
        //                //{
        //                //    Console.WriteLine("Neplatne argumenty");
        //                //}
        //                //output = args[i + 1];
        //                //i++;
        //                if (i + 1 <= args.Length - 1)
        //                {
        //                    output = args[i + 1];
        //                    i++;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Neplatne argumenty");
        //                }
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    if (!File.Exists(input))
        //    {
        //        Console.WriteLine("Zadaná cesta k --input json súboru je neplatná.");
        //    }

        //    //tu startni appku 
        //    ResultViewer resultViewer = new ResultViewer(input, name, mainWorkplace, position, output);
        //    resultViewer.viewResult();



        //}


    }
}
