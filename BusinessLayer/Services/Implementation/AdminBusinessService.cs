using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLayer.Services.Implementation
{
    public class AdminBusinessService : IAdminBusinessService
    {
        public LogsModel GetLogs()
        {
            string file = $"{Directory.GetCurrentDirectory()}\\logs\\log_{DateTime.Today.ToShortDateString()}.txt";
            string date = String.Concat(@DateTime.Today.ToString("yyyy"), "-", @DateTime.Today.ToString("MM"), "-", @DateTime.Today.ToString("dd"));

            return ReturnLogsModel(date, file);
        }

        public LogsModel GetLogsByDate(LogsModel logs)
        {
            string date = String.Concat(logs.Date.Substring(8), ".", logs.Date.Substring(5).Remove(2), ".", logs.Date.Remove(4));
            string file = $"{Directory.GetCurrentDirectory()}\\logs\\log_{date}.txt";

            return ReturnLogsModel(date, file);
        }

        private LogsModel ReturnLogsModel(string date, string file)
        {
            string text = "За указанную дату нет логов!";
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch { };

            LogsModel model = new LogsModel
            {
                Date = date,
                Text = text
            };

            return model;
        }
    }
}
