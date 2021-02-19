using System;

namespace Model
{
	// Token: 0x02000006 RID: 6
	public class tr_address
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002730 File Offset: 0x00000930
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002747 File Offset: 0x00000947
		public int ads_id { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002767 File Offset: 0x00000967
		public string openid { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002770 File Offset: 0x00000970
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002787 File Offset: 0x00000987
		public string company { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002790 File Offset: 0x00000990
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000027A7 File Offset: 0x000009A7
		public string sname { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000027B0 File Offset: 0x000009B0
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000027C7 File Offset: 0x000009C7
		public string mobile { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000027D0 File Offset: 0x000009D0
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000027E7 File Offset: 0x000009E7
		public string address { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002807 File Offset: 0x00000A07
		public string is_default { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002810 File Offset: 0x00000A10
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002827 File Offset: 0x00000A27
		public DateTime create_time { get; set; }
	}
}
