﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace APIGateway
{
    public class JsonLoader
    {

        public static T LoadFromFile<T>(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        public static T Deserialize<T>(object jsonObject)
        {
            return JsonConvert.DeserializeObject<T>(Convert.ToString(jsonObject));
        }

    }
}
