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

        // counter
        int counter = 0;
       

        public void loadDxfFile()
        {
            // path of dxf file
            Console.Write("Podaj sciezke do plikow *.dxf (np. C:\\Users\\user\\Desktop\\): ");
            filesPath = Directory.GetFiles(Console.ReadLine(), "*.dxf");

            int buildNo = 18802;

            foreach (string file in filesPath)
            {
                // stringbuilder for each plate
                StringBuilder stringBuilder = new StringBuilder();

                string lnB, lnP, lnQ, lnM, lnT, lnDesc;
                string lnPTmp = "";
                int blockNo = -100;
                string plateDescrtiption = "";

                // Read file using StreamReader. Reads file line by line
                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnB = fileText.ReadLine()) != null)
                    {
                        if (lnB.StartsWith("{[B]"))
                        {
                            stringBuilder.Append("POZ=").Append(buildNo).Append("-").Append(lnB.Substring(4).Trim('}')).Append("-");
                            blockNo = Int32.Parse(lnB.Substring(4).Trim('}'));
                        }
                    }
                }

                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnP = fileText.ReadLine()) != null)
                        {
                            if (lnP.StartsWith("{[P]"))
                            {
                            string tmp = lnP.Substring(4).Trim('}');
                                stringBuilder.Append(lnP.Substring(4).Trim('}')).Append(" ");
                                lnPTmp = lnP.Trim('{').Substring(0,3)+stringBuilder.ToString().Substring(4);
                                plateDescrtiption = stringBuilder.ToString().Substring(4);
                                Console.WriteLine("[P]: " + lnPTmp);
                                Console.WriteLine("plate desc: " + plateDescrtiption);
                                
                            }
                        }
                    }


                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnQ = fileText.ReadLine()) != null)
                    {
                        if (lnQ.StartsWith("{[Q]"))
                        {
                            stringBuilder.Append("SZT=").Append(lnQ.Substring(4).Trim('}')).Append(" ");
                        }
                    }
                }


                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnM = fileText.ReadLine()) != null)
                    {
                        if (lnM.StartsWith("{[M]"))
                        {
                            stringBuilder.Append("MAT=").Append(lnM.Substring(4).Trim('}')).Append(" ");
                        }
                    }
                }


                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnT = fileText.ReadLine()) != null)
                    {
                        if (lnT.StartsWith("{[T]"))
                        {
                            stringBuilder.Append("GR=").Append(lnT.Substring(4).Trim('}'));
                        }
                    }
                }

                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnDesc = fileText.ReadLine()) != null)
                    {
                        if (lnDesc.StartsWith(blockNo.ToString()))
                        {
                            
                        }
                    }
                }

                componentsList.Add(stringBuilder.ToString());

                    dxfWriter.writeDxf(file, stringBuilder.ToString(), 5.0);
                    dxfWriter.writeDxf(file, lnPTmp, 5.0);
                    dxfWriter.writeDxf(file, plateDescrtiption, 10.0);

                counter++;

            }

            Console.WriteLine();

            int dxfCounter = 1;

            foreach (string item in componentsList)
            {
                Console.WriteLine(dxfCounter + ": " + item);
                Console.WriteLine();
                dxfCounter++;
            }
                Console.WriteLine("Łącznie plików: " + counter);
           
        }
    }
}
