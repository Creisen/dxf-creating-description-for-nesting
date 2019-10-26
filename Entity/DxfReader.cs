using netDxf.Tables;
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace dxf_creating_description_for_nesting.Entity
{
    public class DxfReader
    {

        // dxf filename
        private string[] filesPath;

        // array of components
        ArrayList componentsList = new ArrayList();

        // DxfWriter object
        DxfWriter dxfWriter = new DxfWriter();

        //Layers
        Layer layer5 = new Layer("5");
        Layer layer8 = new Layer("8");

        // counter
        int counter = 1;

        // stringbuilder for each plate
        StringBuilder stringBuilder = new StringBuilder();

        string lnB = "", lnP = "", lnQ = "", lnM = "", lnT = "";

        string lnPTmp = "";
        string plateDescrtiption = "";


        public void loadDxfFile()
        {
            // path of dxf file
            Console.Write("Podaj sciezke do plikow *.dxf (np. C:\\Users\\user\\Desktop\\): ");
            filesPath = Directory.GetFiles(Console.ReadLine(), "*.dxf");

            Console.Write("Wpisz numer budowy (np. 18802): ");
            int buildNo = Int32.Parse(Console.ReadLine());

            Console.WriteLine();

            foreach (string file in filesPath)
            {

                Console.WriteLine(counter + ": Plik: " + file);

                // block number writer
                text_changer.BlockNameReader block = new text_changer.BlockNameReader();
                string newBlockNo = block.readBlockNumber(file, lnB, stringBuilder, buildNo);

                // position code changer
                text_changer.PositionNameReader positionReader = new text_changer.PositionNameReader();
                positionReader.positionText(file, lnP, stringBuilder, lnPTmp, plateDescrtiption);

                

                // Read file using StreamReader. Reads file line by line
                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnQ = fileText.ReadLine()) != null)
                    {
                        if (lnQ.StartsWith("[Q]"))
                        {
                            stringBuilder.Append("SZT=").Append(lnQ.Substring(3).Trim('}')).Append(" ");
                        }
                    }
                }

                // Read file using StreamReader. Reads file line by line
                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnM = fileText.ReadLine()) != null)
                    {
                        if (lnM.StartsWith("[M]"))
                        {
                            stringBuilder.Append("MAT=").Append(lnM.Substring(3).Trim('}')).Append(" ");
                        }
                    }
                }

                // Read file using StreamReader. Reads file line by line
                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnT = fileText.ReadLine()) != null)
                    {
                        if (lnT.StartsWith("[T]"))
                        {
                            stringBuilder.Append("GR=").Append(lnT.Substring(3).Trim('}'));
                        }
                    }
                }
    

                    componentsList.Add(stringBuilder.ToString());
                    Console.WriteLine("pozycja: " + stringBuilder.ToString());
                    Console.WriteLine();

                    dxfWriter.writeDxf(file, stringBuilder.ToString(), 5.0, layer8);
                    dxfWriter.writeDxf(file, lnPTmp, 5.0, layer8);
                    dxfWriter.writeDxf(file, plateDescrtiption, 10.0, layer5);

                counter++;
            }

            Console.WriteLine();
            Console.WriteLine("Łącznie plików: " + (counter-1));
            Console.WriteLine("Wciśnij dowolny klawisz, aby zakończyć działanie programu");
           
        }


        
    }
}
