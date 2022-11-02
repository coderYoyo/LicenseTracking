﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LicenseTracking.Converters
{
    public class ImagePathConverter : IValueConverter
    {
         int days = 0;
         public ImagePathConverter()
        {
            DAL dal = new DAL();   
            days = dal.GetDaysRemainingToExpire();
        }

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string licExpiryDate = value.ToString().Substring(value.ToString().IndexOf("LicenseExpirationDate")).Split('=')[1].Split(',')[0].Trim().Split(' ')[0]; // rowItem.Row.ItemArray[2].ToString();

            string daysToExpire = value.ToString().Substring(value.ToString().IndexOf("LicenseExpirationDate")).Split('=')[1].Split(',')[0].Trim().Split(' ')[0];

            DateTime expiryDate = DateTime.Now;
            string[] dtFormats = { "dd/MMM/yyyy", "yyyy/dd/mm", "mm/dd/yyyy", "mm/dd/yy", "m/d/yyyy" };
            DateTime.TryParseExact(licExpiryDate, dtFormats, CultureInfo.CurrentCulture,
                    DateTimeStyles.None, out expiryDate);
            int daysLeft = Math.Abs(DateTime.Now.Subtract(expiryDate).Days);
            String workingDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (DateTime.Now.CompareTo(expiryDate) == -1)
            {
                if (daysLeft <= days)
                {
                    return workingDirectory + @"\ImagesImages\Warning.png";
                }
                else
                {
                    //solidClrBrush = Brushes.LightGreen;
                    return workingDirectory + @"\Images\Normal.png";
                }
            }
            else if (DateTime.Now.CompareTo(expiryDate) == 1 || DateTime.Now.CompareTo(expiryDate) == 0)
            {
                //byte R = Convert(Color.Substring(1, 2), 16);
                //byte G = Convert.ToByte(color.Substring(3, 2), 16);
                //byte B = Convert.ToByte(color.Substring(5, 2), 16);

                try
                {
                    //solidClrBrush = new SolidColorBrush(Color.FromRgb(255, 80, 80));
                    return workingDirectory + @"\ImagesImages\Warning.png";
                    //solidClrBrush = (SolidColorBrush)(new BrushConverter().Convert("#F08080", null, null, CultureInfo.InvariantCulture)); // Brushes.Red;
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                }

            }           

            //DataRowView row = value as DataRowView;
            //if (row != null)
            //{
            //    if (row.DataView.Table.Columns.Contains("IntVal"))
            //    {
                    
            //        int status = (int)row["IntVal"];
            //        if (status == 0)
            //        {
            //            return workingDirectory + @"\Images\Normal.png";
            //        }
            //        if (status == 1)
            //        {
            //            return workingDirectory + @"\ImagesImages\Warning.png";
            //        }
            //        if (status == 2)
            //        {
            //            return workingDirectory + @"\Images\Error.png";
            //        }
            //    }
            //}
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
