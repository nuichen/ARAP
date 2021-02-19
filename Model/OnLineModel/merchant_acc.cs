using System;

namespace Model
{
	// Token: 0x0200000B RID: 11
	public class merchant_acc
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00002BCF File Offset: 0x00000DCF
		public int mc_id { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002BD8 File Offset: 0x00000DD8
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00002BEF File Offset: 0x00000DEF
		public int bnk_id { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002BF8 File Offset: 0x00000DF8
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002C0F File Offset: 0x00000E0F
		public string bnk_account_name { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002C18 File Offset: 0x00000E18
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002C2F File Offset: 0x00000E2F
		public string bnk_account_no { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002C38 File Offset: 0x00000E38
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00002C4F File Offset: 0x00000E4F
		public decimal bal { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002C58 File Offset: 0x00000E58
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00002C6F File Offset: 0x00000E6F
		public string wx_appid { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002C78 File Offset: 0x00000E78
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002C8F File Offset: 0x00000E8F
		public string wx_secret { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002C98 File Offset: 0x00000E98
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00002CAF File Offset: 0x00000EAF
		public string wx_mcid { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002CB8 File Offset: 0x00000EB8
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00002CCF File Offset: 0x00000ECF
		public string wx_paykey { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00002CEF File Offset: 0x00000EEF
		public DateTime expire_date { get; set; }
	}
}
