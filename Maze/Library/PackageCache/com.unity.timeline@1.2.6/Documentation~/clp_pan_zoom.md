Constructor4Viable ();
			Console.WriteLine ("StaticConstructor4 is viable"); /* a TAE doesn't make a type unusable */
		} catch (TypeInitializationException  e) {
			Console.WriteLine ("StaticConstructor4 not viable");
			Environment.Exit (13);
		}
	}


	class StaticConstructor5 {
		public static bool catched_exception = false;
		static StaticConstructor5 ()
		{
			Driver.mre1.Set ();
			Driver.mre2.WaitOne ();
			try {
				throw new Exception ();
			} catch (Exception) {
				Console.WriteLine ("Catched exception in cctor");
				catched_exception = true;
			}
		}
	}

	static void Test5 ()
	{
		bool catched_abort = false;
		Driver.mre1.Reset ();
		Driver.mre2.Reset ();
		Thread thread = new Thread (() => {
					try {
						new StaticConstructor5 ();
					} catch (ThreadAbortException) {
						Console.WriteLine ("Catched thread abort");
						catched_abort = true;
					}
				});
		thread.Start ();

		Driver.mre1.WaitOne ();
		thread.Abort ();
		Driver.mre2.Set ();

		thread.Join ();

		if (!StaticConstructor5.catched_exception)
			Environment.Exit (14);
		if (!catched_abort)
			Environment.Exit (15);
	}

	public static bool got_to_the_end_of_outer_finally = false;
	public static bool got_to_the_end_of_inner_finally = false;

	class StaticConstructor6 {

		static StaticConstructor6 ()
		{
			try {
				Setup6 ();
			} finally {
				Driver.got_to_the_end_of_outer_finally = true;
			}
		}

		[MethodImplAttribute (MethodImplOptions.NoInlining)]
		public static void Setup6 () {
			try {
			} finally {
				Driver.sema1.Release ();
				Thread.Sleep (1000); /* hopefully we get woken up here */
				Driver.got_to_the_end_of_inner_finally = true;
			}
