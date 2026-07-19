using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using IncidentReportSystem.Properties;

namespace IncidentReportSystem;

public class frmMain : Form
{
	private string username;

	private string usertype;

	private bool chkbtnmaintenance;

	private bool chkbtnreports;

	private bool chkbtnabout;

	private IContainer components;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel toolStripStatusLabel1;

	private ToolStripStatusLabel toolStripStatusLabel2;

	private PictureBox btn_minimize;

	private PictureBox btn_close;

	private PictureBox btn_fullwindow;

	private Panel panelfrmMain;

	private Label labelIncidentReportSystem;

	private Panel panelmenu;

	private Panel panelmaintenance;

	private Button btnmaintenance;

	private PictureBox pbmaintenance;

	private Panel panellogout;

	private Button btnlogout;

	private PictureBox pictureBox5;

	private Panel panelabout;

	private Button btnabout;

	private PictureBox pbabout;

	private Panel panelreports;

	private Button btnreports;

	private PictureBox pbreports;

	private ToolStripMenuItem toolStripMenuItemAccounts;

	private ToolStripMenuItem toolStripMenuItemStudents;

	private ToolStripMenuItem violationsToolStripMenuItem;

	private ToolStripMenuItem casesToolStripMenuItem;

	private ToolStripMenuItem eventsToolStripMenuItem;

	private ToolStripMenuItem ticketsToolStripMenuItem;

	private ContextMenuStrip contextMenuStrip1;

	public frmMain(string username, string usertype)
	{
		InitializeComponent();
		this.username = username;
		this.usertype = usertype;
	}

	private void btnlogout_Click(object sender, EventArgs e)
	{
		Close();
		new frmLogin().Show();
	}

	private void toolStripMenuItemAccounts_Click(object sender, EventArgs e)
	{
		frmAccounts obj = new frmAccounts(username);
		obj.MdiParent = this;
		obj.Show();
	}

	private void toolStripMenuItemStudents_Click(object sender, EventArgs e)
	{
		frmStudents obj = new frmStudents(username);
		obj.MdiParent = this;
		obj.Show();
	}

