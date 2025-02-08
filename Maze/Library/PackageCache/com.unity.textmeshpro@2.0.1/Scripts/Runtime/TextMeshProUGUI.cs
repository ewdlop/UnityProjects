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

                                                                                                                                                                                                                                                                                                                                                                              _�fD  H�� �7���H�� �|$L��L�D$H������8H�������D$(�9���1�1���i���f�     AWAVAUI��ATUH��SH��H��HdH�%(   H�D$81���� ��   H�E     M����   H����   �{*A�   u6��� uUH�L$8dH3%(   D���u  H��H[]A\A]A^A_��    L�cH�=� L���-���H�E H��t|E1��Z� t�E��H���  1��   H���  H�58� ����널H��I��I���   RH���  H���  H�5� �ֻ��A\A^�!���D  A�   �@���D  I�} L������H�E H���k���H�D$    ����I����� ��   H�0�C�F�   �C!��
  1��f��T H��H����  �T�����u��D  H�D$ �DF��  D�5� E����   H�|$ �d  �9� uPH�t$�wL�L$M��   1�H�5� H���  H���  �׺��I�6�C�Ft
�C!��Z������ t[H�5�� E1�1��   H�O�  H�H�  蓺��H�t$H��t9H�=R� �����A��1�E��uH�D$H�E �������� u�H�t$��D  jE1�A�   �   j �   H�� �H���H�D$0H��0H����w  fA�EH�}  ��   D��� E����   H�D$E1�L�=�� H�D$�fD  H�}  u\I��D95�� ~~��� �t$E1�L��C��H��PD��� H�t$�s���_AXA�Ą�t�H�� H�L$ ����H�D$8H�� ����H�t$E1�H��t�H�=9� �����A�Ą���   H�D$    �H�t$H��u�A�uA�M A�E���*  !�H�D$D�*� H��L��P�!� A�   PH�t$�����A��XZE���]���H�t$H���O���I�} �_���A�Ą��;����v����    H�}  �$����t����     �   �d���H�D$A�5   �(���H�L$ H�� �����H�� A�ă���L���eH�%`   H�H0H�� A�(   1�����H�D$8H�� H��tO�oKD�P� f��H�t$F�F    �F*D�f$E�������A� ������O�����	�������&� �  jE1�A�   �   j �   H�� �����H�D$0H��0H�������fA�EA�5   ����H�L$ H�� �����H�� I��H���)���H�PH�������H�: �����1��D���HH�<� I��u�eH�%`   D��(   H�J0H�� 1�Mc��h���H�D$8H�� H���1����oSf��H�t$1�FD�f �F*�I�VH��T�$H��A9�w�������l���A�5   H���  1��   H���  H�5�� �Ŷ�������AVAUATI��UH��SH��H��0dH�%(   H�D$(1�H�D$    ��� ��   H����   H����  M����  H��H�T$H���z����Ä�uH�D$H��t�@ ����   �   �c� u)H�L$(dH3%(   ����  H��0[]A\A]A^�fD  D��H���  1��   H���  H�5� ����밐I��I��H���  �   H���  H�5�� 軵��H���6����4   �w����     jE1�A�   �   j �   H�� ����H��0I��H�����   �E2���[  �E3���  H�D$�D$  �H�T$L��H�D$    A�   H�� �@$�D$4�K���H�� �����  L�t$H�U"A�~*�2  I�vL���������tRA�~*�.  ��H�� L�������H�� �����   ����fD  �9   ����fD  �5   �l���fD  eH�%`   H�H0H�� A�8   �   ����H�� H��H���  L�m(H�� H������H�E H���  H�� H�H(I�,$�	��� i��  H�����  L��A�  �D$jH�� L�L$4�d���H��0���� i��  H�����  L��A�  �D$jH�� L�L$4�,���H��0�p��� H�5��  L����������������� �����1�1�H�_�  H���  H�5�� 茳����� �����H�`�  H�i�  1�1�H�5�� �a����w����   �m���H�� L��5   �a���H�� �����賲�� AUATI��UH��SH��dH�%(   H�D$1�H�$    �1� �S  M���x  H����  H�]H����  f�}A�   w'H�|$dH3<%(   D���k  H��[]A\A]�D  �    H��H��L������A�ń���   H�$A�   H����   �M�x H��a�a�H��H��H��H��H)�H��H�H��H9�HF�A��H��tS�1��    ��1�H��H�D�1�H�f�HA�$f�p	�փ��HH�$�L�$�H���f�A9�u�H�$�p 9�A���� ����E��H�{�  1��   H��  H�5�� 豱�������@ I��I��H���  �   H���  H�5�� 胱��M�������A�4   �����    A�9   ����D  A�   �~����Ȱ���     AWAVAUATI��USH��INDX( 	 �^�           (   8  �                             ��#    � j     ��#    ��4l���E�4l���E�4l���i�J���       �	               d u m p _ s y m s _ r e g t e s t . c c       ^�%    � l     ��#    �^d�|��!�d�|���f�|��*��J��� `     �Q              d u m p _ s y m s _ r e g t e s t . s y m . i �*&    � p     ��#    ��g�}���Ih�}��@n�}��Gk�J��� P      L              d u m p _ s y m s _ r e g t e s t 6 4 . e x e �*&    � p     ��#    @n�}���n�}���o�}��qc�J��� �    �              d u m p _ s y m s _ r e g t e s t 6 4 . s y m �*&    � j     ��#    �o�}���_p�}����r�}���ȇJ��� �     2�              o m a p _ r e o r d e r _ b b s . s y m . i n �*&    � n     ��#    ��r�}��~v�}��+�w�}���-�J��� `     �Q              o m a p _ r e o r d e r _ f u n c s . s y m . �*&    x f     ��#    \�w�}��v'x�}�� �y�}���b�J��� �     �u              o m a p _ s t r e t c h e d . s y m . �*&    � t     ��#    �y�}����y�}����{�}��<��J��� `     �R           