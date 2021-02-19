using System;

namespace Model
{
	// Token: 0x02000004 RID: 4
	public class tr_order_item
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000025C0 File Offset: 0x000007C0
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000025D7 File Offset: 0x000007D7
		public string ord_id { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000025E0 File Offset: 0x000007E0
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000025F7 File Offset: 0x000007F7
		public int row_index { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002600 File Offset: 0x00000800
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002617 File Offset: 0x00000817
		public string goods_id { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002620 File Offset: 0x00000820
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002637 File Offset: 0x00000837
		public decimal qty { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002640 File Offset: 0x00000840
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002657 File Offset: 0x00000857
		public decimal price { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002660 File Offset: 0x00000860
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002677 File Offset: 0x00000877
		public decimal amount { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002680 File Offset: 0x00000880
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002697 File Offset: 0x00000897
		public string enable { get; set; }

        public string remark { get; set; }
	}
}
