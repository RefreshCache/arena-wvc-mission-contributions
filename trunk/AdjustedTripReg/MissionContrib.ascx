    <%@ control language="c#" inherits="ArenaWeb.UserControls.Custom.WVC.AdjustedTripReg.Mod.Mission_Contrib" CodeFile="MissionContrib.ascx.cs" CodeBehind="MissionContrib.ascx.cs"%>
<%@ Register TagPrefix="Arena" Namespace="Arena.Portal.UI" Assembly="Arena.Portal.UI" %>
<script type="text/javascript">
    function switchMission() {
        var ddl = document.getElementById('<%=tripList.ClientID %>');
        if (ddl) {
            window.location = 'default.aspx?page=<%=CurrentPortalPage.PortalPageID %>&mission=' + ddl.value;
        }
    }
</script>
<div class="tripRegistration">
    <asp:UpdatePanel runat="server" ID="registrationPanel">
        <ContentTemplate>
            <asp:Panel id="errorMessagePanel" runat="server" CssClass="tripRegistrationError" Visible="false" />
            <asp:Wizard ID="tripRegistrationWizard" runat="server" DisplaySideBar="false" ActiveStepIndex="0">
                <WizardSteps>
                    <asp:WizardStep ID="tripInformationStep" runat="server" Title="">
                        <h3>Trip Information</h3>
                        <table cellpadding="0" cellspacing="5" border="0" class="tripInfoTable" id="tblTripInfo" runat="server">
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Mission Trip</td>
                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                    <asp:DropDownList ID="tripList" runat="server" AutoPostBack="false" CssClass="tripRegistrationItem" />
                                    <asp:Literal ID="lcTripName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space:nowrap" valign="top">Select an Action</td>
                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="top">
                                    <asp:RadioButtonList ID="registrationAction" runat="server" RepeatDirection="Vertical" AutoPostBack="true" CssClass="tripRegistrationItem" />
                                </td>
                            </tr>
                            <tr id="attendeeListRow" runat="server">
                                <td class="tripRegistrationLabel" style="white-space:nowrap" valign="top">Team Member</td>
                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                    <asp:Label ID="lblNoAvailableAttendees" runat="server" Visible="false" Text="" CssClass="errorText" />
                                    <asp:DropDownList ID="attendeeList" runat="server" AutoPostBack="true" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="rfvAttendee" ControlToValidate="attendeeList" runat="server" CssClass="errorText" Text="*" ErrorMessage="You must have a team member available to select.  Please choose another action." />
                                    <asp:Label ID="lblMissingAttendees" runat="server" CssClass="smallText" Text="* Unapproved team members are not displayed in the list." />
                                </td>
                            </tr>
                            <tr id="attendeeTextRow" runat="server" visible="false">
                                <td class="tripRegistrationLabel" style="white-space:nowrap" valign="top">Team Member</td>
                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="top">
                                    <asp:Panel ID="pnlAttendeeTextRow" runat="server" DefaultButton="verifyAttendee">
                                        <table cellpadding="0" cellspacing="1" border="0">
                                            <tr>
                                                <td class="tripRegistrationItem" style="white-space:nowrap; width: 1%;" valign="middle">
                                                    <asp:Label ID="attendeeFNameLabel" runat="server" Text="First Name: " CssClass="tripRegistrationLabel" />
                                                </td>
                                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                                    <asp:TextBox ID="attendeeFirstName" runat="server" CssClass="tripRegistrationItem" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="top">
                                                    <asp:Label ID="attendeeLNameLabel" runat="server" Text="Last Name: " CssClass="tripRegistrationLabel" />
                                                </td>
                                                <td class="tripRegistrationItem" valign="middle"> 
                                                    <asp:TextBox ID="attendeeLastName" runat="server" CssClass="tripRegistrationItem" /><br /><br />
                                                    <asp:Button ID="verifyAttendee" runat="server" Text="Verify Team Member" CssClass="verifyButton" CausesValidation="false" OnClientClick="hideValues()"/>  
                                                    <div id="verified" runat="server" class="verifyLabel" style="white-space:normal; text-align: left; padding-top: 5px;" />
                                                </td>
                                            </tr>
                                            <tr id="attendeeExtraInfoRow" runat="server" visible="false">
                                                <td class="tripRegistrationLabel" style="white-space:nowrap;text-align:left" valign="top">
                                                    <asp:Label ID="extraInfoLabel" runat="server" Text="Verification Info: " CssClass="tripRegistrationLabel" />
                                                </td>
                                                <td class="tripRegistrationLabel" style="white-space:nowrap;text-align:left" valign="top">
                                                    <asp:RadioButtonList ID="extraInfoList" runat="server" RepeatDirection="Vertical" AutoPostBack="false" CssClass="tripRegistrationItem">
                                                        <asp:ListItem Selected="True" Text="Email Address" Value="0" />
                                                        <asp:ListItem Text="Phone Number" Value="1" />
                                                        <asp:ListItem Text="Street Address" Value="2" />
                                                    </asp:RadioButtonList>
                                                    <asp:TextBox ID="extraInfo" runat="server" CssClass="tripRegistrationItem" Width="250" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr id="anonymousTextRow" runat="server" visible="false">
                                <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Give Anonymously</td>
                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                    <asp:CheckBox ID="anonymousFlag" runat="server" Text="" CssClass="tripRegistrationItem" />
                                </td>
                            </tr>
                            <tr id="cbUpdateGroupStatus" runat="server">
                                <td>&nbsp;</td>
                                <td>
                                    <Arena:StatusCheckBox ID="cbStatus" runat="server" CssClass="tripRegistrationItem" />
                                </td>                            
                            </tr>
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space:nowrap" valign="top">Contribution Goals</td>
                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="top">
                                    <span class="tripRegistrationLabel" id="Goal1Date">Date: </span>
                                    <asp:Label runat="server" ID="contGoal1Date" CssClass="tripRegistrationItem" />
                                    <asp:Label ID="depositCost" runat="server" Text=" Amount: " CssClass="tripRegistrationLabel" />
                                    <asp:Label ID="depositCostAmount" runat="server" CssClass="tripRegistrationItem" />
                                    <br />
                                    <span class="tripRegistrationLabel" id="Goal2Date">Date: </span>
                                    <asp:Label runat="server" ID="contGoal2Date" CssClass="tripRegistrationItem" />
                                    <asp:Label ID="registrationDeadlineDeposit2Cost" runat="server" Text=" Amount: " CssClass="tripRegistrationLabel" />
                                    <asp:Label ID="registrationDeadlineDeposit2Amount" runat="server" CssClass="tripRegistrationItem" />
                                    <br />
                                    <span class="tripRegistrationLabel" id="Goal3Date">Date: </span><asp:Label runat="server" ID="contGoal3Date" CssClass="tripRegistrationItem" />
                                    <asp:Label ID="registrationDeadlineDeposit3Cost" runat="server" Text=" Amount: " CssClass="tripRegistrationLabel" />
                                    <asp:Label ID="registrationDeadlineDeposit3Amount" runat="server" CssClass="tripRegistrationItem" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space:nowrap" valign="top">
                                    <asp:Label ID="registrationCost" runat="server" Text="Mission Trip Cost:" CssClass="tripRegistrationLabel" />
                                </td>
                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="top">
                                    <asp:Label ID="registrationCostAmount" runat="server" CssClass="tripRegistrationItem" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space:nowrap" valign="top">
                                    <asp:Label ID="balanceDue" runat="server" Text="Amount Needed: " CssClass="tripRegistrationLabel" />
                                </td>
                                <td class="tripRegistrationItem" style="white-space:nowrap" valign="top">
                                    <asp:Label ID="balanceDueAmount" runat="server" CssClass="tripRegistrationItem" />
                                    </td>
                                    </tr>
                                    <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="top">My Contribution</td>
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle"> 
                                    <asp:Label ID="customPayment" runat="server" Text="" CssClass="tripRegistrationLabel" />
                                    $<asp:TextBox ID="customPaymentAmount" runat="server" Width="80" Enabled="false" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="rfvCustomAmount" ControlToValidate="customPaymentAmount" runat="server" Text="*" ErrorMessage="An amount is required" CssClass="errorText" />
                                    <asp:RangeValidator ID="rvCustomAmount" ControlToValidate="customPaymentAmount" Type="Currency" runat="server" MinimumValue="1" MaximumValue="1000000" Text="*" ErrorMessage="A valid amount is required." CssClass="errorText" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel id="tripInfoErrorMsgPanel" runat="server" CssClass="tripRegistrationError" Visible="false" />
                    </asp:WizardStep>
                    <asp:WizardStep ID="billingInformationStep" runat="server" Title="">
                        <h3>Billing Information</h3>
                        <table border="0" cellpadding="0" cellspacing="5"  class="billingInfoTable">
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space: nowrap" valign="middle">
                                    <asp:Label ID="firstNameLabel" runat="server" Text="First Name" CssClass="tripRegistrationLabel" />
                                </td>
                                <td class="tripRegistrationItem" style="white-space: nowrap" valign="middle">
                                    <asp:TextBox ID="firstName" runat="server" MaxLength="50" Width="100" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="firstNameRFV" runat="server" ControlToValidate="firstName" CssClass="errorText" Display="Dynamic" ErrorMessage="First Name is required" SetFocusOnError="true" Text=" *" />
                                </td>
                            </tr>
                            <tr id="lastNameRow" runat="server">
                                <td class="tripRegistrationLabel" style="white-space: nowrap" valign="middle">Last Name</td>
                                <td class="tripRegistrationItem" style="white-space: nowrap" valign="middle">
                                    <asp:TextBox ID="lastName" runat="server" MaxLength="50" Width="100" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="lastNameRFV" runat="server" ControlToValidate="lastName" CssClass="errorText" Display="Dynamic" ErrorMessage="Last Name is required" SetFocusOnError="true" Text=" *" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space: nowrap" valign="middle">E-mail Address</td>
                                <td class="tripRegistrationItem" style="white-space: nowrap" valign="middle">
                                    <asp:TextBox ID="emailAddress" runat="server" MaxLength="200" Width="200" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="emailAddressRFV" runat="server" ControlToValidate="emailAddress" CssClass="errorText" Display="Dynamic" ErrorMessage="E-mail Address is required" SetFocusOnError="true" Text=" *" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space: nowrap" valign="middle">Home Phone</td>
                                <td class="tripRegistrationItem" style="white-space: nowrap" valign="middle">
                                    <asp:TextBox ID="homePhone" runat="server" MaxLength="200" Width="200" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="homePhoneRFV" runat="server" ControlToValidate="homePhone" CssClass="errorText" Display="Dynamic" ErrorMessage="Home Phone is required" SetFocusOnError="true" Text=" *" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space: nowrap" valign="middle">Address</td>
                                <td class="tripRegistrationItem" style="white-space: nowrap" valign="middle">
                                    <asp:TextBox ID="address1" runat="server" MaxLength="200" Width="200" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="address1RFV" runat="server" ControlToValidate="address1" CssClass="errorText" Display="Dynamic" ErrorMessage="Billing Address is required" SetFocusOnError="true" Text=" *" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tripRegistrationLabel" style="white-space: nowrap" valign="middle">City/State/Zip</td>
                                <td class="tripRegistrationItem" style="white-space: nowrap" valign="middle">
                                    <asp:TextBox ID="city" runat="server" MaxLength="50" Width="80" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="cityRFV" runat="server" ControlToValidate="city" CssClass="errorText" Display="Dynamic" ErrorMessage="City is required" SetFocusOnError="true" Text=" *" />
                                    <asp:TextBox ID="state" runat="server" MaxLength="2" Width="25" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="stateRFV" runat="server" ControlToValidate="state" CssClass="errorText" Display="Dynamic" ErrorMessage="State is required" SetFocusOnError="true" Text=" *" />
                                    <asp:TextBox ID="zip" runat="server" MaxLength="5" Width="75" CssClass="tripRegistrationItem" />
                                    <asp:RequiredFieldValidator ID="zipRFV" runat="server" ControlToValidate="zip" CssClass="errorText" Display="Dynamic" ErrorMessage="Zip Code is required" SetFocusOnError="true" Text=" *" />
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep ID="paymentInformationStep" runat="server" Title="">
                        <asp:Panel ID="paymentMethodPanel" runat="server">
                            <h3>Payment Method</h3>
                            <table cellpadding="0" cellspacing="5" border="0" class="paymentInfoTable">
                                <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle" />
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                        <asp:RadioButtonList ID="paymentMethodList" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" CssClass="tripRegistrationLabel">
                                            <asp:ListItem Value="Credit Card" Text="Credit Card" />
                                            <asp:ListItem Value="Bank Account" Text="Bank Account" />
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table cellpadding="0" cellspacing="0" border="0" runat="server"
                            id="tblRedirectWarning" visible="false" class="paymentInfoTable">
                            <tr>
                                <td>
                                <p>
                                    <asp:Label ID="lblRedirectMsg" CssClass="redirect" runat="server" Visible="false"></asp:Label>        
                                 </p>                                    
                                    <%--You will then be redirected to a secure Payment Data Systems site. For more information about the redirect, click <a href="#" onclick="window.open('PDSMessage.html','popup','width=500,height=500,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false">here</a>.--%>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="ccPanel" runat="server">
                            <table cellpadding="0" cellspacing="5" border="0" class="paymentInfoTable">
                                <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Card Number</td>
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                        <asp:TextBox ID="ccNumber" runat="server" Width="200" MaxLength="16" CssClass="tripRegistrationItem" />
                                        <asp:Label ID="ccDashMessage" runat="server" CssClass="smallText" Font-Italic="true" Text="Don't include dashes" />
                                        <asp:RequiredFieldValidator ID="ccNumberRFV" ControlToValidate="ccNumber" runat="server" ErrorMessage="Credit Card Number is required" CssClass="errorText" Display="Dynamic" SetFocusOnError="true" Text=" *" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Card Security Code</td>
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                        <asp:TextBox ID="ccCIN" runat="server" Width="50" MaxLength="4" CssClass="tripRegistrationItem" />
                                        <asp:RequiredFieldValidator ID="ccCINRFV" ControlToValidate="ccCIN" runat="server" ErrorMessage="Card Identification Number is required" CssClass="errorText" Display="Dynamic" SetFocusOnError="true" Text=" *" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Expiration Date</td>
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                        <asp:DropDownList ID="expMonthList" runat="server" CssClass="tripRegistrationItem">
                                            <asp:ListItem Text="Month" Value="" />
                                            <asp:ListItem Text="01" Value="01" />
                                            <asp:ListItem Text="02" Value="02" />
                                            <asp:ListItem Text="03" Value="03" />
                                            <asp:ListItem Text="04" Value="04" />
                                            <asp:ListItem Text="05" Value="05" />
                                            <asp:ListItem Text="06" Value="06" />
                                            <asp:ListItem Text="07" Value="07" />
                                            <asp:ListItem Text="08" Value="08" />
                                            <asp:ListItem Text="09" Value="09" />
                                            <asp:ListItem Text="10" Value="10" />
                                            <asp:ListItem Text="11" Value="11" />
                                            <asp:ListItem Text="12" Value="12" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="expMonthRFV" ControlToValidate="expMonthList" runat="server" ErrorMessage="You must select an Expiration Month" CssClass="errorText" Display="Dynamic" SetFocusOnError="true" Text=" *" />
                                        <asp:DropDownList ID="expYearList" runat="server" CssClass="tripRegistrationItem" />
                                        <asp:RequiredFieldValidator ID="expYearRFV" ControlToValidate="expYearList" runat="server" ErrorMessage="You must select an Expiration Year" CssClass="errorText" Display="Dynamic" SetFocusOnError="true" Text=" *" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="achPanel" runat="server">
                            <table cellpadding="0" cellspacing="5" border="0" class="paymentInfoTable">
                                <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Bank Name</td>
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                        <asp:TextBox ID="bankName" runat="server" Width="200" MaxLength="30" CssClass="tripRegistrationItem" />
                                        <asp:RequiredFieldValidator ID="bankNameRFV" ControlToValidate="bankName" runat="server" ErrorMessage="Bank Name is required" CssClass="errorText" Display="Dynamic" SetFocusOnError="true" Text=" *" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Account Type</td>
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="left">
                                        <asp:RadioButtonList ID="accountType" runat="server" RepeatDirection="Horizontal" CssClass="tripRegistrationLabel" RepeatLayout="Flow">
                                            <asp:ListItem Value="Checking" Text="Checking" />
                                            <asp:ListItem Value="Savings" Text="Savings" />
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Routing Number</td>
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                        <asp:TextBox ID="routingNumber" runat="server" Width="200" MaxLength="9" CssClass="tripRegistrationItem" />
                                        <asp:RequiredFieldValidator ID="routingNumberRFV" ControlToValidate="routingNumber" runat="server" ErrorMessage="Routing Number is required" CssClass="errorText" Display="Dynamic" SetFocusOnError="true" Text=" *" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tripRegistrationLabel" style="white-space:nowrap" valign="middle">Account Number</td>
                                    <td class="tripRegistrationItem" style="white-space:nowrap" valign="middle">
                                        <asp:TextBox ID="accountNumber" runat="server" Width="200" MaxLength="17" CssClass="tripRegistrationItem" />
                                        <asp:RequiredFieldValidator ID="accountNumberRFV" ControlToValidate="accountNumber" runat="server" ErrorMessage="Account Number is required" CssClass="errorText" Display="Dynamic" SetFocusOnError="true" Text=" *" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="tripRegistrationImage"><asp:Image ID="imgCheck" runat="server" /></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel id="preAuthErrorMsgPanel" runat="server" CssClass="tripRegistrationError" Visible="false" />
                    </asp:WizardStep>
                    <asp:WizardStep ID="verificationStep" runat="server" Title="" StepType="Finish">
                        <asp:Panel ID="verificationPanel" runat="server">
                            <h3>Payment Verification</h3>
                            <asp:PlaceHolder ID="verificationPlaceholder" runat="server" />
                        </asp:Panel>
                        <asp:Panel id="authErrorMsgPanel" runat="server" CssClass="tripRegistrationError" Visible="false" />
                    </asp:WizardStep>
                    <asp:WizardStep ID="confirmationStep" runat="server" Title="" StepType="Complete">
                        <asp:Panel ID="confirmationPanel" runat="server">
                            <h3>Confirmation</h3>
                        </asp:Panel>
                        <asp:Panel ID="confirmationNumberPanel" runat="server" CssClass="tripRegistrationConfirmationNumber" />
                    </asp:WizardStep>
                </WizardSteps>
                <StartNavigationTemplate>
                    <asp:Button ID="StepCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="cancelButton" />
                    <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Next" CssClass="nextButton" />
                </StartNavigationTemplate>
                <StepNavigationTemplate>
                    <asp:Button ID="StepCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="cancelButton" />
                    <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Previous" CssClass="previousButton" />
                    <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="Next" CssClass="nextButton" />
                </StepNavigationTemplate>
                <FinishNavigationTemplate>
                    <asp:Button ID="FinishCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="cancelButton" />
                    <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Previous" CssClass="previousButton" />
                    <asp:Button ID="FinishCompleteButton" runat="server" CommandName="MoveComplete" Text="Finish" CssClass="finishButton" OnClientClick="return showProcessing(this)" />
                    <span id="FinishLabel" style="display:none" class="alert">Processing your transaction. Please wait...</span>
                </FinishNavigationTemplate>
            </asp:Wizard>
            <asp:Panel ID="messagePanel" runat="server" Visible="false">
                <asp:Label ID="messageLabel" runat="server" CssClass="tripRegistrationLabel" />
            </asp:Panel>
            <input type="hidden" id="hiddenPersonID" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>