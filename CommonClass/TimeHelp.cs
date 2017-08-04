using System;
using System.Text;

namespace Common
{
    public class TimeHelp
    {
        /// <summary>
        /// 返回时间随机码
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeRandom()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        /// <summary>
        /// 返回指定格式时间值
        /// </summary>    
        /// <returns></returns>
        public static string GetFormat(string format = "")
        {
            if (format == "")
            {
                return DateTime.Now.ToString("yyyy年MM月dd日 HH:mm");
            }
            else
            {
                return DateTime.Now.ToString(format);
            }

        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetNumId()
        {
            //时间每个地区的都不一样，但在同一时间全世界的时间戳都是一样的   17位     
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (DateTime.Now.Ticks - startTime.Ticks);
            return t.ToString().PadRight(18, '0');
        }

        /// <summary>
        /// 返回时间差   第一个时间必须比第二个时间小次方法才不会出错
        /// </summary>
        /// <param name="DateTime1">第一个时间,小</param>
        /// <param name="DateTime2">第二个时间，大</param>
        /// <returns></returns>
        public static string DateDiff(DateTime dtSmall, DateTime dtLarge)
        {
            //利用TimeSpan 不能得到年月 所以自己计算一下
            int lHour = dtLarge.Hour;//小时
            int lday = dtLarge.Day;//分钟
            int lmonth = dtLarge.Month;//月
            int lyear = dtLarge.Year;//年

            int year; //年
            int month;//月
            int day;//日
            int hour;//小时
            int minute; //分钟

            if (dtLarge.Minute<dtSmall.Minute) //分钟差
            {
                minute=dtLarge.Minute - dtSmall.Minute + 60;
                lHour--;
            }
            else
            {
                minute = dtLarge.Minute - dtSmall.Minute;
            }

            if (lHour < dtSmall.Hour)  //小时差
            {
                hour = lHour - dtSmall.Hour + 24;
                lday--;
            }
            else
            {
                hour = lHour - dtSmall.Hour;
            }

            if (lday< dtSmall.Day)//天差
            {
                //31天
                if (lmonth==3)
                {
                    day = lday - dtSmall.Day+28;
                }
                else if("5,7,10,12".Contains(lmonth.ToString()))
                {
                    day = lday - dtSmall.Day + 30;
                }
                else
                {
                    day = lday - dtSmall.Day + 31;
                }
                lmonth--;
            }
            else
            {
                day = lday - dtSmall.Day;
            }

            if (lmonth < dtSmall.Month)//月差
            {
                month = lmonth - dtSmall.Month+12;
                lyear--;
            }
            else
            {
                month = lmonth - dtSmall.Month;
            }
            year = lyear - dtSmall.Year; //年差

            StringBuilder sb = new StringBuilder();
            if (year>0)
            {
                sb.AppendFormat("{0}年", year);
            }
            if (month>0)
            {
                sb.AppendFormat("{0}月", month);
            }
            if (day>0)
            {
                sb.AppendFormat("{0}天", day);
            }
            if (hour> 0)
            {
                sb.AppendFormat("{0}小时", hour);
            }
            if (minute>0)
            {
                sb.AppendFormat("{0}分钟", minute);
            }

            sb.Append("之前");
            return sb.ToString();
        }
    }
}