﻿using ImTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackCursoUdemy2.Utils
{
    public class Encriptar
    {

       public static string EncriptarPassword(string input)
        {


            MD5 md5Hash = MD5.Create();

            Byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {

                sBuilder.Append(data[i].ToString("x2"));



            }

            return sBuilder.ToString();


        }


    }
}
