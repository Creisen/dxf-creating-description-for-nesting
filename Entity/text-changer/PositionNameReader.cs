using System;
using System.IO;
using System.Text;

namespace dxf_creating_description_for_nesting.Entity.text_changer
{
    class PositionNameReader
    {
        public void positionText(string fileName, string lnP, StringBuilder stringBuilder
                                 , string lnPTmp, string plateDescrtiption)
        {
            int posCounter = 0;
            int stoppedPosCounter = 0;
            string oldPositionText = "oldPosition";
            string newPositionText = "newPosition";

            // Read file using StreamReader. Reads file line by line
            using (StreamReader fileText = new StreamReader(fileName))
            {
                while ((lnP = fileText.ReadLine()) != null)
                {
                    if (lnP.StartsWith("[P]"))
                    {
                        stoppedPosCounter = posCounter;

                        stringBuilder.Append(lnP.Substring(3).Trim('}')).Append(" ");
                        lnPTmp = lnP.Trim('{').Substring(0, 3) + stringBuilder.ToString().Substring(4);
                        plateDescrtiption = stringBuilder.ToString().Substring(4);
                        Console.WriteLine("[P]: " + lnPTmp);
                        Console.WriteLine("plate desc: " + plateDescrtiption);
                    }
                    posCounter++;
                }
            }

            using (StreamReader fileText = new StreamReader(fileName))
            {

                posCounter = 0;
                while ((lnP = fileText.ReadLine()) != null)
                {
                    posCounter++;
                    if (posCounter == stoppedPosCounter)
                    {
                        oldPositionText = fileText.ReadLine();
                        newPositionText = lnPTmp;
                    }
                }
            }

            string text = File.ReadAllText(fileName);
            text = text.Replace(oldPositionText, newPositionText);
            File.WriteAllText(fileName, text);

            // return stringBuilder.ToString();
        }
    }
}
