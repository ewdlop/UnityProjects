 = Mask[0] + Mask[1] + Mask[2] + Mask[3];

				if (Weight != 0)
				{
					const ValueType ValueSamples[4] = { ValueType(ForceInitToZero), RasterGrid(i + 1, j), ValueType(ForceInitToZero), RasterGrid(i, j + 1) };
					const ValueType AverageValue = DotColor(Mask, ValueSamples) / float(Weight);

					RasterGrid(i, j) = AverageValue;

					// mark this texel as occupied for the next iteration of dilate()
					TopologyGrid(i, j) = 1;
				}
			}

			i = Size.X - 1;
			j = 0;
			// this is an empty cell
			if (IterationGrid(i, j) == 0)
			{
				// probe all the neighbors
				const int32 Mask[4] = { IterationGrid(i - 1, j), 0, 0, IterationGrid(i, j + 1) };
				const int32 Weight = Mask[0] + Mask[1] + Mask[2] + Mask[3];

				if (Weight != 0)
				{
					const ValueType ValueSamples[4] = { RasterGrid(i - 1, j), ValueType(ForceInitToZero), ValueType(ForceInitToZero), RasterGrid(i, j + 1) };
					const ValueType AverageValue = DotColor(Mask, ValueSamples) / float(Weight);

					RasterGrid(i, j) = AverageValue;

					// mark this texel as occupied for the next iteration of dilate()
					TopologyGrid(i, j) = 1;
				}
			}

			i = Size.X - 1;
			j = Size.Y - 1;
			// this is an empty cell
			if (IterationGrid(i, j) == 0)
			{
				// probe all the neighbors
				const int32 Mask[4] = { IterationGrid(i - 1, j), 0, IterationGrid(i, j - 1), 0 };
				const int32 Weight = Mask[0] + Mask[1] + Mask[2] + Mask[3];

				if (Weight != 0)
				{
					const ValueType ValueSamples[4] = { RasterGrid(i - 1, j), ValueType(ForceInitToZero), RasterGrid(i, j - 1), ValueType(ForceInitToZero) };
					const ValueType AverageValue = DotColor(Mask, ValueSamples) / float(Weight);

					RasterGrid(i, j) = AverageValue;

					// mark this texel as occupied for the next iteration of dilate()
					Topo