
using System.IO;


   public  class  ConnectionDB
    {
        public static string ConstrReader;
        public static void ReadConnection()
        {
            if (string.IsNullOrEmpty(ConstrReader) || string.IsNullOrWhiteSpace(ConstrReader))
            {
       using (FileStream fs = new FileStream("Connection.dat", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader r = new StreamReader(fs))
                {
                    string con= r.ReadToEnd();
                    ConstrReader = con;
                    //return con;
                }
            }
            }
     

        }

      

    }

