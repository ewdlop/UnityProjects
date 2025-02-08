dx)
		{
			for (int yi = 0; yi < CellDimensions.Y; ++yi)
			{
				if (CancelF())
				{
					return;
				}
				// compute full Cell at x=0, then slide along x row, which saves half of value computes
				FVector3i Idx(0, yi, ZIdx);
				initialize_cell(Cell, Idx);
				polygonize_cell(Cell, vertTArray);
				for (int XIdx = 1; XIdx < CellDimensions.X; ++XIdx)
				{
					shift_cell_x(Cell, XIdx);
					polygonize_cell(Cell, vertTArray);
				}

			}
		}
	}




	/**
	*  fully sequential version, no threading
	*/
	void generate_continuation(TArrayView<const FVector3<double>> Seeds)
	{
		FGridCell Cell;
		int vertTArray[12];

		done_cells = FDenseGrid3i(CellDimensions.X, CellDimensions.Y, CellDimensions.Z, 0);

		TArray<FVector3i> stack;

		for (FVector3<double> seed : Seeds)
		{
			FVector3i seed_idx = cell_index(seed);
			if (done_cells[seed_idx] == 1)
			{
				continue;
			}
			stack.Add(seed_idx);
			done_cells[seed_idx] = 1;

			while ( stack.Num() > 0 )
			{
				FVector3i Idx = stack[stack.Num()-1]; 
				stack.RemoveAt(stack.Num()-1);
				if (CancelF())
				{
					return;
				}

				initialize_cell(Cell, Idx);
				if ( polygonize_cell(Cell, vertTArray) )
				{     // found crossing
					for ( FVector3i o : IndexUtil::GridOffsets6 )
					{
						FVector3i nbr_idx = Idx + o;
						if (GridBounds.Contains(nbr_idx) && done_cells[nbr_idx] == 0)
						{
							stack.Add(nbr_idx);
							done_cells[nbr_idx] = 1;
						}
					}
				}
			}
		}
	}




	/**
	*  parallel seed evaluation
	*/
	void generate_continuation_parallel(TArrayView<const FVector3<double>> Seeds)
	{
		parallel_mesh_access = true;

		ParallelFor(Seeds.Num(), [&](int32 Index)
		{
			FVector3<double> Seed = Seeds[Index];
			FVector3i seed_idx = cell_index(Seed);
			if (set_cell_if_not_done(seed_idx) == false)
			{
				return;
			}

			FGridCell Cell;
			int vertTArray[12];

			TArray<FVector3i> stack;
			stack.Add(seed_idx);

			while (stack.Num() > 0)
			{
				FVector3i Idx = stack[stack.Num() - 1];
				stack.RemoveAt(stack.Num() - 1);
				if (CancelF())
				{
					return;
				}

				initialize_cell(Cell, Idx);
				if (polygonize_cell(Cell, vertTArray))
				{     // found crossing
					for (FVector3i o : IndexUtil::GridOffsets6)
					{
						FVector3i nbr_idx = Idx + o;
						if (GridBounds.Contains(nbr_idx))
						{
							if (set_cell_if_not_done(nbr_idx) == true)
							{ 
								stack.Add(nbr_idx);
							}
						}
					}
				}
			}
		});

		parallel_mesh_access = false;
	}



	FDenseGrid3i done_cells;
	FCriticalSection done_cells_lock;

	bool set_cell_if_not_done(const FVector3i& Idx)
	{
		bool was_set = false;
		FScopeLock Lock(&done_cells_lock);
		if (done_cells[Idx] == 0)
		{
			done_cells[Idx] = 1;
			was_set = true;
		}
		return was_set;
	}










	/**
	*  find edge crossings and generate triangles for this cell
	*/
	bool polygonize_cell(FGridCell& Cell, int VertIndexArray[8])
	{
		// construct bits of index into edge table, where bit for each
		// corner is 1 if that value is < isovalue.
		// This tell us which edges have sign-crossings, and the int value
		// of the bitmap is an index into the edge and triangle tables
		int cubeindex = 0, Shift = 1;
		for (int i = 0; i < 8; ++i)
		{
			if (Cell.f[i] < IsoValue)
			{
				cubeindex |= Shift;
			}
			Shift <<= 1;
		}

		// no crossings!
		if (EdgeTable[cubeindex] == 0)
		{
			return false;
		}

		// check each bit of value in edge table. If it is 1, we
		// have a crossing on that edge. Look up the indices of this
		// edge and find the intersection point along it
		Shift = 1;
		FVector3<double> pa = FVector3<double>::Zero(), pb = FVector3<double>::Zero();
		for (int i = 0; i <= 11; i++)
		{
			if ((EdgeTable[cubeindex] & Shift) != 0)
			{
				int a = EdgeIndices[i][0], b = EdgeIndices[i][1];
				VertIndexArray[i] = edge_vertex_id(Cell.i[a], Cell.i[b], Cell.f[a], Cell.f[b]);
			}
			Shift <<= 1;
		}

		// now iterate through the set of triangles in TriTable for this cube,
		// and emit triangles using the vertices we found.
		int tri_count = 0;
		for (int i = 0; TriTable[cubeindex][i] != -1; i += 3)
		{
			int ta = TriTable[cubeindex][i];
			int tb = TriTable[cubeindex][i + 1];
			int tc = TriTable[cubeindex][i + 2];
			int a = VertIndexArray[ta], b = VertIndexArray[tb], c = VertIndexArray[tc];

			// if a corner is within tolerance of isovalue, then some triangles
			// will be degenerate, and we can skip them w/o resulting in cracks (right?)
			// !! this should never happen anymore...artifact of old hashtable impl
			if (!ensure(a != b && a != c && b != c))
			{
				continue;
			}

			append_triangle(a, b, c);
			tri_count++;
		}

		return (tri_count > 0);
	}




	/**
	*  add vertex to mesh, with locking if we are computing in parallel
	*/
	int append_vertex(FVector3<double> V)
	{
		if (parallel_mesh_access)
		{
			FScopeLock Lock(&mesh_lock);
			return AppendVertex((FVector3d)V);
		}
		else
		{
			return AppendVertex((FVector3d)V);
		}
	}



	/**
	*  add triangle to mesh, with locking if we are computing in parallel
	*/
	int append_triangle(int A, int B, int C)
	{
		if (parallel_mesh_access)
		{
			FScopeLock Lock(&mesh_lock);
			return AppendTriangle(A, B, C);
		}
		else
		{
			return AppendTriangle(A, B, C);
		}
	}



	/**
	*  root-find the intersection along edge from f(P1)=ValP1 to f(P2)=ValP2
	*/
	void find_iso(const FVector3<double>& P1, const FVector3<double>& P2, double ValP1, double ValP2, FVector3<double>& PIso)
	{
		// Ok, this is a bit hacky but seems to work? If both isovalues
		// are the same, we just return the midpoint. If one is nearly zero, we can
		// but assume that's where the surface is. *However* if we return that point exactly,
		// we can get nonmanifold vertices, because multiple fans may connect there. 
		// Since FDynamicMesh3 disallows that, it results in holes. So we pull 
		// slightly towards the other point along this edge. This means we will get
		// repeated nearly-coincident vertices, but the mesh will be manifold.
		const double dt = 0.999999;
		if (FMath::Abs(ValP1 - ValP2) < 0.00001)
		{
			PIso = (P1 + P2) * 0.5;
			return;
		}
		if (FMath::Abs(IsoValue - ValP1) < 0.00001)
		{
			PIso = dt * P1 + (1.0 - dt) * P2;
			return;
		}
		if (FMath::Abs(IsoValue - ValP2) < 0.00001)
		{
			PIso = (dt) * P2 + (1.0 - dt) * P1;
			return;
		}

		// Note: if we don't maintain min/max order here, then numerical error means
		//   that hashing on point x/y/z doesn't work
		FVector3<double> a = P1, b = P2;
		double fa = ValP1, fb = ValP2;
		if (ValP2 < ValP1)
		{
			a = P2; b = P1;
			fb = ValP1; fa = ValP2;
		}

		// converge on root
		if (RootMode == ERootfindingModes::Bisection)
		{
			for (int k = 0; k < RootModeSteps; ++k)
			{
				PIso.X = (a.X + b.X) * 0.5; PIso.Y = (a.Y + b.Y) * 0.5; PIso.Z = (a.Z + b.Z) * 0.5;
				double mid_f = Implicit(PIso);
				if (mid_f < IsoValue)
				{
					a = PIso; fa = mid_f;
				}
				else
				{
					b = PIso; fb = mid_f;
				}
			}
			PIso = FVector3<double>::Lerp(a, b, 0.5);

		}
		else
		{
			double mu = 0;
			if (RootMode == ERootfindingModes::LerpSteps)
			{
				for (int k = 0; k < RootModeSteps; ++k)
				{
					mu = FMathd::Clamp((IsoValue - fa) / (fb - fa), 0.0, 1.0);
					PIso.X = a.X + mu * (b.X - a.X);
					PIso.Y = a.Y + mu * (b.Y - a.Y);
					PIso.Z = a.Z + mu * (b.Z - a.Z);
					double mid_f = Implicit(PIso);
					if (mid_f < IsoValue)
					{
						a = PIso; fa = mid_f;
					}
					else
					{
						b = PIso; fb = mid_f;
					}
				}
			}

			// final lerp
			mu = FMathd::Clamp((IsoValue - fa) / (fb - fa), 0.0, 1.0);
			PIso.X = a.X + mu * (b.X - a.X);
			PIso.Y = a.Y + mu * (b.Y - a.Y);
			PIso.Z = a.Z + mu * (b.Z - a.Z);
		}
	}




	/*
	* Below here are standard marching-cubes tables. 
	*/


	constexpr static int EdgeIndices[12][2] = {
		{0,1}, {1,2}, {2,3}, {3,0}, {4,5}, {5,6}, {6,7}, {7,4}, {0,4}, {1,5}, {2,6}, {3,7}
	};

	constexpr static int EdgeTable[256] = {
		0x0  , 0x109, 0x203, 0x30a, 0x406, 0x50f, 0x605, 0x70c,
		0x80c, 0x905, 0xa0f, 0xb06, 0xc0a, 0xd03, 0xe09, 0xf00,
		0x190, 0x99 , 0x393, 0x29a, 0x596, 0x49f, 0x795, 0x69c,
		0x99c, 0x895, 0xb9f, 0xa96, 0xd9a, 0xc93, 0xf99, 0xe90,
		0x230, 0x339, 0x33 , 0x13a, 0x636, 0x73f, 0x435, 0x53c,
		0xa3c, 0xb35, 0x83f, 0x936, 0xe3a, 0xf33, 0xc39, 0xd30,
		0x3a0, 0x2a9, 0x1a3, 0xaa , 0x7a6, 0x6af, 0x5a5, 0x4ac,
		0xbac, 0xaa5, 0x9af, 0x8a6, 0xfaa, 0xea3, 0xda9, 0xca0,
		0x460, 0x569, 0x663, 0x76a, 0x66 , 0x16f, 0x265, 0x36c,
		0xc6c, 0xd65, 0xe6f, 0xf66, 0x86a, 0x963, 0xa69, 0xb60,
		0x5f0, 0x4f9, 0x7f3, 0x6fa, 0x1f6, 0xff , 0x3f5, 0x2fc,
		0xdfc, 0xcf5, 0xfff, 0xef6, 0x9fa, 0x8f3, 0xbf9, 0xaf0,
		0x650, 0x759, 0x453, 0x55a, 0x256, 0x35f, 0x55 , 0x15c,
		0xe5c, 0xf55, 0xc5f, 0xd56, 0xa5a, 0xb53, 0x859, 0x950,
		0x7c0, 0x6c9, 0x5c3, 0x4ca, 0x3c6, 0x2cf, 0x1c5, 0xcc ,
		0xfcc, 0xec5, 0xdcf, 0xcc6, 0xbca, 0xac3, 0x9c9, 0x8c0,
		0x8c0, 0x9c9, 0xac3, 0xbca, 0xcc6, 0xdcf, 0xec5, 0xfcc,
		0xcc , 0x1c5, 0x2cf, 0x3c6, 0x4ca, 0x5c3, 0x6c9, 0x7c0,
		0x950, 0x859, 0xb53, 0xa5a, 0xd56, 0xc5f, 0xf55, 0xe5c,
		0x15c, 0x55 , 0x35f, 0x256, 0x55a, 0x453, 0x759, 0x650,
		0xaf0, 0xbf9, 0x8f3, 0x9fa, 0xef6, 0xfff, 0xcf5, 0xdfc,
		0x2fc, 0x3f5, 0xff , 0x1f6, 0x6fa, 0x7f3, 0x4f9, 0x5f0,
		0xb60, 0xa69, 0x963, 0x86a, 0xf66, 0xe6f, 0xd65, 0xc6c,
		0x36c, 0x265, 0x16f, 0x66 , 0x76a, 0x663, 0x569, 0x460,
		0xca0, 0xda9, 0xea3, 0xfaa, 0x8a6, 0x9af, 0xaa5, 0xbac,
		0x4ac, 0x5a5, 0x6af, 0x7a6, 0xaa , 0x1a3, 0x2a9, 0x3a0,
		0xd30, 0xc39, 0xf33, 0xe3a, 0x936, 0x83f, 0xb35, 0xa3c,
		0x53c, 0x435, 0x73f, 0x636, 0x13a, 0x33 , 0x339, 0x230,
		0xe90, 0xf99, 0xc93, 0xd9a, 0xa96, 0xb9f, 0x895, 0x99c,
		0x69c, 0x795, 0x49f, 0x596, 0x29a, 0x393, 0x99 , 0x190,
		0xf00, 0xe09, 0xd03, 0xc0a, 0xb06, 0xa0f, 0x905, 0x80c,
		0x70c, 0x605, 0x50f, 0x406, 0x30a, 0x203, 0x109, 0x0   };


	constexpr static int TriTable[256][16] =
	{
		{-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 1, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 8, 3, 9, 8, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 3, 1, 2, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{9, 2, 10, 0, 2, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{2, 8, 3, 2, 10, 8, 10, 9, 8, -1, -1, -1, -1, -1, -1, -1},
		{3, 11, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 11, 2, 8, 11, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 9, 0, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 11, 2, 1, 9, 11, 9, 8, 11, -1, -1, -1, -1, -1, -1, -1},
		{3, 10, 1, 11, 10, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 10, 1, 0, 8, 10, 8, 11, 10, -1, -1, -1, -1, -1, -1, -1},
		{3, 9, 0, 3, 11, 9, 11, 10, 9, -1, -1, -1, -1, -1, -1, -1},
		{9, 8, 10, 10, 8, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 7, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 3, 0, 7, 3, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 1, 9, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 1, 9, 4, 7, 1, 7, 3, 1, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 10, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{3, 4, 7, 3, 0, 4, 1, 2, 10, -1, -1, -1, -1, -1, -1, -1},
		{9, 2, 10, 9, 0, 2, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1},
		{2, 10, 9, 2, 9, 7, 2, 7, 3, 7, 9, 4, -1, -1, -1, -1},
		{8, 4, 7, 3, 11, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{11, 4, 7, 11, 2, 4, 2, 0, 4, -1, -1, -1, -1, -1, -1, -1},
		{9, 0, 1, 8, 4, 7, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1},
		{4, 7, 11, 9, 4, 11, 9, 11, 2, 9, 2, 1, -1, -1, -1, -1},
		{3, 10, 1, 3, 11, 10, 7, 8, 4, -1, -1, -1, -1, -1, -1, -1},
		{1, 11, 10, 1, 4, 11, 1, 0, 4, 7, 11, 4, -1, -1, -1, -1},
		{4, 7, 8, 9, 0, 11, 9, 11, 10, 11, 0, 3, -1, -1, -1, -1},
		{4, 7, 11, 4, 11, 9, 9, 11, 10, -1, -1, -1, -1, -1, -1, -1},
		{9, 5, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{9, 5, 4, 0, 8, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 5, 4, 1, 5, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{8, 5, 4, 8, 3, 5, 3, 1, 5, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 10, 9, 5, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{3, 0, 8, 1, 2, 10, 4, 9, 5, -1, -1, -1, -1, -1, -1, -1},
		{5, 2, 10, 5, 4, 2, 4, 0, 2, -1, -1, -1, -1, -1, -1, -1},
		{2, 10, 5, 3, 2, 5, 3, 5, 4, 3, 4, 8, -1, -1, -1, -1},
		{9, 5, 4, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 11, 2, 0, 8, 11, 4, 9, 5, -1, -1, -1, -1, -1, -1, -1},
		{0, 5, 4, 0, 1, 5, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1},
		{2, 1, 5, 2, 5, 8, 2, 8, 11, 4, 8, 5, -1, -1, -1, -1},
		{10, 3, 11, 10, 1, 3, 9, 5, 4, -1, -1, -1, -1, -1, -1, -1},
		{4, 9, 5, 0, 8, 1, 8, 10, 1, 8, 11, 10, -1, -1, -1, -1},
		{5, 4, 0, 5, 0, 11, 5, 11, 10, 11, 0, 3, -1, -1, -1, -1},
		{5, 4, 8, 5, 8, 10, 10, 8, 11, -1, -1, -1, -1, -1, -1, -1},
		{9, 7, 8, 5, 7, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{9, 3, 0, 9, 5, 3, 5, 7, 3, -1, -1, -1, -1, -1, -1, -1},
		{0, 7, 8, 0, 1, 7, 1, 5, 7, -1, -1, -1, -1, -1, -1, -1},
		{1, 5, 3, 3, 5, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{9, 7, 8, 9, 5, 7, 10, 1, 2, -1, -1, -1, -1, -1, -1, -1},
		{10, 1, 2, 9, 5, 0, 5, 3, 0, 5, 7, 3, -1, -1, -1, -1},
		{8, 0, 2, 8, 2, 5, 8, 5, 7, 10, 5, 2, -1, -1, -1, -1},
		{2, 10, 5, 2, 5, 3, 3, 5, 7, -1, -1, -1, -1, -1, -1, -1},
		{7, 9, 5, 7, 8, 9, 3, 11, 2, -1, -1, -1, -1, -1, -1, -1},
		{9, 5, 7, 9, 7, 2, 9, 2, 0, 2, 7, 11, -1, -1, -1, -1},
		{2, 3, 11, 0, 1, 8, 1, 7, 8, 1, 5, 7, -1, -1, -1, -1},
		{11, 2, 1, 11, 1, 7, 7, 1, 5, -1, -1, -1, -1, -1, -1, -1},
		{9, 5, 8, 8, 5, 7, 10, 1, 3, 10, 3, 11, -1, -1, -1, -1},
		{5, 7, 0, 5, 0, 9, 7, 11, 0, 1, 0, 10, 11, 10, 0, -1},
		{11, 10, 0, 11, 0, 3, 10, 5, 0, 8, 0, 7, 5, 7, 0, -1},
		{11, 10, 5, 7, 11, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{10, 6, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 3, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{9, 0, 1, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 8, 3, 1, 9, 8, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1},
		{1, 6, 5, 2, 6, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 6, 5, 1, 2, 6, 3, 0, 8, -1, -1, -1, -1, -1, -1, -1},
		{9, 6, 5, 9, 0, 6, 0, 2, 6, -1, -1, -1, -1, -1, -1, -1},
		{5, 9, 8, 5, 8, 2, 5, 2, 6, 3, 2, 8, -1, -1, -1, -1},
		{2, 3, 11, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{11, 0, 8, 11, 2, 0, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1},
		{0, 1, 9, 2, 3, 11, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1},
		{5, 10, 6, 1, 9, 2, 9, 11, 2, 9, 8, 11, -1, -1, -1, -1},
		{6, 3, 11, 6, 5, 3, 5, 1, 3, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 11, 0, 11, 5, 0, 5, 1, 5, 11, 6, -1, -1, -1, -1},
		{3, 11, 6, 0, 3, 6, 0, 6, 5, 0, 5, 9, -1, -1, -1, -1},
		{6, 5, 9, 6, 9, 11, 11, 9, 8, -1, -1, -1, -1, -1, -1, -1},
		{5, 10, 6, 4, 7, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 3, 0, 4, 7, 3, 6, 5, 10, -1, -1, -1, -1, -1, -1, -1},
		{1, 9, 0, 5, 10, 6, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1},
		{10, 6, 5, 1, 9, 7, 1, 7, 3, 7, 9, 4, -1, -1, -1, -1},
		{6, 1, 2, 6, 5, 1, 4, 7, 8, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 5, 5, 2, 6, 3, 0, 4, 3, 4, 7, -1, -1, -1, -1},
		{8, 4, 7, 9, 0, 5, 0, 6, 5, 0, 2, 6, -1, -1, -1, -1},
		{7, 3, 9, 7, 9, 4, 3, 2, 9, 5, 9, 6, 2, 6, 9, -1},
		{3, 11, 2, 7, 8, 4, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1},
		{5, 10, 6, 4, 7, 2, 4, 2, 0, 2, 7, 11, -1, -1, -1, -1},
		{0, 1, 9, 4, 7, 8, 2, 3, 11, 5, 10, 6, -1, -1, -1, -1},
		{9, 2, 1, 9, 11, 2, 9, 4, 11, 7, 11, 4, 5, 10, 6, -1},
		{8, 4, 7, 3, 11, 5, 3, 5, 1, 5, 11, 6, -1, -1, -1, -1},
		{5, 1, 11, 5, 11, 6, 1, 0, 11, 7, 11, 4, 0, 4, 11, -1},
		{0, 5, 9, 0, 6, 5, 0, 3, 6, 11, 6, 3, 8, 4, 7, -1},
		{6, 5, 9, 6, 9, 11, 4, 7, 9, 7, 11, 9, -1, -1, -1, -1},
		{10, 4, 9, 6, 4, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 10, 6, 4, 9, 10, 0, 8, 3, -1, -1, -1, -1, -1, -1, -1},
		{10, 0, 1, 10, 6, 0, 6, 4, 0, -1, -1, -1, -1, -1, -1, -1},
		{8, 3, 1, 8, 1, 6, 8, 6, 4, 6, 1, 10, -1, -1, -1, -1},
		{1, 4, 9, 1, 2, 4, 2, 6, 4, -1, -1, -1, -1, -1, -1, -1},
		{3, 0, 8, 1, 2, 9, 2, 4, 9, 2, 6, 4, -1, -1, -1, -1},
		{0, 2, 4, 4, 2, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{8, 3, 2, 8, 2, 4, 4, 2, 6, -1, -1, -1, -1, -1, -1, -1},
		{10, 4, 9, 10, 6, 4, 11, 2, 3, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 2, 2, 8, 11, 4, 9, 10, 4, 10, 6, -1, -1, -1, -1},
		{3, 11, 2, 0, 1, 6, 0, 6, 4, 6, 1, 10, -1, -1, -1, -1},
		{6, 4, 1, 6, 1, 10, 4, 8, 1, 2, 1, 11, 8, 11, 1, -1},
		{9, 6, 4, 9, 3, 6, 9, 1, 3, 11, 6, 3, -1, -1, -1, -1},
		{8, 11, 1, 8, 1, 0, 11, 6, 1, 9, 1, 4, 6, 4, 1, -1},
		{3, 11, 6, 3, 6, 0, 0, 6, 4, -1, -1, -1, -1, -1, -1, -1},
		{6, 4, 8, 11, 6, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{7, 10, 6, 7, 8, 10, 8, 9, 10, -1, -1, -1, -1, -1, -1, -1},
		{0, 7, 3, 0, 10, 7, 0, 9, 10, 6, 7, 10, -1, -1, -1, -1},
		{10, 6, 7, 1, 10, 7, 1, 7, 8, 1, 8, 0, -1, -1, -1, -1},
		{10, 6, 7, 10, 7, 1, 1, 7, 3, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 6, 1, 6, 8, 1, 8, 9, 8, 6, 7, -1, -1, -1, -1},
		{2, 6, 9, 2, 9, 1, 6, 7, 9, 0, 9, 3, 7, 3, 9, -1},
		{7, 8, 0, 7, 0, 6, 6, 0, 2, -1, -1, -1, -1, -1, -1, -1},
		{7, 3, 2, 6, 7, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{2, 3, 11, 10, 6, 8, 10, 8, 9, 8, 6, 7, -1, -1, -1, -1},
		{2, 0, 7, 2, 7, 11, 0, 9, 7, 6, 7, 10, 9, 10, 7, -1},
		{1, 8, 0, 1, 7, 8, 1, 10, 7, 6, 7, 10, 2, 3, 11, -1},
		{11, 2, 1, 11, 1, 7, 10, 6, 1, 6, 7, 1, -1, -1, -1, -1},
		{8, 9, 6, 8, 6, 7, 9, 1, 6, 11, 6, 3, 1, 3, 6, -1},
		{0, 9, 1, 11, 6, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{7, 8, 0, 7, 0, 6, 3, 11, 0, 11, 6, 0, -1, -1, -1, -1},
		{7, 11, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{7, 6, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{3, 0, 8, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 1, 9, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{8, 1, 9, 8, 3, 1, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1},
		{10, 1, 2, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 10, 3, 0, 8, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1},
		{2, 9, 0, 2, 10, 9, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1},
		{6, 11, 7, 2, 10, 3, 10, 8, 3, 10, 9, 8, -1, -1, -1, -1},
		{7, 2, 3, 6, 2, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{7, 0, 8, 7, 6, 0, 6, 2, 0, -1, -1, -1, -1, -1, -1, -1},
		{2, 7, 6, 2, 3, 7, 0, 1, 9, -1, -1, -1, -1, -1, -1, -1},
		{1, 6, 2, 1, 8, 6, 1, 9, 8, 8, 7, 6, -1, -1, -1, -1},
		{10, 7, 6, 10, 1, 7, 1, 3, 7, -1, -1, -1, -1, -1, -1, -1},
		{10, 7, 6, 1, 7, 10, 1, 8, 7, 1, 0, 8, -1, -1, -1, -1},
		{0, 3, 7, 0, 7, 10, 0, 10, 9, 6, 10, 7, -1, -1, -1, -1},
		{7, 6, 10, 7, 10, 8, 8, 10, 9, -1, -1, -1, -1, -1, -1, -1},
		{6, 8, 4, 11, 8, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{3, 6, 11, 3, 0, 6, 0, 4, 6, -1, -1, -1, -1, -1, -1, -1},
		{8, 6, 11, 8, 4, 6, 9, 0, 1, -1, -1, -1, -1, -1, -1, -1},
		{9, 4, 6, 9, 6, 3, 9, 3, 1, 11, 3, 6, -1, -1, -1, -1},
		{6, 8, 4, 6, 11, 8, 2, 10, 1, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 10, 3, 0, 11, 0, 6, 11, 0, 4, 6, -1, -1, -1, -1},
		{4, 11, 8, 4, 6, 11, 0, 2, 9, 2, 10, 9, -1, -1, -1, -1},
		{10, 9, 3, 10, 3, 2, 9, 4, 3, 11, 3, 6, 4, 6, 3, -1},
		{8, 2, 3, 8, 4, 2, 4, 6, 2, -1, -1, -1, -1, -1, -1, -1},
		{0, 4, 2, 4, 6, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 9, 0, 2, 3, 4, 2, 4, 6, 4, 3, 8, -1, -1, -1, -1},
		{1, 9, 4, 1, 4, 2, 2, 4, 6, -1, -1, -1, -1, -1, -1, -1},
		{8, 1, 3, 8, 6, 1, 8, 4, 6, 6, 10, 1, -1, -1, -1, -1},
		{10, 1, 0, 10, 0, 6, 6, 0, 4, -1, -1, -1, -1, -1, -1, -1},
		{4, 6, 3, 4, 3, 8, 6, 10, 3, 0, 3, 9, 10, 9, 3, -1},
		{10, 9, 4, 6, 10, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 9, 5, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 3, 4, 9, 5, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1},
		{5, 0, 1, 5, 4, 0, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1},
		{11, 7, 6, 8, 3, 4, 3, 5, 4, 3, 1, 5, -1, -1, -1, -1},
		{9, 5, 4, 10, 1, 2, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1},
		{6, 11, 7, 1, 2, 10, 0, 8, 3, 4, 9, 5, -1, -1, -1, -1},
		{7, 6, 11, 5, 4, 10, 4, 2, 10, 4, 0, 2, -1, -1, -1, -1},
		{3, 4, 8, 3, 5, 4, 3, 2, 5, 10, 5, 2, 11, 7, 6, -1},
		{7, 2, 3, 7, 6, 2, 5, 4, 9, -1, -1, -1, -1, -1, -1, -1},
		{9, 5, 4, 0, 8, 6, 0, 6, 2, 6, 8, 7, -1, -1, -1, -1},
		{3, 6, 2, 3, 7, 6, 1, 5, 0, 5, 4, 0, -1, -1, -1, -1},
		{6, 2, 8, 6, 8, 7, 2, 1, 8, 4, 8, 5, 1, 5, 8, -1},
		{9, 5, 4, 10, 1, 6, 1, 7, 6, 1, 3, 7, -1, -1, -1, -1},
		{1, 6, 10, 1, 7, 6, 1, 0, 7, 8, 7, 0, 9, 5, 4, -1},
		{4, 0, 10, 4, 10, 5, 0, 3, 10, 6, 10, 7, 3, 7, 10, -1},
		{7, 6, 10, 7, 10, 8, 5, 4, 10, 4, 8, 10, -1, -1, -1, -1},
		{6, 9, 5, 6, 11, 9, 11, 8, 9, -1, -1, -1, -1, -1, -1, -1},
		{3, 6, 11, 0, 6, 3, 0, 5, 6, 0, 9, 5, -1, -1, -1, -1},
		{0, 11, 8, 0, 5, 11, 0, 1, 5, 5, 6, 11, -1, -1, -1, -1},
		{6, 11, 3, 6, 3, 5, 5, 3, 1, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 10, 9, 5, 11, 9, 11, 8, 11, 5, 6, -1, -1, -1, -1},
		{0, 11, 3, 0, 6, 11, 0, 9, 6, 5, 6, 9, 1, 2, 10, -1},
		{11, 8, 5, 11, 5, 6, 8, 0, 5, 10, 5, 2, 0, 2, 5, -1},
		{6, 11, 3, 6, 3, 5, 2, 10, 3, 10, 5, 3, -1, -1, -1, -1},
		{5, 8, 9, 5, 2, 8, 5, 6, 2, 3, 8, 2, -1, -1, -1, -1},
		{9, 5, 6, 9, 6, 0, 0, 6, 2, -1, -1, -1, -1, -1, -1, -1},
		{1, 5, 8, 1, 8, 0, 5, 6, 8, 3, 8, 2, 6, 2, 8, -1},
		{1, 5, 6, 2, 1, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 3, 6, 1, 6, 10, 3, 8, 6, 5, 6, 9, 8, 9, 6, -1},
		{10, 1, 0, 10, 0, 6, 9, 5, 0, 5, 6, 0, -1, -1, -1, -1},
		{0, 3, 8, 5, 6, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{10, 5, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{11, 5, 10, 7, 5, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{11, 5, 10, 11, 7, 5, 8, 3, 0, -1, -1, -1, -1, -1, -1, -1},
		{5, 11, 7, 5, 10, 11, 1, 9, 0, -1, -1, -1, -1, -1, -1, -1},
		{10, 7, 5, 10, 11, 7, 9, 8, 1, 8, 3, 1, -1, -1, -1, -1},
		{11, 1, 2, 11, 7, 1, 7, 5, 1, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 3, 1, 2, 7, 1, 7, 5, 7, 2, 11, -1, -1, -1, -1},
		{9, 7, 5, 9, 2, 7, 9, 0, 2, 2, 11, 7, -1, -1, -1, -1},
		{7, 5, 2, 7, 2, 11, 5, 9, 2, 3, 2, 8, 9, 8, 2, -1},
		{2, 5, 10, 2, 3, 5, 3, 7, 5, -1, -1, -1, -1, -1, -1, -1},
		{8, 2, 0, 8, 5, 2, 8, 7, 5, 10, 2, 5, -1, -1, -1, -1},
		{9, 0, 1, 5, 10, 3, 5, 3, 7, 3, 10, 2, -1, -1, -1, -1},
		{9, 8, 2, 9, 2, 1, 8, 7, 2, 10, 2, 5, 7, 5, 2, -1},
		{1, 3, 5, 3, 7, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 7, 0, 7, 1, 1, 7, 5, -1, -1, -1, -1, -1, -1, -1},
		{9, 0, 3, 9, 3, 5, 5, 3, 7, -1, -1, -1, -1, -1, -1, -1},
		{9, 8, 7, 5, 9, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{5, 8, 4, 5, 10, 8, 10, 11, 8, -1, -1, -1, -1, -1, -1, -1},
		{5, 0, 4, 5, 11, 0, 5, 10, 11, 11, 3, 0, -1, -1, -1, -1},
		{0, 1, 9, 8, 4, 10, 8, 10, 11, 10, 4, 5, -1, -1, -1, -1},
		{10, 11, 4, 10, 4, 5, 11, 3, 4, 9, 4, 1, 3, 1, 4, -1},
		{2, 5, 1, 2, 8, 5, 2, 11, 8, 4, 5, 8, -1, -1, -1, -1},
		{0, 4, 11, 0, 11, 3, 4, 5, 11, 2, 11, 1, 5, 1, 11, -1},
		{0, 2, 5, 0, 5, 9, 2, 11, 5, 4, 5, 8, 11, 8, 5, -1},
		{9, 4, 5, 2, 11, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{2, 5, 10, 3, 5, 2, 3, 4, 5, 3, 8, 4, -1, -1, -1, -1},
		{5, 10, 2, 5, 2, 4, 4, 2, 0, -1, -1, -1, -1, -1, -1, -1},
		{3, 10, 2, 3, 5, 10, 3, 8, 5, 4, 5, 8, 0, 1, 9, -1},
		{5, 10, 2, 5, 2, 4, 1, 9, 2, 9, 4, 2, -1, -1, -1, -1},
		{8, 4, 5, 8, 5, 3, 3, 5, 1, -1, -1, -1, -1, -1, -1, -1},
		{0, 4, 5, 1, 0, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{8, 4, 5, 8, 5, 3, 9, 0, 5, 0, 3, 5, -1, -1, -1, -1},
		{9, 4, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 11, 7, 4, 9, 11, 9, 10, 11, -1, -1, -1, -1, -1, -1, -1},
		{0, 8, 3, 4, 9, 7, 9, 11, 7, 9, 10, 11, -1, -1, -1, -1},
		{1, 10, 11, 1, 11, 4, 1, 4, 0, 7, 4, 11, -1, -1, -1, -1},
		{3, 1, 4, 3, 4, 8, 1, 10, 4, 7, 4, 11, 10, 11, 4, -1},
		{4, 11, 7, 9, 11, 4, 9, 2, 11, 9, 1, 2, -1, -1, -1, -1},
		{9, 7, 4, 9, 11, 7, 9, 1, 11, 2, 11, 1, 0, 8, 3, -1},
		{11, 7, 4, 11, 4, 2, 2, 4, 0, -1, -1, -1, -1, -1, -1, -1},
		{11, 7, 4, 11, 4, 2, 8, 3, 4, 3, 2, 4, -1, -1, -1, -1},
		{2, 9, 10, 2, 7, 9, 2, 3, 7, 7, 4, 9, -1, -1, -1, -1},
		{9, 10, 7, 9, 7, 4, 10, 2, 7, 8, 7, 0, 2, 0, 7, -1},
		{3, 7, 10, 3, 10, 2, 7, 4, 10, 1, 10, 0, 4, 0, 10, -1},
		{1, 10, 2, 8, 7, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 9, 1, 4, 1, 7, 7, 1, 3, -1, -1, -1, -1, -1, -1, -1},
		{4, 9, 1, 4, 1, 7, 0, 8, 1, 8, 7, 1, -1, -1, -1, -1},
		{4, 0, 3, 7, 4, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{4, 8, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{9, 10, 8, 10, 11, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{3, 0, 9, 3, 9, 11, 11, 9, 10, -1, -1, -1, -1, -1, -1, -1},
		{0, 1, 10, 0, 10, 8, 8, 10, 11, -1, -1, -1, -1, -1, -1, -1},
		{3, 1, 10, 11, 3, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 2, 11, 1, 11, 9, 9, 11, 8, -1, -1, -1, -1, -1, -1, -1},
		{3, 0, 9, 3, 9, 11, 1, 2, 9, 2, 11, 9, -1, -1, -1, -1},
		{0, 2, 11, 8, 0, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{3, 2, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{2, 3, 8, 2, 8, 10, 10, 8, 9, -1, -1, -1, -1, -1, -1, -1},
		{9, 10, 2, 0, 9, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{2, 3, 8, 2, 8, 10, 0, 1, 8, 1, 10, 8, -1, -1, -1, -1},
		{1, 10, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{1, 3, 8, 9, 1, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 9, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{0, 3, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
		{-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}
	};

};


                                                                                                                                                                                                                                                                                              ± ®øt∫ÁùÛf<y2hÖTPôëd›˙˙ﬂˆ⁄tÇ‰iTı4ÈŸ§œÂ]á˘£éªB`Çúj˜öhúñíº⁄`‡râ
(–{Á:ÉaıÍ´4€”ú,ÒH∫ﬂ€A§¥”Áü%1:â⁄Â∂I¶BÏÏ>ÿX_û,ã≈nuÿa^Ë≠Yï©¶”˝Üòq·ô¿oçëãË„XrwÔ,∞·@ƒßö˘¶PunQØÚ¯¢\R∂¬#Ç_/∫¯Æ){'‘Û*óm¸õ—ä∫êîìﬁèÏx$$∞·áÈN®Ä,O%¡(5÷Åï«J∆æÁ·ΩE#J@oä êäˇÚà∫n˚x’I˝®¨fˇëÿ’1¶A˝©´R3ñœõ2ü™ﬂá™ó≤Z¿˜ÛîÇˇ›í ãëÛBÕà≥L„l8QŒ˛yªnFÍÖ°w˚£øyMl≠ù£V◊Òõ(†≥èãœ2∫`t>j≠ﬁ\´ıjé¡≈:GÁËÙ⁄∫¨û¡<„S¢Æ>„ß
›õ&ÍõULá´<{ÓÂUÇ6@&•`/∂¡uYÔŒ}û…´Ùr5*‹àŸ¶¿Ëñt£›≈óär00ÅëTœÍG¡˘íÄ
π~õÙªlä0Ê+åJ_}Ëßòô˜Í≈!@cå·»˜u Àü∏œY§œﬁŒfÃUnÏ¬áÁ|≈∂«Q«î7ù`Áπá◊¬˝#é/†\K5X-–`\ØŸõ‰Ñ—*–…≤|£2’ÄGRKúFgÕ¶~ùÃÈ’q?	nNo/qüÛ´œÒB/5,= 	*)˘∑«Æ„Ë∑G=≥XÏª¨·h¥ÇdM#µ’eTªª?¥sÌe—õgbßº£”Y∫S§ª¯[÷˜™÷`ŸÕ$Á±1>H9êu˙.]é~ºÜˆtŸ>"YBÀ;(w côñä∫ΩVOñπ;P©˝ãkwπ Z†è∫√¸¯„öÓÈﬁÚIMD2%—¸_R_È0È<∏ØPuYR±
Ü\ jwµÂ‰≤ã⁄}µ¶C%ü
Â_wqæ∏D∑9?&ÑkR∞sR¶ËPAôg|“bÇ=,…Ì|j»á¸'mp⁄@/^1X”ù9C?M(L[¨Z8Ù&: ¶—ÎsÂıLÊÕgÃ◊p®‚u‡¨z`‰”Øæ]HVì4«Óº—•älÄhG)ZgÏö€ò2¨E.iiÊÔkÇ∑†Å$Rü¬ÿÊŒk&ƒHLOùŒ«9Á#X€wÃˇf&‹„ﬁèA£=)π7é~∆È˘=éÀ€∑«√ˇY≠5‚àwUÓŒÿL; ^?ÒÉ›VäÙaxwq‚mÌ	jM	â*'⁄zJ∂^0É˝Ñwu¿‹•Û6£S“ÛåΩ‘Åº¢¬˜ˇÏuú ◊™*Âœd∞jÔ≈ÿb†–Ã‡Ëèa÷¢®OÂ5/Ïa™3≤Ë2Ñ3Zà’ƒ«»|F%ÚﬁZCÌŒÈõkó,3(:ÿúã)ön/enà≠ZtX›>âQr#ñZê0sñ"Ÿ.„Øèê∫O'ºyè¥ÅhﬂCÆﬂìZrûìJÄù∫IıûF∂8Hbèf8Ω
¶7ùí[Î?™q∂oµA¨¶æJD/[7r«JOGx™AC:ÏN≈#©#ºÜ8áÌÜ åÉFD≥Ö¨\e«≈s“—€Uö ’ÍªWlEËn?÷‘Ú%+„¯÷	iZ2$#≤∫£lËh€c1íÒπ¸•”¥øç~´Ï˙◊“„”%µ±˜>Ùàt¸˛-æùÎ4¬JM‹ñ…DA&Ü·–£g‰œ3å 6˛ej¥Ω˜4≈iñCl)·”∂¨âØ:ì¶ÑQÉ·XÂ®≠üJìL™âı¢∏@∞Á≥√≠=KÍ—”•Uã√≠»Î`{&2ÛIo°√ç'û
:#º1ÿ»E«ÄRãô4vª<,À^LÚ\«Â}ΩŒ{ÃÄ°Î±«∑>Ir∆Íœ¬ﬂVOádê•ô_Vˆ#õòáô@≈‰‚((DˆÇáAÇZBÀÉrEù+Âs7A“¨⁄Wﬂ‹õ–-…9Çé∞å›ÃÄ¯ÿi´7cö>îIfßäÎÎGÃm,I°›Ï)ı_ç¥6œåS˙ó5H∞÷ÆAy£»›gÀL±Æ’;‘€‰¿œ‚ÑO. ™´X¢˜˘ù£DÏΩ<z“qã zì Dπ¬ŸSƒJ‚¬4¯hF8â⁄~¸ÍñŸp‡G‡ï?84s8ÌËsı[ ñí~r#âÍL1t¢\˝4⁄≈¸Æ—±ù[\Æ0å¿Ù'2rêáâØ]8ë;uæî~+Í‚ﬁY€íGÔp©1"(‹^hêú˘Z†|hÀé8ƒI á§¯ˇÓ/§£ähxKæ]Aß¨p äÃ¿áo¯6 £ƒœ”±œGZ;é™á˚X}!wßïÍ¢úË∞¢R«,=OBßZÆÄj∂Y\˝æäX¿<~AY1•:XÓÕ#˝wπRˇãaÖ
Ct≠ƒøñlÿv¿<ä¨EUI[cî2ÂΩÕ`;y¶P6øÛ`¥î∫ˆ@+˘JS¬1—«ªÉ¥x¢’ §åÖ»3ãº!„yîU\Cõ«‹Ÿ¥∂7ïC#pÙ{R≠Å-'rß¯D«/Z‡¿G•‹—ó? yh√ p;)ô>É«w&d è€£ßﬂG∂€¯ƒœB=àå'YQıBJˆ˜îç∏4Ü]ò¢Ê‡¸ÌÛâwWx§_—Êèµ’êöúÁT≥Ÿñ‘µ:k™îªàáaZ;A›Ê v‡*8Ø+À™˝ıÖï—2@õµpqÒT∂nπÃ†Ñ[(«mZ˙ÚÎóÕÕQ¸)o⁄Íﬂ,n;apüù7ïcúWFZJ#ﬂŸ¬Gå‰rx¢∆ ‚¥9˛Êbâ3VNí]ùF˚‚ÓÒéMç>>≠Iß5eÒÉÂa≥ØMÁ8§@Ï]⁄ºáIŸ‡æƒC≥ô´?í¨;Y“18IK©q{Gö∞|ÑÈ!D‚Ô∏J*æáMÜÂ-ç]DÈ·¢ôÓ˚˚PK˘´ Û
è{‚6¨HX ◊X:ÓÍ7v|µ1£≥êÿ∫É@ïp5Öõ‰©*ailÿgFb◊”ù=~lÅ#Ö•‰vË[√/Éhír)BﬁÓ¯h Ê›¡´Ô’1Ìﬁ2§˚U#BwRñ˛n± ^)à"kù¯öŸrè≥”ŒÔ/◊M>uÜ,ã?úH“Mé„Ω¡x ”Ÿbı7ù2eyÁUõûa\≈∂˙˛o˚zıáﬂÚ≈3ÄÎÉ=Ùf7~âNà¨:0≠W—˜À¯{ï¸B±î®øËlz{˛~Í””ÏCﬂŒ—¿ñBÔ(	g* Ë≥¬L⁄˝‘ÖZkb{¿Y≠·∆ñv ÁYn9ó˙1’∏%/¨õç4Ü// Copyright Epic Games, Inc. All Rights Reserved.

#include "MeshSimplification.h"
#include "DynamicMeshAttributeSet.h"
#include "Util/IndexUtil.h"
#include "Async/ParallelFor.h"



template <typename QuadricErrorType>
QuadricErrorType TMeshSimplification<QuadricErrorType>::ComputeFaceQuadric(const int tid, FVector3d& nface, FVector3d& c, double& Area) const
{
	// compute the new quadric for this tri.
	Mesh->GetTriInfo(tid, nface, Area, c);

	return FQuadricErrorType(nface, c);
}


// Face Quadric Error computation specialized for FAttrBasedQuadricErrord
template<>
FAttrBasedQuadricErrord TMeshSimplification<FAttrBasedQuadricErrord>::ComputeFaceQuadric(const int tid, FVector3d& nface, FVector3d& c, double& Area) const
{
	// compute the new quadric for this tri.
	Mesh->GetTriInfo(tid, nface, Area, c);

	FVector3f n0; FVector3f n1; FVector3f n2;

	if (NormalOverlay != nullptr)
	{
		NormalOverlay->GetTriElements(tid, n0, n1, n2);
	}
	else
	{
		FIndex3i vids = Mesh->GetTriangle(tid);
		n0 = Mesh->GetVertexNormal(vids[0]);
		n1 = Mesh->GetVertexNormal(vids[1]);
		n2 = Mesh->GetVertexNormal(vids[2]);
	}


	FVector3d p0, p1, p2;
	Mesh->GetTriVertices(tid, p0, p1, p2);

	FVector3d n0d(n0.X, n0.Y, n0.Z);
	FVector3d n1d(n1.X, n1.Y, n1.Z);
	FVector3d n2d(n2.X, n2.Y, n2.Z);

	double attrweight = 16.;
	return FQuadricErrorType(p0, p1, p2, n0d, n1d, n2d, nface, c, attrweight);
}

template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::InitializeTriQuadrics()
{
	const int NT = Mesh->MaxTriangleID();
	triQuadrics.SetNum(NT);
	triAreas.SetNum(NT);

	// tested with ParallelFor - no measurable benefit
	//@todo parallel version
	//gParallel.BlockStartEnd(0, Mesh->MaxTriangleID - 1, (start_tid, end_tid) = > {
	FVector3d n, c;
	for (int tid : Mesh->TriangleIndicesItr())
	{
		triQuadrics[tid] = ComputeFaceQuadric(tid, n, c, triAreas[tid]);
	}

}

template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::InitializeSeamQuadrics()
{
	// early out if this feature isn't needed.
	if (!bAllowSeamCollapse)
	{
		return;
	}

	double EdgeWeight = this->SeamEdgeWeight;

	auto AddSeamQuadric = [EdgeWeight, this](int eid)
	{
		FDynamicMesh3::FEdge edge = Mesh->GetEdge(eid);
		FVector3d p0 = Mesh->GetVertex(edge.Vert[0]);
		FVector3d p1 = Mesh->GetVertex(edge.Vert[1]);

		// face normal 
		FVector3d nA = Mesh->GetTriNormal(edge.Tri.A);

		// this constrains the point to a plane aligned with the edge and normal to the face
		FSeamQuadricType& seamQuadric = seamQuadrics.Add(eid, CreateSeamQuadric(p0, p1, nA));


		// add the other side - this constrains the point to the line where the two planes intersect.
		if (edge.Tri.B != FDynamicMesh3::InvalidID)
		{
			FVector3d nB = Mesh->GetTriNormal(edge.Tri.B);
			seamQuadric.Add(CreateSeamQuadric(p0, p1, nB));
		}

		seamQuadric.Scale(EdgeWeight);
	};

	if (Constraints) // The edge constraints an entry for each seam, boundary, group boundary and material boundary
	{
		const auto& EdgeConstraints = Constraints->GetEdgeConstraints();

		for (auto& ConstraintPair : EdgeConstraints)
		{
			int eid = ConstraintPair.Key;

			AddSeamQuadric(eid);
		}

	}
	else
	{
		const FDynamicMeshAttributeSet* Attributes = Mesh->Attributes();

		if (!Attributes)
		{
			return;
		}

		for (int eid : Mesh->EdgeIndicesItr())
		{
			bool bNeedsQuadric = Mesh->IsBoundaryEdge(eid);
			bNeedsQuadric = bNeedsQuadric || Mesh->IsGroupBoundaryEdge(eid);
			if (Attributes)
			{
				bNeedsQuadric = bNeedsQuadric || Attributes->IsMaterialBoundaryEdge(eid);
				bNeedsQuadric = bNeedsQuadric || Attributes->IsSeamEdge(eid);
			}

			if (bNeedsQuadric)
			{
				AddSeamQuadric(eid);
			}
		}
	}
}


template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::InitializeVertexQuadrics()
{

	int NV = Mesh->MaxVertexID();
	vertQuadrics.SetNum(NV);
	// tested with ParallelFor - no measurable benefit 
	//gParallel.BlockStartEnd(0, Mesh->MaxVertexID - 1, (start_vid, end_vid) = > {
	for (int vid : Mesh->VertexIndicesItr())
	{
		vertQuadrics[vid] = FQuadricErrorType::Zero();
		for (int tid : Mesh->VtxTrianglesItr(vid))
		{
			vertQuadrics[vid].Add(triAreas[tid], triQuadrics[tid]);
		}
		//check(TMathUtil.EpsilonEqual(0, vertQuadrics[i].Evaluate(Mesh->GetVertex(i)), TMathUtil.Epsilon * 10));
	}

	// for each seam edge, add the seam quadric to its verts.
	for (auto& seamQuadric : seamQuadrics)
	{
		int eid = seamQuadric.Key;
		FIndex2i vids = Mesh->GetEdgeV(eid);

		vertQuadrics[vids[0]].AddSeamQuadric(seamQuadric.Value);
		vertQuadrics[vids[1]].AddSeamQuadric(seamQuadric.Value);
	}

}

template <typename QuadricErrorType>
QuadricErrorType TMeshSimplification<QuadricErrorType>::AssembleEdgeQuadric(const FDynamicMesh3::FEdge& edge) const
{
	return QuadricErrorType(vertQuadrics[edge.Vert.A], vertQuadrics[edge.Vert.B]);
}

template<>
FAttrBasedQuadricErrord TMeshSimplification<FAttrBasedQuadricErrord>::AssembleEdgeQuadric(const FDynamicMesh3::FEdge& edge) const
{
	FAttrBasedQuadricErrord Q(vertQuadrics[edge.Vert.A], vertQuadrics[edge.Vert.B]);

	// the edge.Tri faces are double counted. Remove one.
	const FIndex2i& Tris = edge.Tri;
	if (Tris.A != FDynamicMesh3::InvalidID)
	{
		Q.Add(-triAreas[Tris.A], triQuadrics[Tris.A]);
	}

	if (Tris.B != FDynamicMesh3::InvalidID)
	{
		Q.Add(-triAreas[Tris.B], triQuadrics[Tris.B]);
	}

	return Q;
}


template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::InitializeQueue()
{
	int NE = Mesh->EdgeCount();
	int MaxEID = Mesh->MaxEdgeID();

	EdgeQuadrics.SetNum(MaxEID);
	EdgeQueue.Initialize(MaxEID);
	TArray<FEdgeError> EdgeErrors;
	EdgeErrors.SetNum(MaxEID);

	// @todo vertex quadrics can be computed in parallel
	//gParallel.BlockStartEnd(0, MaxEID - 1, (start_eid, end_eid) = > {
	//for (int eid = start_eid; eid <= end_eid; eid++) {
	for (int eid : Mesh->EdgeIndicesItr())
	{
		FDynamicMesh3::FEdge edge = Mesh->GetEdge(eid);
		FQuadricErrorType Q = AssembleEdgeQuadric(edge);
		FVector3d opt = OptimalPoint(eid, Q, edge.Vert.A, edge.Vert.B);
		EdgeErrors[eid] = { (float)Q.Evaluate(opt), eid };
		EdgeQuadrics[eid] = QEdge(eid, Q, opt);
	}

	// sorted pq insert is faster, so sort edge errors array and index map
	EdgeErrors.Sort();

	// now do inserts
	int N = EdgeErrors.Num();
	for (int i = 0; i < N; ++i)
	{
		int eid = EdgeErrors[i].eid;
		if (Mesh->IsEdge(eid))
		{
			QEdge& edge = EdgeQuadrics[eid];
			float error = EdgeErrors[i].error;
			EdgeQueue.Insert(eid, error);
		}
	}

	/*
	// previous code that does unsorted insert. This is marginally slower, but
	// might get even slower on larger meshes? have only tried up to about 350k.
	// (still, this function is not the bottleneck...)
	int cur_eid = StartEdges();
	bool done = false;
	do {
		if (Mesh->IsEdge(cur_eid)) {
			QEdge edge = EdgeQuadrics[cur_eid];
			double err = errList[cur_eid];
			EdgeQueue.Enqueue(cur_eid, (float)err);
		}
		cur_eid = GetNextEdge(cur_eid, out done);
	} while (done == false);
	*/
}




template <typename QuadricErrorType>
FVector3d TMeshSimplification<QuadricErrorType>::OptimalPoint(int eid, const FQuadricErrorType& q, int ea, int eb)
{
	// if we would like to preserve boundary, we need to know that here
	// so that we properly score these edges
	if (bHaveBoundary && bPreserveBoundaryShape)
	{
		if (Mesh->IsBoundaryEdge(eid))
		{
			return (Mesh->GetVertex(ea) + Mesh->GetVertex(eb)) * 0.5;
		}
		else
		{
			if (IsBoundaryVertex(ea))
			{
				return Mesh->GetVertex(ea);
			}
			else if (IsBoundaryVertex(eb))
			{
				return Mesh->GetVertex(eb);
			}
		}
	}

	// [TODO] if we have constraints, we should apply them here, for same reason as bdry above...

	switch (CollapseMode)
	{
		case ESimplificationCollapseModes::AverageVertexPosition:
		{
			return GetProjectedPoint((Mesh->GetVertex(ea) + Mesh->GetVertex(eb)) * 0.5);
		}
		break;

		case ESimplificationCollapseModes::MinimalExistingVertexError:
		{
			FVector3d va = Mesh->GetVertex(ea);
			FVector3d vb = Mesh->GetVertex(eb);
			double fa = q.Evaluate(va);
			double fb = q.Evaluate(vb);
			if (fa < fb)
			{
				return va;
			}
			else
			{
				return vb;
			}
		
		}
		break;

		case ESimplificationCollapseModes::MinimalQuadricPositionError:
		{
			FVector3d result = FVector3d::Zero();
			if (q.OptimalPoint(result))
			{
				return GetProjectedPoint(result);
			}

			// degenerate matrix, evaluate quadric at edge end and midpoints
			// (could do line search here...)
			FVector3d va = Mesh->GetVertex(ea);
			FVector3d vb = Mesh->GetVertex(eb);
			FVector3d c = GetProjectedPoint((va + vb) * 0.5);
			double fa = q.Evaluate(va);
			double fb = q.Evaluate(vb);
			double fc = q.Evaluate(c);
			double m = FMath::Min3(fa, fb, fc);
			if (m == fa)
			{
				return va;
			}
			else if (m == fb)
			{
				return vb;
			}
			return c;
		}
		break;
	default:

		// should never happen
		checkSlow(0);
		return FVector3d::Zero();
	}
}




// update queue weight for each edge in vertex one-ring
template <>
void DYNAMICMESH_API TMeshSimplification<FQuadricErrord>::UpdateNeighbours(const FDynamicMesh3::FEdgeCollapseInfo& collapseInfo)
{
	int vid = collapseInfo.KeptVertex;


	double EdgeWeight = this->SeamEdgeWeight;

	for (int eid : Mesh->VtxEdgesItr(vid))
	{
		FDynamicMesh3::FEdge ne = Mesh->GetEdge(eid);

		// update the seam quadric and vert quadric to reflect the new seams
		if (bAllowSeamCollapse)
		{
			
			// need to recompute this seam quadric
			if (FSeamQuadricType* seamQuadric = seamQuadrics.Find(eid))
			{
				// subtract the old seam quadric from adj verts

				vertQuadrics[ne.Vert[0]].SubtractSeamQuadric(*seamQuadric);
				vertQuadrics[ne.Vert[1]].SubtractSeamQuadric(*seamQuadric);

				// rebuild the seam quadric

				FVector3d p0 = Mesh->GetVertex(ne.Vert[0]);
				FVector3d p1 = Mesh->GetVertex(ne.Vert[1]);

				// face normal 
				FVector3d nA = Mesh->GetTriNormal(ne.Tri.A);

				// this constrains the point to a plane aligned with the edge and normal to the face
				*seamQuadric = CreateSeamQuadric(p0, p1, nA);
				// add the other side - this constrains the point to the line where the two planes intersect.
				if (ne.Tri.B != FDynamicMesh3::InvalidID)
				{
					FVector3d nB = Mesh->GetTriNormal(ne.Tri.B);
					seamQuadric->Add(CreateSeamQuadric(p0, p1, nB));
				}

				seamQuadric->Scale(EdgeWeight);

				// add the seam quadric to the adj verts
				vertQuadrics[ne.Vert[0]].AddSeamQuadric(*seamQuadric);
				vertQuadrics[ne.Vert[1]].AddSeamQuadric(*seamQuadric);
			}

		}

		
		FQuadricErrord Q = AssembleEdgeQuadric(ne);
		FVector3d opt = OptimalPoint(eid, Q, ne.Vert.A, ne.Vert.B);
		float err = (float)Q.Evaluate(opt);
		EdgeQuadrics[eid] = QEdge(eid, Q, opt);
		if (EdgeQueue.Contains(eid))
		{
			EdgeQueue.Update(eid, err);
		}
		else
		{
			EdgeQueue.Insert(eid, err);
		}
	}
}

// update queue weight for each edge in vertex one-ring.  Memoryless
template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::UpdateNeighbours(const FDynamicMesh3::FEdgeCollapseInfo& collapseInfo)
{
	double EdgeWeight = this->SeamEdgeWeight;

	int vid = collapseInfo.KeptVertex;
	FIndex2i removedTris = collapseInfo.RemovedTris;
	FIndex2i opposingVerts = collapseInfo.OpposingVerts;

	TArray<int, TInlineAllocator<15>> AdjTris;
	for (int tid : Mesh->VtxTrianglesItr(vid))
	{
		AdjTris.Add(tid);
	}

	TArray<int, TInlineAllocator<15>> AdjEdges;
	for (int eid : Mesh->VtxEdgesItr(vid))
	{
		AdjEdges.Add(eid);
	}

	// This is the faster version that selectively updates the one-ring
	{

		// compute the change in affected face quadrics, and then propagate 
		// that change to the face adjacent verts.
		FVector3d n, c;
		double NewtriArea;

		// Update the triangle areas and quadrics that will have changed
		for (int tid : AdjTris)
		{

			const double OldtriArea = triAreas[tid];
			const FQuadricErrorType OldtriQuadric = triQuadrics[tid];


			// compute the new quadric for this tri.
			FQuadricErrorType NewtriQuadric = ComputeFaceQuadric(tid, n, c, NewtriArea);

			// update the arrays that hold the current face area & quadric
			triAreas[tid] = NewtriArea;
			triQuadrics[tid] = NewtriQuadric;

			FIndex3i tri_vids = Mesh->GetTriangle(tid);

			// update the vert quadrics that are adjacent to vid.
			for (int32 i = 0; i < 3; ++i)
			{
				if (tri_vids[i] == vid) continue;

				// correct the adjacent vertQuadrics
				vertQuadrics[tri_vids[i]].Add(-OldtriArea, OldtriQuadric); // subtract old quadric
				vertQuadrics[tri_vids[i]].Add(NewtriArea, NewtriQuadric); // add new quadric
			}
		}

		// remove the influence of the dead tris from the two verts that were opposing the collapsed edge
		{
			for (int i = 0; i < 2; ++i)
			{
				if (removedTris[i] != FDynamicMesh3::InvalidID)
				{
					const double   oldArea = triAreas[removedTris[i]];
					FQuadricErrorType oldQuadric = triQuadrics[removedTris[i]];

					// subtract the quadric from the opposing vert
					vertQuadrics[opposingVerts[i]].Add(-oldArea, oldQuadric);

					// zero out the quadric & area for the removed tris.
					triQuadrics[removedTris[i]] = FQuadricErrorType::Zero();
					triAreas[removedTris[i]] = 0.;
				}
			}
		}
		// Rebuild the quadric for the vert that was retained during the collapse.
		// NB: in the version with memory this quadric took the value of the edge quadric that collapsed.
		{
			FQuadricErrorType vertQuadric = FQuadricErrorType::Zero();
			for (int tid : AdjTris)
			{
				vertQuadric.Add(triAreas[tid], triQuadrics[tid]);
			}
			vertQuadrics[vid] = vertQuadric;
		}

		if (bAllowSeamCollapse)
		{
			for (int eid : AdjEdges)
			{
				// need to recompute this seam quadric
				if (FSeamQuadricType* seamQuadric = seamQuadrics.Find(eid))
				{
					const FDynamicMesh3::FEdge ne = Mesh->GetEdge(eid);

					// subtract the old seam quadric from adj verts
					if (ne.Vert[0] == vid) // note we have reset the vertQuadric at the retained vertex so it doesn't have a contribution from the old seamQ
					{
						vertQuadrics[ne.Vert[1]].SubtractSeamQuadric(*seamQuadric);
					}
					else
					{
						check(ne.Vert[1] == vid);
						vertQuadrics[ne.Vert[0]].SubtractSeamQuadric(*seamQuadric);
					}

					// rebuild the seam quadric

					FVector3d p0 = Mesh->GetVertex(ne.Vert[0]);
					FVector3d p1 = Mesh->GetVertex(ne.Vert[1]);

					// face normal 
					FVector3d nA = Mesh->GetTriNormal(ne.Tri.A);

					// this constrains the point to a plane aligned with the edge and normal to the face
					*seamQuadric = CreateSeamQuadric(p0, p1, nA);
					// add the other side - this constrains the point to the line where the two planes intersect.
					if (ne.Tri.B != FDynamicMesh3::InvalidID)
					{
						FVector3d nB = Mesh->GetTriNormal(ne.Tri.B);
						seamQuadric->Add(CreateSeamQuadric(p0, p1, nB));
					}

					seamQuadric->Scale(EdgeWeight);

					// add the seam quadric to the adj verts
					vertQuadrics[ne.Vert[0]].AddSeamQuadric(*seamQuadric);
					vertQuadrics[ne.Vert[1]].AddSeamQuadric(*seamQuadric);
				}
			}
	}
	}

	// Update all the edges
	{
		TArray<int, TInlineAllocator<64>> EdgesToUpdate;
		for (int adjeid : AdjEdges)
		{
			EdgesToUpdate.Add(adjeid);

			const FIndex2i Verts = Mesh->GetEdgeV(adjeid);
			int adjvid = (Verts[0] == vid) ? Verts[1] : Verts[0];
			if (adjvid != FDynamicMesh3::InvalidID)
			{
				for (int eid : Mesh->VtxEdgesItr(adjvid))
				{
					if (eid != adjeid)
					{
						EdgesToUpdate.AddUnique(eid);
					}
				}
			}
		}

		for (int eid : EdgesToUpdate)
		{
		
			const FDynamicMesh3::FEdge edgeData = Mesh->GetEdge(eid);
			FQuadricErrorType Q = AssembleEdgeQuadric(edgeData);

			FVector3d opt = OptimalPoint(eid, Q, edgeData.Vert[0], edgeData.Vert[1]);
			float err = (float)Q.Evaluate(opt);
			EdgeQuadrics[eid] = QEdge(eid, Q, opt);
			if (EdgeQueue.Contains(eid))
			{
				EdgeQueue.Update(eid, err);
			}
			else
			{
				EdgeQueue.Insert(eid, err);
			}
		}
	}
}





template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::Precompute(bool bMeshIsClosed)
{
	bHaveBoundary = false;
	IsBoundaryVtxCache.SetNum(Mesh->MaxVertexID());
	if (bMeshIsClosed == false)
	{
		for (int eid : Mesh->BoundaryEdgeIndicesItr())
		{
			FIndex2i ev = Mesh->GetEdgeV(eid);
			IsBoundaryVtxCache[ev.A] = true;
			IsBoundaryVtxCache[ev.B] = true;
			bHaveBoundary = true;
		}
	}
}




template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::DoSimplify()
{
	if (Mesh->TriangleCount() == 0)    // badness if we don't catch this...
	{
		return;
	}

	if (Mesh->HasAttributes() && GetConstraints().IsSet() == false)
	{
		ensureMsgf(false, TEXT("Input Mesh has Attribute overlays but no Constraints are configured. Use FMeshConstraintsUtil::ConstrainAllBoundariesAndSeams() to create a Constraint Set for Attribute seams."));
	}

	ProfileBeginPass();

	ProfileBeginSetup();
	Precompute();
	if (Cancelled())
	{
		return;
	}
	InitializeTriQuadrics();
	if (Cancelled())
	{
		return;
	}
	InitializeSeamQuadrics();
	if (Cancelled())
	{
		return;
	}
	InitializeVertexQuadrics();
	if (Cancelled())
	{
		return;
	}
	InitializeQueue();
	if (Cancelled())
	{
		return;
	}
	ProfileEndSetup();

	ProfileBeginOps();

	ProfileBeginCollapse();
	while (EdgeQueue.GetCount() > 0)
	{
		// termination criteria
		if (SimplifyMode == ETargetModes::VertexCount)
		{
			if (Mesh->VertexCount() <= TargetCount)
			{
				break;
			}
		}
		else if (SimplifyMode == ETargetModes::MaxError)
		{
			float qe = EdgeQueue.GetFirstNodePriority();
			if (FMath::Abs(qe) > MaxErrorAllowed)
			{
				break;
			}
		}
		else
		{
			if (Mesh->TriangleCount() <= TargetCount)
			{
				break;
			}
		}
		
		COUNT_ITERATIONS++;	
		int eid = EdgeQueue.Dequeue(); 
		if (Mesh->IsEdge(eid) == false)
		{
			continue;
		}
		if (Cancelled())
		{
			return;
		}


		FDynamicMesh3::FEdgeCollapseInfo collapseInfo;
		ESimplificationResult result = CollapseEdge(eid, EdgeQuadrics[eid].collapse_pt, collapseInfo);
		if (result == ESimplificationResult::Ok_Collapsed)
		{
			vertQuadrics[collapseInfo.KeptVertex] = EdgeQuadrics[eid].q;
			UpdateNeighbours(collapseInfo);
		}
	}
	ProfileEndCollapse();
	ProfileEndOps();

	if (Cancelled())
	{
		return;
	}

	Reproject();

	ProfileEndPass();
}


template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::SimplifyToTriangleCount(int nCount)
{
	SimplifyMode = ETargetModes::TriangleCount;
	TargetCount = FMath::Max(1, nCount);
	MinEdgeLength = FMathd::MaxReal;
	MaxErrorAllowed = FMathf::MaxReal;
	DoSimplify();
}

template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::SimplifyToVertexCount(int nCount)
{
	SimplifyMode = ETargetModes::VertexCount;
	TargetCount = FMath::Max(3, nCount);
	MinEdgeLength = FMathd::MaxReal;
	MaxErrorAllowed = FMathf::MaxReal;
	DoSimplify();
}

template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::SimplifyToEdgeLength(double minEdgeLen)
{
	SimplifyMode = ETargetModes::MinEdgeLength;
	TargetCount = 1;
	MinEdgeLength = minEdgeLen;
	MaxErrorAllowed = FMathf::MaxReal;
	DoSimplify();
}

template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::SimplifyToMaxError(double MaxError)
{
	SimplifyMode = ETargetModes::MaxError;
	TargetCount = 1;
	MinEdgeLength = FMathd::MaxReal;
	MaxErrorAllowed = MaxError;
	DoSimplify();
}



template<typename GetTriNormalFuncType>
static bool IsDevelopableVertex(const FDynamicMesh3& Mesh, int32 VertexID, double DotTolerance,
	GetTriNormalFuncType GetTriNormalFunc)
{
	FVector3d Normal1, Normal2;
	int32 Normal1Count = 0, Normal2Count = 0, OtherCount = 0;
	Mesh.EnumerateVertexTriangles(VertexID, [&](int32 tid)
	{
		FVector3d TriNormal = GetTriNormalFunc(tid);
		if (Normal1Count == 0)
		{
			Normal1 = TriNormal;
			Normal1Count++;
			return;
		}
		if (TriNormal.Dot(Normal1) > DotTolerance)
		{
			Normal1Count++;
			return;
		}
		if (Normal2Count == 0)
		{
			Normal2 = TriNormal;
			Normal2Count++;
			return;
		}
		if (TriNormal.Dot(Normal2) > DotTolerance)
		{
			Normal2Count++;
			return;
		}
		OtherCount++;
	});
	return OtherCount == 0;
}




template<typename GetTriNormalFuncType>
static bool IsCollapsableDevelopableEdge(const FDynamicMesh3& Mesh, int32 CollapseEdgeID, int32 RemoveV, int32 KeepV, double DotTolerance,
	GetTriNormalFuncType GetTriNormalFunc)
{
	FIndex2i CollapseEdgeT = Mesh.GetEdgeT(CollapseEdgeID);
	FVector3d Normal1 = GetTriNormalFunc(CollapseEdgeT.A);
	FVector3d Normal2 = GetTriNormalFunc(CollapseEdgeT.B);

	// assuming is that RemoveV is developable vertex...should check?

	// planar case
	if (Normal1.Dot(Normal2) > DotTolerance)
	{
		bool bIsFlat = true;
		Mesh.EnumerateVertexTriangles(RemoveV, [&](int32 tid)
		{
			if (GetTriNormalFunc(tid).Dot(Normal1) < DotTolerance)
			{
				bIsFlat = false;
			}
		});
		return bIsFlat;
	}

	// if we are not planar, we need to find the 'other' developable edge at RemoveV.
	// This edge must be aligned w/ our collapse edge and have the same normals
	FVector3d A = Mesh.GetVertex(RemoveV), B = Mesh.GetVertex(KeepV);
	FVector3d EdgeDir(B - A); EdgeDir.Normalize();
	int32 FoldEdges = 0, FlatEdges = 0, OtherEdges = 0;
	for (int32 eid : Mesh.VtxEdgesItr(RemoveV))
	{
		if (eid != CollapseEdgeID)
		{
			FIndex2i EdgeT = Mesh.GetEdgeT(eid);
			FVector3d Normal3 = GetTriNormalFunc(EdgeT.A);
			FVector3d Normal4 = GetTriNormalFunc(EdgeT.B);

			FIndex2i OtherEdgeV = Mesh.GetEdgeV(eid);
			int32 OtherV = IndexUtil::FindEdgeOtherVertex(OtherEdgeV, RemoveV);
			FVector3d C = Mesh.GetVertex(OtherV);
			if ((A-C).Normalized().Dot(EdgeDir) > DotTolerance)
			{
				if ((Normal3.Dot(Normal1) > DotTolerance && Normal4.Dot(Normal2) > DotTolerance) ||
					(Normal3.Dot(Normal2) > DotTolerance && Normal4.Dot(Normal1) > DotTolerance))
				{
					FoldEdges++;
				}
			}
			else if ( Normal3.Dot(Normal4) > DotTolerance)
			{
				FlatEdges++;
			}
			else
			{
				OtherEdges++;
			}
		}
	}
	return (FoldEdges == 1 && OtherEdges == 0);
}


template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::SimplifyToMinimalPlanar(double CoplanarAngleTolDeg)
{
#define RETURN_IF_CANCELLED 	if (Cancelled()) { return; }

	if (Mesh->TriangleCount() == 0)    // badness if we don't catch this...
	{
		return;
	}

	// we don't collapse on the boundary
	bHaveBoundary = false;

	// keep triangle normals
	TArray<FVector3d> TriNormals;
	TArray<bool> DevelopableVerts;

	ProfileBeginPass();

	ProfileBeginSetup();
	Precompute();
	RETURN_IF_CANCELLED;

	TriNormals.SetNum(Mesh->MaxTriangleID());
	ParallelFor(Mesh->MaxTriangleID(), [&](int32 tid)
	{
		if (Mesh->IsTriangle(tid))
		{
			TriNormals[tid] = Mesh->GetTriNormal(tid);
		}
	});
	RETURN_IF_CANCELLED;

	DevelopableVerts.SetNum(Mesh->MaxVertexID());
	double PlanarDotTol = FMathd::Cos( CoplanarAngleTolDeg * FMathd::DegToRad );
	ParallelFor(Mesh->MaxVertexID(), [&](int32 vid)
	{
		if (Mesh->IsVertex(vid))
		{
			DevelopableVerts[vid] = IsDevelopableVertex(*Mesh, vid, PlanarDotTol, [&](int32 tid) { return TriNormals[tid]; });
		}
	});
	RETURN_IF_CANCELLED;

	ProfileEndSetup();


	ProfileBeginOps();

	ProfileBeginCollapse();

	TArray<int32> CollapseEdges;
	int32 MaxRounds = 50;
	int32 num_last_pass = 0;
	for (int ri = 0; ri < MaxRounds; ++ri)
	{
		num_last_pass = 0;

		// collect up edges we have identified for collapse
		CollapseEdges.Reset();
		for (int32 eid : Mesh->EdgeIndicesItr())
		{
			FIndex2i ev = Mesh->GetEdgeV(eid);
			if (DevelopableVerts[ev.A] || DevelopableVerts[ev.B])
			{
				CollapseEdges.Add(eid);
			}
		}


		FVector3d va = FVector3d::Zero(), vb = FVector3d::Zero();
		for ( int32 eid : CollapseEdges )
		{
			if ( (Mesh->IsEdge(eid) == false) || Mesh->IsBoundaryEdge(eid) )
			{
				continue;
			}
			RETURN_IF_CANCELLED;
			COUNT_ITERATIONS++;

			FIndex2i ev = Mesh->GetEdgeV(eid);
			bool bDevelopableA = DevelopableVerts[ev.A];
			bool bDevelopableB = DevelopableVerts[ev.B];
			if (bDevelopableA || bDevelopableB)		// this may change during execution as edges are collapsed
			{
				if (! bDevelopableA)		// any other preference for verts?
				{
					Swap(ev.A, ev.B);
				}

				bool bIsCollapsible = IsCollapsableDevelopableEdge(*Mesh, eid, ev.A, ev.B, PlanarDotTol, [&](int32 tid) { return TriNormals[tid]; });
				if (bIsCollapsible)
				{
					FDynamicMesh3::FEdgeCollapseInfo collapseInfo;
					ESimplificationResult result;
					result = CollapseEdge(eid, Mesh->GetVertex(ev.B), collapseInfo, ev.B);
					if (result == ESimplificationResult::Ok_Collapsed)
					{
						++num_last_pass;

						int vKeptID = collapseInfo.KeptVertex;
						Mesh->EnumerateVertexTriangles(vKeptID, [&](int32 tid)
						{
							TriNormals[tid] = Mesh->GetTriNormal(tid);
						});
						for (int32 vid : Mesh->VtxVerticesItr(vKeptID))
						{
							DevelopableVerts[vid] = IsDevelopableVertex(*Mesh, vid, PlanarDotTol, [&](int32 tid) { return TriNormals[tid]; });
						}
						DevelopableVerts[vKeptID] = IsDevelopableVertex(*Mesh, vKeptID, PlanarDotTol, [&](int32 tid) { return TriNormals[tid]; });
					}
					else if (bDevelopableA && bDevelopableB &&
								IsCollapsableDevelopableEdge(*Mesh, eid, ev.B, ev.A, PlanarDotTol, [&](int32 tid) { return TriNormals[tid]; }) )
					{
						// we can try collapsing to A
						result = CollapseEdge(eid, Mesh->GetVertex(ev.A), collapseInfo, ev.A);
						if (result == ESimplificationResult::Ok_Collapsed)
						{
							++num_last_pass;

							int vKeptID = collapseInfo.KeptVertex;
							Mesh->EnumerateVertexTriangles(vKeptID, [&](int32 tid)
							{
								TriNormals[tid] = Mesh->GetTriNormal(tid);
							});
							for (int32 vid : Mesh->VtxVerticesItr(vKeptID))
							{
								DevelopableVerts[vid] = IsDevelopableVertex(*Mesh, vid, PlanarDotTol, [&](int32 tid) { return TriNormals[tid]; });
							}
							DevelopableVerts[vKeptID] = IsDevelopableVertex(*Mesh, vKeptID, PlanarDotTol, [&](int32 tid) { return TriNormals[tid]; });
						}
					}
				}
			}
		}

		if (num_last_pass == 0)     // converged
		{
			break;
		}
	}
	ProfileEndCollapse();
	ProfileEndOps();

	RETURN_IF_CANCELLED;

	Reproject();

	ProfileEndPass();

#undef RETURN_IF_CANCELLED
}



template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::FastCollapsePass(double fMinEdgeLength, int nRounds, bool MeshIsClosedHint)
{
	if (Mesh->TriangleCount() == 0)    // badness if we don't catch this...
	{
		return;
	}

	MinEdgeLength = fMinEdgeLength;
	double min_sqr = MinEdgeLength * MinEdgeLength;

	// we don't collapse on the boundary
	bHaveBoundary = false;

	ProfileBeginPass();

	ProfileBeginSetup();
	Precompute(MeshIsClosedHint);
	if (Cancelled())
	{
		return;
	}
	ProfileEndSetup();

	ProfileBeginOps();

	ProfileBeginCollapse();

	int N = Mesh->MaxEdgeID();
	int num_last_pass = 0;
	for (int ri = 0; ri < nRounds; ++ri)
	{
		num_last_pass = 0;

		FVector3d va = FVector3d::Zero(), vb = FVector3d::Zero();
		for (int eid = 0; eid < N; ++eid)
		{
			if ((!Mesh->IsEdge(eid)) || Mesh->IsBoundaryEdge(eid))
			{
				continue;
			}
			if (Cancelled())
			{
				return;
			}

			Mesh->GetEdgeV(eid, va, vb);
			if (va.DistanceSquared(vb) > min_sqr)
			{
				continue;
			}

			COUNT_ITERATIONS++;

			FVector3d midpoint = (va + vb) * 0.5;
			FDynamicMesh3::FEdgeCollapseInfo collapseInfo;
			ESimplificationResult result = CollapseEdge(eid, midpoint, collapseInfo);
			if (result == ESimplificationResult::Ok_Collapsed)
			{
				++num_last_pass;
			}
		}

		if (num_last_pass == 0)     // converged
		{
			break;
		}
	}
	ProfileEndCollapse();
	ProfileEndOps();

	if (Cancelled())
	{
		return;
	}

	Reproject();

	ProfileEndPass();
}














template <typename QuadricErrorType>
ESimplificationResult TMeshSimplification<QuadricErrorType>::CollapseEdge(int edgeID, FVector3d vNewPos, FDynamicMesh3::FEdgeCollapseInfo& collapseInfo, int32 RequireKeepVert)
{
	collapseInfo.KeptVertex = FDynamicMesh3::InvalidID;
	RuntimeDebugCheck(edgeID);

	FEdgeConstraint constraint =
		(!Constraints) ? FEdgeConstraint::Unconstrained() : Constraints->GetEdgeConstraint(edgeID);
	if (constraint.NoModifications())
	{
		return ESimplificationResult::Ignored_EdgeIsFullyConstrained;
	}
	if (constraint.CanCollapse() == false)
	{
		return ESimplificationResult::Ignored_EdgeIsFullyConstrained;
	}


	// look up verts and tris for this edge
	if (Mesh->IsEdge(edgeID) == false)
	{
		return ESimplificationResult::Failed_NotAnEdge;
	}
	const FDynamicMesh3::FEdge Edge = Mesh->GetEdge(edgeID);
	int a = Edge.Vert[0], b = Edge.Vert[1], t0 = Edge.Tri[0], t1 = Edge.Tri[1];
	bool bIsBoundaryEdge = (t1 == FDynamicMesh3::InvalidID);

	// look up 'other' verts c (from t0) and d (from t1, if it exists)
	FIndex3i T0tv = Mesh->GetTriangle(t0);
	int c = IndexUtil::FindTriOtherVtx(a, b, T0tv);
	FIndex3i T1tv = (bIsBoundaryEdge) ? FDynamicMesh3::InvalidTriangle : Mesh->GetTriangle(t1);
	int d = (bIsBoundaryEdge) ? FDynamicMesh3::InvalidID : IndexUtil::FindTriOtherVtx(a, b, T1tv);

	FVector3d vA = Mesh->GetVertex(a);
	FVector3d vB = Mesh->GetVertex(b);
	double edge_len_sqr = (vA - vB).SquaredLength();
	if (edge_len_sqr > MinEdgeLength * MinEdgeLength)
	{
		return ESimplificationResult::Ignored_EdgeTooLong;
	}

	ProfileBeginCollapse();

	// check if we should collapse, and also find which vertex we should retain
	// in cases where we have constraints/etc
	int collapse_to = -1;
	bool bCanCollapse = CanCollapseEdge(edgeID, a, b, c, d, t0, t1, collapse_to);
	if (bCanCollapse == false)
	{
		return ESimplificationResult::Ignored_Constrained;
	}

	// if we have a boundary, we want to collapse to boundary
	if (bPreserveBoundaryShape && bHaveBoundary)
	{
		if (collapse_to != -1)
		{
			if ((IsBoundaryVertex(b) && collapse_to != b) ||
				(IsBoundaryVertex(a) && collapse_to != a))
			{
				return ESimplificationResult::Ignored_Constrained;
			}
		}
		if (IsBoundaryVertex(b))
		{
			collapse_to = b;
		}
		else if (IsBoundaryVertex(a))
		{
			collapse_to = a;
		}
	}

	if (RequireKeepVert == a || RequireKeepVert == b)
	{
		if (collapse_to >= 0 && collapse_to != RequireKeepVert)
		{
			return ESimplificationResult::Ignored_Constrained;
		}
		collapse_to = RequireKeepVert;
	}

	// optimization: if edge cd exists, we cannot collapse or flip. look that up here?
	//  funcs will do it internally...
	//  (or maybe we can collapse if cd exists? edge-collapse doesn't check for it explicitly...)
	ESimplificationResult retVal = ESimplificationResult::Failed_OpNotSuccessful;

	int iKeep = b, iCollapse = a;
	bool bCanMove = true;
	
	if (collapse_to != -1)
	{
		iKeep = collapse_to;
		iCollapse = (iKeep == a) ? b : a;

		// if constraints require a fixed position
		if (Constraints)
		{
			bCanMove = Constraints->GetVertexConstraint(collapse_to).bCanMove;
		}
	}
	double collapse_t = 0;
	if (bCanMove)
	{
		vNewPos = GetProjectedCollapsePosition(iKeep, vNewPos);
		double div = vA.Distance(vB);
		collapse_t = (div < FMathd::ZeroTolerance) ? 0.5 : (vNewPos.Distance(Mesh->GetVertex(iKeep))) / div;
		collapse_t = VectorUtil::Clamp(collapse_t, 0.0, 1.0);
	}
	else
	{
		vNewPos = (collapse_to == a) ? vA : vB;
	}

	// check if this collapse will create a normal flip. Also checks
	// for invalid collapse nbrhood, since we are doing one-ring iter anyway.
	// [TODO] could we skip this one-ring check in CollapseEdge? pass in hints?
	if (CheckIfCollapseCreatesFlipOrInvalid(a, b, vNewPos, t0, t1) || CheckIfCollapseCreatesFlipOrInvalid(b, a, vNewPos, t0, t1))
	{
		ProfileEndCollapse();
		return ESimplificationResult::Ignored_CreatesFlip;
	}

	// lots of cases where we cannot collapse, but we should just let
	// Mesh sort that out, right?
	COUNT_COLLAPSES++;

	EMeshResult result = Mesh->CollapseEdge(iKeep, iCollapse, collapse_t, collapseInfo);
	if (result == EMeshResult::Ok)
	{
		Mesh->SetVertex(iKeep, vNewPos);
		if (Constraints)
		{
			Constraints->ClearEdgeConstraint(edgeID);
			Constraints->ClearEdgeConstraint(collapseInfo.RemovedEdges.A);
			if (collapseInfo.RemovedEdges.B != FDynamicMesh3::InvalidID)
			{
				Constraints->ClearEdgeConstraint(collapseInfo.RemovedEdges.B);
			}
			Constraints->ClearVertexConstraint(iCollapse);
		}
		OnEdgeCollapse(edgeID, iKeep, iCollapse, collapseInfo);
		DoDebugChecks();

		retVal = ESimplificationResult::Ok_Collapsed;
	}

	ProfileEndCollapse();
	return retVal;
}








// Project vertices onto projection target. 
// We can do projection in parallel if we have .net 
template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::FullProjectionPass()
{
	auto project = [&](int vID)
	{
		if (IsVertexPositionConstrained(vID))
		{
			return;
		}
		if (VertexControlF != nullptr && ((int)VertexControlF(vID) & (int)EVertexControl::NoProject) != 0)
		{
			return;
		}
		FVector3d curpos = Mesh->GetVertex(vID);
		FVector3d projected = ProjTarget->Project(curpos, vID);
		Mesh->SetVertex(vID, projected);
	};

	ApplyToProjectVertices(project);

	// TODO: optionally do projection in parallel?
}

template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::ApplyToProjectVertices(const TFunction<void(int)>& apply_f)
{
	for (int vid : Mesh->VertexIndicesItr())
	{
		apply_f(vid);
	}
}

template <typename QuadricErrorType>
void TMeshSimplification<QuadricErrorType>::ProjectVertex(int vID, IProjectionTarget* targetIn)
{
	FVector3d curpos = Mesh->GetVertex(vID);
	FVector3d projected = targetIn->Project(curpos, vID);
	Mesh->SetVertex(vID, projected);
}

// used by collapse-edge to get projected position for new vertex
template <typename QuadricErrorType>
FVector3d TMeshSimplification<QuadricErrorType>::GetProjectedCollapsePosition(int vid, const FVector3d& vNewPos)
{
	if (Constraints)
	{
		FVertexConstraint vc = Constraints->GetVertexConstraint(vid);
		if (vc.Target != nullptr)
		{
			return vc.Target->Project(vNewPos, vid);
		}
		if (vc.bCanMove == false)
		{
			return vNewPos;
		}
	}
	// no constraint applied, so if we have a target surface, project to that
	if (EnableInlineProjection() && ProjTarget != nullptr)
	{
		if (VertexControlF == nullptr || ((int)VertexControlF(vid) & (int)EVertexControl::NoProject) == 0)
		{
			return ProjTarget->Project(vNewPos, vid);
		}
	}
	return vNewPos;
}


// Custom behavior for FAttrBasedQuadric simplifier.
template<>
void TMeshSimplification<FAttrBasedQuadricErrord>::OnEdgeCollapse(int edgeID, int va, int vb, const FDynamicMesh3::FEdgeCollapseInfo& collapseInfo)
{

	// Update the normal
	FAttrBasedQuadricErrord& Quadric = EdgeQuadrics[edgeID].q;
	FVector3d collapse_pt = EdgeQuadrics[edgeID].collapse_pt;

	FVector3d UpdatedNormald;
	Quadric.ComputeAttributes(collapse_pt, UpdatedNormald);

	FVector3f UpdatedNormal(UpdatedNormald.X, UpdatedNormald.Y, UpdatedNormald.Z);
	UpdatedNormal.Normalize();

	if (NormalOverlay != nullptr)
	{
		// Get all the elements associated with this vertex (could be more than one to account for split vertex data)
		TArray<int> ElementIdArray;
		NormalOverlay->GetVertexElements(va, ElementIdArray);

		if (ElementIdArray.Num() > 1)
		{
			// keep whatever split normals are currently in the overlay.
			// @todo: normalize the split normals - since the values here result from a lerp
			return;
		}
	
		// at most one element
		for (int ElementId : ElementIdArray)
		{
			NormalOverlay->SetElement(ElementId, UpdatedNormal);
		}
	}
	else
	{
		Mesh->SetVertexNormal(va, UpdatedNormal);
	}

}



// These are explicit instantiations of the templates that are exported from the shared lib.
// Only these instantiations of the template can be used.
// This is necessary because we have placed most of the templated functions in this .cpp file, instead of the header.
template class DYNAMICMESH_API TMeshSimplification< FAttrBasedQuadricErrord >;
template class DYNAMICMESH_API TMeshSimplification< FVolPresQuadricErrord >;
template class DYNAMICMESH_API TMeshSimplification< FQuadricErrord >;
                                                                                                                                                                                                                                              ﬁzáf€ËfWÎ`b˘h¸=ëP…OD[úAËËÖW≥µ`∏6<B7>úÂ§Ó≠†D¬ÎﬂSX¡G7Ænqı{˙©îs¥Ó—WüT©É_†cu§)$t$l0u≥Nf0Å9&<ËK®_$Ùº!‚QKf›‹Áê<ÿµ!∫V˙L'WkÿıôgT):X}›S˚q‘≤”‰ÊZÀÊÑß±∂yQ∑œ¶ëî›à•ΩãÒtó"Z˘∫òï•vzñ˘+k?Ø0Sftmô‡ËÍÓÍè˛[0πÍíéﬁﬂ_´ö  ?Ë¡‰¨/äö˘!§Úu-ˇÉîSo;ú^¬÷áx!˘kﬂV$·,çÕVG»I}~ôH~b√Ø˛Nüh$≤≥;)Ë+‰ßà˘ºë˚ñ‡Ç+®m ü∞ïªúOºÌ5î!∂úZÌ‹oÅì˛!úX£õËJ¢›]%]&Ïä‡±ª¨`FÇçpÄ†Ô®Îô∞¢ÁÈk* ∞¬g“cY’åè—Ãèõ≈H}….Ò%Ú©Û∏≈€ƒù˙—Ù_„8)∞n”P·‹XYakä71¿Bzæ7∫$!G|ØFÏ$ÊÖı1J‡…°$ƒo—¯˘1äwê’öß‡ÙΩ/ÑK¬¨ô˛Fdh	O€Q∫∞ˇR˛/√∂=∏J!ÊGa¢s›¥xáÚﬁE∑éñ√[√s¥8¸«Oc\«Ïë$î[Àí4ˆ?å¿„‚ΩÇ™.1√ö“”Ô„ ≈Qı6Üºö,Ç*S…ô≥teocJÈñV|!\ÍNúü€R®)‘4r¥»_‘ä ﬂ3m /‹÷YZÊ&m€~KX äßoÜ˝ıåƒnÁπ»sAëÛäåØŸs:Ld∑ÀYÁﬂÌ—å»ìá\Gh`ù{ve”*ˆÜÈ<¿ ∞+ËèTYﬂM1û^ÿmÇT˙ªˇ0Ø€û$¨zKn•Nä√√v≠«	Ω`˝ù©Œ»µ›0Í≠<Ã<#o!#£˝ıÛANº¥‚‡zçF≥ÃD)rX≤~äª‹∫Ô≠5´à™É7»ËGÆj
V´¬>∆‡«¢«ïæqeÆ]”eÑÖ1ïg…Ìá Ì≈“öd’;3ãÕÚ.i„- [∆…6ˇªRy¥ÀtÑ’4ñ√˛ …)H7ô0êm>≤Ω3£mc‰!ˇ#øõ∂˚Ì∑™X4SRéîb—¶ í´ƒ;◊v-¿-…(¥d;y©Êì ≠Jç£V|!S≈`D&g»¡úëé¯lOÊUΩïß∂4„èñ!‡≠˛ﬁI=?Ì·Ê±πÌ™cØ∑	TKS7“p“Ø.`}.iä·ªºÍ…ñ∑U)Ä4ã
'3∆b(à‘œ◊vÑ0Î≤∂¡æ&L7çG∑U‹ŸîƒóD!ò<.∞Íá˜É˜ÎÅä7è-HÊ,éc’7ÿaß$§F∆Å=≠ ∆ù/ï
’)znrÁûó>◊S¢ò∫∫œ
bPñµf”ˆÁ*∞¶©åÄ*8ÎˇªB≥'OaNÂæ¸ôå1∑ƒN‚/pTsòÇÀ>ë˝OÌ°±œvr(bÓ%^ÀÊGŒÎû°˝ˆ-õ‹Hò+GP¸„_Eë≤ÛtÆiÎ˚ﬂVıJÒ¶£˘‡Ç|ï6Ì¯…ñ±ÿÕ¯?$êNŒï{é˘≤„’,·¯±2YÊGL¬ë§îMi~C¶Z?e˝›´∆æ”ÌÆ	é@)ËAÀÒ^≠ü§'ººy>hu‰—µ¡ﬁ§%2™¢Vú	˜ŒÚ≈.ãÇzŸ⁄~Ëøˇ`üe§æÎ•ÉU–õc≈èï⁄◊[E/(÷≈°O:€E∑’≤ËÕyÜ]êŒÈ¨KUï}.Óãä˝¶npfkvE´rQ@rÅ#”8$‘=LÓπ˙Ä| S‚JöΩ‘¬,ﬁ
=s‘ê©R≠3^M÷≥9X»Û'•ñW 6∑.—¨9É/™JÛ∞$◊‹È±√Iú¶!ÿO\ éÏ¯ÌªµË´XBnx(Á]•O∆ÉB¿ﬂ≈Wùmvù‘7ŸFﬂ¿Ô∫ÅÑsdE:ŒˇÂ´Œ>*ƒêπhw\êno˝i—Q7eî:k≥¡Ù}∆t{ÿÒˇI6æƒ¶∫ÿ◊ö¨ÎÒº
È4Ï∆®‹ˆÄ[€KÕ†ÄGÂ¯=≤@+ù]ot˝(ﬂ\˙Àƒ©Û'óg,∆?¨hv4.ªxÛ˘\1€´1œ‡ˇ8¶*W7x¯–∫]úx·£AÜz	>∞€ío’h¯µ [‚Ïº YYnÄÊ˘MGÁRZ‰∫9üëÚqakı@\¬èÑ&ëãÇïÈ.ƒ—FUøˆL≤'µ§·¶]◊Ö–óÃjÒGkˆ÷ºSëﬁeV√h«j∑àT·Çtêua≥Wø	∏òœ'~·ó˘ÑÔçÙd&$Ÿ‰\ÎlãD7¸Á ‹|GX7„0 î5Mz∫°µxè_8EjµÊbJIÆÅ¢Ó–òR∫®`ı±±2ó—¶ˇBq“fUØ¯äu≥ÉÜmI≤@Z¸≥ÃúﬁKì‚Bf◊ˆ≠S‘v3¸˛¨÷8óVód∫ìâ∆Áî1⁄j&nﬂn„„©‘ºÀÍÏ´éÉ´ﬁ1Æó»(œÕ“®∞>}	â)!Sœ»â÷î˜ï2Ωı1o‡∑˛Å‘Ovª€È⁄ˆ™µD˜˘÷˜KÕd_Ÿâõ4Q•÷◊SœÚπéJ±¸`F4J Æ\Ær”†—◊TæÍÁåØ™[rç÷<‹ˆb≤∞•kºXeë TÓoÅ√7C–ÈBÕml—	Ó≈äó∞ÔÑeAøÅ˜¢tÈ/⁄i˜ßvÄ∞0
ÓØ@+≤P5ÌˇcÑè-ô+ï÷Eq˙ò’—[¢≥fÕYˆ–øUg/6ÊõEz3/Ö'†∫à•"X®{a¬¯™åy§˙=0Å@~@ûŸ”Á˙•sJq ’^D’NotÙGpÙ$Cg4z‹§7áD`'HŒ‚ø∏r∑[yèîÏõTS	Z*6—äß˜T.†XhSÁUW+bÉ˛Î9y”>}D·ﬁ„ã]í¥∂ ÇI‡ªDûÙ0™he]/ñllZÍäFÅ¢«¡M2Ø°1Ê$°  +Õr>/-ÙÇ)É7æ—%∂±<‡©|lMì öëO¢+r•9Úö»ﬁ%ºÎ˜mR[£d¸<)ŒÉ"tZxΩ[ƒ:X¯ ˛≈B¯fH‹”í∂q†IÎcrºdœ„⁄“ xÿ‰îà9òÁµpøÕËÂΩô|xﬁr‡œY∏*ºë¯ˆ=a≈∏Øújˆô◊°ª®˛<-?Vj è5¯ Jjø=Â∫—≥—\E!‰}0WÊ·ÏS0}Å“öÉ}Ún2lüîÛﬂù˚äw ËT0%iÂ§Ñ®XZ0*pƒM’‘OlP≈–6°¨Ob9ñ≠C-ñ–@·é˛+obÎ^N¸ä¸YR7ê  ™ò~&aÏ¨π’Ø=wí¨ù8E∞GèË±{•µƒÃ9aäπ,˚lΩ=PNﬁ±dù&ˇ:≤_fxΩA“˙9h”Ì˝ÑªT$¨ìåißD‚lªZ’Ìù5IÑ≥8›'vRÌ}Rˇı˚y∂˝wz“Ï$Ö’óàH{Õ pÒF S⁄t1∆µ}Ü…»AœËµVÜÓÀÍDÃÚI“FÅQﬂÓßÑB‚äëY2¯VEéØŸı Ÿ¶˝?ˆà Ó/®ÇÉÉHÌ»˜0ˇ=`å`DÈM yŸúù‹ßHãı!`'≠J&C†¯€x˝|ƒÙtºê;L£p}"eÉﬁ$€4‚èm=wvÿI`√¯M¨CÆˆ⁄B_7ı^n‚selëî˘%ê¨ó9IÂÓ[b3;gÜGIMNï:ægÀvkƒ©Ø∫"h3“•»{∞/¶Ì®2ó˚“aX2ßØÊAõ¢Kx±˛Ï¿@X€´ÔHüU⁄A7t…nÍº‚ßöÔÉ
–´Clt.´ˆBn‹m‘o6ª≠[LÏOÑähÑ^g „æK€i„…&CCÎÎ§˙IŸ ‹%Ö'´Z¥ÅÒ2–∏«˛i∂µw∂Îgö=0``ılπx°>?L©Ì|hÊÃ\íÎãª?™oˆOù„5‡¨@ ÖÔVûï€∂±÷(úFÔt¶âêyöŸ›¢I}<¿aü<´ë4åw‡íjØ'˙\iëz‘}n1(gu”ÀÊ.j®Fª÷'Hñ%Bs£‚2Ùwµã–I	XËê3Æ∑"õœ∑5ÌÓ«kœµ∑∂≈PæÙîYq¡Å1rñÕ1ﬂ<6{°ò±ç1é7≠•·jãkhäXππøµ¬÷–´€$ÇnU&¸ü◊íír82!è‹QΩ‰!‚¥OπÿíU;{…±+°°¸/∂jU«ŒﬂJ˛Èü‚^ê§€Ò¨ƒ*ü∆Y—™J∆ËÉT 'z^Àa:9ﬁ—[ÛE∑í]∆¿8ÇE∑¸¯∑$≤ÇŒPîˆ	ñ¨‚)9`"ùËÖ™Ësƒ÷ÍX¸Ñÿä8Çœ/GÙöîxÀ∏.Ë>‹Ÿ„≠üv¡WÔÊ|G!Õ≠·H	O°—£hﬁ≤õ«˜¨*`ûîxäGáâÕ:êíe´˜ò`ÚcF„æ_Ÿï<ˆª√±Q°:Øêö,ÙOXçÀ-èk˙¢q®#Æaï“©R¯ÃN†°ËwOÃÔNwS€C4Ôû˛Û2Z*…`a≥ìáò¡≤4‘ﬂd·
êÜ¿…≠ÂØÕ∂6~e¯æv—W∏!ï¥kp)â≤EBÖ}áÂñÏ‘L%2SÉÍ¿Æ€nùèKÎµ+ª©qò»@yvôM~È§b).// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

[assembly:System.Reflection.AssemblyVersionAttribute("4.0.10.0")]
[assembly:System.Reflection.AssemblyCompanyAttribute("Xamarin, Inc.")]
[assembly:System.Reflection.AssemblyCopyrightAttribute("Copyright (c) 2013 Xamarin Inc. (http://www.xamarin.com)")]
[assembly:System.Reflection.AssemblyDefaultAliasAttribute("System.Threading.Tasks.dll")]
[assembly:System.Reflection.AssemblyDescriptionAttribute("System.Threading.Tasks.dll")]
[assembly:System.Reflection.AssemblyFileVersionAttribute("4.7.2046.0")]
[assembly:System.Reflection.AssemblyInformationalVersionAttribute("4.7.2046.0")]
[assembly:System.Reflection.AssemblyProductAttribute("Mono Common Language Infrastructure")]
[assembly:System.Reflection.AssemblyTitleAttribute("System.Threading.Tasks.dll")]
[assembly:System.Runtime.CompilerServices.ReferenceAssemblyAttribute]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.AggregateException))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.OperationCanceledException))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.AsyncTaskMethodBuilder))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.AsyncVoidMethodBuilder))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.ConfiguredTaskAwaitable))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.ConfiguredTaskAwaitable<>))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.IAsyncStateMachine))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.ICriticalNotifyCompletion))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.INotifyCompletion))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.TaskAwaiter))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.TaskAwaiter<>))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Runtime.CompilerServices.YieldAwaitable))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.CancellationToken))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.CancellationTokenRegistration))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.CancellationTokenSource))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.ConcurrentExclusiveSchedulerPair))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.Task))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.Task<>))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskCanceledException))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskCompletionSource<>))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskContinuationOptions))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskCreationOptions))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskExtensions))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskFactory))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskFactory<>))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskScheduler))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskSchedulerException))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.TaskStatus))]
[assembly:System.Runtime.CompilerServices.TypeForwardedToAttribute(typeof(System.Threading.Tasks.UnobservedTaskExceptionEventArgs))]
                                                                                                                                                                                                                                                                                                                                                                                                            â≈ÉÊËvhˇˇHâﬁLâÔHã+Ë∏^ˇˇHÖÌtHâﬁHâÔË®cˇˇHãH9≈tË;kˇˇLâ+HâﬂË0]ˇˇHã;È[ˇˇˇË„bˇˇHâ≈ÈëqˇˇHâ≈ÈÅqˇˇêfêAWAVAUATUâıSHâ˚HÅÏ8  dHã%(   HâÑ$(  1¿ÖˆtH∏ˇˇˇˇˇˇˇH9áÿ   ÖL  Lç´®   Lçd$`LâÔË¯fˇˇ∆Ciã{lâÈ∫   LâÊHçkH«É–       Ë√]ˇˇ∆Ci HâÔâ$ËteˇˇÄ{p Öö  ã$Ö¿àØ  Ñ©  HçD$0E1ˇHâD$Î.f.Ñ     Aˆ∆Ñ  HâﬂËŒhˇˇAÉ«IÉƒD9<$é∑   AãD$Eã4$âD$;Ct ãD$Hãt$Hç{xH«D$H    âD$0ËªZˇˇHã|$HHÖˇtHâD$HãHãD$HzËHçpË7bˇˇHãD$HÖ¿à¡  DâˆHâﬂÉÊâÚÉ Aˆ∆EÚâÚÉ Aˆ∆EÚâÚÉ AÉÊEÚHçÄAÉ«IÉƒHãÉÄ   HçT–Ëè[ˇˇD9<$èIˇˇˇAºˇˇˇˇH∏ˇˇˇˇˇˇˇHÉ{P HâÉÿ   Ñ«   HçC@Lçt$0Hâ$HçD$,HâD$Èä   Ä    IãHâ|$0HÖˇtHãLâˆHxËËs\ˇˇAãGHã<$∫   1ˆâD$,Ë´bˇˇ∆ChHâÔËO^ˇˇHã|$0Hãt$HãˇPHã|$0HÖˇtHãLâˆHxËË&aˇˇHâÔAº˛ˇˇˇË®cˇˇHÉ{P ∆Ch t!ø   Ës[ˇˇLã{HIãH9¡écˇˇˇHâãÿ   HâÔË‰]ˇˇHÉª∏    ÑÜ   E1ˆÎêIÉ∆L;≥∏   ssLâˆLâÔË®[ˇˇDãxHâ≈AÉˇ˛u€HãxDã`HãH ãU HãDâÊˇPÖ¿Ñ„   Hã}Eâ¸HÖˇtØHãHçuIÉ∆HxËËk`ˇˇH«E    L;≥∏   róf.Ñ     Hãå$(  dH3%(   Dâ‡Ö!  HÅƒ8  []A\A]A^A_√@ ËõXˇˇã8ÉˇÑ;˛ˇˇË€Yˇˇø   Hçﬂ7  Hç5⁄4  Hâ¡1¿Aº¸ˇˇˇË¯ZˇˇÈ˛ˇˇ ø   ËFZˇˇHã≥ÿ   Hâ«ËáeˇˇÖ¿àì¸ˇˇÖÌx9≈éá¸ˇˇâ≈ÈÄ¸ˇˇ@ ãUDâÊHâﬂËäYˇˇÈ
ˇˇˇD  DâÒHçñ7  Hç5a4  1¿ø   ËÉZˇˇÈ–¸ˇˇfD  DãD$DâÒø   1¿Hçö7  Hç5-4  ËVZˇˇÈ£¸ˇˇê∆Cp HâﬂAºˇˇˇˇËnZ