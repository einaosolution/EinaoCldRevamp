using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Net;
using System.Collections;

namespace IPORevamp.Core.Utilities
{
    public class Utilities
    {

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public static string GenerateRandomString(int length = 10)
        {
            char[] chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[length];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(10);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }


        // This method will get the months Difference betwo two dates
        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        // This method generate SHA512
        public static string GetSHA512(string text)
        {
            ASCIIEncoding UE = new ASCIIEncoding();
            byte[] hashValue;
            byte[] data = UE.GetBytes(text);
            SHA512 hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(data);
            hex = ByteToString(hashValue);
            return hex;
        }

        // This method convert Byte to String
        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }




        // This method give details of interswitch feedback
        public static string InterswitchFeedback(string p)
        {
            string desc = "";
            if (p == "00")
                desc = "Approved by Financial Institution";
            else if (p == "01")
                desc = "Refer to Financial Institution";
            else if (p == "02")
                desc = "Refer to Financial Institution, Special Condition";
            else if (p == "03")
                desc = "Invalid Merchant";
            else if (p == "04")
                desc = "Pick-up card";
            else if (p == "05")
                desc = "Do Not Honor";
            else if (p == "06")
                desc = "Error";
            else if (p == "07")
                desc = "Pick-Up Card, Special Condition";
            else if (p == "08")
                desc = "Honor with Identification";
            else if (p == "09")
                desc = "Request in Progress";
            else if (p == "10")
                desc = "Approved by Financial Institution, Partial";
            else if (p == "11")
                desc = "Approved by Financial Institution, VIP";
            else if (p == "12")
                desc = " Invalid Transaction";
            else if (p == "13")
                desc = "Invalid Amount";
            else if (p == "14")
                desc = "Invalid Card Number";
            else if (p == "15")
                desc = "No Such Financial Institution";
            else if (p == "16")
                desc = "Approved by Financial Institution, Update Track 3";
            else if (p == "17")
                desc = "Customer Cancellation";
            else if (p == "18")
                desc = "Customer Dispute";
            else if (p == "19")
                desc = "Re-enter Transaction";
            else if (p == "20")
                desc = "Invalid Response from Financial Institution";
            else if (p == "21")
                desc = "No Action Taken by Financial Institution";
            else if (p == "22")
                desc = "Suspected Malfunction";
            else if (p == "23")
                desc = "Unacceptable Transaction Fee";
            else if (p == "24")
                desc = "File Update not Supported";
            else if (p == "25")
                desc = "Unable to Locate Record";
            else if (p == "26")
                desc = "Duplicate Record";
            else if (p == "27")
                desc = "File Update Field Edit Error";
            else if (p == "28")
                desc = "File Update File Locked";
            else if (p == "29")
                desc = "File Update Failed";
            else if (p == "30")
                desc = "Format Error";
            else if (p == "31")
                desc = "Bank Not Supported";
            else if (p == "32")
                desc = "Completed Partially by Financial Institution";
            else if (p == "33")
                desc = "Expired Card, Pick-Up";
            else if (p == "34")
                desc = "Suspected Fraud, Pick-Up";
            else if (p == "35")
                desc = "Contact Acquirer, Pick-Up";
            else if (p == "36")
                desc = "Restricted Card, Pick-Up";
            else if (p == "37")
                desc = "Call Acquirer Security, Pick-Up";
            else if (p == "38")
                desc = "PIN Tries Exceeded, Pick-Up";
            else if (p == "39")
                desc = "No Credit Account";
            else if (p == "40")
                desc = "Function not Supported";
            else if (p == "41")
                desc = "Lost Card, Pick-Up";
            else if (p == "42")
                desc = "No Universal Account";
            else if (p == "43")
                desc = "Stolen Card, Pick-Up";
            else if (p == "44")
                desc = "No Investment Account";
            else if (p == "51")
                desc = "Insufficient Funds";
            else if (p == "52")
                desc = "No Check Account";
            else if (p == "53")
                desc = "No Savings Account";
            else if (p == "54")
                desc = "Expired Card";
            else if (p == "55")
                desc = "Incorrect PIN";
            else if (p == "56")
                desc = "No Card Record";
            else if (p == "57")
                desc = "Transaction not permitted to Cardholder";
            else if (p == "58")
                desc = "Transaction not permitted on Terminal";
            else if (p == "59")
                desc = "Suspected Fraud";
            else if (p == "60")
                desc = "Contact Acquirer";
            else if (p == "61")
                desc = "Exceeds Withdrawal Limit";
            else if (p == "62")
                desc = "Restricted Card";
            else if (p == "63")
                desc = "Security Violation";
            else if (p == "64")
                desc = "Original Amount Incorrect";
            else if (p == "65")
                desc = "Exceeds withdrawal frequency";
            else if (p == "66")
                desc = "Call Acquirer Security";
            else if (p == "67")
                desc = "Hard Capture";
            else if (p == "68")
                desc = "Response Received Too Late";
            else if (p == "75")
                desc = "PIN tries exceeded";
            else if (p == "76")
                desc = "Reserved for Future Postilion Use";
            else if (p == "77")
                desc = "Intervene, Bank Approval Required";
            else if (p == "78")
                desc = "Intervene, Bank Approval Required for Partial Amount";
            else if (p == "90")
                desc = "Cut-off in Progress";
            else if (p == "91")
                desc = "Issuer or Switch Inoperative";
            else if (p == "92")
                desc = "Routing Error";
            else if (p == "93")
                desc = "Violation of law";
            else if (p == "94")
                desc = "Duplicate Transaction";
            else if (p == "95")
                desc = "Reconcile Error";
            else if (p == "96")
                desc = "System Malfunction";
            else if (p == "98")
                desc = "Exceeds Cash Limit";
            else if (p == "A0")
                desc = "Unexpected error";
            else if (p == "A4")
                desc = "Transaction not permitted to card holder, via channels";
            else if (p == "Z0")
                desc = "Transaction Status Unconfirmed";
            else if (p == "Z1")
                desc = "Transaction Error";
            else if (p == "Z2")
                desc = "Bank account error";
            else if (p == "Z3")
                desc = "Bank collections account error";
            else if (p == "Z4")
                desc = "Interface Integration Error";
            else if (p == "Z5")
                desc = "Duplicate Reference Error";
            else if (p == "Z6")
                desc = "Incomplete Transaction";
            else if (p == "Z7")
                desc = "Transaction Split Pre-processing Error";
            else if (p == "Z8")
                desc = "Invalid Card Number, via channels";
            else if (p == "Z9")
                desc = "Transaction not permitted to card holder, via channels";

            return desc;
        }



