using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmUpdateStudent : Form
{
	private frmStudents frmStudents_load;

	private string editstudentid;

	private string editstudentln;

	private string editstudentfn;

	private string editstudentmn;

	private string edityearlevel;

	private string editstudentcourse;

	private string username;

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

	private int errorcount;

	private Class1 updatestudent = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private IContainer components;

	private ErrorProvider errorProvider1;

	private Panel panelstrandcourse;

	private Label labelflexible;

	private Label labelcourse;

	private ComboBox cmbstudentcourse;

	private Label labelyearlevel;

	private ComboBox cmbyearlevel;

	private Label labelfirstname;

	private Label labelmiddlename;

	private Label labellastname;

	private Label labelstudentid;

	private TextBox txtstudentmn;

	private TextBox txtstudentfn;

	private TextBox txtstudentln;

	private TextBox txtstudentid;

	private Panel panel1;

	private Label labelupdatestudent;

	private Panel panel2;

	private Panel panel3;

	private Panel panel4;

	private Label label1;

	private Panel panelbtnlogin;

	private Button btnsave;

	private PictureBox pictureBoxbtnsave;

	private Panel panel5;

	private Button btnclear;

	private PictureBox pictureBox1;

	private PictureBox btn_close;

	private PictureBox btn_minimize;

	private Panel panel8;

	private Panel panel7;

	private Panel panel6;

	private PictureBox pictureBox2;

	private PictureBox pictureBox4;

	private PictureBox pictureBox5;

	private void frmUpdateStudent_Load(object sender, EventArgs e)
	{
		txtstudentid.Text = editstudentid;
		txtstudentln.Text = editstudentln;
		txtstudentfn.Text = editstudentfn;
		txtstudentmn.Text = editstudentmn;
		cmbstudentcourse.Text = editstudentcourse;
		if (edityearlevel == "ELEMENTARY")
		{
			cmbyearlevel.SelectedIndex = 0;
		}
		else if (edityearlevel == "JUNIOR HS")
		{
			cmbyearlevel.SelectedIndex = 1;
		}
		else if (edityearlevel == "SENIOR HS")
		{
			cmbyearlevel.SelectedIndex = 2;
			if (strandsOptions.TryGetValue(editstudentcourse, out var selectedcourse))
			{
				cmbstudentcourse.SelectedIndex = selectedcourse;
			}
			else
			{
				cmbstudentcourse.SelectedIndex = -1;
			}
		}
		else
		{
			cmbyearlevel.SelectedIndex = 3;
			if (courseOptions.TryGetValue(editstudentcourse, out var selectedcourse2))
			{
				cmbstudentcourse.SelectedIndex = selectedcourse2;
			}
			else
			{
				cmbstudentcourse.SelectedIndex = -1;
			}
		}
	}

	private void cmbyearlevel_SelectedIndexChanged_1(object sender, EventArgs e)
	{
		cmbstudentcourse.Items.Clear();
		labelflexible.Text = "N/A";
		if (cmbyearlevel.SelectedIndex == 2 || cmbyearlevel.SelectedIndex == 3)
		{
			cmbstudentcourse.Text = editstudentcourse;
			if (cmbyearlevel.SelectedIndex == 2)
			{
				foreach (string course in strandsOptions.Keys)
				{
					cmbstudentcourse.Items.Add(course);
				}
				if (!strandsOptions.Keys.Contains(cmbstudentcourse.Text))
				{
					cmbstudentcourse.Text = "";
				}
				labelflexible.Text = "STRANDS";
				labelcourse.Text = "Select:";
				cmbstudentcourse.Enabled = true;
			}
			else
			{
				if (cmbyearlevel.SelectedIndex != 3)
				{
					return;
				}
				foreach (string course2 in courseOptions.Keys)
				{
					cmbstudentcourse.Items.Add(course2);
				}
				if (!courseOptions.Keys.Contains(cmbstudentcourse.Text))
				{
					cmbstudentcourse.Text = "";
				}
				labelflexible.Text = "COURSES";
				labelcourse.Text = "Select:";
				cmbstudentcourse.Enabled = true;
			}
		}
		else
		{
			cmbstudentcourse.Text = "N/A";
			cmbstudentcourse.Enabled = false;
		}
	}

	private void btnclear_Click_1(object sender, EventArgs e)
	{
		errorProvider1.Clear();
		txtstudentln.Clear();
		txtstudentfn.Clear();
		txtstudentmn.Clear();
		cmbyearlevel.SelectedIndex = -1;
		cmbstudentcourse.SelectedIndex = -1;
		txtstudentln.Focus();
	}

	private void btnsave_Click(object sender, EventArgs e)
	{
		validateForm();
		if (errorcount != 0 || MessageBox.Show("Are you sure you want to update this account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		try
		{
			updatestudent.executeSQL("UPDATE tblstudents SET studentLN = '" + txtstudentln.Text + "', studentFN = '" + txtstudentfn.Text + "', studentMN = '" + txtstudentmn.Text + "', yearLevel = '" + cmbyearlevel.Text.ToUpper() + "', studentCourse = '" + cmbstudentcourse.Text + "' WHERE studentID = '" + txtstudentid.Text + "'");
			if (updatestudent.rowAffected > 0)
			{
				updatestudent.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Update', 'Students Management', '" + txtstudentid.Text + "', '" + username + "')");
				MessageBox.Show("Student Updated", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				frmStudents_load.LoadStudents();
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on save", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public frmUpdateStudent(frmStudents frmStudents_load, string editstudentid, string editstudentln, string editstudentfn, string editstudentmn, string edityearlevel, string editstudentcourse, string username)
	{
		InitializeComponent();
		this.username = username;
		this.editstudentid = editstudentid;
		this.editstudentln = editstudentln;
		this.editstudentfn = editstudentfn;
		this.editstudentmn = editstudentmn;
		this.edityearlevel = edityearlevel;
		this.editstudentcourse = editstudentcourse;
		this.Draggable(enable: true);
		this.frmStudents_load = frmStudents_load;
	}

	private void validateForm()
	{
		errorProvider1.Clear();
		errorcount = 0;
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
		if (cmbyearlevel.SelectedIndex != 2 && cmbyearlevel.SelectedIndex != 3)
		{
			return;
		}
		if (cmbyearlevel.SelectedIndex == 2)
		{
			if (!strandsOptions.Keys.Contains(cmbstudentcourse.Text))
			{
				errorProvider1.SetError(cmbstudentcourse, "Select a valid student strand");
				errorcount++;
			}
		}
		else if (cmbyearlevel.SelectedIndex == 3 && !courseOptions.Keys.Contains(cmbstudentcourse.Text))
		{
			errorProvider1.SetError(cmbstudentcourse, "Select a valid student course");
			errorcount++;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmUpdateStudent));
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.panelstrandcourse = new System.Windows.Forms.Panel();
		this.labelflexible = new System.Windows.Forms.Label();
		this.labelcourse = new System.Windows.Forms.Label();
		this.cmbstudentcourse = new System.Windows.Forms.ComboBox();
		this.labelyearlevel = new System.Windows.Forms.Label();
		this.cmbyearlevel = new System.Windows.Forms.ComboBox();
		this.labelfirstname = new System.Windows.Forms.Label();
		this.labelmiddlename = new System.Windows.Forms.Label();
		this.labellastname = new System.Windows.Forms.Label();
		this.labelstudentid = new System.Windows.Forms.Label();
		this.txtstudentmn = new System.Windows.Forms.TextBox();
		this.txtstudentfn = new System.Windows.Forms.TextBox();
		this.txtstudentln = new System.Windows.Forms.TextBox();
		this.txtstudentid = new System.Windows.Forms.TextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.labelupdatestudent = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.pictureBox5 = new System.Windows.Forms.PictureBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.pictureBox4 = new System.Windows.Forms.PictureBox();
		this.panel4 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.panelbtnlogin = new System.Windows.Forms.Panel();
		this.btnsave = new System.Windows.Forms.Button();
		this.pictureBoxbtnsave = new System.Windows.Forms.PictureBox();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btnclear = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.panel8 = new System.Windows.Forms.Panel();
		this.panel7 = new System.Windows.Forms.Panel();
		this.panel6 = new System.Windows.Forms.Panel();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.panelstrandcourse.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).BeginInit();
		this.panelbtnlogin.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).BeginInit();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
		this.errorProvider1.ContainerControl = this;
		this.panelstrandcourse.BackColor = System.Drawing.Color.Firebrick;
		this.panelstrandcourse.Controls.Add(this.labelflexible);
		this.panelstrandcourse.Controls.Add(this.labelcourse);
		this.panelstrandcourse.Location = new System.Drawing.Point(22, 251);
		this.panelstrandcourse.Name = "panelstrandcourse";
		this.panelstrandcourse.Size = new System.Drawing.Size(674, 83);
		this.panelstrandcourse.TabIndex = 65;
		this.labelflexible.AutoSize = true;
		this.labelflexible.Font = new System.Drawing.Font("Beaufort for LOL", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelflexible.ForeColor = System.Drawing.SystemColors.Control;
		this.labelflexible.Location = new System.Drawing.Point(14, 16);
		this.labelflexible.Name = "labelflexible";
		this.labelflexible.Size = new System.Drawing.Size(41, 20);
		this.labelflexible.TabIndex = 48;
		this.labelflexible.Text = "N/A";
		this.labelcourse.AutoSize = true;
		this.labelcourse.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold);
		this.labelcourse.ForeColor = System.Drawing.SystemColors.Control;
		this.labelcourse.Location = new System.Drawing.Point(15, 53);
		this.labelcourse.Name = "labelcourse";
		this.labelcourse.Size = new System.Drawing.Size(48, 16);
		this.labelcourse.TabIndex = 47;
		this.labelcourse.Text = "Select:";
		this.cmbstudentcourse.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold);
		this.cmbstudentcourse.FormattingEnabled = true;
		this.cmbstudentcourse.Location = new System.Drawing.Point(98, 299);
		this.cmbstudentcourse.Name = "cmbstudentcourse";
		this.cmbstudentcourse.Size = new System.Drawing.Size(577, 24);
		this.cmbstudentcourse.TabIndex = 46;
		this.labelyearlevel.AutoSize = true;
		this.labelyearlevel.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelyearlevel.ForeColor = System.Drawing.SystemColors.Control;
		this.labelyearlevel.Location = new System.Drawing.Point(50, 12);
		this.labelyearlevel.Name = "labelyearlevel";
		this.labelyearlevel.Size = new System.Drawing.Size(76, 16);
		this.labelyearlevel.TabIndex = 61;
		this.labelyearlevel.Text = "YEAR LEVEL:";
		this.cmbyearlevel.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbyearlevel.FormattingEnabled = true;
		this.cmbyearlevel.Items.AddRange(new object[4] { "ELEMENTARY", "JUNIOR HS", "SENIOR HS", "COLLEGE" });
		this.cmbyearlevel.Location = new System.Drawing.Point(129, 9);
		this.cmbyearlevel.Name = "cmbyearlevel";
		this.cmbyearlevel.Size = new System.Drawing.Size(144, 23);
		this.cmbyearlevel.TabIndex = 60;
		this.cmbyearlevel.SelectedIndexChanged += new System.EventHandler(cmbyearlevel_SelectedIndexChanged_1);
		this.labelfirstname.AutoSize = true;
		this.labelfirstname.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelfirstname.ForeColor = System.Drawing.SystemColors.Control;
		this.labelfirstname.Location = new System.Drawing.Point(232, 45);
		this.labelfirstname.Name = "labelfirstname";
		this.labelfirstname.Size = new System.Drawing.Size(69, 16);
		this.labelfirstname.TabIndex = 56;
		this.labelfirstname.Text = "First name:";
		this.labelmiddlename.AutoSize = true;
		this.labelmiddlename.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelmiddlename.ForeColor = System.Drawing.SystemColors.Control;
		this.labelmiddlename.Location = new System.Drawing.Point(467, 45);
		this.labelmiddlename.Name = "labelmiddlename";
		this.labelmiddlename.Size = new System.Drawing.Size(84, 16);
		this.labelmiddlename.TabIndex = 57;
		this.labelmiddlename.Text = "Middle name:";
		this.labellastname.AutoSize = true;
		this.labellastname.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labellastname.ForeColor = System.Drawing.SystemColors.Control;
		this.labellastname.Location = new System.Drawing.Point(9, 45);
		this.labellastname.Name = "labellastname";
		this.labellastname.Size = new System.Drawing.Size(69, 16);
		this.labellastname.TabIndex = 58;
		this.labellastname.Text = "Last name:";
		this.labelstudentid.AutoSize = true;
		this.labelstudentid.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelstudentid.ForeColor = System.Drawing.SystemColors.Control;
		this.labelstudentid.Location = new System.Drawing.Point(50, 14);
		this.labelstudentid.Name = "labelstudentid";
		this.labelstudentid.Size = new System.Drawing.Size(77, 16);
		this.labelstudentid.TabIndex = 59;
		this.labelstudentid.Text = "STUDENT ID:";
		this.txtstudentmn.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtstudentmn.Location = new System.Drawing.Point(557, 44);
		this.txtstudentmn.Name = "txtstudentmn";
		this.txtstudentmn.Size = new System.Drawing.Size(96, 22);
		this.txtstudentmn.TabIndex = 52;
		this.txtstudentfn.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtstudentfn.Location = new System.Drawing.Point(307, 44);
		this.txtstudentfn.Name = "txtstudentfn";
		this.txtstudentfn.Size = new System.Drawing.Size(136, 22);
		this.txtstudentfn.TabIndex = 53;
		this.txtstudentln.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtstudentln.Location = new System.Drawing.Point(86, 44);
		this.txtstudentln.Name = "txtstudentln";
		this.txtstudentln.Size = new System.Drawing.Size(124, 22);
		this.txtstudentln.TabIndex = 54;
		this.txtstudentid.Enabled = false;
		this.txtstudentid.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtstudentid.Location = new System.Drawing.Point(132, 11);
		this.txtstudentid.Name = "txtstudentid";
		this.txtstudentid.Size = new System.Drawing.Size(187, 22);
		this.txtstudentid.TabIndex = 55;
		this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel1.Controls.Add(this.pictureBox2);
		this.panel1.Controls.Add(this.btn_close);
		this.panel1.Controls.Add(this.btn_minimize);
		this.panel1.Controls.Add(this.labelupdatestudent);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(718, 62);
		this.panel1.TabIndex = 66;
		this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(0, 0);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Padding = new System.Windows.Forms.Padding(12);
		this.pictureBox2.Size = new System.Drawing.Size(66, 62);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 75;
		this.pictureBox2.TabStop = false;
		this.btn_close.BackColor = System.Drawing.Color.Transparent;
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(679, 12);
		this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_close.Name = "btn_close";
		this.btn_close.Padding = new System.Windows.Forms.Padding(3);
		this.btn_close.Size = new System.Drawing.Size(28, 25);
		this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_close.TabIndex = 78;
		this.btn_close.TabStop = false;
		this.btn_close.Click += new System.EventHandler(btn_close_Click);
		this.btn_minimize.BackColor = System.Drawing.Color.Transparent;
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(647, 12);
		this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_minimize.Name = "btn_minimize";
		this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
		this.btn_minimize.Size = new System.Drawing.Size(28, 25);
		this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_minimize.TabIndex = 79;
		this.btn_minimize.TabStop = false;
		this.btn_minimize.Click += new System.EventHandler(btn_minimize_Click);
		this.labelupdatestudent.AutoSize = true;
		this.labelupdatestudent.BackColor = System.Drawing.Color.Transparent;
		this.labelupdatestudent.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelupdatestudent.ForeColor = System.Drawing.SystemColors.Control;
		this.labelupdatestudent.Location = new System.Drawing.Point(69, 20);
		this.labelupdatestudent.Name = "labelupdatestudent";
		this.labelupdatestudent.Size = new System.Drawing.Size(316, 24);
		this.labelupdatestudent.TabIndex = 49;
		this.labelupdatestudent.Text = "UPDATE STUDENT INFORMATION";
		this.panel2.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.panel2.Controls.Add(this.pictureBox5);
		this.panel2.Controls.Add(this.labelstudentid);
		this.panel2.Controls.Add(this.txtstudentid);
		this.panel2.Controls.Add(this.labellastname);
		this.panel2.Controls.Add(this.txtstudentln);
		this.panel2.Controls.Add(this.labelfirstname);
		this.panel2.Controls.Add(this.labelmiddlename);
		this.panel2.Controls.Add(this.txtstudentmn);
		this.panel2.Controls.Add(this.txtstudentfn);
		this.panel2.Location = new System.Drawing.Point(22, 111);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(674, 74);
		this.panel2.TabIndex = 67;
		this.pictureBox5.BackColor = System.Drawing.SystemColors.HotTrack;
		this.pictureBox5.Image = (System.Drawing.Image)resources.GetObject("pictureBox5.Image");
		this.pictureBox5.Location = new System.Drawing.Point(0, 1);
		this.pictureBox5.Name = "pictureBox5";
		this.pictureBox5.Padding = new System.Windows.Forms.Padding(4);
		this.pictureBox5.Size = new System.Drawing.Size(49, 37);
		this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox5.TabIndex = 76;
		this.pictureBox5.TabStop = false;
		this.panel3.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.panel3.Controls.Add(this.pictureBox4);
		this.panel3.Controls.Add(this.labelyearlevel);
		this.panel3.Controls.Add(this.cmbyearlevel);
		this.panel3.Location = new System.Drawing.Point(22, 205);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(301, 40);
		this.panel3.TabIndex = 68;
		this.pictureBox4.BackColor = System.Drawing.SystemColors.HotTrack;
		this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox4.Image = (System.Drawing.Image)resources.GetObject("pictureBox4.Image");
		this.pictureBox4.Location = new System.Drawing.Point(0, 0);
		this.pictureBox4.Name = "pictureBox4";
		this.pictureBox4.Padding = new System.Windows.Forms.Padding(4);
		this.pictureBox4.Size = new System.Drawing.Size(49, 40);
		this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox4.TabIndex = 75;
		this.pictureBox4.TabStop = false;
		this.panel4.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel4.Location = new System.Drawing.Point(22, 80);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(13, 23);
		this.panel4.TabIndex = 70;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.SystemColors.HotTrack;
		this.label1.Font = new System.Drawing.Font("Spiegel Bold", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.SystemColors.Control;
		this.label1.Location = new System.Drawing.Point(41, 80);
		this.label1.Name = "label1";
		this.label1.Padding = new System.Windows.Forms.Padding(2);
		this.label1.Size = new System.Drawing.Size(95, 24);
		this.label1.TabIndex = 69;
		this.label1.Text = "Information";
		this.panelbtnlogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelbtnlogin.Controls.Add(this.btnsave);
		this.panelbtnlogin.Controls.Add(this.pictureBoxbtnsave);
		this.panelbtnlogin.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panelbtnlogin.Location = new System.Drawing.Point(178, 355);
		this.panelbtnlogin.Name = "panelbtnlogin";
		this.panelbtnlogin.Size = new System.Drawing.Size(173, 29);
		this.panelbtnlogin.TabIndex = 72;
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
		this.btnsave.Size = new System.Drawing.Size(138, 27);
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
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.btnclear);
		this.panel5.Controls.Add(this.pictureBox1);
		this.panel5.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panel5.Location = new System.Drawing.Point(400, 356);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(94, 30);
		this.panel5.TabIndex = 73;
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
		this.panel8.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel8.Location = new System.Drawing.Point(10, 402);
		this.panel8.Name = "panel8";
		this.panel8.Size = new System.Drawing.Size(698, 10);
		this.panel8.TabIndex = 91;
		this.panel7.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel7.Location = new System.Drawing.Point(708, 62);
		this.panel7.Name = "panel7";
		this.panel7.Size = new System.Drawing.Size(10, 350);
		this.panel7.TabIndex = 90;
		this.panel6.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
		this.panel6.Location = new System.Drawing.Point(0, 62);
		this.panel6.Name = "panel6";
		this.panel6.Size = new System.Drawing.Size(10, 350);
		this.panel6.TabIndex = 89;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(718, 412);
		base.Controls.Add(this.panel8);
		base.Controls.Add(this.panel7);
		base.Controls.Add(this.cmbstudentcourse);
		base.Controls.Add(this.panel6);
		base.Controls.Add(this.panel5);
		base.Controls.Add(this.panelbtnlogin);
		base.Controls.Add(this.panel4);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panelstrandcourse);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel3);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "frmUpdateStudent";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmUpdateStudent";
		base.Load += new System.EventHandler(frmUpdateStudent_Load);
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
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox4).EndInit();
		this.panelbtnlogin.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBoxbtnsave).EndInit();
		this.panel5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
