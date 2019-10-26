using System;
using System.IO;
using System.Text;

namespace dxf_creating_description_for_nesting.Entity.text_changer
{
    class BlockNameReader
    {
        
        public string readBlockNumber(string file, string lnB, StringBuilder stringBuilder, 
                                                                            int buildNo)
        {
            int counter = 0;
            int stopperCounter = 0;
            string oldBuildText = "oldBuild";
            string newBuildText = "newBuild";

            // Read file using StreamReader. Reads file line by line
            using (StreamReader fileText = new StreamReader(file))
            {
                while ((lnB = fileText.ReadLine()) != null)
                {
                    if (lnB.StartsWith("[B]"))
                    {
                        stopperCounter = counter;

                        stringBuilder.Append("POZ=").Append(buildNo).Append("-")
                                     .Append(lnB.Substring(3).Trim('}')).Append("-");
                    }
                    counter++;
                }
            }

            //using (StreamReader fileText = new StreamReader(file))
            //{

            //    counter = 0;
            //    while ((lnB = fileText.ReadLine()) != null)
            //    {
            //        counter++;
            //        if (counter == stopperCounter)
            //        {
            //            oldBuildText = fileText.ReadLine();
            //            newBuildText = oldBuildText.Substring(0,3)+buildNo.ToString();
            //        }
            //    }
            //}

            //    string text = File.ReadAllText(file);
            //    text = text.Replace(oldBuildText, newBuildText);
            //    File.WriteAllText(file, text);

            return stringBuilder.ToString();
        }
    }
}
