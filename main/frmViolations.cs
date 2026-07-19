using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmViolations : Form
{
	private string username;

	private Class1 violations = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private int row;

	private IContainer components;

	private Panel panel1;

	private Panel panel3;

	private Panel paneladd;

	private Label labelAccountsManagement;

	private PictureBox pictureBox2;

	private PictureBox pictureBoxSearch;

	private Panel panelsearchbar;

	private TextBox txtsearch;

	private Panel panelrefresh;

	private PictureBox btn_refresh;

	private Panel panelupdate;

	private Panel paneldelete;

	private DataGridView dataGridView1;

	private PictureBox btnupdate;

	private Label labelupdate;

	private PictureBox btndelete;

	private Label labeldelete;

	private PictureBox btnAdd;

	private Label labeladd;

	private PictureBox btn_minimize;

	private PictureBox btn_close;

	private PictureBox pictureBoxOptions;

	private Label label1;

	private Panel panel2;

	private Panel panel11;

	private Panel panel18;

	public frmViolations(string username)
	{
		InitializeComponent();
		this.username = username;
		this.Draggable(enable: true);
	}

	private void frmViolations_Load(object sender, EventArgs e)
	{
		LoadViolations();
	}

	public void LoadViolations()
	{
		try
		{
			DataTable dt = violations.GetData("SELECT violationcode, description, type, status, createdby, datecreated FROM tblviolations ORDER BY violationcode");
			dataGridView1.DataSource = dt;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on violations load", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtsearch_TextChanged_1(object sender, EventArgs e)
	{
		try
		{
			DataTable dt = violations.GetData("SELECT violationcode, description, type, status, createdby, datecreated FROM tblviolations WHERE (violationcode LIKE '%" + txtsearch.Text + "%' OR description LIKE '%" + txtsearch.Text + "%' OR type LIKE '%" + txtsearch.Text + "%') ORDER BY violationcode");
			dataGridView1.DataSource = dt;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on search", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btn_refresh_Click_1(object sender, EventArgs e)
	{
		frmViolations_Load(sender, e);
	}

	private void paneladd_Click(object sender, EventArgs e)
	{
		new frmAddviolation(this, username).Show();
	}

	private void panelupdate_Click(object sender, EventArgs e)
	{
		string editviolationcode = dataGridView1.Rows[row].Cells[0].Value.ToString();
		string editdescription = dataGridView1.Rows[row].Cells[1].Value.ToString();
		string edittype = dataGridView1.Rows[row].Cells[2].Value.ToString();
		string editstatus = dataGridView1.Rows[row].Cells[3].Value.ToString();
		new frmUpdateViolation(this, editviolationcode, editdescription, edittype, editstatus, username).Show();
	}

	private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
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

	private void paneldelete_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("Are you sure you want to delete this violation?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		string selectedviolation = dataGridView1.Rows[row].Cells[0].Value.ToString();
		try
		{
			violations.executeSQL("DELETE FROM tblviolations WHERE violationcode = '" + selectedviolation + "'");
			if (violations.rowAffected > 0)
			{
				violations.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Delete', 'Violations Management', '" + selectedviolation + "', '" + username + "')");
				MessageBox.Show("Violation Deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				LoadViolations();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on delete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void paneladd_MouseEnter(object sender, EventArgs e)
	{
		paneladd.BackColor = Color.LimeGreen;
		labeladd.Enabled = false;
		btnAdd.BackColor = Color.FromArgb(7, 205, 50);
	}

	private void paneladd_MouseLeave(object sender, EventArgs e)
	{
		paneladd.BackColor = Color.Azure;
		labeladd.Enabled = true;
		btnAdd.BackColor = Color.Azure;
	}

	private void panelupdate_MouseEnter(object sender, EventArgs e)
	{
		panelupdate.BackColor = Color.FromArgb(89, 133, 225);
		labelupdate.Enabled = false;
		btnupdate.BackColor = Color.FromArgb(73, 116, 225);
	}

	private void panelupdate_MouseLeave(object sender, EventArgs e)
	{
		panelupdate.BackColor = Color.Azure;
		labelupdate.Enabled = true;
		btnupdate.BackColor = Color.Azure;
	}

	private void paneldelete_MouseEnter(object sender, EventArgs e)
	{
		paneldelete.BackColor = Color.FromArgb(234, 51, 35);
		labeldelete.Enabled = false;
		btndelete.BackColor = Color.FromArgb(200, 51, 40);
	}

	private void paneldelete_MouseLeave(object sender, EventArgs e)
	{
		paneldelete.BackColor = Color.Azure;
		labeldelete.Enabled = true;
		btndelete.BackColor = Color.Azure;
	}

	private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
	{
		dataGridView1.Rows[e.RowIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
		dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
		btn_minimize.BackColor = Color.FromArgb(135, 156, 34, 23);
	}

	private void btn_close_MouseEnter(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.Salmon;
	}

	private void btn_close_MouseLeave(object sender, EventArgs e)
	{
		btn_close.BackColor = Color.FromArgb(135, 156, 34, 23);
	}

	private void btn_refresh_MouseEnter(object sender, EventArgs e)
	{
		panelrefresh.BackColor = Color.FromArgb(135, 156, 34, 23);
	}

	private void btn_refresh_MouseLeave(object sender, EventArgs e)
	{
		panelrefresh.BackColor = Color.FromArgb(218, 41, 28);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmViolations));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.labelAccountsManagement = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.paneladd = new System.Windows.Forms.Panel();
		this.btnAdd = new System.Windows.Forms.PictureBox();
		this.labeladd = new System.Windows.Forms.Label();
		this.panel18 = new System.Windows.Forms.Panel();
		this.panelupdate = new System.Windows.Forms.Panel();
		this.btnupdate = new System.Windows.Forms.PictureBox();
		this.labelupdate = new System.Windows.Forms.Label();
		this.paneldelete = new System.Windows.Forms.Panel();
		this.btndelete = new System.Windows.Forms.PictureBox();
		this.labeldelete = new System.Windows.Forms.Label();
		this.panel11 = new System.Windows.Forms.Panel();
		this.panel2 = new System.Windows.Forms.Panel();
		this.pictureBoxOptions = new System.Windows.Forms.PictureBox();
		this.label1 = new System.Windows.Forms.Label();
		this.panelsearchbar = new System.Windows.Forms.Panel();
		this.txtsearch = new System.Windows.Forms.TextBox();
		this.panelrefresh = new System.Windows.Forms.Panel();
		this.btn_refresh = new System.Windows.Forms.PictureBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.pictureBoxSearch = new System.Windows.Forms.PictureBox();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		this.panel3.SuspendLayout();
		this.paneladd.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnAdd).BeginInit();
		this.panelupdate.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnupdate).BeginInit();
		this.paneldelete.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btndelete).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxOptions).BeginInit();
		this.panelsearchbar.SuspendLayout();
		this.panelrefresh.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_refresh).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxSearch).BeginInit();
		base.SuspendLayout();
		this.panel1.BackColor = System.Drawing.Color.FromArgb(234, 51, 35);
		this.panel1.Controls.Add(this.btn_minimize);
		this.panel1.Controls.Add(this.pictureBox2);
		this.panel1.Controls.Add(this.btn_close);
		this.panel1.Controls.Add(this.labelAccountsManagement);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(800, 77);
		this.panel1.TabIndex = 0;
		this.btn_minimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_minimize.BackColor = System.Drawing.Color.FromArgb(135, 156, 34, 23);
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(729, 12);
		this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_minimize.Name = "btn_minimize";
		this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
		this.btn_minimize.Size = new System.Drawing.Size(28, 25);
		this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_minimize.TabIndex = 43;
		this.btn_minimize.TabStop = false;
		this.btn_minimize.Click += new System.EventHandler(btn_minimize_Click);
		this.btn_minimize.MouseEnter += new System.EventHandler(btn_minimize_MouseEnter);
		this.btn_minimize.MouseLeave += new System.EventHandler(btn_minimize_MouseLeave);
		this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(0, 0);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Padding = new System.Windows.Forms.Padding(7);
		this.pictureBox2.Size = new System.Drawing.Size(84, 77);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 28;
		this.pictureBox2.TabStop = false;
		this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_close.BackColor = System.Drawing.Color.FromArgb(135, 156, 34, 23);
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(761, 12);
		this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_close.Name = "btn_close";
		this.btn_close.Padding = new System.Windows.Forms.Padding(3);
		this.btn_close.Size = new System.Drawing.Size(28, 25);
		this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_close.TabIndex = 42;
		this.btn_close.TabStop = false;
		this.btn_close.Click += new System.EventHandler(btn_close_Click);
		this.btn_close.MouseEnter += new System.EventHandler(btn_close_MouseEnter);
		this.btn_close.MouseLeave += new System.EventHandler(btn_close_MouseLeave);
		this.labelAccountsManagement.AutoSize = true;
		this.labelAccountsManagement.BackColor = System.Drawing.Color.Transparent;
		this.labelAccountsManagement.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelAccountsManagement.ForeColor = System.Drawing.Color.White;
		this.labelAccountsManagement.Location = new System.Drawing.Point(90, 27);
		this.labelAccountsManagement.Name = "labelAccountsManagement";
		this.labelAccountsManagement.Size = new System.Drawing.Size(299, 26);
		this.labelAccountsManagement.TabIndex = 27;
		this.labelAccountsManagement.Text = "VIOLATIONS MANAGEMENT";
		this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.panel3.BackgroundImage = (System.Drawing.Image)resources.GetObject("panel3.BackgroundImage");
		this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panel3.Controls.Add(this.paneladd);
		this.panel3.Controls.Add(this.panel18);
		this.panel3.Controls.Add(this.panelupdate);
		this.panel3.Controls.Add(this.paneldelete);
		this.panel3.Controls.Add(this.panel11);
		this.panel3.Location = new System.Drawing.Point(7, 164);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(162, 274);
		this.panel3.TabIndex = 2;
		this.paneladd.BackColor = System.Drawing.Color.Azure;
		this.paneladd.Controls.Add(this.btnAdd);
		this.paneladd.Controls.Add(this.labeladd);
		this.paneladd.Cursor = System.Windows.Forms.Cursors.Hand;
		this.paneladd.Location = new System.Drawing.Point(8, 11);
		this.paneladd.Name = "paneladd";
		this.paneladd.Size = new System.Drawing.Size(146, 34);
		this.paneladd.TabIndex = 3;
		this.paneladd.Click += new System.EventHandler(paneladd_Click);
		this.paneladd.MouseEnter += new System.EventHandler(paneladd_MouseEnter);
		this.paneladd.MouseLeave += new System.EventHandler(paneladd_MouseLeave);
		this.btnAdd.BackColor = System.Drawing.Color.Azure;
		this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
		this.btnAdd.Image = (System.Drawing.Image)resources.GetObject("btnAdd.Image");
		this.btnAdd.Location = new System.Drawing.Point(0, 0);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Padding = new System.Windows.Forms.Padding(4);
		this.btnAdd.Size = new System.Drawing.Size(36, 34);
		this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btnAdd.TabIndex = 1;
		this.btnAdd.TabStop = false;
		this.labeladd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.labeladd.AutoSize = true;
		this.labeladd.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labeladd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.labeladd.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labeladd.ForeColor = System.Drawing.Color.Black;
		this.labeladd.Location = new System.Drawing.Point(77, 10);
		this.labeladd.Name = "labeladd";
		this.labeladd.Size = new System.Drawing.Size(28, 15);
		this.labeladd.TabIndex = 0;
		this.labeladd.Text = "ADD";
		this.panel18.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panel18.Location = new System.Drawing.Point(102, 333);
		this.panel18.Name = "panel18";
		this.panel18.Size = new System.Drawing.Size(20, 16);
		this.panel18.TabIndex = 38;
		this.panelupdate.BackColor = System.Drawing.Color.Azure;
		this.panelupdate.Controls.Add(this.btnupdate);
		this.panelupdate.Controls.Add(this.labelupdate);
		this.panelupdate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.panelupdate.Location = new System.Drawing.Point(8, 57);
		this.panelupdate.Name = "panelupdate";
		this.panelupdate.Size = new System.Drawing.Size(146, 34);
		this.panelupdate.TabIndex = 4;
		this.panelupdate.Click += new System.EventHandler(panelupdate_Click);
		this.panelupdate.MouseEnter += new System.EventHandler(panelupdate_MouseEnter);
		this.panelupdate.MouseLeave += new System.EventHandler(panelupdate_MouseLeave);
		this.btnupdate.BackColor = System.Drawing.Color.Azure;
		this.btnupdate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnupdate.Dock = System.Windows.Forms.DockStyle.Left;
		this.btnupdate.Image = (System.Drawing.Image)resources.GetObject("btnupdate.Image");
		this.btnupdate.Location = new System.Drawing.Point(0, 0);
		this.btnupdate.Name = "btnupdate";
		this.btnupdate.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
		this.btnupdate.Size = new System.Drawing.Size(36, 34);
		this.btnupdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btnupdate.TabIndex = 1;
		this.btnupdate.TabStop = false;
		this.labelupdate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.labelupdate.AutoSize = true;
		this.labelupdate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labelupdate.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelupdate.ForeColor = System.Drawing.Color.Black;
		this.labelupdate.Location = new System.Drawing.Point(67, 10);
		this.labelupdate.Name = "labelupdate";
		this.labelupdate.Size = new System.Drawing.Size(47, 15);
		this.labelupdate.TabIndex = 0;
		this.labelupdate.Text = "UPDATE";
		this.paneldelete.BackColor = System.Drawing.Color.Azure;
		this.paneldelete.Controls.Add(this.btndelete);
		this.paneldelete.Controls.Add(this.labeldelete);
		this.paneldelete.Cursor = System.Windows.Forms.Cursors.Hand;
		this.paneldelete.Location = new System.Drawing.Point(8, 103);
		this.paneldelete.Name = "paneldelete";
		this.paneldelete.Size = new System.Drawing.Size(146, 34);
		this.paneldelete.TabIndex = 31;
		this.paneldelete.Click += new System.EventHandler(paneldelete_Click);
		this.paneldelete.MouseEnter += new System.EventHandler(paneldelete_MouseEnter);
		this.paneldelete.MouseLeave += new System.EventHandler(paneldelete_MouseLeave);
		this.btndelete.BackColor = System.Drawing.Color.Azure;
		this.btndelete.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btndelete.Dock = System.Windows.Forms.DockStyle.Left;
		this.btndelete.Image = (System.Drawing.Image)resources.GetObject("btndelete.Image");
		this.btndelete.Location = new System.Drawing.Point(0, 0);
		this.btndelete.Name = "btndelete";
		this.btndelete.Padding = new System.Windows.Forms.Padding(4);
		this.btndelete.Size = new System.Drawing.Size(36, 34);
		this.btndelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btndelete.TabIndex = 1;
		this.btndelete.TabStop = false;
		this.labeldelete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.labeldelete.AutoSize = true;
		this.labeldelete.Cursor = System.Windows.Forms.Cursors.Hand;
		this.labeldelete.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labeldelete.ForeColor = System.Drawing.Color.Black;
		this.labeldelete.Location = new System.Drawing.Point(68, 10);
		this.labeldelete.Name = "labeldelete";
		this.labeldelete.Size = new System.Drawing.Size(43, 15);
		this.labeldelete.TabIndex = 0;
		this.labeldelete.Text = "DELETE";
		this.panel11.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panel11.Location = new System.Drawing.Point(29, 320);
		this.panel11.Name = "panel11";
		this.panel11.Size = new System.Drawing.Size(17, 29);
		this.panel11.TabIndex = 37;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panel2.Controls.Add(this.pictureBoxOptions);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Location = new System.Drawing.Point(7, 89);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(162, 60);
		this.panel2.TabIndex = 34;
		this.pictureBoxOptions.BackColor = System.Drawing.Color.Transparent;
		this.pictureBoxOptions.Image = (System.Drawing.Image)resources.GetObject("pictureBoxOptions.Image");
		this.pictureBoxOptions.Location = new System.Drawing.Point(6, 9);
		this.pictureBoxOptions.Name = "pictureBoxOptions";
		this.pictureBoxOptions.Size = new System.Drawing.Size(43, 40);
		this.pictureBoxOptions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBoxOptions.TabIndex = 32;
		this.pictureBoxOptions.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Azure;
		this.label1.Location = new System.Drawing.Point(62, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 20);
		this.label1.TabIndex = 33;
		this.label1.Text = "OPTIONS";
		this.panelsearchbar.BackColor = System.Drawing.Color.FromArgb(218, 41, 28);
		this.panelsearchbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelsearchbar.Controls.Add(this.txtsearch);
		this.panelsearchbar.Controls.Add(this.panelrefresh);
		this.panelsearchbar.Location = new System.Drawing.Point(255, 105);
		this.panelsearchbar.Name = "panelsearchbar";
		this.panelsearchbar.Size = new System.Drawing.Size(353, 32);
		this.panelsearchbar.TabIndex = 30;
		this.txtsearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtsearch.BackColor = System.Drawing.Color.FromArgb(218, 41, 28);
		this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtsearch.ForeColor = System.Drawing.SystemColors.Control;
		this.txtsearch.Location = new System.Drawing.Point(5, 6);
		this.txtsearch.Name = "txtsearch";
		this.txtsearch.Size = new System.Drawing.Size(311, 17);
		this.txtsearch.TabIndex = 2;
		this.txtsearch.TextChanged += new System.EventHandler(txtsearch_TextChanged_1);
		this.panelrefresh.Controls.Add(this.btn_refresh);
		this.panelrefresh.Dock = System.Windows.Forms.DockStyle.Right;
		this.panelrefresh.Location = new System.Drawing.Point(321, 0);
		this.panelrefresh.Name = "panelrefresh";
		this.panelrefresh.Size = new System.Drawing.Size(30, 30);
		this.panelrefresh.TabIndex = 31;
		this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
		this.btn_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_refresh.Image = (System.Drawing.Image)resources.GetObject("btn_refresh.Image");
		this.btn_refresh.Location = new System.Drawing.Point(3, 4);
		this.btn_refresh.Name = "btn_refresh";
		this.btn_refresh.Size = new System.Drawing.Size(26, 21);
		this.btn_refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_refresh.TabIndex = 30;
		this.btn_refresh.TabStop = false;
		this.btn_refresh.Click += new System.EventHandler(btn_refresh_Click_1);
		this.btn_refresh.MouseEnter += new System.EventHandler(btn_refresh_MouseEnter);
		this.btn_refresh.MouseLeave += new System.EventHandler(btn_refresh_MouseLeave);
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.Firebrick;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
		this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(187, 41, 28);
		this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Azure;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
		this.dataGridView1.Location = new System.Drawing.Point(185, 164);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(603, 277);
		this.dataGridView1.TabIndex = 29;
		this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick_1);
		this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellLeave);
		this.pictureBoxSearch.BackColor = System.Drawing.Color.Transparent;
		this.pictureBoxSearch.Image = (System.Drawing.Image)resources.GetObject("pictureBoxSearch.Image");
		this.pictureBoxSearch.Location = new System.Drawing.Point(185, 89);
		this.pictureBoxSearch.Name = "pictureBoxSearch";
		this.pictureBoxSearch.Padding = new System.Windows.Forms.Padding(1);
		this.pictureBoxSearch.Size = new System.Drawing.Size(64, 60);
		this.pictureBoxSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBoxSearch.TabIndex = 29;
		this.pictureBoxSearch.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Azure;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.ControlBox = false;
		base.Controls.Add(this.dataGridView1);
		base.Controls.Add(this.panelsearchbar);
		base.Controls.Add(this.pictureBoxSearch);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		base.Name = "frmViolations";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "                                      ";
		base.Load += new System.EventHandler(frmViolations_Load);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		this.panel3.ResumeLayout(false);
		this.paneladd.ResumeLayout(false);
		this.paneladd.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnAdd).EndInit();
		this.panelupdate.ResumeLayout(false);
		this.panelupdate.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnupdate).EndInit();
		this.paneldelete.ResumeLayout(false);
		this.paneldelete.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btndelete).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxOptions).EndInit();
		this.panelsearchbar.ResumeLayout(false);
		this.panelsearchbar.PerformLayout();
		this.panelrefresh.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.btn_refresh).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxSearch).EndInit();
		base.ResumeLayout(false);
	}
}
