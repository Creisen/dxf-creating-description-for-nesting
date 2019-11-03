
using System.IO;


namespace dxf_creating_description_for_nesting.Entity.text_changer
{
    class PlateShortDescription
    {
        public void changeText(string fileName, string lnP, string oldPlateDesc, string plateDesc)
        {
            int posCounter = 0;
            int stoppedPosCounter = 0;

            // Read file using StreamReader. Reads file line by line
            using (StreamReader fileText = new StreamReader(fileName))
            {
                while ((lnP = fileText.ReadLine()) != null)
                {
                    if (lnP.StartsWith(oldPlateDesc))
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
                        oldPlateDesc = fileText.ReadLine();
                        //oldPlateDesc = plateDesc
                    }
                }
            }

            string text = File.ReadAllText(fileName);
            text = text.Replace(oldPlateDesc, plateDesc);
            File.WriteAllText(fileName, text);

        }
    }
}
