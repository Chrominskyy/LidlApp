// Decompiled with JetBrains decompiler
// Type: LidlAvailabilityNotifierUI.Form1
// Assembly: LidlAvailabilityNotifierUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5865371B-D735-43D4-A5D6-EC04D94FE962
// Assembly location: C:\Users\Bartek\Downloads\LidlAvailabilityNotifierUI.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace LidlAvailabilityNotifierUI
{
  public class Form1 : Form
  {
    public static string url = "https://www.lidl-sklep.pl/p/api/gridboxes/PL/pl?erpNumbers={itemCode}";
    private IContainer components = (IContainer) null;
    private GroupBox groupBox1;
    private TextBox tbProductName;
    private Label label2;
    private Button btnAddCode;
    private TextBox tbProductCode;
    private Label label1;
    private Button btnStart;
    private CheckBox cbSendEmail;
    private Label label3;
    private ListView listView1;
    private ColumnHeader productCode;
    private ColumnHeader productName;
    private ColumnHeader productStatus;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripStatusLabel ttStatus;
    private System.Windows.Forms.Timer timer1;

    public Form1() => this.InitializeComponent();

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
      if (this.btnStart.Text == "Start")
      {
        this.timer1.Enabled = true;
        this.btnStart.Text = "Stop";
        this.groupBox1.Enabled = false;
        this.ttStatus.Text = "Czekam ...";
      }
      else
      {
        this.timer1.Enabled = false;
        this.btnStart.Text = "Start";
        this.groupBox1.Enabled = true;
        this.ttStatus.Text = "Opierdala się jak murzyn";
      }
    }

    private void btnAddCode_Click(object sender, EventArgs e) => this.listView1.Items.Add(new ListViewItem()
    {
      Text = this.tbProductCode.Text,
      SubItems = {
        this.tbProductName.Text,
        "Niesprawdzony"
      }
    });

    public void CheckStatus(string productCode)
    {
      if (Encoding.UTF8.GetString(new WebClient().DownloadData(Form1.url.Replace("{itemCode}", productCode))).Contains("onlineAvailable\":true"))
      {
        this.listView1.FindItemWithText(productCode, true, 0).SubItems[2].Text = "Dostępny";
        this.listView1.FindItemWithText(productCode, true, 0).UseItemStyleForSubItems = false;
        this.listView1.FindItemWithText(productCode, true, 0).SubItems[2].BackColor = Color.GreenYellow;
        Form1.SendMail(productCode);
        int num = (int) MessageBox.Show(DateTime.Now.ToString() + " KURWA KUPUJ!!!!!!!!! https://www.lidl-sklep.pl/p/" + productCode, "JEST KURWA DOSTĘPNEEEEEEE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        this.listView1.FindItemWithText(productCode, true, 0).SubItems[2].Text = "Niedostępny";
        this.listView1.FindItemWithText(productCode, true, 0).UseItemStyleForSubItems = false;
        this.listView1.FindItemWithText(productCode, true, 0).SubItems[2].BackColor = Color.Red;
        this.listView1.FindItemWithText(productCode, true, 0).SubItems[2].ForeColor = Color.White;
      }
    }

    public static void SendMail(string productCode)
    {
      string str1 = "Produkt o kodzie " + productCode + " jest dostępny";
      string str2 = "https://www.lidl-sklep.pl/p/" + productCode;
      MailAddress from = new MailAddress("<<MailFrom>>", "APKA DO SPRAWDZANIA STANU LIDLA");
      MailAddress to = new MailAddress("<<MailTo>>", "Bartosz Chromiński");
      using (MailMessage message = new MailMessage(from, to)
      {
        Subject = str1,
        Body = str2
      })
        new SmtpClient()
        {
          Host = "smtp.gmail.com",
          Port = 587,
          EnableSsl = true,
          DeliveryMethod = SmtpDeliveryMethod.Network,
          UseDefaultCredentials = false,
          Credentials = ((ICredentialsByHost) new NetworkCredential(from.Address, "<<MailFromPassword>>"))
        }.Send(message);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.ttStatus.Text = "Pracuję ...";
      foreach (ListViewItem listViewItem in this.listView1.Items)
        this.CheckStatus(listViewItem.Text);
      this.timer1.Stop();
      this.timer1.Start();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      ListViewItem listViewItem = new ListViewItem();
      this.listView1.Items.Add(new ListViewItem()
      {
        Text = "100315805",
        SubItems = {
          "Młotowiertarka",
          "Niesprawdzony"
        }
      });
      this.timer1.Enabled = true;
      this.btnStart.Text = "Stop";
      this.groupBox1.Enabled = false;
      this.ttStatus.Text = "Czekam ...";
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.groupBox1 = new GroupBox();
      this.btnStart = new Button();
      this.label1 = new Label();
      this.tbProductCode = new TextBox();
      this.btnAddCode = new Button();
      this.label2 = new Label();
      this.tbProductName = new TextBox();
      this.label3 = new Label();
      this.cbSendEmail = new CheckBox();
      this.listView1 = new ListView();
      this.productCode = new ColumnHeader();
      this.productName = new ColumnHeader();
      this.productStatus = new ColumnHeader();
      this.statusStrip1 = new StatusStrip();
      this.toolStripStatusLabel1 = new ToolStripStatusLabel();
      this.ttStatus = new ToolStripStatusLabel();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.groupBox1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      this.groupBox1.Controls.Add((Control) this.cbSendEmail);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.tbProductName);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.btnAddCode);
      this.groupBox1.Controls.Add((Control) this.tbProductCode);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Location = new Point(13, 13);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(456, 99);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Ustawienia";
      this.btnStart.Location = new Point(23, 118);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new Size(443, 23);
      this.btnStart.TabIndex = 0;
      this.btnStart.Text = "Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new EventHandler(this.btnStart_Click);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(7, 20);
      this.label1.Name = "label1";
      this.label1.Size = new Size(77, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Kod produktu :";
      this.tbProductCode.Location = new Point(99, 17);
      this.tbProductCode.Name = "tbProductCode";
      this.tbProductCode.Size = new Size(144, 20);
      this.tbProductCode.TabIndex = 2;
      this.btnAddCode.Location = new Point(306, 43);
      this.btnAddCode.Name = "btnAddCode";
      this.btnAddCode.Size = new Size(144, 40);
      this.btnAddCode.TabIndex = 3;
      this.btnAddCode.Text = "Dodaj";
      this.btnAddCode.UseVisualStyleBackColor = true;
      this.btnAddCode.Click += new EventHandler(this.btnAddCode_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(6, 47);
      this.label2.Name = "label2";
      this.label2.Size = new Size(94, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Nazwa produktu : ";
      this.label2.Click += new EventHandler(this.label2_Click);
      this.tbProductName.Location = new Point(99, 44);
      this.tbProductName.Name = "tbProductName";
      this.tbProductName.Size = new Size(144, 20);
      this.tbProductName.TabIndex = 6;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(7, 70);
      this.label3.Name = "label3";
      this.label3.Size = new Size(61, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Wyślij email";
      this.cbSendEmail.AutoSize = true;
      this.cbSendEmail.Location = new Point(99, 70);
      this.cbSendEmail.Name = "cbSendEmail";
      this.cbSendEmail.Size = new Size(15, 14);
      this.cbSendEmail.TabIndex = 8;
      this.cbSendEmail.UseVisualStyleBackColor = true;
      this.listView1.Columns.AddRange(new ColumnHeader[3]
      {
        this.productCode,
        this.productName,
        this.productStatus
      });
      this.listView1.Font = new Font("Microsoft Sans Serif", 12f);
      this.listView1.HideSelection = false;
      this.listView1.Location = new Point(13, 147);
      this.listView1.Name = "listView1";
      this.listView1.Size = new Size(456, 235);
      this.listView1.TabIndex = 1;
      this.listView1.UseCompatibleStateImageBehavior = false;
      this.listView1.View = View.Details;
      this.productCode.Text = "Kod produktu";
      this.productCode.Width = 102;
      this.productName.Text = "Nazwa produktu";
      this.productName.Width = 161;
      this.productStatus.Text = "Status produktu";
      this.productStatus.Width = 97;
      this.statusStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.toolStripStatusLabel1,
        (ToolStripItem) this.ttStatus
      });
      this.statusStrip1.Location = new Point(0, 411);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new Size(478, 22);
      this.statusStrip1.TabIndex = 2;
      this.statusStrip1.Text = "statusStrip1";
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new Size(45, 17);
      this.toolStripStatusLabel1.Text = "Status :";
      this.ttStatus.Name = "ttStatus";
      this.ttStatus.Size = new Size(136, 17);
      this.ttStatus.Text = "Opierdala się jak murzyn";
      this.timer1.Interval = 10000;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(478, 433);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.listView1);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.btnStart);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Name = nameof (Form1);
      this.Text = "Stan magazynowy Lidla";
      this.Load += new EventHandler(this.Form1_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
