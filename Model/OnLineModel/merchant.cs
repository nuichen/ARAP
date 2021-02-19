using System;

namespace Model
{
	// Token: 0x0200000D RID: 13
	public class merchant
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002E08 File Offset: 0x00001008
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002E1F File Offset: 0x0000101F
		public int mc_id { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002E28 File Offset: 0x00001028
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002E3F File Offset: 0x0000103F
		public string mc_no { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002E48 File Offset: 0x00001048
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002E5F File Offset: 0x0000105F
		public string mc_name { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002E68 File Offset: 0x00001068
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002E7F File Offset: 0x0000107F
		public string contact { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002E88 File Offset: 0x00001088
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002E9F File Offset: 0x0000109F
		public string mobile { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002EA8 File Offset: 0x000010A8
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002EBF File Offset: 0x000010BF
		public string address { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002EC8 File Offset: 0x000010C8
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00002EDF File Offset: 0x000010DF
		public DateTime reg_time { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002EE8 File Offset: 0x000010E8
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002EFF File Offset: 0x000010FF
		public string status { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002F08 File Offset: 0x00001108
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002F1F File Offset: 0x0000111F
		public string pwd { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002F28 File Offset: 0x00001128
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002F3F File Offset: 0x0000113F
		public string qrcode { get; set; }
	}
}
