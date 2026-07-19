using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmUpdateCase : Form
{
	private Class1 updatecase = new Class1(
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

	private string violationcode;

	private string violationdescription;

	private string status;

	private string caseid;

	private string resolution;

	private string schoolyear;

	private string concernlevel;

	private string recommendation;

	private int errorcount;

	private frmCaseManagement frmCaseManagement_load;

	private IContainer components;

	private Label label7;

	private ContextMenuStrip contextMenuStrip1;

	private Label labelstatus;

	private ComboBox cmbstatus;

	private Label labelresolution;

	private TextBox txtresolution;

	private Panel panel2;

	private Label labelviolationheader;

	private Panel panel5;

	private Button btnclear;

	private PictureBox pictureBox1;

	private Panel panelbtnlogin;

	private Button btnsave;

	private PictureBox pictureBoxbtnsave;

	private Panel panelinformation;

	private Label labelinformation;

	private TextBox txtstudentid;

	private TextBox txtcourse;

	private TextBox txtyearlevel;

	private TextBox txtmiddlename;

	private TextBox txtfirstname;

	private TextBox txtlastname;

	private TextBox txtviolationdescription;

	private Label labelviolationdescription;

	private Label label11;

	private Label label12;

	private Label label13;

	private Label label14;

	private Label label15;

	private Label label16;

	private TextBox txtviolationcode;

	private Panel panel1;

	private PictureBox pictureBox5;

	private Label labelAddViolation;

	private PictureBox btn_minimize;

	private PictureBox btn_close;

	private Panel panel3;

	private Label label1;

	private ErrorProvider errorProvider1;

	private Panel panel10;

	private Panel panel9;

	private Panel panel8;

	private TextBox txtschoolyear;

	private ComboBox cmbconcernlevel;

	private TextBox txtrecommendation;

	private Label labelRecommendation;

	private Label labelConcernlevel;

	private Label labelSchoolYear;

	private void validateForm()
	{
		errorProvider1.Clear();
		errorcount = 0;
		if (cmbstatus.SelectedIndex < 0)
		{
			errorProvider1.SetError(cmbstatus, "Select a status");
			errorcount++;
		}
		else if (cmbstatus.SelectedIndex == 1 && string.IsNullOrEmpty(txtresolution.Text))
		{
			errorProvider1.SetError(txtresolution, "Resolution is empty");
			errorcount++;
		}
		if (string.IsNullOrEmpty(txtschoolyear.Text))
		{
			errorProvider1.SetError(txtschoolyear, "School year is empty");
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
		if (errorcount != 0 || MessageBox.Show("Are you sure you want to update this violation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		try
		{
			updatecase.executeSQL("UPDATE tblcases SET status = '" + cmbstatus.Text + "', resolution = '" + txtresolution.Text + "', schoolyear = '" + txtschoolyear.Text + "', concernlevel = '" + cmbconcernlevel.Text + "', recommendation = '" + txtrecommendation.Text + "' WHERE caseID = '" + caseid + "'");
			if (updatecase.rowAffected > 0)
			{
				updatecase.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Update', 'Case Management', '" + caseid + "', '" + username + "')");
				MessageBox.Show("Case Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				frmCaseManagement_load.LoadCases();
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
		cmbstatus.SelectedIndex = -1;
		txtresolution.Text = "";
		txtresolution.Enabled = false;
		txtschoolyear.Clear();
		cmbconcernlevel.SelectedIndex = -1;
		txtrecommendation.Clear();
		cmbstatus.Focus();
	}

	private void cmbstatus_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cmbstatus.SelectedIndex == 1)
		{
			txtresolution.Enabled = true;
		}
		else if (cmbstatus.SelectedIndex == 0)
		{
			txtresolution.Text = "";
			txtresolution.Enabled = false;
		}
	}

	public frmUpdateCase(frmCaseManagement frmCaseManagement_Load, string studentnumber, string lastname, string firstname, string middlename, string yearlevel, string course, string username, string violationcode, string violationdescription, string status, string caseid, string resolution, string schoolyear, string concernlevel, string recommendation)
	{
		InitializeComponent();
		this.studentnumber = studentnumber;
		this.lastname = lastname;
		this.firstname = firstname;
		this.middlename = middlename;
		this.yearlevel = yearlevel;
		this.course = course;
		this.username = username;
		this.violationcode = violationcode;
		this.violationdescription = violationdescription;
		this.status = status;
		this.caseid = caseid;
		this.resolution = resolution;
		this.schoolyear = schoolyear;
		this.concernlevel = concernlevel;
		this.recommendation = recommendation;
		this.Draggable(enable: true);
		frmCaseManagement_load = frmCaseManagement_Load;
	}

	private void frmUpdateCase_Load(object sender, EventArgs e)
	{
		txtstudentid.Text = studentnumber;
		txtlastname.Text = lastname;
		txtfirstname.Text = firstname;
		txtmiddlename.Text = middlename;
		txtyearlevel.Text = yearlevel;
		txtcourse.Text = course;
		txtviolationcode.Text = violationcode;
		txtviolationdescription.Text = violationdescription;
		txtresolution.Text = resolution;
		txtschoolyear.Text = schoolyear;
		if (concernlevel == "Prefect of Discipline")
		{
			cmbconcernlevel.SelectedIndex = 0;
		}
		else if (concernlevel == "Branch OSA")
		{
			cmbconcernlevel.SelectedIndex = 1;
		}
		else if (concernlevel == "Dean of OSA")
		{
			cmbconcernlevel.SelectedIndex = 2;
		}
		else if (concernlevel == "Council of Discipline")
		{
			cmbconcernlevel.SelectedIndex = 3;
		}
		txtrecommendation.Text = recommendation;
		if (status == "On-going")
		{
			cmbstatus.SelectedIndex = 0;
		}
		else
		{
			cmbstatus.SelectedIndex = 1;
		}
	}

	private void btn_close_MouseEnter(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.Salmon;
	}

	private void btn_close_MouseLeave(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.FromArgb(150, 0, 52, 112);
	}

	private void btn_close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_minimize_MouseLeave(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.FromArgb(150, 0, 52, 112);
	}

	private void btn_minimize_MouseEnter(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.Silver;
	}

	private void btn_minimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmUpdateCase));
		this.label7 = new System.Windows.Forms.Label();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.labelstatus = new System.Windows.Forms.Label();
		this.cmbstatus = new System.Windows.Forms.ComboBox();
		this.labelresolution = new System.Windows.Forms.Label();
		this.txtresolution = new System.Windows.Forms.TextBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.labelviolationheader = new System.Windows.Forms.Label();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btnclear = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panelbtnlogin = new System.Windows.Forms.Panel();
		this.btnsave = new System.Windows.Forms.Button();
		this.pictureBoxbtnsave = new System.Windows.Forms.PictureBox();
		this.panelinformation = new System.Windows.Forms.Panel();
		this.labelinformation = new System.Windows.Forms.Label();
		this.txtstudentid = new System.Windows.Forms.TextBox();
		this.txtcourse = new System.Windows.Forms.TextBox();
		this.txtyearlevel = new System.Windows.Forms.TextBox();
		this.txtmiddlename = new System.Windows.Forms.TextBox();
		this.txtfirstname = new System.Windows.Forms.TextBox();
		this.txtlastname = new System.Windows.Forms.TextBox();
		this.txtviolationdescription = new System.Windows.Forms.TextBox();
		this.labelviolationdescription = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.txtviolationcode = new System.Windows.Forms.TextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.pictureBox5 = new System.Windows.Forms.PictureBox();
		this.labelAddViolation = new System.Windows.Forms.Label();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.panel10 = new System.Windows.Forms.Panel();
		this.panel9 = new System.Windows.Forms.Panel();
		this.panel8 = new System.Windows.Forms.Panel();
		this.txtschoolyear = new System.Windows.Forms.TextBox();
		this.cmbconcernlevel = new System.Windows.Forms.ComboBox();
		this.txtrecommendation = new System.Windows.Forms.TextBox();
		this.labelRecommendation = new System.Windows.Forms.Label();
		this.labelConcernlevel = new System.Windows.Forms.Label();
		this.labelSchoolYear = new System.Windows.Forms.Label();
		this.panel2.SuspendLayout();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panelbtnlogin.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).BeginInit();
		this.panelinformation.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(21, 273);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(92, 16);
		this.label7.TabIndex = 24;
		this.label7.Text = "Violation Code:";
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
		this.labelstatus.AutoSize = true;
		this.labelstatus.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelstatus.Location = new System.Drawing.Point(51, 597);
		this.labelstatus.Name = "labelstatus";
		this.labelstatus.Size = new System.Drawing.Size(49, 16);
		this.labelstatus.TabIndex = 30;
		this.labelstatus.Text = "Status:";
		this.cmbstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbstatus.FormattingEnabled = true;
		this.cmbstatus.Items.AddRange(new object[2] { "On-going", "Resolved" });
		this.cmbstatus.Location = new System.Drawing.Point(106, 596);
		this.cmbstatus.Name = "cmbstatus";
		this.cmbstatus.Size = new System.Drawing.Size(157, 21);
		this.cmbstatus.TabIndex = 31;
		this.cmbstatus.SelectedIndexChanged += new System.EventHandler(cmbstatus_SelectedIndexChanged);
		this.labelresolution.AutoSize = true;
		this.labelresolution.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelresolution.Location = new System.Drawing.Point(35, 631);
		this.labelresolution.Name = "labelresolution";
		this.labelresolution.Size = new System.Drawing.Size(70, 16);
		this.labelresolution.TabIndex = 32;
		this.labelresolution.Text = "Resolution:";
		this.txtresolution.Enabled = false;
		this.txtresolution.Location = new System.Drawing.Point(106, 630);
		this.txtresolution.Multiline = true;
		this.txtresolution.Name = "txtresolution";
		this.txtresolution.Size = new System.Drawing.Size(433, 119);
		this.txtresolution.TabIndex = 33;
		this.panel2.BackColor = System.Drawing.Color.Firebrick;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.labelviolationheader);
		this.panel2.Location = new System.Drawing.Point(16, 236);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(102, 25);
		this.panel2.TabIndex = 97;
		this.labelviolationheader.AutoSize = true;
		this.labelviolationheader.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelviolationheader.ForeColor = System.Drawing.Color.Azure;
		this.labelviolationheader.Location = new System.Drawing.Point(3, 2);
		this.labelviolationheader.Name = "labelviolationheader";
		this.labelviolationheader.Size = new System.Drawing.Size(94, 19);
		this.labelviolationheader.TabIndex = 0;
		this.labelviolationheader.Text = "VIOLATION";
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.btnclear);
		this.panel5.Controls.Add(this.pictureBox1);
		this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panel5.Location = new System.Drawing.Point(332, 771);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(94, 30);
		this.panel5.TabIndex = 99;
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
		this.panelbtnlogin.Location = new System.Drawing.Point(125, 770);
		this.panelbtnlogin.Name = "panelbtnlogin";
		this.panelbtnlogin.Size = new System.Drawing.Size(175, 29);
		this.panelbtnlogin.TabIndex = 98;
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
		this.btnsave.Size = new System.Drawing.Size(140, 27);
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
		this.panelinformation.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panelinformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelinformation.Controls.Add(this.labelinformation);
		this.panelinformation.Location = new System.Drawing.Point(16, 73);
		this.panelinformation.Name = "panelinformation";
		this.panelinformation.Size = new System.Drawing.Size(197, 25);
		this.panelinformation.TabIndex = 96;
		this.labelinformation.AutoSize = true;
		this.labelinformation.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelinformation.ForeColor = System.Drawing.Color.Azure;
		this.labelinformation.Location = new System.Drawing.Point(3, 2);
		this.labelinformation.Name = "labelinformation";
		this.labelinformation.Size = new System.Drawing.Size(190, 19);
		this.labelinformation.TabIndex = 0;
		this.labelinformation.Text = "STUDENT INFORMATION";
		this.txtstudentid.Enabled = false;
		this.txtstudentid.Location = new System.Drawing.Point(128, 110);
		this.txtstudentid.Name = "txtstudentid";
		this.txtstudentid.ReadOnly = true;
		this.txtstudentid.Size = new System.Drawing.Size(219, 20);
		this.txtstudentid.TabIndex = 90;
		this.txtcourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtcourse.Enabled = false;
		this.txtcourse.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcourse.Location = new System.Drawing.Point(345, 167);
		this.txtcourse.Multiline = true;
		this.txtcourse.Name = "txtcourse";
		this.txtcourse.ReadOnly = true;
		this.txtcourse.Size = new System.Drawing.Size(194, 47);
		this.txtcourse.TabIndex = 95;
		this.txtyearlevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtyearlevel.Enabled = false;
		this.txtyearlevel.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtyearlevel.Location = new System.Drawing.Point(345, 142);
		this.txtyearlevel.Name = "txtyearlevel";
		this.txtyearlevel.ReadOnly = true;
		this.txtyearlevel.Size = new System.Drawing.Size(122, 22);
		this.txtyearlevel.TabIndex = 94;
		this.txtmiddlename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtmiddlename.Enabled = false;
		this.txtmiddlename.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtmiddlename.Location = new System.Drawing.Point(97, 192);
		this.txtmiddlename.Name = "txtmiddlename";
		this.txtmiddlename.ReadOnly = true;
		this.txtmiddlename.Size = new System.Drawing.Size(148, 22);
		this.txtmiddlename.TabIndex = 93;
		this.txtfirstname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtfirstname.Enabled = false;
		this.txtfirstname.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtfirstname.Location = new System.Drawing.Point(97, 167);
		this.txtfirstname.Name = "txtfirstname";
		this.txtfirstname.ReadOnly = true;
		this.txtfirstname.Size = new System.Drawing.Size(148, 22);
		this.txtfirstname.TabIndex = 92;
		this.txtlastname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtlastname.Enabled = false;
		this.txtlastname.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtlastname.Location = new System.Drawing.Point(97, 142);
		this.txtlastname.Name = "txtlastname";
		this.txtlastname.ReadOnly = true;
		this.txtlastname.Size = new System.Drawing.Size(148, 22);
		this.txtlastname.TabIndex = 91;
		this.txtviolationdescription.Enabled = false;
		this.txtviolationdescription.Location = new System.Drawing.Point(23, 318);
		this.txtviolationdescription.Multiline = true;
		this.txtviolationdescription.Name = "txtviolationdescription";
		this.txtviolationdescription.ReadOnly = true;
		this.txtviolationdescription.Size = new System.Drawing.Size(516, 82);
		this.txtviolationdescription.TabIndex = 89;
		this.labelviolationdescription.AutoSize = true;
		this.labelviolationdescription.BackColor = System.Drawing.Color.Transparent;
		this.labelviolationdescription.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelviolationdescription.ForeColor = System.Drawing.SystemColors.ControlText;
		this.labelviolationdescription.Location = new System.Drawing.Point(22, 300);
		this.labelviolationdescription.Name = "labelviolationdescription";
		this.labelviolationdescription.Size = new System.Drawing.Size(128, 16);
		this.labelviolationdescription.TabIndex = 88;
		this.labelviolationdescription.Text = "Violation Description:";
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label11.Location = new System.Drawing.Point(21, 111);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(104, 16);
		this.label11.TabIndex = 85;
		this.label11.Text = "Student number:";
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.Color.Transparent;
		this.label12.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label12.Location = new System.Drawing.Point(260, 169);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(81, 14);
		this.label12.TabIndex = 84;
		this.label12.Text = "Strand/Course:";
		this.label13.AutoSize = true;
		this.label13.BackColor = System.Drawing.Color.Transparent;
		this.label13.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label13.Location = new System.Drawing.Point(260, 147);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(35, 14);
		this.label13.TabIndex = 83;
		this.label13.Text = "Level:";
		this.label14.AutoSize = true;
		this.label14.BackColor = System.Drawing.Color.Transparent;
		this.label14.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label14.Location = new System.Drawing.Point(20, 192);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(70, 14);
		this.label14.TabIndex = 82;
		this.label14.Text = "Middlename:";
		this.label15.AutoSize = true;
		this.label15.BackColor = System.Drawing.Color.Transparent;
		this.label15.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label15.Location = new System.Drawing.Point(21, 169);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(58, 14);
		this.label15.TabIndex = 81;
		this.label15.Text = "Firstname:";
		this.label16.AutoSize = true;
		this.label16.BackColor = System.Drawing.Color.Transparent;
		this.label16.Font = new System.Drawing.Font("Spiegel Bold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label16.Location = new System.Drawing.Point(21, 147);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(57, 14);
		this.label16.TabIndex = 80;
		this.label16.Text = "Lastname:";
		this.txtviolationcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtviolationcode.Enabled = false;
		this.txtviolationcode.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtviolationcode.Location = new System.Drawing.Point(124, 273);
		this.txtviolationcode.Name = "txtviolationcode";
		this.txtviolationcode.ReadOnly = true;
		this.txtviolationcode.Size = new System.Drawing.Size(148, 22);
		this.txtviolationcode.TabIndex = 100;
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
		this.panel1.TabIndex = 101;
		this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox5.Image = (System.Drawing.Image)resources.GetObject("pictureBox5.Image");
		this.pictureBox5.Location = new System.Drawing.Point(0, 0);
		this.pictureBox5.Name = "pictureBox5";
		this.pictureBox5.Padding = new System.Windows.Forms.Padding(10, 7, 7, 7);
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
		this.labelAddViolation.Size = new System.Drawing.Size(115, 20);
		this.labelAddViolation.TabIndex = 28;
		this.labelAddViolation.Text = "UPDATE CASE";
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
		this.panel3.BackColor = System.Drawing.Color.DodgerBlue;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.label1);
		this.panel3.Location = new System.Drawing.Point(25, 562);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(113, 25);
		this.panel3.TabIndex = 98;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Azure;
		this.label1.Location = new System.Drawing.Point(3, 2);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(106, 19);
		this.label1.TabIndex = 0;
		this.label1.Text = "RESOLUTION";
		this.errorProvider1.ContainerControl = this;
		this.panel10.BackColor = System.Drawing.Color.FromArgb(150, 0, 78, 168);
		this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel10.Location = new System.Drawing.Point(10, 815);
		this.panel10.Name = "panel10";
		this.panel10.Size = new System.Drawing.Size(550, 10);
		this.panel10.TabIndex = 104;
		this.panel9.BackColor = System.Drawing.Color.FromArgb(150, 0, 78, 168);
		this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel9.Location = new System.Drawing.Point(560, 63);
		this.panel9.Name = "panel9";
		this.panel9.Size = new System.Drawing.Size(10, 762);
		this.panel9.TabIndex = 103;
		this.panel8.BackColor = System.Drawing.Color.FromArgb(150, 0, 78, 168);
		this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel8.Location = new System.Drawing.Point(0, 63);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(10, 762);
		this.panel8.TabIndex = 102;
		this.txtschoolyear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtschoolyear.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtschoolyear.Location = new System.Drawing.Point(147, 412);
		this.txtschoolyear.Name = "txtschoolyear";
		this.txtschoolyear.Size = new System.Drawing.Size(148, 22);
		this.txtschoolyear.TabIndex = 110;
		this.cmbconcernlevel.FormattingEnabled = true;
		this.cmbconcernlevel.Items.AddRange(new object[4] { "Prefect of Discipline", "Branch OSA", "Dean of OSA", "Council of Discipline" });
		this.cmbconcernlevel.Location = new System.Drawing.Point(147, 440);
		this.cmbconcernlevel.Name = "cmbconcernlevel";
		this.cmbconcernlevel.Size = new System.Drawing.Size(196, 21);
		this.cmbconcernlevel.TabIndex = 109;
		this.txtrecommendation.Location = new System.Drawing.Point(25, 481);
		this.txtrecommendation.Multiline = true;
		this.txtrecommendation.Name = "txtrecommendation";
		this.txtrecommendation.Size = new System.Drawing.Size(514, 71);
		this.txtrecommendation.TabIndex = 108;
		this.labelRecommendation.AutoSize = true;
		this.labelRecommendation.Font = new System.Drawing.Font("Spiegel Regular", 9.75f);
		this.labelRecommendation.Location = new System.Drawing.Point(22, 462);
		this.labelRecommendation.Name = "labelRecommendation";
		this.labelRecommendation.Size = new System.Drawing.Size(113, 16);
		this.labelRecommendation.TabIndex = 107;
		this.labelRecommendation.Text = "Recommendation:";
		this.labelConcernlevel.AutoSize = true;
		this.labelConcernlevel.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelConcernlevel.Location = new System.Drawing.Point(21, 438);
		this.labelConcernlevel.Name = "labelConcernlevel";
		this.labelConcernlevel.Size = new System.Drawing.Size(90, 16);
		this.labelConcernlevel.TabIndex = 106;
		this.labelConcernlevel.Text = "Concern Level:";
		this.labelSchoolYear.AutoSize = true;
		this.labelSchoolYear.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelSchoolYear.Location = new System.Drawing.Point(21, 412);
		this.labelSchoolYear.Name = "labelSchoolYear";
		this.labelSchoolYear.Size = new System.Drawing.Size(78, 16);
		this.labelSchoolYear.TabIndex = 105;
		this.labelSchoolYear.Text = "School Year:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(570, 825);
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
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.txtviolationcode);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panelbtnlogin);
		base.Controls.Add(this.panelinformation);
		base.Controls.Add(this.txtstudentid);
		base.Controls.Add(this.txtcourse);
		base.Controls.Add(this.txtyearlevel);
		base.Controls.Add(this.txtmiddlename);
		base.Controls.Add(this.txtfirstname);
		base.Controls.Add(this.txtlastname);
		base.Controls.Add(this.txtviolationdescription);
		base.Controls.Add(this.labelviolationdescription);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.txtresolution);
		base.Controls.Add(this.labelresolution);
		base.Controls.Add(this.cmbstatus);
		base.Controls.Add(this.labelstatus);
		base.Controls.Add(this.label7);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "frmUpdateCase";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " ";
		base.Load += new System.EventHandler(frmUpdateCase_Load);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panelbtnlogin.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).EndInit();
		this.panelinformation.ResumeLayout(false);
		this.panelinformation.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
