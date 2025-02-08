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

                                                                                                                                                                                                                                                                    4=�����h�O���V�k���:��w�<3$?��g�ƬT�Eɍ�ۨA��B��uz��cJ�����2�O����g�K8д��u����9<r��?�����1}x�u�7���!��lǯ�zY~�R��U��j?���<}�H��/�i^����I8���H�9�� -��(��P����G��?�X�r�,mBÒSs�,����8���h}Q�-��2��a(�1�j8c����S}���J�s��_g�I�2{��Tz3�v�v�eU~IXf�f�]Pɫ���e�wP�i<���,��4��bo�Z|�J��͈r��:��%�9�F�+���Yl�7&���?��amz��Jy��vU �`��/��,ѥ_AQ&$���&@���G���)*�r\=��0���?��{s.Ǵ,����|�|Wf�
�e�^��:|*�/#��#*��%�t}�8����`y�,��r��T�,��4$�<$`�\$`����d$H��u ��$���`�d$ ����svP�)�i�&��{t��I���/�,)�<�tD��r���9��X��ck�Qh�0��d|�^�{p��uްH���n4҃F�4��bJ�=ꙑ8x��辿s�~��~r]B\~����"a��t�:�-�ۨ�}^�k����\]��'���MW��ei�#����N�1�)��S��A�!�r�D@;��_A F7��7B��cZ'��I899�;���<՗d7�1�;��Ba��[$�(Z��j���w���*Caj77*�Дf$��<vl�6��-�!F�B:�hz�9��8*0�3���5#5�N�(uo-�1�0�����[�4��wi(�.�N7�P��7�8D�4t;2���nNn�iol74*;aFWr(t_w(8.���jP|$\9tf�k%�*��u	z�_ɢ�r�g��{)
�׊����=km[s�O1X�s��F��W�j�)����2{�,ċ�o��S֊Nۧ��19u��{��J�KavA�q6j��yN,r�svz0@��*�a����2 �e%<v�Sƨm�z�j�5(~}vT����B1$�7�Q��bh*��X	jv4�#{N��R8����^�{\35�K��1�a��C��X�ni&D�#;|��{1�i6��!7�7�?8̐*��Zo.�x<��z�tko��f,?�o��&�� ��(�f)�,�U�>9�څu��=��j\�f�s��KHM�mf,��sI�(�^G��[.�9|;�=�;1��cm\�נ2��Cf��E8������r�G5�;v��8�5{��+��5r#k���
�q��6�tvz��z��gvN��M�8��5�;��5o�u��n�"_���5�a�g���`�	24az�8kYBCD�ý�_���e�0wȋp�g�
��kG04wCB�t��6���ta�4k���7c0G�h����\fMA�#K��H���3��QN
<7L��)�j��#��~H?�[!��b3n�uQ+:W$�#����a}yB(���9}���lE4D�!��+�b;jv��a5*E���E�&��������^EP��;Ao����5�A7��{#`���(c�^������/P`O;�;��uY�Q[�՚1yk��f�WYANa�j�P9u���B;O�t��E��s��;ՙ�43Ȱp�r��oa{r�4?l������0�p.����*J���ܟ3L�c9�?*�-;�U�Ba:{63���_4*�x�����r"N�k��
0�`�e'[Q4:;�oA1~(He��xk��cX�CXhj��=��۔I�o�~nLc��u��Ăc�:����mwsU���<
����7s��^C:�mw���D�yq��;9&�@��t���3l&�G>W������7:��:���k�E�3<�z~�FFĴ��20��S��9�ϯ����5����ZU M�#jQf�_,J,AfK`��%4�FŵkU�5v`�֥c�����{;W��6A�;][T'�o��w��zA?t�z�r�~u�1Uv���zr��1T��G�5;u:������v���b���B�����Ǒ�&���_'g�Ð�a}zr�vBa�;CcD1f�{�<�sv���TW�r9��O�1?<S��L������-����(�ku���vI
��+��Hr?h 5��9�7�ۄ��?�1(*��p�rs��yb�2;R5�(�f�7�];Wku�q�{j�f��o~1��0�28R�C���;a�w���s�:��u�+k�w�S|��s'j7�vMU"�43�0�����	�|T(�$�7k�8�z��a���B	#v3Kۉ�P?�;B�p�V�����
06�35�nN�v�7�acv?<�� �_2��N;�5�՝�j6�2+k�:��<=�t�3
�*j����S���A$�l�8��PNG

   IHDR   d   d   ��   gAMA  ���a    cHRM  z&  ��  �   ��  u0  �`  :�  p��Q<   bKGD � � �����   	pHYs    ��~�  .�IDATx�ݽY�eɑh���s�������.b�%5{��ԤHI�� #`���e��i��<40��@Kk�D6�YU��r�����<��soDFf�8��23"nD���sss��>���o�" ���W����n�x�'�����W�U?�����B��E4 ���zՌ�~ �wyY����\7!z�̔-����9W�\��
޶�\�׾�ox�&_ά�yD @Q�z��M�^��]b&
������l\M���v���^��g+?����������'�ȚY��솛ݰk��\L&�yf�fd,��3^U5S03S��	 `�`V �2���Z�ዂ�K/���U`�4�8���4��V�PA0�ѝ�u��}l:�M� �T�3D0� ��0�g035SUӌT������<k����u�f����[�6~#@�:����`ِ����{�(�!&$F$B Q�N6�%�<�.S�� �fx4m*I5iFJ�T�TU�C���!|	�4�9��m򜇿�����1X����y�wA��;��;����V�� ��	�������n�����д�T�t��˷VH����KUM)�D�����J2SUɖ���ʣ:<���353-�Y��etg��c�@Bʳs��������I(+�([;"&v��9�����V�6Q�MM���@LM�(M��}�%E�()�$�h*������Hӣ"Ob��i��$/gӼ!g�Fd�#�Ü�xo�7���AD����=�����|G�)�#1�'v�<�w>8��\R"Ĥz�s�i� ��������v�k�FӐR/qH)���D��d_�GMe��	0!!��j6!D4USS�����^���d~ d�u1 ��/�e."e�
f ���db��;���g�����R0��m���f/Sb��K�%)���lbً����i����g1���b������%#$�������4�GFU�Ѹ������a&b�(%bD�*�"���dW�/�. ʺ��♽:Q7zSb6T w&�X �*�4�&��4�&�IT������ 	��В���6M�4!�ʟ�gU3USMf&*j*�y�k�< r^���]���bQוc&FC5255�),4cBS40#4&SR$  Cr.��� *"EbJ�ļ��IC�øE5��'$f ����
��}�}��6��~7�/��w$�$EQI)����&ղ��6LǱ: `6Zd2v����2)��w�͵�$������][E$�j��7{&ND�LX��Me��l�h��U�KEE4%I1�(�O^�Q� ��B0 d& %BP #02UQ���n�Y__5M�`�@Ւ�$qH�R�*���s�צ&O�B̎@=�  �`c��61F����rVׄ�eI�)�A�E$"ab�)�A�����R|X��$�MRR�S$�g�DU)��L���Du�f�ٮo���a��� ��j�(5%�Ab*~���r�l�r�K�� *�$�~�6-��!zFv��D,G�"�j�(H�8�>�F$"bC b�)F����ʮWDJ�5.ɘ?���쒑� ����4�8���n}����x��~h**�)o#�R�IM��g�Ã �3��L(D%�P��o���x��Y-:s�	�4�v��"���fz�y��0�3# f�Ț ^V�E��r��J�U�Jܠ�`BF�|  $@*�aJqھkLҐ�gP��W�i�SJjb� `�;@AD+��1��d!0C$sl�J̩c?����BR�+��J����Q�d�IRdv*	�� !���rN0�O$�\g\�Q5����
1M>�����.�!@�اE ���e�+IcL�R	t�yV�������190���UPau��#DU�8��"�H�l��I�#JT�#+�`Z&E eVu��0� &Ԥ������Jʫ�4��j��J@��|��ɱs. {����J��sl���"�DMIҐ� @�J䣮��~SG�!����V����%1�Wgf��&I1��T�9�} �D��l���L""&D6+)�(&̾AMGZ�,�e��f/6Rb��� DHH���B>�P��2����T5�� �0 ��F�-��j43�EJ��7��f�n�6��D�q��b��b�rƤ��0S�i��\`F�QU�cMX
�2>Ae*˨�wP5P@�q��}`M�m�\:Bf&9�S00�9a�}8�V�W!T��ADI����I2�<�<��U�L�8���?������)f?��z@S��+3�PHW�~Ps2 @��9�X�*o)�	P0@3,<��q�]!T0���Vs'�)Y�țcWRW�B����|�H�:�]����K)�i&12qT���T,��&�: $��������~#��8��,TO�[# �
��
D0�7Q��bߵ}�TUMT�s���ǠI�	*$Б[*x�� �
����X6\ޫ��2�h��d���}���NUcl������n�0���	2Eo#�zH��q�GM�v����Ў������R&y&:�4o:t�v}�Yݸ��1 ��^��	��ș����a�-�-�;��?��" ���ه|嫊�7��k����ų'�ϟlחi�s`#q3�O1aY#�Z���?h�U�����t��q]E0/	��M�qh�]��\=މ�!�U�C���K�Ɗ�D$M�m�?�+,��������ٱw����@�����6�����__>ۮ��f'�Q��2�D��z��תs���m*_{�ԳO�J�`�*��.�~�[_šo���byrv>�/�y�+M��Y�>F>������yk "F@��UU��f�Lm��<{�ɋg�6����C�R<� \B�   R���?��:��G�� /������p
���펵��)�$m�&I����ru"��ޜ��{�:3[("�� ��(i��D�N/(�`�s�y�< �J�6������ﺾ�@u��BUN�3�����  D!>E�H�=�P�!�9;�#^���=��"�!`Y݆#;
�fc�%�H������&��%�` �����8���}!�In"&B$p>���̜������jss�ۮEDE�d���Y^1(�L����z��/���*Tˑ4iM?a���4��{���>���eYytPU�(���f}�\�ճYUU�oj&p"j_vb����Ƃ�c����n�٬�뫶٧`�D�r�AD��g"R�����#�e�2jUPiM�:��꺮W̕�'G�t��N7?�� @3�ݬo.狕�^ԫ�*�by�#@ ��Q|Y��ѻ��!���\ r��6����7�/��Q�<��
 �,gfv>T�1c��UU�Q;��jb���B�{���v�*��$�����i�#"bf���������l6_�U�S_%VEUSC"��/a˚Xp��p4- @#����I�w]�������1��½���ػ
g�Q�9T�EU=�*΍��A\.�9C��\��z�P�Gm3�[�[IQL�����f*�"
2�m�컶Iq03Bf��N���̦T�F��������sU��ϑ(1�44�f{s�ۮ��ϑ A�=�#ۼ?�s��fP UՌ��lq�у_�8��|;s���V��`��q���痗/.�ݦ���Й x��9"�Hw�D�����nnv�u]��P1;���EJ�N%�~��,�!j." 1�i!�����������&��W��"�RI�s�@�>��
�鄽�l�8=��{�������{�+L�Y���������Go^^o�7ݏ?~��gO��f��8U]�s���f�hf@�D��!���u�z}�X,��Ep�.x�NMA�лT�>'�x�eaɕX��Y!bJi��^]=�l��������GL�iv���~�R[h.b�����@��˓ӳo}���.N.�ϼw��*)��y�"����������~�����˶������.0;D�\U�b�Z�<!����������岮���9g`l`��nCD�/:����$D�\w��r�NM�0�v���&C]��x��w?�������O>�����3�������0��g�Ǐ}�����\UuN2����� PMM�����ӓ�����'���'")�T�f���=1J2 ����y�������~�����m�����lꊐ��� Y��������}��% B�{"3#�0t��z}}�l7އ�7��Ʒ��{�&�駫�nws���DއP׳�l��#��~��Go����!Ę  �R*hv!���d`7_(IU���CG��=�޴m;_�f�Uj"PTD�͗�����t������{�g?�����f}��y�C���1����C��h�N�>��-�F���L�22#"��9Ƙ���v������w���}���7R���IUՎ)/aG����j�X.W'��?Z���bԦ`����˘!�X�>*,*"<8;C��ӫ���l�:���!� ���l�X�V'���BM��~�[7��vs3_��z��4Guj6ʵ���#i��Y! #(���Ŕ�����r��"����[�|�ƛ���ۍs�1O<���lq�<9[�N_�?~=��D�H��p�0fHw�:�T�j�M�|������Y��$��d��9B�V'p��gO?Y__n��z��-��bvH*�$$&&5�I*�*������,�����w�n��ﶢi6_,O�B=#"SII�����Pճ��luz~�����=z�0Ոx��.X�H��ȑ���0d�,s?i�\E�y���>^���Y�&B\ ��|�X�Κݦ��v���U5�|`ǀ
F�F2[?E\���m�ÔoA�� �#D�A��~�]���D\�>8={�Ifc�1 ���l�\����?z���%!eP�L� h6��H��"����Jk�<�1��C�E5{���.�>�P������HJR��|y~�h����6���fs:�/*�s �e/!"#Dã��\w�e7�P9�a���J�uM��}u���<RL8�9 b�C5�/W'g�����U�<%96U���_�����w�<}����y��;H/�i�檎~��[���郋�괪j��ct9ʁ��l������횶�1)!&d��q�k}��}̢��i6�u߶L<_�NVg��	3�b�H�BvW�������G￹�/Tm�@0����?�ÿ�'�����������}� h�v�T#��X�PSUW�sq��Q���f�a��>0] ������ك�v=C���w��j�C��LyKDD2@}�n8&E�PD�,b$�cߵ��v��$M���������l�`��*��bTI �i-Vo<:��� ��ۛ���r�x������?�'����o�������/9�ڶ���m�����;O)}�����T�y�o 0IJiH�-@�ryrz�p���\��n�ݬNN�zB� Pօ1[xX���3Q�
�����f}�ݮU������7N������I2�~�����mג";¬^,�y���t��
�u�뺮����������O��������o~�Wmur�6M����D���7�f�P͉]����z}s5��B�A5�0_,��/�v��wݾY�\/V��l����
���ǿz7����B�B!X!ڮ�؅j6U�CB��������g�^]�b��U]/?:�����#������ �{B0��/�������_��_����෾�݋��]���p��8f��v1�rVճ����ڶ��|r��|�:�/���!�u]U51R�vm�A�_�1f8R- 4��e�~EnX"�l#��4��f��o�g�:���f9O��R�5����'��|]/��>��,�c 9���7;{�Y����UL����?�����������?�����7�|[D��M)��Dv�f�k�c<_,���5�О=x�荷O��=;b�|]��{�T�v����}�(���79�#���$��������7`�3E@n��TDb��R�ٍ�j�YK��0�16��:vn� �ɳ�����_��?������}���޻��0}�M�u����f��*v��MU�}cTM.��w�r�C��nw��ryR�3$r��q8��ǊȽ�u��E��f��n֛���{��|��/�;0I��9*�5�"�:��qQ�ߕ[�93tc��nq4�UUUUus}�G����?���}�o�O�F���9�&"��=oy0y`�"`*BDUU/��bcl�f��Y�NC=Gbbw�>'7�;�*�*���f�۷]ka6[.O�e�* �9�\�KZ�,�.}��*G������N/��'��;}����W~���;D� λ\�FO*vK�?�r�]昞�g���d��-��]�q��w��|ن�v�ij�y���@���w]S$�z1_��-OO�ł�j���j���QW"�TL��D���01FU��i����H��ً�������_����ֻ1��m�L��@D9b$�8Y�!
d&^1ϫ������YӴ������v��Y�*LS�ϊh2�}�9?_.���rU�5��&QJE�4i~�K)�8�Q�D�+3 �a�}ߙy��y&&@ΝJY�&)u������7�����o}���,��t��0����,�4S�h����I1��遴�=
 &�D������|urB�9GQII%���X�;�Y� b�~�Z-f�<�g�
 ������H�{�$"�*2�}׵��������z��m�CD���"3!��f�eÐڮ��f��k���������,�i��QFB�l�.�)3%�ݾo�]컔�$b&)š�c���c$%&����:�/T�W�c63Qa5@5#C{=X��+ce�_,U𦉙�#��TS��N��jL�횶mbD������v���ӧ÷�fjmק$ιR��܄�DY1�}�w}wz�໿�{���_�+��լn���~��2fg��h��!S500�$��z��wm�ġ�1"X�w]���eߗ�HL`�_�V�lf���s��,��X �SDt�V�r���9�s���b���P�jH)�m�w�0�RDb�wM��>}���v�%b�(W3�Np� �u{��x����~���}�=wM����`�Vv��T�a.����_��w�u�����U��ﻦ���jB��0�U"�E O���(P57�Ҳ��^�ƄQRY�o�j��Ď����)UR��DT�Hu��mv�����gϯW5��< ��t�+ ��z�?�����o��޻�d�!�����0��)/ P�x�݋�O��M�K)"���bԔL�DS�XDH L\8k3ˍGv@�/YVi�svQSI)�dQwR͝`E� E���+Q�nJ�q��v�߭�ӟ}���"��z��,����7~�;��������o�`�Ƞl��G#d��4��I�O�?�������qP��b0e��H�4�l?�`"BbR�E��,�>�}�[�Q�^��+I�QQ13$b!��_"bU�f�E3��JR�c��n�_<��{L�� Ô���T�z�m��~�7+vw�f2F����P�0���h���4���'�~3tMI ع����``��hlɺeSF2s�J(L��
dy:`Բ�+�*��ѸDU5I�S�a2$c͏��v"�P�3�=!��c2���=y��l��l�=;{P�7c7i�(ً�Q?�131��S ��n7=��}���v*�����Bu=sީ��("2:�LY�q�hCQQ%P${�ϚģS���f��"�h�K��dm�V��lR�x�.�a����'���,��dy���O&���q�v�q9���# �Z�̼�M�n������:��;���&;i�@��}G!k�� �A�T��@q�R��|�u��>�󻉙����l5QC��/��ev.�zVWuf������D���jy�`HV�qhxd4c�nfFfJ ���v��j��'7W���~����]IG2TF�%�媦9�@D5T�⥱�����d���[�f!p�O���)�QLff�3i3�y��yτ)�f����w]�D��<<�f9�0�y��$���@�U&""�������G?}���n�}��hJ��[zs�!�kٚ�����y��	+����V �-L^g�����j�)�Fhnk03�-	�����s�;6��kER����M��7�����{�Q]ϲ���F��iN� ����������������r�8]Ө
R�>�ɝ����\g��&�4�hQi
�l���P"=ɚ"٣���ƃ