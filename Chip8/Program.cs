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

            CPU cpu = new CPU();
            using (BinaryReader reader = new BinaryReader(new FileStream("IBM LOGO.ch8", FileMode.Open)))
            {
                
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var opcode = (ushort)((reader.ReadByte() << 8) | reader.ReadByte());

                    try
                    {
                        cpu.ExecuteOpcode(opcode);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                
            }
        }

    }

    public class CPU
    {    
        public ushort PC = 0;
        public Stack<ushort> Stack = new Stack<ushort>();       
        public byte[] Display = new byte[64 * 32];
        
        public void ExecuteOpcode(ushort opcode)
        {
            ushort nibble = (ushort)(opcode & 0xF000);
            switch (nibble)
            {
                case 0x0000:
                    if (opcode == 0x00e0)
                    {
                        //Display is a single array
                        for (int i = 0; i < Display.Length; i++) Display[i] = 0;
                    }
                    else if (opcode == 0x00ee)
                    {
                        PC = Stack.Pop();
                    }
                    else
                    {
                        throw new Exception($"Unsupported opcode {opcode.ToString("X4")}");
                    }
                    break;
                default:
                    throw new Exception($"Unsupported opcode {opcode.ToString("X4")}");
               
            }

        }

    }

}
