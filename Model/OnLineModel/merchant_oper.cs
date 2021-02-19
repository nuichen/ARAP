using System;

namespace Model
{
	// Token: 0x0200000C RID: 12
	public class merchant_oper
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002D00 File Offset: 0x00000F00
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002D17 File Offset: 0x00000F17
		public int oper_id { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002D20 File Offset: 0x00000F20
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00002D37 File Offset: 0x00000F37
		public string oper_name { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002D40 File Offset: 0x00000F40
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002D57 File Offset: 0x00000F57
		public string type { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002D60 File Offset: 0x00000F60
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002D77 File Offset: 0x00000F77
		public string mobile { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002D80 File Offset: 0x00000F80
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00002D97 File Offset: 0x00000F97
		public DateTime reg_time { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002DA0 File Offset: 0x00000FA0
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002DB7 File Offset: 0x00000FB7
		public string status { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002DC0 File Offset: 0x00000FC0
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00002DD7 File Offset: 0x00000FD7
		public string pwd { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00002DF7 File Offset: 0x00000FF7
		public int mc_id { get; set; }
	}
}
