using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmAccounts : Form
{
	private string username;

	private Class1 accounts = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private int row;

	private IContainer components;

	private PictureBox btn_close;

	private PictureBox btn_minimize;

	private Label labelAccountsManagement;

	private Panel panelheader;

	private DataGridView dataGridView1;

	private PictureBox pictureBox2;

	private PictureBox pictureBoxSearch;

	private Panel panelsearchbar;

	private PictureBox btn_refresh;

	private TextBox txtsearch;

	private Panel panel2;

	private PictureBox pictureBoxOptions;

	private Label labeloptions;

	private Panel paneloptions;

	private Panel panel1;

	private PictureBox btnupdate;

	private Label label1;

	private Panel panel5;

	private PictureBox btndelete;

	private Label label4;

	private Panel panel3;

	private PictureBox btnAdd;

	private Label label2;

	public frmAccounts(string username)
	{
		InitializeComponent();
		this.username = username;
		this.Draggable(enable: true);
		Text = " ";
		base.FormBorderStyle = FormBorderStyle.Sizable;
	}

	private void frmAccounts_Load(object sender, EventArgs e)
	{
		LoadAccounts();
	}

	public void LoadAccounts()
	{
		try
		{
			DataTable dt = accounts.GetData("SELECT username, password, usertype, status, createdby, datecreated FROM tblaccounts WHERE username <> '" + username + "'ORDER BY username");
			dataGridView1.DataSource = dt;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on accounts load", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtsearch_TextChanged(object sender, EventArgs e)
	{
		try
		{
			DataTable dt = accounts.GetData("SELECT username, password, usertype, status, createdby, datecreated FROM tblaccounts WHERE username <> '" + username + "' AND (username LIKE '%" + txtsearch.Text + "%' OR usertype LIKE '%" + txtsearch.Text + "%') ORDER BY username");
			dataGridView1.DataSource = dt;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on search", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btn_refresh_Click(object sender, EventArgs e)
	{
		LoadAccounts();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		new frmNewaccount(this, username).Show();
	}

	private void btnupdate_Click(object sender, EventArgs e)
	{
		string editusername = dataGridView1.Rows[row].Cells[0].Value.ToString();
		string editpassword = dataGridView1.Rows[row].Cells[1].Value.ToString();
		string editype = dataGridView1.Rows[row].Cells[2].Value.ToString();
		string editstatus = dataGridView1.Rows[row].Cells[3].Value.ToString();
		new frmUpdateAccount(this, editusername, editpassword, editype, editstatus, username).Show();
	}

	private void btndelete_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("Are you sure you want to delete this account", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
		{
			return;
		}
		string selecteduser = dataGridView1.Rows[row].Cells[0].Value.ToString();
		try
		{
			accounts.executeSQL("DELETE FROM tblaccounts WHERE username = '" + selecteduser + "'");
			if (accounts.rowAffected > 0)
			{
				accounts.executeSQL("INSERT INTO tbllogs (datelog, timelog, action, module, ID, performedby) VALUES ('" + DateTime.Now.ToShortDateString() + "', '" + DateTime.Now.ToShortTimeString() + "', 'Delete', 'Accounts Management', '" + selecteduser + "', '" + username + "')");
				MessageBox.Show("Account Deleted", "Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				LoadAccounts();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on delete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

	private void btnrefresh_Click(object sender, EventArgs e)
	{
		frmAccounts_Load(sender, e);
	}

	private void btn_close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_minimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
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

	private void btn_refresh_MouseHover(object sender, EventArgs e)
	{
		btn_refresh.BackColor = Color.LightBlue;
	}

	private void btn_refresh_MouseLeave_1(object sender, EventArgs e)
	{
		btn_refresh.BackColor = Color.Transparent;
	}

	private void btnAdd_MouseHover(object sender, EventArgs e)
	{
		btnAdd.BackColor = Color.LimeGreen;
	}

	private void btnAdd_MouseLeave(object sender, EventArgs e)
	{
		btnAdd.BackColor = Color.Azure;
	}

	private void btnupdate_MouseHover(object sender, EventArgs e)
	{
		btnupdate.BackColor = Color.FromArgb(0, 78, 168);
	}

	private void btnupdate_MouseLeave(object sender, EventArgs e)
	{
		btnupdate.BackColor = Color.Azure;
	}

	private void btndelete_MouseHover(object sender, EventArgs e)
	{
		btndelete.BackColor = Color.Red;
	}

	private void btndelete_MouseLeave(object sender, EventArgs e)
	{
		btndelete.BackColor = Color.Azure;
	}

	private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
	{
		dataGridView1.Rows[e.RowIndex].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
		dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmAccounts));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.labelAccountsManagement = new System.Windows.Forms.Label();
		this.panelheader = new System.Windows.Forms.Panel();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.pictureBoxSearch = new System.Windows.Forms.PictureBox();
		this.panelsearchbar = new System.Windows.Forms.Panel();
		this.txtsearch = new System.Windows.Forms.TextBox();
		this.btn_refresh = new System.Windows.Forms.PictureBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.pictureBoxOptions = new System.Windows.Forms.PictureBox();
		this.labeloptions = new System.Windows.Forms.Label();
		this.paneloptions = new System.Windows.Forms.Panel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnupdate = new System.Windows.Forms.PictureBox();
		this.label1 = new System.Windows.Forms.Label();
		this.panel5 = new System.Windows.Forms.Panel();
		this.btndelete = new System.Windows.Forms.PictureBox();
		this.label4 = new System.Windows.Forms.Label();
		this.panel3 = new System.Windows.Forms.Panel();
		this.btnAdd = new System.Windows.Forms.PictureBox();
		this.label2 = new System.Windows.Forms.Label();
		this.panelheader.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxSearch).BeginInit();
		this.panelsearchbar.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_refresh).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxOptions).BeginInit();
		this.paneloptions.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnupdate).BeginInit();
		this.panel5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btndelete).BeginInit();
		this.panel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnAdd).BeginInit();
		base.SuspendLayout();
		this.labelAccountsManagement.AutoSize = true;
		this.labelAccountsManagement.BackColor = System.Drawing.Color.Transparent;
		this.labelAccountsManagement.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelAccountsManagement.ForeColor = System.Drawing.Color.Azure;
		this.labelAccountsManagement.Location = new System.Drawing.Point(91, 26);
		this.labelAccountsManagement.Name = "labelAccountsManagement";
		this.labelAccountsManagement.Size = new System.Drawing.Size(284, 26);
		this.labelAccountsManagement.TabIndex = 26;
		this.labelAccountsManagement.Text = "ACCOUNTS MANAGEMENT";
		this.panelheader.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panelheader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panelheader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelheader.Controls.Add(this.pictureBox2);
		this.panelheader.Controls.Add(this.labelAccountsManagement);
		this.panelheader.Controls.Add(this.btn_minimize);
		this.panelheader.Controls.Add(this.btn_close);
		this.panelheader.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelheader.Location = new System.Drawing.Point(0, 0);
		this.panelheader.Name = "panelheader";
		this.panelheader.Size = new System.Drawing.Size(880, 79);
		this.panelheader.TabIndex = 32;
		this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.InitialImage = null;
		this.pictureBox2.Location = new System.Drawing.Point(2, 1);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Padding = new System.Windows.Forms.Padding(3);
		this.pictureBox2.Size = new System.Drawing.Size(83, 74);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 27;
		this.pictureBox2.TabStop = false;
		this.btn_minimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_minimize.BackColor = System.Drawing.Color.Transparent;
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(808, 11);
		this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_minimize.Name = "btn_minimize";
		this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
		this.btn_minimize.Size = new System.Drawing.Size(28, 25);
		this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_minimize.TabIndex = 24;
		this.btn_minimize.TabStop = false;
		this.btn_minimize.Click += new System.EventHandler(btn_minimize_Click);
		this.btn_minimize.MouseLeave += new System.EventHandler(btn_minimize_MouseLeave);
		this.btn_minimize.MouseHover += new System.EventHandler(btn_minimize_MouseHover);
		this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_close.BackColor = System.Drawing.Color.Transparent;
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(840, 11);
		this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_close.Name = "btn_close";
		this.btn_close.Padding = new System.Windows.Forms.Padding(3);
		this.btn_close.Size = new System.Drawing.Size(28, 25);
		this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_close.TabIndex = 23;
		this.btn_close.TabStop = false;
		this.btn_close.Click += new System.EventHandler(btn_close_Click);
		this.btn_close.MouseLeave += new System.EventHandler(btn_close_MouseLeave);
		this.btn_close.MouseHover += new System.EventHandler(btn_close_MouseHover);
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.Firebrick;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Spiegel Regular", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
		this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(0, 78, 168);
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
		this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Spiegel Regular", 9f);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
		this.dataGridView1.Location = new System.Drawing.Point(127, 167);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(740, 330);
		this.dataGridView1.TabIndex = 0;
		this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);
		this.dataGridView1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellLeave);
		this.pictureBoxSearch.BackColor = System.Drawing.Color.Transparent;
		this.pictureBoxSearch.Image = (System.Drawing.Image)resources.GetObject("pictureBoxSearch.Image");
		this.pictureBoxSearch.Location = new System.Drawing.Point(127, 93);
		this.pictureBoxSearch.Name = "pictureBoxSearch";
		this.pictureBoxSearch.Padding = new System.Windows.Forms.Padding(3);
		this.pictureBoxSearch.Size = new System.Drawing.Size(65, 62);
		this.pictureBoxSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBoxSearch.TabIndex = 0;
		this.pictureBoxSearch.TabStop = false;
		this.panelsearchbar.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panelsearchbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelsearchbar.Controls.Add(this.txtsearch);
		this.panelsearchbar.Controls.Add(this.btn_refresh);
		this.panelsearchbar.Location = new System.Drawing.Point(198, 110);
		this.panelsearchbar.Name = "panelsearchbar";
		this.panelsearchbar.Size = new System.Drawing.Size(392, 32);
		this.panelsearchbar.TabIndex = 28;
		this.txtsearch.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtsearch.ForeColor = System.Drawing.Color.Azure;
		this.txtsearch.Location = new System.Drawing.Point(5, 6);
		this.txtsearch.Name = "txtsearch";
		this.txtsearch.Size = new System.Drawing.Size(348, 17);
		this.txtsearch.TabIndex = 2;
		this.txtsearch.TextChanged += new System.EventHandler(txtsearch_TextChanged);
		this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
		this.btn_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_refresh.Image = (System.Drawing.Image)resources.GetObject("btn_refresh.Image");
		this.btn_refresh.Location = new System.Drawing.Point(360, 4);
		this.btn_refresh.Name = "btn_refresh";
		this.btn_refresh.Size = new System.Drawing.Size(26, 21);
		this.btn_refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_refresh.TabIndex = 30;
		this.btn_refresh.TabStop = false;
		this.btn_refresh.Click += new System.EventHandler(btn_refresh_Click);
		this.btn_refresh.MouseLeave += new System.EventHandler(btn_refresh_MouseLeave_1);
		this.btn_refresh.MouseHover += new System.EventHandler(btn_refresh_MouseHover);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(218, 41, 28);
		this.panel2.Controls.Add(this.pictureBoxOptions);
		this.panel2.Controls.Add(this.labeloptions);
		this.panel2.Location = new System.Drawing.Point(12, 93);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(81, 69);
		this.panel2.TabIndex = 37;
		this.pictureBoxOptions.BackColor = System.Drawing.Color.FromArgb(187, 41, 28);
		this.pictureBoxOptions.Dock = System.Windows.Forms.DockStyle.Top;
		this.pictureBoxOptions.Image = (System.Drawing.Image)resources.GetObject("pictureBoxOptions.Image");
		this.pictureBoxOptions.Location = new System.Drawing.Point(0, 0);
		this.pictureBoxOptions.Name = "pictureBoxOptions";
		this.pictureBoxOptions.Padding = new System.Windows.Forms.Padding(22, 7, 22, 7);
		this.pictureBoxOptions.Size = new System.Drawing.Size(81, 49);
		this.pictureBoxOptions.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBoxOptions.TabIndex = 4;
		this.pictureBoxOptions.TabStop = false;
		this.labeloptions.AutoSize = true;
		this.labeloptions.BackColor = System.Drawing.Color.Transparent;
		this.labeloptions.Font = new System.Drawing.Font("Spiegel Bold", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labeloptions.ForeColor = System.Drawing.Color.Snow;
		this.labeloptions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labeloptions.Location = new System.Drawing.Point(10, 49);
		this.labeloptions.Name = "labeloptions";
		this.labeloptions.Size = new System.Drawing.Size(66, 18);
		this.labeloptions.TabIndex = 24;
		this.labeloptions.Text = "OPTIONS";
		this.labeloptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.paneloptions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.paneloptions.BackColor = System.Drawing.Color.FromArgb(218, 41, 28);
		this.paneloptions.Controls.Add(this.panel1);
		this.paneloptions.Controls.Add(this.panel5);
		this.paneloptions.Controls.Add(this.panel3);
		this.paneloptions.Location = new System.Drawing.Point(12, 167);
		this.paneloptions.Name = "paneloptions";
		this.paneloptions.Size = new System.Drawing.Size(81, 330);
		this.paneloptions.TabIndex = 36;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(89, 133, 225);
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.btnupdate);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Location = new System.Drawing.Point(13, 83);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(56, 63);
		this.panel1.TabIndex = 35;
		this.btnupdate.BackColor = System.Drawing.Color.Azure;
		this.btnupdate.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnupdate.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnupdate.Image = (System.Drawing.Image)resources.GetObject("btnupdate.Image");
		this.btnupdate.Location = new System.Drawing.Point(0, 0);
		this.btnupdate.Name = "btnupdate";
		this.btnupdate.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
		this.btnupdate.Size = new System.Drawing.Size(54, 45);
		this.btnupdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btnupdate.TabIndex = 1;
		this.btnupdate.TabStop = false;
		this.btnupdate.Click += new System.EventHandler(btnupdate_Click);
		this.btnupdate.MouseLeave += new System.EventHandler(btnupdate_MouseLeave);
		this.btnupdate.MouseHover += new System.EventHandler(btnupdate_MouseHover);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.label1.AutoSize = true;
		this.label1.Cursor = System.Windows.Forms.Cursors.Default;
		this.label1.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Azure;
		this.label1.Location = new System.Drawing.Point(5, 45);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(47, 15);
		this.label1.TabIndex = 0;
		this.label1.Text = "UPDATE";
		this.panel5.BackColor = System.Drawing.Color.FromArgb(234, 51, 35);
		this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel5.Controls.Add(this.btndelete);
		this.panel5.Controls.Add(this.label4);
		this.panel5.Location = new System.Drawing.Point(14, 253);
		this.panel5.Name = "panel5";
		this.panel5.Size = new System.Drawing.Size(54, 63);
		this.panel5.TabIndex = 37;
		this.btndelete.BackColor = System.Drawing.Color.Azure;
		this.btndelete.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btndelete.Dock = System.Windows.Forms.DockStyle.Top;
		this.btndelete.Image = (System.Drawing.Image)resources.GetObject("btndelete.Image");
		this.btndelete.Location = new System.Drawing.Point(0, 0);
		this.btndelete.Name = "btndelete";
		this.btndelete.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
		this.btndelete.Size = new System.Drawing.Size(52, 43);
		this.btndelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btndelete.TabIndex = 1;
		this.btndelete.TabStop = false;
		this.btndelete.Click += new System.EventHandler(btndelete_Click);
		this.btndelete.MouseLeave += new System.EventHandler(btndelete_MouseLeave);
		this.btndelete.MouseHover += new System.EventHandler(btndelete_MouseHover);
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.label4.AutoSize = true;
		this.label4.Cursor = System.Windows.Forms.Cursors.Default;
		this.label4.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.Azure;
		this.label4.Location = new System.Drawing.Point(5, 44);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(43, 15);
		this.label4.TabIndex = 0;
		this.label4.Text = "DELETE";
		this.panel3.BackColor = System.Drawing.Color.LimeGreen;
		this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel3.Controls.Add(this.btnAdd);
		this.panel3.Controls.Add(this.label2);
		this.panel3.Location = new System.Drawing.Point(12, 15);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(57, 62);
		this.panel3.TabIndex = 36;
		this.btnAdd.BackColor = System.Drawing.Color.Azure;
		this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnAdd.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnAdd.Image = (System.Drawing.Image)resources.GetObject("btnAdd.Image");
		this.btnAdd.Location = new System.Drawing.Point(0, 0);
		this.btnAdd.Name = "btnAdd";
		this.btnAdd.Padding = new System.Windows.Forms.Padding(50, 0, 50, 0);
		this.btnAdd.Size = new System.Drawing.Size(55, 46);
		this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btnAdd.TabIndex = 1;
		this.btnAdd.TabStop = false;
		this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
		this.btnAdd.MouseLeave += new System.EventHandler(btnAdd_MouseLeave);
		this.btnAdd.MouseHover += new System.EventHandler(btnAdd_MouseHover);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.label2.AutoSize = true;
		this.label2.Cursor = System.Windows.Forms.Cursors.Default;
		this.label2.Font = new System.Drawing.Font("Spiegel Bold", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.Azure;
		this.label2.Location = new System.Drawing.Point(14, 46);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(28, 15);
		this.label2.TabIndex = 0;
		this.label2.Text = "ADD";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Azure;
		base.ClientSize = new System.Drawing.Size(880, 509);
		base.ControlBox = false;
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.paneloptions);
		base.Controls.Add(this.dataGridView1);
		base.Controls.Add(this.panelheader);
		base.Controls.Add(this.pictureBoxSearch);
		base.Controls.Add(this.panelsearchbar);
		base.MaximizeBox = false;
		base.Name = "frmAccounts";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Load += new System.EventHandler(frmAccounts_Load);
		this.panelheader.ResumeLayout(false);
		this.panelheader.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxSearch).EndInit();
		this.panelsearchbar.ResumeLayout(false);
		this.panelsearchbar.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_refresh).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBoxOptions).EndInit();
		this.paneloptions.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnupdate).EndInit();
		this.panel5.ResumeLayout(false);
		this.panel5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btndelete).EndInit();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnAdd).EndInit();
		base.ResumeLayout(false);
	}
}
