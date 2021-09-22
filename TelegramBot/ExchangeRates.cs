using System;
using System.Collections.Generic;
using System.Text;


namespace TelegramBot
{
    class ExchangeRates
    {
        public string PrintSend(String msg)
        {
            string print = "Курс рубля: " + ExchangeRate() +
                    "\r\nМои БО: " + msg +
                    "\r\nМои БО в RUB: " + Math.Round(ConvertBOtoRUB(msg), 2)  +
                    "\r\nRUB в BYN: " + Math.Round(ConvertRUBtoBYN(msg), 2) +
                    "\r\nНалог: " + Math.Round(CalculationTax(ConvertRUBtoBYN(msg)), 2) +
                    "\r\nКонечная зп: " + Math.Round(CalculationFinalWage(msg), 2);
            return print;
        }

        private static double ExchangeRate()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            String Response = wc.DownloadString("https://mogilev.bankibel.by/kursy-valut/rubl");

            String RateString = System.Text.RegularExpressions.Regex.Match(Response, @"<td class=""rur"">([0-9]+\.[0-9]+)</td>").Groups[1].Value.Replace(".", ",");
            return Convert.ToDouble(RateString);
        }
        private static double ConvertBOtoRUB(String msg)
        {
            String textBo = msg;
            double bo = Convert.ToDouble(textBo);
            double rub = bo * 1.6;
            return rub;
        }
        private static double ConvertRUBtoBYN(String msg)
        {
            double byn = ConvertBOtoRUB(msg) * ExchangeRate() / 100;
            return byn;
        }

        private static double CalculationTax(double wage)
        {
            double tax = 0.13 * wage;
            return tax;
        }
        private static double CalculationFinalWage(String msg)
        {
            return CalculationTax(ConvertRUBtoBYN(msg)) + ConvertRUBtoBYN(msg);
        }

    }
}
