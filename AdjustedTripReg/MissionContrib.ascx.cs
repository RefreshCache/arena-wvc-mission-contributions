namespace ArenaWeb.UserControls.Custom.WVC.AdjustedTripReg.Mod
{
    using Arena;
    using Arena.Contributions;
    using Arena.Core;
    using Arena.Core.Communications;
    using Arena.Exceptions;
    using Arena.Payment;
    using Arena.Portal;
    using Arena.Portal.UI;
    using Arena.Trip;
    using Arena.Utility;
    using ASP;
    using aspNetEmail;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Profile;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

	public partial class Mission_Contrib : PortalControl
	{
		protected enum ActionType
		{
			Register = 1,
			Contribute = 2,
			Payment = 3,
            GenContribute = 4
		}
		/*protected Panel errorMessagePanel;
		protected DropDownList tripList;
		protected Literal lcTripName;
		protected RadioButtonList registrationAction;
		protected Label lblNoAvailableAttendees;
		protected DropDownList attendeeList;
		protected RequiredFieldValidator rfvAttendee;
		protected Label lblMissingAttendees;
		protected HtmlTableRow attendeeListRow;
		protected Label attendeeFNameLabel;
		protected TextBox attendeeFirstName;
		protected Label attendeeLNameLabel;
		protected TextBox attendeeLastName;
		protected Button verifyAttendee;
		protected HtmlGenericControl verified;
		protected Label extraInfoLabel;
		protected RadioButtonList extraInfoList;
		protected TextBox extraInfo;
		protected HtmlTableRow attendeeExtraInfoRow;
		protected Panel pnlAttendeeTextRow;
		protected HtmlTableRow attendeeTextRow;
		protected CheckBox anonymousFlag;
		protected HtmlTableRow anonymousTextRow;
		protected StatusCheckBox cbStatus;
		protected HtmlTableRow cbUpdateGroupStatus;
		protected RadioButton depositCost;
		protected Label depositCostAmount;
		protected RadioButton registrationDeadlineDeposit2Cost;
		protected Label registrationDeadlineDeposit2Amount;
		protected RadioButton registrationDeadlineDeposit3Cost;
		protected Label registrationDeadlineDeposit3Amount;
		protected RadioButton registrationCost;
		protected Label registrationCostAmount;
		protected RadioButton balanceDue;
		protected Label balanceDueAmount;
		protected RadioButton customPayment;
		protected TextBox customPaymentAmount;
		protected RequiredFieldValidator rfvCustomAmount;
		protected RangeValidator rvCustomAmount;
		protected HtmlTable tblTripInfo;
		protected Panel tripInfoErrorMsgPanel;
		protected WizardStep tripInformationStep;
		protected Label firstNameLabel;
		protected TextBox firstName;
		protected RequiredFieldValidator firstNameRFV;
		protected TextBox lastName;
		protected RequiredFieldValidator lastNameRFV;
		protected HtmlTableRow lastNameRow;
		protected TextBox emailAddress;
		protected RequiredFieldValidator emailAddressRFV;
		protected TextBox homePhone;
		protected RequiredFieldValidator homePhoneRFV;
		protected TextBox address1;
		protected RequiredFieldValidator address1RFV;
		protected TextBox city;
		protected RequiredFieldValidator cityRFV;
		protected TextBox state;
		protected RequiredFieldValidator stateRFV;
		protected TextBox zip;
		protected RequiredFieldValidator zipRFV;
		protected WizardStep billingInformationStep;
		protected RadioButtonList paymentMethodList;
		protected Panel paymentMethodPanel;
		protected Label lblRedirectMsg;
		protected HtmlTable tblRedirectWarning;
		protected TextBox ccNumber;
		protected Label ccDashMessage;
		protected RequiredFieldValidator ccNumberRFV;
		protected TextBox ccCIN;
		protected RequiredFieldValidator ccCINRFV;
		protected DropDownList expMonthList;
		protected RequiredFieldValidator expMonthRFV;
		protected DropDownList expYearList;
		protected RequiredFieldValidator expYearRFV;
		protected Panel ccPanel;
		protected TextBox bankName;
		protected RequiredFieldValidator bankNameRFV;
		protected RadioButtonList accountType;
		protected TextBox routingNumber;
		protected RequiredFieldValidator routingNumberRFV;
		protected TextBox accountNumber;
		protected RequiredFieldValidator accountNumberRFV;
		protected Image imgCheck;
		protected Panel achPanel;
		protected Panel preAuthErrorMsgPanel;
		protected WizardStep paymentInformationStep;
		protected PlaceHolder verificationPlaceholder;
		protected Panel verificationPanel;
		protected Panel authErrorMsgPanel;
		protected WizardStep verificationStep;
		protected Panel confirmationPanel;
		protected Panel confirmationNumberPanel;
		protected WizardStep confirmationStep;
		protected Wizard tripRegistrationWizard;
		protected Label messageLabel;
		protected Panel messagePanel;
		protected HtmlInputHidden hiddenPersonID;
		protected UpdatePanel registrationPanel;*/
		protected Mission _mission;
		protected GatewayAccount _ccGatewayAcct;
		protected GatewayAccount _achGatewayAcct;
		protected bool _validateCardNumber;
		protected bool _showLastName = true;
		protected DynamicFieldHelper _fieldHelper;
		protected bool _requireGoerApproval;
        protected string suppliedFirstName;
        protected string suppliedLastName;
        /*protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }
        protected global_asax ApplicationInstance
        {
            get
            {
                return (global_asax)this.Context.ApplicationInstance;
            }
        }*/

        #region Module Settings

        [GatewayAccountSetting("CC Payment Gateway Name", "The name of the Payment Gateway to use for Credit Card Transactions", false)]
		public string CCPaymentGatewayNameSetting
		{
			get
			{
				return base.Setting("CCPaymentGatewayName", "", false);
			}
		}
		[GatewayAccountSetting("ACH Payment Gateway Name", "The name of the Payment Gateway to use for ACH Transactions", false)]
		public string ACHPaymentGatewayNameSetting
		{
			get
			{
				return base.Setting("ACHPaymentGatewayName", "", false);
			}
		}
		[BooleanSetting("Validate Card Number", "Flag indicating whether or not the card number should be validated for correct prefix, length, and check-digit.", false, true)]
		public string ValidateCardNumberSetting
		{
			get
			{
				return base.Setting("ValidateCardNumber", "true", false);
			}
		}
		[PageSetting("Cancel Page", "The page that should be displayed when user clicks 'Cancel'", true)]
		public string CancelPageSetting
		{
			get
			{
				return base.Setting("CancelPage", "", true);
			}
		}
		[CssSetting("Css File", "An optional CSS File to use for this module.", false)]
		public string CssFileSetting
		{
			get
			{
				return base.Setting("CssFile", "", false);
			}
		}
		[BooleanSetting("Require Goer Approval", "Flag indicating if a team member must be approved before anyone can contribute on behalf of them.", false, false)]
		public string RequireGoerApprovalSetting
		{
			get
			{
				return base.Setting("RequireGoerApproval", "false", false);
			}
		}
		[BooleanSetting("Allow Choosing Trip", "Flag indicating if the user will be presented with a drop down of mission trips to pick from.  If this setting is false, a valid mission id must be passed in the querystring.", false, true)]
		public string ChooseTripSetting
		{
			get
			{
				return base.Setting("ChooseTrip", "true", false);
			}
		}
		[ImageSetting("Check Image", "The Image to display for showing user check with account number and routing number.", false)]
		public string CheckImageSetting
		{
			get
			{
				return base.Setting("CheckImage", "~/images/bankcheck.jpg", false);
			}
		}

        #endregion

        protected void Page_Init(object sender, EventArgs e)
		{
			if (base.CurrentPerson == null || base.CurrentPerson.PersonID == -1)
			{
				throw new ModuleException(base.CurrentPortalPage, base.CurrentModule, "This module requires that the user is logged in. Make sure to set security for this page so that only registered users can access it.");
			}
			this._validateCardNumber = Convert.ToBoolean(this.ValidateCardNumberSetting);
			if (this.CCPaymentGatewayNameSetting.Trim() != string.Empty)
			{
				try
				{
					this._ccGatewayAcct = new GatewayAccount(int.Parse(this.CCPaymentGatewayNameSetting.Trim()));
				}
				catch
				{
				}
			}
			if (this.ACHPaymentGatewayNameSetting.Trim() != string.Empty)
			{
				try
				{
					this._achGatewayAcct = new GatewayAccount(int.Parse(this.ACHPaymentGatewayNameSetting.Trim()));
				}
				catch
				{
				}
			}
			string path;
			if (this.CssFileSetting == string.Empty)
			{
				path = Utilities.GetApplicationPath() + "/css/tripRegistration.css";
			}
			else
			{
				path = Utilities.GetApplicationPath() + "/css" + this.CssFileSetting;
			}
			Utilities.AddCssLink(this.Page, path);
            
            BasePage.AddJavascriptInclude(this.Page, "http://code.jquery.com/jquery-latest.min.js");
            BasePage.AddJavascriptInclude(this.Page, "UserControls/Custom/WVC/AdjustedTripReg/js/wvcMissionContrib.js");

			this.imgCheck.ImageUrl = this.CheckImageSetting;
			if (base.Request.QueryString["mission"] != null && base.Request.QueryString["mission"] != string.Empty)
			{
				try
				{
					this._mission = new Mission(int.Parse(base.Request.QueryString["mission"]));
					goto IL_1CF;
				}
				catch (Exception inner)
				{
					throw new ModuleException(base.CurrentPortalPage, base.CurrentModule, "Could not load selected Mission Trip", inner);
				}
			}
			if (!bool.Parse(this.ChooseTripSetting))
			{
				throw new ModuleException(base.CurrentPortalPage, base.CurrentModule, "A valid mission ID must be passed in the query string when the 'ChooseTrip' module setting is set to 'false'");
			}
			IL_1CF:
			MissionCollection missionCollection = new MissionCollection();
			missionCollection.LoadByOrganizationId(base.CurrentOrganization.OrganizationID);
			foreach (Mission current in missionCollection)
			{
				if ((current.TripStatus.Value.ToString() != "Inactive") && (current.DepartureDate.AddDays(90) > DateTime.Today))
				{
					ListItem listItem = new ListItem(current.Name, current.MissionId.ToString());
					if (this._mission == null)
					{
						this._mission = current;
					}
					listItem.Selected = (current.MissionId == this._mission.MissionId);
					this.tripList.Attributes["onchange"] = "switchMission();";
					this.tripList.Items.Add(listItem);
				}
			}
			if (this._mission != null && string.IsNullOrEmpty(base.Request.QueryString["token"]))
			{
				this.BuildFields();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			this.RegisterScripts();
			this._requireGoerApproval = bool.Parse(this.RequireGoerApprovalSetting);
            this.suppliedFirstName = Request.QueryString["firstname"];
            this.suppliedLastName = Request.QueryString["lastname"];

			if (!this.Page.IsPostBack)
			{
				if (this._mission == null)
				{
					this.messageLabel.Text = "There are no available Mission Trips to load";
					this.tripRegistrationWizard.Visible = false;
					this.messagePanel.Visible = true;
				}
				else
				{
					if (!bool.Parse(this.ChooseTripSetting))
					{
						this.tripList.Visible = false;
						this.lcTripName.Text = this._mission.Name;
					}
					else
					{
						this.tripList.Visible = true;
						this.lcTripName.Text = "";
					}
					this.LoadMissionDetails();
					this.LoadActions();
					this.LoadAttendees();
					this.SetPaymentAmount();
					this.paymentMethodList.Items[0].Enabled = (this._ccGatewayAcct != null && this._ccGatewayAcct.GatewayAccountId != -1 && this._ccGatewayAcct.PaymentProcessorID != -1);
					this.paymentMethodList.Items[1].Enabled = (this._achGatewayAcct != null && this._achGatewayAcct.GatewayAccountId != -1 && this._achGatewayAcct.PaymentProcessorID != -1);
					if (!this.paymentMethodList.Items[0].Enabled && !this.paymentMethodList.Items[1].Enabled)
					{
						throw new ArenaApplicationException("Trip Registration requires a valid Gateway Provider");
					}
					Processor processor = null;
					Processor processor2 = null;
					if (this.paymentMethodList.Items[0].Enabled)
					{
						processor = Processor.GetProcessorClass(this._ccGatewayAcct.PaymentProcessor);
						this.paymentMethodList.SelectedIndex = 0;
					}
					else
					{
						this.paymentMethodList.SelectedIndex = 1;
					}
					if (this.paymentMethodList.Items[1].Enabled)
					{
						processor2 = Processor.GetProcessorClass(this._achGatewayAcct.PaymentProcessor);
					}
					this.SetPaymentMethod();
					if ((processor == null || !processor.UseSeparateNameFields) && (processor2 == null || !processor2.UseSeparateNameFields))
					{
						this.lastNameRow.Visible = false;
						this.lastNameRFV.Enabled = false;
						this.firstNameLabel.Text = "Name";
						this.firstNameRFV.ErrorMessage = "Name is required";
						this.firstName.Width = Unit.Pixel(200);
						this.firstName.MaxLength = 100;
						this._showLastName = false;
					}
					this.LoadBillingInfo();
					if (!this.paymentMethodList.Items[0].Enabled || !this.paymentMethodList.Items[1].Enabled)
					{
						this.paymentMethodPanel.Style.Add("display", "none");
					}
					this.accountType.SelectedIndex = 0;
					this.expYearList.Items.Add(new ListItem("Year", ""));
					int year = DateTime.Now.Year;
					for (int i = 0; i < 10; i++)
					{
						string text = (year + i).ToString();
						this.expYearList.Items.Add(new ListItem(text, text));
					}
				}
			}
			if (!string.IsNullOrEmpty(base.Request.QueryString["token"]))
			{
				try
				{
					Dictionary<string, object> dictionary = base.Session[base.Request.QueryString["token"]] as Dictionary<string, object>;
					if (dictionary != null)
					{
						Mission_Contrib missionRegistration = dictionary["Data"] as Mission_Contrib;
						if (missionRegistration != null)
						{
							this.tripList.SelectedValue = missionRegistration.tripList.SelectedValue;
							this._mission = new Mission(int.Parse(this.tripList.SelectedValue));
							this.LoadActions();
							this.registrationAction.SelectedValue = missionRegistration.registrationAction.SelectedValue;
							this.LoadMissionDetails();
							this.LoadAttendees();
							this.SetPaymentAmount();
							this.attendeeFirstName.Text = missionRegistration.attendeeFirstName.Text;
							this.attendeeLastName.Text = missionRegistration.attendeeLastName.Text;
							this.hiddenPersonID.Value = missionRegistration.hiddenPersonID.Value;
							//this.depositCost.Checked = missionRegistration.depositCost.Checked;
							//this.registrationDeadlineDeposit2Cost.Checked = missionRegistration.registrationDeadlineDeposit2Cost.Checked;
							//this.registrationDeadlineDeposit3Cost.Checked = missionRegistration.registrationDeadlineDeposit3Cost.Checked;
							//this.customPayment.Checked = missionRegistration.customPayment.Checked;
							//this.balanceDue.Checked = missionRegistration.balanceDue.Checked;
							this.customPaymentAmount.Text = missionRegistration.customPaymentAmount.Text;
							this.attendeeList.SelectedValue = missionRegistration.attendeeList.SelectedValue;
							this.anonymousFlag.Checked = missionRegistration.anonymousFlag.Checked;
							this.cbStatus.Checked = missionRegistration.cbStatus.Checked;
							this.BuildFields();
							MissionGoer missionGoer = new MissionGoer();
							using (List<CustomField>.Enumerator enumerator = this._mission.Fields.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									MissionField missionField = (MissionField)enumerator.Current;
									if (missionField.Visible)
									{
										MissionGoerFieldValue missionGoerFieldValue = new MissionGoerFieldValue(missionField.CustomFieldId);
										missionGoerFieldValue.SelectedValue = DynamicFieldHelper.GetFieldValue(missionRegistration.tblTripInfo, missionField.FieldType, missionGoerFieldValue, "mission_goer_" + missionField.CustomFieldId.ToString());
										missionGoer.FieldValues.Add(missionGoerFieldValue);
									}
								}
							}
							using (List<CustomFieldModule>.Enumerator enumerator2 = this._mission.FieldModules.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									MissionFieldModule missionFieldModule = (MissionFieldModule)enumerator2.Current;
									using (List<CustomField>.Enumerator enumerator3 = missionFieldModule.CustomFieldModuleFields.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											CustomFieldModuleField customFieldModuleField = (CustomFieldModuleField)enumerator3.Current;
											if (customFieldModuleField.Visible)
											{
												MissionGoerFieldValue missionGoerFieldValue2 = new MissionGoerFieldValue(customFieldModuleField.CustomFieldId);
												missionGoerFieldValue2.SelectedValue = DynamicFieldHelper.GetFieldValue(missionRegistration.tblTripInfo, customFieldModuleField.FieldType, missionGoerFieldValue2, "mission_goer_" + customFieldModuleField.CustomFieldId.ToString());
												missionGoer.FieldValues.Add(missionGoerFieldValue2);
											}
										}
									}
								}
							}
							DynamicFieldHelper.SetFieldValues(this.tblTripInfo, "mission_goer_", this._mission.Fields, missionGoer.FieldValues);
							DynamicFieldHelper.SetFieldValues(this.tblTripInfo, "mission_goer_", this._mission.FieldModules, missionGoer.FieldValues);
							base.Session[base.Request.QueryString["token"]] = dictionary;
						}
						if (!this.Page.IsPostBack)
						{
							if (!this.SubmitTransaction())
							{
								this.tripRegistrationWizard.ActiveStepIndex = 2;
								if (this.tripInfoErrorMsgPanel.Controls.Count > 0)
								{
									LiteralControl literalControl = this.tripInfoErrorMsgPanel.Controls[0] as LiteralControl;
									this.DisplayError(literalControl.Text);
								}
							}
							else
							{
								this.tripRegistrationWizard.ActiveStepIndex = 4;
							}
						}
					}
					else
					{
						this.DisplayError("Invalid Session Detected");
					}
				}
				catch (Exception ex)
				{
					this.DisplayError(ex.Message);
				}
			}
			int num = 4;
			int num2 = 0;
			if (this._mission != null)
			{
				for (int j = 0; j < this._mission.Fields.Count; j++)
				{
					if (this._mission.Fields[j].Visible)
					{
						this.tblTripInfo.Rows[num + j].Visible = (this.registrationAction.SelectedIndex == 0);
						num2++;
					}
				}
				num += num2;
				using (List<CustomFieldModule>.Enumerator enumerator4 = this._mission.FieldModules.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						MissionFieldModule missionFieldModule2 = (MissionFieldModule)enumerator4.Current;
						num2 = 0;
						for (int k = 0; k < missionFieldModule2.CustomFieldModuleFields.Count; k++)
						{
							if (missionFieldModule2.CustomFieldModuleFields[k].Visible)
							{
								this.tblTripInfo.Rows[num + k].Visible = (this.registrationAction.SelectedIndex == 0);
								num2++;
							}
						}
						num += num2;
					}
				}
			}
		}
		private void BuildFields()
		{
			if (this._mission != null)
			{
				this._fieldHelper = new DynamicFieldHelper();
				this._fieldHelper.LabelCssClass = "tripRegistrationLabel";
				this._fieldHelper.LabelRequiredCssClass = "tripRegistrationRequiredLabel";
				this._fieldHelper.FormCssClass = "tripRegistrationForm";
				int num = 4;
				this._fieldHelper.InsertFieldRows(this.tblTripInfo, base.CurrentArenaContext, num, "mission_goer_", this._mission.Fields, new CustomFieldValueCollection(), true);
				num += this._mission.Fields.Count;
				this._fieldHelper.InsertFieldRows(this.tblTripInfo, base.CurrentArenaContext, num, "mission_goer_", this._mission.FieldModules, new CustomFieldValueCollection(), true);
			}
		}
		private void registrationAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._mission = new Mission(int.Parse(this.tripList.SelectedValue));
			this.LoadAttendees();
			this.SetPaymentAmount();
			this.SetBalanceDue();
		}
		private void paymentAmountList_CheckedChanged(object sender, EventArgs e)
		{
			//this.customPaymentAmount.Enabled = this.customPayment.Checked;
            this.customPaymentAmount.Enabled = true;
			this.rfvCustomAmount.Enabled = this.customPaymentAmount.Enabled;
			this.rvCustomAmount.Enabled = this.customPaymentAmount.Enabled;
			this.LoadMissionDetails();
			this.SetBalanceDue();
		}
		private void attendeeList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._mission = new Mission(int.Parse(this.tripList.SelectedValue));
			this.SetBalanceDue();
		}
		private void paymentMethodList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._mission = new Mission(int.Parse(this.tripList.SelectedValue));
			this.SetPaymentMethod();
		}
		private void tripRegistrationWizard_ActiveStepChanged(object sender, EventArgs e)
		{
			if (this.tripRegistrationWizard.ActiveStep.ID == "verificationStep")
			{
				this.LoadVerificationPanel();
			}
			if (this.tripRegistrationWizard.ActiveStep.ID == "paymentInformationStep")
			{
				if (this._ccGatewayAcct != null && this._ccGatewayAcct.RequiresPaymentGateway)
				{
					this.ccPanel.Visible = false;
					this.tblRedirectWarning.Visible = true;
				}
				if (this._achGatewayAcct != null && this._achGatewayAcct.RequiresPaymentGateway)
				{
					this.achPanel.Visible = false;
					this.tblRedirectWarning.Visible = true;
				}
			}
		}
		private void tripRegistrationWizard_NextButtonClick(object sender, WizardNavigationEventArgs e)
		{
			if (this.tripRegistrationWizard.ActiveStep.ID == "tripInformationStep")
			{
				if (this.attendeeTextRow.Visible && this.hiddenPersonID.Value == "" && this.attendeeFirstName.Text.Trim().Length + this.attendeeLastName.Text.Trim().Length > 0)
				{
					this.DisplayError("You must verify that the person you have entered is registered for this Mission Trip");
					e.Cancel = true;
				}
				if (decimal.Parse(this.GetPaymentAmount()) == 0m)
				{
					this.tripRegistrationWizard.ActiveStepIndex = 3;
				}
			}
			if (this.tripRegistrationWizard.ActiveStep.ID == "billingInformationStep")
			{
				this.SetGatewayRedirectMessage();
			}
			if (this.tripRegistrationWizard.ActiveStep.ID == "paymentInformationStep")
			{
				GatewayAccount gatewayAccount;
				if (this.paymentMethodList.SelectedValue == "Credit Card")
				{
					gatewayAccount = this._ccGatewayAcct;
				}
				else
				{
					gatewayAccount = this._achGatewayAcct;
				}
				if (gatewayAccount.RequiresPaymentGateway)
				{
					this.RedirectToGateway();
					e.Cancel = true;
					return;
				}
				if (!this.SubmitPreAuthorization())
				{
					e.Cancel = true;
				}
			}
		}
		private void SaveGoerFieldValues(MissionGoer goer)
		{
			using (List<CustomField>.Enumerator enumerator = this._mission.Fields.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MissionField missionField = (MissionField)enumerator.Current;
					if (missionField.Visible)
					{
						MissionGoerFieldValue missionGoerFieldValue = goer.FieldValues.FindById(missionField.CustomFieldId);
						if (missionGoerFieldValue == null)
						{
							missionGoerFieldValue = new MissionGoerFieldValue(missionField.CustomFieldId);
							goer.FieldValues.Add(missionGoerFieldValue);
						}
						missionGoerFieldValue.SelectedValue = DynamicFieldHelper.GetFieldValue(this.tblTripInfo, missionField.FieldType, missionGoerFieldValue, "mission_goer_" + missionField.CustomFieldId.ToString());
					}
				}
			}
			using (List<CustomFieldModule>.Enumerator enumerator2 = this._mission.FieldModules.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					MissionFieldModule missionFieldModule = (MissionFieldModule)enumerator2.Current;
					using (List<CustomField>.Enumerator enumerator3 = missionFieldModule.CustomFieldModuleFields.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							CustomFieldModuleField customFieldModuleField = (CustomFieldModuleField)enumerator3.Current;
							if (customFieldModuleField.Visible)
							{
								MissionGoerFieldValue missionGoerFieldValue2 = goer.FieldValues.FindById(customFieldModuleField.CustomFieldId);
								if (missionGoerFieldValue2 == null)
								{
									missionGoerFieldValue2 = new MissionGoerFieldValue(customFieldModuleField.CustomFieldId);
									goer.FieldValues.Add(missionGoerFieldValue2);
								}
								missionGoerFieldValue2.SelectedValue = DynamicFieldHelper.GetFieldValue(this.tblTripInfo, customFieldModuleField.FieldType, missionGoerFieldValue2, "mission_goer_" + customFieldModuleField.CustomFieldId.ToString());
							}
						}
					}
				}
			}
		}
		private void tripRegistrationWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
		{
			this._mission = new Mission(int.Parse(this.tripList.SelectedValue));
			if (!this.SubmitTransaction())
			{
				this.LoadVerificationPanel();
				e.Cancel = true;
				return;
			}
			if (decimal.Parse(this.GetPaymentAmount()) == 0m)
			{
				this.confirmationNumberPanel.Controls.Add(new LiteralControl("Thank you for your registration!"));
			}
		}
		private void tripRegistrationWizard_CancelButtonClick(object sender, EventArgs e)
		{
			base.Response.Redirect("~/default.aspx?page=" + this.CancelPageSetting, true);
		}
		private void verifyAttendee_Click(object sender, EventArgs e)
		{
			this._mission = new Mission(int.Parse(this.tripList.SelectedValue));
			this.VerifyAttendeeName();
            
		}
		private void SetGatewayRedirectMessage()
		{
			if (this.GetSelectedPaymentGateway().RequiresPaymentGateway)
			{
				this.lblRedirectMsg.Visible = true;
				StringBuilder stringBuilder = new StringBuilder(340);
				stringBuilder.Append(string.Format("You will be redirected to a secure {0} site. ", this.GetSelectedPaymentGateway().ProcessorClass.GatewayName));
				stringBuilder.Append("For more information about the redirect, click ");
				stringBuilder.Append("<a href=\"#\" onclick=\"window.open('PDSMessage.html','popup','width=500,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false\">here</a>.");
				this.lblRedirectMsg.Text = stringBuilder.ToString();
			}
		}
		private GatewayAccount GetSelectedPaymentGateway()
		{
			string selectedValue;
			if ((selectedValue = this.paymentMethodList.SelectedValue) != null)
			{
				if (selectedValue == "Credit Card")
				{
					return this._ccGatewayAcct;
				}
				if (selectedValue == "Bank Account")
				{
					return this._achGatewayAcct;
				}
			}
			throw new ArenaApplicationException("Error Determining Selected Payment Method");
		}
		private void RegisterScripts()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("function showProcessing(element){\n");
			stringBuilder.Append("\telement.disabled = true;\n");
			stringBuilder.AppendFormat("\tdocument.getElementById('{0}').disabled = true;\n", this.tripRegistrationWizard.FindControl("FinishNavigationTemplateContainerID").FindControl("FinishCompleteButton").ClientID);
			stringBuilder.AppendFormat("\tdocument.getElementById('{0}').disabled = true;\n", this.tripRegistrationWizard.FindControl("FinishNavigationTemplateContainerID").FindControl("FinishPreviousButton").ClientID);
			stringBuilder.AppendFormat("\tdocument.getElementById('{0}').disabled = true;\n", this.tripRegistrationWizard.FindControl("FinishNavigationTemplateContainerID").FindControl("FinishCancel").ClientID);
			stringBuilder.AppendFormat("\tdocument.getElementById('FinishLabel').style.display = '';\n", this.tripRegistrationWizard.FindControl("FinishNavigationTemplateContainerID").FindControl("FinishCompleteButton").ClientID);
			stringBuilder.AppendFormat("\t{0}\n", this.Page.ClientScript.GetPostBackEventReference(this.tripRegistrationWizard.FindControl("FinishNavigationTemplateContainerID").FindControl("FinishCompleteButton"), ""));
			stringBuilder.Append("return false");
			stringBuilder.Append("}\n");
			ScriptManager.RegisterClientScriptBlock(this.tripRegistrationWizard, typeof(string), "TripWizardScripts", stringBuilder.ToString(), true);
		}
		private void RedirectToGateway()
		{
			try
			{
				decimal transactionAmount = decimal.Parse(this.GetPaymentAmount());
				GatewayAccount gatewayAccount;
				if (this.paymentMethodList.SelectedValue == "Credit Card")
				{
					gatewayAccount = this._ccGatewayAcct;
				}
				else
				{
					gatewayAccount = this._achGatewayAcct;
				}
				Processor.GetProcessorClass(gatewayAccount.PaymentProcessor);
				if (gatewayAccount.RequiresPaymentGateway)
				{
					gatewayAccount.PopulateProcessor(TransactionType.Sale, base.CurrentPerson.PersonID, this.firstName.Text, this.firstName.Text, this.lastName.Text, this.address1.Text, this.city.Text, this.state.Text, this.zip.Text, this.emailAddress.Text, transactionAmount, DateTime.MinValue, PaymentFrequency.Unknown, 0, ArenaUrl.FullApplicationRoot(), null);
					string gatewayUrl = gatewayAccount.GatewayUrl;
					if (gatewayUrl == string.Empty)
					{
						this.DisplayError("Correct the following", gatewayAccount.ProcessorClass.Messages);
					}
					else
					{
						Dictionary<string, object> dictionary = new Dictionary<string, object>();
						dictionary.Add("Data", this);
						dictionary.Add("UrlReferrer", base.Request.UrlReferrer);
						base.Session[gatewayAccount.ProcessorClass.Token] = dictionary;
						base.Response.Redirect(gatewayUrl);
					}
				}
			}
			catch (Exception ex)
			{
				this.DisplayError(ex.Message);
			}
		}
		private bool SubmitPreAuthorization()
		{
			try
			{
				decimal transactionAmount = decimal.Parse(this.GetPaymentAmount());
				GatewayAccount gatewayAccount;
				if (this.paymentMethodList.SelectedValue == "Credit Card")
				{
					gatewayAccount = this._ccGatewayAcct;
				}
				else
				{
					gatewayAccount = this._achGatewayAcct;
				}
				Processor processorClass = Processor.GetProcessorClass(gatewayAccount.PaymentProcessor);
				if (processorClass != null && processorClass.TransactionTypeSupported(TransactionType.PreAuth))
				{
					bool flag;
					if (this.paymentMethodList.SelectedValue == "Credit Card")
					{
						flag = gatewayAccount.Authorize(TransactionType.PreAuth, this.ccNumber.Text, this.ccCIN.Text, this.expMonthList.SelectedValue, this.expYearList.SelectedValue, -1, base.CurrentPerson.PersonID, this.firstName.Text, this.firstName.Text, this.lastName.Text, this.address1.Text, this.city.Text, this.state.Text, this.zip.Text, this.homePhone.Text, this.emailAddress.Text, transactionAmount, "", DateTime.MinValue, PaymentFrequency.Unknown, 0, this._validateCardNumber);
					}
					else
					{
						flag = gatewayAccount.AuthorizeACH(TransactionType.PreAuth, this.accountNumber.Text.Trim(), this.routingNumber.Text.Trim(), this.accountType.Items[0].Selected, base.CurrentPerson.PersonID, this.firstName.Text.Trim(), this.firstName.Text.Trim(), this.lastName.Text.Trim(), this.address1.Text.Trim(), this.city.Text.Trim(), this.state.Text.Trim(), this.zip.Text.Trim(), this.homePhone.Text.Trim(), this.emailAddress.Text.Trim(), transactionAmount, "", DateTime.MinValue, PaymentFrequency.Unknown, 0);
					}
					if (!flag)
					{
						this.DisplayError("Authorization of your information failed for the following reason(s):", gatewayAccount.Messages);
						return false;
					}
				}
			}
			catch (Exception inner)
			{
				throw new ArenaApplicationException("Error occurred during preauthorization", inner);
			}
			return true;
		}
		private bool SubmitTransaction()
		{
			bool result;
			try
			{
				decimal num = decimal.Parse(this.GetPaymentAmount());
				Mission_Contrib.ActionType actionType = (Mission_Contrib.ActionType)Enum.ToObject(typeof(Mission_Contrib.ActionType), int.Parse(this.registrationAction.SelectedValue));
				int num2 = -1;
				if (actionType == Mission_Contrib.ActionType.Contribute)
				{
					if (this.hiddenPersonID.Value.Trim().Length > 0)
					{
						num2 = int.Parse(this.hiddenPersonID.Value.Trim());
					}
				}
				/*else
				{
					num2 = int.Parse(this.attendeeList.SelectedValue);
				}*/
				MissionGoer missionGoer = null;
				if (actionType == Mission_Contrib.ActionType.Register)
				{
					missionGoer = new MissionGoer();
					missionGoer.PersonID = num2;
					missionGoer.MissionId = this._mission.MissionId;
					this.SaveGoerFieldValues(missionGoer);
					missionGoer.Active = true;
					missionGoer.Approved = false;
					missionGoer.Save(base.CurrentUser.Identity.Name);
				}
				else
				{
					if (num2 != -1)
					{
						missionGoer = this._mission.MissionGoers.FindByID(num2);
					}
				}
				if (num > 0m)
				{
					GatewayAccount gatewayAccount;
					string value;
					if (this.paymentMethodList.SelectedValue == "Credit Card")
					{
						gatewayAccount = this._ccGatewayAcct;
						value = this.MaskAccountNumber(this.ccNumber.Text);
					}
					else
					{
						gatewayAccount = this._achGatewayAcct;
						value = this.MaskAccountNumber(this.accountNumber.Text);
					}
					string text = string.Format("F{0}:{1}", this._mission.PurposeCode.ToString(), num.ToString("C2"));
					ContributionFundCollection contributionFundCollection = new ContributionFundCollection();
					contributionFundCollection.Add(new ContributionFund
					{
						FundId = this._mission.PurposeCode,
						Amount = num
					});
					Transaction transaction = null;
					if (gatewayAccount.RequiresPaymentGateway)
					{
						string text2 = base.Request.QueryString["confid"];
						if (text2 != string.Empty)
						{
							gatewayAccount.ProcessorClass.PaymentFrequency = PaymentFrequency.One_Time;
							if (gatewayAccount.Authorize(text2))
							{
								transaction = gatewayAccount.Transaction;
								transaction.PersonId = ArenaContext.Current.Person.PersonID;
							}
						}
						else
						{
							gatewayAccount.Messages.Add("Invalid Confirmation Id");
						}
					}
					else
					{
						if (this.paymentMethodList.SelectedValue == "Credit Card")
						{
							if (gatewayAccount.Authorize(TransactionType.Sale, this.ccNumber.Text, this.ccCIN.Text, this.expMonthList.SelectedValue, this.expYearList.SelectedValue, -1, base.CurrentPerson.PersonID, this.firstName.Text.Trim(), this.firstName.Text.Trim(), this.lastName.Text.Trim(), this.address1.Text.Trim(), this.city.Text.Trim(), this.state.Text.Trim(), this.zip.Text.Trim(), this.homePhone.Text.Trim(), this.emailAddress.Text.Trim(), num, text, DateTime.MinValue, PaymentFrequency.One_Time, 0, this._validateCardNumber))
							{
								transaction = gatewayAccount.Transaction;
							}
						}
						else
						{
							if (gatewayAccount.AuthorizeACH(TransactionType.Sale, this.accountNumber.Text, this.routingNumber.Text, this.accountType.Items[0].Selected, base.CurrentPerson.PersonID, this.firstName.Text.Trim(), this.firstName.Text.Trim(), this.lastName.Text.Trim(), this.address1.Text.Trim(), this.city.Text.Trim(), this.state.Text.Trim(), this.zip.Text.Trim(), this.homePhone.Text.Trim(), this.emailAddress.Text.Trim(), num, text, DateTime.MinValue, PaymentFrequency.One_Time, 0))
							{
								transaction = gatewayAccount.Transaction;
							}
						}
					}
					if (transaction != null)
					{
						transaction.Save(base.CurrentUser.Identity.Name);
						if (!transaction.Success)
						{
							this.DisplayError("Authorization of your information failed for the following reason(s):", gatewayAccount.Messages);
							result = false;
							return result;
						}
						string arg = "Online Giving";
						if (base.CurrentOrganization.Settings["GivingBatchName"] != null)
						{
							arg = base.CurrentOrganization.Settings["GivingBatchName"];
						}
						BatchType batchType = Batch.GetBatchType(transaction.PaymentMethod.Guid);
						Batch batch = new Batch(base.CurrentOrganization.OrganizationID, true, string.Format("{0} {1}", arg, Enum.GetName(typeof(BatchType), batchType)), transaction.TransactionDate, batchType, gatewayAccount.GatewayAccountId, base.CurrentUser.Identity.Name);
						batch.VerifyAmount += transaction.TransactionAmount;
						batch.Save(base.CurrentUser.Identity.Name);
						Contribution contribution = new Contribution();
						contribution.PersonId = transaction.PersonId;
						contribution.TransactionId = transaction.TransactionId;
						contribution.BatchId = batch.BatchId;
						contribution.ContributionDate = transaction.TransactionDate;
						contribution.CurrencyAmount = transaction.TransactionAmount;
						contribution.TransactionNumber = transaction.TransactionDetail;
						contribution.CurrencyType = transaction.PaymentMethod;
						contribution.AccountNumber = transaction.RepeatingPayment.AccountNumber;
						contribution.ContributionFunds = contributionFundCollection;
						contribution.Memo = text;
						contribution.Save(base.CurrentUser.Identity.Name);
						new MissionContribution
						{
							MissionID = this._mission.MissionId,
							ContributionID = contribution.ContributionId
						}.Save(base.CurrentUser.Identity.Name);
						if (missionGoer != null)
						{
							new MissionContributionGoer
							{
								GoerID = missionGoer.GoerID,
								ContributionID = contribution.ContributionId,
								Amount = contribution.CurrencyAmount,
								IsAnonymous = this.anonymousFlag.Checked
							}.Save(base.CurrentUser.Identity.Name);
						}
						this.confirmationNumberPanel.Controls.Clear();
						this.confirmationNumberPanel.Controls.Add(new LiteralControl("Confirmation Number: " + transaction.TransactionDetail));
						base.Session.Clear();
						try
						{
							MissionTripContribution missionTripContribution = new MissionTripContribution();
							Dictionary<string, string> dictionary = new Dictionary<string, string>();
							dictionary.Add("##FirstName##", base.CurrentPerson.FirstName);
							dictionary.Add("##LastName##", base.CurrentPerson.LastName);
							dictionary.Add("##MissionTrip##", this._mission.Name);
							string value2 = string.Empty;
							switch (actionType)
							{
							case Mission_Contrib.ActionType.Register:
								value2 = "Registration";
								break;
                            case Mission_Contrib.ActionType.Contribute:
								value2 = "Contribution";
								break;
                            case Mission_Contrib.ActionType.Payment:
								value2 = "Payment";
								break;
							}
							dictionary.Add("##RegistrationAction##", value2);
							dictionary.Add("##TripAttendee##", (missionGoer != null) ? missionGoer.Person.FullName : "N/A");
							dictionary.Add("##TransactionAmount##", num.ToString("C2"));
							dictionary.Add("##TransactionDate##", transaction.TransactionDate.ToShortDateString());
							dictionary.Add("##PaymentMethod##", this.paymentMethodList.SelectedValue);
							dictionary.Add("##AccountNumber##", value);
							dictionary.Add("##ConfirmationNumber##", transaction.TransactionDetail);
							dictionary.Add("##OrganizationName##", base.CurrentOrganization.Name);
							missionTripContribution.Send(this.emailAddress.Text.Trim(), dictionary);
							goto IL_86D;
						}
						catch (Exception)
						{
							goto IL_86D;
						}
					}
					this.DisplayError("Authorization of your information failed for the following reason(s):", gatewayAccount.Messages);
					result = false;
					return result;
				}
				IL_86D:
                if (actionType == Mission_Contrib.ActionType.Register)
				{
					try
					{
						MissionTripRegistration missionTripRegistration = new MissionTripRegistration(this._mission.EmailTemplateID);
						Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
						dictionary2.Add("##TripName##", this._mission.Name);
						dictionary2.Add("##TripDescription##", this._mission.Description);
						dictionary2.Add("##DestinationCountry##", this._mission.DestinationCountry.Value);
						dictionary2.Add("##DestinationRegion##", this._mission.DestinationRegion.Value);
						dictionary2.Add("##DestinationCity##", this._mission.DestinationCity.Value);
						dictionary2.Add("##DepartureDate##", this._mission.DepartureDate.ToShortDateString());
						dictionary2.Add("##ReturnDate##", this._mission.ReturnDate.ToShortDateString());
						dictionary2.Add("##TripPurpose##", this._mission.TripPurpose.Value);
						dictionary2.Add("##CostPerPerson##", this._mission.CostPerPerson.ToString("C2"));
						dictionary2.Add("##TripCoordinator##", this._mission.Coordinator.FullName);
						dictionary2.Add("##OrganizationName##", base.CurrentOrganization.Name);
						StringBuilder stringBuilder = new StringBuilder();
						MissionPackingListCollection missionPackingListCollection = new MissionPackingListCollection(this._mission.MissionId);
						foreach (MissionPackingList current in missionPackingListCollection)
						{
							string arg2 = current.IsRequired ? "(Required)" : "";
							stringBuilder.AppendFormat("{0} - {1}, {2}<br/>", current.Item, current.Description, arg2);
						}
						dictionary2.Add("##PackingList##", stringBuilder.ToString());
						dictionary2.Add("##BalanceDue##", missionGoer.AmountDue.ToString("N2"));
						missionTripRegistration.LoadPersonFields(dictionary2, missionGoer.Person);
						List<Attachment> list = new List<Attachment>();
						foreach (MissionDocument current2 in this._mission.MissionDocuments)
						{
							if (current2.IncludeInEmail)
							{
								string text3 = (current2.Title != string.Empty) ? current2.Title : current2.OriginalFileName;
								if (text3 == string.Empty)
								{
									text3 = string.Format("Attachment-{0}", list.Count + 1);
								}
								if (!text3.Contains(".") && current2.FileExtension != string.Empty)
								{
									text3 = text3 + "." + current2.FileExtension;
								}
								list.Add(new Attachment(current2.ByteArray, text3));
							}
						}
						missionTripRegistration.Send(missionGoer.Person.Emails.FirstActive, dictionary2, list);
					}
					catch (Exception)
					{
					}
				}
                if (actionType == Mission_Contrib.ActionType.Register)
				{
					string str = string.Empty;
					string str2 = string.Format("<a href='default.aspx?page={0}&mission={1}'>{2}</a>", base.CurrentPortalPage.PortalPageID, this._mission.MissionId, this._mission.Name);
					if (this.attendeeList.SelectedItem.Value == ArenaContext.Current.Person.PersonID.ToString())
					{
						str = "has registered for the mission trip";
					}
					else
					{
						str = string.Format("has registered {0} for the mission trip", this.attendeeList.SelectedItem.Text);
					}
					this.cbStatus.UpdateStatus(str + " " + str2, string.Empty);
				}
				result = true;
			}
			catch (Exception inner)
			{
				throw new ArenaApplicationException("Error occurred during Authorization", inner);
			}
			return result;
		}
		private string MaskAccountNumber(string accountNumber)
		{
			string text = "";
			for (int i = 0; i < accountNumber.Length - 4; i++)
			{
				text += "X";
			}
			if (accountNumber.Length - 4 > 0)
			{
				text += accountNumber.Substring(accountNumber.Length - 4, 4);
			}
			return text;
		}
		private void DisplayError(string header, List<string> errorMsgs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}\n", header);
			stringBuilder.Append("<ul>\n");
			for (int i = 0; i < errorMsgs.Count; i++)
			{
				stringBuilder.AppendFormat("<li>{0}</li>\n", errorMsgs[i]);
			}
			stringBuilder.Append("</ul>\n");
			this.DisplayError(stringBuilder.ToString());
		}
		private void DisplayError(string errorMsg)
		{
			string iD;
			Panel panel;
			if ((iD = this.tripRegistrationWizard.ActiveStep.ID) != null)
			{
				if (iD == "tripInformationStep")
				{
					panel = this.tripInfoErrorMsgPanel;
					goto IL_77;
				}
				if (iD == "paymentInformationStep")
				{
					panel = this.preAuthErrorMsgPanel;
					goto IL_77;
				}
				if (iD == "verificationStep")
				{
					panel = this.authErrorMsgPanel;
					goto IL_77;
				}
			}
			panel = this.errorMessagePanel;
			IL_77:
			panel.Controls.Clear();
			panel.Controls.Add(new LiteralControl(errorMsg));
			panel.Visible = true;
		}
		private void VerifyAttendeeName()
		{
			MissionGoerCollection missionGoerCollection = new MissionGoerCollection();
			if (this.attendeeFirstName.Text.Trim().Length > 0 && this.attendeeLastName.Text.Trim().Length > 0)
			{
				if (this.attendeeExtraInfoRow.Visible)
				{
					switch (this.extraInfoList.SelectedIndex)
					{
					case 0:
						missionGoerCollection = this._mission.ActiveMissionGoers.FindByName(this.attendeeFirstName.Text.Trim(), this.attendeeLastName.Text.Trim(), this.extraInfo.Text, ExtraInfoType.EmailAddress);
						break;
					case 1:
						missionGoerCollection = this._mission.ActiveMissionGoers.FindByName(this.attendeeFirstName.Text.Trim(), this.attendeeLastName.Text.Trim(), this.extraInfo.Text, ExtraInfoType.PhoneNumber);
						break;
					case 2:
						missionGoerCollection = this._mission.ActiveMissionGoers.FindByName(this.attendeeFirstName.Text.Trim(), this.attendeeLastName.Text.Trim(), this.extraInfo.Text, ExtraInfoType.StreetAddress);
						break;
					}
				}
				else
				{
					missionGoerCollection = this._mission.ActiveMissionGoers.FindByName(this.attendeeFirstName.Text.Trim(), this.attendeeLastName.Text.Trim());
				}
			}
			this.verified.Visible = true;
			switch (missionGoerCollection.Count)
			{
			case 0:
				this.hiddenPersonID.Value = "";
				this.balanceDueAmount.Text = "$" + this._mission.CostPerPerson.ToString("N2");
				this.attendeeExtraInfoRow.Visible = false;
				if (this.attendeeLastName.Text.Trim().Length > 0)
				{
					this.verified.InnerHtml = "The name entered is not registered for this mission trip.";
					this.verified.Attributes.Add("class", "errorText");
					return;
				}
				this.verified.Visible = false;
				return;
			case 1:
			{
				MissionGoer missionGoer = missionGoerCollection[0];
				if (bool.Parse(this.RequireGoerApprovalSetting) && !missionGoer.Approved)
				{
					this.verified.InnerHtml = "The information you entered has been verified however the person that was found has not been approved. You can either give to another person, give just to the mission trip, or wait until they are approved.";
					this.verified.Attributes.Add("class", "errorText");
					this.attendeeFirstName.Focus();
					return;
				}
				decimal d = missionGoer.AmountDue;
				if (d < 0m)
				{
					d = 0m;
				}
                if (d > 1)
                {
                    this.balanceDueAmount.Text = "$" + d.ToString("N2");
                    this.balanceDue.Visible = true;
                    this.balanceDueAmount.Visible = true;
                }
                else
                {
                    this.balanceDue.Visible = false;
                    this.balanceDueAmount.Visible = false;
                }
                if (this._mission.CostPerPerson - missionGoer.DiscountAmount > 1)
                {
                    this.registrationCostAmount.Text = "$" + (this._mission.CostPerPerson - missionGoer.DiscountAmount).ToString("N2");
                    this.registrationCostAmount.Visible = true;
                    this.registrationCost.Visible = true;
                }
                else
                {
                    this.registrationCostAmount.Visible = false;
                    this.registrationCost.Visible = false;
                }
				this.hiddenPersonID.Value = missionGoer.PersonID.ToString();
				this.verified.InnerHtml = "Verified";
				this.verified.Attributes.Add("class", "verifyLabel");
				this.attendeeExtraInfoRow.Visible = false;
				return;
			}
			default:
				this.hiddenPersonID.Value = "";
				this.balanceDueAmount.Text = "$" + this._mission.CostPerPerson.ToString("N2");
				this.attendeeExtraInfoRow.Visible = true;
				this.extraInfoList.Focus();
				this.verified.InnerHtml = "The name entered has more than one match. Please provide more information to find the correct person.";
				this.verified.Attributes.Add("class", "errorText");
				return;
			}
		}
		private void LoadMissionDetails()
		{
			this.depositCostAmount.Text = "$" + this._mission.DepositAmount.ToString("N2");
			this.registrationDeadlineDeposit2Amount.Text = "$" + this._mission.RegistrationDeposit1Amount.ToString("N2");
			this.registrationDeadlineDeposit3Amount.Text = "$" + this._mission.RegistrationDeposit2Amount.ToString("N2");
			this.registrationCostAmount.Text = "$" + this._mission.CostPerPerson.ToString("N2");
			this.balanceDueAmount.Text = "$" + this._mission.CostPerPerson.ToString("N2");
            this.contGoal1Date.Text = this._mission.DepositDeadline.ToShortDateString();
            this.contGoal2Date.Text = this._mission.RegistrationDeposit1Deadline.ToShortDateString();
            this.contGoal3Date.Text = this._mission.RegistrationDeposit2Deadline.ToShortDateString();
		}
		private void LoadActions()
		{
			bool flag = false;
			/*foreach (MissionGoer current in this._mission.ActiveMissionGoers)
			{
				if (current.PersonID == base.CurrentPerson.PersonID || current.Person.Family().FamilyID == base.CurrentPerson.Family().FamilyID)
				{
					flag = true;
					break;
				}
			}*/
			this.registrationAction.Items.Clear();
			/*if (this._mission.ActiveMissionGoers.Count < this._mission.MaxGoers || this._mission.MaxGoers == 0)
			{
                this.registrationAction.Items.Add(new ListItem("Contribute to a trip for yourself or a family member", Convert.ToString(1)));
			}*/
			this.registrationAction.Items.Add(new ListItem("Contribute to the mission trip on behalf of a team member", Convert.ToString(2)));
			this.registrationAction.SelectedIndex = 0;
			if (flag)
			{
				this.registrationAction.Items.Add(new ListItem("Make a Payment for yourself or a family member", Convert.ToString(3)));
				this.registrationAction.SelectedIndex = this.registrationAction.Items.Count - 1;
			}
            this.registrationAction.Items.Add(new ListItem("Make a general contribution to the mission team", Convert.ToString(4)));
		}
		private void LoadAttendees()
		{
            Mission_Contrib.ActionType actionType = (Mission_Contrib.ActionType)Enum.ToObject(typeof(Mission_Contrib.ActionType), int.Parse(this.registrationAction.SelectedValue));
			this.attendeeList.Items.Clear();
			this.attendeeFirstName.Text = string.Empty;
			this.attendeeLastName.Text = string.Empty;
			this.hiddenPersonID.Value = "";
			this.verified.Visible = false;
			this.lblMissingAttendees.Visible = false;
			this.lblNoAvailableAttendees.Text = "There are no eligible team members.";
			switch (actionType)
			{
                case Mission_Contrib.ActionType.Register:
			{
				Family family = base.CurrentPerson.Family();
				if (family != null && family.FamilyID != -1)
				{
					using (List<FamilyMember>.Enumerator enumerator = family.FamilyMembers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							FamilyMember current = enumerator.Current;
							if (this._mission.MissionGoers.FindByID(current.PersonID) == null)
							{
								this.attendeeList.Items.Add(new ListItem(current.FullName, current.PersonID.ToString()));
							}
						}
						goto IL_198;
					}
				}
				if (this._mission.MissionGoers.FindByID(base.CurrentPerson.PersonID) == null)
				{
					this.attendeeList.Items.Add(new ListItem(base.CurrentPerson.FullName, base.CurrentPerson.PersonID.ToString()));
				}
				IL_198:
				this.attendeeListRow.Visible = true;
				this.attendeeTextRow.Visible = false;
				this.anonymousTextRow.Visible = false;
				this.rfvCustomAmount.Enabled = true;
				this.hiddenPersonID.Value = this.attendeeList.SelectedValue;
				break;
			}
                case Mission_Contrib.ActionType.Contribute:
            {
                this.attendeeListRow.Visible = false;
                this.attendeeTextRow.Visible = true;
                this.anonymousTextRow.Visible = true;
                this.attendeeFirstName.Focus();
                break;
            }
                case Mission_Contrib.ActionType.GenContribute:
            {
                this.attendeeListRow.Visible = false;
                this.attendeeTextRow.Visible = false;
                this.anonymousTextRow.Visible = true;
                break;
            }
                case Mission_Contrib.ActionType.Payment:
			{
				bool visible = false;
				foreach (MissionGoer current2 in this._mission.ActiveMissionGoers)
				{
					if (current2.Person.Family().FamilyID == base.CurrentPerson.Family().FamilyID)
					{
						if (!this._requireGoerApproval || current2.Approved)
						{
							this.attendeeList.Items.Add(new ListItem(current2.Person.FullName, current2.PersonID.ToString()));
						}
						else
						{
							visible = true;
						}
					}
				}
				if (this._requireGoerApproval && this.attendeeList.Items.Count == 0)
				{
					Label expr_2F5 = this.lblNoAvailableAttendees;
					expr_2F5.Text += "<br />Team members are not eligible to receive contributions<br />until they have been approved for the mission trip.";
				}
				this.lblMissingAttendees.Visible = visible;
				this.attendeeList.Enabled = true;
				this.attendeeListRow.Visible = true;
				this.attendeeTextRow.Visible = false;
				this.anonymousTextRow.Visible = false;
				this.hiddenPersonID.Value = this.attendeeList.SelectedValue;
				break;
			}
			}
			this.lblNoAvailableAttendees.Visible = (this.attendeeList.Items.Count == 0);
			this.attendeeList.Style["display"] = ((this.attendeeList.Items.Count == 0) ? "none" : "block");
            this.cbUpdateGroupStatus.Visible = (actionType == Mission_Contrib.ActionType.Register && this.attendeeList.Items.Count > 0);
            this.cbStatus.Enabled = (actionType == Mission_Contrib.ActionType.Register && this.attendeeList.Items.Count > 0);
			this.SetBalanceDue();
            this.attendeeFirstName.Text = this.suppliedFirstName;
            this.attendeeLastName.Text = this.suppliedLastName;
            this.VerifyAttendeeName();
            
		}
		private void LoadVerificationPanel()
		{
			this.verificationPlaceholder.Controls.Clear();
			HtmlTable htmlTable = new HtmlTable();
			this.verificationPlaceholder.Controls.Add(htmlTable);
			htmlTable.CellPadding = 0;
			htmlTable.CellSpacing = 5;
			htmlTable.Attributes["class"] = "verificationTable";
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTable.Rows.Add(htmlTableRow);
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Mission Trip";
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
			htmlTableCell.InnerText = this.tripList.SelectedItem.Text;
			htmlTableRow = new HtmlTableRow();
			htmlTable.Rows.Add(htmlTableRow);
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Action";
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
			htmlTableCell.InnerText = this.registrationAction.SelectedItem.Text;
			htmlTableRow = new HtmlTableRow();
			htmlTable.Rows.Add(htmlTableRow);
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Team Member";
            Mission_Contrib.ActionType actionType = (Mission_Contrib.ActionType)Enum.ToObject(typeof(Mission_Contrib.ActionType), int.Parse(this.registrationAction.SelectedValue));
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
            htmlTableCell.InnerText = ((actionType == Mission_Contrib.ActionType.Contribute) ? string.Format("{0} {1}", this.attendeeFirstName.Text, this.attendeeLastName.Text) : "Team Support");//this.attendeeList.SelectedItem.Text);
			if (decimal.Parse(this.GetPaymentAmount()) > 0m)
			{
				htmlTableRow = new HtmlTableRow();
				htmlTable.Rows.Add(htmlTableRow);
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
				htmlTableCell.InnerText = "Payment Amount";
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
				htmlTableCell.InnerText = this.GetPaymentAmount();
				htmlTableRow = new HtmlTableRow();
				htmlTable.Rows.Add(htmlTableRow);
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
				htmlTableCell.InnerText = "Billing Name";
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
				htmlTableCell.InnerText = string.Format("{0} {1}", this.firstName.Text, this.lastName.Text);
				htmlTableRow = new HtmlTableRow();
				htmlTable.Rows.Add(htmlTableRow);
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
				htmlTableCell.InnerText = "Billing Email";
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
				htmlTableCell.InnerText = this.emailAddress.Text;
				htmlTableRow = new HtmlTableRow();
				htmlTable.Rows.Add(htmlTableRow);
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
				htmlTableCell.InnerText = "Billing Phone";
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
				htmlTableCell.InnerText = this.homePhone.Text;
				htmlTableRow = new HtmlTableRow();
				htmlTable.Rows.Add(htmlTableRow);
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
				htmlTableCell.InnerText = "Billing Address";
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
				htmlTableCell.InnerText = this.address1.Text;
				htmlTableRow = new HtmlTableRow();
				htmlTable.Rows.Add(htmlTableRow);
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
				htmlTableCell.InnerText = "Billing City/State/Zip";
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
				htmlTableCell.InnerText = string.Format("{0}, {1} {2}", this.city.Text, this.state.Text, this.zip.Text);
				htmlTableRow = new HtmlTableRow();
				htmlTable.Rows.Add(htmlTableRow);
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
				htmlTableCell.InnerText = "Payment Method";
				htmlTableCell = new HtmlTableCell();
				htmlTableRow.Cells.Add(htmlTableCell);
				htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
				htmlTableCell.InnerText = this.paymentMethodList.SelectedItem.Text;
				if (this.paymentMethodList.SelectedValue == "Credit Card")
				{
					this.AddCCRows(htmlTable);
					return;
				}
				this.AddCheckRows(htmlTable);
			}
		}
		private void AddCCRows(HtmlTable table)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			table.Rows.Add(htmlTableRow);
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Card Number";
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
			if (this.ccNumber.Text.Length == 15)
			{
				htmlTableCell.InnerText = "XXXX-XXXXXX-" + this.ccNumber.Text.Substring(10);
			}
			else
			{
				if (this.ccNumber.Text.Length > 12)
				{
					htmlTableCell.InnerText = "XXXX-XXXX-XXXX-" + this.ccNumber.Text.Substring(12);
				}
			}
			htmlTableRow = new HtmlTableRow();
			table.Rows.Add(htmlTableRow);
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Expiration Date";
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
			htmlTableCell.InnerText = this.expMonthList.SelectedItem.Text + "/" + this.expYearList.SelectedItem.Text;
		}
		private void AddCheckRows(HtmlTable table)
		{
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			table.Rows.Add(htmlTableRow);
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Bank Name";
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
			htmlTableCell.InnerText = this.bankName.Text;
			htmlTableRow = new HtmlTableRow();
			table.Rows.Add(htmlTableRow);
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Account Type";
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
			htmlTableCell.InnerText = this.accountType.SelectedValue;
			htmlTableRow = new HtmlTableRow();
			table.Rows.Add(htmlTableRow);
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Account Number";
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
			htmlTableCell.InnerText = this.accountNumber.Text;
			htmlTableRow = new HtmlTableRow();
			table.Rows.Add(htmlTableRow);
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmLabel";
			htmlTableCell.InnerText = "Routing Number";
			htmlTableCell = new HtmlTableCell();
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableCell.Attributes["class"] = "tripRegistrationConfirmItem";
			htmlTableCell.InnerText = this.routingNumber.Text;
		}
		private void SetPaymentAmount()
		{
			string b = 0.ToString("N2");
            Mission_Contrib.ActionType actionType = (Mission_Contrib.ActionType)Enum.ToObject(typeof(Mission_Contrib.ActionType), int.Parse(this.registrationAction.SelectedValue));
			//this.depositCost.Enabled = true;
			//this.registrationDeadlineDeposit2Cost.Enabled = true;
			//this.registrationDeadlineDeposit3Cost.Enabled = true;
			//this.balanceDue.Enabled = true;
			//this.registrationCost.Enabled = true;
            if (this.depositCostAmount.Text == b && actionType != Mission_Contrib.ActionType.Register)
			{
				//this.depositCost.Enabled = false;
			}
            if ((this.registrationDeadlineDeposit2Amount.Text == b && actionType != Mission_Contrib.ActionType.Register) || this._mission.RegistrationDeposit1Deadline == DateTime.Parse("1/1/1900"))
			{
				//this.registrationDeadlineDeposit2Cost.Enabled = false;
			}
            if ((this.registrationDeadlineDeposit3Amount.Text == b && actionType != Mission_Contrib.ActionType.Register) || this._mission.RegistrationDeposit2Deadline == DateTime.Parse("1/1/1900"))
			{
				//this.registrationDeadlineDeposit3Cost.Enabled = false;
			}
			if (this.balanceDueAmount.Text == b)
			{
				//this.balanceDue.Enabled = false;
			}
			if (this.registrationCostAmount.Text == b)
			{
				//this.registrationCost.Enabled = false;
			}
			switch (actionType)
			{
                case Mission_Contrib.ActionType.Register:
				//this.depositCost.Checked = this.depositCost.Enabled;
				//this.registrationDeadlineDeposit2Cost.Checked = false;
				//this.registrationDeadlineDeposit3Cost.Checked = false;
				//this.balanceDue.Checked = false;
				//this.customPayment.Checked = false;
				//this.registrationCost.Checked = false;
				break;
                case Mission_Contrib.ActionType.Contribute:
                case Mission_Contrib.ActionType.GenContribute:
				//this.depositCost.Checked = false;
				//this.registrationDeadlineDeposit2Cost.Checked = false;
				//this.registrationDeadlineDeposit3Cost.Checked = false;
				//this.customPayment.Checked = true;
				//this.balanceDue.Checked = false;
				//this.registrationCost.Checked = false;
				break;
                case Mission_Contrib.ActionType.Payment:
				//this.depositCost.Checked = false;
				//this.registrationDeadlineDeposit2Cost.Checked = false;
				//this.registrationDeadlineDeposit3Cost.Checked = false;
				//this.balanceDue.Checked = this.balanceDue.Enabled;
				//this.customPayment.Checked = !this.balanceDue.Checked;
				//this.registrationCost.Checked = false;
				break;
			}
			//this.customPaymentAmount.Enabled = this.customPayment.Checked;
            this.customPaymentAmount.Enabled = true;
			this.rfvCustomAmount.Enabled = this.customPaymentAmount.Enabled;
		}
		private string GetPaymentAmount()
		{
			string text;
			
		    text = this.customPaymentAmount.Text;
						
			return text;
		}
		private void LoadBillingInfo()
		{
			if (base.CurrentPerson != null && base.CurrentPerson.PersonID != -1)
			{
				if (this._showLastName)
				{
					this.firstName.Text = base.CurrentPerson.FirstName;
					this.lastName.Text = base.CurrentPerson.LastName;
				}
				else
				{
					this.firstName.Text = base.CurrentPerson.FirstName + " " + base.CurrentPerson.LastName;
				}
				this.emailAddress.Text = base.CurrentPerson.Emails.FirstActive;
				PersonPhone personPhone = base.CurrentPerson.Phones.FindByType(SystemLookup.PhoneType_Home);
				if (personPhone != null)
				{
					this.homePhone.Text = personPhone.Number;
				}
				if (base.CurrentPerson.PrimaryAddress != null && base.CurrentPerson.PrimaryAddress.AddressID != -1)
				{
					this.address1.Text = base.CurrentPerson.PrimaryAddress.StreetLine1;
					this.city.Text = base.CurrentPerson.PrimaryAddress.City;
					this.state.Text = base.CurrentPerson.PrimaryAddress.State;
					this.zip.Text = base.CurrentPerson.PrimaryAddress.PostalCode;
				}
			}
		}
		private void SetBalanceDue()
		{
			if (this.attendeeList.SelectedIndex != -1 && this.attendeeList.Visible)
			{
				this.hiddenPersonID.Value = this.attendeeList.SelectedValue;
			}
			if (!(this.hiddenPersonID.Value != string.Empty))
			{
				this.balanceDueAmount.Text = "$" + this._mission.CostPerPerson.ToString("N2");
				return;
			}
			MissionGoer missionGoer = this._mission.ActiveMissionGoers.FindByID(int.Parse(this.hiddenPersonID.Value));
			if (missionGoer != null)
			{
				decimal d = missionGoer.AmountDue;
				if (d < 0m)
				{
					d = 0m;
				}
				this.balanceDueAmount.Text = "$" + d.ToString("N2");
				this.registrationCostAmount.Text = "$" + (this._mission.CostPerPerson - missionGoer.DiscountAmount).ToString("N2");
				this.hiddenPersonID.Value = missionGoer.PersonID.ToString();
				return;
			}
			this.balanceDueAmount.Text = "$" + this._mission.CostPerPerson.ToString("N2");
		}
		private void SetPaymentMethod()
		{
			this.tblRedirectWarning.Visible = false;
			this.ccNumberRFV.Enabled = this.paymentMethodList.Items[0].Selected;
			this.ccCINRFV.Enabled = this.paymentMethodList.Items[0].Selected;
			this.expMonthRFV.Enabled = this.paymentMethodList.Items[0].Selected;
			this.expYearRFV.Enabled = this.paymentMethodList.Items[0].Selected;
			this.ccPanel.Visible = this.paymentMethodList.Items[0].Selected;
			this.bankNameRFV.Enabled = !this.paymentMethodList.Items[0].Selected;
			this.routingNumberRFV.Enabled = !this.paymentMethodList.Items[0].Selected;
			this.accountNumberRFV.Enabled = !this.paymentMethodList.Items[0].Selected;
			this.achPanel.Visible = !this.paymentMethodList.Items[0].Selected;
			if (this.ccPanel.Visible && this._ccGatewayAcct != null && this._ccGatewayAcct.RequiresPaymentGateway)
			{
				this.ccPanel.Visible = false;
				this.tblRedirectWarning.Visible = true;
			}
			if (this.achPanel.Visible && this._achGatewayAcct != null && this._achGatewayAcct.RequiresPaymentGateway)
			{
				this.achPanel.Visible = false;
				this.tblRedirectWarning.Visible = true;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.registrationAction.SelectedIndexChanged += new EventHandler(this.registrationAction_SelectedIndexChanged);
			//this.balanceDue.CheckedChanged += new EventHandler(this.paymentAmountList_CheckedChanged);
			//this.depositCost.CheckedChanged += new EventHandler(this.paymentAmountList_CheckedChanged);
			//this.registrationDeadlineDeposit2Cost.CheckedChanged += new EventHandler(this.paymentAmountList_CheckedChanged);
			//this.registrationDeadlineDeposit3Cost.CheckedChanged += new EventHandler(this.paymentAmountList_CheckedChanged);
			//this.registrationCost.CheckedChanged += new EventHandler(this.paymentAmountList_CheckedChanged);
			//this.customPayment.CheckedChanged += new EventHandler(this.paymentAmountList_CheckedChanged);
			this.attendeeList.SelectedIndexChanged += new EventHandler(this.attendeeList_SelectedIndexChanged);
			this.paymentMethodList.SelectedIndexChanged += new EventHandler(this.paymentMethodList_SelectedIndexChanged);
			this.tripRegistrationWizard.ActiveStepChanged += new EventHandler(this.tripRegistrationWizard_ActiveStepChanged);
			this.tripRegistrationWizard.NextButtonClick += new WizardNavigationEventHandler(this.tripRegistrationWizard_NextButtonClick);
			this.tripRegistrationWizard.FinishButtonClick += new WizardNavigationEventHandler(this.tripRegistrationWizard_FinishButtonClick);
			this.tripRegistrationWizard.CancelButtonClick += new EventHandler(this.tripRegistrationWizard_CancelButtonClick);
			this.verifyAttendee.Click += new EventHandler(this.verifyAttendee_Click);
		}
	}
}
