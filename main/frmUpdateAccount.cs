using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmUpdateAccount : Form
{
	private string username;

	private string editusername;

	private string editpassword;

	private string edittype;

	private string editstatus;

	private int errorcount;

	private frmAccounts frmAccounts_load;

	private Class1 updateaccount = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private IContainer components;

	private ErrorProvider errorProvider1;

	private Label label1;

	private PictureBox btn_close;

	private PictureBox btn_minimize;

	private PictureBox pictureBox1;

	private Panel panel1;

	private ComboBox cmbstatus;

	private Label labelstatus;

	private Panel panel2;

	private PictureBox pictureBox4;

	private PictureBox pictureBox3;

	private CheckBox cbshow;

	private Label labelpassword;

	private TextBox txtpassword;

	private Label labelusername;

	private TextBox txtusername;

	private Panel panel4;

	private PictureBox pictureBox5;

	private ComboBox cmbtype;

	private Label label5;

	private Panel panel5;

	private Button btnclear;

	private PictureBox pictureBox2;

	private Panel panelbtnlogin;

	private Button btnsave;

	private PictureBox pictureBoxbtnsave;

	private PictureBox pictureBox6;

	private Panel panel8;

	private Panel panel7;

	private Panel panel6;

	private Panel panel3;

	private Label label2;

	private void validateForm()
	{
		errorProvider1.Clear();
		errorcount = 0;
		if (string.IsNullOrEmpty(txtpassword.Text))
		{
			errorProvider1.SetError(txtpassword, "Password is empty");
			errorcount++;
		}
		if (txtpassword.TextLength < 6)
		{
			errorProvider1.SetError(txtpassword, "Password must be atleast 6 characters");
			errorcount++;
		}
		if (cmbtype.SelectedIndex < 0)
		{
			errorProvider1.SetError(cmbtype, "Select usertype");
			errorcount++;
		}
		if (cmbstatus.SelectedIndex < 0)
		{
			errorProvider1.SetError(cmbstatus, "Select status");
			errorcount++;
		}
	}

	private void btnsave_Click_1(object sender, EventArgs e)
	{
		validateForm();
		if (errorcount != 0 || MessageBox.Show("Are you sure you want to update this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		try
		{
			updateaccount.executeSQL("UPDATE tblaccounts SET password = '" + txtpassword.Text + "', usertype = '" + cmbtype.Text.ToUpper() + "', status = '" + cmbstatus.Text.ToUpper() + "' WHERE username = '" + txtusername.Text + "'");
			if (updateaccount.rowAffected > 0)
			{
				updateaccount.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Update', 'Accounts Management', '" + txtusername.Text + "', '" + username + "')");
				MessageBox.Show("Account Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				frmAccounts_load.LoadAccounts();
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on save", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnclear_Click_1(object sender, EventArgs e)
	{
		errorProvider1.Clear();
		txtpassword.Clear();
		cmbtype.SelectedIndex = -1;
		cmbstatus.SelectedIndex = -1;
		txtpassword.Focus();
	}

	private void cbshow_CheckedChanged(object sender, EventArgs e)
	{
		if (cbshow.Checked)
		{
			txtpassword.PasswordChar = '\0';
		}
		else
		{
			txtpassword.PasswordChar = '*';
		}
	}

	public frmUpdateAccount(frmAccounts frmAccounts_load, string editusername, string editpassword, string edittype, string editstatus, string username)
	{
		InitializeComponent();
		this.username = username;
		this.editusername = editusername;
		this.editpassword = editpassword;
		this.edittype = edittype;
		this.editstatus = editstatus;
		this.Draggable(enable: true);
		this.frmAccounts_load = frmAccounts_load;
	}

	private void frmUpdateAccount_Load(object sender, EventArgs e)
	{
		txtusername.Text = editusername;
		txtpassword.Text = editpassword;
		if (edittype == "ADMNISTRATOR")
		{
			cmbtype.SelectedIndex = 0;
		}
		else if (edittype == "BRANCH ADMINISTRATOR")
		{
			cmbtype.SelectedIndex = 1;
		}
		else
		{
			cmbtype.SelectedIndex = 2;
		}
		if (editstatus == "ACTIVE")
		{
			cmbstatus.SelectedIndex = 0;
		}
		else
		{
			cmbstatus.SelectedIndex = 1;
		}
	}

	private void btn_minimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void btn_close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_minimize_MouseHover(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.Silver;
		btn_minimize.BorderStyle = BorderStyle.FixedSingle;
	}

	private void btn_minimize_MouseLeave(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.Transparent;
		btn_minimize.BorderStyle = BorderStyle.None;
	}

	private void btn_close_MouseHover(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.Salmon;
		btn_close.BorderStyle = BorderStyle.FixedSingle;
	}

	private void btn_close_MouseLeave(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.Transparent;
		btn_close.BorderStyle = BorderStyle.None;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmUpdateAccount));
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.label1 = new System.Windows.Forms.Label();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.labelstatus = new System.Windows.Forms.Label();
		this.cmbstatus = new System.Windows.Forms.ComboBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.pictureBox4 = new System.Windows.Forms.PictureBox();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.cbshow = new System.Windows.Forms.CheckBox();
		this.labelpassword = new System.Windows.Forms.Label();
		this.txtpassword = new System.Windows.Forms.TextBox();
		this.labelusername = new System.Windows.Forms.Label();
		this.txtusername = new System.Windows.Forms.TextBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.pictureBox6 = new System.Windows.Forms.PictureBox();
		this.pictureBox5 = new System.Windows.Forms.PictureBox();
		this.cmbtype = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btnclear = new System.Windows.Forms.Button();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.panelbtnlogin = new System.Windows.Forms.Panel();
		this.btnsave = new System.Windows.Forms.Button();
		this.pictureBoxbtnsave = new System.Windows.Forms.PictureBox();
		this.panel8 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panel1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).BeginInit();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		this.panelbtnlogin.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).BeginInit();
		base.SuspendLayout();
		this.errorProvider1.ContainerControl = this;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 12.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.SystemColors.Control;
		this.label1.Location = new System.Drawing.Point(53, 18);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(163, 21);
		this.label1.TabIndex = 23;
		this.label1.Text = "UPDATE ACCOUNT";
		this.btn_close.BackColor = System.Drawing.Color.Transparent;
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(361, 12);
		this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_close.Name = "btn_close";
		this.btn_close.Padding = new System.Windows.Forms.Padding(3);
		this.btn_close.Size = new System.Drawing.Size(28, 25);
		this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_close.TabIndex = 21;
		this.btn_close.TabStop = false;
		this.btn_close.Click += new System.EventHandler(btn_close_Click);
		this.btn_close.MouseLeave += new System.EventHandler(btn_close_MouseLeave);
		this.btn_close.MouseHover += new System.EventHandler(btn_close_MouseHover);
		this.btn_minimize.BackColor = System.Drawing.Color.Transparent;
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(325, 12);
		this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_minimize.Name = "btn_minimize";
		this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
		this.btn_minimize.Size = new System.Drawing.Size(28, 25);
		this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_minimize.TabIndex = 22;
		this.btn_minimize.TabStop = false;
		this.btn_minimize.Click += new System.EventHandler(btn_minimize_Click);
		this.btn_minimize.MouseLeave += new System.EventHandler(btn_minimize_MouseLeave);
		this.btn_minimize.MouseHover += new System.EventHandler(btn_minimize_MouseHover);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(12, 12);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(35, 34);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 24;
		this.pictureBox1.TabStop = false;
		this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.pictureBox1);
		this.panel1.Controls.Add(this.btn_minimize);
		this.panel1.Controls.Add(this.btn_close);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(400, 56);
		this.panel1.TabIndex = 25;
		this.labelstatus.AutoSize = true;
		this.labelstatus.BackColor = System.Drawing.Color.Transparent;
		this.labelstatus.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelstatus.ForeColor = System.Drawing.SystemColors.Control;
		this.labelstatus.Location = new System.Drawing.Point(42, 46);
		this.labelstatus.Name = "labelstatus";
		this.labelstatus.Size = new System.Drawing.Size(49, 16);
		this.labelstatus.TabIndex = 15;
		this.labelstatus.Text = "Status:";
		this.cmbstatus.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbstatus.FormattingEnabled = true;
		this.cmbstatus.Items.AddRange(new object[2] { "Active", "Inactive" });
		this.cmbstatus.Location = new System.Drawing.Point(109, 41);
		this.cmbstatus.Name = "cmbstatus";
		this.cmbstatus.Size = new System.Drawing.Size(109, 23);
		this.cmbstatus.TabIndex = 16;
		this.panel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.panel2.Controls.Add(this.pictureBox4);
		this.panel2.Controls.Add(this.pictureBox3);
		this.panel2.Controls.Add(this.cbshow);
		this.panel2.Controls.Add(this.labelpassword);
		this.panel2.Controls.Add(this.txtpassword);
		this.panel2.Controls.Add(this.labelusername);
		this.panel2.Controls.Add(this.txtusername);
		this.panel2.Location = new System.Drawing.Point(30, 106);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(339, 106);
		this.panel2.TabIndex = 83;
		this.pictureBox4.Image = (System.Drawing.Image)resources.GetObject("pictureBox4.Image");
		this.pictureBox4.Location = new System.Drawing.Point(7, 43);
		this.pictureBox4.Name = "pictureBox4";
		this.pictureBox4.Padding = new System.Windows.Forms.Padding(3);
		this.pictureBox4.Size = new System.Drawing.Size(39, 33);
		this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox4.TabIndex = 4;
		this.pictureBox4.TabStop = false;
		this.pictureBox3.Image = (System.Drawing.Image)resources.GetObject("pictureBox3.Image");
		this.pictureBox3.Location = new System.Drawing.Point(7, 4);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Padding = new System.Windows.Forms.Padding(3);
		this.pictureBox3.Size = new System.Drawing.Size(39, 33);
		this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox3.TabIndex = 3;
		this.pictureBox3.TabStop = false;
		this.cbshow.AutoSize = true;
		this.cbshow.BackColor = System.Drawing.Color.Transparent;
		this.cbshow.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.cbshow.ForeColor = System.Drawing.SystemColors.Control;
		this.cbshow.Location = new System.Drawing.Point(188, 77);
		this.cbshow.Name = "cbshow";
		this.cbshow.Size = new System.Drawing.Size(111, 19);
		this.cbshow.TabIndex = 2;
		this.cbshow.Text = "Show password";
		this.cbshow.UseVisualStyleBackColor = false;
		this.labelpassword.AutoSize = true;
		this.labelpassword.BackColor = System.Drawing.Color.Transparent;
		this.labelpassword.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelpassword.ForeColor = System.Drawing.SystemColors.Control;
		this.labelpassword.Location = new System.Drawing.Point(47, 53);
		this.labelpassword.Name = "labelpassword";
		this.labelpassword.Size = new System.Drawing.Size(62, 15);
		this.labelpassword.TabIndex = 1;
		this.labelpassword.Text = "Password:";
		this.txtpassword.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtpassword.Location = new System.Drawing.Point(118, 50);
		this.txtpassword.Name = "txtpassword";
		this.txtpassword.PasswordChar = '*';
		this.txtpassword.Size = new System.Drawing.Size(191, 22);
		this.txtpassword.TabIndex = 0;
		this.labelusername.AutoSize = true;
		this.labelusername.BackColor = System.Drawing.Color.Transparent;
		this.labelusername.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelusername.ForeColor = System.Drawing.SystemColors.Control;
		this.labelusername.Location = new System.Drawing.Point(47, 16);
		this.labelusername.Name = "labelusername";
		this.labelusername.Size = new System.Drawing.Size(65, 15);
		this.labelusername.TabIndex = 1;
		this.labelusername.Text = "Username:";
		this.txtusername.Enabled = false;
		this.txtusername.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtusername.Location = new System.Drawing.Point(118, 13);
		this.txtusername.Name = "txtusername";
		this.txtusername.Size = new System.Drawing.Size(191, 22);
		this.txtusername.TabIndex = 0;
		this.panel4.BackColor = System.Drawing.Color.Firebrick;
		this.panel4.Controls.Add(this.pictureBox6);
		this.panel4.Controls.Add(this.pictureBox5);
		this.panel4.Controls.Add(this.cmbtype);
		this.panel4.Controls.Add(this.label5);
		this.panel4.Controls.Add(this.labelstatus);
		this.panel4.Controls.Add(this.cmbstatus);
		this.panel4.Location = new System.Drawing.Point(30, 222);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(339, 79);
		this.panel4.TabIndex = 82;
		this.pictureBox6.Image = (System.Drawing.Image)resources.GetObject("pictureBox6.Image");
		this.pictureBox6.Location = new System.Drawing.Point(7, 38);
		this.pictureBox6.Name = "pictureBox6";
		this.pictureBox6.Padding = new System.Windows.Forms.Padding(2);
		this.pictureBox6.Size = new System.Drawing.Size(32, 28);
		this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox6.TabIndex = 17;
		this.pictureBox6.TabStop = false;
		this.pictureBox5.Image = (System.Drawing.Image)resources.GetObject("pictureBox5.Image");
		this.pictureBox5.Location = new System.Drawing.Point(7, 4);
		this.pictureBox5.Name = "pictureBox5";
		this.pictureBox5.Padding = new System.Windows.Forms.Padding(3);
		this.pictureBox5.Size = new System.Drawing.Size(32, 28);
		this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox5.TabIndex = 5;
		this.pictureBox5.TabStop = false;
		this.cmbtype.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbtype.FormattingEnabled = true;
		this.cmbtype.Items.AddRange(new object[3] { "Administrator", "Branch Administrator", "Staff" });
		this.cmbtype.Location = new System.Drawing.Point(110, 11);
		this.cmbtype.Name = "cmbtype";
		this.cmbtype.Size = new System.Drawing.Size(191, 23);
		this.cmbtype.TabIndex = 3;
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.SystemColors.Control;
		this.label5.Location = new System.Drawing.Point(41, 13);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(59, 15);
		this.label5.TabIndex = 1;
		this.label5.Text = "Usertype:";
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.btnclear);
		this.panel5.Controls.Add(this.pictureBox2);
		this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panel5.Location = new System.Drawing.Point(248, 330);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(94, 30);
		this.panel5.TabIndex = 85;
		this.btnclear.BackColor = System.Drawing.Color.Firebrick;
		this.btnclear.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnclear.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnclear.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
		this.btnclear.FlatAppearance.BorderSize = 0;
		this.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnclear.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnclear.ForeColor = System.Drawing.SystemColors.ButtonFace;
		this.btnclear.Location = new System.Drawing.Point(33, 0);
		this.btnclear.Margin = new System.Windows.Forms.Padding(0);
		this.btnclear.Name = "btnclear";
		this.btnclear.Size = new System.Drawing.Size(59, 28);
		this.btnclear.TabIndex = 5;
		this.btnclear.Text = "&Clear";
		this.btnclear.UseVisualStyleBackColor = false;
		this.btnclear.Click += new System.EventHandler(btnclear_Click_1);
		this.pictureBox2.BackColor = System.Drawing.Color.Firebrick;
		this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(0, 0);
		this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Padding = new System.Windows.Forms.Padding(3);
		this.pictureBox2.Size = new System.Drawing.Size(33, 28);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 21;
		this.pictureBox2.TabStop = false;
		this.panelbtnlogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelbtnlogin.Controls.Add(this.btnsave);
		this.panelbtnlogin.Controls.Add(this.pictureBoxbtnsave);
		this.panelbtnlogin.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panelbtnlogin.Location = new System.Drawing.Point(50, 330);
		this.panelbtnlogin.Name = "panelbtnlogin";
		this.panelbtnlogin.Size = new System.Drawing.Size(168, 29);
		this.panelbtnlogin.TabIndex = 84;
		this.btnsave.BackColor = System.Drawing.SystemColors.Highlight;
		this.btnsave.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnsave.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnsave.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
		this.btnsave.FlatAppearance.BorderSize = 0;
		this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnsave.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnsave.ForeColor = System.Drawing.SystemColors.ButtonFace;
		this.btnsave.Location = new System.Drawing.Point(33, 0);
		this.btnsave.Margin = new System.Windows.Forms.Padding(0);
		this.btnsave.Name = "btnsave";
		this.btnsave.Size = new System.Drawing.Size(133, 27);
		this.btnsave.TabIndex = 5;
		this.btnsave.Text = "&Save";
		this.btnsave.UseVisualStyleBackColor = false;
		this.btnsave.Click += new System.EventHandler(btnsave_Click_1);
		this.pictureBoxbtnsave.BackColor = System.Drawing.SystemColors.Highlight;
		this.pictureBoxbtnsave.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBoxbtnsave.Image = (System.Drawing.Image)resources.GetObject("pictureBoxbtnsave.Image");
		this.pictureBoxbtnsave.Location = new System.Drawing.Point(0, 0);
		this.pictureBoxbtnsave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.pictureBoxbtnsave.Name = "pictureBoxbtnsave";
		this.pictureBoxbtnsave.Padding = new System.Windows.Forms.Padding(3);
		this.pictureBoxbtnsave.Size = new System.Drawing.Size(33, 27);
		this.pictureBoxbtnsave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBoxbtnsave.TabIndex = 21;
		this.pictureBoxbtnsave.TabStop = false;
		this.panel8.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(10, 375);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(380, 10);
		this.panel8.TabIndex = 88;
		this.panel7.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel7.Location = new System.Drawing.Point(390, 56);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(10, 329);
		this.panel7.TabIndex = 87;
		this.panel6.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel6.Location = new System.Drawing.Point(0, 56);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(10, 329);
		this.panel6.TabIndex = 86;
		this.panel3.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel3.Location = new System.Drawing.Point(30, 75);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(13, 23);
		this.panel3.TabIndex = 74;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.SystemColors.HotTrack;
		this.label2.Font = new System.Drawing.Font("Spiegel Bold", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.SystemColors.Control;
		this.label2.Location = new System.Drawing.Point(49, 75);
		this.label2.Name = "label2";
		this.label2.Padding = new System.Windows.Forms.Padding(2);
		this.label2.Size = new System.Drawing.Size(95, 24);
		this.label2.TabIndex = 73;
		this.label2.Text = "Information";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Azure;
		base.ClientSize = new System.Drawing.Size(400, 385);
		base.ControlBox = false;
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.panel8);
		base.Controls.Add(this.panel7);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panelbtnlogin);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.MaximizeBox = false;
		base.Name = "frmUpdateAccount";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Load += new System.EventHandler(frmUpdateAccount_Load);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).EndInit();
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		this.panelbtnlogin.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
