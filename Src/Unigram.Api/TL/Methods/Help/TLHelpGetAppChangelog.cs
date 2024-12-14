// <auto-generated/>
using System;

namespace Telegram.Api.TL.Methods.Help
{
	/// <summary>
	/// RCP method help.getAppChangelog.
	/// Returns <see cref="Telegram.Api.TL.TLHelpAppChangelogBase"/>
	/// </summary>
	public partial class TLHelpGetAppChangelog : TLObject
	{
		public TLHelpGetAppChangelog() { }
		public TLHelpGetAppChangelog(TLBinaryReader from)
		{
			Read(from);
		}

		public override TLType TypeId { get { return TLType.HelpGetAppChangelog; } }

		public override void Read(TLBinaryReader from)
		{
		}

		public override void Write(TLBinaryWriter to)
		{
			to.Write(0xB921197A);
		}
	}
}