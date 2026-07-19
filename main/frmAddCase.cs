using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmAddCase : Form
{
	private Class1 addcase = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private string studentnumber;

	private string lastname;

	private string firstname;

	private string middlename;

	private string yearlevel;

	private string course;

	private string username;

	private int errorcount;

	private frmCaseManagement frmCaseManagement_Load;

	private IContainer components;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private Label labelviolation;

	private ComboBox cmbviolation;

	private Label labelviolationdescription;

	private TextBox txtviolationdescription;

	private TextBox txtstudentid;

	private TextBox txtcourse;

	private TextBox txtyearlevel;

	private TextBox txtmiddlename;

	private TextBox txtfirstname;

	private TextBox txtlastname;

	private Panel panelinformation;

	private Label labelinformation;

	private Panel panel1;

	private PictureBox pictureBox5;

	private Label labelAddViolation;

	private PictureBox btn_minimize;

	private PictureBox btn_close;

	private Panel panel5;

	private Button btnclear;

	private PictureBox pictureBox1;

	private Panel panelbtnlogin;

	private Button btnsave;

	private PictureBox pictureBoxbtnsave;

	private ErrorProvider errorProvider1;

	private Panel panel2;

	private Label labelviolationheader;

	private Panel panel10;

	private Panel panel9;

	private Panel panel8;

	private Label labelRecommendation;

	private Label labelConcernlevel;

	private Label labelSchoolYear;

	private TextBox txtschoolyear;

	private ComboBox cmbconcernlevel;

	private TextBox txtrecommendation;

	private void cmbviolation_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cmbviolation.SelectedIndex != -1)
		{
			string selectedViolationCode = cmbviolation.SelectedItem.ToString();
			string query = "SELECT description FROM tblviolations WHERE violationcode = '" + selectedViolationCode + "';";
			DataTable result = addcase.GetData(query);
			if (result.Rows.Count > 0)
			{
				string description = result.Rows[0]["description"].ToString();
				txtviolationdescription.Text = description;
			}
		}
	}

	private void btnclear_Click(object sender, EventArgs e)
	{
		errorProvider1.Clear();
		cmbviolation.SelectedIndex = -1;
		txtviolationdescription.Text = "";
		txtschoolyear.Clear();
		cmbconcernlevel.SelectedIndex = -1;
		txtrecommendation.Clear();
	}

	private void validateForm()
	{
		errorProvider1.Clear();
		errorcount = 0;
		if (cmbviolation.SelectedIndex < 0)
		{
			errorProvider1.SetError(cmbviolation, "Select a violation");
			errorcount++;
		}
		if (string.IsNullOrEmpty(txtschoolyear.Text))
		{
			errorProvider1.SetError(txtschoolyear, "School Year is empty");
			errorcount++;
		}
		if (cmbconcernlevel.SelectedIndex < 0)
		{
			errorProvider1.SetError(cmbconcernlevel, "Select a concern level");
			errorcount++;
		}
		if (string.IsNullOrEmpty(txtrecommendation.Text))
		{
			errorProvider1.SetError(txtrecommendation, "Recommendation is empty");
			errorcount++;
		}
	}

	private void btnsave_Click(object sender, EventArgs e)
	{
		validateForm();
		if (errorcount != 0 || MessageBox.Show("Are you sure you want to add this case?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		try
		{
			string formattedDate = DateTime.Now.ToString("yyyyMMddHHmmss");
			string checkviolationcount = "SELECT studentID, violationID FROM tblcases WHERE violationID = '" + cmbviolation.Text + "' AND studentID = '" + studentnumber + "';";
			DataTable offensecount = addcase.GetData(checkviolationcount);
			string violationcount = "1st Offense";
			if (offensecount.Rows.Count == 1)
			{
				violationcount = "2nd Offense";
			}
			else if (offensecount.Rows.Count > 1)
			{
				violationcount = "Repeat Offense";
			}
			addcase.executeSQL("INSERT INTO tblcases (caseID, studentID, violationID, violationcount, status, resolution, createdby, datecreated, schoolyear, concernlevel, recommendation) VALUES ('" + formattedDate + "', '" + studentnumber + "', '" + cmbviolation.Text + "', '" + violationcount + "', 'On-going', '', '" + username + "', '" + DateTime.Now.ToShortDateString() + "', '" + txtschoolyear.Text + "', '" + cmbconcernlevel.Text + "', '" + txtrecommendation.Text + "')");
			if (addcase.rowAffected > 0)
			{
				addcase.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Add', 'Case Management', '" + formattedDate + "', '" + username + "')");
				MessageBox.Show("New case added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				frmCaseManagement_Load.LoadCases();
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on save", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void frmAddCase_Load(object sender, EventArgs e)
	{
		txtstudentid.Text = studentnumber;
		txtlastname.Text = lastname;
		txtfirstname.Text = firstname;
		txtmiddlename.Text = middlename;
		txtyearlevel.Text = yearlevel;
		txtcourse.Text = course;
		string query = "SELECT violationcode FROM tblviolations";
		DataTable getviolations = addcase.GetData(query);
		if (getviolations.Rows.Count <= 0)
		{
			return;
		}
		cmbviolation.Items.Clear();
		foreach (DataRow row in getviolations.Rows)
		{
			cmbviolation.Items.Add(row["violationcode"].ToString());
		}
	}

	public frmAddCase(frmCaseManagement frmCaseManagement_Load, string studentnumber, string lastname, string firstname, string middlename, string yearlevel, string course, string username)
	{
		InitializeComponent();
		this.studentnumber = studentnumber;
		this.lastname = lastname;
		this.firstname = firstname;
		this.middlename = middlename;
		this.yearlevel = yearlevel;
		this.course = course;
		this.username = username;
		this.frmCaseManagement_Load = frmCaseManagement_Load;
		this.Draggable(enable: true);
	}

	private void btn_minimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void btn_close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_minimize_MouseEnter(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.Silver;
	}

	private void btn_minimize_MouseLeave(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.FromArgb(150, 0, 52, 112);
	}

	private void btn_close_MouseEnter(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.Salmon;
	}

	private void btn_close_MouseLeave(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.FromArgb(150, 0, 52, 112);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmAddCase));
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.labelviolation = new System.Windows.Forms.Label();
		this.cmbviolation = new System.Windows.Forms.ComboBox();
		this.labelviolationdescription = new System.Windows.Forms.Label();
		this.txtviolationdescription = new System.Windows.Forms.TextBox();
		this.txtstudentid = new System.Windows.Forms.TextBox();
		this.txtcourse = new System.Windows.Forms.TextBox();
		this.txtyearlevel = new System.Windows.Forms.TextBox();
		this.txtmiddlename = new System.Windows.Forms.TextBox();
		this.txtfirstname = new System.Windows.Forms.TextBox();
		this.txtlastname = new System.Windows.Forms.TextBox();
		this.panelinformation = new System.Windows.Forms.Panel();
		this.labelinformation = new System.Windows.Forms.Label();
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
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.labelviolationheader = new System.Windows.Forms.Label();
		this.panel10 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.labelSchoolYear = new System.Windows.Forms.Label();
		this.labelConcernlevel = new System.Windows.Forms.Label();
		this.labelRecommendation = new System.Windows.Forms.Label();
		this.txtrecommendation = new System.Windows.Forms.TextBox();
		this.cmbconcernlevel = new System.Windows.Forms.ComboBox();
		this.txtschoolyear = new System.Windows.Forms.TextBox();
		this.panelinformation.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panelbtnlogin.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label6.Location = new System.Drawing.Point(258, 182);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(81, 14);
		this.label6.TabIndex = 11;
		this.label6.Text = "Strand/Course:";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label5.Location = new System.Drawing.Point(258, 160);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(35, 14);
		this.label5.TabIndex = 10;
		this.label5.Text = "Level:";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label4.Location = new System.Drawing.Point(18, 205);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(70, 14);
		this.label4.TabIndex = 9;
		this.label4.Text = "Middlename:";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label3.Location = new System.Drawing.Point(19, 182);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(58, 14);
		this.label3.TabIndex = 8;
		this.label3.Text = "Firstname:";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label2.Location = new System.Drawing.Point(19, 160);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(57, 14);
		this.label2.TabIndex = 7;
		this.label2.Text = "Lastname:";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label1.Location = new System.Drawing.Point(19, 116);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(104, 16);
		this.label1.TabIndex = 12;
		this.label1.Text = "Student number:";
		this.labelviolation.AutoSize = true;
		this.labelviolation.BackColor = System.Drawing.Color.Transparent;
		this.labelviolation.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelviolation.ForeColor = System.Drawing.SystemColors.ControlText;
		this.labelviolation.Location = new System.Drawing.Point(23, 282);
		this.labelviolation.Name = "labelviolation";
		this.labelviolation.Size = new System.Drawing.Size(99, 16);
		this.labelviolation.TabIndex = 13;
		this.labelviolation.Text = "Select violation:";
		this.cmbviolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbviolation.FormattingEnabled = true;
		this.cmbviolation.Location = new System.Drawing.Point(130, 282);
		this.cmbviolation.Name = "cmbviolation";
		this.cmbviolation.Size = new System.Drawing.Size(167, 21);
		this.cmbviolation.TabIndex = 14;
		this.cmbviolation.SelectedIndexChanged += new System.EventHandler(cmbviolation_SelectedIndexChanged);
		this.labelviolationdescription.AutoSize = true;
		this.labelviolationdescription.BackColor = System.Drawing.Color.Transparent;
		this.labelviolationdescription.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelviolationdescription.ForeColor = System.Drawing.SystemColors.ControlText;
		this.labelviolationdescription.Location = new System.Drawing.Point(24, 313);
		this.labelviolationdescription.Name = "labelviolationdescription";
		this.labelviolationdescription.Size = new System.Drawing.Size(128, 16);
		this.labelviolationdescription.TabIndex = 15;
		this.labelviolationdescription.Text = "Violation Description:";
		this.txtviolationdescription.Enabled = false;
		this.txtviolationdescription.Location = new System.Drawing.Point(25, 331);
		this.txtviolationdescription.Multiline = true;
		this.txtviolationdescription.Name = "txtviolationdescription";
		this.txtviolationdescription.Size = new System.Drawing.Size(516, 88);
		this.txtviolationdescription.TabIndex = 18;
		this.txtstudentid.Enabled = false;
		this.txtstudentid.Location = new System.Drawing.Point(126, 115);
		this.txtstudentid.Name = "txtstudentid";
		this.txtstudentid.ReadOnly = true;
		this.txtstudentid.Size = new System.Drawing.Size(219, 20);
		this.txtstudentid.TabIndex = 19;
		this.txtcourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtcourse.Enabled = false;
		this.txtcourse.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcourse.Location = new System.Drawing.Point(343, 180);
		this.txtcourse.Multiline = true;
		this.txtcourse.Name = "txtcourse";
		this.txtcourse.ReadOnly = true;
		this.txtcourse.Size = new System.Drawing.Size(194, 47);
		this.txtcourse.TabIndex = 24;
		this.txtyearlevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtyearlevel.Enabled = false;
		this.txtyearlevel.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtyearlevel.Location = new System.Drawing.Point(343, 155);
		this.txtyearlevel.Name = "txtyearlevel";
		this.txtyearlevel.ReadOnly = true;
		this.txtyearlevel.Size = new System.Drawing.Size(122, 22);
		this.txtyearlevel.TabIndex = 23;
		this.txtmiddlename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtmiddlename.Enabled = false;
		this.txtmiddlename.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtmiddlename.Location = new System.Drawing.Point(95, 205);
		this.txtmiddlename.Name = "txtmiddlename";
		this.txtmiddlename.ReadOnly = true;
		this.txtmiddlename.Size = new System.Drawing.Size(148, 22);
		this.txtmiddlename.TabIndex = 22;
		this.txtfirstname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtfirstname.Enabled = false;
		this.txtfirstname.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtfirstname.Location = new System.Drawing.Point(95, 180);
		this.txtfirstname.Name = "txtfirstname";
		this.txtfirstname.ReadOnly = true;
		this.txtfirstname.Size = new System.Drawing.Size(148, 22);
		this.txtfirstname.TabIndex = 21;
		this.txtlastname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtlastname.Enabled = false;
		this.txtlastname.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtlastname.Location = new System.Drawing.Point(95, 155);
		this.txtlastname.Name = "txtlastname";
		this.txtlastname.ReadOnly = true;
		this.txtlastname.Size = new System.Drawing.Size(148, 22);
		this.txtlastname.TabIndex = 20;
		this.panelinformation.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panelinformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelinformation.Controls.Add(this.labelinformation);
		this.panelinformation.Location = new System.Drawing.Point(14, 78);
		this.panelinformation.Name = "panelinformation";
		this.panelinformation.Size = new System.Drawing.Size(197, 25);
		this.panelinformation.TabIndex = 25;
		this.labelinformation.AutoSize = true;
		this.labelinformation.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelinformation.ForeColor = System.Drawing.Color.Azure;
		this.labelinformation.Location = new System.Drawing.Point(3, 2);
		this.labelinformation.Name = "labelinformation";
		this.labelinformation.Size = new System.Drawing.Size(190, 19);
		this.labelinformation.TabIndex = 0;
		this.labelinformation.Text = "STUDENT INFORMATION";
		this.panel1.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.pictureBox5);
		this.panel1.Controls.Add(this.labelAddViolation);
		this.panel1.Controls.Add(this.btn_minimize);
		this.panel1.Controls.Add(this.btn_close);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(570, 63);
		this.panel1.TabIndex = 26;
		this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox5.Image = (System.Drawing.Image)resources.GetObject("pictureBox5.Image");
		this.pictureBox5.Location = new System.Drawing.Point(0, 0);
		this.pictureBox5.Name = "pictureBox5";
		this.pictureBox5.Padding = new System.Windows.Forms.Padding(10, 7, 5, 7);
		this.pictureBox5.Size = new System.Drawing.Size(73, 61);
		this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox5.TabIndex = 29;
		this.pictureBox5.TabStop = false;
		this.labelAddViolation.AutoSize = true;
		this.labelAddViolation.BackColor = System.Drawing.Color.Transparent;
		this.labelAddViolation.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelAddViolation.ForeColor = System.Drawing.Color.White;
		this.labelAddViolation.Location = new System.Drawing.Point(74, 22);
		this.labelAddViolation.Name = "labelAddViolation";
		this.labelAddViolation.Size = new System.Drawing.Size(129, 20);
		this.labelAddViolation.TabIndex = 28;
		this.labelAddViolation.Text = "ADD NEW CASE";
		this.btn_minimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_minimize.BackColor = System.Drawing.Color.FromArgb(150, 0, 52, 112);
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(497, 12);
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
		this.btn_close.BackColor = System.Drawing.Color.FromArgb(150, 0, 52, 112);
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(529, 12);
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
		this.panel5.Location = new System.Drawing.Point(327, 618);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(94, 30);
		this.panel5.TabIndex = 79;
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
		this.panelbtnlogin.Location = new System.Drawing.Point(116, 618);
		this.panelbtnlogin.Name = "panelbtnlogin";
		this.panelbtnlogin.Size = new System.Drawing.Size(177, 29);
		this.panelbtnlogin.TabIndex = 78;
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
		this.btnsave.Size = new System.Drawing.Size(142, 27);
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
		this.errorProvider1.ContainerControl = this;
		this.panel2.BackColor = System.Drawing.Color.Firebrick;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.labelviolationheader);
		this.panel2.Location = new System.Drawing.Point(18, 249);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(102, 25);
		this.panel2.TabIndex = 26;
		this.labelviolationheader.AutoSize = true;
		this.labelviolationheader.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelviolationheader.ForeColor = System.Drawing.Color.Azure;
		this.labelviolationheader.Location = new System.Drawing.Point(3, 2);
		this.labelviolationheader.Name = "labelviolationheader";
		this.labelviolationheader.Size = new System.Drawing.Size(94, 19);
		this.labelviolationheader.TabIndex = 0;
		this.labelviolationheader.Text = "VIOLATION";
		this.panel10.BackColor = System.Drawing.Color.FromArgb(150, 0, 78, 168);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel10.Location = new System.Drawing.Point(10, 670);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(550, 10);
		this.panel10.TabIndex = 94;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(150, 0, 78, 168);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel9.Location = new System.Drawing.Point(560, 63);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(10, 617);
		this.panel9.TabIndex = 93;
		this.panel8.BackColor = System.Drawing.Color.FromArgb(150, 0, 78, 168);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel8.Location = new System.Drawing.Point(0, 63);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(10, 617);
		this.panel8.TabIndex = 92;
		this.labelSchoolYear.AutoSize = true;
		this.labelSchoolYear.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelSchoolYear.Location = new System.Drawing.Point(24, 428);
		this.labelSchoolYear.Name = "labelSchoolYear";
		this.labelSchoolYear.Size = new System.Drawing.Size(78, 16);
		this.labelSchoolYear.TabIndex = 95;
		this.labelSchoolYear.Text = "School Year:";
		this.labelConcernlevel.AutoSize = true;
		this.labelConcernlevel.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelConcernlevel.Location = new System.Drawing.Point(24, 455);
		this.labelConcernlevel.Name = "labelConcernlevel";
		this.labelConcernlevel.Size = new System.Drawing.Size(90, 16);
		this.labelConcernlevel.TabIndex = 96;
		this.labelConcernlevel.Text = "Concern Level:";
		this.labelRecommendation.AutoSize = true;
		this.labelRecommendation.Font = new System.Drawing.Font("Spiegel Regular", 9.75f);
		this.labelRecommendation.Location = new System.Drawing.Point(22, 488);
		this.labelRecommendation.Name = "labelRecommendation";
		this.labelRecommendation.Size = new System.Drawing.Size(113, 16);
		this.labelRecommendation.TabIndex = 97;
		this.labelRecommendation.Text = "Recommendation:";
		this.txtrecommendation.Location = new System.Drawing.Point(25, 506);
		this.txtrecommendation.Multiline = true;
		this.txtrecommendation.Name = "txtrecommendation";
		this.txtrecommendation.Size = new System.Drawing.Size(514, 63);
		this.txtrecommendation.TabIndex = 98;
		this.cmbconcernlevel.FormattingEnabled = true;
		this.cmbconcernlevel.Items.AddRange(new object[4] { "Prefect of Discipline", "Branch OSA", "Dean of OSA", "Council of Discipline" });
		this.cmbconcernlevel.Location = new System.Drawing.Point(120, 455);
		this.cmbconcernlevel.Name = "cmbconcernlevel";
		this.cmbconcernlevel.Size = new System.Drawing.Size(196, 21);
		this.cmbconcernlevel.TabIndex = 99;
		this.txtschoolyear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtschoolyear.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtschoolyear.Location = new System.Drawing.Point(119, 428);
		this.txtschoolyear.Name = "txtschoolyear";
		this.txtschoolyear.Size = new System.Drawing.Size(148, 22);
		this.txtschoolyear.TabIndex = 100;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(570, 680);
		base.ControlBox = false;
		base.Controls.Add(this.txtschoolyear);
		base.Controls.Add(this.cmbconcernlevel);
		base.Controls.Add(this.txtrecommendation);
		base.Controls.Add(this.labelRecommendation);
		base.Controls.Add(this.labelConcernlevel);
		base.Controls.Add(this.labelSchoolYear);
		base.Controls.Add(this.panel10);
		base.Controls.Add(this.panel9);
		base.Controls.Add(this.panel8);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panelbtnlogin);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panelinformation);
		base.Controls.Add(this.txtstudentid);
		base.Controls.Add(this.txtcourse);
		base.Controls.Add(this.txtyearlevel);
		base.Controls.Add(this.txtmiddlename);
		base.Controls.Add(this.txtfirstname);
		base.Controls.Add(this.txtlastname);
		base.Controls.Add(this.txtviolationdescription);
		base.Controls.Add(this.labelviolationdescription);
		base.Controls.Add(this.cmbviolation);
		base.Controls.Add(this.labelviolation);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "frmAddCase";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Load += new System.EventHandler(frmAddCase_Load);
		this.panelinformation.ResumeLayout(false);
		this.panelinformation.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panelbtnlogin.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
