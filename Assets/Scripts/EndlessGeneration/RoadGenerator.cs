using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneratorParameters
{
	public RoadBlock RoadBlock;
}

public class RoadGenerator
{	
	private RoadGeneratorParameters _parameters = null;
	private List<RoadBlock> _currentInstanciated = new List<RoadBlock>();

	public RoadGenerator(RoadGeneratorParameters parameters)
	{
		_parameters = parameters;
	}
}
