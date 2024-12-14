// <auto-generated/>
using System;

namespace Telegram.Api.TL
{
	public partial class TLMessagesDHConfig : TLMessagesDHConfigBase 
	{
		public Int32 G { get; set; }
		public Byte[] P { get; set; }
		public Int32 Version { get; set; }

		public TLMessagesDHConfig() { }
		public TLMessagesDHConfig(TLBinaryReader from)
		{
			Read(from);
		}

		public override TLType TypeId { get { return TLType.MessagesDHConfig; } }

		public override void Read(TLBinaryReader from)
		{
			G = from.ReadInt32();
			P = from.ReadByteArray();
			Version = from.ReadInt32();
			Random = from.ReadByteArray();
		}

		public override void Write(TLBinaryWriter to)
		{
			to.Write(0x2C221EDD);
			to.Write(G);
			to.WriteByteArray(P);
			to.Write(Version);
			to.WriteByteArray(Random);
		}
	}
}