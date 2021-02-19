using System;

namespace Model
{
	// Token: 0x02000005 RID: 5
	public class sys_acc
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000026BF File Offset: 0x000008BF
		public string wx_appid { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000026DF File Offset: 0x000008DF
		public string wx_secret { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000026E8 File Offset: 0x000008E8
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000026FF File Offset: 0x000008FF
		public string wx_mcid { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002708 File Offset: 0x00000908
		// (set) Token: 0x0600006F RID: 111 RVA: 0x0000271F File Offset: 0x0000091F
		public string wx_paykey { get; set; }
	}
}
