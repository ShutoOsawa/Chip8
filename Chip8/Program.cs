using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
namespace Chip8
{
    class Program
    {
        static void Main(string[] args)
        {

            using (BinaryReader reader = new BinaryReader(new FileStream("IBM Logo.ch8", FileMode.Open)))
            {
               
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var opcode = (ushort)((reader.ReadByte() << 8) | reader.ReadByte());
                    //var opcode = (ushort)((reader.ReadByte()));

                    //var opcode = reader.ReadUInt16();
                    Console.WriteLine($"{opcode.ToString("X4")}");
                    //test
                    //test1
                    //test2
                    //test3

                }

            }
        }

    }


}
