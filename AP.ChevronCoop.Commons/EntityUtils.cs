using System.ComponentModel;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Xml;

namespace AP.ChevronCoop.Commons
{
    public static class EntityUtils
    {

        public static Guid CreateCryptoSecureGuid()
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[16];
                provider.GetBytes(bytes);

                return new Guid(bytes);
            }
        }

        public static Guid GenerateSequentialId()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.UtcNow;

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }


        //public static string ToJson(this object @object)
        //{
        //    return JsonConvert.SerializeObject(@object,
        //    new JsonSerializerSettings
        //    {
        //        Formatting = Formatting.Indented,
        //        TypeNameHandling = TypeNameHandling.None,
        //        //ContractResolver = new  CamelCasePropertyNamesContractResolver(),
        //        ContractResolver = new DefaultContractResolver(),
        //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
        //        PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All
        //    });
        //}

        public static string ToJson(this object @object)
        {
            return System.Text.Json.JsonSerializer.Serialize(@object, new System.Text.Json.JsonSerializerOptions
            { WriteIndented = true });
        }


        public static string ToXml(this object @object)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(@object.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, @object);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }

        public static IDictionary<string, string> ToDictionary(this object source)
        {
            if (source == null)
                throw new NullReferenceException("Unable to convert anonymous object to a dictionary. The source anonymous object is null.");

            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                object value = property.GetValue(source);
                if (value != null)
                    dictionary.Add(property.Name, value.ToString());
            }
            return dictionary;
        }
    }
}