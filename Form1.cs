using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Cafe_System
{
    public class Form1 : Form
    {
        ListBox listBox1;       // Səbət
        TextBox textBox1;       // Məbləğ (müştərinin verdiyi pul)
        TextBox textBox2;       // Hesab
        Label label4;           // Qalıq (Qaytarılır: yanındakı rəqəm)
        Button button1;         // Səbətdən sil
        Button button2;         // Yenilə
        Button button3;         // Hesabla (sağ tərəf - yekun hesab)
        Button button4;         // Qalıq hesabı (sol tərəf)
        Button button5;         // Təmizlə

        double total;

        public Form1()
        {
            BuildUI();
        }

        

        private void BuildUI()
        {
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;

            Text = "Cafe System";
            ClientSize = new Size(740, 440);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            Color gray = Color.FromArgb(128, 128, 128);
            Font bold12 = new Font("Segoe UI", 12f, FontStyle.Bold);
            Font bold11 = new Font("Segoe UI", 11f, FontStyle.Bold);
            Font bold9 = new Font("Segoe UI", 9f, FontStyle.Bold);

            Panel left = new Panel { Location = new Point(0, 0), Size = new Size(190, 440), BackColor = gray };
            Panel center = new Panel { Location = new Point(190, 0), Size = new Size(360, 440), BackColor = Color.White };
            Panel right = new Panel { Location = new Point(550, 0), Size = new Size(190, 440), BackColor = gray };
            Controls.Add(left);
            Controls.Add(center);
            Controls.Add(right);

            
            left.Controls.Add(MakeLabel("☕", new Rectangle(0, 20, 190, 75),
                new Font("Segoe UI Emoji", 40f), Color.Black, ContentAlignment.MiddleCenter));
            left.Controls.Add(MakeLabel("KAFE", new Rectangle(0, 100, 190, 30),
                bold12, Color.Black, ContentAlignment.MiddleCenter));

            left.Controls.Add(MakeLabel("Məbləğ:", new Rectangle(12, 150, 166, 20),
                bold9, Color.White, ContentAlignment.MiddleLeft));
            textBox1 = new TextBox { Location = new Point(12, 172), Size = new Size(166, 23) };
            left.Controls.Add(textBox1);

            button4 = MakeButton("Qalıq hesabı", new Rectangle(12, 210, 166, 30));
            button4.Click += button4_Click;
            left.Controls.Add(button4);

            left.Controls.Add(MakeLabel("Qaytarılır:", new Rectangle(12, 270, 105, 24),
                bold11, Color.White, ContentAlignment.MiddleLeft));
            label4 = MakeLabel("0", new Rectangle(117, 270, 61, 24),
                bold11, Color.White, ContentAlignment.MiddleLeft);
            left.Controls.Add(label4);

            button5 = MakeButton("Təmizlə", new Rectangle(12, 305, 166, 30));
            button5.Click += button5_Click;
            left.Controls.Add(button5);

        
            center.Controls.Add(MakeLabel("MENYU", new Rectangle(0, 15, 360, 30),
                bold12, Color.Black, ContentAlignment.MiddleCenter));

            
            AddMenuItem(center, "🍔", "Burger", 6.3, 0, 0);
            AddMenuItem(center, "🍰", "Tort", 4.5, 1, 0);
            AddMenuItem(center, "☕", "Çay", 1.5, 2, 0);
            AddMenuItem(center, "🌭", "Hot-dog", 3.5, 0, 1);
            AddMenuItem(center, "🍹", "Limonad", 2.8, 1, 1);
            AddMenuItem(center, "🍗", "Toyuq", 7.2, 2, 1);
            AddMenuItem(center, "🍕", "Pizza", 9, 0, 2);
            AddMenuItem(center, "🥤", "Kola", 2, 1, 2);
            AddMenuItem(center, "🍖", "Bütöv toyuq", 12.5, 2, 2);

            
            right.Controls.Add(MakeLabel("SƏBƏT", new Rectangle(0, 15, 190, 30),
                bold12, Color.Black, ContentAlignment.MiddleCenter));

            listBox1 = new ListBox
            {
                Location = new Point(6, 55),
                Size = new Size(178, 200),
                Font = new Font("Segoe UI", 9f)
            };
            right.Controls.Add(listBox1);

            button1 = MakeButton("Səbətdən sil", new Rectangle(6, 262, 178, 28));
            button1.Click += button1_Click;
            right.Controls.Add(button1);

            button2 = MakeButton("Yenilə", new Rectangle(6, 294, 178, 28));
            button2.Click += button2_Click;
            right.Controls.Add(button2);

            button3 = MakeButton("Hesabla", new Rectangle(6, 326, 178, 28));
            button3.Click += button3_Click;
            right.Controls.Add(button3);

            right.Controls.Add(MakeLabel("Hesab:", new Rectangle(6, 360, 178, 18),
                bold9, Color.White, ContentAlignment.MiddleLeft));
            textBox2 = new TextBox
            {
                Location = new Point(6, 380),
                Size = new Size(178, 23),
                ReadOnly = true,
                BackColor = Color.White
            };
            right.Controls.Add(textBox2);
        }

        private Label MakeLabel(string text, Rectangle r, Font font, Color foreColor, ContentAlignment align)
        {
            return new Label
            {
                Text = text,
                Location = r.Location,
                Size = r.Size,
                Font = font,
                ForeColor = foreColor,
                TextAlign = align,
                AutoSize = false,
                BackColor = Color.Transparent
            };
        }

        private Button MakeButton(string text, Rectangle r)
        {
            Button b = new Button
            {
                Text = text,
                Location = r.Location,
                Size = r.Size,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(225, 225, 225),
                Font = new Font("Segoe UI", 9f),
                Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

    
        private void AddMenuItem(Panel parent, string emoji, string name, double price, int col, int row)
        {
            int x = 10 + col * 115;
            int y = 55 + row * 127;

            Button btn = new Button
            {
                Text = emoji,
                Font = new Font("Segoe UI Emoji", 32f),
                Location = new Point(x, y),
                Size = new Size(105, 78),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.Gainsboro;
            btn.Click += (s, e) => AddToCart(name, price);
            parent.Controls.Add(btn);

            string priceText = price.ToString("0.0#", CultureInfo.InvariantCulture);
            parent.Controls.Add(MakeLabel(name + "\n" + priceText, new Rectangle(x, y + 80, 105, 34),
                new Font("Segoe UI", 9f), Color.Black, ContentAlignment.TopCenter));
        }

    

        private void AddToCart(string name, double price)
        {
            listBox1.Items.Add(name + " - " + price.ToString("0.0#", CultureInfo.InvariantCulture));
            RecalculateTotal();
        }

        // "Burger - 6.3" -> "Burger"
        private string GetName(string item)
        {
            return item.Substring(0, item.LastIndexOf(" - "));
        }

        // "Burger - 6.3" -> 6.3
        private double GetPrice(string item)
        {
            string priceText = item.Substring(item.LastIndexOf(" - ") + 3);
            return double.Parse(priceText, CultureInfo.InvariantCulture);
        }

        // Cəmi hər dəfə siyahıdan yenidən hesablayırıq ki, səhv yığılmasın
        private void RecalculateTotal()
        {
            total = 0;
            foreach (string item in listBox1.Items)
            {
                total += GetPrice(item);
            }
            total = Math.Round(total, 2);
        }


        
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Silmək üçün səbətdən yemək seçin!");
                return;
            }

            string name = GetName(listBox1.SelectedItem.ToString());
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            RecalculateTotal();

            MessageBox.Show(name + " səbətdən silindi");
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Xanalar sıfırlansınmı?",
                "Yenilə",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                listBox1.Items.Clear();
                total = 0;
                textBox1.Clear();   // Məbləğ
                textBox2.Clear();   // Hesab
                label4.Text = "0";  // Qalıq
            }
            
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Səbətdə yemək yoxdur!");
                return;
            }

            textBox2.Text = total.ToString("0.00", CultureInfo.InvariantCulture);
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Səbətdə yemək yoxdur!");
                return;
            }

        
            string text = textBox1.Text.Trim().Replace(',', '.');
            double amount;

            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out amount))
            {
                MessageBox.Show("Məbləği düzgün daxil edin!");
                return;
            }

            amount = Math.Round(amount, 2);

            if (amount < total)
            {
                MessageBox.Show("Daxil edilən məbləğ hesabdan azdır");
                return;
            }

            double change = amount - total;
            label4.Text = change.ToString("0.00", CultureInfo.InvariantCulture);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            label4.Text = "0";
        }
    }
}
