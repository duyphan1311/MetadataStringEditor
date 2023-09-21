using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetaDataStringEditor {
    class MetadataFile : IDisposable {
        public BinaryReader reader;

        private uint stringLiteralOffset;
        private uint stringLiteralCount;
        private long DataInfoPosition;
        private uint stringLiteralDataOffset;
        private uint stringLiteralDataCount;
        private List<StringLiteral> stringLiterals = new List<StringLiteral>();
        public List<byte[]> strBytes = new List<byte[]>();

        public MetadataFile(string fullName) {
            reader = new BinaryReader(File.OpenRead(fullName));

            // Read file
            ReadHeader();

            // Read string
            ReadLiteral();
            ReadStrByte();

            Logger.I("Reading completed!");
        }

        private void ReadHeader() {
            Logger.I("Reading header");
            uint vansity = reader.ReadUInt32();
            if (vansity != 0xFAB11BAF) {
                throw new Exception("Flag check failed");
            }
            int version = reader.ReadInt32();
            stringLiteralOffset = reader.ReadUInt32();      // The position of the list area will not be changed later.
            stringLiteralCount = reader.ReadUInt32();       // The size of the list area will not be changed later.
            DataInfoPosition = reader.BaseStream.Position;  // Remember the current location, you will need it later.
            stringLiteralDataOffset = reader.ReadUInt32();  // The location of the data area may need to be changed.
            stringLiteralDataCount = reader.ReadUInt32();   // The length of the data area may need to be changed.
        }

        private void ReadLiteral() {
            Logger.I("Reading file");
            ProgressBar.SetMax((int)stringLiteralCount / 8);

            reader.BaseStream.Position = stringLiteralOffset;
            for (int i = 0; i < stringLiteralCount / 8; i++) {
                stringLiterals.Add(new StringLiteral {
                    Length = reader.ReadUInt32(),
                    Offset = reader.ReadUInt32()
                });
                ProgressBar.Report();
            }
        }

        private void ReadStrByte() {
            Logger.I("Reading string");
            ProgressBar.SetMax(stringLiterals.Count);

            for (int i = 0; i < stringLiterals.Count; i++) {
                reader.BaseStream.Position = stringLiteralDataOffset + stringLiterals[i].Offset;
                strBytes.Add(reader.ReadBytes((int)stringLiterals[i].Length));
                ProgressBar.Report();
            }
        }

        public void WriteToNewFile(string fileName) {
            BinaryWriter writer = new BinaryWriter(File.Create(fileName));

            // Copy them all first
            reader.BaseStream.Position = 0;
            reader.BaseStream.CopyTo(writer.BaseStream);

            // Update Literal
            Logger.I("Update file");
            ProgressBar.SetMax(stringLiterals.Count);
            writer.BaseStream.Position = stringLiteralOffset;
            uint count = 0;
            for (int i = 0; i < stringLiterals.Count; i++) {

                stringLiterals[i].Offset = count;
                stringLiterals[i].Length = (uint)strBytes[i].Length;

                writer.Write(stringLiterals[i].Length);
                writer.Write(stringLiterals[i].Offset);
                count += stringLiterals[i].Length;

                ProgressBar.Report();
            }

            // Perform an alignment, not sure if it is necessary, but Unity has done it, so it is better to make up for it.
            var tmp = (stringLiteralDataOffset + count) % 4;
            if (tmp != 0) count += 4 - tmp;

            // Check if there is enough space to place it
            if (count > stringLiteralDataCount) {
                // Check if there is any other data behind the data area. If not, you can directly extend the data area.
                if (stringLiteralDataOffset + stringLiteralDataCount < writer.BaseStream.Length) {
                    // The original space is not enough, and it cannot be extended directly,
                    // so the entire file is moved to the end of the file.
                    stringLiteralDataOffset = (uint)writer.BaseStream.Length;
                }
            }
            stringLiteralDataCount = count;

            // Write string
            Logger.I("Replace string");
            ProgressBar.SetMax(strBytes.Count);
            writer.BaseStream.Position = stringLiteralDataOffset;
            for (int i = 0; i < strBytes.Count; i++) {
                writer.Write(strBytes[i]);
                ProgressBar.Report();
            }

            // Update header
            Logger.I("Update header");
            writer.BaseStream.Position = DataInfoPosition;
            writer.Write(stringLiteralDataOffset);
            writer.Write(stringLiteralDataCount);

            Logger.I("Update completed");
            writer.Close();
        }
        
        public void Dispose() {
            reader?.Dispose();
        }
        
        public class StringLiteral {
            public uint Length;
            public uint Offset;
        }
    }
}
