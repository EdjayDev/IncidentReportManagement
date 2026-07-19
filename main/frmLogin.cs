using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IndidentReportSystem.db_class;

namespace IncidentReportSystem;

public class frmLogin : Form
{
	private Class1 login = new Class1(
    DbConfig.Instance.ServerAddress,
    DbConfig.Instance.Database,
    DbConfig.Instance.Username,
    DbConfig.Instance.Password
);

	private int errorcount;

	private IContainer components;

	private TextBox txtusername;

	private Label labelusername;

	private Label labelpassword;

	private TextBox txtpassword;

	private CheckBox chkboxShow;

	private ErrorProvider errorProvider1;

	private PictureBox icon_password;

	private PictureBox icon_username;

	private PictureBox btn_minimize;

	private PictureBox btn_close;

	private Button btnlogin;

	private PictureBox logo_auemsLogin;

	private Label labelA_IncidentReport;

	private Label labelB_IncidentReport;

	private PictureBox pictureBoxbtnlogin;

	private Panel panelbtnlogin;

	private Panel panelbtnreset;

	private Button btnreset;

	private PictureBox pictureBoxbtnreset;

	public frmLogin()
	{
		InitializeComponent();
		this.Draggable(enable: true);
	}

	private void btnlogin_Click(object sender, EventArgs e)
	{
		errorProvider1.Clear();
		if (string.IsNullOrEmpty(txtusername.Text))
		{
			errorProvider1.SetError(txtusername, "Input is empty");
		}
		if (string.IsNullOrEmpty(txtpassword.Text))
		{
			errorProvider1.SetError(txtpassword, "Input is empty");
		}
		errorcount = 0;
		foreach (Control c in errorProvider1.ContainerControl.Controls)
		{
			if (!string.IsNullOrEmpty(errorProvider1.GetError(c)))
			{
				errorcount++;
			}
		}
		if (errorcount != 0)
		{
			return;
		}
		try
		{
			DataTable dt = login.GetData("SELECT * FROM tblaccounts WHERE username = '" + txtusername.Text + "' AND password = '" + txtpassword.Text + "' AND status = 'ACTIVE'");
			if (dt.Rows.Count > 0)
			{
				new frmMain(dt.Rows[0].Field<string>("username"), dt.Rows[0].Field<string>("usertype")).Show();
				Hide();
			}
			else
			{
				MessageBox.Show("Incorrect account information or account is inactive", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error on login", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void chkboxShow_CheckedChanged_1(object sender, EventArgs e)
	{
		if (chkboxShow.Checked)
		{
			txtpassword.PasswordChar = '\0';
		}
		else
		{
			txtpassword.PasswordChar = '*';
		}
	}

	private void btnreset_Click(object sender, EventArgs e)
	{
		txtusername.Clear();
		txtpassword.Clear();
		errorProvider1.Clear();
		txtusername.Focus();
	}

	private void txtpassword_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			btnlogin_Click(sender, e);
		}
	}

	private void btn_close_Click_1(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_minimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
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

	private void btn_minimize_MouseHover(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.LightGray;
		btn_minimize.BorderStyle = BorderStyle.FixedSingle;
	}

	private void btn_minimize_MouseLeave(object sender, EventArgs e)
	{
		btn_minimize.BackColor = Color.Transparent;
		btn_minimize.BorderStyle = BorderStyle.None;
	}

	private void btnlogin_MouseHover(object sender, EventArgs e)
	{
		btnlogin.BackColor = Color.DodgerBlue;
		pictureBoxbtnlogin.BackColor = Color.DodgerBlue;
	}

	private void btnlogin_MouseLeave(object sender, EventArgs e)
	{
		btnlogin.BackColor = Color.FromArgb(0, 103, 184);
		pictureBoxbtnlogin.BackColor = Color.FromArgb(0, 103, 184);
	}

	private void btnreset_MouseHover_1(object sender, EventArgs e)
	{
		btnreset.BackColor = Color.Red;
		pictureBoxbtnreset.BackColor = Color.Red;
	}

	private void btnreset_MouseLeave_1(object sender, EventArgs e)
	{
		btnreset.BackColor = Color.Firebrick;
		pictureBoxbtnreset.BackColor = Color.Firebrick;
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
            this.txtusername = new System.Windows.Forms.TextBox();
            this.labelusername = new System.Windows.Forms.Label();
            this.labelpassword = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.chkboxShow = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnlogin = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.PictureBox();
            this.btn_minimize = new System.Windows.Forms.PictureBox();
            this.icon_password = new System.Windows.Forms.PictureBox();
            this.icon_username = new System.Windows.Forms.PictureBox();
            this.logo_auemsLogin = new System.Windows.Forms.PictureBox();
            this.labelA_IncidentReport = new System.Windows.Forms.Label();
            this.labelB_IncidentReport = new System.Windows.Forms.Label();
            this.pictureBoxbtnlogin = new System.Windows.Forms.PictureBox();
            this.panelbtnlogin = new System.Windows.Forms.Panel();
            this.panelbtnreset = new System.Windows.Forms.Panel();
            this.btnreset = new System.Windows.Forms.Button();
            this.pictureBoxbtnreset = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon_password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon_username)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo_auemsLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxbtnlogin)).BeginInit();
            this.panelbtnlogin.SuspendLayout();
            this.panelbtnreset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxbtnreset)).BeginInit();
            this.SuspendLayout();
            // 
            // txtusername
            // 
            this.txtusername.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtusername.Location = new System.Drawing.Point(82, 294);
            this.txtusername.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(190, 22);
            this.txtusername.TabIndex = 0;
            // 
            // labelusername
            // 
            this.labelusername.AutoSize = true;
            this.labelusername.BackColor = System.Drawing.Color.Transparent;
            this.labelusername.Font = new System.Drawing.Font("Spiegel Bold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelusername.ForeColor = System.Drawing.SystemColors.Control;
            this.labelusername.Location = new System.Drawing.Point(80, 275);
            this.labelusername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelusername.Name = "labelusername";
            this.labelusername.Size = new System.Drawing.Size(69, 16);
            this.labelusername.TabIndex = 1;
            this.labelusername.Text = "Username:";
            // 
            // labelpassword
            // 
            this.labelpassword.AutoSize = true;
            this.labelpassword.BackColor = System.Drawing.Color.Transparent;
            this.labelpassword.Font = new System.Drawing.Font("Spiegel Bold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelpassword.ForeColor = System.Drawing.SystemColors.Control;
            this.labelpassword.Location = new System.Drawing.Point(80, 322);
            this.labelpassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelpassword.Name = "labelpassword";
            this.labelpassword.Size = new System.Drawing.Size(66, 16);
            this.labelpassword.TabIndex = 2;
            this.labelpassword.Text = "Password:";
            // 
            // txtpassword
            // 
            this.txtpassword.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpassword.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtpassword.Location = new System.Drawing.Point(82, 341);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(190, 22);
            this.txtpassword.TabIndex = 3;
            this.txtpassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpassword_KeyPress);
            // 
            // chkboxShow
            // 
            this.chkboxShow.AutoSize = true;
            this.chkboxShow.BackColor = System.Drawing.Color.Transparent;
            this.chkboxShow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkboxShow.Font = new System.Drawing.Font("Spiegel Regular", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkboxShow.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.chkboxShow.Location = new System.Drawing.Point(176, 372);
            this.chkboxShow.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chkboxShow.Name = "chkboxShow";
            this.chkboxShow.Size = new System.Drawing.Size(102, 18);
            this.chkboxShow.TabIndex = 4;
            this.chkboxShow.Text = "Show Password";
            this.chkboxShow.UseVisualStyleBackColor = false;
            this.chkboxShow.CheckedChanged += new System.EventHandler(this.chkboxShow_CheckedChanged_1);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // btnlogin
            // 
            this.btnlogin.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnlogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnlogin.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnlogin.FlatAppearance.BorderSize = 0;
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogin.Font = new System.Drawing.Font("Spiegel Bold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnlogin.Location = new System.Drawing.Point(33, 0);
            this.btnlogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(90, 27);
            this.btnlogin.TabIndex = 5;
            this.btnlogin.Text = "&Login";
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            this.btnlogin.MouseLeave += new System.EventHandler(this.btnlogin_MouseLeave);
            this.btnlogin.MouseHover += new System.EventHandler(this.btnlogin_MouseHover);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.Location = new System.Drawing.Point(275, 12);
            this.btn_close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Padding = new System.Windows.Forms.Padding(3);
            this.btn_close.Size = new System.Drawing.Size(28, 25);
            this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_close.TabIndex = 16;
            this.btn_close.TabStop = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click_1);
            this.btn_close.MouseLeave += new System.EventHandler(this.btn_close_MouseLeave);
            this.btn_close.MouseHover += new System.EventHandler(this.btn_close_MouseHover);
            // 
            // btn_minimize
            // 
            this.btn_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_minimize.BackColor = System.Drawing.Color.Transparent;
            this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_minimize.Location = new System.Drawing.Point(243, 12);
            this.btn_minimize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Padding = new System.Windows.Forms.Padding(3);
            this.btn_minimize.Size = new System.Drawing.Size(28, 25);
            this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_minimize.TabIndex = 17;
            this.btn_minimize.TabStop = false;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            this.btn_minimize.MouseLeave += new System.EventHandler(this.btn_minimize_MouseLeave);
            this.btn_minimize.MouseHover += new System.EventHandler(this.btn_minimize_MouseHover);
            // 
            // icon_password
            // 
            this.icon_password.BackColor = System.Drawing.Color.Transparent;
            this.icon_password.Location = new System.Drawing.Point(33, 322);
            this.icon_password.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.icon_password.Name = "icon_password";
            this.icon_password.Size = new System.Drawing.Size(45, 45);
            this.icon_password.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.icon_password.TabIndex = 11;
            this.icon_password.TabStop = false;
            // 
            // icon_username
            // 
            this.icon_username.BackColor = System.Drawing.Color.Transparent;
            this.icon_username.Location = new System.Drawing.Point(33, 275);
            this.icon_username.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.icon_username.Name = "icon_username";
            this.icon_username.Size = new System.Drawing.Size(45, 45);
            this.icon_username.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.icon_username.TabIndex = 12;
            this.icon_username.TabStop = false;
            // 
            // logo_auemsLogin
            // 
            this.logo_auemsLogin.BackColor = System.Drawing.Color.Transparent;
            this.logo_auemsLogin.Location = new System.Drawing.Point(77, 41);
            this.logo_auemsLogin.Name = "logo_auemsLogin";
            this.logo_auemsLogin.Padding = new System.Windows.Forms.Padding(3);
            this.logo_auemsLogin.Size = new System.Drawing.Size(170, 156);
            this.logo_auemsLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo_auemsLogin.TabIndex = 18;
            this.logo_auemsLogin.TabStop = false;
            // 
            // labelA_IncidentReport
            // 
            this.labelA_IncidentReport.AutoSize = true;
            this.labelA_IncidentReport.BackColor = System.Drawing.Color.Transparent;
            this.labelA_IncidentReport.Font = new System.Drawing.Font("Beaufort for LOL Heavy", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelA_IncidentReport.ForeColor = System.Drawing.SystemColors.Control;
            this.labelA_IncidentReport.Location = new System.Drawing.Point(28, 199);
            this.labelA_IncidentReport.Name = "labelA_IncidentReport";
            this.labelA_IncidentReport.Size = new System.Drawing.Size(256, 34);
            this.labelA_IncidentReport.TabIndex = 19;
            this.labelA_IncidentReport.Text = "INCIDENT REPORT";
            // 
            // labelB_IncidentReport
            // 
            this.labelB_IncidentReport.AutoSize = true;
            this.labelB_IncidentReport.BackColor = System.Drawing.Color.Transparent;
            this.labelB_IncidentReport.Font = new System.Drawing.Font("Beaufort for LOL", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelB_IncidentReport.ForeColor = System.Drawing.SystemColors.Control;
            this.labelB_IncidentReport.Location = new System.Drawing.Point(62, 231);
            this.labelB_IncidentReport.Name = "labelB_IncidentReport";
            this.labelB_IncidentReport.Size = new System.Drawing.Size(188, 20);
            this.labelB_IncidentReport.TabIndex = 20;
            this.labelB_IncidentReport.Text = "MANAGEMENT SYSTEM";
            // 
            // pictureBoxbtnlogin
            // 
            this.pictureBoxbtnlogin.BackColor = System.Drawing.SystemColors.Highlight;
            this.pictureBoxbtnlogin.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxbtnlogin.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxbtnlogin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBoxbtnlogin.Name = "pictureBoxbtnlogin";
            this.pictureBoxbtnlogin.Padding = new System.Windows.Forms.Padding(5);
            this.pictureBoxbtnlogin.Size = new System.Drawing.Size(33, 27);
            this.pictureBoxbtnlogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxbtnlogin.TabIndex = 21;
            this.pictureBoxbtnlogin.TabStop = false;
            // 
            // panelbtnlogin
            // 
            this.panelbtnlogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelbtnlogin.Controls.Add(this.btnlogin);
            this.panelbtnlogin.Controls.Add(this.pictureBoxbtnlogin);
            this.panelbtnlogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelbtnlogin.Location = new System.Drawing.Point(33, 413);
            this.panelbtnlogin.Name = "panelbtnlogin";
            this.panelbtnlogin.Size = new System.Drawing.Size(125, 29);
            this.panelbtnlogin.TabIndex = 22;
            // 
            // panelbtnreset
            // 
            this.panelbtnreset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelbtnreset.Controls.Add(this.btnreset);
            this.panelbtnreset.Controls.Add(this.pictureBoxbtnreset);
            this.panelbtnreset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelbtnreset.Location = new System.Drawing.Point(181, 412);
            this.panelbtnreset.Name = "panelbtnreset";
            this.panelbtnreset.Size = new System.Drawing.Size(90, 29);
            this.panelbtnreset.TabIndex = 23;
            // 
            // btnreset
            // 
            this.btnreset.BackColor = System.Drawing.Color.Firebrick;
            this.btnreset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnreset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnreset.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.btnreset.FlatAppearance.BorderSize = 0;
            this.btnreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnreset.Font = new System.Drawing.Font("Spiegel Bold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreset.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnreset.Location = new System.Drawing.Point(33, 0);
            this.btnreset.Margin = new System.Windows.Forms.Padding(0);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(55, 27);
            this.btnreset.TabIndex = 5;
            this.btnreset.Text = "&Reset";
            this.btnreset.UseVisualStyleBackColor = false;
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            this.btnreset.MouseLeave += new System.EventHandler(this.btnreset_MouseLeave_1);
            this.btnreset.MouseHover += new System.EventHandler(this.btnreset_MouseHover_1);
            // 
            // pictureBoxbtnreset
            // 
            this.pictureBoxbtnreset.BackColor = System.Drawing.Color.Firebrick;
            this.pictureBoxbtnreset.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxbtnreset.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxbtnreset.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBoxbtnreset.Name = "pictureBoxbtnreset";
            this.pictureBoxbtnreset.Padding = new System.Windows.Forms.Padding(5);
            this.pictureBoxbtnreset.Size = new System.Drawing.Size(33, 27);
            this.pictureBoxbtnreset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxbtnreset.TabIndex = 21;
            this.pictureBoxbtnreset.TabStop = false;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(184)))));
            this.ClientSize = new System.Drawing.Size(314, 459);
            this.Controls.Add(this.panelbtnreset);
            this.Controls.Add(this.panelbtnlogin);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.labelB_IncidentReport);
            this.Controls.Add(this.labelA_IncidentReport);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_minimize);
            this.Controls.Add(this.icon_password);
            this.Controls.Add(this.icon_username);
            this.Controls.Add(this.chkboxShow);
            this.Controls.Add(this.labelpassword);
            this.Controls.Add(this.labelusername);
            this.Controls.Add(this.logo_auemsLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AUEMS-Login";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon_password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon_username)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo_auemsLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxbtnlogin)).EndInit();
            this.panelbtnlogin.ResumeLayout(false);
            this.panelbtnreset.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxbtnreset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

	}
}
