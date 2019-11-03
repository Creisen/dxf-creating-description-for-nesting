using System;
using System.IO;
using System.Text;

namespace dxf_creating_description_for_nesting.Entity.text_changer
{
    class OldPositionNameReader
    {
        public string positionText(string fileName, string lnP)
        {
            int posCounter = 0;
            int stoppedPosCounter = 0;
            string oldPositionText = "oldPosition";

            // Read file using StreamReader. Reads file line by line
            using (StreamReader fileText = new StreamReader(fileName))
            {
                while ((lnP = fileText.ReadLine()) != null)
                {
                    if (lnP.StartsWith("[P]"))
                    {
                        stoppedPosCounter = posCounter;
                        break;
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
                    }
                }
            }

            return oldPositionText.Substring(9);
        }
    }
}
