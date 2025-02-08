Range& Range)
		{
			{
				// do the top
				const int32 j = 0;

				for (int32 i = Range.begin(), I = Range.end(); i < I; ++i)
				{

					if (IterationGrid(i, j) == 0)
					{
						// probe all the neighbors
						const int32 Mask[4] = { IterationGrid(i - 1, j), IterationGrid(i + 1, j), 0, IterationGrid(i, j + 1) };
						const int32 Weight = Mask[0] + Mask[1] + Mask[2] + Mask[3];

						if (Weight != 0)
						{
							const ValueType ValueSamples[4] = { RasterGrid(i - 1, j), RasterGrid(i + 1, j), ValueType(ForceInitToZero), RasterGrid(i, j + 1) };
							const ValueType AverageValue = DotColor(Mask, ValueSamples) / float(Weight);

							RasterGrid(i, j) = AverageValue;

							// mark this texel as occupied for the next iteration of dilate()
							TopologyGrid(i, j) = 1;
						}
					}
				}
			}
		});

		Parallel_For(FI