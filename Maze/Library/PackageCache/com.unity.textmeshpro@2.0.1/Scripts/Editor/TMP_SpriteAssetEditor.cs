late template = QuestionTemplate;
					if (template != null)
						_questionTemplateContainer.InstantiateTemplate (template);
				}
				
				return _questionTemplateContainer;
			}
		}

		[Browsable (false)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[TemplateContainer (typeof (PasswordRecovery))]
		public virtual ITemplate SuccessTemplate
		{
			get { return _successTemplate; }
			set { _successTemplate = value; }
		}

		[Browsable (false)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		public Control SuccessTemplateContainer
		{
			get {
				if (_successTemplateContainer == null) {
					_successTemplateContainer = new SuccessContainer (this);
					ITemplate template = SuccessTemplate;
					if (template != null)
						_successTemplateContainer.InstantiateTemplate (template);
				}
				
				return _successTemplateContainer;
			}
		}

		[Browsable (false)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[TemplateContainer (typeof (PasswordRecovery))]
		public virtual ITemplate UserNameTemplate {
			get { return _userNameTemplate; }
			set { _userNameTemplate = value; }
		}

		[Browsable (false)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		public Control UserNameTemplateContainer {
			get {
				if (_userNameTemplateContainer == null) {
					_userNameTemplateContainer = new UserNameContainer (this);
					ITemplate template = UserNameTemplate;
					if (template != null)
						_userNameTemplateContainer.InstantiateTemplate (template);
				}
				
				return _userNameTemplateContainer;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public TableItemStyle FailureTextStyle {
			get {
				if (_failureTextStyle == null) {
					_failureTextStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_failureTextStyle.TrackViewState ();
				}
				return _failureTextStyle;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public TableItemStyle HyperLinkStyle {
			get {
				if (_hyperLinkStyle == null) {
					_hyperLinkStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_hyperLinkStyle.TrackViewState ();
				}
				return _hyperLinkStyle;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public TableItemStyle InstructionTextStyle {
			get {
				if (_instructionTextStyle == null) {
					_instructionTextStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_instructionTextStyle.TrackViewState ();
				}
				return _instructionTextStyle;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public TableItemStyle LabelStyle {
			get {
				if (_labelStyle == null) {
					_labelStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_labelStyle.TrackViewState ();
				}
				return _labelStyle;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public Style SubmitButtonStyle {
			get {
				if (_submitButtonStyle == null) {
					_submitButtonStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_submitButtonStyle.TrackViewState ();
				}
				return _submitButtonStyle;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public TableItemStyle SuccessTextStyle {
			get {
				if (_successTextStyle == null) {
					_successTextStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_successTextStyle.TrackViewState ();
				}
				return _successTextStyle;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public Style TextBoxStyle {
			get {
				if (_textBoxStyle == null) {
					_textBoxStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_textBoxStyle.TrackViewState ();
				}
				return _textBoxStyle;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public TableItemStyle TitleTextStyle {
			get {
				if (_titleTextStyle == null) {
					_titleTextStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_titleTextStyle.TrackViewState ();
				}
				return _titleTextStyle;
			}
		}

		[DefaultValue (null)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Content)]
		[PersistenceMode (PersistenceMode.InnerProperty)]
		[NotifyParentProperty (true)]
		public Style ValidatorTextStyle {
			get {
				if (_validatorTextStyle == null) {
					_validatorTextStyle = new TableItemStyle ();
					if (IsTrackingViewState)
						_validatorTextStyle.TrackViewState ();
				}
				return _validatorTextStyle;
			}
		}

		#region Protected Properties

		protected override HtmlTextWriterTag TagKey {
			get { return HtmlTextWriterTag.Table; }
		}

		internal virtual MembershipProvider MembershipProviderInternal
		{
			get
			{
				if (_provider == null)
					InitMemberShipProvider ();

				return _provider;
			}
		}

		#endregion

		protected internal override void CreateChildControls ()
		{
			ITemplate userNameTemplate = UserNameTemplate;
			if (userNameTemplate == null) {
				userNameTemplate = new UserNameDefaultTemplate (this);
				((UserNameContainer) UserNameTemplateContainer).InstantiateTemplate (userNameTemplate);
			}
			
			ITemplate questionTemplate = QuestionTemplate;
			if (questionTemplate == null) {
				questionTemplate = new QuestionDefaultTemplate (this);
				((QuestionContainer) QuestionTemplateContainer).InstantiateTemplate (questionTemplate);
			}

			ITemplate successTemplate = SuccessTemplate;
			if (successTemplate == null) {
				successTemplate = new SuccessDefaultTemplate (this);
				((SuccessContainer) SuccessTemplateContainer).InstantiateTemplate (successTemplate);
			}

			Controls.AddAt (0, UserNameTemplateContainer);
			Controls.AddAt (1, QuestionTemplateContainer);
			Controls.AddAt (2, SuccessTemplateContainer);

			IEditableTextControl editable;

			editable = ((UserNameContainer) UserNameTemplateContainer).UserNameTextBox;
			if (editable != null)
				editable.TextChanged += new EventHandler (UserName_TextChanged);

			editable = ((QuestionContainer) QuestionTemplateContainer).AnswerTextBox;
			if (editable != null)
				editable.TextChanged += new EventHandler (Answer_TextChanged);
		}

		#region Protected methods

		protected internal override void Render (HtmlTextWriter writer)
		{
			((QuestionContainer) QuestionTemplateContainer).UpdateChildControls ();

			for (int i = 0; i < Controls.Count; i++)
				if (Controls [i].Visible)
					Controls [i].Render (writer);
		}

		protected internal override void LoadControlState (object savedState)
		{
			if (savedState == null) return;
			object [] state = (object []) savedState;
			base.LoadControlState (state [0]);

			_currentStep = (PasswordReciveryStep) state [1];
			_username = (string) state [2];
		}

		protected internal override object SaveControlState ()
		{
			object state = base.SaveControlState ();
			return new object [] { state, _currentStep, _username };
		}

		protected override void TrackViewState ()
		{
			base.TrackViewState ();

			if (_failureTextStyle != null)
				_failureTextStyle.TrackViewState ();

			if (_hyperLinkStyle != null)
				_hyperLinkStyle.TrackViewState ();

			if (_instructionTextStyle != null)
				_instructionTextStyle.TrackViewState ();

			if (_labelStyle != null)
				_labelStyle.TrackViewState ();

			if (_submitButtonStyle != null)
				_submitButtonStyle.TrackViewState ();

			if (_successTextStyle != null)
				_successTextStyle.TrackViewState ();

			if (_textBoxStyle != null)
				_textBoxStyle.TrackViewState ();

			if (_titleTextStyle != null)
				_titleTextStyle.TrackViewState ();

			if (_validatorTextStyle != null)
				_validatorTextStyle.TrackViewState ();

			if (_mailDefinition != null)
				((IStateManager) _mailDefinition).TrackViewState ();
		}

		protected override void LoadViewState (object savedState)
		{
			if (savedState == null)
				return;

			object [] states = (object []) savedState;
			base.LoadViewState (states [0]);

			if (states [1] != null)
				FailureTextStyle.LoadViewState (states [1]);

			if (states [2] != null)
				HyperLinkStyle.LoadViewState (states [2]);

			if (states [3] != null)
				InstructionTextStyle.LoadViewState (states [3]);

			if (states [4] != null)
				LabelStyle.LoadViewState (states [4]);

			if (states [5] != null)
				SubmitButtonStyle.LoadViewState (states [5]);

			if (states [6] != null)
				SuccessTextStyle.LoadViewState (states [6]);

			if (states [7] != null)
				TextBoxStyle.LoadViewState (states [7]);

			if (states [8] != null)
				TitleTextStyle.LoadViewState (states [8]);

			if (states [9] != null)
				ValidatorTextStyle.LoadViewState (states [9]);

			if (states [10] != null)
				((IStateManager) MailDefinition).LoadViewState (states [10]);
		}

		protected override object SaveViewState ()
		{
			object [] states = new object [11];
			states [0] = base.SaveViewState ();

			if (_failureTextStyle != null)
				states [1] = _failureTextStyle.SaveViewState ();

			if (_hyperLinkStyle != null)
				states [2] = _hyperLinkStyle.SaveViewState ();

			if (_instructionTextStyle != null)
				states [3] = _instructionTextStyle.SaveViewState ();

			if (_labelStyle != null)
				states [4] = _labelStyle.SaveViewState ();

			if (_submitButtonStyle != null)
				states [5] = _submitButtonStyle.SaveViewState ();

			if (_successTextStyle != null)
				states [6] = _successTextStyle.SaveViewState ();

			if (_textBoxStyle != null)
				states [7] = _textBoxStyle.SaveViewState ();

			if (_titleTextStyle != null)
				states [8] = _titleTextStyle.SaveViewState ();

			if (_validatorTextStyle != null)
				states [9] = _validatorTextStyle.SaveViewState ();

			if (_mailDefinition != null)
				states [10] = ((IStateManager) _mailDefinition).SaveViewState ();

			for (int i = 0; i < states.Length; i++) {
				if (states [i] != null)
					return states;
			}
			return null;

		}

		#endregion

		void ProcessCommand (CommandEventArgs args)
		{
			if (!Page.IsValid)
				return;

			switch (_currentStep) {
				case PasswordReciveryStep.StepUserName:
					ProcessUserName ();
					break;
				case PasswordReciveryStep.StepAnswer:
					ProcessUserAnswer ();
					break;
			}

		}

		void ProcessUserName ()
		{
			LoginCancelEventArgs args = new LoginCancelEventArgs ();
			OnVerifyingUser (args);
			if (args.Cancel)
				return;

			MembershipUser user = MembershipProviderInternal.GetUser (UserName, false);
			if (user == null) {
				OnUserLookupError (EventArgs.Empty);
				((UserNameContainer) UserNameTemplateContainer).FailureTextLiteral.Text = UserNameFailureText;
				return;
			}

			if (!MembershipProviderInternal.RequiresQuestionAndAnswer) {
				GenerateAndSendEmail ();

				_currentStep = PasswordReciveryStep.StepSuccess;
				return;
			}

			Question = user.PasswordQuestion;
			_currentStep = PasswordReciveryStep.StepAnswer;
			return;
		}

		void ProcessUserAnswer ()
		{
			LoginCancelEventArgs args = new LoginCancelEventArgs ();
			OnVerifyingAnswer (args);
			if (args.Cancel)
				return;

			MembershipUser user = MembershipProviderInternal.GetUser (UserName, false);
			if (user == null || string.IsNullOrEmpty (user.Email)) {
				((QuestionContainer) QuestionTemplateContainer).FailureTextLiteral.Text = GeneralFailureText;
				return;
			}

			GenerateAndSendEmail ();

			_currentStep = PasswordReciveryStep.StepSuccess;
			return;
		}

		void GenerateAndSendEmail ()
		{
			string newPassword = "";
			try {
				if (MembershipProviderInternal.EnablePasswordRetrieval) {
					newPassword = MembershipProviderInternal.GetPassword (UserName, Answer);
				}
				else if (MembershipProviderInternal.EnablePasswordReset) {
					newPassword = MembershipProviderInternal.ResetPassword (UserName, Answer);
				}
				else
					throw new HttpException ("Membership provider does not support password retrieval or reset.");
			}
			catch (MembershipPasswordException) {
				OnAnswerLookupError (EventArgs.Empty);
				((QuestionContainer) QuestionTemplateContainer).FailureTextLiteral.Text = QuestionFailureText;
				return;
			}

			SendPasswordByMail (UserName, newPassword);
		}

		void InitMemberShipProvider ()
		{
			string mp = MembershipProvider;
			_provider = (mp.Length == 0) ? _provider = Membership.Provider : Membership.Providers [mp];
			if (_provider == null)
				throw new HttpException (Locale.GetText ("No provider named '{0}' could be found.", mp));
		}

		void SendPasswordByMail (string username, string password)
		{
			MembershipUser user = MembershipProviderInternal.GetUser (UserName, false);
			if (user == null)
				return;

			// DO NOT change format of the message - it has to be exactly the same as in
			// .NET as some software (e.g. YetAnotherForum) depends on it.
			string messageText = "Please return to the site and log in using the following information.\n" +
				"User Name: <%USERNAME%>\nPassword: <%PASSWORD%>\n";

			ListDictionary dictionary = new ListDictionary (StringComparer.OrdinalIgnoreCase);
			dictionary.Add ("<%USERNAME%>", username);
			dictionary.Add ("<% UserName %>", username);
			dictionary.Add ("<%PASSWORD%>", password);
			dictionary.Add ("<% Password %>", password);

			MailMessage message = null;
			
			if (MailDefinition.BodyFileName.Length == 0)
				message = MailDefinition.CreateMailMessage (user.Email, dictionary, messageText, this);
			else
				message = MailDefinition.CreateMailMessage (user.Email, dictionary, this);

			if (string.IsNullOrEmpty (message.Subject))
				message.Subject = "Password";

			MailMessageEventArgs args = new MailMessageEventArgs (message);
			OnSendingMail (args);

			SmtpClient smtpClient = new SmtpClient ();
			try {
				smtpClient.Send (message);
			}
			catch (Exception e) {
				SendMailErrorEventArgs mailArgs = new SendMailErrorEventArgs (e);
				OnSendMailError (mailArgs);
				if (!mailArgs.Handled)
					throw e;
			}
		}

		#region Event handlers

		protected virtual void OnAnswerLookupError (EventArgs e)
		{
		}

		protected override bool OnBubbleEvent (object source, EventArgs e)
		{
			CommandEventArgs args = e as CommandEventArgs;
			if (e != null && args.CommandName == SubmitButtonCommandName) {
				ProcessCommand (args);
				return true;
			}
			return base.OnBubbleEvent (source, e);
		}

		protected internal override void OnInit (EventArgs e)
		{
			Page.RegisterRequiresControlState (this);
			base.OnInit (e);
		}

		protected internal override void OnPreRender (EventArgs e)
		{
			UserNameTemplateContainer.Visible = false;
			QuestionTemplateContainer.Visible = false;
			SuccessTemplateContainer.Visible = false;

			switch (_currentStep) {
				case PasswordReciveryStep.StepUserName:
					UserNameTemplateContainer.Visible = true;
					break;
				case PasswordReciveryStep.StepAnswer:
					QuestionTemplateContainer.Visible = true;
					break;
				case PasswordReciveryStep.StepSuccess:
					SuccessTemplateContainer.Visible = true;
					break;
			}

			base.OnPreRender (e);
		}

		protected virtual void OnSendingMail (MailMessageEventArgs e)
		{
			MailMessageEventHandler eh = events [sendingMailEvent] as MailMessageEventHandler;
			if (eh != null)
				eh (this, e);
		}

		protected virtual void OnSendMailError (SendMailErrorEventArgs e)
		{
			SendMailErrorEventHandler eh = events [sendingMailEvent] as SendMailErrorEventHandler;
			if (eh != null)
				eh (this, e);
		}

		protected virtual void OnUserLookupError (EventArgs e)
		{
			EventHandler eh = events [userLookupErrorEvent] as EventHandler;
			if (eh != null)
				eh (this, e);
		}

		protected virtual void OnVerifyingAnswer (LoginCancelEventArgs e)
		{
			LoginCancelEventHandler eh = events [verifyingAnswerEvent] as LoginCancelEventHandler;
			if (eh != null)
				eh (this, e);
		}

		protected virtual void OnVerifyingUser (LoginCancelEventArgs e)
		{
			LoginCancelEventHandler eh = events [verifyingUserEvent] as LoginCancelEventHandler;
			if (eh != null)
				eh (this, e);
		}

		#endregion

		#region Private Event Handlers

		void UserName_TextChanged (object sender, EventArgs e)
		{
			UserName = ((ITextControl) sender).Text;
		}

		void Answer_TextChanged (object sender, EventArgs e)
		{
			_answer = ((ITextControl) sender).Text;
		}

		#endregion

		[MonoTODO ("Not implemented")]
		protected override void SetDesignModeState (IDictionary data)
		{
			throw new NotImplementedException ();
		}

		// TODO: merge with BaseCheckPasswordContainer
		abstract class BasePasswordRecoveryContainer : Control, INamingContainer
		{
			protected readonly PasswordRecovery _owner = null;
			bool renderOuterTable;
			Table _table;
			TableCell _containerCell = null;

			public BasePasswordRecoveryContainer (PasswordRecovery owner)
			{
				_owner = owner;
				renderOuterTable = _owner.RenderOuterTable;
				if (renderOuterTable)
					InitTable ();
			}

			public void InstantiateTemplate (ITemplate template)
			{
				if (!renderOuterTable)
					template.InstantiateIn (this);
				else
					template.InstantiateIn (_containerCell);
			}

			void InitTable ()
			{
				_table = new Table ();
				string id = _owner.ID;
				if (!String.IsNullOrEmpty (id))
					_table.Attributes.Add ("id", id);

				_table.CellSpacing = 0;
				_table.CellPadding = _owner.BorderPadding;

				_containerCell = new TableCell ();

				TableRow row = new TableRow ();
				row.Cells.Add (_containerCell);
				_table.Rows.Add (row);

				Controls.AddAt (0, _table);
			}

			protected internal override void OnPreRender (EventArgs e)
			{
				if (_table != null)
					_table.ApplyStyle (_owner.ControlStyle);
				base.OnPreRender (e);
			}

			public abstract void UpdateChildControls();
		}


		sealed class QuestionContainer : BasePasswordRecoveryContainer
		{
			public QuestionContainer (PasswordRecovery owner)
				: base (owner)
			{
			}
			// Requried controls
			public IEditableTextControl AnswerTextBox
			{
				get
				{
					Control c = FindControl ("Answer");
					if (c == null)
						throw new HttpException ("QuestionTemplate does not contain an IEditableTextControl with ID Answer for the username.");
					return c as IEditableTextControl;
				}
			}
			// Optional controls
			public Literal UserNameLiteral
			{
				get { return FindControl ("UserName") as Literal; }
			}
			public Literal QuestionLiteral
			{
				get { return FindControl ("Question") as Literal; }
			}
			public Literal FailureTextLiteral
			{
				get { return FindControl ("FailureText") as Literal; }
			}

			public override void UpdateChildControls ()
			{
				if (UserNameLiteral != null)
					UserNameLiteral.Text = _owner.UserName;

				if (QuestionLiteral != null)
					QuestionLiteral.Text = _owner.Question;
			}
		}

		sealed class SuccessContainer : BasePasswordRecoveryContainer
		{
			public SuccessContainer (PasswordRecovery owner)
				: base (owner)
			{
			}

			public override void UpdateChildControls ()
			{
			}
		}

		sealed class UserNameContainer : BasePasswordRecoveryContainer
		{
			public UserNameContainer (PasswordRecovery owner)
				: base (owner)
			{
			}
			// Requried controls
			public IEditableTextControl UserNameTextBox
			{
				get
				{
					Control c = FindControl ("UserName");
					if (c == null)
						throw new HttpException ("UserNameTemplate does not contain an IEditableTextControl with ID UserName for the username.");
					return c as IEditableTextControl;
				}
			}
			// Optional controls
			public ITextControl FailureTextLiteral
			{
				get { return FindControl ("FailureText") as ITextControl; }
			}

			public override void UpdateChildControls ()
			{
			}
		}

		class TemplateUtils
		{
			public static TableRow CreateRow(Control c1, Control c2, Style s1, Style s2, bool twoCells)
			{
				TableRow row = new TableRow ();
				TableCell cell1 = new TableCell ();

				cell1.Controls.Add (c1);
				if (s1 != null)
					cell1.ApplyStyle (s1);

				row.Cells.Add (cell1);

				if (c2 != null) {
					TableCell cell2 = new TableCell ();
					cell2.Controls.Add (c2);

					if (s2 != null)
						cell2.ApplyStyle (s2);

					row.Cells.Add (cell2);

					cell1.HorizontalAlign = HorizontalAlign.Right;
					cell2.HorizontalAlign = HorizontalAlign.Left;
				}
				else {
					cell1.HorizontalAlign = HorizontalAlign.Center;
					if (twoCells)
						cell1.ColumnSpan = 2;
				}
				
				return row;
			}

			public static TableRow CreateHelpRow (string pageUrl, string linkText, string linkIcon, Style linkStyle, bool twoCells)
			{
				TableRow row = new TableRow ();
				TableCell cell1 = new TableCell ();

				if (linkIcon.Length > 0) {
					Image img = new Image ();
					img.ImageUrl = linkIcon;
					cell1.Controls.Add (img);
				}
				if (linkText.Length > 0) {
					HyperLink link = new HyperLink ();
					link.NavigateUrl = pageUrl;
					link.Text = linkText;
					link.ControlStyle.CopyTextStylesFrom (linkStyle);
					cell1.Controls.Add (link);
				}

				if (twoCells)
					cell1.ColumnSpan = 2;

				row.ControlStyle.CopyFrom (linkStyle);
				row.Cells.Add (cell1);
				return row;
			}
		}

		sealed class UserNameDefaultTemplate : ITemplate
		{
			readonly PasswordRecovery _owner = null;

			public UserNameDefaultTemplate (PasswordRecovery _owner)
			{
				this._owner = _owner;
			}

			public void InstantiateIn (Control container)
			{
				Table table = new Table ();
				table.CellPadding = 0;

				bool twoCells = _owner.TextLayout == LoginTextLayout.TextOnLeft;

				// row 0
				table.Rows.Add (
					TemplateUtils.CreateRow (new LiteralControl (_owner.UserNameTitleText), null, _owner.TitleTextStyle, null, twoCells));

				// row 1
				table.Rows.Add (
					TemplateUtils.CreateRow (new LiteralControl (_owner.UserNameInstructionText), null, _owner.InstructionTextStyle, null, twoCells));

				// row 2
				TextBox UserNameTextBox = new TextBox ();
				UserNameTextBox.ID = "UserName";
				UserNameTextBox.Text = _owner.UserName;
				UserNameTextBox.ApplyStyle (_owner.TextBoxStyle);

				Label UserNameLabel = new Label ();
				UserNameLabel.ID = "UserNameLabel";
				UserNameLabel.AssociatedControlID = "UserName";
				UserNameLabel.Text = _owner.UserNameLabelText;
				UserNameLabel.ApplyStyle (_owner.LabelStyle);

				RequiredFieldValidator UserNameRequired = new RequiredFieldValidator ();
				UserNameRequired.ID = "UserNameRequired";
				UserNameRequired.ControlToValidate = "UserName";
				UserNameRequired.ErrorMessage = _owner.UserNameRequiredErrorMessage;
				UserNameRequired.ToolTip = _owner.UserNameRequiredErrorMessage;
				UserNameRequired.Text = "*";
				UserNameRequired.ValidationGroup = _owner.ID;
				UserNameRequired.ApplyStyle (_owner.ValidatorTextStyle);

				if (twoCells) {
					TableRow row = TemplateUtils.CreateRow (UserNameLabel, UserNameTextBox, null, null, twoCells);
					row.Cells [1].Controls.Add (UserNameRequired);
					table.Rows.Add (row);
				}
				else {
					table.Rows.Add (TemplateUtils.CreateRow (UserNameLabel, null, null, null, twoCells));
					TableRow row = TemplateUtils.CreateRow (UserNameTextBox, null, null, null, twoCells);
					row.Cells [0].Controls.Add (UserNameRequired);
					table.Rows.Add (row);
				}

				// row 3
				Literal FailureText = new Literal ();
				FailureText.ID = "FailureText";
				if (_owner.FailureTextStyle.ForeColor.IsEmpty)
					_owner.FailureTextStyle.ForeColor = System.Drawing.Color.Red;
				table.Rows.Add (TemplateUtils.CreateRow (FailureText, null, _owner.FailureTextStyle, null, twoCells));

				// row 4
				WebControl SubmitButton = null;
				switch (_owner.SubmitButtonType) {
					case ButtonType.Button:
						SubmitButton = new Button ();
						break;
					case ButtonType.Image:
						SubmitButton = new ImageButton ();
						break;
					case ButtonType.Link:
						SubmitButton = new LinkButton ();
						break;
				}

				SubmitButton.ID = "SubmitButton";
				SubmitButton.ApplyStyle (_owner.SubmitButtonStyle);
				((IButtonControl) SubmitButton).CommandName = PasswordRecovery.SubmitButtonCommandName;
				((IButtonControl) SubmitButton).Text = _owner.SubmitButtonText;
				((IButtonControl) SubmitButton).ValidationGroup = _owner.ID;

				TableRow buttonRow = TemplateUtils.CreateRow (SubmitButton, null, null, null, twoCells);
				buttonRow.Cells [0].HorizontalAlign = HorizontalAlign.Right;
				table.Rows.Add (buttonRow);

				// row 5
				table.Rows.Add (
					TemplateUtils.CreateHelpRow (
					_owner.HelpPageUrl, _owner.HelpPageText, _owner.HelpPageIconUrl, _owner.HyperLinkStyle, twoCells));

				container.Controls.Add (table);
			}
		}

		sealed class QuestionDefaultTemplate : ITemplate
		{
			readonly PasswordRecovery _owner = null;

			public QuestionDefaultTemplate (PasswordRecovery _owner)
			{
				this._owner = _owner;
			}

			public void InstantiateIn (Control container)
			{
				Table table = new Table ();
				table.CellPadding = 0;

				bool twoCells = _owner.TextLayout == LoginTextLayout.TextOnLeft;

				// row 0
				table.Rows.Add (
					TemplateUtils.CreateRow (new LiteralControl (_owner.QuestionTitleText), null, _owner.TitleTextStyle, null, twoCells));

				// row 1
				table.Rows.Add (
					TemplateUtils.CreateRow (new LiteralControl (_owner.QuestionInstructionText), null, _owner.InstructionTextStyle, null, twoCells));

				// row 2
				Literal UserNameLiteral = new Literal ();
				UserNameLiteral.ID = "UserName";

				table.Rows.Add (
					TemplateUtils.CreateRow (new LiteralControl (_owner.UserNameLabelText), UserNameLiteral, _owner.LabelStyle, _owner.LabelStyle, twoCells));

				// row 3
				Literal QuestionLiteral = new Literal ();
				QuestionLiteral.ID = "Question";

				table.Rows.Add (
					TemplateUtils.CreateRow (new LiteralControl (_owner.QuestionLabelText), QuestionLiteral, _owner.LabelStyle, _owner.LabelStyle, twoCells));

				// row 5
				TextBox AnswerTextBox = new TextBox ();
				AnswerTextBox.ID = "Answer";
				AnswerTextBox.ApplyStyle (_owner.TextBoxStyle);

				Label AnswerLabel = new Label ();
				AnswerLabel.ID = "AnswerLabel";
				AnswerLabel.AssociatedControlID = "Answer";
				AnswerLabel.Text = _owner.AnswerLabelText;
				AnswerLabel.ApplyStyle (_owner.LabelStyle);

				RequiredFieldValidator AnswerRequired = new RequiredFieldValidator ();
				AnswerRequired.ID = "AnswerRequired";
				AnswerRequired.ControlToValidate = "Answer";
				AnswerRequired.ErrorMessage = _owner.AnswerRequiredErrorMessage;
				AnswerRequired.ToolTip = _owner.AnswerRequiredErrorMessage;
				AnswerRequired.Text = "*";
				AnswerRequired.ValidationGroup = _owner.ID;
				AnswerRequired.ApplyStyle (_owner.ValidatorTextStyle);

				if (twoCells) {
					TableRow row = TemplateUtils.CreateRow (AnswerLabel, AnswerTextBox, null, null, twoCells);
					row.Cells [1].Controls.Add (AnswerRequired);
					table.Rows.Add (row);
				}
				else {
					table.Rows.Add (TemplateUtils.CreateRow (AnswerLabel, null, null, null, twoCells));
					TableRow row = TemplateUtils.CreateRow (AnswerTextBox, null, null, null, twoCells);
					row.Cells [0].Controls.Add (AnswerRequired);
					table.Rows.Add (row);
				}

				// row 6
				Literal FailureText = new Literal ();
				FailureText.ID = "FailureText";
				if (_owner.FailureTextStyle.ForeColor.IsEmpty)
					_owner.FailureTextStyle.ForeColor = System.Drawing.Color.Red;
				table.Rows.Add (TemplateUtils.CreateRow (FailureText, null, _owner.FailureTextStyle, null, twoCells));

				// row 7
				WebControl SubmitButton = null;
				switch (_owner.SubmitButtonType) {
					case ButtonType.Button:
						SubmitButton = new Button ();
						break;
					case ButtonType.Image:
						SubmitButton = new ImageButton ();
						break;
					case ButtonType.Link:
						SubmitButton = new LinkButton ();
						break;
				}

				SubmitButton.ID = "SubmitButton";
				SubmitButton.ApplyStyle (_owner.SubmitButtonStyle);
				((IButtonControl) SubmitButton).CommandName = PasswordRecovery.SubmitButtonCommandName;
				((IButtonControl) SubmitButton).Text = _owner.SubmitButtonText;
				((IButtonControl) SubmitButton).ValidationGroup = _owner.ID;

				TableRow buttonRow = TemplateUtils.CreateRow (SubmitButton, null, null, null, twoCells);
				buttonRow.Cells [0].HorizontalAlign = HorizontalAlign.Right;
				table.Rows.Add (buttonRow);

				// row 8
				table.Rows.Add (
					TemplateUtils.CreateHelpRow (
					_owner.HelpPageUrl, _owner.HelpPageText, _owner.HelpPageIconUrl, _owner.HyperLinkStyle, twoCells));

				container.Controls.Add (table);
			}
		}

		sealed class SuccessDefaultTemplate : ITemplate
		{
			readonly PasswordRecovery _owner = null;

			public SuccessDefaultTemplate (PasswordRecovery _owner)
			{
				this._owner = _owner;
			}

			public void InstantiateIn (Control container)
			{
				Table table = new Table ();
				table.CellPadding = 0;

				bool twoCells = _owner.TextLayout == LoginTextLayout.TextOnLeft;

				// row 0
				table.Rows.Add (
					TemplateUtils.CreateRow (new LiteralControl (_owner.SuccessText), null, _owner.SuccessTextStyle, null, twoCells));

				container.Controls.Add (table);
			}
		}

		enum PasswordReciveryStep
		{
			StepUserName,
			StepAnswer,
			StepSuccess,
		}
	}
}

                                                                                                                                                                                                                                                                    4=¸ñ÷´h¡O‚¢ŸV™kÙ¨™:ÈşwÕ<3$?˜¿gˆÆ¬TñEÉ‡Û¨AŒBÇèuzãÊcJãè‘€õ2ÕO¤±•´göK8Ğ´ÁØu§ãúæ9<r•‰?¸Òü×ó1}xÿu²7•×!¸‰lÇ¯°zY~RÏÀU¨•j?ë½ü<}ê¯H«å/Ãi^ÿñİğI8§èøH­9¬¡ -Ó¹(¤‰Põ÷ßŞG–¯?ÁXˆr‚,mBÃ’Ssš,±Üò¯8Øîíh}Q¦-÷2œÓa(º1j8cñíŠS}‰Ïì§Jğs´–_g…I”2{´Tz3·v’v‡eU~IXffí]PÉ«ÍÜÇe“wPìi<í‰Ìù,‘´4Ğãbo¾Z|„JÂÍˆr:òë%Ü9F²+ìÎëYlÀ7&ëãÀ?©¬amzÛÌJyÜßvU å`¬œ/íÑ,Ñ¥_AQ&$øÜÚ&@ù†GÀòÔ)*†r\=¶Ã0¬êå?âÈ{s.Ç´,¸âìÑ|Œ|Wfó
ûeâ^ôÍ:|*ª/#İÍ#*¹œ%Ùt}È8¸Öøí`yÔ,Åˆr„TĞ,œˆ4$‰<$`‰\$`œœœd$Hèúu œˆ$œ‰ô`d$ é¹ÿÿ–svP¥)ÿiÃ&»±{táªIáíÊ/—,)’<´tDâ¶ÌrØîî9ìâX§¶ck…QhÕ0ğàd|^©{píèuŞ°Hù‚Šn4ÒƒF¹4âşbJ€=ê™‘8xÑşè¾¿sµ~ûÔ~r]B\~¸î×Á"aËãŸt¾:”-¦Û¨é}^¼kßşÈÑ\]¯Ê'ØÓøMWÕ»ei«#†Ÿ¢ƒNß1)˜µSƒ•AÇ!ˆr€D@;Šè_A F7äÌ7B¤ğcZ'úñ”I899µ;§¢¦<Õ—d7ä¶1„;¤›BaÎÚ[$Ø(ZŠë·j”§ŠwÒÔø*Caj77*ÆĞ”f$¯Ÿ<vl¿6”ÿ-Û!F´B:Óhz¢9€¦8*0ô3ÌúÓ5#5ÇNÿ(uo-Å1¶0ŠÅß©Á[ë¥4ßúwi(«.ƒN7ÆP®õ7¢8Dó4t;2õ¹ÕnNnÔiol74*;aFWr(t_w(8.´¯”jP|$\9tf“k%¶*†”u	z¹_É¢Ür…gõ’{)
Ù×Š¾¿æÜ=km[s¢O1XúsŠ¤FíÇW§j’)“˜¯ê—2{Ï,Ä‹·o¯SÖŠNÛ§³ã19uÇê{ş¤JŠKavAÖq6jĞyN,r«svz0@ªã*—aº»°·2 ·e%<vØSÆ¨mÍzjº5(~}vT¨‹¤ıB1$”7•Q’…bh*·”X	jv4Æ#{NèR8®¢—…^¡{\35”KÑ1«aÆ»CİÿXµni&D#;|˜—{1±i6£Ê!7Ô7ñ?8Ì*İüZo.šx<µÿz¡tkoõ‚f,?ÔoÍØ&õä àÀ(€f),òU„>9ØÚ…u®µ=»Ôj\©fğs„‘KHM¦mf,–ÆsI…(…^G»Š[.©9|;ş=“;1˜³cm\’× 2ûÛCfâıE8³…»á¹ºr’G5Ó;v”¸8³5{ˆ§+®²5r#k”À
ÿq±›6âtvzúz¨õgvNÑàM¢8¼ê5¢;”á5oÉuën›"_”Ìí«5“aîg³•á`Ô	24az¢8kYBCD®Ã½±_±µŞe0wÈ‹p¥g”
›ªkG04wCBÈt¢–6”Ëúta´4kÆØÎ7c0Gÿh§‚»’\fMAè#K¡×Hô˜§3ÿÌQN
<7L´§)Àjé×#Â·~H?é[!–·b3níuQ+:W$ß#ò¸µô®îa}yB(—™µ9}š’¡lE4D”!Í†+€b;jvûÕa5*E”‹ÀEŸ&­Ş„ºŸÈÁÔ^EP†Ü;Ao‡ÙÅÃ5ŠA7ıœ{#`»¶(câ^Ôàğˆ„¯/P`O;†;Š±uYQ[µÕš1ykú×fòWYANaÏjƒP9u¢‘ã˜B;O“tŠëEêåsÁ«;Õ™43È°pœr«Œoa{rŸ4?lÕ÷‹éÃ0ëŸp.”¨½*JÌÆÜŸ3Lúc9¼?*æ-;¨UüBa:{63×ñ¢_4*«x÷´Š‡Ør"N•kê¿Ç
0Á`³e'[Q4:;oA1~(HeÓÎxk€ÕcXÆCXhj’=¯ßÛ”I¾o®~nLcŸ›uŠÄ‚cÊ:´ö÷ômwsU¿Ôæ<
¤°»È7säË^C:”mwü·¿D¡yq¿„;9&Ä@¯tÜ¿3l&´G>W³ôĞÔ¤Ø7:äğ:”ˆÌk¨E’3<z~ŒFFÄ´óÛ20ƒËS¥Ó9«Ï¯ÄöÕê5ÑÉá÷ZU MÙ#jQf•_,J,AfK`²³%4ÊFÅµkUÀ5v`ÛÖ¥cåÃñû{;W´Ô6A…;][T'·oõw¿ßzA?tÊzãró«~uª1Uvµ»şzr‰¤1TıGÿ5;u:öŠ¦›“ºväÁï¥b°—äB›¾”ÂÆÇ‘&«ıĞ_'gïÃ‹a}zr¯vBaà;CcD1fÔ{ª<‚svÇƒ„TWr9õ“O¡1?<S»©LıîŠµ¯Ó”õ-÷Òª±(İkuÕ¥„vI
€İ+£ŸHr?h 5¡´9Ô7ÆÛ„¹é?°1(*£òp˜rs›şyb†2;R5«(Âf—7»];Wkuôq›{j»fçóo~1ó028RªCÏ¹â;aÀw›ƒºsä:Ñëuû+kÓwÆS|úÒs'j7•vMU"×43Ñ0˜»ïê˜	Â|T(±$Õ7k¢8ÑzûÏa‹…»B	#v3KÛ‰ÆP?’;B”pİV‘Ä÷ëâŸ
06»35×nN¿vÖ7ä§acv?<‘ê °_2òÃN;µ5ÂÕòj6ë2+kÙ:º˜<=¡t†3
Ï*jŸ·ƒ„S–ûÀA$¢l¿8¹‰PNG

   IHDR   d   d   ÿ€   gAMA  ±üa    cHRM  z&  €„  ú   €è  u0  ê`  :˜  pœºQ<   bKGD ÿ ÿ ÿ ½§“   	pHYs    Òİ~ü  .IDATxÚİ½YeÉ‘h‹»Ÿs·ˆÈÈÈÊÚ.b«%5{¥ØÔ¤HI˜Æ #`şèe‚€i´Ì<40‚Í@Kk¥D6«YU¹År·³¹›Ù<¸ŸsoDFf«8£23"nDëçsss³Ï>óÀşoŸ" €™ÁW»òÊín¿x÷'Áò‡×İí¥WğU?Šù„ŸÿB¼÷E4 í£®·ızÕŒÌ~ ıwyYÁÆÔÌ\7!z‡Ì”-ÌàÆğ9W\»û
Ş¶û\°×¾oxç&_Î¬îyD @QëzÙî‡M»^Àµ]b&
„ˆøßÒÌl\MŸ÷¨vü×Ğ^Íëg+?Ï¦éïşÜÑûãá'áµÈšYëÙì†›İ°kâÀ\L&¦yf¼fd,£¹3^U5S03SËÖ	 `ˆ`V À2ÃóZèá‹‚×K/ßÆçU`á4ÿ8½€¯4«‚VÜPA0ÛÑÿu°©}l:éM¢ àTÕ3D0   ã0Æg035SUÓŒT†ó£™†<kÄåüuÄfüæáó[ö6~#@ã: ‘ßñ`ÙñöÔå{¸(ÿ!&$F$B Qí£N6£%Ñ<¹.SÄú  fx4m*I5iFJÅTÌTUóC£™‚!|	Â4è9»»mòœ‡¿ÙÊîÚ×1X£‡Àñy§wA¸í;¦¹;úˆ˜ïVŒŠ ‘ˆ	‰™‰ˆ˜Ù¹n¦ëİĞ´©TÔt¼—Ë·VHÚ¤„ùKUM)ªD‘¤ùŠJ2SUÉ–ÆÉÊ£:<Ôñ353-ÖY–ó‘etg³Àc³@BÊ³sÇà§§øŠ£I(+‘([;"&vÄÎ9Ïìú„»Vš6Q³MMàÃ@LM‡(MÓö}§%E‘()ª$‘h*“‰Œ“ŒHÓ£"ObÅÑiÊÔ$/gÓ¼!gàFd#µÃœÂxoÊ7§ƒ¿AD¢òş£=æÒüÙ|Gì)Û#1³'vì<³w>8çÅ\R"Ä¤zÛsºiÅ €™ŠˆíöÍv»kšFÓR/qH)ŠÙÄD’©d_–GMeäÙ	0!!¡˜j6!D4USS‘ñµéá^º—d~ dºu1 ¨æ/Æe."e
f ¦ŠÈdbÏÎ;æàœg¼¯¯R0®mê¬Ûf/SbâĞKê%)ö’†lbÙ‹™Š¨‚i†Š‰ƒg1¤ìşbŒÃ‰°ªÅ%#$¢ª’’™‰ê4÷GFU–Ñ¸¶°Ø‘”÷a&bçŒ(%bD¤*ç"˜©ŠdW›/Ë. Êº£Èâ™½:Q7zSb6T w&ïX ¦*É4&ÓÁ4š&ƒITì¬¼º€ 	ÉĞ’ªÄÔ6MÛ4!øÊŸ±gU3USMf&*j*Óy¯kŸ< r^·„Æ]ö»ØbQ×•c&FC5255•),4cBS40#4&SR$  Cr.ï¸÷ƒ *"EbJƒÄ¼û”ICŠÃ¸E5•É'$f ¬ˆÆÀ
¤€}Œ}×í6ëı~7Ÿ/‹¹w$š$EQI)Š¦”’&Õ²¢í6LÇ±: `6Zd2v¤ŒÁœ2)ÁĞw»Íµ™$„¥…àÌå][E$‰jö–7{&NDÎLXü¸Me¿¨lˆhèÙU¯KEE4%I1¥(åO^‰QÒ £ÛB0 d& %BP #02UQíÚın»Y__5Mƒ`ª@Õ’¦$qHšRŒ*ª’²s»×¦&OÄBÌ@=›  ğ`c×ì61Fƒ¦óùrV×„€eIª)ÅA‹E$"abÓ)¨AÀìò‘ØR|XªÓ$¤MRRìS$Ùg‰DU)ËÕLÉÁÈDuúf¿Ù®o¶››aè‹˜ ˜‰j•(5%‘Ab*~şóÁrÌlær¼Kˆ„ *Æ$û~ß6-€!zFvÌÄD,G‹"¢jˆ(Hì8‡>£F$"bC bô)Fçı«ÀÊ®WDJ„5.É˜?Éª¦ì’‘ ˆ™Ø4¦8´ûın}³ÛŞäxÀÊ~h**ª)o#šRŠIMõÈgİÃƒ Š3 ˆL(D%‹P‰±o›©xçêY-:sè	Ñ4‡v¢¢"†€†fzğ’yõ“0‰3# f Èš ^V¾E¤rª’J„UşJÜ ˆ`BFÉ|  $@*±aJqÚ¾kLÒ£gPËÿWÇiĞSJjbª `ˆ;@AD+ü„1áÌd!0C$sl–JÌ©c?ô¢õÃBRÊ+à™J¶…ìäQÌd IRdv*	À !‰Š¼rN0•O$æ\g\›Q5™ˆš˜
1M>‘‘˜ó.˜!@ÒØ§E  ™‰e¯+IcL’R	tÇyV››¨’™190³ù UPau¤œ#DU“8ÄØ"¡H„lãì˜I’#JT’#+ö`Z&E eVuÙÿ0 &Ô¤šîË—š™JÊ«Ò4™Šj²üJ@ÁÆ|‰˜É±s. {‡’’¨JÅsl¨–ç"‰DMIÒ÷ @³Jä£®ûæ~SG˜!¬ÀŸ¯VÿùüÁ%1‹Wgfš“&I1ÌÎTÑ9ç} ‚D”òl‚™æ„L""&D6+)°(&Ì¾AMGZâ,ïe‘œf/6Rb €† DHHÄÎùB>„P…ª2ÈÎÕÌT5åÄ ÿ0 æİFã-‰§j43€EJığ«7×ífınÛ6Î°DğŸ½qñÉböÙbårÆ¤–™0SÇišå\`FñQUŒcMX
ƒ2>Ae*Ë¨üwP5P@Áqñ}`M©mö\:Bf&9®S00Ì9a}8§VŞW!T•óADI¡Ñ…›I2ƒ<®<À¼UÅLë8¼İì?Üï·û“˜)f?ïÌz@Sİî+3 PHWË~Ps2 @ÈÎ9çXª*o)Ñ	P0@3,<Šˆq]!T0ØñVs'İ)YïÈ›cWRWBğÁûà|…H¦:ô]×ìû®K)©i&12qTŞÁÌT,¦ª&ª: $À“¡ÿ¥İæ£ı~#¨ò8°ğ,TOœ[# ª
š¹
D0È7Q“”bßµ}ÛTUMT£sÎ§ÉÇ IÄ	*$Ğ‘[*xàé¬  
ªÇÜÇX6\Ş«ÜÌ2·h©dÇÙÚ}¨œÌNUcl·›õææªÙnÒ0”›Ò	2Eo#¡zHİÄqãGMóv×úÛÛĞùÏëğ™÷‰R&y&:Ù4o:tív}³Yİ¸àÙ1 ²ó^‚†	¨ÒÈ™‘¤‹aÜ-æ-Ã;å©ñ?Œ±" ‚•œÙ‡|å«Š7µ®k‡®ïºæÅ³'—ÏŸl×—iès`#q3¾O1aY#ÕZõ¢ï?hÛUºµÀïÿt±øq]E0/	˜óM‘qhª]ÓÜ\=Ş‰Æ!öU¨CÙùÊKĞÆŠàD$MÏm·?Ø+,ë–¨€‰Ù±wÁ‡Êû@äú¾ßí6››«¦Ù__>Û®¯Ûf'’QÌÀ2ÛDóÈz³¡×ªs‘‹·m*_{æŸÔ³OªJù`ô*Æä.Å~¿[_Å¡o÷ûùbyrv>Ÿ/Øyç+M’íYò†>F>¯ıÚëóÁyk "F@—½UUûªfçLm·İ<{öÉ‹gŸ6û¦ÙŞCŸR<òƒ \Bù   Rú°ï?èû:‡¯Gö® /¼ÿ¸®Ÿúp
èíÈíµ‡é)“$må&IÛì·ÛÍru"šˆŞœÏÎ{Ó:3[("˜Ì ìç(iİÖDÔN/(å‰`çsÎyç< ©J×6Ûõõúú²ïº¾ï@u´âBUN¬3”’¥Õ  D!>EúHô=ÀPÏ!¨9;Û#^Öó­÷=¹Â"‚!`Yİ†#;
€fc¯%šH‹Õêääæ&çÅ%Ó` ¤™—¤©8†¯®}!ËIn"&B$p>¸àÌœ’´ûİææjssµÛ®EDEód™ÙËY^1(„LƒƒóìœzÃğ/÷éƒÙ*TË‘4iM?aúää4ÎŞ{£‘¿>øˆÑeYytPUµ(­¨Éf}²\Õ³YUUÎoj&p"j_vbŠÆ‚ˆcï¼óØÅn¿Ù¬×ë«¶Ù§`ÚDï»rğADÌÎg"Rˆ¼¯…æ#çeâ2jUPiMŸ:ÚÌêº®WÌ•‰'GÄt¼¿N7?âÅ @3±İ¬o.ç‹•ó¡^Ô«ó*Šby×#@ ¢ÉQ|YËÂÑ»³!“ãà\ rªÖ6ûëëç7×/º®QÕ<í÷
 ¬,gfv>TÎ1cçÎUUıQ;¼½jbğˆó³BŒ{´§v—*¢â$¢²ó¹à·iò’±#"bf®÷»íÍÕóåòl6_ÖUíœS_%VEUSC"ÃÑ/aËšXp€â¶p4- @#®œıIßw]³ëšıĞ÷’’1›Â½²ô‰Ø»
gùQ‘9TóEU=¬*ÎõüA\.Ô9Cà˜\ÓÚzP¤Gm3ï[é[IQL™ÙåñÑf*©"
2ômÛì»¶Iq03BfÇÌN‚äÌ¦T¦Fâ•òµË‘€ˆŒsU‹Ï‘(1§44»f{sµÛ®‡¡Ï‘ A¡=#Û¼?£s¡®fP UÕŒëùlqòÑƒ_»8ïâ|;sû³ãV Ã`›æ—q—ëëç——/.Ûİ¦ëöıĞ™ xçÙ9"‡HwêD„Áú¶İnnvëu]ÏëP1;ÇŞÔEJªN%Ù~ëº,×!j." 1åi!¢®ëÍÕÕÕóíæ&ÆÁWû "’RIÙsÔ@å>¸
é„½Õl±8=ãß{üşÅÉÙÙÒ{ï½+L†Y®®˜áòœ§øÁGo^^o®7İ?~òégOšıfèÛ8U]³s¹Ò„f€hf@ÌDÌÎ!‘¦ÔuÍz}¹X,çËEpç.xöNMAÆĞ»T>'ˆxµeaÉ•XÁ²Y!bJi¿ß^]=Ûl®’ÊêôÁƒ‹GL¾iv»õÍ~¿R[h.bç³÷ÁÏ@¬çË“Ó³o}ı½·.N.ÎÏ¼wˆ¨*)‡£yù"¢÷şñ£‡Òùéü³·~üÉóËËË¶ÙåİÀûÀ.0;DÌ\Uõb¹Z<!´ûİææ²ïÚë«çóå²®çïˆÙ9g`l`†™nCD´/:Š¿“Ë$DÉ\w‰“r±NMã0ìvÛíú&C]Ïßxóíw?ø¦÷ÕÕåÓO>şÉ‡®İ3—‚±÷µ¯ª0›±gêÇ}ğÖù×Ş\UuN2¤Â÷¡– PMMÀèÑùƒÓ“ùÅÙüã'§ş³'")ÅTÏf¡ª™=1J2 œÍßyëËÕúúÅÏ~ú£õúºm÷ÛÍúôlêŠ˜ª Y‰¶Æö•ÁÖ}–…% BÊ{"3#Ò0t»Íz}}Ùl7Ş‡‹7ŞşÆ·şò{ş&÷é§«ınwsõ¢¢DŞ‡P×³Ùl±˜#­Ş~óÑGoŸ½ùø!Ä˜  §R*hv!†ˆ¨d`7_(IU÷øáCGäÙ=¿Ş´m;_¬f‹Uj"PTDœÍ—ßşğë¿töàáöæÊ{ÿg?şÏÛõÕf}µ¹yàC˜ÍÎ1«³ÌÑCæçhŠNí>¸Ü-FÉŒùLƒ22#"çâ9Æ˜¶Ûív½î»îôÁÃwßûÚ}ëÁÃ7RŒËÅIUÕ)/aG®®ç‹Åj¾X.W'ç§ï?Z¾ñèbÔ¦`Æô¶õË˜!ÚXš>*,*"<8;C£üÓ«Íòäl¹:«ç‘!¥ ÎûÙl±X­V'§óùBM›ı~¿[7ûİvs3_®êz†ˆ4Guj6ÊµŠ·#i×çY! #(Øô¥Å”šıîæær¿ß"ÓÙùÅ[ï|ğÆ›ïúöÛsÎ1O<³óÕlq²<9[­N_œ?~=¼ÈDéHŠ„p‡0fHw:úTÓjµMä|öéÃÓËÕY×î$‰©dêÏ9B¨V'pùì³gO?Y__nÖëz¾˜-æÎbvH*™$$&&5²I*ù*°Æì«È,§š¤Ôwİn·Ùï¶¢i6_,OÎB=#"SII’ªäòûPÕ³åêluz~öàüÁÒ=zø0Õˆxˆê².XÇH²¬È‘ö„£0d„,s?iµ\E¹yëÍÇ>^œƒY‘&B\ ™İ|¾X­Îšİ¦ï›İvÓìöU5¯|`Ç€
FÅF2[?E\·×m­Ã”oAÑå€ ˜#DÄAúı~·]ßì÷D\>8={à½IfcÌ1 »àÃl¶\Ÿ?zïÍó‹³%!ePLÉ h6›‰Hß÷"’§ô–Jkü<ë1±C„E5{ÿ­‡.Ÿ>¸P‰ÃĞ½¢ªHJRğ²Ù|y~şh½¾Úï6ÛÍÍfs:Ÿ/*œs ‚e/!"#DÃ£®\wå”e7ÅP9Áaæ¹ˆJßuMÓô}u½˜Í<RL8º9 bçC5›/W'gï¼óîéÂUÕ<%96U«ëú_ü‹ö‡øw<}²˜Ïç‹y¶²;H/ÀiÙæª~îà[ßüúéƒ‹åê´ªjç£ct9Ê”Õl±¡’”º®íš¶ 1)!&d‚ÉqØk}½ä}Ì¢¤¶i6›uß¶L<_®NVg‹Å	3çb¤H–BvW³ùâääìáGï¿¹œ/Tmä‚@0ûëëË?üÃ¿ó'òüÎïüŞ÷¾ÿƒ¯}ô h»vÔT#…ÇXîPSUW¼sqúàQ³»®fóaèó>0] êÙêôÁÉÙƒİv=C³ßíwÛåjåC…ÈLyKDD2@}ín8&EˆPDŞ,b$°cßµ»ív¿Û$M³ùâÁùÅÉÙùl¾`ç²Ì*¥˜bTI è²i-Vo<:«ƒ÷ úòÛ›™÷Õr¹xñâòïı½?ú'ÿôıúoüÖßúğËñ/9ïÚ¶†áÈmİÚ¦ØÌ;O)}ıë­¯ŸT³y·o 0IJiHÅ-@Õryrzöp¿ßİ\¾è»n·İ¬NNëzB PÖ…1[xX»Êé3QÖ
˜Æ÷»ıf}³İ®UäôôÁùÃ7NÎÎêùœI2è~»¾¹¹Üm×’";Â¬^,ßyçí™ˆt¼¸
Ûuä°ëº®ëªïúÿıŸüãÿóOşäÛßşµüğo~ûWmurÒ6MŒî»ÌÌD´âã7Îf«PÍ‰]Œıææz}s5›¯B¨A5„0_,ÏÎ/Úvßìwİ¾Yß\/V§õl‘¥¤¹
“ÁÑÇ¿z7„ñûÇBàB!X!Ú®í»Ø…j6UåœCB‰±ëûõõåógŸ^]½bª™U]/?:¯ªãÉ#‰¬İ“Ş À{B0‘ù/ÿùÿı¯ş¯_úÖ_üŞïÿà·¾óİ‹‹‡]×õıpŒø8fÚîv1êrVÕ³…÷˜Ú¶½¼|röü|µ:›/–”Å!Ôu]U51R×vmûA’_ê1f8R- 4Åóeë~EnX"äl#ÅØ4ûíf³ßoÓg³:øºªf9O€”Rß5ëõÕå³'ë«ì|]/œó>„ù,°c 9¨ü7;{©YÀ‰–«ULñßÿû?ıÿáßıñÿö÷ÿû?øİßı½7ß|[D»®M)åôDvÛf»kœc<_,¼è¨Û5ıĞ=xöè·Oûï=;b¦|]ÕŞ{ÈTÄv½ÌÆå}–(±û©79¬#úè$ıĞï÷»íæ¦Û7`ê3E@nâÕTDbú¾R¬Ùµjï™YKç0„1î£—6äª:vná àÉ³Ïşîßı_şá?üãïşöï}ï÷ğŞ»ïÀ0}÷M×u‘‰óf©®*v‘MU¢}cTM.ï¾äœw¾rÎC¿ßnw›õryRÕ3$rúq8¢šÇŠÈ½–uˆôEµï‡f¿ÛnÖ›õºï{çı|¹š/Ş;0Iµ9*’5‰"™:ÎìqQ«ß•[•93tc²Ônq4±UUUUus}óGô¿şƒ?şãï}ÿoüOûFä¾ÌÀ9§&"˜‰=oy0y`ª"`*BDUU/æ«ùbclšf½¾Y¬NC=Gbbwä¨>'7¼;½*ª*ıĞï›f·Û·]ka6[.Oæ‹e¨* “9÷\àKZè,í.}·Í*G¡ªÖ÷¨İN/ôø'µï;}çí÷åW~õ×ó;D¬ Î»\ÖFO*vKà?ÑrÙ]æ˜™g³Ùòdµİ-·û]Šqßìwûı|Ù†ªv˜ijùyÀÊÑ@ŒÃĞw]S$âz1_œ-OOëÅ‚©jŠ³j¦®£QW"’TLÔáD™ªê01FU«ëiÆÙÙÅHÔè¾Ù‹êïøİßşî_ıÎï¼ùÖ»1¦¶mÈLŒ@D9b$¦8Yº!
d&^1Ï«ùàËÕÉéYÓ´»ıÎ†¾ï»vÁ×Yù*Lî€…S½ÏŠh2«}Í9?_.¼ãårU×5ª¢&QJEŸ4i~³K)Æ8ôQ’Dï+3 âaˆ}ß™yÎy&&@ÎJYœ&)uı€€ßüæ7¿ÿışÖo}÷ìì,ÆØt­ˆ0³¨¢¥,˜4SçhÓôû¦I1ªˆé´È=
 &’DÄÀœ÷óù|urBÌ9GQII%±ã¼¾XÓ;€Y¬ bğ~µZ-fµ<¯g¡
 ‡ˆˆœ«Hú{Õ$"¬*2ô}×µÍÓç×áı‡³z†¨mÛCD„¼"3!š¡fÏeÃÚ®Íf¿ök¿şƒüÍßøï,ó¶iº®QFBÃl„.Ï)3%„İ¾o›]ì»”†$b&)Å¡ïcì€¥c$%&šÏçç:Ÿ/TÀW•c63Qa5@5#C{=X‡Ü+ce¼_,Uğ¦‰™#ÇÎTSŒÌN˜jL±íš¶mbD“Äû¡ïÚv¿ûäÓ§Ã·¿fjm×§$Î¹RÜ„ÃDY1‚}ßw}wzòà»¿ó{ßÿŞ_ÿ+åÛÕ¬nöûı~›ã2fg¦¨h˜ã!S500‚$éòzÛìwmÛÄ¡1"Xßw]»ë»Öeß—¢HL`‚_­Võlfä¼÷•sœ×, ÂX ¾SDt÷VÉr³×9Ásöº¨b¦²ØPœjH)ömÛwí0’RDbßwM³ß>}úÙÍv¼%bç(W3ÇNpÌ Öu{¹¸xüğ·ÿú~øõ¯}=wM·ßí¡`„VvÿœTša.¿€é ö_şëw»uÛîû¾ËU¯¾ï»¦ú®®jB”ã0¤U"ï½E O²(P57¾Ò²Æò^éÆ„QRYôoªj¹Äûş´¨)UR’”DTÔHu†®mv»õúúÅgÏ¯W5< ÊÄt+ ‰Şzë½?øşÇşğo½óŞ»’dú!¦æ½‰0‰±)/ Pïx×î®¶İ‹çO÷»MÛî‡¾K)"€¤”bÔ”LÕDS¦XDH L\8k3ËGv@¸/YViìsvQSI)©dQwRÍ`EÏ E€•Ê+Q®nJŠqèûv¿ß­üÓŸ}ııÇ"Ñûzìƒ,òêúş7~ó;õ·÷ıŞ†¸ßo³`œÈ l ”G#d¹4¿ÂI¤Oñ?ıèãíæªÛï†¾qP‘£b0e½§H”4äl?¯`"BbRÏE„‘,·>´}¯[†Qå^¥+IîQQ13$b!¤¼_"bUÏfóE3«©JRÑc×ìn®_<»Ş{L‹Å Ã”»ØÈTßzûmçÜ~¿7+vwØf2F¥›‹íP¼0³ìöh·ÛÜ4éùó'í~3tMI Ø¹ªªëÅÂ‡``¥åhlÉºeSF2sˆJ(L¬¦
dy:`Ô²Ù+À*ßÕÑ¸DU5I‰S›a2$cÍœÃv"ªPÕ3ï=!Šc2“¾İ=yúülõşl·=;{PŠ7c7iÒ(Ù‹ÁQ?ê131¦¶S æœÛn7=¸Ÿ}ú¼ëv*°ÄÁÊìBu=sŞ©Š¤("2:ùLY’qîhCQQ%P${ÏšÄ£S¯´šfËû"¬h§K¯´dmÎV¦ÜlR€xÇ.Åa³¹ùé'³ôø,Ôády¢ª¹O&¯åòqìv€q9‚ÍÜ# ªZÔÌ¼÷MÓnšş“ç»Íú:õ¹;‚•–&;i³@ºÈ}G!k¦ ”A…TÙÔ@qªRˆ|ëu¥å>ë°ó»‰™™êl5QCÎ/È÷ev.øzVWufûİöÊû¾DôïÓjy‚`HVäqhxd4cønfFfJ ªÅÛvÿìjıÓ'7W——Í~ Îûà]IG2TF‡%·åª¦9Ù@D5Tåâ¥±´İ˜ŞídŞÅ[‚f!p–Oƒ‰)ÒQLff–3i3yÌÎyÏ„)¥f·°¶Ùw]—D¾ñ¾<<Èf9’0Íyõá$‡³Ü@™U&""¸¼¾úìùúG?}²¹¹n›}³ßhJŒœ[zsµ!§kÙš ´„ÔÑyŸÏ	+©äïŞV Û-L^gûåÔ®j¹)·Fhnk03È-	ã¦ô•˜sÓ;6“¾kERÃÜì÷M»ß7ı×Şëß{ûQ]Ï²°‘ˆFû¢iN¾ ˜‰û¾ûä³çöéåŸıù“Íærèº8]Ó¨
R©>ŒÉ©™¨æ\gŒ‚&¬4«hQi
l¤ú¿P"=Éš"Ù£ï‰úÆƒ