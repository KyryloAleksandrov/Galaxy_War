using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface IMapFunctionalService
{
    GridSystem<GridObject> gridSystem {get;}

    List<GridPosition> GetNeighbourGridPositions(GridPosition gridPosition);
    List<GridPosition> GetNeighbourGridPositions(GridPosition gridPosition, int radius);
}
public class MapFunctionalService : IMapFunctionalService
{
    public GridSystem<GridObject> gridSystem {get; private set;}

    public MapFunctionalService(IConfigService ConfigService)
    {
        var MapData = ConfigService.MapData;
        gridSystem = new GridSystem<GridObject>(MapData.width, MapData.height, MapData.hexSize, (GridSystem<GridObject> gridSystem, GridPosition gridPosition) => new GridObject(gridSystem, gridPosition));
    }

    public List<GridPosition> GetNeighbourGridPositions(GridPosition gridPosition)
    {
        if(!gridSystem.IsInBounds(gridPosition))
        {
            return null;
        }

        List<GridPosition> positionsToTest = GetNeighboursToTest(gridPosition);

        List<GridPosition> neighbourPositions = new List<GridPosition>();
        foreach (var position in positionsToTest)
        {
            if(gridSystem.IsInBounds(position))
            {
                neighbourPositions.Add(position);
            }
        }

        return neighbourPositions;
    }

    public List<GridPosition> GetNeighbourGridPositions(GridPosition gridPosition, int radius)
    {
        if(!gridSystem.IsInBounds(gridPosition))
        {
            return null;
        }

        List<GridPosition> neighbourPositions = GetNeighbourGridPositions(gridPosition);
        List<GridPosition> positionsToTest = new List<GridPosition>();

        for(int i = 0; i < radius - 1; i++)
        {
            foreach(var position in neighbourPositions.ToList())    //ToList fixed the issue
            {
                positionsToTest = GetNeighboursToTest(position);
                foreach(var toTest in positionsToTest)
                {
                    if(gridSystem.IsInBounds(toTest) && !neighbourPositions.Contains(toTest))
                    {
                        neighbourPositions.Add(toTest);
                    }
                }
            } 
        }
        return neighbourPositions;
    }

    public List<GridPosition> GetNeighboursToTest(GridPosition gridPosition)
    {
        bool isOddRow = gridPosition.z % 2 == 1;
        List<GridPosition> positionsToTest = new List<GridPosition>
        {
            //top left
            new GridPosition(isOddRow ? gridPosition.x : gridPosition.x - 1, gridPosition.z + 1),
            //top right
            new GridPosition(isOddRow ? gridPosition.x + 1 : gridPosition.x, gridPosition.z + 1),

            //left
            new GridPosition(gridPosition.x - 1, gridPosition.z),
            //right
            new GridPosition(gridPosition.x + 1, gridPosition.z),

            //bottom left
            new GridPosition(isOddRow ? gridPosition.x : gridPosition.x - 1, gridPosition.z - 1),
            //bottom right
            new GridPosition(isOddRow ? gridPosition.x + 1 : gridPosition.x, gridPosition.z - 1)
        };

        return positionsToTest;
    }
}