        /// <summary>
        ///  returns the conversion status of a string to an integer
        /// </summary>
        /// <param name="Value">the String value to convert</param>
        /// <returns> returns true or false</returns>
        public static Boolean ValidateNumericValue(String Value)
        {
            int number;
            return Int32.TryParse(Value, out number);
        }

        public static Boolean ValidateDecimalValue(String Value)
        {
            decimal number;
            return Decimal.TryParse(Value, out number);
        }


        public static string  FormatAmount(string numericValue)
        {
            decimal con = Convert.ToDecimal(numericValue);
            string formatted = con.ToString("#,##0.00");
            return formatted;

        }

        public static string Tokenize(string amount)
        {
            return (amount.Split('.')[0].Replace(",", ""));
        }

        public static string xTokenize(string amount)
        {
            return (amount.Replace(",", ""));
        }

        public static string DecryptString(string inputString, int dwKeySize, string xmlString)
        {
            try
            {

            
            CspParameters _cpsParameter;
            _cpsParameter = new CspParameters();
            _cpsParameter.Flags = CspProviderFlags.UseMachineKeyStore;

            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize, _cpsParameter);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ? (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize));
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the EncryptString function.
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);

            }

            catch(Exception ee)
            {
                var verror = ee.Message;
                return null;
            }
        }

        //This method generate random numbers
        public static string SMSServicesmsprovider(string username, string password, string message, string sender, string mobiles)
        {
            try
            {
                HttpWebRequest s = default(HttpWebRequest);
                UTF8Encoding enc = default(UTF8Encoding);
                string postdata = null;
                byte[] postdatabytes = null;
                s = (HttpWebRequest)HttpWebRequest.Create("http://customer.smsprovider.com.ng/api/");
                enc = new System.Text.UTF8Encoding();
                postdata = string.Format("username={0}&password={1}&message={2}&sender={3}&mobiles={4}", username, password, message, sender, mobiles);
                postdatabytes = enc.GetBytes(postdata);
                s.Method = "POST";
                s.ContentType = "application/x-www-form-urlencoded";
                s.ContentLength = postdatabytes.Length;

                Stream stream = s.GetRequestStream();
                stream.Write(postdatabytes, 0, postdatabytes.Length);
                stream.Close();

                // Close the Stream object.
                WebResponse result = s.GetResponse();
                // Open the stream using a StreamReader for easy access.

                // Get the stream containing content returned by the server.
                stream = result.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Clean up the streams.
                reader.Close();
                stream.Close();
                result.Close();
                return responseFromServer;
            }
            catch (Exception ex)
            {
             //   Common.WriteLog(ex);
                return "";
            }

        }


        public static string Randomize()
        {
            string number = "";
            for (int i = 0; i < 16; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        //This method generate random numbers
        public static string Randomize(bool isGuid)
        {
            string guid = Guid.NewGuid().ToString();
            guid = guid.Replace("-", "").Substring(0, 10);
            return guid;
        }

        private static Random Rnd = new Random();
        public static string RandomizeNumber()
        {
            string number = "";
            for (int i = 0; i < 10; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        //This method generate random password
        public static string RandomizePassword()
        {
            string number = "";
            for (int i = 0; i < 5; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        //This method return Interswitch FeedBack

        public static string InterSwitchFeedback(string p)
        {
            string desc = "";
            switch (p)
            {
                case "01":
                    {
                        desc = "Refer to card issuer";
                    }
                    break;
                case "02":
                    {
                        desc = "Refer to card issuer, special condition";
                    }
                    break;
                case "03":
                    {
                        desc = "Invalid merchant";
                    }
                    break;
                case "04":
                    {
                        desc = "Pick-up card";
                    }
                    break;
                case "05":
                    {
                        desc = "Do not honor";
                    }
                    break;
                case "06":
                    {
                        desc = "Error";
                    }
                    break;
                case "07":
                    {
                        desc = "Pick-up card, special condition";
                    }
                    break;
                case "08":
                    {
                        desc = "Honor with identification";
                    }
                    break;
                case "09":
                    {
                        desc = "Request in progress";
                    }
                    break;
                case "10":
                    {
                        desc = "Approved, partial";
                    }
                    break;
                case "11":
                    {
                        desc = "Approved, VIP";
                    }
                    break;
                case "12":
                    {
                        desc = "Invalid transaction";
                    }
                    break;
                case "13":
                    {
                        desc = "Invalid amount";
                    }
                    break;
                case "14":
                    {
                        desc = "Invalid card number";
                    }
                    break;
                case "15":
                    {
                        desc = "No such issuer";
                    }
                    break;
                case "16":
                    {
                        desc = "Approved, update track 3";
                    }
                    break;
                case "17":
                    {
                        desc = "Customer cancellation";
                    }
                    break;
                case "18":
                    {
                        desc = "Customer dispute";
                    }
                    break;
                case "19":
                    {
                        desc = "Re-enter transaction";
                    }
                    break;
                case "20":
                    {
                        desc = "Invalid response";
                    }
                    break;
                case "21":
                    {
                        desc = "No action taken";
                    }
                    break;
                case "22":
                    {
                        desc = "Suspected malfunction";
                    }
                    break;
                default:
                    {
                        desc = "Payment status could not be verified";
                    }
                    break;
            }
            return desc;
        }



        // Converts a number from 1 to 9 into text. 
        public static string GetDigit(string Digit)
        {
            string functionReturnValue = null;
            switch (Convert.ToInt32(Digit))
            {
                case 1:
                    functionReturnValue = "One";
                    break;
                case 2:
                    functionReturnValue = "Two";
                    break;
                case 3:
                    functionReturnValue = "Three";
                    break;
                case 4:
                    functionReturnValue = "Four";
                    break;
                case 5:
                    functionReturnValue = "Five";
                    break;
                case 6:
                    functionReturnValue = "Six";
                    break;
                case 7:
                    functionReturnValue = "Seven";
                    break;
                case 8:
                    functionReturnValue = "Eight";
                    break;
                case 9:
                    functionReturnValue = "Nine";
                    break;
                default:
                    functionReturnValue = "";
                    break;
            }
            return functionReturnValue;
        }






        //This method convert to Int32

        public static int GetIntegerValue(Object obj)
        {
            int retVal = 0;
            try
            {
                retVal = (!(obj is DBNull) ? Convert.ToInt32(obj.ToString()) : 0);
            }
            catch
            {
            }
            return retVal;


        }
        //This method convert to String
        public static string GetStringValue(object obj)
        {
            string s = "";
            try
            {
                s = (!(obj is DBNull) ? obj.ToString() : "");
            }
            catch
            {
            }
            return s;
        }

        public static string GetStringValue_ZeroString(object obj)
        {
            string s = "0";
            try
            {
                s = (!(obj is DBNull) ? obj.ToString() : "0");
            }
            catch
            {
            }
            return s;
        }
        //This method  generate string value
        public static string GetStringValue_NullString(object obj)
        {
            string s = null;
            try
            {
                s = (!(obj is DBNull) ? obj.ToString() : "");
            }
            catch
            {
            }
            return s;
        }

        //This method  convert to decimal 
        public static decimal GetDecimalValue(Object obj)
        {
            decimal retVal = 0M;
            try
            {
                retVal = (!(obj is DBNull) ? Convert.ToDecimal(obj.ToString()) : 0M);
            }
            catch
            {
            }
            return retVal;
        }

        public static byte[] GetByteValue(Object obj)
        {
            return (!(obj is DBNull) ? (byte[])obj : null);
        }

        //This method  get bool value 
        public static bool GetBoolValue(Object obj)
        {
            bool retVal = false;
            try
            {
                retVal = (!(obj is DBNull) ? (bool)obj : false);
            }
            catch { }
            return retVal;
        }

        //This method  get date null value
        public static DateTime? GetDateNullValue(Object obj)
        {
            DateTime? retVal = null;
            try
            {
                if (!(obj is DBNull))
                {
                    retVal = (DateTime)obj;
                }
                else
                {
                    retVal = null;
                }
            }
            catch { }
            return retVal;
        }



        //This method  get date value 

        public static DateTime GetDateValue(Object obj)
        {
            DateTime retVal = DateTime.Now;
            try
            {
                if (!(obj is DBNull))
                {
                    retVal = (DateTime)obj;
                }
            }
            catch { }
            return retVal;
        }

        public static DateTime ConvertDateValue(Object obj)
        {
            DateTime result;
            var succeeded = DateTime.TryParse(obj.ToString(), out result);
            if (succeeded == true)
            {
                return result;
            }
            else
            {
                return DateTime.Now;
            }
        }


        //This method  convert date with the culture 
        public static DateTime ConvertDateValueWithCulture(String obj)
        {
            DateTime result;
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");

            var succeeded = DateTime.TryParse(obj, culture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out result);
            if (succeeded == true)
            {
                return result;
            }
            else
            {
                return DateTime.Now;
            }
        }


        //This method  get date from string
        public static string GetDateStringValue(Object obj)
        {
            string retVal = DateTime.Now.ToShortDateString();
            try
            {
                retVal = (!(obj is DBNull) ? ((DateTime)obj).ToShortDateString() : "");
            }
            catch { }
            return retVal;
        }

        //This method check if the value is a date
        public static bool isDate(String date)
        {
            DateTime temp;
            return DateTime.TryParse(date, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out temp) &&
                   temp.Hour == 0 &&
                   temp.Minute == 0 &&
                   temp.Second == 0 &&
                   temp.Millisecond == 0 &&
                   temp > DateTime.MinValue;

        }




        public static byte[] StreamFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();
            return ImageData; //return the byte data
        }

        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            // Create a buffer to hold the stream bytes
            byte[] buffer = new byte[stream.Length];

            // Read the bytes from this stream
            stream.Position = 0;
            stream.Read(buffer, 0, (int)stream.Length);
            stream.Close();
            return buffer;

        }

        public static byte[] ConvertStreamToByteArray(MemoryStream stream)
        {
            // Create a buffer to hold the stream bytes
            byte[] buffer = new byte[stream.Length];

            // Read the bytes from this stream

            stream.Position = 0;

            stream.Read(buffer, 0, (int)stream.Length);
            stream.Close();
            return buffer;

        }



        public static string FormatDate_Long(DateTime dt)
        {
            if (dt != null)
            {
                return String.Format("{0:dd MMMM yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate_Long1(DateTime dt)
        {
            if (dt != null)
            {
                return String.Format("{0:MMMM dd, yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:dd-MMM-yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate_Short(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:dd/MM/yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate(DateTime dt)
        {
            if (dt != null)
            {
                return String.Format("{0:dd-MMM-yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }


        }

        public static string FormatDate_YYYY(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:yyyy/MM/dd}", dt);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate_Get_YYYY(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:yyyy}", dt);
            }
            else
            {
                return string.Empty;
            }


        }
        public static string FormatDate_YYYY_1(string dt)
        {
            if (dt != null)
            {
                try
                {
                    return String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(dt));
                }
                catch { return string.Empty; }
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate(string dt)
        {
            if (dt != null)
            {
                try
                {
                    return String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dt));
                }
                catch { return string.Empty; }
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatDate(object dt)
        {
            if (dt != null)
            {
                try
                {
                    return String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(dt));
                }
                catch { return string.Empty; }
            }
            else
            {
                return string.Empty;
            }
        }



        #region EncryptionHelper

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "P@A8#$38381#$";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "P@A8#$38381#$";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion


        //convert List object to DataTable

        public static DataTable LINQToDataTable<T>(IEnumerable<T> data)
        {
            //if (data.Count <= 0)
            //    return null;
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                if (prop.Name != "EntityState" && prop.Name != "EntityKey")
                    table.Columns.Add(
                        prop.Name,
                        (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            ? Nullable.GetUnderlyingType(prop.PropertyType)
                            : prop.PropertyType
                    );
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    if (prop.Name != "EntityState" && prop.Name != "EntityKey")
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {


            //if (data.Count <= 0)
            //    return null;
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                if (prop.Name != "EntityState" && prop.Name != "EntityKey")
                    table.Columns.Add(
                        prop.Name,
                        (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            ? Nullable.GetUnderlyingType(prop.PropertyType)
                            : prop.PropertyType
                    );
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    if (prop.Name != "EntityState" && prop.Name != "EntityKey")
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static string TransformNameValue(NameValueCollection Collection, string Template)
        {
            string transformedBody = Template;
            int count = Collection.Count;
            string key = "";
            string value = "";
            for (int i = 0; i < count; i++)
            {
                key = Collection.GetKey(i);
                value = Collection.GetValues(i)[0];
                transformedBody = transformedBody.Replace(key, value);
            }
            return transformedBody;

        }



        public static string CoputrMD5Hash(string value)
        {
            string retVal = string.Empty;

            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(string.Format("wm7GG&@M6D*1@${0}[9*I1xr92]!", value)));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            retVal = sBuilder.ToString();

            return retVal;
        }


    }
}
