using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayerMasksService
{
    LayerMask hexGridMask {get; set;}
    LayerMask shipsMask {get; set;}
}
public class LayerMasksService : ILayerMasksService
{
    public LayerMask hexGridMask { get; set; }
    public LayerMask shipsMask { get; set; }

    public LayerMasksService (IConfigService configService)
    {
        foreach(var layerMask in configService.LayerMasksDatas)
        {
            switch(layerMask.type)
            {
                case LayerMaskType.HexGrid:
                hexGridMask = layerMask.layerMask;
                break;

                case LayerMaskType.Ship:
                shipsMask = layerMask.layerMask;
                break;
            }
        }
    }
}
