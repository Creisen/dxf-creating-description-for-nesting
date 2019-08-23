using netDxf;
using netDxf.Entities;
using netDxf.Header;
using netDxf.Tables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dxf_creating_description_for_nesting.Entity
{
    public class DxfReader
    {

        // dxf filename
        private string[] filesPath;

        // array of components
        ArrayList componentsList = new ArrayList();

       

        public void loadDxfFile()
        {
            // path of dxf file
            Console.Write("Podaj sciezke do plikow *.dxf (np. C:\\Users\\user\\Desktop\\): ");
            filesPath = Directory.GetFiles(Console.ReadLine(), "*.dxf");      


            foreach (string file in filesPath)
            {
                // stringbuilder for each plate
                StringBuilder stringBuilder = new StringBuilder();

                string lnB, lnP, lnQ, lnM, lnT;

                // Read file using StreamReader. Reads file line by line
                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnB = fileText.ReadLine()) != null)
                    {
                        if (lnB.StartsWith("{[B]"))
                        {
                            stringBuilder.Append("POZ=18802-").Append(lnB.Substring(4).Trim('}')).Append("-");
                        }
                    }
                }

                using (StreamReader fileText = new StreamReader(file))
                {
                    while ((lnP = fileText.ReadLine()) != null)
                        {
                            if (lnP.StartsWith("{[P]"))
                            {
                                stringBuilder.Append(lnP.Substring(4).Trim('}')).Append(" ");
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
                
                    componentsList.Add(stringBuilder.ToString());
            }

            foreach (string item in componentsList)
            {
                Console.WriteLine(item);
                Console.WriteLine();
                Console.ReadKey();
            }
           
        }
    }
}