	private void violationsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmViolations obj = new frmViolations(username);
		obj.MdiParent = this;
		obj.Show();
	}

	private void casesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		frmCaseManagement obj = new frmCaseManagement(username);
		obj.MdiParent = this;
		obj.Show();
	}

	private void frmMain_Load(object sender, EventArgs e)
	{
		toolStripStatusLabel1.Text = "Username: " + username;
		toolStripStatusLabel2.Text = "User type: " + usertype;
		if (usertype == "ADMINISTRATOR")
		{
			toolStripMenuItemAccounts.Visible = true;
			toolStripMenuItemStudents.Visible = true;
			eventsToolStripMenuItem.Visible = true;
			ticketsToolStripMenuItem.Visible = true;
		}
		else if (usertype == "BRANCH ADMINISTRATOR")
		{
			toolStripMenuItemAccounts.Visible = false;
			toolStripMenuItemStudents.Visible = false;
			eventsToolStripMenuItem.Visible = true;
			ticketsToolStripMenuItem.Visible = true;
		}
		else
		{
			toolStripMenuItemAccounts.Visible = false;
			toolStripMenuItemStudents.Visible = false;
			eventsToolStripMenuItem.Visible = false;
			ticketsToolStripMenuItem.Visible = true;
		}
	}

	private void btn_close_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_minimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void btn_fullwindow_Click(object sender, EventArgs e)
	{
		if (base.WindowState == FormWindowState.Maximized)
		{
			base.FormBorderStyle = FormBorderStyle.Fixed3D;
			Text = " ";
			this.Draggable(enable: true);
			base.WindowState = FormWindowState.Normal;
			btn_fullwindow.Image = Resources.icon_full_screen_frm;
		}
		else
		{
			this.Draggable(enable: false);
			base.WindowState = FormWindowState.Maximized;
			base.FormBorderStyle = FormBorderStyle.None;
			btn_fullwindow.Image = Resources.icon_windowmode_frm;
		}
	}

	private void btn_minimize_MouseHover(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.SteelBlue;
		btn_minimize.BorderStyle = BorderStyle.FixedSingle;
	}

	private void btn_minimize_MouseLeave(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.Transparent;
		btn_minimize.BorderStyle = BorderStyle.None;
	}

	private void btn_fullwindow_MouseHover(object sender, EventArgs e)
	{
		btn_fullwindow.BackColor = Color.SteelBlue;
		btn_fullwindow.BorderStyle = BorderStyle.FixedSingle;
	}

	private void btn_fullwindow_MouseLeave(object sender, EventArgs e)
	{
		btn_fullwindow.BackColor = Color.Transparent;
		btn_fullwindow.BorderStyle = BorderStyle.None;
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

	private void BackColorpaste(Control pb, Control btn, Control panel)
	{
		pb.BackColor = Color.FromArgb(0, 78, 168);
		btn.BackColor = Color.FromArgb(0, 78, 168);
		((Panel)panel).BorderStyle = BorderStyle.None;
	}

	private void BackColorLeave(Control pb, Control btn, Control panel)
	{
		pb.BackColor = SystemColors.MenuHighlight;
		btn.BackColor = SystemColors.MenuHighlight;
		((Panel)panel).BorderStyle = BorderStyle.FixedSingle;
	}

	private void btnmaintenance_Click(object sender, EventArgs e)
	{
		chkbtnmaintenance = !chkbtnmaintenance;
		BackColorpaste(pbmaintenance, btnmaintenance, panelmaintenance);
		contextMenuStrip1.Show(panelmaintenance, new Point(0, panelmaintenance.Height));
	}

	private void btnmaintenance_MouseHover(object sender, EventArgs e)
	{
		BackColorpaste(pbmaintenance, btnmaintenance, panelmaintenance);
	}

	private void btnmaintenance_MouseLeave(object sender, EventArgs e)
	{
		if (!chkbtnmaintenance)
		{
			BackColorLeave(pbmaintenance, btnmaintenance, panelmaintenance);
		}
	}

	private void btnreports_Click(object sender, EventArgs e)
	{
		chkbtnreports = !chkbtnreports;
		BackColorpaste(pbreports, btnreports, panelreports);
	}

	private void btnreports_MouseHover(object sender, EventArgs e)
	{
		BackColorpaste(pbreports, btnreports, panelreports);
	}

	private void btnreports_MouseLeave(object sender, EventArgs e)
	{
		if (!chkbtnreports)
		{
			BackColorLeave(pbreports, btnreports, panelreports);
		}
	}

	private void btnabout_Click(object sender, EventArgs e)
	{
		chkbtnabout = !chkbtnabout;
		BackColorpaste(pbabout, btnabout, panelabout);
	}

	private void btnabout_MouseHover(object sender, EventArgs e)
	{
		BackColorpaste(pbabout, btnabout, panelabout);
	}

	private void btnabout_MouseLeave(object sender, EventArgs e)
	{
		if (!chkbtnabout)
		{
			BackColorLeave(pbabout, btnabout, panelabout);
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncidentReportSystem.frmMain));
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
		this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
		this.btn_minimize = new System.Windows.Forms.PictureBox();
		this.btn_close = new System.Windows.Forms.PictureBox();
		this.btn_fullwindow = new System.Windows.Forms.PictureBox();
		this.panelfrmMain = new System.Windows.Forms.Panel();
		this.labelIncidentReportSystem = new System.Windows.Forms.Label();
		this.panelmenu = new System.Windows.Forms.Panel();
		this.panelabout = new System.Windows.Forms.Panel();
		this.btnabout = new System.Windows.Forms.Button();
		this.pbabout = new System.Windows.Forms.PictureBox();
		this.panelreports = new System.Windows.Forms.Panel();
		this.btnreports = new System.Windows.Forms.Button();
		this.pbreports = new System.Windows.Forms.PictureBox();
		this.panellogout = new System.Windows.Forms.Panel();
		this.btnlogout = new System.Windows.Forms.Button();
		this.pictureBox5 = new System.Windows.Forms.PictureBox();
		this.panelmaintenance = new System.Windows.Forms.Panel();
		this.btnmaintenance = new System.Windows.Forms.Button();
		this.pbmaintenance = new System.Windows.Forms.PictureBox();
		this.toolStripMenuItemAccounts = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItemStudents = new System.Windows.Forms.ToolStripMenuItem();
		this.violationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.casesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ticketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.statusStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btn_fullwindow).BeginInit();
		this.panelfrmMain.SuspendLayout();
		this.panelmenu.SuspendLayout();
		this.panelabout.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbabout).BeginInit();
		this.panelreports.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbreports).BeginInit();
		this.panellogout.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).BeginInit();
		this.panelmaintenance.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbmaintenance).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.statusStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.toolStripStatusLabel1, this.toolStripStatusLabel2 });
		this.statusStrip1.Location = new System.Drawing.Point(0, 489);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(870, 28);
		this.statusStrip1.TabIndex = 2;
		this.statusStrip1.Text = "statusStrip1";
		this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(0, 52, 112);
		this.toolStripStatusLabel1.Font = new System.Drawing.Font("Beaufort for LOL", 8.999999f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Snow;
		this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(0, -1, 0, 0);
		this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
		this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(7);
		this.toolStripStatusLabel1.Size = new System.Drawing.Size(135, 29);
		this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
		this.toolStripStatusLabel2.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.toolStripStatusLabel2.Font = new System.Drawing.Font("Beaufort for LOL", 8.999999f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Snow;
		this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(0, -1, 0, 0);
		this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
		this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(7);
		this.toolStripStatusLabel2.Size = new System.Drawing.Size(135, 29);
		this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
		this.btn_minimize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_minimize.Image = (System.Drawing.Image)resources.GetObject("btn_minimize.Image");
		this.btn_minimize.Location = new System.Drawing.Point(763, 16);
		this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_minimize.Name = "btn_minimize";
		this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
		this.btn_minimize.Size = new System.Drawing.Size(28, 25);
		this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_minimize.TabIndex = 21;
		this.btn_minimize.TabStop = false;
		this.btn_minimize.Click += new System.EventHandler(btn_minimize_Click);
		this.btn_minimize.MouseLeave += new System.EventHandler(btn_minimize_MouseLeave);
		this.btn_minimize.MouseHover += new System.EventHandler(btn_minimize_MouseHover);
		this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_close.BackColor = System.Drawing.Color.Transparent;
		this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_close.Image = (System.Drawing.Image)resources.GetObject("btn_close.Image");
		this.btn_close.Location = new System.Drawing.Point(827, 16);
		this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_close.Name = "btn_close";
		this.btn_close.Padding = new System.Windows.Forms.Padding(3);
		this.btn_close.Size = new System.Drawing.Size(28, 25);
		this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_close.TabIndex = 20;
		this.btn_close.TabStop = false;
		this.btn_close.Click += new System.EventHandler(btn_close_Click);
		this.btn_close.MouseLeave += new System.EventHandler(btn_close_MouseLeave);
		this.btn_close.MouseHover += new System.EventHandler(btn_close_MouseHover);
		this.btn_fullwindow.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btn_fullwindow.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btn_fullwindow.Image = (System.Drawing.Image)resources.GetObject("btn_fullwindow.Image");
		this.btn_fullwindow.Location = new System.Drawing.Point(795, 16);
		this.btn_fullwindow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		this.btn_fullwindow.Name = "btn_fullwindow";
		this.btn_fullwindow.Padding = new System.Windows.Forms.Padding(4);
		this.btn_fullwindow.Size = new System.Drawing.Size(28, 25);
		this.btn_fullwindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.btn_fullwindow.TabIndex = 23;
		this.btn_fullwindow.TabStop = false;
		this.btn_fullwindow.Click += new System.EventHandler(btn_fullwindow_Click);
		this.btn_fullwindow.MouseLeave += new System.EventHandler(btn_fullwindow_MouseLeave);
		this.btn_fullwindow.MouseHover += new System.EventHandler(btn_fullwindow_MouseHover);
		this.panelfrmMain.BackColor = System.Drawing.Color.FromArgb(0, 78, 168);
		this.panelfrmMain.Controls.Add(this.labelIncidentReportSystem);
		this.panelfrmMain.Controls.Add(this.btn_fullwindow);
		this.panelfrmMain.Controls.Add(this.btn_close);
		this.panelfrmMain.Controls.Add(this.btn_minimize);
		this.panelfrmMain.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelfrmMain.Location = new System.Drawing.Point(0, 0);
		this.panelfrmMain.Name = "panelfrmMain";
		this.panelfrmMain.Padding = new System.Windows.Forms.Padding(4);
		this.panelfrmMain.Size = new System.Drawing.Size(870, 85);
		this.panelfrmMain.TabIndex = 4;
		this.labelIncidentReportSystem.AutoSize = true;
		this.labelIncidentReportSystem.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelIncidentReportSystem.ForeColor = System.Drawing.SystemColors.Control;
		this.labelIncidentReportSystem.Location = new System.Drawing.Point(12, 25);
		this.labelIncidentReportSystem.Name = "labelIncidentReportSystem";
		this.labelIncidentReportSystem.Size = new System.Drawing.Size(509, 30);
		this.labelIncidentReportSystem.TabIndex = 25;
		this.labelIncidentReportSystem.Text = "INCIDENT REPORT MANAGEMENT SYSTEM";
		this.panelmenu.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.panelmenu.Controls.Add(this.panelabout);
		this.panelmenu.Controls.Add(this.panelreports);
		this.panelmenu.Controls.Add(this.panellogout);
		this.panelmenu.Controls.Add(this.panelmaintenance);
		this.panelmenu.Dock = System.Windows.Forms.DockStyle.Top;
		this.panelmenu.Location = new System.Drawing.Point(0, 85);
		this.panelmenu.Name = "panelmenu";
		this.panelmenu.Size = new System.Drawing.Size(870, 39);
		this.panelmenu.TabIndex = 9;
		this.panelabout.BackColor = System.Drawing.Color.Transparent;
		this.panelabout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelabout.Controls.Add(this.btnabout);
		this.panelabout.Controls.Add(this.pbabout);
		this.panelabout.Location = new System.Drawing.Point(276, 0);
		this.panelabout.Name = "panelabout";
		this.panelabout.Size = new System.Drawing.Size(107, 39);
		this.panelabout.TabIndex = 6;
		this.btnabout.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.btnabout.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnabout.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnabout.FlatAppearance.BorderSize = 0;
		this.btnabout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnabout.Font = new System.Drawing.Font("Spiegel Regular", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnabout.ForeColor = System.Drawing.SystemColors.Control;
		this.btnabout.Location = new System.Drawing.Point(38, 0);
		this.btnabout.Name = "btnabout";
		this.btnabout.Size = new System.Drawing.Size(67, 37);
		this.btnabout.TabIndex = 5;
		this.btnabout.Text = "&About";
		this.btnabout.UseVisualStyleBackColor = false;
		this.btnabout.Click += new System.EventHandler(btnabout_Click);
		this.btnabout.MouseLeave += new System.EventHandler(btnabout_MouseLeave);
		this.btnabout.MouseHover += new System.EventHandler(btnabout_MouseHover);
		this.pbabout.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.pbabout.Dock = System.Windows.Forms.DockStyle.Left;
		this.pbabout.Image = (System.Drawing.Image)resources.GetObject("pbabout.Image");
		this.pbabout.Location = new System.Drawing.Point(0, 0);
		this.pbabout.Name = "pbabout";
		this.pbabout.Padding = new System.Windows.Forms.Padding(6);
		this.pbabout.Size = new System.Drawing.Size(38, 37);
		this.pbabout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pbabout.TabIndex = 0;
		this.pbabout.TabStop = false;
		this.panelreports.BackColor = System.Drawing.Color.Transparent;
		this.panelreports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelreports.Controls.Add(this.btnreports);
		this.panelreports.Controls.Add(this.pbreports);
		this.panelreports.Location = new System.Drawing.Point(155, 0);
		this.panelreports.Name = "panelreports";
		this.panelreports.Size = new System.Drawing.Size(121, 39);
		this.panelreports.TabIndex = 7;
		this.btnreports.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.btnreports.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnreports.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnreports.FlatAppearance.BorderSize = 0;
		this.btnreports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnreports.Font = new System.Drawing.Font("Spiegel Regular", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnreports.ForeColor = System.Drawing.SystemColors.Control;
		this.btnreports.Location = new System.Drawing.Point(38, 0);
		this.btnreports.Name = "btnreports";
		this.btnreports.Size = new System.Drawing.Size(81, 37);
		this.btnreports.TabIndex = 5;
		this.btnreports.Text = "&Reports";
		this.btnreports.UseVisualStyleBackColor = false;
		this.btnreports.Click += new System.EventHandler(btnreports_Click);
		this.btnreports.MouseLeave += new System.EventHandler(btnreports_MouseLeave);
		this.btnreports.MouseHover += new System.EventHandler(btnreports_MouseHover);
		this.pbreports.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.pbreports.Dock = System.Windows.Forms.DockStyle.Left;
		this.pbreports.Image = (System.Drawing.Image)resources.GetObject("pbreports.Image");
		this.pbreports.Location = new System.Drawing.Point(0, 0);
		this.pbreports.Name = "pbreports";
		this.pbreports.Padding = new System.Windows.Forms.Padding(6);
		this.pbreports.Size = new System.Drawing.Size(38, 37);
		this.pbreports.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pbreports.TabIndex = 0;
		this.pbreports.TabStop = false;
		this.panellogout.BackColor = System.Drawing.Color.Transparent;
		this.panellogout.Controls.Add(this.btnlogout);
		this.panellogout.Controls.Add(this.pictureBox5);
		this.panellogout.Dock = System.Windows.Forms.DockStyle.Right;
		this.panellogout.Location = new System.Drawing.Point(763, 0);
		this.panellogout.Margin = new System.Windows.Forms.Padding(0);
		this.panellogout.Name = "panellogout";
		this.panellogout.Size = new System.Drawing.Size(107, 39);
		this.panellogout.TabIndex = 7;
		this.btnlogout.BackColor = System.Drawing.Color.Firebrick;
		this.btnlogout.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnlogout.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnlogout.FlatAppearance.BorderSize = 0;
		this.btnlogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnlogout.Font = new System.Drawing.Font("Spiegel Regular", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnlogout.ForeColor = System.Drawing.SystemColors.Control;
		this.btnlogout.Location = new System.Drawing.Point(38, 0);
		this.btnlogout.Margin = new System.Windows.Forms.Padding(0);
		this.btnlogout.Name = "btnlogout";
		this.btnlogout.Size = new System.Drawing.Size(69, 39);
		this.btnlogout.TabIndex = 5;
		this.btnlogout.Text = "&Logout";
		this.btnlogout.UseVisualStyleBackColor = false;
		this.btnlogout.Click += new System.EventHandler(btnlogout_Click);
		this.pictureBox5.BackColor = System.Drawing.Color.Firebrick;
		this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox5.Image = (System.Drawing.Image)resources.GetObject("pictureBox5.Image");
		this.pictureBox5.Location = new System.Drawing.Point(0, 0);
		this.pictureBox5.Name = "pictureBox5";
		this.pictureBox5.Padding = new System.Windows.Forms.Padding(7);
		this.pictureBox5.Size = new System.Drawing.Size(38, 39);
		this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox5.TabIndex = 0;
		this.pictureBox5.TabStop = false;
		this.panelmaintenance.BackColor = System.Drawing.Color.Transparent;
		this.panelmaintenance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panelmaintenance.Controls.Add(this.btnmaintenance);
		this.panelmaintenance.Controls.Add(this.pbmaintenance);
		this.panelmaintenance.Location = new System.Drawing.Point(0, 0);
		this.panelmaintenance.Name = "panelmaintenance";
		this.panelmaintenance.Size = new System.Drawing.Size(155, 39);
		this.panelmaintenance.TabIndex = 7;
		this.btnmaintenance.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.btnmaintenance.Cursor = System.Windows.Forms.Cursors.Hand;
		this.btnmaintenance.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnmaintenance.FlatAppearance.BorderSize = 0;
		this.btnmaintenance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnmaintenance.Font = new System.Drawing.Font("Spiegel Regular", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnmaintenance.ForeColor = System.Drawing.SystemColors.Control;
		this.btnmaintenance.Location = new System.Drawing.Point(38, 0);
		this.btnmaintenance.Name = "btnmaintenance";
		this.btnmaintenance.Size = new System.Drawing.Size(115, 37);
		this.btnmaintenance.TabIndex = 5;
		this.btnmaintenance.Text = "&Maintenance";
		this.btnmaintenance.UseVisualStyleBackColor = false;
		this.btnmaintenance.Click += new System.EventHandler(btnmaintenance_Click);
		this.btnmaintenance.MouseLeave += new System.EventHandler(btnmaintenance_MouseLeave);
		this.btnmaintenance.MouseHover += new System.EventHandler(btnmaintenance_MouseHover);
		this.pbmaintenance.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.pbmaintenance.Dock = System.Windows.Forms.DockStyle.Left;
		this.pbmaintenance.Image = (System.Drawing.Image)resources.GetObject("pbmaintenance.Image");
		this.pbmaintenance.Location = new System.Drawing.Point(0, 0);
		this.pbmaintenance.Name = "pbmaintenance";
		this.pbmaintenance.Padding = new System.Windows.Forms.Padding(6);
		this.pbmaintenance.Size = new System.Drawing.Size(38, 37);
		this.pbmaintenance.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pbmaintenance.TabIndex = 0;
		this.pbmaintenance.TabStop = false;
		this.toolStripMenuItemAccounts.BackColor = System.Drawing.Color.Transparent;
		this.toolStripMenuItemAccounts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.toolStripMenuItemAccounts.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripMenuItemAccounts.ForeColor = System.Drawing.Color.Azure;
		this.toolStripMenuItemAccounts.Image = (System.Drawing.Image)resources.GetObject("toolStripMenuItemAccounts.Image");
		this.toolStripMenuItemAccounts.Name = "toolStripMenuItemAccounts";
		this.toolStripMenuItemAccounts.Size = new System.Drawing.Size(130, 22);
		this.toolStripMenuItemAccounts.Text = "&Accounts";
		this.toolStripMenuItemAccounts.Click += new System.EventHandler(toolStripMenuItemAccounts_Click);
		this.toolStripMenuItemStudents.BackColor = System.Drawing.Color.Transparent;
		this.toolStripMenuItemStudents.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.toolStripMenuItemStudents.ForeColor = System.Drawing.Color.Azure;
		this.toolStripMenuItemStudents.Image = (System.Drawing.Image)resources.GetObject("toolStripMenuItemStudents.Image");
		this.toolStripMenuItemStudents.Name = "toolStripMenuItemStudents";
		this.toolStripMenuItemStudents.Size = new System.Drawing.Size(130, 22);
		this.toolStripMenuItemStudents.Text = "&Students";
		this.toolStripMenuItemStudents.Click += new System.EventHandler(toolStripMenuItemStudents_Click);
		this.violationsToolStripMenuItem.Font = new System.Drawing.Font("Spiegel Regular", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.violationsToolStripMenuItem.ForeColor = System.Drawing.Color.Azure;
		this.violationsToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("violationsToolStripMenuItem.Image");
		this.violationsToolStripMenuItem.Name = "violationsToolStripMenuItem";
		this.violationsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
		this.violationsToolStripMenuItem.Text = "&Violations";
		this.violationsToolStripMenuItem.Click += new System.EventHandler(violationsToolStripMenuItem_Click);
		this.casesToolStripMenuItem.ForeColor = System.Drawing.Color.Azure;
		this.casesToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("casesToolStripMenuItem.Image");
		this.casesToolStripMenuItem.Name = "casesToolStripMenuItem";
		this.casesToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
		this.casesToolStripMenuItem.Text = "&Cases";
		this.casesToolStripMenuItem.Click += new System.EventHandler(casesToolStripMenuItem_Click);
		this.eventsToolStripMenuItem.ForeColor = System.Drawing.Color.Azure;
		this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
		this.eventsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
		this.eventsToolStripMenuItem.Text = "&Events";
		this.ticketsToolStripMenuItem.ForeColor = System.Drawing.Color.Azure;
		this.ticketsToolStripMenuItem.Name = "ticketsToolStripMenuItem";
		this.ticketsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
		this.ticketsToolStripMenuItem.Text = "&Tickets";
		this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.HotTrack;
		this.contextMenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.toolStripMenuItemAccounts, this.toolStripMenuItemStudents, this.violationsToolStripMenuItem, this.casesToolStripMenuItem, this.eventsToolStripMenuItem, this.ticketsToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
		this.contextMenuStrip1.Size = new System.Drawing.Size(131, 136);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(870, 517);
		base.ControlBox = false;
		base.Controls.Add(this.panelmenu);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.panelfrmMain);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.IsMdiContainer = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmMain";
		this.Text = "                                                                                                                                              ";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmMain_Load);
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btn_minimize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_close).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btn_fullwindow).EndInit();
		this.panelfrmMain.ResumeLayout(false);
		this.panelfrmMain.PerformLayout();
		this.panelmenu.ResumeLayout(false);
		this.panelabout.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pbabout).EndInit();
		this.panelreports.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pbreports).EndInit();
		this.panellogout.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox5).EndInit();
		this.panelmaintenance.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pbmaintenance).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
