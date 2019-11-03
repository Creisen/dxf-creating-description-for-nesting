using System.IO;
using System.Text;

namespace dxf_creating_description_for_nesting.Entity.text_changer
{
    class OldBlockNameReader
    {
        public string readBlockNumber(string file, string lnB)
        {
            int counter = 0;
            int stopperCounter = 0;
            string oldBuildText = "oldBuild";

            // Read file using StreamReader. Reads file line by line
            using (StreamReader fileText = new StreamReader(file))
            {
                while ((lnB = fileText.ReadLine()) != null)
                {
                    if (lnB.StartsWith("[B]"))
                    {
                        stopperCounter = counter;
                        break;
                    }
                    counter++;
                }
            }

            using (StreamReader fileText = new StreamReader(file))
            {

                counter = 0;
                while ((lnB = fileText.ReadLine()) != null)
                {
                    counter++;
                    if (counter == stopperCounter)
                    {
                        oldBuildText = fileText.ReadLine();
                    }
                }
            }

            return oldBuildText.Substring(3);
        }
    }
}
