﻿using System;
using System.Threading.Tasks;

namespace satbot.runner31
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClienteSatBot cli = new ClienteSatBot(@"c:\borrame\satbot.txt", @"C:\GoogleDrive\SAT\DIGO7101313f0\FIEL\digo7101313f0.pfx", "ines0202", "DIGO7101313F0");
            Task.Run(()=> cli.Procesar());

        }
    }
}