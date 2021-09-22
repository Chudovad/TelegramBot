using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace TelegramBot
{
    class Activity
    {
        private static string UseApi()
        {
            string url = "http://www.boredapi.com/api/activity/";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string respons;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                respons = streamReader.ReadToEnd();
            }

            return respons;
        }

        public List<string> PrintActivity()
        {
            List<string> listActivity = new List<string>();
            ActivityRespons activityRespons = JsonConvert.DeserializeObject<ActivityRespons>(UseApi());

            listActivity.Add(activityRespons.activity);
            listActivity.Add(activityRespons.type);
            listActivity.Add(activityRespons.participants);

            return listActivity;
        }

    }
}
