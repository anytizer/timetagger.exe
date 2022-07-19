using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using timetagger;

namespace WindowsFormsApp1
{
    public partial class Tagger : Form
    {
        TimeTagger tt = new TimeTagger();
        Stopwatch stopWatch = new Stopwatch();

        public Tagger()
        {
            InitializeComponent();
        }

        private void updateLyricsWithTime()
        {
            richTextBox1.Text = "";
            foreach(lyrics l in tt.lyrics)
            {
                this.richTextBox1.AppendText("[" + l.time + "]", Color.Brown);
                this.richTextBox1.AppendText(l.line+ Environment.NewLine, Color.Blue);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                this.handle();
            }
        }

        private void handle()
        {
            int LastPosition = this.richTextBox1.SelectionStart;

            int at = this.richTextBox1.GetLineFromCharIndex(LastPosition);
            //this.textBox1.Text = at.ToString();

            string elapsed = this.elapsed();
            tt.updateTime(at, elapsed);

            //this.richTextBox1.Text = tt.getLyrics();
            this.updateLyricsWithTime();

            // put cursor in next line
        //    this.putCursor(LastPosition);
        //}

        //private void putCursor(int LastPosition)
        //{
            for (int i = LastPosition; i < this.richTextBox1.Text.Length; ++i)
            {
                if (this.richTextBox1.Text[i] == '\n')
                {
                    this.richTextBox1.SelectionStart = i;
                    this.richTextBox1.Select(i + 1, 0);
                    break;
                }
            }
        }

        private void first_tag()
        {
            this.button1.Enabled = false;
            richTextBox1.Focus();

            tt.setLyrics(richTextBox1.Text);
            this.updateLyricsWithTime();

            stopWatch.Reset();
            stopWatch.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            first_tag();
        }

        public string elapsed()
        {
            TimeSpan ts = stopWatch.Elapsed;
            string elapsed = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            return elapsed;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
