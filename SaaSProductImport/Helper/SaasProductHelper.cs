using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SaaSProductImport.Helper
{
    public static class SaasProductHelper
    {
        /// <summary>
        /// Validate command is valid or not
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static bool IsValidCommand(string command)
        {
            List<string> cmdValues = (command == null && command.Length < 1) ? null : command.Split(" ").ToList();

            if(cmdValues==null || cmdValues.Count<3)
            {
                Console.WriteLine("Please enter a valid command to proceed!! like: import [sourcename] [filelocationWithFileName]");
                return false;
            }
            else if(!string.Equals(cmdValues[0],"import",StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{cmdValues[0]} is not recognized as a valid command, please enter in this format: import [sourcename] [filelocationWithFileName]");
                return false;
            }
            else if (!string.Equals(cmdValues[1], "capterra", StringComparison.OrdinalIgnoreCase) && !string.Equals(cmdValues[1], "softwareadvice", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{cmdValues[1]} is not recognized as a valid command, please enter in this format: import [sourcename] [filelocationWithFileName]");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validate requested file extension
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool RequestedFileValidation(string filePath)
        {
            string ext = Path.GetExtension(filePath);
            switch(ext.ToLower())
            {
                case ".yaml":
                    return true;
                case ".json":
                    return true;
                default:
                    return false;
            }
        }
    }
}
