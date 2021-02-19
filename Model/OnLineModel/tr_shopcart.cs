using System;

namespace Model
{
	// Token: 0x02000010 RID: 16
	public class tr_shopcart
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000032A0 File Offset: 0x000014A0
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000032B7 File Offset: 0x000014B7
		public int mc_id { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000032C0 File Offset: 0x000014C0
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000032D7 File Offset: 0x000014D7
		public string openid { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000032E0 File Offset: 0x000014E0
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000032F7 File Offset: 0x000014F7
		public int row_index { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00003300 File Offset: 0x00001500
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00003317 File Offset: 0x00001517
		public string goods_id { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003320 File Offset: 0x00001520
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00003337 File Offset: 0x00001537
		public decimal price { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003340 File Offset: 0x00001540
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00003357 File Offset: 0x00001557
		public decimal qty { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003360 File Offset: 0x00001560
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00003377 File Offset: 0x00001577
		public decimal amount { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003380 File Offset: 0x00001580
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00003397 File Offset: 0x00001597
		public DateTime create_time { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000033A0 File Offset: 0x000015A0
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000033B7 File Offset: 0x000015B7
		public string wx_head_url { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000033C0 File Offset: 0x000015C0
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000033D7 File Offset: 0x000015D7
		public string wx_nick_name { get; set; }
	}
}
