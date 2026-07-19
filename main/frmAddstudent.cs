using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmAddstudent : Form
{
	private string username;

	private int errorcount;

	private frmStudents frmStudents_load;

	private Class1 addstudent = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private Dictionary<string, int> strandsOptions = new Dictionary<string, int>
	{
		{ "ACADEMIC TRACK - General Academic Strand (GAS)", 0 },
		{ "ACADEMIC TRACK - Humanities and Social Sciences (HUMSS)", 1 },
		{ "ACADEMIC TRACK - Accountancy, Business and Management (ABM)", 2 },
		{ "ACADEMIC TRACK - Science, Technology, Engineering and Mathematics (STEM)", 3 },
		{ "ARTS AND DESIGN TRACK - Performing Arts", 4 },
		{ "SPORTS TRACK - Coaching and Sports", 5 },
		{ "SPORTS TRACK - Officiating", 6 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Food and Beverage Services", 7 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Bread and Pastry Production", 8 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Housekeeping", 9 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Cookery", 10 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Caregiving", 11 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Tour Guiding Services", 12 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Bartending", 13 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Tourism Promotion Services", 14 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Computer Programming", 15 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Animation", 16 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Electrical Installation and Maintenance", 17 },
		{ "TECHNICAL VOCATIONAL LIVELIHOOD TRACK - Machining", 18 }
	};

	private Dictionary<string, int> courseOptions = new Dictionary<string, int>
	{
		{ "BACHELOR OF ARTS - Bachelor of Arts in English Language", 0 },
		{ "BACHELOR OF ARTS - Bachelor of Arts in History (BA History)", 1 },
		{ "BACHELOR OF ARTS - Bachelor of Arts in Political Science (BA PoS)", 2 },
		{ "BACHELOR OF ARTS - Bachelor of Arts in Psychology (AB/BA Psychology)", 3 },
		{ "BACHELOR OF ARTS - Bachelor of Performing Arts-Dance (BPeA)", 4 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Criminology", 5 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Medical Technology", 6 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Nursing", 7 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Pharmacy", 8 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Physical Therapy (BSPT)", 9 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Radiologic Technology", 10 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Computer Science", 11 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Information Technology", 12 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Information Systems", 13 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Accountancy", 14 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Business Administration Major in Marketing Management", 15 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Business Administration Major in Financial Management", 16 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Business Administration Major in Operations Management", 17 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Business Administration Major in Human Resource Development Management", 18 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Accounting Information System (BSAIS)", 19 },
		{ "BACHELOR OF SCIENCE - BSBA – Major in Human Resource Management (BSBA – HRM)", 20 },
		{ "BACHELOR OF SCIENCE - BSBA – Major in Marketing Management (BSBA – MM)", 21 },
		{ "BACHELOR OF SCIENCE - BSBA – Major in Financial Management (BSBA – FM)", 22 },
		{ "BACHELOR OF SCIENCE - BSBA – Major in Business Management (BSBA – BM)", 23 },
		{ "BACHELOR OF SCIENCE - BSBA – Major in Management Accounting (BSBA – MA)", 24 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Hospitality Management (BSHM)", 25 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Tourism Management (BSTM)", 26 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Midwifery", 27 },
		{ "BACHELOR OF SCIENCE - Bachelor of Science in Psychology", 28 },
		{ "BACHELOR OF EDUCATION - Bachelor of Elementary Education - General Education", 29 },
		{ "BACHELOR OF EDUCATION - Bachelor of Elementary - Pre-School Education", 30 },
		{ "BACHELOR OF EDUCATION - Bachelor of Elementary Education - SPED", 31 },
		{ "BACHELOR OF EDUCATION - Bachelor of Secondary Education Major in Science", 32 },
		{ "BACHELOR OF EDUCATION - Bachelor of Secondary Education Major in English", 33 },
		{ "BACHELOR OF EDUCATION - Bachelor of Secondary Education Major in Math", 34 },
		{ "BACHELOR OF EDUCATION - Bachelor of Secondary Education Major in Filipino", 35 },
		{ "BACHELOR OF EDUCATION - Bachelor of Secondary Education Major in Social Studies", 36 },
		{ "BACHELOR OF EDUCATION - Bachelor of Secondary Education Major in Values Education", 37 },
		{ "BACHELOR OF EDUCATION - Bachelor of Physical Education – Sports and Wellness Management (BPE-SWM)", 38 },
		{ "BACHELOR OF EDUCATION - Bachelor of Physical Education (BPEd)", 39 },
		{ "BACHELOR OF EDUCATION - Bachelor of Library and Information Science", 40 }
	};

	private IContainer components;

	private Label labelcourse;

	private Label labelyearlevel;

	private ComboBox cmbstudentcourse;

	private ComboBox cmbyearlevel;

	private Label labelfirstname;

	private Label labelmiddlename;

	private Label labellastname;

	private Label labelstudentid;

	private TextBox txtstudentmn;

	private TextBox txtstudentfn;

	private TextBox txtstudentln;

	private TextBox txtstudentid;

	private Label labeladdstudent;

	private ErrorProvider errorProvider1;

	private Panel panelstrandcourse;

	private Label labelflexible;

	private Panel panel1;

	private Panel panel2;

	private Panel panel4;

	private Panel panel5;

	private Button btnclear;

	private PictureBox pictureBox1;

	private Panel panelbtnlogin;

	private Button btnsave;

	private PictureBox pictureBoxbtnsave;

	private PictureBox btn_close;

	private PictureBox btn_minimize;

	private PictureBox pictureBox3;

	private Panel panel8;

	private Panel panel7;

	private Panel panel6;

	private PictureBox pictureBox2;

	private PictureBox pictureBox4;

	private Panel panel3;

	private Label label1;

	public frmAddstudent(frmStudents frmStudents_load, string username)
	{
		InitializeComponent();
		this.username = username;
		this.Draggable(enable: true);
		this.frmStudents_load = frmStudents_load;
	}

	private void validateForm()
	{
		errorProvider1.Clear();
		errorcount = 0;
		if (string.IsNullOrEmpty(txtstudentid.Text))
		{
			errorProvider1.SetError(txtstudentid, "studentID is empty");
			errorcount++;
		}
		if (string.IsNullOrEmpty(txtstudentln.Text))
		{
			errorProvider1.SetError(txtstudentln, "Student last name is empty");
			errorcount++;
		}
		if (string.IsNullOrEmpty(txtstudentfn.Text))
		{
			errorProvider1.SetError(txtstudentfn, "Student first name is empty");
			errorcount++;
		}
		if (string.IsNullOrEmpty(txtstudentmn.Text))
		{
			txtstudentmn.Text = "N/A";
		}
		if (cmbyearlevel.SelectedIndex < 0)
		{
			errorProvider1.SetError(cmbyearlevel, "Select student year level");
			errorcount++;
		}
		if (cmbyearlevel.SelectedIndex == 2 || cmbyearlevel.SelectedIndex == 3)
		{
			if (cmbyearlevel.SelectedIndex == 2)
			{
				if (!strandsOptions.Keys.Contains(cmbstudentcourse.Text))
				{
					errorProvider1.SetError(cmbstudentcourse, " Select a valid student strand");
					errorcount++;
				}
			}
			else if (cmbyearlevel.SelectedIndex == 3 && !courseOptions.Keys.Contains(cmbstudentcourse.Text))
			{
				errorProvider1.SetError(cmbstudentcourse, "Select a valid student course");
				errorcount++;
			}
		}
		else if (cmbyearlevel.SelectedIndex == 0 || cmbyearlevel.SelectedIndex == 1)
		{
			cmbstudentcourse.Text = "N/A";
		}
		try
		{
			if (addstudent.GetData("SELECT * FROM tblstudents WHERE studentID = '" + txtstudentid.Text + "'").Rows.Count > 0)
			{
				errorProvider1.SetError(txtstudentid, "student ID already in use");
				errorcount++;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on validating existing student", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cmbyearlevel_SelectedIndexChanged(object sender, EventArgs e)
	{
		cmbstudentcourse.Items.Clear();
		labelflexible.Text = "N/A";
		cmbstudentcourse.Text = "";
		if (cmbyearlevel.SelectedIndex == 2)
		{
			foreach (string course in strandsOptions.Keys)
			{
				cmbstudentcourse.Items.Add(course);
			}
			labelflexible.Text = "STRANDS";
			labelcourse.Text = "Select:";
			cmbstudentcourse.Enabled = true;
		}
		else if (cmbyearlevel.SelectedIndex == 3)
		{
			foreach (string course2 in courseOptions.Keys)
			{
				cmbstudentcourse.Items.Add(course2);
			}
			labelflexible.Text = "COURSES";
			labelcourse.Text = "Select:";
			cmbstudentcourse.Enabled = true;
		}
		else
		{
			cmbstudentcourse.Text = "N/A";
			cmbstudentcourse.Enabled = false;
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
			addstudent.executeSQL("INSERT INTO tblstudents (studentID, studentLN, studentFN, studentMN, yearLevel, studentCourse, dateCreated, createdBy) VALUES ('" + txtstudentid.Text + "', '" + txtstudentln.Text + "', '" + txtstudentfn.Text + "', '" + txtstudentmn.Text + "', '" + cmbyearlevel.Text.ToUpper() + "', '" + cmbstudentcourse.Text + "', '" + DateTime.Now.ToShortDateString() + "', '" + username + "')");
			if (addstudent.rowAffected > 0)
			{
				addstudent.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Add', 'Students Management', '" + txtstudentid.Text + "', '" + username + "')");
				MessageBox.Show("New account added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				frmStudents_load.LoadStudents();
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
		txtstudentid.Clear();
		txtstudentln.Clear();
		txtstudentfn.Clear();
		txtstudentmn.Clear();
		txtstudentmn.Clear();
		cmbyearlevel.SelectedIndex = -1;
		cmbstudentcourse.Text = "";
		cmbstudentcourse.SelectedIndex = -1;
		txtstudentid.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmAddstudent));
		this.labelcourse = new System.Windows.Forms.Label();
		this.labelyearlevel = new System.Windows.Forms.Label();
		this.cmbstudentcourse = new System.Windows.Forms.ComboBox();
		this.cmbyearlevel = new System.Windows.Forms.ComboBox();
		this.labelfirstname = new System.Windows.Forms.Label();
		this.labelmiddlename = new System.Windows.Forms.Label();
		this.labellastname = new System.Windows.Forms.Label();
		this.labelstudentid = new System.Windows.Forms.Label();
		this.txtstudentmn = new System.Windows.Forms.TextBox();
		this.txtstudentfn = new System.Windows.Forms.TextBox();
		this.txtstudentln = new System.Windows.Forms.TextBox();
		this.txtstudentid = new System.Windows.Forms.TextBox();
		this.labeladdstudent = new System.Windows.Forms.Label();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.panelstrandcourse = new System.Windows.Forms.Panel();
		this.labelflexible = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.pictureBox3 = new System.Windows.Forms.PictureBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.pictureBox4 = new System.Windows.Forms.PictureBox();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btnclear = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panelbtnlogin = new System.Windows.Forms.Panel();
		this.btnsave = new System.Windows.Forms.Button();
		this.pictureBoxbtnsave = new System.Windows.Forms.PictureBox();
		this.panel8 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		this.panel3 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.panelstrandcourse.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).BeginInit();
		this.panel4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).BeginInit();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.panelbtnlogin.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).BeginInit();
		base.SuspendLayout();
		this.labelcourse.AutoSize = true;
		this.labelcourse.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelcourse.ForeColor = System.Drawing.SystemColors.Control;
		this.labelcourse.Location = new System.Drawing.Point(15, 49);
		this.labelcourse.Name = "labelcourse";
		this.labelcourse.Size = new System.Drawing.Size(51, 16);
		this.labelcourse.TabIndex = 47;
		this.labelcourse.Text = "Select :";
		this.labelyearlevel.AutoSize = true;
		this.labelyearlevel.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelyearlevel.ForeColor = System.Drawing.SystemColors.Control;
		this.labelyearlevel.Location = new System.Drawing.Point(48, 11);
		this.labelyearlevel.Name = "labelyearlevel";
		this.labelyearlevel.Size = new System.Drawing.Size(67, 16);
		this.labelyearlevel.TabIndex = 48;
		this.labelyearlevel.Text = "Year Level:";
		this.cmbstudentcourse.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbstudentcourse.FormattingEnabled = true;
		this.cmbstudentcourse.Location = new System.Drawing.Point(112, 292);
		this.cmbstudentcourse.Name = "cmbstudentcourse";
		this.cmbstudentcourse.Size = new System.Drawing.Size(573, 24);
		this.cmbstudentcourse.TabIndex = 46;
		this.cmbyearlevel.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbyearlevel.FormattingEnabled = true;
		this.cmbyearlevel.Items.AddRange(new object[4] { "ELEMENTARY", "JUNIOR HS", "SENIOR HS", "COLLEGE" });
		this.cmbyearlevel.Location = new System.Drawing.Point(122, 8);
		this.cmbyearlevel.Name = "cmbyearlevel";
		this.cmbyearlevel.Size = new System.Drawing.Size(144, 23);
		this.cmbyearlevel.TabIndex = 45;
		this.cmbyearlevel.SelectedIndexChanged += new System.EventHandler(cmbyearlevel_SelectedIndexChanged);
		this.labelfirstname.AutoSize = true;
		this.labelfirstname.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelfirstname.ForeColor = System.Drawing.SystemColors.Control;
		this.labelfirstname.Location = new System.Drawing.Point(238, 51);
		this.labelfirstname.Name = "labelfirstname";
		this.labelfirstname.Size = new System.Drawing.Size(69, 16);
		this.labelfirstname.TabIndex = 41;
		this.labelfirstname.Text = "First name:";
		this.labelmiddlename.AutoSize = true;
		this.labelmiddlename.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelmiddlename.ForeColor = System.Drawing.SystemColors.Control;
		this.labelmiddlename.Location = new System.Drawing.Point(471, 48);
		this.labelmiddlename.Name = "labelmiddlename";
		this.labelmiddlename.Size = new System.Drawing.Size(84, 16);
		this.labelmiddlename.TabIndex = 42;
		this.labelmiddlename.Text = "Middle name:";
		this.labellastname.AutoSize = true;
		this.labellastname.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labellastname.ForeColor = System.Drawing.SystemColors.Control;
		this.labellastname.Location = new System.Drawing.Point(11, 48);
		this.labellastname.Name = "labellastname";
		this.labellastname.Size = new System.Drawing.Size(69, 16);
		this.labellastname.TabIndex = 43;
		this.labellastname.Text = "Last name:";
		this.labelstudentid.AutoSize = true;
		this.labelstudentid.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelstudentid.ForeColor = System.Drawing.SystemColors.Control;
		this.labelstudentid.Location = new System.Drawing.Point(49, 12);
		this.labelstudentid.Name = "labelstudentid";
		this.labelstudentid.Size = new System.Drawing.Size(77, 16);
		this.labelstudentid.TabIndex = 44;
		this.labelstudentid.Text = "STUDENT ID:";
		this.txtstudentmn.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtstudentmn.Location = new System.Drawing.Point(561, 48);
		this.txtstudentmn.Name = "txtstudentmn";
		this.txtstudentmn.Size = new System.Drawing.Size(96, 22);
		this.txtstudentmn.TabIndex = 37;
		this.txtstudentfn.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtstudentfn.Location = new System.Drawing.Point(313, 47);
		this.txtstudentfn.Name = "txtstudentfn";
		this.txtstudentfn.Size = new System.Drawing.Size(136, 22);
		this.txtstudentfn.TabIndex = 38;
		this.txtstudentln.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtstudentln.Location = new System.Drawing.Point(112, 156);
		this.txtstudentln.Name = "txtstudentln";
		this.txtstudentln.Size = new System.Drawing.Size(124, 22);
		this.txtstudentln.TabIndex = 39;
		this.txtstudentid.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtstudentid.Location = new System.Drawing.Point(128, 11);
		this.txtstudentid.Name = "txtstudentid";
		this.txtstudentid.Size = new System.Drawing.Size(187, 22);
		this.txtstudentid.TabIndex = 40;
		this.labeladdstudent.AutoSize = true;
		this.labeladdstudent.BackColor = System.Drawing.Color.Transparent;
		this.labeladdstudent.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labeladdstudent.ForeColor = System.Drawing.SystemColors.Control;
		this.labeladdstudent.Location = new System.Drawing.Point(65, 19);
		this.labeladdstudent.Name = "labeladdstudent";
		this.labeladdstudent.Size = new System.Drawing.Size(195, 24);
		this.labeladdstudent.TabIndex = 49;
		this.labeladdstudent.Text = "ADD NEW STUDENT";
		this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
		this.errorProvider1.ContainerControl = this;
		this.panelstrandcourse.BackColor = System.Drawing.Color.Firebrick;
		this.panelstrandcourse.Controls.Add(this.labelflexible);
		this.panelstrandcourse.Controls.Add(this.labelcourse);
		this.panelstrandcourse.Location = new System.Drawing.Point(28, 250);
		this.panelstrandcourse.Name = "panelstrandcourse";
		this.panelstrandcourse.Size = new System.Drawing.Size(678, 78);
		this.panelstrandcourse.TabIndex = 51;
		this.labelflexible.AutoSize = true;
		this.labelflexible.Font = new System.Drawing.Font("Beaufort for LOL", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelflexible.ForeColor = System.Drawing.SystemColors.Control;
		this.labelflexible.Location = new System.Drawing.Point(14, 11);
		this.labelflexible.Name = "labelflexible";
		this.labelflexible.Size = new System.Drawing.Size(41, 20);
		this.labelflexible.TabIndex = 48;
		this.labelflexible.Text = "N/A";
		this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel1.Controls.Add(this.pictureBox2);
		this.panel1.Controls.Add(this.btn_close);
		this.panel1.Controls.Add(this.btn_minimize);
		this.panel1.Controls.Add(this.labeladdstudent);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(735, 61);
		this.panel1.TabIndex = 52;
		this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(0, 0);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Padding = new System.Windows.Forms.Padding(12);
		this.pictureBox2.Size = new System.Drawing.Size(63, 61);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 95;
		this.pictureBox2.TabStop = false;
		this.btn_close.BackColor = System.Drawing.Color.Transparent;
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(696, 12);
		this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_close.Name = "btn_close";
		this.btn_close.Padding = new System.Windows.Forms.Padding(3);
		this.btn_close.Size = new System.Drawing.Size(28, 25);
		this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_close.TabIndex = 76;
		this.btn_close.TabStop = false;
		this.btn_close.Click += new System.EventHandler(btn_close_Click);
		this.btn_close.MouseLeave += new System.EventHandler(btn_close_MouseLeave);
		this.btn_close.MouseHover += new System.EventHandler(btn_close_MouseHover);
		this.btn_minimize.BackColor = System.Drawing.Color.Transparent;
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(664, 12);
		this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_minimize.Name = "btn_minimize";
		this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
		this.btn_minimize.Size = new System.Drawing.Size(28, 25);
		this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_minimize.TabIndex = 77;
		this.btn_minimize.TabStop = false;
		this.btn_minimize.Click += new System.EventHandler(btn_minimize_Click);
		this.btn_minimize.MouseLeave += new System.EventHandler(btn_minimize_MouseLeave);
		this.btn_minimize.MouseHover += new System.EventHandler(btn_minimize_MouseHover);
		this.panel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.panel2.Controls.Add(this.pictureBox3);
		this.panel2.Controls.Add(this.labelstudentid);
		this.panel2.Controls.Add(this.txtstudentid);
		this.panel2.Controls.Add(this.labellastname);
		this.panel2.Controls.Add(this.labelfirstname);
		this.panel2.Controls.Add(this.txtstudentmn);
		this.panel2.Controls.Add(this.txtstudentfn);
		this.panel2.Controls.Add(this.labelmiddlename);
		this.panel2.Location = new System.Drawing.Point(28, 109);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(678, 78);
		this.panel2.TabIndex = 54;
		this.pictureBox3.BackColor = System.Drawing.SystemColors.HotTrack;
		this.pictureBox3.Image = (System.Drawing.Image)resources.GetObject("pictureBox3.Image");
		this.pictureBox3.Location = new System.Drawing.Point(0, 0);
		this.pictureBox3.Name = "pictureBox3";
		this.pictureBox3.Padding = new System.Windows.Forms.Padding(4);
		this.pictureBox3.Size = new System.Drawing.Size(47, 37);
		this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox3.TabIndex = 45;
		this.pictureBox3.TabStop = false;
		this.panel4.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.panel4.Controls.Add(this.pictureBox4);
		this.panel4.Controls.Add(this.cmbyearlevel);
		this.panel4.Controls.Add(this.labelyearlevel);
		this.panel4.Location = new System.Drawing.Point(28, 207);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(297, 37);
		this.panel4.TabIndex = 56;
		this.pictureBox4.BackColor = System.Drawing.SystemColors.HotTrack;
		this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox4.Image = (System.Drawing.Image)resources.GetObject("pictureBox4.Image");
		this.pictureBox4.Location = new System.Drawing.Point(0, 0);
		this.pictureBox4.Name = "pictureBox4";
		this.pictureBox4.Padding = new System.Windows.Forms.Padding(4);
		this.pictureBox4.Size = new System.Drawing.Size(47, 37);
		this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox4.TabIndex = 95;
		this.pictureBox4.TabStop = false;
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.btnclear);
		this.panel5.Controls.Add(this.pictureBox1);
		this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panel5.Location = new System.Drawing.Point(393, 357);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(94, 30);
		this.panel5.TabIndex = 75;
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
		this.panelbtnlogin.Location = new System.Drawing.Point(181, 358);
		this.panelbtnlogin.Name = "panelbtnlogin";
		this.panelbtnlogin.Size = new System.Drawing.Size(179, 29);
		this.panelbtnlogin.TabIndex = 74;
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
		this.btnsave.Size = new System.Drawing.Size(144, 27);
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
		this.panel8.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(10, 405);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(715, 10);
		this.panel8.TabIndex = 94;
		this.panel7.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel7.Location = new System.Drawing.Point(725, 61);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(10, 354);
		this.panel7.TabIndex = 93;
		this.panel6.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel6.Location = new System.Drawing.Point(0, 61);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(10, 354);
		this.panel6.TabIndex = 92;
		this.panel3.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel3.Location = new System.Drawing.Point(28, 80);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(13, 23);
		this.panel3.TabIndex = 72;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.SystemColors.HotTrack;
		this.label1.Font = new System.Drawing.Font("Spiegel Bold", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.SystemColors.Control;
		this.label1.Location = new System.Drawing.Point(47, 80);
		this.label1.Name = "label1";
		this.label1.Padding = new System.Windows.Forms.Padding(2);
		this.label1.Size = new System.Drawing.Size(95, 24);
		this.label1.TabIndex = 71;
		this.label1.Text = "Information";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(735, 415);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.panel8);
		base.Controls.Add(this.panel7);
		base.Controls.Add(this.cmbstudentcourse);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panelbtnlogin);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.panelstrandcourse);
		base.Controls.Add(this.txtstudentln);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "frmAddstudent";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " ";
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.panelstrandcourse.ResumeLayout(false);
		this.panelstrandcourse.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox3).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).EndInit();
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.panelbtnlogin.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
