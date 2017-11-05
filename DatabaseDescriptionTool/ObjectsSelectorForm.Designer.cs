/*
Copyright (C) 2017 Lars Hove Christiansen
http://virtcore.com

This file is a part of Database Description Tool

	Database Description Tool is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Database Description Tool is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Database Description Tool. If not, see <http://www.gnu.org/licenses/>.
*/

partial class ObjectsSelectorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectsSelectorForm));
			this.objectsGroupBox = new System.Windows.Forms.GroupBox();
			this.chooseScalarValuedFunctionsLinkLabel = new System.Windows.Forms.LinkLabel();
			this.chooseTableValuedFunctionsLinkLabel = new System.Windows.Forms.LinkLabel();
			this.chooseStoredProceduresLinkLabel = new System.Windows.Forms.LinkLabel();
			this.chooseViewsLinkLabel = new System.Windows.Forms.LinkLabel();
			this.chooseTabelsLinkLabel = new System.Windows.Forms.LinkLabel();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.scalarValuedFunctionParametersCheckBox = new System.Windows.Forms.CheckBox();
			this.tableValuedFunctionParametersCheckBox = new System.Windows.Forms.CheckBox();
			this.storedProcedureParametersCheckBox = new System.Windows.Forms.CheckBox();
			this.tableKeysCheckBox = new System.Windows.Forms.CheckBox();
			this.tablesCheckBox = new System.Windows.Forms.CheckBox();
			this.scalarValuedFunctionsCheckBox = new System.Windows.Forms.CheckBox();
			this.tableConstraintsCheckBox = new System.Windows.Forms.CheckBox();
			this.tableColumnsCheckBox = new System.Windows.Forms.CheckBox();
			this.tableValuedFunctionsCheckBox = new System.Windows.Forms.CheckBox();
			this.tableTriggersCheckBox = new System.Windows.Forms.CheckBox();
			this.tableIndexesCheckBox = new System.Windows.Forms.CheckBox();
			this.storedProceduresCheckBox = new System.Windows.Forms.CheckBox();
			this.viewsCheckBox = new System.Windows.Forms.CheckBox();
			this.viewColumnsCheckBox = new System.Windows.Forms.CheckBox();
			this.viewIndexesCheckBox = new System.Windows.Forms.CheckBox();
			this.viewTriggersCheckBox = new System.Windows.Forms.CheckBox();
			this.selectAllButton = new System.Windows.Forms.Button();
			this.deselectAllButton = new System.Windows.Forms.Button();
			this.descriptionFieldsGroupBox = new System.Windows.Forms.GroupBox();
			this.fieldNameListView = new System.Windows.Forms.ListView();
			this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.allCheckBox = new System.Windows.Forms.CheckBox();
			this.okButton = new System.Windows.Forms.Button();
			this.objectsGroupBox.SuspendLayout();
			this.descriptionFieldsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// objectsGroupBox
			// 
			this.objectsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objectsGroupBox.Controls.Add(this.chooseScalarValuedFunctionsLinkLabel);
			this.objectsGroupBox.Controls.Add(this.chooseTableValuedFunctionsLinkLabel);
			this.objectsGroupBox.Controls.Add(this.chooseStoredProceduresLinkLabel);
			this.objectsGroupBox.Controls.Add(this.chooseViewsLinkLabel);
			this.objectsGroupBox.Controls.Add(this.chooseTabelsLinkLabel);
			this.objectsGroupBox.Controls.Add(this.label4);
			this.objectsGroupBox.Controls.Add(this.label2);
			this.objectsGroupBox.Controls.Add(this.label1);
			this.objectsGroupBox.Controls.Add(this.label3);
			this.objectsGroupBox.Controls.Add(this.scalarValuedFunctionParametersCheckBox);
			this.objectsGroupBox.Controls.Add(this.tableValuedFunctionParametersCheckBox);
			this.objectsGroupBox.Controls.Add(this.storedProcedureParametersCheckBox);
			this.objectsGroupBox.Controls.Add(this.tableKeysCheckBox);
			this.objectsGroupBox.Controls.Add(this.tablesCheckBox);
			this.objectsGroupBox.Controls.Add(this.scalarValuedFunctionsCheckBox);
			this.objectsGroupBox.Controls.Add(this.tableConstraintsCheckBox);
			this.objectsGroupBox.Controls.Add(this.tableColumnsCheckBox);
			this.objectsGroupBox.Controls.Add(this.tableValuedFunctionsCheckBox);
			this.objectsGroupBox.Controls.Add(this.tableTriggersCheckBox);
			this.objectsGroupBox.Controls.Add(this.tableIndexesCheckBox);
			this.objectsGroupBox.Controls.Add(this.storedProceduresCheckBox);
			this.objectsGroupBox.Controls.Add(this.viewsCheckBox);
			this.objectsGroupBox.Controls.Add(this.viewColumnsCheckBox);
			this.objectsGroupBox.Controls.Add(this.viewIndexesCheckBox);
			this.objectsGroupBox.Controls.Add(this.viewTriggersCheckBox);
			this.objectsGroupBox.Location = new System.Drawing.Point(262, 12);
			this.objectsGroupBox.Name = "objectsGroupBox";
			this.objectsGroupBox.Size = new System.Drawing.Size(247, 406);
			this.objectsGroupBox.TabIndex = 3;
			this.objectsGroupBox.TabStop = false;
			this.objectsGroupBox.Text = "Objects";
			// 
			// chooseScalarValuedFunctionsLinkLabel
			// 
			this.chooseScalarValuedFunctionsLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chooseScalarValuedFunctionsLinkLabel.AutoSize = true;
			this.chooseScalarValuedFunctionsLinkLabel.Location = new System.Drawing.Point(189, 362);
			this.chooseScalarValuedFunctionsLinkLabel.Name = "chooseScalarValuedFunctionsLinkLabel";
			this.chooseScalarValuedFunctionsLinkLabel.Size = new System.Drawing.Size(52, 13);
			this.chooseScalarValuedFunctionsLinkLabel.TabIndex = 23;
			this.chooseScalarValuedFunctionsLinkLabel.TabStop = true;
			this.chooseScalarValuedFunctionsLinkLabel.Text = "Choose...";
			this.chooseScalarValuedFunctionsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChooseScalarValuedFunctionsLinkLabel_LinkClicked);
			// 
			// chooseTableValuedFunctionsLinkLabel
			// 
			this.chooseTableValuedFunctionsLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chooseTableValuedFunctionsLinkLabel.AutoSize = true;
			this.chooseTableValuedFunctionsLinkLabel.Location = new System.Drawing.Point(189, 311);
			this.chooseTableValuedFunctionsLinkLabel.Name = "chooseTableValuedFunctionsLinkLabel";
			this.chooseTableValuedFunctionsLinkLabel.Size = new System.Drawing.Size(52, 13);
			this.chooseTableValuedFunctionsLinkLabel.TabIndex = 22;
			this.chooseTableValuedFunctionsLinkLabel.TabStop = true;
			this.chooseTableValuedFunctionsLinkLabel.Text = "Choose...";
			this.chooseTableValuedFunctionsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChooseTableValuedFunctionsLinkLabel_LinkClicked);
			// 
			// chooseStoredProceduresLinkLabel
			// 
			this.chooseStoredProceduresLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chooseStoredProceduresLinkLabel.AutoSize = true;
			this.chooseStoredProceduresLinkLabel.Location = new System.Drawing.Point(189, 260);
			this.chooseStoredProceduresLinkLabel.Name = "chooseStoredProceduresLinkLabel";
			this.chooseStoredProceduresLinkLabel.Size = new System.Drawing.Size(52, 13);
			this.chooseStoredProceduresLinkLabel.TabIndex = 21;
			this.chooseStoredProceduresLinkLabel.TabStop = true;
			this.chooseStoredProceduresLinkLabel.Text = "Choose...";
			this.chooseStoredProceduresLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChooseStoredProceduresLinkLabel_LinkClicked);
			// 
			// chooseViewsLinkLabel
			// 
			this.chooseViewsLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chooseViewsLinkLabel.AutoSize = true;
			this.chooseViewsLinkLabel.Location = new System.Drawing.Point(189, 163);
			this.chooseViewsLinkLabel.Name = "chooseViewsLinkLabel";
			this.chooseViewsLinkLabel.Size = new System.Drawing.Size(52, 13);
			this.chooseViewsLinkLabel.TabIndex = 20;
			this.chooseViewsLinkLabel.TabStop = true;
			this.chooseViewsLinkLabel.Text = "Choose...";
			this.chooseViewsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChooseViewsLinkLabel_LinkClicked);
			// 
			// chooseTabelsLinkLabel
			// 
			this.chooseTabelsLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chooseTabelsLinkLabel.AutoSize = true;
			this.chooseTabelsLinkLabel.Location = new System.Drawing.Point(189, 20);
			this.chooseTabelsLinkLabel.Name = "chooseTabelsLinkLabel";
			this.chooseTabelsLinkLabel.Size = new System.Drawing.Size(52, 13);
			this.chooseTabelsLinkLabel.TabIndex = 19;
			this.chooseTabelsLinkLabel.TabStop = true;
			this.chooseTabelsLinkLabel.Text = "Choose...";
			this.chooseTabelsLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChooseTabelsLinkLabel_LinkClicked);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label4.Location = new System.Drawing.Point(6, 352);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(235, 2);
			this.label4.TabIndex = 29;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Location = new System.Drawing.Point(6, 301);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(235, 2);
			this.label2.TabIndex = 28;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(6, 250);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(235, 2);
			this.label1.TabIndex = 27;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(6, 153);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(235, 2);
			this.label3.TabIndex = 22;
			// 
			// scalarValuedFunctionParametersCheckBox
			// 
			this.scalarValuedFunctionParametersCheckBox.AutoSize = true;
			this.scalarValuedFunctionParametersCheckBox.Checked = true;
			this.scalarValuedFunctionParametersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.scalarValuedFunctionParametersCheckBox.Location = new System.Drawing.Point(29, 384);
			this.scalarValuedFunctionParametersCheckBox.Name = "scalarValuedFunctionParametersCheckBox";
			this.scalarValuedFunctionParametersCheckBox.Size = new System.Drawing.Size(79, 17);
			this.scalarValuedFunctionParametersCheckBox.TabIndex = 18;
			this.scalarValuedFunctionParametersCheckBox.Text = "Parameters";
			this.scalarValuedFunctionParametersCheckBox.UseVisualStyleBackColor = true;
			this.scalarValuedFunctionParametersCheckBox.CheckedChanged += new System.EventHandler(this.ScalarValuedFunctionParametersCheckBox_CheckedChanged);
			// 
			// tableValuedFunctionParametersCheckBox
			// 
			this.tableValuedFunctionParametersCheckBox.AutoSize = true;
			this.tableValuedFunctionParametersCheckBox.Checked = true;
			this.tableValuedFunctionParametersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableValuedFunctionParametersCheckBox.Location = new System.Drawing.Point(29, 333);
			this.tableValuedFunctionParametersCheckBox.Name = "tableValuedFunctionParametersCheckBox";
			this.tableValuedFunctionParametersCheckBox.Size = new System.Drawing.Size(79, 17);
			this.tableValuedFunctionParametersCheckBox.TabIndex = 16;
			this.tableValuedFunctionParametersCheckBox.Text = "Parameters";
			this.tableValuedFunctionParametersCheckBox.UseVisualStyleBackColor = true;
			this.tableValuedFunctionParametersCheckBox.CheckedChanged += new System.EventHandler(this.TableValuedFunctionParametersCheckBox_CheckedChanged);
			// 
			// storedProcedureParametersCheckBox
			// 
			this.storedProcedureParametersCheckBox.AutoSize = true;
			this.storedProcedureParametersCheckBox.Checked = true;
			this.storedProcedureParametersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.storedProcedureParametersCheckBox.Location = new System.Drawing.Point(29, 282);
			this.storedProcedureParametersCheckBox.Name = "storedProcedureParametersCheckBox";
			this.storedProcedureParametersCheckBox.Size = new System.Drawing.Size(79, 17);
			this.storedProcedureParametersCheckBox.TabIndex = 14;
			this.storedProcedureParametersCheckBox.Text = "Parameters";
			this.storedProcedureParametersCheckBox.UseVisualStyleBackColor = true;
			this.storedProcedureParametersCheckBox.CheckedChanged += new System.EventHandler(this.StoredProcedureParametersCheckBox_CheckedChanged);
			// 
			// tableKeysCheckBox
			// 
			this.tableKeysCheckBox.AutoSize = true;
			this.tableKeysCheckBox.Checked = true;
			this.tableKeysCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableKeysCheckBox.Location = new System.Drawing.Point(29, 65);
			this.tableKeysCheckBox.Name = "tableKeysCheckBox";
			this.tableKeysCheckBox.Size = new System.Drawing.Size(49, 17);
			this.tableKeysCheckBox.TabIndex = 5;
			this.tableKeysCheckBox.Text = "Keys";
			this.tableKeysCheckBox.UseVisualStyleBackColor = true;
			this.tableKeysCheckBox.CheckedChanged += new System.EventHandler(this.TableKeysCheckBox_CheckedChanged);
			// 
			// tablesCheckBox
			// 
			this.tablesCheckBox.AutoSize = true;
			this.tablesCheckBox.Checked = true;
			this.tablesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tablesCheckBox.Location = new System.Drawing.Point(9, 19);
			this.tablesCheckBox.Name = "tablesCheckBox";
			this.tablesCheckBox.Size = new System.Drawing.Size(58, 17);
			this.tablesCheckBox.TabIndex = 3;
			this.tablesCheckBox.Text = "Tables";
			this.tablesCheckBox.UseVisualStyleBackColor = true;
			this.tablesCheckBox.CheckedChanged += new System.EventHandler(this.TablesCheckBox_CheckedChanged);
			// 
			// scalarValuedFunctionsCheckBox
			// 
			this.scalarValuedFunctionsCheckBox.AutoSize = true;
			this.scalarValuedFunctionsCheckBox.Checked = true;
			this.scalarValuedFunctionsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.scalarValuedFunctionsCheckBox.Location = new System.Drawing.Point(9, 361);
			this.scalarValuedFunctionsCheckBox.Name = "scalarValuedFunctionsCheckBox";
			this.scalarValuedFunctionsCheckBox.Size = new System.Drawing.Size(140, 17);
			this.scalarValuedFunctionsCheckBox.TabIndex = 17;
			this.scalarValuedFunctionsCheckBox.Text = "Scalar-valued Functions";
			this.scalarValuedFunctionsCheckBox.UseVisualStyleBackColor = true;
			this.scalarValuedFunctionsCheckBox.CheckedChanged += new System.EventHandler(this.ScalarValuedFunctionsCheckBox_CheckedChanged);
			// 
			// tableConstraintsCheckBox
			// 
			this.tableConstraintsCheckBox.AutoSize = true;
			this.tableConstraintsCheckBox.Checked = true;
			this.tableConstraintsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableConstraintsCheckBox.Location = new System.Drawing.Point(29, 88);
			this.tableConstraintsCheckBox.Name = "tableConstraintsCheckBox";
			this.tableConstraintsCheckBox.Size = new System.Drawing.Size(78, 17);
			this.tableConstraintsCheckBox.TabIndex = 6;
			this.tableConstraintsCheckBox.Text = "Constraints";
			this.tableConstraintsCheckBox.UseVisualStyleBackColor = true;
			this.tableConstraintsCheckBox.CheckedChanged += new System.EventHandler(this.TableConstraintsCheckBox_CheckedChanged);
			// 
			// tableColumnsCheckBox
			// 
			this.tableColumnsCheckBox.AutoSize = true;
			this.tableColumnsCheckBox.Checked = true;
			this.tableColumnsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableColumnsCheckBox.Location = new System.Drawing.Point(29, 42);
			this.tableColumnsCheckBox.Name = "tableColumnsCheckBox";
			this.tableColumnsCheckBox.Size = new System.Drawing.Size(66, 17);
			this.tableColumnsCheckBox.TabIndex = 4;
			this.tableColumnsCheckBox.Text = "Columns";
			this.tableColumnsCheckBox.UseVisualStyleBackColor = true;
			this.tableColumnsCheckBox.CheckedChanged += new System.EventHandler(this.TableColumnsCheckBox_CheckedChanged);
			// 
			// tableValuedFunctionsCheckBox
			// 
			this.tableValuedFunctionsCheckBox.AutoSize = true;
			this.tableValuedFunctionsCheckBox.Checked = true;
			this.tableValuedFunctionsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableValuedFunctionsCheckBox.Location = new System.Drawing.Point(9, 310);
			this.tableValuedFunctionsCheckBox.Name = "tableValuedFunctionsCheckBox";
			this.tableValuedFunctionsCheckBox.Size = new System.Drawing.Size(137, 17);
			this.tableValuedFunctionsCheckBox.TabIndex = 15;
			this.tableValuedFunctionsCheckBox.Text = "Table-valued Functions";
			this.tableValuedFunctionsCheckBox.UseVisualStyleBackColor = true;
			this.tableValuedFunctionsCheckBox.CheckedChanged += new System.EventHandler(this.TableValuedFunctionsCheckBox_CheckedChanged);
			// 
			// tableTriggersCheckBox
			// 
			this.tableTriggersCheckBox.AutoSize = true;
			this.tableTriggersCheckBox.Checked = true;
			this.tableTriggersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableTriggersCheckBox.Location = new System.Drawing.Point(29, 111);
			this.tableTriggersCheckBox.Name = "tableTriggersCheckBox";
			this.tableTriggersCheckBox.Size = new System.Drawing.Size(64, 17);
			this.tableTriggersCheckBox.TabIndex = 7;
			this.tableTriggersCheckBox.Text = "Triggers";
			this.tableTriggersCheckBox.UseVisualStyleBackColor = true;
			this.tableTriggersCheckBox.CheckedChanged += new System.EventHandler(this.TableTriggersCheckBox_CheckedChanged);
			// 
			// tableIndexesCheckBox
			// 
			this.tableIndexesCheckBox.AutoSize = true;
			this.tableIndexesCheckBox.Checked = true;
			this.tableIndexesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableIndexesCheckBox.Location = new System.Drawing.Point(29, 134);
			this.tableIndexesCheckBox.Name = "tableIndexesCheckBox";
			this.tableIndexesCheckBox.Size = new System.Drawing.Size(63, 17);
			this.tableIndexesCheckBox.TabIndex = 8;
			this.tableIndexesCheckBox.Text = "Indexes";
			this.tableIndexesCheckBox.UseVisualStyleBackColor = true;
			this.tableIndexesCheckBox.CheckedChanged += new System.EventHandler(this.TableIndexesCheckBox_CheckedChanged);
			// 
			// storedProceduresCheckBox
			// 
			this.storedProceduresCheckBox.AutoSize = true;
			this.storedProceduresCheckBox.Checked = true;
			this.storedProceduresCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.storedProceduresCheckBox.Location = new System.Drawing.Point(9, 259);
			this.storedProceduresCheckBox.Name = "storedProceduresCheckBox";
			this.storedProceduresCheckBox.Size = new System.Drawing.Size(114, 17);
			this.storedProceduresCheckBox.TabIndex = 13;
			this.storedProceduresCheckBox.Text = "Stored Procedures";
			this.storedProceduresCheckBox.UseVisualStyleBackColor = true;
			this.storedProceduresCheckBox.CheckedChanged += new System.EventHandler(this.StoredProceduresCheckBox_CheckedChanged);
			// 
			// viewsCheckBox
			// 
			this.viewsCheckBox.AutoSize = true;
			this.viewsCheckBox.Checked = true;
			this.viewsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.viewsCheckBox.Location = new System.Drawing.Point(9, 162);
			this.viewsCheckBox.Name = "viewsCheckBox";
			this.viewsCheckBox.Size = new System.Drawing.Size(54, 17);
			this.viewsCheckBox.TabIndex = 9;
			this.viewsCheckBox.Text = "Views";
			this.viewsCheckBox.UseVisualStyleBackColor = true;
			this.viewsCheckBox.CheckedChanged += new System.EventHandler(this.ViewsCheckBox_CheckedChanged);
			// 
			// viewColumnsCheckBox
			// 
			this.viewColumnsCheckBox.AutoSize = true;
			this.viewColumnsCheckBox.Checked = true;
			this.viewColumnsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.viewColumnsCheckBox.Location = new System.Drawing.Point(29, 185);
			this.viewColumnsCheckBox.Name = "viewColumnsCheckBox";
			this.viewColumnsCheckBox.Size = new System.Drawing.Size(66, 17);
			this.viewColumnsCheckBox.TabIndex = 10;
			this.viewColumnsCheckBox.Text = "Columns";
			this.viewColumnsCheckBox.UseVisualStyleBackColor = true;
			this.viewColumnsCheckBox.CheckedChanged += new System.EventHandler(this.ViewColumnsCheckBox_CheckedChanged);
			// 
			// viewIndexesCheckBox
			// 
			this.viewIndexesCheckBox.AutoSize = true;
			this.viewIndexesCheckBox.Checked = true;
			this.viewIndexesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.viewIndexesCheckBox.Location = new System.Drawing.Point(29, 231);
			this.viewIndexesCheckBox.Name = "viewIndexesCheckBox";
			this.viewIndexesCheckBox.Size = new System.Drawing.Size(63, 17);
			this.viewIndexesCheckBox.TabIndex = 12;
			this.viewIndexesCheckBox.Text = "Indexes";
			this.viewIndexesCheckBox.UseVisualStyleBackColor = true;
			this.viewIndexesCheckBox.CheckedChanged += new System.EventHandler(this.ViewIndexesCheckBox_CheckedChanged);
			// 
			// viewTriggersCheckBox
			// 
			this.viewTriggersCheckBox.AutoSize = true;
			this.viewTriggersCheckBox.Checked = true;
			this.viewTriggersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.viewTriggersCheckBox.Location = new System.Drawing.Point(29, 208);
			this.viewTriggersCheckBox.Name = "viewTriggersCheckBox";
			this.viewTriggersCheckBox.Size = new System.Drawing.Size(64, 17);
			this.viewTriggersCheckBox.TabIndex = 11;
			this.viewTriggersCheckBox.Text = "Triggers";
			this.viewTriggersCheckBox.UseVisualStyleBackColor = true;
			this.viewTriggersCheckBox.CheckedChanged += new System.EventHandler(this.ViewTriggersCheckBox_CheckedChanged);
			// 
			// selectAllButton
			// 
			this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.selectAllButton.Location = new System.Drawing.Point(262, 428);
			this.selectAllButton.Name = "selectAllButton";
			this.selectAllButton.Size = new System.Drawing.Size(75, 24);
			this.selectAllButton.TabIndex = 24;
			this.selectAllButton.Text = "Select All";
			this.selectAllButton.UseVisualStyleBackColor = true;
			this.selectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
			// 
			// deselectAllButton
			// 
			this.deselectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.deselectAllButton.Location = new System.Drawing.Point(343, 428);
			this.deselectAllButton.Name = "deselectAllButton";
			this.deselectAllButton.Size = new System.Drawing.Size(75, 24);
			this.deselectAllButton.TabIndex = 25;
			this.deselectAllButton.Text = "Deselect All";
			this.deselectAllButton.UseVisualStyleBackColor = true;
			this.deselectAllButton.Click += new System.EventHandler(this.DeselectAllButton_Click);
			// 
			// descriptionFieldsGroupBox
			// 
			this.descriptionFieldsGroupBox.Controls.Add(this.fieldNameListView);
			this.descriptionFieldsGroupBox.Controls.Add(this.allCheckBox);
			this.descriptionFieldsGroupBox.Location = new System.Drawing.Point(12, 12);
			this.descriptionFieldsGroupBox.Name = "descriptionFieldsGroupBox";
			this.descriptionFieldsGroupBox.Size = new System.Drawing.Size(244, 142);
			this.descriptionFieldsGroupBox.TabIndex = 0;
			this.descriptionFieldsGroupBox.TabStop = false;
			this.descriptionFieldsGroupBox.Text = "Description Fields";
			// 
			// fieldNameListView
			// 
			this.fieldNameListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fieldNameListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
			this.fieldNameListView.Enabled = false;
			this.fieldNameListView.HideSelection = false;
			this.fieldNameListView.Location = new System.Drawing.Point(6, 19);
			this.fieldNameListView.Name = "fieldNameListView";
			this.fieldNameListView.Size = new System.Drawing.Size(232, 95);
			this.fieldNameListView.TabIndex = 0;
			this.fieldNameListView.UseCompatibleStateImageBehavior = false;
			this.fieldNameListView.View = System.Windows.Forms.View.Details;
			this.fieldNameListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.FieldNameListView_ColumnClick);
			this.fieldNameListView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.FieldNameListView_ColumnWidthChanged);
			this.fieldNameListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FieldNameListView_KeyDown);
			// 
			// columnHeader
			// 
			this.columnHeader.Text = "Field name";
			this.columnHeader.Width = 150;
			// 
			// allCheckBox
			// 
			this.allCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.allCheckBox.AutoSize = true;
			this.allCheckBox.Checked = true;
			this.allCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.allCheckBox.Location = new System.Drawing.Point(6, 120);
			this.allCheckBox.Name = "allCheckBox";
			this.allCheckBox.Size = new System.Drawing.Size(37, 17);
			this.allCheckBox.TabIndex = 1;
			this.allCheckBox.Text = "All";
			this.allCheckBox.UseVisualStyleBackColor = true;
			this.allCheckBox.CheckedChanged += new System.EventHandler(this.AllCheckBox_CheckedChanged);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(434, 428);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 24);
			this.okButton.TabIndex = 26;
			this.okButton.Text = "Ok";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// ObjectsSelectorForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(521, 459);
			this.Controls.Add(this.selectAllButton);
			this.Controls.Add(this.deselectAllButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.descriptionFieldsGroupBox);
			this.Controls.Add(this.objectsGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ObjectsSelectorForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choose Objects...";
			this.objectsGroupBox.ResumeLayout(false);
			this.objectsGroupBox.PerformLayout();
			this.descriptionFieldsGroupBox.ResumeLayout(false);
			this.descriptionFieldsGroupBox.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.GroupBox objectsGroupBox;
	private System.Windows.Forms.Button selectAllButton;
	private System.Windows.Forms.Button deselectAllButton;
	private System.Windows.Forms.GroupBox descriptionFieldsGroupBox;
	public System.Windows.Forms.CheckBox allCheckBox;
	public System.Windows.Forms.CheckBox scalarValuedFunctionParametersCheckBox;
	public System.Windows.Forms.CheckBox tableValuedFunctionParametersCheckBox;
	public System.Windows.Forms.CheckBox storedProcedureParametersCheckBox;
	public System.Windows.Forms.CheckBox tableKeysCheckBox;
	public System.Windows.Forms.CheckBox tablesCheckBox;
	public System.Windows.Forms.CheckBox scalarValuedFunctionsCheckBox;
	public System.Windows.Forms.CheckBox tableConstraintsCheckBox;
	public System.Windows.Forms.CheckBox tableColumnsCheckBox;
	public System.Windows.Forms.CheckBox tableValuedFunctionsCheckBox;
	public System.Windows.Forms.CheckBox tableTriggersCheckBox;
	public System.Windows.Forms.CheckBox tableIndexesCheckBox;
	public System.Windows.Forms.CheckBox storedProceduresCheckBox;
	public System.Windows.Forms.CheckBox viewsCheckBox;
	public System.Windows.Forms.CheckBox viewColumnsCheckBox;
	public System.Windows.Forms.CheckBox viewIndexesCheckBox;
	public System.Windows.Forms.CheckBox viewTriggersCheckBox;
	private System.Windows.Forms.Button okButton;
	public System.Windows.Forms.ListView fieldNameListView;
	private System.Windows.Forms.ColumnHeader columnHeader;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.LinkLabel chooseTabelsLinkLabel;
	private System.Windows.Forms.LinkLabel chooseViewsLinkLabel;
	private System.Windows.Forms.LinkLabel chooseStoredProceduresLinkLabel;
	private System.Windows.Forms.LinkLabel chooseTableValuedFunctionsLinkLabel;
	private System.Windows.Forms.LinkLabel chooseScalarValuedFunctionsLinkLabel;
}
