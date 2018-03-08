namespace ServerFromApp
{
    partial class frmCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnStartListen = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtMessageClient = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMsdToSend = new System.Windows.Forms.TextBox();
            this.btnCloseConnection = new System.Windows.Forms.Button();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(55, 12);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(93, 23);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            // 
            // btnStartListen
            // 
            this.btnStartListen.Location = new System.Drawing.Point(55, 41);
            this.btnStartListen.Name = "btnStartListen";
            this.btnStartListen.Size = new System.Drawing.Size(93, 23);
            this.btnStartListen.TabIndex = 1;
            this.btnStartListen.Text = "Start Listen";
            this.btnStartListen.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(176, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            // 
            // txtMessageClient
            // 
            this.txtMessageClient.Location = new System.Drawing.Point(55, 104);
            this.txtMessageClient.Multiline = true;
            this.txtMessageClient.Name = "txtMessageClient";
            this.txtMessageClient.Size = new System.Drawing.Size(338, 83);
            this.txtMessageClient.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mensaje recivido del Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mensaje a enviar al Cliente";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtMsdToSend
            // 
            this.txtMsdToSend.Location = new System.Drawing.Point(55, 225);
            this.txtMsdToSend.Multiline = true;
            this.txtMsdToSend.Name = "txtMsdToSend";
            this.txtMsdToSend.Size = new System.Drawing.Size(338, 74);
            this.txtMsdToSend.TabIndex = 5;
            // 
            // btnCloseConnection
            // 
            this.btnCloseConnection.Location = new System.Drawing.Point(283, 305);
            this.btnCloseConnection.Name = "btnCloseConnection";
            this.btnCloseConnection.Size = new System.Drawing.Size(110, 23);
            this.btnCloseConnection.TabIndex = 8;
            this.btnCloseConnection.Text = "Close Connection";
            this.btnCloseConnection.UseVisualStyleBackColor = true;
            this.btnCloseConnection.Click += new System.EventHandler(this.btnCloseConnection_Click);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(55, 305);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(93, 23);
            this.btnSendMessage.TabIndex = 7;
            this.btnSendMessage.Text = "Send message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 341);
            this.Controls.Add(this.btnCloseConnection);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMsdToSend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMessageClient);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStartListen);
            this.Controls.Add(this.btnStartServer);
            this.Name = "frmCliente";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStartListen;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtMessageClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMsdToSend;
        private System.Windows.Forms.Button btnCloseConnection;
        private System.Windows.Forms.Button btnSendMessage;
    }
}

