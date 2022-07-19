using System;
using System.Collections.Generic;
using timetagger;

namespace WindowsFormsApp1
{
    public class TimeTagger
    {
        public List<lyrics> lyrics = new List<lyrics>(){};

        public string getLyrics()
        {
            string lyrics = "";

            foreach (lyrics lyric in this.lyrics)
            {
                lyrics += this.formattedLine(lyric) + "\r\n";
            }

            return lyrics;
        }

        public string formattedLine(lyrics lyric)
        {
            // [time] lyric line
            return string.Format("[{0}] {1}", lyric.time, lyric.line);
        }

        public void setLyrics(string text)
        {
            string[] lines = text.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            List<lyrics> lyrics = new List<lyrics>() { };
            foreach(string line in lines)
            {
                lyrics.Add(new lyrics() { 
                    time = "00:00:00.00",
                    line = line,
                });
            }
            
            this.lyrics = lyrics;
        }

        public void updateTime(int at, string elapsed)
        {
            if (at >= 0 && at < this.lyrics.Count)
            {
               this.lyrics[at].time = elapsed;
            }            
        }
    }
}
