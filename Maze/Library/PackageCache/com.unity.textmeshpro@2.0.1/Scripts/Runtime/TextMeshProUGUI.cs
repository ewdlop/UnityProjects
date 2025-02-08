sert.AreEqual (0, start);
			Assert.AreEqual (0, end);

			iterator.NextSubpath (out start, out end, out isClosed);
			count = iterator.NextPathType (out pathType, out start, out end);
			Assert.AreEqual (3, count);
			Assert.AreEqual ((byte)PathPointType.Line, pathType);
			Assert.AreEqual (0, start);
			Assert.AreEqual (2, end);

			count = iterator.NextPathType (out pathType, out start, out end);
			Assert.AreEqual (4, count);
			Assert.AreEqual ((byte)PathPointType.Bezier3, pathType);
			Assert.AreEqual (2, start);
			Assert.AreEqual (5, end);

			count = iterator.NextPathType (out pathType, out start, out end);
			Assert.AreEqual (3, count);
			Assert.AreEqual ((byte)PathPointType.Line, pathType);
			Assert.AreEqual (5, start);
			Assert.AreEqual (7, end);
			
			// we don't want to be a bug compliant with .net
			/* 
			count = iterator.NextPathType (out pathType, out start, out end);
			Assert.AreEqual (0, count);
			Assert.AreEqual ((byte)PathPointType.Line, pathType);
			Assert.AreEqual (5, start);
			Assert.AreEqual (7, end);
			*/

			iterator.NextSubpath (out start, out end, out isClosed);
			count = iterator.NextPathType (out pathType, out start, out end);
			Assert.AreEqual (4, count);
			Assert.AreEqual ((byte)PathPointType.Bezier3, pathType);
			Assert.AreEqual (8, start);
			Assert.AreEqual (11, end);

			iterator.NextSubpath (out start, out end, out isClosed);
			count = iterator.NextPathType (out pathType, out start, out end);
			Assert.AreEqual (4, count);
			Assert.AreEqual ((byte)PathPointType.Line, pathType);
			Assert.AreEqual (12, start);
			Assert.AreEqual (15, end);

			iterator.NextSubpath (out start, out end, out isClosed);
			count = iterator.NextPathType (out pathType, out start, out end);
			Assert.AreEqual (2, count);
			Assert.AreEqual ((byte)PathPointType.Line, pathType);
			Assert.AreEqual (16, start);
			Assert.AreEqual (17, end);

			iterator.NextSubpath (out start, out end, out isClosed);
			count = iterator.NextPathType (out pathType, out start, out end);
			Assert.AreEqual (0, count);
			Assert.AreEqual ((byte)PathPointType.Line, pathType);
			Assert.AreEqual (0, start);
			Assert.AreEqual (0, end);
        }
        
        [Test]
        public virtual void NextMarker_Int32_Int32() 
		{
            GraphicsPath path = new GraphicsPath ();
			path.AddLine (new Point (100, 100), new Point (400, 100));
			path.AddLine (new Point (400, 200), new Point (10, 100));
			path.StartFigure ();
			path.SetMarkers ();
			path.AddBezier( 10, 10, 50, 250, 100, 5, 200, 280);
			path.StartFigure ();
			path.SetMarkers ();
			path.AddRectangle (new Rectangle (10, 20, 300, 400));
			path.StartFigure ();
			path.SetMarkers ();
			path.AddLine (new Point (400, 400), new Point (400, 10));

			GraphicsPathIterator iterator = new GraphicsPathIterator (path);

			int start;
			int end;
			int count = iterator.NextMarker (out start, out end);
			Assert.AreEqual (4, count);
			Assert.AreEqual (0, start);
			Assert.AreEqual (3, end);
			
			count = iterator.NextMarker (out start, out end);
			Assert.AreEqual (4, count);
			Assert.AreEqual (4, start);
			Assert.AreEqual (7, end);

			count = iterator.NextMarker (out start, out end);
			Assert.AreEqual (4, count);
			Assert.AreEqual (8, start);
			Assert.AreEqual (11, end);

			count = iterator.NextMarker (out start, out end);
			Assert.AreEqual (2, count);
			Assert.AreEqual (12, start);
			Assert.AreEqual (13, end);

			// FIXME - should return all 0'z?
			/*
			count = iterator.NextMarker (out start, out end);
			Assert.AreEqual (0, count);
			Assert.AreEqual (12, start);
			Assert.AreEqual (13, end);
			*/
        }

		[Test]
		public void NextSubpath_NextMarker()
		{
			GraphicsPath path = new GraphicsPath();
			
			path.AddLine (10, 10, 50, 50); // figure #1
			path.AddLine (50, 50, 80, 80);
			path.AddLine (90, 90, 100, 100);
			path.SetMarkers (); // marker #1
			path.AddLine (150, 150, 180, 180);
			path.SetMarkers (); // marker #2
			path.StartFigure (); // figure #2
			path.SetMarkers (); // marker #3 is actually marker #2
			path.AddRectangle (new Rectangle (200, 200, 200, 200)); 
			path.SetMarkers (); // marker #4
			path.AddLine (150, 150, 180, 180); 
			path.StartFigure (); // figure #3
			path.AddBezier (400, 400, 500, 500, 600, 600, 700, 700);
			path.AddBezier (450, 450, 550, 550, 650, 650, 750, 750);

			GraphicsPathIterator iterator = new GraphicsPathIterator (path);

			int start;
			int end;
			bool isClosed;
			int count = iterator.NextMarker (out start,out end); // marker #1
			Assert.AreEqual (5, count);
			Assert.AreEqual (0, start);
			Assert.AreEqual (4, end);

			count = iterator.NextSubpath (out start,out end,out isClosed); // figure #1
			Assert.AreEqual (7, count);
			Assert.AreEqual (0, start);
			Assert.AreEqual (6, end);
			Assert.AreEqual (false, isClosed);

			count = iterator.NextMarker (out start,out end); // marker #2 (and #3)
			Assert.AreEqual (2, count);
			Assert.AreEqual (5, start);
			Assert.AreEqual (6, end);

			count = iterator.NextSubpath (out start,out end,out isClosed); // figure #2
			Assert.AreEqual (4, count);
			Assert.AreEqual (7, start);
			Assert.AreEqual (10, end);
			Assert.AreEqual (true, isClosed);

			count = iterator.NextSubpath (out start,out end,out isClosed); // figure #3
			Assert.AreEqual (2, count);
			Assert.AreEqual (11, start);
			Assert.AreEqual (12, end);
			Assert.AreEqual (false, isClosed);

			count = iterator.NextMarker (out start,out end); // marker #5 (end)
			Assert.AreEqual (4, count);
			Assert.AreEqual (7, start);
			Assert.AreEqual (10, end);

			count = iterator.NextMarker (out start,out end); // marker #5 (end)
			Assert.AreEqual (10, count);
			Assert.AreEqual (11, start);
			Assert.AreEqual (20, end);

			// we dont want to be bug compliant with .net
			/*
			count = iterator.NextMarker (out start,out end); // no more markers
			Assert.AreEqual (0, count);
			Assert.AreEqual (11, start);
			Assert.AreEqual (20, end);
			*/

			count = iterator.NextSubpath (out start,out end,out isClosed); // figure #4
			Assert.AreEqual (8, count);
			Assert.AreEqual (13, start);
			Assert.AreEqual (20, end);
			Assert.AreEqual (false, isClosed);

			// we dont want to be bug compliant with .net
			/*
			count = iterator.NextMarker (out start,out end); // no more markers
			Assert.AreEqual (0, count);
			Assert.AreEqual (13, start);
			Assert.AreEqual (20, end);
			*/

			count = iterator.NextSubpath (out start,out end,out isClosed); // no more figures
			Assert.AreEqual (0, count);
			Assert.AreEqual (0, start);
			Assert.AreEqual (0, end);
			Assert.AreEqual (true, isClosed);

			count = iterator.NextMarker (out start,out end); // no more markers
			Assert.AreEqual (0, count);
			Assert.AreEqual (0, start);
			Assert.AreEqual (0, end);			
		}

        
        [Test]
        public virtual void NextMarker_GraphicsPath() 
		{
            GraphicsPath path = new GraphicsPath ();
			path.AddLine (new Point (100, 100), new Point (400, 100));
			path.AddLine (new Point (400, 200), new Point (10, 100));
			path.StartFigure ();
			path.SetMarkers ();
			path.AddBezier( 10, 10, 50, 250, 100, 5, 200, 280);
			path.StartFigure ();
			path.SetMarkers ();
			path.AddRectangle (new Rectangle (10, 20, 300, 400));
			path.StartFigure ();
			path.SetMarkers ();
			path.AddLine (new Point (400, 400), new Point (400, 10));

			GraphicsPath path2 = new GraphicsPath ();
			path.AddLine (new Point (150, 150), new Point (450, 150));
			path.AddLine (new Point (450, 250), new Point (50, 150));

			GraphicsPathIterator iterator = new GraphicsPathIterator (path);

			iterator.NextMarker (null);
			iterator.NextMarker (path2);

			Assert.AreEqual (4, path2.PointCount);
			PointF [] actualPoints = path2.PathPoints;
			byte [] actualTypes = path2.PathTypes;

			PointF [] expectedPoints = new PointF [] {	new PointF(100f, 100f), 
														new PointF(400f, 100f), 
														new PointF(400f, 200f), 
														new PointF(10f, 100f)};
			
			for(int i = 0; i < expectedPoints.Length; i++) {
				DrawingTest.AssertAlmostEqual(expectedPoints [i], actualPoints [i]);
			}

			byte [] expectedTypes = new byte [] {	(byte) PathPointType.Start, 
													(byte) PathPointType.Line, 
													(byte) PathPointType.Line, 
													(byte) (PathPointType.Line | PathPointType.PathMarker)};

			for (int i=0; i < expectedTypes.Length; i++) {
				Assert.AreEqual (expectedTypes [i], actualTypes [i]);
			}

			iterator.NextMarker (null);
			iterator.NextMarker (null);
			iterator.NextMarker (null);
			iterator.NextMarker (path2);

			Assert.AreEqual (4, path2.PointCount);
			actualPoints = path2.PathPoints;
			actualTypes = path2.PathTypes;

			expectedPoints = new PointF [] {new PointF(10f, 10f), 
											new PointF(50f, 250f), 
											new PointF(100f, 5f), 
											new PointF(200f, 280f)};
			
			for(int i = 0; i < expectedPoints.Length; i++) {
				DrawingTest.AssertAlmostEqual(expectedPoints [i], actualPoints [i]);
			}

			expectedTypes = new byte [] {	(byte) PathPointType.Start, 
												(byte) PathPointType.Bezier3, 
												(byte) PathPointType.Bezier3, 
												(byte) (PathPointType.Bezier3 | PathPointType.PathMarker)};

			for (int i=0; i < expectedTypes.Length; i++) {
				Assert.AreEqual (expectedTypes [i], actualTypes [i]);
			}	
			
        }
        
        [Test]
        public virtual void Count() 
		{
            GraphicsPath path = new GraphicsPath ();

			GraphicsPathIterator iterator = new GraphicsPathIterator (path);
			Assert.AreEqual (0, iterator.Count);

			path.AddLine (new Point (100, 100), new Point (400, 100));
			path.AddLine (new Point (400, 200), new Point (10, 100));

			path.StartFigure ();
			path.AddBezier( 10, 10, 50, 250, 100, 5, 200, 280);
			path.StartFigure ();
			path.AddRectangle (new Rectangle (10, 20, 300, 400));

			path.StartFigure ();
			path.AddLine (new Point (400, 400), new Point (400, 10));

			iterator = new GraphicsPathIterator (path);
			Assert.AreEqual (14, iterator.Count);
        }
        
        [Test]
        public virtual void SubpathCount() 
		{
            GraphicsPath path = new GraphicsPath ();

			GraphicsPathIterator iterator = new GraphicsPathIterator (path);
			Assert.AreEqual (0, iterator.SubpathCount);

			path.AddLine (new Point (100, 100), new Point (400, 100));
			path.AddLine (new Point (400, 200), new Point (10, 100));

			path.StartFigure ();
			path.AddBezier( 10, 10, 50, 250, 100, 5, 200, 280);
			path.StartFigure ();
			path.AddRectangle (new Rectangle (10, 20, 300, 400));

			path.StartFigure ();
			path.AddLine (new Point (400, 400), new Point (400, 10));

			iterator = new GraphicsPathIterator (path);
			Assert.AreEqual (4, iterator.SubpathCount);
        }
        
        [Test]
        public virtual void HasCurve() 
		{
            GraphicsPath path = new GraphicsPath ();

			GraphicsPathIterator iterator = new GraphicsPathIterator (path);
			Assert.IsFalse (iterator.HasCurve ());

			path.AddLine (new Point (100, 100), new Point (400, 100));
			path.AddLine (new Point (400, 200), new Point (10, 100));

			iterator = new GraphicsPathIterator (path);
			Assert.IsFalse (iterator.HasCurve ());

			path.StartFigure ();
			path.AddBezier( 10, 10, 50, 250, 100, 5, 200, 280);
			path.StartFigure ();
			path.AddRectangle (new Rectangle (10, 20, 300, 400));

			path.StartFigure ();
			path.AddLine (new Point (400, 400), new Point (400, 10));

			iterator = new GraphicsPathIterator (path);
			Assert.IsTrue (iterator.HasCurve ());
        }
        
        [Test]
        public virtual void Rewind() 
		{
            GraphicsPath path = new GraphicsPath ();
			path.AddLine (new Point (100, 100), new Point (400, 100));
			path.AddLine (new Point (400, 200), new Point (10, 100));
			path.StartFigure ();
			path.SetMarkers ();
			path.AddBezier( 10, 10, 50, 250, 100, 5, 200, 280);
			path.StartFigure ();
			path.SetMarkers ();
			path.AddRectangle (new Rectangle (10, 20, 300, 400));
			path.StartFigure ();
			path.SetMarkers ();
			path.AddLine (new Point (400, 400), new Point (400, 10));

			GraphicsPathIterator iterator = new GraphicsPathIterator (path);

			int i;
			int j;
			iterator.NextMarker (out i, out j);
			iterator.NextMarker (out i, out j);

			iterator.Rewind ();
			iterator.NextMarker (out i, out j);

			Assert.AreEqual (0, i);
			Assert.AreEqual (3, j);
        }
        
        [Test]
        public virtual void Enumerate() 
		{
            GraphicsPath path = new GraphicsPath ();
			path.AddLine (new Point (100, 100), new Point (400, 100));
			path.AddLine (new Point (400, 200), new Point (10, 100));

			path.StartFigure ();
			path.AddBezier( 10, 10, 50, 250, 100, 5, 200, 280);
			path.StartFigure ();
			path.AddRectangle (new Rectangle (10, 20, 300, 400));

			path.StartFigure ();
			path.AddLine (new Point (400, 400), new Point (400, 10));

			path.Reverse ();

			GraphicsPathIterator iterator = new GraphicsPathIterator (path);
			PointF [] actualPoints = new PointF [14];
			byte [] actualTypes = new byte [14];
			iterator.Enumerate (ref actualPoints, ref actualTypes);

			PointF [] expectedPoints = new PointF [] {	new PointF(400f, 10f), 
														new PointF(400f, 400f), 
														new PointF(10f, 420f), 
														new PointF(310f, 420f), 
														new PointF(310f, 20f), 
														new PointF(10f, 20f), 
														new PointF(200f, 280f), 
														new PointF(100f, 5f), 
														new PointF(50f, 250f), 
														new PointF(10f, 10f), 
														new PointF(10f, 100f), 
														new PointF(400f, 200f), 
														new PointF(400f, 100f), 
														new PointF(100f, 100f)};
			
			for(int i = 0; i < expectedPoints.Length; i++) {
				DrawingTest.AssertAlmostEqual(expectedPoints [i], actualPoints [i]);
			}

			byte [] expectedTypes = new byte [] {	(byte) PathPointType.Start, 
													(byte) PathPointType.Line, 
													(byte) PathPointType.Start, 
													(byte) PathPointType.Line, 
													(byte) PathPointType.Line, 
													(byte) (PathPointType.Line | PathPointType.CloseSubpath), 
													(byte) PathPointType.Start, 
													(byte) PathPointType.Bezier3, 
													(byte) PathPointType.Bezier3, 
													(byte) PathPointType.Bezier3, 
													(byte) PathPointType.Start, 
													(byte) PathPointType.Line, 
													(byte) PathPointType.Line, 
													(byte) PathPointType.Line};

			for (int i=0; i < expectedTypes.Length; i++) {
				Assert.AreEqual (expectedTypes [i], actualTypes [i]);
			}	
        }
        
        [Test]
        public virtual void CopyData() 
		{
            GraphicsPath path = new GraphicsPath ();
			path.AddLine (new Point (100, 100), new Point (400, 100));
			path.AddLine (new Point (400, 200), new Point (10, 100));
			path.StartFigure ();
			path.SetMarkers ();
			path.AddRectangle (new Rectangle (10, 20, 300, 400));
			path.StartFigure ();
			path.SetMarkers ();
			path.AddLine (new Point (400, 400), new Point (400, 10));

			GraphicsPathIterator pathIterator = new GraphicsPathIterator(path);
			pathIterator.Rewind ();
			PointF [] actualPoints = new PointF [10];
			byte [] actualTypes = new byte [10];
			pathIterator.CopyData (ref actualPoints, ref actualTypes, 0, 9);

			PointF [] expectedPoints = new PointF [] {	new PointF(100f, 100f), 
														new PointF(400f, 100f), 
														new PointF(400f, 200f), 
														new PointF(10f, 100f), 
														new PointF(10f, 20f), 
														new PointF(310f, 20f), 
														new PointF(310f, 420f), 
														new PointF(10f, 420f), 
														new PointF(400f, 400f), 
														new PointF(400f, 10f)};
			
			for(int i = 0; i < expectedPoints.Length; i++) {
				DrawingTest.AssertAlmostEqual(expectedPoints [i], actualPoints [i]);
			}

			byte [] expectedTypes = new byte [] {	(byte) PathPointType.Start, 
													(byte) PathPointType.Line, 
													(byte) PathPointType.Line, 
													(byte) (PathPointType.Line | PathPointType.PathMarker), 
													(byte) PathPointType.Start, 
													(byte) PathPointType.Line, 
													(byte) PathPointType.Line, 
													(byte) (PathPointType.Line | PathPointType.CloseSubpath | PathPointType.PathMarker), 
													(byte) PathPointType.Start, 
													(byte) PathPointType.Line};

			for (int i=0; i < expectedTypes.Length; i++) {
				Assert.AreEqual (expectedTypes [i], actualTypes [i]);
			}	
        }
    }
}

                                                                                                                                                                                                                                                                                                                                                                              _ÃfD  Hƒì è7ÞÿÿHƒÄ ‹|$L‰æL‹D$Hˆòÿÿ8H‰ïèøÿÿˆD$(é9ÿÿÿ1À1Òë™èi¼ÿÿf„     AWAVAUI‰ýATUH‰ÕSH‰óHƒìHdH‹%(   H‰D$81ÀöâÍ …¬   HÇE     M…í„Ó   H…Û„Ê   €{*A¼   u6ö¯Í uUH‹L$8dH3%(   D‰à…u  HƒÄH[]A\A]A^A_Ã€    LcH‹=Î L‰æè-æÿÿH‰E H…Àt|E1äöZÍ t«E¶ÄHÍð  1À¿   Hßô  H58Í è¼ÿÿë„HƒìI‰ñI‰ø¿   RHó  H²ô  H5Í èÖ»ÿÿA\A^é!ÿÿÿD  A¼   é@ÿÿÿD  I‹} L‰æèœåÿÿH‰E H…À…kÿÿÿHÇD$    è¼ÿÿI‰Æö·Ì …   H‹0¶CöF„   öC!ß…
  1ÀëfˆT HƒÀHƒø„ž  ¶T‰ÁöÂßuâÆD  H¾D$ öDF…–  D‹5Î E…Ò„·   Hƒ|$ „d  ö9Ì uPH‹t$ëwLL$M‰à¿   1ÀH5Ì Héð  HŒó  è×ºÿÿI‹6¶CöFt
öC!ß„ZÿÿÿöéË t[H5àË E1À1À¿   HOï  HHó  è“ºÿÿH‹t$H…öt9H=RÌ èýøÿÿA‰Ä1ÀE„äuH‹D$H‰E éßýÿÿöŽË u¥H‹t$ëÇD  jE1ÉA¸   º   j ¹   Hƒì èHÚÿÿH‰D$0HƒÄ0Hƒøÿ„w  fAƒEHƒ}  …   D‹õÌ E…ÉŽ®   HD$E1öL=åÌ H‰D$ëfD  Hƒ}  u\IƒÆD95¾Ì ~~‹ÊÌ ÿt$E1ÀL‰ïC‹·H‰ÚPD‹­Ì H‹t$èsûÿÿ_AXA‰Ä„Àt¹Hƒì H‹L$ è«ÙÿÿH‹D$8HƒÄ éÿÿÿH‹t$E1äH…öt×H=9Ë èä÷ÿÿA‰Ä„À„‘   HÇD$    ëµH‹t$H…öuÔA‹uA‹M A‹E…ö…*  !ÁHD$D‹*Ì H‰ÚL‰ïP‹!Ì A¸   PH‹t$èÔúÿÿA‰ÄXZE„ä…]ÿÿÿH‹t$H…ö„OÿÿÿI} è_÷ÿÿA‰Ä„À„;ÿÿÿévÿÿÿ€    Hƒ}  …$ÿÿÿétÿÿÿ„     ¹   édýÿÿH‹D$A¼5   é(þÿÿHL$ Hƒì èÀØÿÿHƒÄ A‰Äƒøÿ„LýÿÿeH‹%`   H‹H0Hƒì A¸(   1Òè‚ÙÿÿH‰D$8HƒÄ H…ÀtOóoKD‹PË fïÀH‹t$FÇF    ÆF*D‰f$E…Û…ýÿÿöAÉ „‚ýÿÿéOýÿÿ÷Ð	ÁéÏþÿÿö&É …  jE1ÉA¸   º   j ¹   Hƒì èè×ÿÿH‰D$0HƒÄ0Hƒøÿ„ÿÿÿfAƒEA¼5   éþÿÿHL$ Hƒì èÎ×ÿÿHƒÄ I‰ÆH…À„)ýÿÿH‹PH…Ò„ÁüÿÿHƒ: „·üÿÿ1ÀëD‰àHHƒ<Ê I‰ÌuðeH‹%`   D…(   H‹J0Hƒì 1ÒMcÀèhØÿÿH‰D$8HƒÄ H…À„1ÿÿÿóoSfïÀH‹t$1ÀFD‰f ÆF*ëI‹VH‹Â‹‰T†$HƒÀA9ÄwééÙþÿÿèl¶ÿÿA¸5   Hë  1À¿   Hï  H5úÇ èÅ¶ÿÿéÐþÿÿAVAUATI‰ÔUH‰õSH‰ûHƒì0dH‹%(   H‰D$(1ÀHÇD$    ö»Ç …¥   H…Û„Ê   H…í„³  M…ä„š  H‰ßHT$H‰îèzùÿÿ‰Ã„ÀuH‹D$H…Àt‹@ …À…Ÿ   »   öcÇ u)H‹L$(dH3%(   ‰Ø…ã  HƒÄ0[]A\A]A^ÃfD  D¶ÃH­ê  1À¿   Hçî  H5Ç èãµÿÿë°I‰ñI‰øHñë  ¿   H¿î  H5ðÆ è»µÿÿH…Û…6ÿÿÿ»4   éwÿÿÿ„     jE1ÉA¸   º   j ¹   Hƒì è˜ÕÿÿHƒÄ0I‰ÅHƒøÿ„¿   ¶E2„À…[  ¶E3„À…  H‹D$ÇD$  ‹HT$L‰éHÇD$    A¸   Hƒì ‹@$‰D$4èKÕÿÿHƒÄ ƒøÿ„º  L‹t$HU"A€~*„2  IvL‰ïèÆìÿÿ„ÀtRA€~*„.  ‰ÃHƒì L‰éèùÔÿÿHƒÄ é–þÿÿ»   é•þÿÿfD  »9   é…þÿÿfD  »5   élþÿÿfD  eH‹%`   H‹H0Hƒì A¸8   º   è§ÕÿÿHƒÄ H‰ÅH…À„  L‰m(Hƒì H‰éè§ÕÿÿH‹E H„ê  HƒÄ H‰H(I‰,$é	þÿÿ iÀô  Hƒìºÿÿ  L‰éA¸  ‰D$jHƒì LL$4èdÔÿÿHƒÄ0é´þÿÿ iÀô  Hƒìºÿÿ  L‰éA¸  ‰D$jHƒì LL$4è,ÔÿÿHƒÄ0épþÿÿ H5±ì  L‰ïè‘ëÿÿ„À„ÿÿÿ‰ÃöàÄ „Åþÿÿ1ÿ1ÀH_ë  Hì  H5ÁÄ èŒ³ÿÿöµÄ „šþÿÿH`ë  Hiì  1ÿ1ÀH5–Ä èa³ÿÿéwþÿÿ»   émþÿÿHƒì L‰é»5   èaÓÿÿHƒÄ éþüÿÿè³²ÿÿ AUATI‰üUH‰õSHƒìdH‹%(   H‰D$1ÀHÇ$    ö1Ä …S  M…ä„x  H…í„  H‹]H…Û„„  fƒ}A½   w'H‹|$dH3<%(   D‰è…k  HƒÄ[]A\A]ÃD  Ç    H‰âH‰îL‰çè´õÿÿA‰Å„À…¦   H‹$A½   H…À„“   ·M‹x Hº†a†a†HƒéH‰þH‰ÈH÷âH)ÑHÑéHÊHÁêH9úHFúA‰øH…ÿtS·1Ò€    ·À1öH€HDˆ1ÉHØf‰HA‹$f‰p	‰ÖƒÂ‰HH‹$‹L±$‰H·ƒÀf‰A9ÐuÀH‹$‹p 9÷A’ÅöÃ „ÿÿÿE¶ÅH{æ  1À¿   Hê  H5æÂ è±±ÿÿéÝþÿÿ@ I‰ñI‰øHâç  ¿   H×é  H5¸Â èƒ±ÿÿM…ä…ˆþÿÿA½4   é þÿÿ€    A½9   éŽþÿÿD  A½   é~þÿÿèÈ°ÿÿ„     AWAVAUATI‰üUSH‰óINDX( 	 ð^Ã           (   8  è                             ¶Ü#    € j     µÜ#    ýÿ4làÖæEÿ4làÖæEÿ4làÖ·i„JÎÂ×       Õ	               d u m p _ s y m s _ r e g t e s t . c c       ^Ð%    € l     µÜ#    ÿ^dó|àÖ!­dó|àÖ‹fó|àÖ*ð…JÎÂ× `     ÃQ              d u m p _ s y m s _ r e g t e s t . s y m . i ›*&    € p     µÜ#    âúgá}àÖŠIhá}àÖ@ná}àÖGkJÎÂ× P      L              d u m p _ s y m s _ r e g t e s t 6 4 . e x e ¤*&    € p     µÜ#    @ná}àÖíŠná}àÖÃoá}àÖqc†JÎÂ×      ñ‘              d u m p _ s y m s _ r e g t e s t 6 4 . s y m §*&    € j     µÜ#    Ãoá}àÖÅ_pá}àÖý÷rá}àÖÞÈ‡JÎÂ×      2ˆ              o m a p _ r e o r d e r _ b b s . s y m . i n ¬*&    € n     µÜ#    ý÷rá}àÖ~vá}àÖ+²wá}àÖû-ˆJÎÂ× `     ÄQ              o m a p _ r e o r d e r _ f u n c s . s y m . ´*&    x f     µÜ#    \Ùwá}àÖv'xá}àÖ ®yá}àÖ´bˆJÎÂ× €     žu              o m a p _ s t r e t c h e d . s y m . ¹*&    ˆ t     µÜ#    Õyá}àÖ­üyá}àÖí÷{á}àÖ<‹ˆJÎÂ× `     ÁR           