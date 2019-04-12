using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spire.Doc;
using System.Web.Mvc;
using System.IO;
using Spire.Doc.Documents;
using Kursach_v3.Models;
using System.ComponentModel.DataAnnotations;

namespace Kursach_v3.Models
{
    public class Text
    {

        public string[] AllText { get; set; }

        [DataType(DataType.MultilineText)]
        public string InputText { get; set; }

        public int Shift { get; set; }
        private static char[] alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
        private static char[] ALPH = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".ToCharArray();

        public string[] ReadableText { get; set; }

        
        public void InputToAll()
        {
           this.AllText = InputText.Split(new[] { Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);
        }
        //дешифровка 
        public void Decipher()
        {
            ReadableText = AllText;
            for (int i = 0; i < ReadableText.Length; i++)
            {
                for (int j = 0; j < ReadableText[i].Length; j++)
                {
                    var arr = ReadableText[i].ToCharArray();
                    if (ALPH.Contains<char>(arr[j]))
                    {
                        for (int k = 0; k < ALPH.Length; k++)
                        {
                            if (arr[j] == ALPH[k])
                            {
                                if (k + Shift >= ALPH.Length -1)
                                {
                                    arr[j] = ALPH[(k + Shift) % (ALPH.Length )];
                                    break;
                                }
                                else
                            if (k + Shift < 0)
                                {
                                    arr[j] = ALPH[ALPH.Length + k + Shift];
                                    break;
                                }
                                else
                                {
                                    arr[j] = ALPH[k + Shift];
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int k = 0; k < alph.Length; k++)
                        {
                            if (arr[j] == alph[k])
                            {
                                if (k + Shift >= alph.Length - 1)
                                {
                                    arr[j] = alph[(k + Shift) % (alph.Length )];
                                    break;
                                }
                                else
                                if (k + Shift < 0)
                                {
                                    arr[j] = alph[alph.Length + k + Shift];
                                    break;
                                }
                                else
                                {
                                    arr[j] = alph[k + Shift];
                                    break;
                                }
                            }
                        }
                    }
                    ReadableText[i] = new string(arr);
                }
            }

        }

    }
}