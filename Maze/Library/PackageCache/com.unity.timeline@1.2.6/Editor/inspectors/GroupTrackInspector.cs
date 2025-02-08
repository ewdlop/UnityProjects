
		public void RemotingConfiguration () 
		{
			SecurityPermissionAttribute a = Empty ();
			a.RemotingConfiguration = true;
			Assert.AreEqual (SecurityPermissionFlag.RemotingConfiguration, a.Flags, "Flags=RemotingConfiguration");
			a.RemotingConfiguration = false;
			Assert.AreEqual (SecurityPermissionFlag.NoFlags, a.Flags, "Flags=NoFlags");
		}

		[Test]
		public void SerializationFormatter () 
		{
			SecurityPermissionAttribute a = Empty ();
			a.SerializationFormatter = true;
			Assert.AreEqual (SecurityPermissionFlag.SerializationFormatter, a.Flags, "Flags=SerializationFormatter");
			a.SerializationFormatter = false;
			Assert.AreEqual (SecurityPermissionFlag.NoFlags, a.Flags, "Flags=NoFlags");
		}

		[Test]
		public void SkipVerification () 
		{
			SecurityPermissionAttribute a = Empty ();
			a.SkipVerification = true;
			Assert.AreEqual (SecurityPermissionFlag.SkipVerification, a.Flags, "Flags=SkipVerification");
			a.SkipVerification = false;
			Assert.AreEqual (SecurityPermissionFlag.NoFlags, a.Flags, "Flags=NoFlags");
		}

		[Test]
		public void UnmanagedCode () 
		{
			SecurityPermissionAttribute a = Empty ();
			a.UnmanagedCode = true;
			Assert.AreEqual (SecurityPermissionFlag.UnmanagedCode, a.Flags, "Flags=UnmanagedCode");
			a.UnmanagedCode = false;
			Assert.AreEqual (SecurityPermissionFlag.NoFlags, a.Flags, "Flags=NoFlags");
		}

		[Test]
		public void Unrestricted () 
		{
			SecurityPermissionAttribute a = new SecurityPermissionAttribute (SecurityAction.Assert);
			a.Unrestricted = true;
			Assert.AreEqual (SecurityPermissionFlag.NoFlags, a.Flags, "Unrestricted");

			SecurityPermission perm = (SecurityPermission) a.CreatePermission ();
			Assert.AreEqual (SecurityPermissionFlag.AllFlags, perm.Flags, "CreatePermission.Flags");
		}

		[Test]
		public void Flags ()
		{
			SecurityPermissionAttribute a = new SecurityPermissionAttribute (SecurityAction.Assert);
			a.Flags = SecurityPermissionFlag.Assertion;
			Assert.IsTrue (a.Assertion, "Assertion");
			a.Flags |= SecurityPermissionFlag.BindingRedirects;
			Assert.IsTrue (a.BindingRedirects, "BindingRedirects");
			a.Flags |= SecurityPermissionFlag.ControlAppDomain;
			Assert.IsTrue (a.ControlAppDomain, "ControlAppDomain");
			a.Flags |= SecurityPermissionFlag.ControlDomainPolicy;
			Assert.IsTrue (a.ControlDomainPolicy, "ControlDomainPolicy");
			a.Flags |= SecurityPermissionFlag.ControlEvidence;
			Assert.IsTrue (a.ControlEvidence, "ControlEvidence");
			a.Flags |= SecurityPermissionFlag.ControlPolicy;
			Assert.IsTrue (a.ControlPolicy, "ControlPolicy");
			a.Flags |= SecurityPermissionFlag.ControlPrincipal;
			Assert.IsTrue (a.ControlPrincipal, "ControlPrincipal");
			a.Flags |= SecurityPermissionFlag.ControlThread;
			Assert.IsTrue (a.ControlThread, "ControlThread");
			a.Flags |= SecurityPermissionFlag.Execution;
			Assert.IsTrue (a.Execution, "Execution");
			a.Flags |= SecurityPermissionFlag.Infrastructure;
			Assert.IsTrue (a.Infrastructure, "Infrastructure");
			a.Flags |= SecurityPermissionFlag.RemotingConfiguration;
			Assert.IsTrue (a.RemotingConfiguration, "RemotingConfiguration");
			a.Flags |= SecurityPermissionFlag.SerializationFormatter;
			Assert.IsTrue (a.SerializationFormatter, "SerializationFormatter");
			a.Flags |= SecurityPermissionFlag.SkipVerification;
			Assert.IsTrue (a.SkipVerification, "SkipVerification");
			a.Flags |= SecurityPermissionFlag.UnmanagedCode;

			Assert.IsTrue (a.UnmanagedCode, "UnmanagedCode");
			Assert.IsFalse (a.Unrestricted, "Unrestricted");

			a.Flags &= ~SecurityPermissionFlag.Assertion;
			Assert.IsFalse (a.Assertion, "Assertion-False");
			a.Flags &= ~SecurityPermissionFlag.BindingRedirects;
			Assert.IsFalse (a.BindingRedirects, "BindingRedirects-False");
			a.Flags &= ~SecurityPermissionFlag.ControlAppDomain;
			Assert.IsFalse (a.ControlAppDomain, "ControlAppDomain-False");
			a.Flags &= ~SecurityPermissionFlag.ControlDomainPolicy;
			Assert.IsFalse (a.ControlDomainPolicy, "ControlDomainPolicy-False");
			a.Flags &= ~SecurityPermissionFlag.ControlEvidence;
			Assert.IsFalse (a.ControlEvidence, "ControlEvidence-False");
			a.Flags &= ~SecurityPermissionFlag.ControlPolicy;
			Assert.IsFalse (a.ControlPolicy, "ControlPolicy-False");
			a.Flags &= ~SecurityPermissionFlag.ControlPrincipal;
			Assert.IsFalse (a.ControlPrincipal, "ControlPrincipal-False");
			