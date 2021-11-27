using System;
using System.Collections.Generic;
using System.IO;

namespace Chip8
{
    class Program
    {
        static void Main(string[] args)
        {
            using (BinaryReader reader = new BinaryReader(new FileStream("IBM Logo.ch8", FileMode.Open)))
            {
                CPU cpu = new CPU();
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    //switch from little endian to bit endian
                    var opcode = (ushort)((reader.ReadByte() << 8) | reader.ReadByte());
                    //var opcode = (ushort)(reader.ReadByte());
                    //var opcode = reader.ReadUInt16();
                    try
                    {
                        cpu.ExecuteOpcode(opcode);
                    }
                        catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                //var opcode = reader.ReadUInt16();
                //Console.WriteLine($"{opcode.ToString("x")}");
            }
            }

        }

        //Console.ReadKey();
    }

    public class CPU 
    {
        public byte[] RAM = new byte[4096];
        public byte[] Registers = new byte[16];
        public ushort I = 0;
        public Stack<ushort> Stack = new Stack<ushort>();
        public byte DelayTimer;
        public byte SoundTimer;
        public byte Keyboard;

        public byte[] Display = new byte[64 * 32];

        public void ExecuteOpcode(ushort opcode)
        {
            ushort nibble = (ushort)(opcode & 0xF000);
            switch (nibble) 
            {
                case 0x0000:
                    if(opcode == 0x00e0)
                    {
                        for (int i = 0; i < Display.Length; i++) Display[0] = 0;
                    }
                    else if(opcode == 0x00ee)
                    {
                        I = Stack.Pop();
                    }
                    else
                    {
                        throw new Exception($"Unsupported opcode {opcode.ToString("X4")}");
                    }
                    break;
                default:
                    throw new Exception($"Unsupported opcode {opcode.ToString("X4")}");
                    break;
            }

        }
    }



}
