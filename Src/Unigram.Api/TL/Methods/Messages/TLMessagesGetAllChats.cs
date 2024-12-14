// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Messages
{
	/// <summary>
	/// RCP method messages.getAllChats.
	/// Returns <see cref="Telegram.Api.TL.TLMessagesChats"/>
	/// </summary>
	public partial class TLMessagesGetAllChats : TLObject
	{
		public TLVector<Int32> ExceptIds { get; set; }

		public TLMessagesGetAllChats() { }
		public TLMessagesGetAllChats(TLBinaryReader from)
		{
			Read(from);
		}

		public override TLType TypeId { get { return TLType.MessagesGetAllChats; } }

		public override void Read(TLBinaryReader from)
		{
			ExceptIds = TLFactory.Read<TLVector<Int32>>(from);
		}

		public override void Write(TLBinaryWriter to)
		{
			to.Write(0xEBA80FF0);
			to.WriteObject(ExceptIds);
		}
	}
}