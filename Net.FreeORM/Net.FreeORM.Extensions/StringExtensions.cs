using System;
using System.Text;

namespace Net.FreeORM.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertTurkishCharactersInWord(this string theWord)
        {
            try
            {
                theWord = theWord.Replace("ö", "o");
                theWord = theWord.Replace("ü", "u");
                theWord = theWord.Replace("ı", "i");
                theWord = theWord.Replace("ş", "s");
                theWord = theWord.Replace("ğ", "g");
                theWord = theWord.Replace("ç", "c");

                theWord = theWord.Replace("Ö", "O");
                theWord = theWord.Replace("Ü", "U");
                theWord = theWord.Replace("İ", "I");
                theWord = theWord.Replace("Ş", "S");
                theWord = theWord.Replace("Ğ", "G");
                theWord = theWord.Replace("Ç", "C");

                return theWord;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Str2Int(this string str)
        {
            try
            {
                return int.Parse(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Length == 0;
            }
        }

        public static bool IsNullOrSpace(this string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Replace(" ", string.Empty).Length == 0;
            }
        }

        public static string Reverse(this string Str)
        {
            try
            {
                int len = Str.Length;
                switch (len)
                {
                    case 0:
                    case 1:
                        return Str;

                    case 2:
                        return string.Format("{0}{1}", Str[1], Str[0]);

                    default:
                        return string.Format("{0}{1}{2}", Str[Str.Length - 1], Reverse(Str.Substring(1, Str.Length - 2)), Str[0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool isNumber(this string willBeExamined)
        {
            try
            {
                if (willBeExamined.Length < 1)
                    return false;

                bool willBeReturned = true;
                foreach (char ch in willBeExamined.ToCharArray())
                {
                    willBeReturned = Char.IsNumber(ch);
                    if (willBeReturned == false)
                        break;
                }
                return willBeReturned;
            }
            catch (Exception)
            {
                throw;
            }
        } // end isNumber

        public static string ReverseString(this string willBeReversed)
        {
            try
            {
                int len = willBeReversed.Length;
                switch (len)
                {
                    case 0:
                    case 1:
                        return willBeReversed;

                    default:
                        char[] tmpCh = willBeReversed.ToCharArray();
                        for (int i = 0; i < len / 2; i++)
                        {
                            char ch = tmpCh[i];
                            tmpCh[i] = tmpCh[len - i - 1];
                            tmpCh[len - i - 1] = ch;
                        }
                        return StringFromCharArray(tmpCh);
                }
            }
            catch (Exception)
            {
                throw;
            }
        } // end ReverseString

        public static string MultiplyStrings(this string str1, string str2)
        {
            try
            {
                if (isNumber(str1) && isNumber(str2))
                {
                    string tmp1 = str1.Reverse();
                    string tmp2 = str2.Reverse();
                    int len1 = tmp1.Length;
                    int len2 = tmp2.Length;
                    int returnLength = len1 + len2;
                    int[] IntArray = new int[returnLength];

                    for (int i = 0; i < len2; i++)
                    {
                        for (int j = 0; j < len1; j++)
                        {
                            int sayi1 = tmp1[j].ToStr().Str2Int();
                            int sayi2 = tmp2[i].ToStr().Str2Int();

                            int say = sayi1 * sayi2;
                            IntArray[i + j] += say;
                            if (IntArray[i + j] > 9)
                            {
                                IntArray[i + j + 1] += IntArray[i + j] / 10;
                                IntArray[i + j] %= 10;
                            }
                            /*
                            if (say > 9)
                            {
                                // Recursive yapı oluşturulacak.
                                int mod = say % 10;
                                say /= 10;
                                IntArray[i + j] += mod;
                                int bolum = IntArray[i + j] / 10;
                                IntArray[i + j] %= 10;
                                IntArray[i + j + 1] += say + bolum;
                            }
                            else
                                IntArray[i + j] += say;
                             * */
                        }
                    }
                    for (int i = 0; i < IntArray.Length - 1; i++)
                    {
                        int willBeAddedUpperIndex = IntArray[i] / 10;
                        IntArray[i] %= 10;
                        IntArray[i + 1] += willBeAddedUpperIndex;
                    }
                    String strResult = StringFromIntArray(IntArray);

                    // İlk rakam sıfırsa kesiyoruz.
                    return strResult[0].Equals('0') ?
                        strResult.Substring(1, strResult.Length - 1) : strResult;
                }
                else
                    throw new InvalidOperationException("Parameters only consist of numbers.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string StringFromIntArray(int[] array)
        {
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                foreach (int i in array)
                {
                    string str = i.ToString();
                    strBuilder.Append(str);
                }
                return ReverseString(strBuilder.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        } // end StringFromIntArray

        private static string StringFromCharArray(char[] charArray)
        {
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                foreach (char ch in charArray)
                {
                    strBuilder.Append(ch);
                }
                return strBuilder.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}