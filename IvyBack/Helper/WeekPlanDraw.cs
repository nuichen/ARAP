using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using IvyBack.body;

namespace IvyBack.Helper
{
    /// <summary>
    /// 一周计划绘制
    /// </summary>
    public class WeekPlanDraw
    {
        private readonly Graphics _graphics;
        private readonly bool _isPrint;
        private readonly Size _pageSize;
        public WeekPlanDraw(Graphics graphics)
        {
            _graphics = graphics;
        }

        public WeekPlanDraw(Graphics graphics, bool isPrint, Size pageSize) : this(graphics)
        {
            _isPrint = isPrint;
            _pageSize = pageSize;
            if (_isPrint)
            {
                Height = 65;
                Width = 165;
                FontSize = 10;
                HeaderFontSize = 15;
            }
        }

        private readonly Pen _linePen = new Pen(Color.Black);
        public Color HeaderBackground { get; set; } = Color.FromArgb(222, 222, 222);
        public Color Foreground { get; set; } = Color.Black;
        public Color Background { get; set; } = Color.White;


        public int Height { get; set; } = 90;
        public int Width { get; set; } = 200;

        /// <summary>
        /// 头部字体大小
        /// </summary>
        public int HeaderFontSize { get; set; } = 19;

        /// <summary>
        /// 表格内部字体大小
        /// </summary>
        public int FontSize { get; set; } = 14;

        private bool _isDrawTitle;
        private const int TitleHeight = 80;
        public void DrawPrintTitle(string customer)
        {
            if (!_isPrint)
            {
                return;
            }
            var font = new Font("微软雅黑", 19);
            var customerFont = new Font("微软雅黑", 9);
            var fontBrush = new SolidBrush(Foreground);


            var rect = new Rectangle(new Point(0, 0), new Size(_pageSize.Width, 65));
            _graphics.DrawString("一周销售计划表", font, fontBrush, rect, new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });

            var customerRect = new Rectangle(new Point((_pageSize.Width - 7 * Width) / 2, 65), new Size(_pageSize.Width, 15));
            _graphics.DrawString($"客户: {customer}", customerFont, fontBrush, customerRect, new StringFormat
            {
                LineAlignment = StringAlignment.Center
            });

            _isDrawTitle = true;

            font.Dispose();
            customerFont.Dispose();
            fontBrush.Dispose();
        }

        public void DrawHeader(DateTime startTime)
        {
            var dayOfWeek = new[] { "日", "一", "二", "三", "四", "五", "六" };
            var currentTime = startTime;

            var brush = new SolidBrush(HeaderBackground);
            var font = new Font("微软雅黑", HeaderFontSize, FontStyle.Bold);
            var fontBrush = new SolidBrush(Foreground);
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };


            var x = 0;
            if (_isPrint)
            {
                // 居中起始位置
                x = (_pageSize.Width - 7 * Width) / 2;
            }


            for (var i = 0; i < 7; i++)
            {
                var rect = new Rectangle(new Point(x, _isDrawTitle ? TitleHeight : 0), new Size(Width, Height));
                rect = DrawAndFill(_linePen, brush, rect);

                var content =
                    $"星期{dayOfWeek[(int)currentTime.DayOfWeek]}\r\n" +
                    currentTime.ToString("MM-dd");

                _graphics.DrawString(content, font, fontBrush, rect, stringFormat);

                currentTime = currentTime.AddDays(1);
                x += Width;
            }

            brush.Dispose();
            font.Dispose();
            fontBrush.Dispose();
            stringFormat.Dispose();
        }

        public int DrawContent(
            List<DayOfWeekContent> contents,
            out List<DayOfWeekContentWrapper> wrappers
            )
        {
            wrappers = new List<DayOfWeekContentWrapper>();

            if (contents == null)
            {
                return 0;
            }

            var brush = new SolidBrush(Background);
            var font = new Font("微软雅黑", FontSize);
            var addFont = new Font("微软雅黑", 10, FontStyle.Underline);
            var fontBrush = new SolidBrush(Foreground);
            var addFontBrush = new SolidBrush(Color.Blue);
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            int x = 0, maxHeight = 0;

            if (_isPrint)
            {
                x = (_pageSize.Width - 7 * Width) / 2;
            }

            foreach (var dayOfWeekContent in contents)
            {
                var y = _isDrawTitle ? TitleHeight + Height : Height;
                foreach (var content in dayOfWeekContent.Contents)
                {
                    var rect = new Rectangle(new Point(x, y), new Size(Width, Height));
                    rect = DrawAndFill(_linePen, brush, rect);

                    _graphics.DrawString(
                        $"{content.Item}\r\n{(string.IsNullOrEmpty(content.Supplier) ? " " : content.Supplier)}",
                        font,
                        fontBrush,
                        rect,
                        stringFormat
                    );
                    y += Height;

                    wrappers.Add(new DayOfWeekContentWrapper
                    {
                        Content = content,
                        Rectangle = rect,
                        DayOfWeekContent = dayOfWeekContent
                    });
                }

                // 添加添加计划
                if (!_isPrint && dayOfWeekContent.Contents.Count == 0)
                {
                    var rect = new Rectangle(new Point(x, y), new Size(Width, Height));
                    rect = DrawAndFill(_linePen, brush, rect);

                    _graphics.DrawString(
                        "添加计划",
                        addFont,
                        addFontBrush,
                        rect,
                        stringFormat
                    );

                    wrappers.Add(new DayOfWeekContentWrapper
                    {
                        IsEmpty = true,
                        Rectangle = rect,
                        DayOfWeekContent = dayOfWeekContent
                    });
                }

                if (maxHeight < y)
                {
                    maxHeight = y;
                }
                x += Width;
            }

            brush.Dispose();
            font.Dispose();
            fontBrush.Dispose();
            stringFormat.Dispose();

            return maxHeight;
        }

  

        /// <summary>
        /// 描边并填充
        /// </summary>
        /// <param name="pen"></param>
        /// <param name="brush"></param>
        /// <returns>返回客户区区域</returns>
        private Rectangle DrawAndFill(Pen pen, Brush brush, Rectangle rect)
        {
            _graphics.DrawRectangle(pen, rect);

            rect.X += 1;
            rect.Y += 1;
            rect.Width -= 1;
            rect.Height -= 1;
            _graphics.FillRectangle(brush, rect);
            return rect;
        }
    }
}
