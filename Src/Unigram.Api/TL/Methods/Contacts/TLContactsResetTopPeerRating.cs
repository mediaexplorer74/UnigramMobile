// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Contacts
{
	/// <summary>
	/// RCP method contacts.resetTopPeerRating.
	/// Returns <see cref="Telegram.Api.TL.TLBoolBase"/>
	/// </summary>
	public partial class TLContactsResetTopPeerRating : TLObject
	{
		public TLTopPeerCategoryBase Category { get; set; }
		public TLInputPeerBase Peer { get; set; }

		public TLContactsResetTopPeerRating() { }
		public TLContactsResetTopPeerRating(TLBinaryReader from)
		{
			Read(from);
		}

		public override TLType TypeId { get { return TLType.ContactsResetTopPeerRating; } }

		public override void Read(TLBinaryReader from)
		{
			Category = TLFactory.Read<TLTopPeerCategoryBase>(from);
			Peer = TLFactory.Read<TLInputPeerBase>(from);
		}

		public override void Write(TLBinaryWriter to)
		{
			to.Write(0x1AE373AC);
			to.WriteObject(Category);
			to.WriteObject(Peer);
		}
	}
}