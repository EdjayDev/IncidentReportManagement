using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmAddviolation : Form
{
	private string username;

	private int errorcount;

	private frmViolations frmViolations_load;

	private Class1 addviolation = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private IContainer components;

	private TextBox txtdescription;

	private Label labeldescription;

	private Label labelviolationcode;

	private TextBox txtviolationcode;

	private ComboBox cmbtype;

	private Label labeltype;

	private ComboBox cmbstatus;

	private Label labelstatus;

	private Panel panel1;

	private PictureBox btn_minimize;

	private PictureBox btn_close;

	private Label labelAddViolation;

	private Panel panel5;

	private Button btnclear;

	private PictureBox pictureBox1;

	private Panel panelbtnlogin;

	private Button btnsave;

	private PictureBox pictureBoxbtnsave;

	private Panel panel2;

	private PictureBox pictureBox2;

	private Panel panel3;

	private Panel panel4;

	private PictureBox pictureBox3;

	private Panel panel6;

	private PictureBox pictureBox4;

	private PictureBox pictureBox5;

	private Panel panel7;

	private ErrorProvider errorProvider1;

	private Panel panel10;

	private Panel panel9;

	private Panel panel8;

	public frmAddviolation(frmViolations frmViolations_load, string username)
	{
		InitializeComponent();
		this.username = username;
		this.frmViolations_load = frmViolations_load;
		this.Draggable(enable: true);
		cmbstatus.SelectedIndex = 0;
	}

	private void validateForm()
	{
		errorProvider1.Clear();
		errorcount = 0;
		if (string.IsNullOrEmpty(txtviolationcode.Text))
		{
			errorProvider1.SetError(txtviolationcode, "Violation code is empty");
			errorcount++;
		}
		if (string.IsNullOrEmpty(txtdescription.Text))
		{
			errorProvider1.SetError(txtdescription, "Description is empty");
			errorcount++;
		}
		if (cmbstatus.SelectedIndex < 0)
		{
			errorProvider1.SetError(cmbstatus, "Select status");
			errorcount++;
		}
		if (cmbtype.SelectedIndex < 0)
		{
			errorProvider1.SetError(cmbtype, "Select type");
			errorcount++;
		}
		try
		{
			if (addviolation.GetData("SELECT * FROM tblviolations WHERE violationcode = '" + txtviolationcode.Text + "'").Rows.Count > 0)
			{
				errorProvider1.SetError(txtviolationcode, "Violation ID already in use");
				errorcount++;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on validating existing violation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnsave_Click(object sender, EventArgs e)
	{
		validateForm();
		if (errorcount != 0 || MessageBox.Show("Are you sure you want to add this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		try
		{
			addviolation.executeSQL("INSERT INTO tblviolations (violationcode, description, type, status, createdby, datecreated) VALUES ('" + txtviolationcode.Text + "', '" + txtdescription.Text + "', '" + cmbtype.Text.ToUpper() + "', '" + cmbstatus.Text.ToUpper() + "', '" + username + "', '" + DateTime.Now.ToShortDateString() + "')");
			if (addviolation.rowAffected > 0)
			{
				addviolation.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Add', 'Violations Management', '" + txtviolationcode.Text + "', '" + username + "')");
				MessageBox.Show("New violation added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				frmViolations_load.LoadViolations();
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on save", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnclear_Click(object sender, EventArgs e)
	{
		errorProvider1.Clear();
		txtviolationcode.Clear();
		txtdescription.Clear();
		cmbtype.SelectedIndex = -1;
		txtviolationcode.Focus();
	}

	private void btn_close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_close_MouseEnter(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.Salmon;
	}

	private void btn_close_MouseLeave(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.FromArgb(135, 156, 34, 23);
	}

	private void btn_minimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void btn_minimize_MouseEnter(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.Silver;
	}

	private void btn_minimize_MouseLeave(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.FromArgb(135, 156, 34, 23);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmAddviolation));
		this.txtdescription = new System.Windows.Forms.TextBox();
		this.labeldescription = new System.Windows.Forms.Label();
		this.labelviolationcode = new System.Windows.Forms.Label();
		this.txtviolationcode = new System.Windows.Forms.TextBox();
		this.cmbtype = new System.Windows.Forms.ComboBox();
		this.labeltype = new System.Windows.Forms.Label();
		this.cmbstatus = new System.Windows.Forms.ComboBox();
		this.labelstatus = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.pictureBox5 = new System.Windows.Forms.PictureBox();
		this.labelAddViolation = new System.Windows.Forms.Label();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btnclear = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panelbtnlogin = new System.Windows.Forms.Panel();
		this.btnsave = new System.Windows.Forms.Button();
		this.pictureBoxbtnsave = new System.Windows.Forms.PictureBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.panel4 = new System.Windows.Forms.Panel();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.panel6 = new System.Windows.Forms.Panel();
		this.pictureBox4 = new System.Windows.Forms.PictureBox();
		this.panel7 = new System.Windows.Forms.Panel();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.panel10 = new System.Windows.Forms.Panel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panelbtnlogin.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		this.panel3.SuspendLayout();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		this.panel6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).BeginInit();
		this.panel7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.txtdescription.Location = new System.Drawing.Point(7, 8);
		this.txtdescription.Multiline = true;
		this.txtdescription.Name = "txtdescription";
		this.txtdescription.Size = new System.Drawing.Size(293, 87);
		this.txtdescription.TabIndex = 0;
		this.labeldescription.AutoSize = true;
		this.labeldescription.BackColor = System.Drawing.Color.Transparent;
		this.labeldescription.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labeldescription.ForeColor = System.Drawing.Color.Azure;
		this.labeldescription.Location = new System.Drawing.Point(6, 6);
		this.labeldescription.Name = "labeldescription";
		this.labeldescription.Size = new System.Drawing.Size(68, 15);
		this.labeldescription.TabIndex = 1;
		this.labeldescription.Text = "Description";
		this.labelviolationcode.AutoSize = true;
		this.labelviolationcode.BackColor = System.Drawing.Color.Transparent;
		this.labelviolationcode.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelviolationcode.ForeColor = System.Drawing.Color.Azure;
		this.labelviolationcode.Location = new System.Drawing.Point(49, 11);
		this.labelviolationcode.Name = "labelviolationcode";
		this.labelviolationcode.Size = new System.Drawing.Size(92, 16);
		this.labelviolationcode.TabIndex = 2;
		this.labelviolationcode.Text = "Violation Code:";
		this.txtviolationcode.Location = new System.Drawing.Point(147, 10);
		this.txtviolationcode.Name = "txtviolationcode";
		this.txtviolationcode.Size = new System.Drawing.Size(153, 20);
		this.txtviolationcode.TabIndex = 3;
		this.cmbtype.Cursor = System.Windows.Forms.Cursors.Hand;
		this.cmbtype.FormattingEnabled = true;
		this.cmbtype.Items.AddRange(new object[2] { "MINOR OFFENSE", "MAJOR OFFENSE" });
		this.cmbtype.Location = new System.Drawing.Point(87, 8);
		this.cmbtype.Name = "cmbtype";
		this.cmbtype.Size = new System.Drawing.Size(159, 21);
		this.cmbtype.TabIndex = 4;
		this.labeltype.AutoSize = true;
		this.labeltype.BackColor = System.Drawing.Color.Transparent;
		this.labeltype.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labeltype.ForeColor = System.Drawing.Color.Azure;
		this.labeltype.Location = new System.Drawing.Point(41, 10);
		this.labeltype.Name = "labeltype";
		this.labeltype.Size = new System.Drawing.Size(32, 14);
		this.labeltype.TabIndex = 5;
		this.labeltype.Text = "Type:";
		this.cmbstatus.Cursor = System.Windows.Forms.Cursors.Hand;
		this.cmbstatus.Enabled = false;
		this.cmbstatus.FormattingEnabled = true;
		this.cmbstatus.Items.AddRange(new object[2] { "ACTIVE", "INACTIVE" });
		this.cmbstatus.Location = new System.Drawing.Point(87, 8);
		this.cmbstatus.Name = "cmbstatus";
		this.cmbstatus.Size = new System.Drawing.Size(97, 21);
		this.cmbstatus.TabIndex = 6;
		this.labelstatus.AutoSize = true;
		this.labelstatus.BackColor = System.Drawing.Color.Transparent;
		this.labelstatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.labelstatus.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelstatus.ForeColor = System.Drawing.Color.Azure;
		this.labelstatus.Location = new System.Drawing.Point(40, 11);
		this.labelstatus.Name = "labelstatus";
		this.labelstatus.Size = new System.Drawing.Size(41, 14);
		this.labelstatus.TabIndex = 7;
		this.labelstatus.Text = "Status:";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(234, 51, 35);
		this.panel1.Controls.Add(this.pictureBox5);
		this.panel1.Controls.Add(this.labelAddViolation);
		this.panel1.Controls.Add(this.btn_minimize);
		this.panel1.Controls.Add(this.btn_close);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(367, 63);
		this.panel1.TabIndex = 8;
		this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox5.Image = (System.Drawing.Image)resources.GetObject("pictureBox5.Image");
		this.pictureBox5.Location = new System.Drawing.Point(0, 0);
		this.pictureBox5.Name = "pictureBox5";
		this.pictureBox5.Padding = new System.Windows.Forms.Padding(10, 7, 7, 7);
		this.pictureBox5.Size = new System.Drawing.Size(73, 63);
		this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox5.TabIndex = 29;
		this.pictureBox5.TabStop = false;
		this.labelAddViolation.AutoSize = true;
		this.labelAddViolation.BackColor = System.Drawing.Color.Transparent;
		this.labelAddViolation.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelAddViolation.ForeColor = System.Drawing.Color.White;
		this.labelAddViolation.Location = new System.Drawing.Point(74, 22);
		this.labelAddViolation.Name = "labelAddViolation";
		this.labelAddViolation.Size = new System.Drawing.Size(136, 20);
		this.labelAddViolation.TabIndex = 28;
		this.labelAddViolation.Text = "ADD VIOLATION";
		this.btn_minimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_minimize.BackColor = System.Drawing.Color.FromArgb(135, 156, 34, 23);
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(296, 12);
		this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_minimize.Name = "btn_minimize";
		this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
		this.btn_minimize.Size = new System.Drawing.Size(28, 25);
		this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_minimize.TabIndex = 26;
		this.btn_minimize.TabStop = false;
		this.btn_minimize.Click += new System.EventHandler(btn_minimize_Click);
		this.btn_minimize.MouseEnter += new System.EventHandler(btn_minimize_MouseEnter);
		this.btn_minimize.MouseLeave += new System.EventHandler(btn_minimize_MouseLeave);
		this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_close.BackColor = System.Drawing.Color.FromArgb(135, 156, 34, 23);
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(328, 12);
		this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_close.Name = "btn_close";
		this.btn_close.Padding = new System.Windows.Forms.Padding(3);
		this.btn_close.Size = new System.Drawing.Size(28, 25);
		this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_close.TabIndex = 25;
		this.btn_close.TabStop = false;
		this.btn_close.Click += new System.EventHandler(btn_close_Click);
		this.btn_close.MouseEnter += new System.EventHandler(btn_close_MouseEnter);
		this.btn_close.MouseLeave += new System.EventHandler(btn_close_MouseLeave);
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.btnclear);
		this.panel5.Controls.Add(this.pictureBox1);
		this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panel5.Location = new System.Drawing.Point(249, 397);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(94, 30);
		this.panel5.TabIndex = 77;
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
		this.btnclear.Click += new System.EventHandler(btnclear_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Firebrick;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Padding = new System.Windows.Forms.Padding(3);
		this.pictureBox1.Size = new System.Drawing.Size(33, 28);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 21;
		this.pictureBox1.TabStop = false;
		this.panelbtnlogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelbtnlogin.Controls.Add(this.btnsave);
		this.panelbtnlogin.Controls.Add(this.pictureBoxbtnsave);
		this.panelbtnlogin.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panelbtnlogin.Location = new System.Drawing.Point(24, 399);
		this.panelbtnlogin.Name = "panelbtnlogin";
		this.panelbtnlogin.Size = new System.Drawing.Size(148, 29);
		this.panelbtnlogin.TabIndex = 76;
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
		this.btnsave.Size = new System.Drawing.Size(113, 27);
		this.btnsave.TabIndex = 5;
		this.btnsave.Text = "&Save";
		this.btnsave.UseVisualStyleBackColor = false;
		this.btnsave.Click += new System.EventHandler(btnsave_Click);
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
		this.panel2.BackColor = System.Drawing.Color.FromArgb(218, 41, 28);
		this.panel2.Controls.Add(this.pictureBox2);
		this.panel2.Controls.Add(this.labelviolationcode);
		this.panel2.Controls.Add(this.txtviolationcode);
		this.panel2.Location = new System.Drawing.Point(24, 84);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(319, 39);
		this.panel2.TabIndex = 78;
		this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(135, 156, 34, 23);
		this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(0, 0);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Padding = new System.Windows.Forms.Padding(7);
		this.pictureBox2.Size = new System.Drawing.Size(49, 39);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 4;
		this.pictureBox2.TabStop = false;
		this.panel3.BackColor = System.Drawing.Color.FromArgb(218, 41, 28);
		this.panel3.Controls.Add(this.txtdescription);
		this.panel3.Location = new System.Drawing.Point(24, 175);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(319, 104);
		this.panel3.TabIndex = 79;
		this.panel4.BackColor = System.Drawing.Color.FromArgb(187, 41, 28);
		this.panel4.Controls.Add(this.pictureBox3);
		this.panel4.Controls.Add(this.cmbtype);
		this.panel4.Controls.Add(this.labeltype);
		this.panel4.Location = new System.Drawing.Point(24, 295);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(275, 35);
		this.panel4.TabIndex = 80;
		this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(135, 156, 34, 23);
		this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox3.Image = (System.Drawing.Image)resources.GetObject("pictureBox3.Image");
		this.pictureBox3.Location = new System.Drawing.Point(0, 0);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Padding = new System.Windows.Forms.Padding(7);
		this.pictureBox3.Size = new System.Drawing.Size(36, 35);
		this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox3.TabIndex = 5;
		this.pictureBox3.TabStop = false;
		this.panel6.BackColor = System.Drawing.Color.FromArgb(187, 41, 28);
		this.panel6.Controls.Add(this.pictureBox4);
		this.panel6.Controls.Add(this.labelstatus);
		this.panel6.Controls.Add(this.cmbstatus);
		this.panel6.Location = new System.Drawing.Point(24, 333);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(213, 35);
		this.panel6.TabIndex = 81;
		this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(135, 156, 34, 23);
		this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox4.Image = (System.Drawing.Image)resources.GetObject("pictureBox4.Image");
		this.pictureBox4.Location = new System.Drawing.Point(0, 0);
		this.pictureBox4.Name = "pictureBox4";
		this.pictureBox4.Padding = new System.Windows.Forms.Padding(5);
		this.pictureBox4.Size = new System.Drawing.Size(36, 35);
		this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox4.TabIndex = 5;
		this.pictureBox4.TabStop = false;
		this.panel7.BackColor = System.Drawing.Color.FromArgb(218, 41, 28);
		this.panel7.Controls.Add(this.labeldescription);
		this.panel7.Location = new System.Drawing.Point(24, 140);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(81, 29);
		this.panel7.TabIndex = 82;
		this.errorProvider1.BlinkRate = 360;
		this.errorProvider1.ContainerControl = this;
		this.panel10.BackColor = System.Drawing.Color.FromArgb(234, 51, 35);
		this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel10.Location = new System.Drawing.Point(10, 444);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(347, 10);
		this.panel10.TabIndex = 94;
		this.panel8.BackColor = System.Drawing.Color.FromArgb(234, 51, 35);
		this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel8.Location = new System.Drawing.Point(0, 63);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(10, 391);
		this.panel8.TabIndex = 92;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(234, 51, 35);
		this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel9.Location = new System.Drawing.Point(357, 63);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(10, 391);
		this.panel9.TabIndex = 93;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(367, 454);
		base.Controls.Add(this.panel10);
		base.Controls.Add(this.panel9);
		base.Controls.Add(this.panel8);
		base.Controls.Add(this.panel7);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panelbtnlogin);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panel6);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "frmAddviolation";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmAddviolation";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panelbtnlogin.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		this.panel6.ResumeLayout(false);
		this.panel6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).EndInit();
		this.panel7.ResumeLayout(false);
		this.panel7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
