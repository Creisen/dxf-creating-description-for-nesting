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

        // stringbuilder for each plate
        StringBuilder stringBuilder = new StringBuilder();

        public void loadDxfFile()
        {
            // path of dxf file
            Console.Write("Podaj sciezke do plikow *.dxf (np. C:\\Users\\user\\Desktop\\): ");
            filesPath = Directory.GetFiles(Console.ReadLine(), "*.dxf");

           

            
            bool isBinary;
            foreach (string file in filesPath)
            {

                string line;



                while (line = file.ReadLine()) != null
                {
                    if (file)
                    {

                    }
                }
                
                
            }
            
        }
    }
}
