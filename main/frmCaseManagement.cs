using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmCaseManagement : Form
{
	private Class1 cases = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private string username;

	private string studentnumber;

	private string lastname;

	private string firstname;

	private string middlename;

	private string yearlevel;

	private string course;

	private string violationcode;

	private string violationdescription;

	private string status;

	private string caseid;

	private string resolution;

	private string schoolyear;

	private string concernlevel;

	private string recommendation;

	private int row;

	private IContainer components;

	private Label labelstudentnumber;

	private TextBox txtstudentid;

	private Label labellastname;

	private Label labelfirstname;

	private Label labelmiddlename;

	private Label labelyearlevel;

	private Label labelcourse;

	private DataGridView dataGridView1;

	private Panel panelheader;

	private PictureBox btn_minimize;

	private PictureBox pbcasemanagement;

	private PictureBox btn_close;

	private Label labelAccountsManagement;

	private TextBox txtlastname;

	private TextBox txtfirstname;

	private TextBox txtmiddlename;

	private TextBox txtyearlevel;

	private TextBox txtcourse;

	private Panel panelinformation;

	private Label labelinformation;

	private PictureBox pbstudentnumber;

	private Panel panelsearchbar;

	private Panel paneladdcase;

	private PictureBox btnAdd;

	private Label labeladd;

	private Panel paneloptionlist;

	private Panel paneloptions;

	private Label labeloptions;

	private Panel panelupdatecase;

	private PictureBox btnupdate;

	private Label labelupdate;

	private Panel panelclear;

	private Button btnclear;

	private PictureBox pbclear;

	private PictureBox btn_refresh;

	private Label labelguide;

	private void btn_refresh_Click(object sender, EventArgs e)
	{
		LoadCases();
	}

	public void LoadCases()
	{
		try
		{
			DataTable dt = cases.GetData("SELECT c.caseID, c.violationID, c.violationCount, v.description, c.status, c.resolution, c.schoolyear, c.concernlevel, c.recommendation, c.createdBy, c.dateCreated FROM tblcases c INNER JOIN tblviolations v ON c.violationID = v.violationcode WHERE c.studentID = '" + txtstudentid.Text + "' ORDER BY c.caseID DESC");
			dataGridView1.DataSource = dt;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on cases load", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			row = e.RowIndex;
			dataGridView1.Rows[e.RowIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on datagrid cellclick", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnclear_Click(object sender, EventArgs e)
	{
		txtstudentid.Text = "";
		txtstudentid.Focus();
	}

	private void btnupdate_Click(object sender, EventArgs e)
	{
		studentnumber = txtstudentid.Text;
		lastname = txtlastname.Text;
		firstname = txtfirstname.Text;
		middlename = txtmiddlename.Text;
		yearlevel = txtyearlevel.Text;
		course = txtcourse.Text;
		caseid = dataGridView1.Rows[row].Cells[0].Value.ToString();
		violationcode = dataGridView1.Rows[row].Cells[1].Value.ToString();
		violationdescription = dataGridView1.Rows[row].Cells[3].Value.ToString();
		status = dataGridView1.Rows[row].Cells[4].Value.ToString();
		resolution = dataGridView1.Rows[row].Cells[5].Value.ToString();
		schoolyear = dataGridView1.Rows[row].Cells[6].Value.ToString();
		concernlevel = dataGridView1.Rows[row].Cells[7].Value.ToString();
		recommendation = dataGridView1.Rows[row].Cells[8].Value.ToString();
		new frmUpdateCase(this, studentnumber, lastname, firstname, middlename, yearlevel, course, username, violationcode, violationdescription, status, caseid, resolution, schoolyear, concernlevel, recommendation).Show();
	}

	public frmCaseManagement(string username)
	{
		InitializeComponent();
		this.Draggable(enable: true);
		this.username = username;
		txtstudentid.Focus();
	}

	private void frmCaseManagement_Load(object sender, EventArgs e)
	{
		txtstudentid.Focus();
	}

	private void txtstudentid_TextChanged(object sender, EventArgs e)
	{
		txtlastname.Text = "";
		txtfirstname.Text = "";
		txtmiddlename.Text = "";
		txtyearlevel.Text = "";
		txtcourse.Text = "";
		string casequery = "SELECT c.caseID, c.violationID, c.violationCount, v.description, c.status, c.resolution, c.schoolyear, c.concernlevel, c.recommendation, c.createdBy, c.dateCreated FROM tblcases c INNER JOIN tblviolations v ON c.violationID = v.violationcode WHERE c.studentID = '" + txtstudentid.Text + "' ORDER BY c.caseID DESC";
		DataTable caseDetails = cases.GetData(casequery);
		dataGridView1.DataSource = null;
		dataGridView1.Rows.Clear();
		dataGridView1.Columns.Clear();
		if (caseDetails.Rows.Count > 0)
		{
			dataGridView1.DataSource = caseDetails;
		}
		string studentquery = "SELECT studentLN, studentFN, studentMN, yearLevel, studentCourse FROM tblstudents WHERE studentID = '" + txtstudentid.Text + "'";
		DataTable studentcase = cases.GetData(studentquery);
		if (studentcase.Rows.Count > 0)
		{
			labelguide.Visible = false;
			btn_refresh.Visible = true;
			btn_refresh.Enabled = true;
			btn_refresh.BackColor = Color.FromArgb(150, 0, 52, 112);
			btnAdd.Visible = true;
			btnupdate.Visible = true;
			paneloptionlist.BackColor = SystemColors.Highlight;
			paneladdcase.Enabled = true;
			panelupdatecase.Enabled = true;
			paneladdcase.BackColor = Color.LimeGreen;
			panelupdatecase.BackColor = Color.FromArgb(89, 133, 225);
			txtlastname.Text = studentcase.Rows[0]["studentLN"].ToString();
			txtfirstname.Text = studentcase.Rows[0]["studentFN"].ToString();
			txtmiddlename.Text = studentcase.Rows[0]["studentMN"].ToString();
			txtyearlevel.Text = studentcase.Rows[0]["yearLevel"].ToString();
			txtcourse.Text = studentcase.Rows[0]["studentCourse"].ToString();
		}
		else
		{
			labelguide.Visible = true;
			btn_refresh.Visible = false;
			btn_refresh.Enabled = false;
			btn_refresh.BackColor = Color.Transparent;
			btnAdd.Visible = false;
			btnupdate.Visible = false;
			paneloptionlist.BackColor = SystemColors.ButtonShadow;
			paneladdcase.Enabled = false;
			panelupdatecase.Enabled = false;
			paneladdcase.BackColor = SystemColors.ButtonShadow;
			panelupdatecase.BackColor = SystemColors.ButtonShadow;
		}
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		studentnumber = txtstudentid.Text;
		lastname = txtlastname.Text;
		firstname = txtfirstname.Text;
		middlename = txtmiddlename.Text;
		yearlevel = txtyearlevel.Text;
		course = txtcourse.Text;
		new frmAddCase(this, studentnumber, lastname, firstname, middlename, yearlevel, course, username).Show();
	}

	private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
	{
		dataGridView1.Rows[e.RowIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
		dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
	}

	private void btnAdd_MouseEnter(object sender, EventArgs e)
	{
		btnAdd.BackColor = Color.LimeGreen;
	}

	private void btnAdd_MouseLeave(object sender, EventArgs e)
	{
		btnAdd.BackColor = Color.Azure;
	}

	private void btnupdate_MouseEnter(object sender, EventArgs e)
	{
		btnupdate.BackColor = Color.FromArgb(73, 116, 225);
	}

	private void btnupdate_MouseLeave(object sender, EventArgs e)
	{
		btnupdate.BackColor = Color.Azure;
	}

	private void btn_refresh_MouseLeave(object sender, EventArgs e)
	{
		btn_refresh.BackColor = Color.FromArgb(150, 0, 52, 112);
	}

	private void btn_refresh_MouseEnter(object sender, EventArgs e)
	{
		btn_refresh.BackColor = Color.LightBlue;
	}

	private void btn_minimize_Click_1(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void btn_minimize_MouseEnter_1(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.Silver;
	}

	private void btn_minimize_MouseLeave_1(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.FromArgb(150, 0, 52, 112);
	}

	private void btn_close_Click_1(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_close_MouseEnter_1(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.Salmon;
	}

	private void btn_close_MouseLeave_1(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmCaseManagement));
		this.labelstudentnumber = new System.Windows.Forms.Label();
		this.txtstudentid = new System.Windows.Forms.TextBox();
		this.labellastname = new System.Windows.Forms.Label();
		this.labelfirstname = new System.Windows.Forms.Label();
		this.labelmiddlename = new System.Windows.Forms.Label();
		this.labelyearlevel = new System.Windows.Forms.Label();
		this.labelcourse = new System.Windows.Forms.Label();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.panelheader = new System.Windows.Forms.Panel();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.pbcasemanagement = new System.Windows.Forms.PictureBox();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.labelAccountsManagement = new System.Windows.Forms.Label();
		this.txtlastname = new System.Windows.Forms.TextBox();
		this.txtfirstname = new System.Windows.Forms.TextBox();
		this.txtmiddlename = new System.Windows.Forms.TextBox();
		this.txtyearlevel = new System.Windows.Forms.TextBox();
		this.txtcourse = new System.Windows.Forms.TextBox();
		this.panelinformation = new System.Windows.Forms.Panel();
		this.labelinformation = new System.Windows.Forms.Label();
		this.pbstudentnumber = new System.Windows.Forms.PictureBox();
		this.panelsearchbar = new System.Windows.Forms.Panel();
		this.btn_refresh = new System.Windows.Forms.PictureBox();
		this.panelclear = new System.Windows.Forms.Panel();
		this.btnclear = new System.Windows.Forms.Button();
		this.pbclear = new System.Windows.Forms.PictureBox();
		this.paneladdcase = new System.Windows.Forms.Panel();
		this.btnAdd = new System.Windows.Forms.PictureBox();
		this.labeladd = new System.Windows.Forms.Label();
		this.paneloptionlist = new System.Windows.Forms.Panel();
		this.panelupdatecase = new System.Windows.Forms.Panel();
		this.btnupdate = new System.Windows.Forms.PictureBox();
		this.labelupdate = new System.Windows.Forms.Label();
		this.paneloptions = new System.Windows.Forms.Panel();
		this.labeloptions = new System.Windows.Forms.Label();
		this.labelguide = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.panelheader.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbcasemanagement).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		this.panelinformation.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbstudentnumber).BeginInit();
		this.panelsearchbar.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_refresh).BeginInit();
		this.panelclear.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbclear).BeginInit();
		this.paneladdcase.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnAdd).BeginInit();
		this.paneloptionlist.SuspendLayout();
		this.panelupdatecase.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnupdate).BeginInit();
		this.paneloptions.SuspendLayout();
		base.SuspendLayout();
		this.labelstudentnumber.AutoSize = true;
		this.labelstudentnumber.BackColor = System.Drawing.Color.Transparent;
		this.labelstudentnumber.Font = new System.Drawing.Font("Spiegel Bold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelstudentnumber.ForeColor = System.Drawing.Color.Azure;
		this.labelstudentnumber.Location = new System.Drawing.Point(71, 15);
		this.labelstudentnumber.Name = "labelstudentnumber";
		this.labelstudentnumber.Size = new System.Drawing.Size(105, 16);
		this.labelstudentnumber.TabIndex = 0;
		this.labelstudentnumber.Text = "Student Number:";
		this.txtstudentid.Location = new System.Drawing.Point(179, 13);
		this.txtstudentid.Name = "txtstudentid";
		this.txtstudentid.Size = new System.Drawing.Size(219, 20);
		this.txtstudentid.TabIndex = 1;
		this.txtstudentid.TextChanged += new System.EventHandler(txtstudentid_TextChanged);
		this.labellastname.AutoSize = true;
		this.labellastname.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labellastname.Location = new System.Drawing.Point(20, 187);
		this.labellastname.Name = "labellastname";
		this.labellastname.Size = new System.Drawing.Size(60, 15);
		this.labellastname.TabIndex = 2;
		this.labellastname.Text = "Lastname:";
		this.labelfirstname.AutoSize = true;
		this.labelfirstname.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelfirstname.Location = new System.Drawing.Point(21, 210);
		this.labelfirstname.Name = "labelfirstname";
		this.labelfirstname.Size = new System.Drawing.Size(62, 15);
		this.labelfirstname.TabIndex = 3;
		this.labelfirstname.Text = "Firstname:";
		this.labelmiddlename.AutoSize = true;
		this.labelmiddlename.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelmiddlename.Location = new System.Drawing.Point(20, 236);
		this.labelmiddlename.Name = "labelmiddlename";
		this.labelmiddlename.Size = new System.Drawing.Size(76, 15);
		this.labelmiddlename.TabIndex = 4;
		this.labelmiddlename.Text = "Middlename:";
		this.labelyearlevel.AutoSize = true;
		this.labelyearlevel.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelyearlevel.Location = new System.Drawing.Point(274, 190);
		this.labelyearlevel.Name = "labelyearlevel";
		this.labelyearlevel.Size = new System.Drawing.Size(37, 15);
		this.labelyearlevel.TabIndex = 5;
		this.labelyearlevel.Text = "Level:";
		this.labelcourse.AutoSize = true;
		this.labelcourse.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelcourse.Location = new System.Drawing.Point(274, 216);
		this.labelcourse.Name = "labelcourse";
		this.labelcourse.Size = new System.Drawing.Size(85, 15);
		this.labelcourse.TabIndex = 6;
		this.labelcourse.Text = "Strand/Course:";
		this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.Location = new System.Drawing.Point(22, 278);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(1089, 385);
		this.dataGridView1.TabIndex = 7;
		this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
		this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellLeave);
		this.panelheader.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panelheader.Controls.Add(this.btn_minimize);
		this.panelheader.Controls.Add(this.pbcasemanagement);
		this.panelheader.Controls.Add(this.btn_close);
		this.panelheader.Controls.Add(this.labelAccountsManagement);
		this.panelheader.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelheader.Location = new System.Drawing.Point(0, 0);
		this.panelheader.Name = "panelheader";
		this.panelheader.Size = new System.Drawing.Size(1136, 77);
		this.panelheader.TabIndex = 11;
		this.btn_minimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_minimize.BackColor = System.Drawing.Color.FromArgb(150, 0, 52, 112);
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(1065, 12);
		this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_minimize.Name = "btn_minimize";
		this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
		this.btn_minimize.Size = new System.Drawing.Size(28, 25);
		this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_minimize.TabIndex = 43;
		this.btn_minimize.TabStop = false;
		this.btn_minimize.Click += new System.EventHandler(btn_minimize_Click_1);
		this.btn_minimize.MouseEnter += new System.EventHandler(btn_minimize_MouseEnter_1);
		this.btn_minimize.MouseLeave += new System.EventHandler(btn_minimize_MouseLeave_1);
		this.pbcasemanagement.BackColor = System.Drawing.Color.Transparent;
		this.pbcasemanagement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pbcasemanagement.Dock = System.Windows.Forms.DockStyle.Left;
		this.pbcasemanagement.Image = (System.Drawing.Image)resources.GetObject("pbcasemanagement.Image");
		this.pbcasemanagement.Location = new System.Drawing.Point(0, 0);
		this.pbcasemanagement.Name = "pbcasemanagement";
		this.pbcasemanagement.Padding = new System.Windows.Forms.Padding(10);
		this.pbcasemanagement.Size = new System.Drawing.Size(84, 77);
		this.pbcasemanagement.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pbcasemanagement.TabIndex = 28;
		this.pbcasemanagement.TabStop = false;
		this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_close.BackColor = System.Drawing.Color.FromArgb(150, 0, 52, 112);
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(1097, 12);
		this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_close.Name = "btn_close";
		this.btn_close.Padding = new System.Windows.Forms.Padding(3);
		this.btn_close.Size = new System.Drawing.Size(28, 25);
		this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_close.TabIndex = 42;
		this.btn_close.TabStop = false;
		this.btn_close.Click += new System.EventHandler(btn_close_Click_1);
		this.btn_close.MouseEnter += new System.EventHandler(btn_close_MouseEnter_1);
		this.btn_close.MouseLeave += new System.EventHandler(btn_close_MouseLeave_1);
		this.labelAccountsManagement.AutoSize = true;
		this.labelAccountsManagement.BackColor = System.Drawing.Color.Transparent;
		this.labelAccountsManagement.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelAccountsManagement.ForeColor = System.Drawing.Color.White;
		this.labelAccountsManagement.Location = new System.Drawing.Point(90, 27);
		this.labelAccountsManagement.Name = "labelAccountsManagement";
		this.labelAccountsManagement.Size = new System.Drawing.Size(223, 26);
		this.labelAccountsManagement.TabIndex = 27;
		this.labelAccountsManagement.Text = "CASE MANAGEMENT";
		this.txtlastname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtlastname.Cursor = System.Windows.Forms.Cursors.Default;
		this.txtlastname.Enabled = false;
		this.txtlastname.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtlastname.Location = new System.Drawing.Point(102, 187);
		this.txtlastname.Name = "txtlastname";
		this.txtlastname.ReadOnly = true;
		this.txtlastname.Size = new System.Drawing.Size(148, 22);
		this.txtlastname.TabIndex = 12;
		this.txtlastname.TabStop = false;
		this.txtfirstname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtfirstname.Cursor = System.Windows.Forms.Cursors.Default;
		this.txtfirstname.Enabled = false;
		this.txtfirstname.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtfirstname.Location = new System.Drawing.Point(102, 211);
		this.txtfirstname.Name = "txtfirstname";
		this.txtfirstname.ReadOnly = true;
		this.txtfirstname.Size = new System.Drawing.Size(148, 22);
		this.txtfirstname.TabIndex = 13;
		this.txtfirstname.TabStop = false;
		this.txtmiddlename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtmiddlename.Cursor = System.Windows.Forms.Cursors.Default;
		this.txtmiddlename.Enabled = false;
		this.txtmiddlename.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtmiddlename.Location = new System.Drawing.Point(102, 235);
		this.txtmiddlename.Name = "txtmiddlename";
		this.txtmiddlename.ReadOnly = true;
		this.txtmiddlename.Size = new System.Drawing.Size(148, 22);
		this.txtmiddlename.TabIndex = 14;
		this.txtmiddlename.TabStop = false;
		this.txtyearlevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtyearlevel.Cursor = System.Windows.Forms.Cursors.Default;
		this.txtyearlevel.Enabled = false;
		this.txtyearlevel.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtyearlevel.Location = new System.Drawing.Point(362, 185);
		this.txtyearlevel.Name = "txtyearlevel";
		this.txtyearlevel.ReadOnly = true;
		this.txtyearlevel.Size = new System.Drawing.Size(122, 22);
		this.txtyearlevel.TabIndex = 15;
		this.txtyearlevel.TabStop = false;
		this.txtcourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtcourse.Cursor = System.Windows.Forms.Cursors.Default;
		this.txtcourse.Enabled = false;
		this.txtcourse.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcourse.Location = new System.Drawing.Point(362, 210);
		this.txtcourse.Multiline = true;
		this.txtcourse.Name = "txtcourse";
		this.txtcourse.ReadOnly = true;
		this.txtcourse.Size = new System.Drawing.Size(194, 47);
		this.txtcourse.TabIndex = 16;
		this.txtcourse.TabStop = false;
		this.panelinformation.BackColor = System.Drawing.SystemColors.HotTrack;
		this.panelinformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelinformation.Controls.Add(this.labelinformation);
		this.panelinformation.Location = new System.Drawing.Point(22, 156);
		this.panelinformation.Name = "panelinformation";
		this.panelinformation.Size = new System.Drawing.Size(125, 25);
		this.panelinformation.TabIndex = 17;
		this.labelinformation.AutoSize = true;
		this.labelinformation.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelinformation.ForeColor = System.Drawing.Color.Azure;
		this.labelinformation.Location = new System.Drawing.Point(3, 2);
		this.labelinformation.Name = "labelinformation";
		this.labelinformation.Size = new System.Drawing.Size(117, 19);
		this.labelinformation.TabIndex = 0;
		this.labelinformation.Text = "INFORMATION";
		this.pbstudentnumber.BackColor = System.Drawing.SystemColors.HotTrack;
		this.pbstudentnumber.Dock = System.Windows.Forms.DockStyle.Left;
		this.pbstudentnumber.Image = (System.Drawing.Image)resources.GetObject("pbstudentnumber.Image");
		this.pbstudentnumber.Location = new System.Drawing.Point(0, 0);
		this.pbstudentnumber.Name = "pbstudentnumber";
		this.pbstudentnumber.Padding = new System.Windows.Forms.Padding(9, 5, 3, 5);
		this.pbstudentnumber.Size = new System.Drawing.Size(63, 46);
		this.pbstudentnumber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pbstudentnumber.TabIndex = 46;
		this.pbstudentnumber.TabStop = false;
		this.panelsearchbar.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panelsearchbar.Controls.Add(this.btn_refresh);
		this.panelsearchbar.Controls.Add(this.panelclear);
		this.panelsearchbar.Controls.Add(this.pbstudentnumber);
		this.panelsearchbar.Controls.Add(this.labelstudentnumber);
		this.panelsearchbar.Controls.Add(this.txtstudentid);
		this.panelsearchbar.Location = new System.Drawing.Point(22, 99);
		this.panelsearchbar.Name = "panelsearchbar";
		this.panelsearchbar.Size = new System.Drawing.Size(552, 46);
		this.panelsearchbar.TabIndex = 47;
		this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
		this.btn_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_refresh.Enabled = false;
		this.btn_refresh.Image = (System.Drawing.Image)resources.GetObject("btn_refresh.Image");
		this.btn_refresh.Location = new System.Drawing.Point(407, 8);
		this.btn_refresh.Name = "btn_refresh";
		this.btn_refresh.Padding = new System.Windows.Forms.Padding(3);
		this.btn_refresh.Size = new System.Drawing.Size(33, 30);
		this.btn_refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_refresh.TabIndex = 52;
		this.btn_refresh.TabStop = false;
		this.btn_refresh.Visible = false;
		this.btn_refresh.Click += new System.EventHandler(btn_refresh_Click);
		this.btn_refresh.MouseEnter += new System.EventHandler(btn_refresh_MouseEnter);
		this.btn_refresh.MouseLeave += new System.EventHandler(btn_refresh_MouseLeave);
		this.panelclear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelclear.Controls.Add(this.btnclear);
		this.panelclear.Controls.Add(this.pbclear);
		this.panelclear.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panelclear.Location = new System.Drawing.Point(446, 8);
		this.panelclear.Name = "panelclear";
		this.panelclear.Size = new System.Drawing.Size(94, 30);
		this.panelclear.TabIndex = 86;
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
		this.pbclear.BackColor = System.Drawing.Color.Firebrick;
		this.pbclear.Dock = System.Windows.Forms.DockStyle.Left;
		this.pbclear.Image = (System.Drawing.Image)resources.GetObject("pbclear.Image");
		this.pbclear.Location = new System.Drawing.Point(0, 0);
		this.pbclear.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.pbclear.Name = "pbclear";
		this.pbclear.Padding = new System.Windows.Forms.Padding(3);
		this.pbclear.Size = new System.Drawing.Size(33, 28);
		this.pbclear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pbclear.TabIndex = 21;
		this.pbclear.TabStop = false;
		this.paneladdcase.BackColor = System.Drawing.SystemColors.ButtonShadow;
		this.paneladdcase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.paneladdcase.Controls.Add(this.btnAdd);
		this.paneladdcase.Controls.Add(this.labeladd);
		this.paneladdcase.Enabled = false;
		this.paneladdcase.Location = new System.Drawing.Point(11, 6);
		this.paneladdcase.Name = "paneladdcase";
		this.paneladdcase.Size = new System.Drawing.Size(49, 55);
		this.paneladdcase.TabIndex = 48;
		this.btnAdd.BackColor = System.Drawing.Color.Azure;
		this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnAdd.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnAdd.Image = (System.Drawing.Image)resources.GetObject("btnAdd.Image");
		this.btnAdd.Location = new System.Drawing.Point(0, 0);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Padding = new System.Windows.Forms.Padding(4);
		this.btnAdd.Size = new System.Drawing.Size(47, 38);
		this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btnAdd.TabIndex = 1;
		this.btnAdd.TabStop = false;
		this.btnAdd.Visible = false;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.btnAdd.MouseEnter += new System.EventHandler(btnAdd_MouseEnter);
		this.btnAdd.MouseLeave += new System.EventHandler(btnAdd_MouseLeave);
		this.labeladd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.labeladd.AutoSize = true;
		this.labeladd.Cursor = System.Windows.Forms.Cursors.Default;
		this.labeladd.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labeladd.ForeColor = System.Drawing.Color.Azure;
		this.labeladd.Location = new System.Drawing.Point(10, 38);
		this.labeladd.Name = "labeladd";
		this.labeladd.Size = new System.Drawing.Size(28, 15);
		this.labeladd.TabIndex = 0;
		this.labeladd.Text = "ADD";
		this.paneloptionlist.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.paneloptionlist.BackColor = System.Drawing.SystemColors.ButtonShadow;
		this.paneloptionlist.Controls.Add(this.panelupdatecase);
		this.paneloptionlist.Controls.Add(this.paneladdcase);
		this.paneloptionlist.Location = new System.Drawing.Point(600, 187);
		this.paneloptionlist.Name = "paneloptionlist";
		this.paneloptionlist.Size = new System.Drawing.Size(511, 70);
		this.paneloptionlist.TabIndex = 51;
		this.panelupdatecase.BackColor = System.Drawing.SystemColors.ButtonShadow;
		this.panelupdatecase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelupdatecase.Controls.Add(this.btnupdate);
		this.panelupdatecase.Controls.Add(this.labelupdate);
		this.panelupdatecase.Enabled = false;
		this.panelupdatecase.Location = new System.Drawing.Point(66, 6);
		this.panelupdatecase.Name = "panelupdatecase";
		this.panelupdatecase.Size = new System.Drawing.Size(49, 56);
		this.panelupdatecase.TabIndex = 52;
		this.btnupdate.BackColor = System.Drawing.Color.Azure;
		this.btnupdate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnupdate.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnupdate.Image = (System.Drawing.Image)resources.GetObject("btnupdate.Image");
		this.btnupdate.Location = new System.Drawing.Point(0, 0);
		this.btnupdate.Name = "btnupdate";
		this.btnupdate.Padding = new System.Windows.Forms.Padding(4);
		this.btnupdate.Size = new System.Drawing.Size(47, 38);
		this.btnupdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btnupdate.TabIndex = 1;
		this.btnupdate.TabStop = false;
		this.btnupdate.Visible = false;
		this.btnupdate.Click += new System.EventHandler(btnupdate_Click);
		this.btnupdate.MouseEnter += new System.EventHandler(btnupdate_MouseEnter);
		this.btnupdate.MouseLeave += new System.EventHandler(btnupdate_MouseLeave);
		this.labelupdate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.labelupdate.AutoSize = true;
		this.labelupdate.Cursor = System.Windows.Forms.Cursors.Default;
		this.labelupdate.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelupdate.ForeColor = System.Drawing.Color.Azure;
		this.labelupdate.Location = new System.Drawing.Point(1, 37);
		this.labelupdate.Name = "labelupdate";
		this.labelupdate.Size = new System.Drawing.Size(47, 15);
		this.labelupdate.TabIndex = 0;
		this.labelupdate.Text = "UPDATE";
		this.paneloptions.BackColor = System.Drawing.SystemColors.HotTrack;
		this.paneloptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.paneloptions.Controls.Add(this.labeloptions);
		this.paneloptions.Location = new System.Drawing.Point(600, 156);
		this.paneloptions.Name = "paneloptions";
		this.paneloptions.Size = new System.Drawing.Size(93, 25);
		this.paneloptions.TabIndex = 18;
		this.labeloptions.AutoSize = true;
		this.labeloptions.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labeloptions.ForeColor = System.Drawing.Color.Azure;
		this.labeloptions.Location = new System.Drawing.Point(7, 2);
		this.labeloptions.Name = "labeloptions";
		this.labeloptions.Size = new System.Drawing.Size(76, 19);
		this.labeloptions.TabIndex = 0;
		this.labeloptions.Text = "OPTIONS";
		this.labelguide.AutoSize = true;
		this.labelguide.BackColor = System.Drawing.SystemColors.AppWorkspace;
		this.labelguide.Font = new System.Drawing.Font("Spiegel Bold", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelguide.Location = new System.Drawing.Point(369, 462);
		this.labelguide.Name = "labelguide";
		this.labelguide.Size = new System.Drawing.Size(419, 22);
		this.labelguide.TabIndex = 52;
		this.labelguide.Text = "Enter the registered student number to proceed";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1136, 681);
		base.ControlBox = false;
		base.Controls.Add(this.labelguide);
		base.Controls.Add(this.paneloptions);
		base.Controls.Add(this.paneloptionlist);
		base.Controls.Add(this.panelsearchbar);
		base.Controls.Add(this.panelinformation);
		base.Controls.Add(this.txtcourse);
		base.Controls.Add(this.txtyearlevel);
		base.Controls.Add(this.txtmiddlename);
		base.Controls.Add(this.txtfirstname);
		base.Controls.Add(this.txtlastname);
		base.Controls.Add(this.panelheader);
		base.Controls.Add(this.dataGridView1);
		base.Controls.Add(this.labelcourse);
		base.Controls.Add(this.labelyearlevel);
		base.Controls.Add(this.labelmiddlename);
		base.Controls.Add(this.labelfirstname);
		base.Controls.Add(this.labellastname);
		base.Name = "frmCaseManagement";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "               ";
		base.Load += new System.EventHandler(frmCaseManagement_Load);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.panelheader.ResumeLayout(false);
		this.panelheader.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbcasemanagement).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		this.panelinformation.ResumeLayout(false);
		this.panelinformation.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbstudentnumber).EndInit();
		this.panelsearchbar.ResumeLayout(false);
		this.panelsearchbar.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_refresh).EndInit();
		this.panelclear.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pbclear).EndInit();
		this.paneladdcase.ResumeLayout(false);
		this.paneladdcase.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnAdd).EndInit();
		this.paneloptionlist.ResumeLayout(false);
		this.panelupdatecase.ResumeLayout(false);
		this.panelupdatecase.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnupdate).EndInit();
		this.paneloptions.ResumeLayout(false);
		this.paneloptions.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
